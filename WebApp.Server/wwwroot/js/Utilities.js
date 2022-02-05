
function setInLocalStorage(key, value) {
    localStorage[key] = value;
}

function getFromLocalStorage(key) {
    return localStorage[key];
}

function renderLanguageDropdown() {

	// Language Dropdown
	//----------------------------------------------------------------
	var langSwitcher = $('.lang-currency-switcher'),
		langToggle = $('.lang-currency-toggle');
	langToggle.on('click', function () {
		$(this).parent().addClass('open');
	});
	$(document).on('click', function (e) {
		if (!$(e.target).closest('.lang-currency-switcher').length) {
			$('.lang-currency-switcher').removeClass('open');
		}
	});


}


function renderToolbarDropdown() {
	// Toolbar Dropdown
	//------------------------------------------------------------
	var toolbarToggle = $('.toolbar-toggle'),
		toolbarDropdown = $('.toolbar-dropdown'),
		toolbarSection = $('.toolbar-section'),
		mobileMenu = $('.slideable-menu .menu');

	function closeToolBox() {
		toolbarToggle.removeClass('active');
		toolbarSection.removeClass('current');
	}

	toolbarToggle.on('click', function (e) {
		var currentValue = $(this).attr('href');
		if ($(e.target).is('.active')) {
			closeToolBox();
			toolbarDropdown.removeClass('open');
		} else {
			toolbarDropdown.addClass('open');

			closeToolBox();
			$(this).addClass('active');
			$(currentValue).addClass('current');
			mobileMenu.attr('data-height', mobileMenu.height());
		}
		e.preventDefault();
	});
	$('.close-dropdown').on('click', function () {
		toolbarDropdown.removeClass('open');
		toolbarToggle.removeClass('active');
		toolbarSection.removeClass('current');
	});

	// Slidable (Mobile) Menu
	//---------------------------------------------------------
	var backBtnText = 'Back',
		subMenu = $('.slideable-menu .slideable-submenu');
	subMenu.each(function () {
		$(this).prepend('<li class="back-btn"><a href="#">' + backBtnText + '</a></li>');
	});

	var hasChildLink = $('.has-children .sub-menu-toggle'),
		backBtn = $('.slideable-menu .slideable-submenu .back-btn');

	backBtn.on('click', function (e) {
		var self = this,
			parent = $(self).parent(),
			siblingParent = $(self).parent().parent().siblings().parent(),
			menu = $(self).parents('.menu'),
			menuInitHeight = $('.slideable-menu .menu').attr('data-height');
		parent.removeClass('in-view');
		siblingParent.removeClass('off-view');
		if (siblingParent.attr('class') === 'menu') {
			menu.css('height', menuInitHeight);
		} else {
			menu.css('height', siblingParent.height());
		}

		e.preventDefault();
	});

	hasChildLink.on('click', function (e) {
		var self = this,
			parent = $(self).parent().parent().parent(),
			menu = $(self).parents('.menu');

		parent.addClass('off-view');
		$(self).parent().parent().find('> .slideable-submenu').addClass('in-view');
		menu.css('height', $(self).parent().parent().find('> .slideable-submenu').height());

		e.preventDefault();
		return false;
	});
}

function renderScrollToTop() {

	// Animated Scroll to Top Button
	//-----------------------------------------------------------
	var $scrollTop = $('.scroll-to-top-btn');
	
	if ($scrollTop.length > 0) {
		$(window).on('scroll', function () {
			if ($(this).scrollTop() > 300) {
				$scrollTop.addClass('visible');
			} else {
				$scrollTop.removeClass('visible');
			}
		});
		$scrollTop.on('click', function (e) {			
			//$('ul.mega-menu').css('top', '105px');
			e.preventDefault();
			$('html').velocity('scroll', {
				offset: 0,
				duration: 1200,
				easing: 'easeOutExpo',
				mobileHA: false
			});
		});
	}
}

function renderPhotoSwipe() {
	// Gallery (Photoswipe)
	//------------------------------------------------------------------------------
	if ($('.gallery-wrapper').length) {

		var initPhotoSwipeFromDOM = function (gallerySelector) {

			// parse slide data (url, title, size ...) from DOM elements
			// (children of gallerySelector)
			var parseThumbnailElements = function (el) {
				var thumbElements = $(el).find('.gallery-item:not(.isotope-hidden)').get(),
					numNodes = thumbElements.length,
					items = [],
					figureEl,
					linkEl,
					size,
					item;

				for (var i = 0; i < numNodes; i++) {

					figureEl = thumbElements[i]; // <figure> element

					// include only element nodes
					if (figureEl.nodeType !== 1) {
						continue;
					}

					linkEl = figureEl.children[0]; // <a> element

					// create slide object
					if ($(linkEl).data('type') == 'video') {
						item = {
							html: $(linkEl).data('video')
						};
					} else {
						size = linkEl.getAttribute('data-size').split('x');
						item = {
							src: linkEl.getAttribute('href'),
							w: parseInt(size[0], 10),
							h: parseInt(size[1], 10)
						};
					}

					if (figureEl.children.length > 1) {
						// <figcaption> content
						item.title = $(figureEl).find('.caption').html();
					}

					if (linkEl.children.length > 0) {
						// <img> thumbnail element, retrieving thumbnail url
						item.msrc = linkEl.children[0].getAttribute('src');
					}

					item.el = figureEl; // save link to element for getThumbBoundsFn
					items.push(item);
				}

				return items;
			};

			// find nearest parent element
			var closest = function closest(el, fn) {
				return el && (fn(el) ? el : closest(el.parentNode, fn));
			};

			function hasClass(element, cls) {
				return (' ' + element.className + ' ').indexOf(' ' + cls + ' ') > -1;
			}

			// triggers when user clicks on thumbnail
			var onThumbnailsClick = function (e) {
				e = e || window.event;
				e.preventDefault ? e.preventDefault() : e.returnValue = false;

				var eTarget = e.target || e.srcElement;

				// find root element of slide
				var clickedListItem = closest(eTarget, function (el) {					
					return (hasClass(el, 'gallery-item'));
				});

				if (!clickedListItem) {
					return;
				}

				// find index of clicked item by looping through all child nodes
				// alternatively, you may define index via data- attribute
				var clickedGallery = clickedListItem.closest('.gallery-wrapper'),
					childNodes = $(clickedListItem.closest('.gallery-wrapper')).find('.gallery-item:not(.isotope-hidden)').get(),
					numChildNodes = childNodes.length,
					nodeIndex = 0,
					index;

				for (var i = 0; i < numChildNodes; i++) {
					if (childNodes[i].nodeType !== 1) {
						continue;
					}

					if (childNodes[i] === clickedListItem) {
						index = nodeIndex;
						break;
					}
					nodeIndex++;
				}

				if (index >= 0) {
					// open PhotoSwipe if valid index found
					openPhotoSwipe(index, clickedGallery);
				}
				return false;
			};

			// parse picture index and gallery index from URL (#&pid=1&gid=2)
			var photoswipeParseHash = function () {
				var hash = window.location.hash.substring(1),
					params = {};

				if (hash.length < 5) {
					return params;
				}

				var vars = hash.split('&');
				for (var i = 0; i < vars.length; i++) {
					if (!vars[i]) {
						continue;
					}
					var pair = vars[i].split('=');
					if (pair.length < 2) {
						continue;
					}
					params[pair[0]] = pair[1];
				}

				if (params.gid) {
					params.gid = parseInt(params.gid, 10);
				}

				return params;
			};

			var openPhotoSwipe = function (index, galleryElement, disableAnimation, fromURL) {
				var pswpElement = document.querySelectorAll('.pswp')[0],
					gallery,
					options,
					items;

				items = parseThumbnailElements(galleryElement);

				// define options (if needed)
				options = {

					closeOnScroll: false,

					// define gallery index (for URL)
					galleryUID: galleryElement.getAttribute('data-pswp-uid'),

					getThumbBoundsFn: function (index) {
						// See Options -> getThumbBoundsFn section of documentation for more info
						var thumbnail = items[index].el.getElementsByTagName('img')[0]; // find thumbnail
						if ($(thumbnail).length > 0) {
							var pageYScroll = window.pageYOffset || document.documentElement.scrollTop,
								rect = thumbnail.getBoundingClientRect();

							return {
								x: rect.left,
								y: rect.top + pageYScroll,
								w: rect.width
							};
						}
					}

				};

				// PhotoSwipe opened from URL
				if (fromURL) {
					if (options.galleryPIDs) {
						// parse real index when custom PIDs are used
						// http://photoswipe.com/documentation/faq.html#custom-pid-in-url
						for (var j = 0; j < items.length; j++) {
							if (items[j].pid == index) {
								options.index = j;
								break;
							}
						}
					} else {
						// in URL indexes start from 1
						options.index = parseInt(index, 10) - 1;
					}
				} else {
					options.index = parseInt(index, 10);
				}

				// exit if index not found
				if (isNaN(options.index)) {
					return;
				}

				if (disableAnimation) {
					options.showAnimationDuration = 0;
				}

				// Pass data to PhotoSwipe and initialize it
				gallery = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, items, options);
				gallery.init();

				gallery.listen('beforeChange', function () {
					var currItem = $(gallery.currItem.container);
					$('.pswp__video').removeClass('active');
					var currItemIframe = currItem.find('.pswp__video').addClass('active');
					$('.pswp__video').each(function () {
						if (!$(this).hasClass('active')) {
							$(this).attr('src', $(this).attr('src'));
						}
					});
				});

				gallery.listen('close', function (event) {

					console.log('close was clicked 1034');
					$('.pswp').removeClass('pswp--open');

					var uri = window.location.toString();
					if (uri.indexOf("#") > 0) {
						var clean_uri = uri.substring(0, uri.indexOf("#"));
						window.history.replaceState({}, document.title, clean_uri);
					}

					

					$('.pswp__video').each(function () {
						$(this).attr('src', $(this).attr('src'));
					});

					preventDefault;
				});

			};

			// loop through all gallery elements and bind events
			var galleryElements = document.querySelectorAll(gallerySelector);

			for (var i = 0, l = galleryElements.length; i < l; i++) {
				galleryElements[i].setAttribute('data-pswp-uid', i + 1);
				galleryElements[i].onclick = onThumbnailsClick
			}

			// Parse URL and open gallery if it contains #&pid=3&gid=1
			var hashData = photoswipeParseHash();
			if (hashData.pid && hashData.gid) {
				openPhotoSwipe(hashData.pid, galleryElements[hashData.gid - 1], true, true);
			}

		};

		// execute above function
		initPhotoSwipeFromDOM('.gallery-wrapper');
	}
}

function renderProductGallery() {
	// Product Gallery
	//------------------------------------------------------------------------------
	var $productCarousel = $('.product-carousel');
	if ($productCarousel.length) {

		// Carousel init
		$productCarousel.owlCarousel({
			items: 1,
			loop: false,
			dots: false,
			URLhashListener: true,
			startPosition: 'URLHash',
			onTranslate: activeHash
		});

		function activeHash(e) {
			var i = e.item.index;
			var $activeHash = $('.owl-item').eq(i).find('[data-hash]').attr('data-hash');
			$('.product-thumbnails li').removeClass('active');
			$('[href="#' + $activeHash + '"]').parent().addClass('active');
			$('.gallery-wrapper .gallery-item').removeClass('active');
			$('[data-hash="' + $activeHash + '"]').parent().addClass('active');

		}
	}

	var productThumb = $('ul.product-thumbnails li > a');

	productThumb.on('click', function (e) {

		// MY MOD
		//alert('thumb click : ' + e);
		//console.log(e)
		//alert($(this).attr("id"));
		$('ul.product-thumbnails li').removeClass("active");
		$('.gallery-item a > img').attr('src', $(this).attr("id"));
		$(this).parent().addClass("active");

		//if ($(e.target).parent().is('.expanded')) {
		//    closeCategorySubmenu();
		//} else {
		//    closeCategorySubmenu();
		//    $(this).parent().addClass('expanded');
		//}

	});
}

function renderHeroSlider() {
	/*!
	 * data-owl-carousel - wrapper for owlCarousel with data-attributes support
	 */

		!function (a, b, c) { "use strict"; a.fn.siCarousel = function () { return "undefined" == typeof a.fn.owlCarousel ? this : this.each(function () { var b = a(this), c = a.extend(!0, {}, a.fn.siCarousel.defaults, b.data("owl-carousel")); b.owlCarousel(c) }) }, a.fn.siCarousel.defaults = { items: 1, loop: !1, nav: !1, navText: [], dots: !0, slideBy: 1, lazyLoad: !1, autoplay: !1, autoplayTimeout: 4e3, responsive: {}, animateOut: !1, animateIn: !1, smartSpeed: 450, navSpeed: 450 }, a(function () { a("[data-owl-carousel]").siCarousel() }) }(jQuery, window, document);

}

function renderHorizontalScroll() {
	// https://codepen.io/mahish/pen/RajmQw
	// Arrow buttons for responsive horizontal scroll menu
	// duration of scroll animation
	var scrollDuration = 300;
	// paddles
	var leftPaddle = document.getElementsByClassName('left-paddle');
	var rightPaddle = document.getElementsByClassName('right-paddle');
	// get items dimensions
	var itemsLength = $('.item').length;
	var itemSize = $('.item').outerWidth(true);
	// get some relevant size for the paddle triggering point
	var paddleMargin = 20;

	// get wrapper width
	var getMenuWrapperSize = function () {
		return $('.menu-wrapper').outerWidth();
	}
	var menuWrapperSize = getMenuWrapperSize();
	// the wrapper is responsive
	$(window).on('resize', function () {
		menuWrapperSize = getMenuWrapperSize();
	});
	// size of the visible part of the menu is equal as the wrapper size 
	var menuVisibleSize = menuWrapperSize;

	// get total width of all menu items
	var getMenuSize = function () {
		return itemsLength * itemSize;
	};
	var menuSize = getMenuSize();
	// get how much of menu is invisible
	var menuInvisibleSize = menuSize - menuWrapperSize;

	// get how much have we scrolled to the left
	var getMenuPosition = function () {
		return $('.menu').scrollLeft();
	};

	// finally, what happens when we are actually scrolling the menu
	$('.menu').on('scroll', function () {

		// get how much of menu is invisible
		menuInvisibleSize = menuSize - menuWrapperSize;
		// get how much have we scrolled so far
		var menuPosition = getMenuPosition();

		var menuEndOffset = menuInvisibleSize - paddleMargin;

		// show & hide the paddles 
		// depending on scroll position
		if (menuPosition <= paddleMargin) {
			$(leftPaddle).addClass('hidden');
			$(rightPaddle).removeClass('hidden');
		} else if (menuPosition < menuEndOffset) {
			// show both paddles in the middle
			$(leftPaddle).removeClass('hidden');
			$(rightPaddle).removeClass('hidden');
		} else if (menuPosition >= menuEndOffset) {
			$(leftPaddle).removeClass('hidden');
			$(rightPaddle).addClass('hidden');
		}

		// print important values
		$('#print-wrapper-size span').text(menuWrapperSize);
		$('#print-menu-size span').text(menuSize);
		$('#print-menu-invisible-size span').text(menuInvisibleSize);
		$('#print-menu-position span').text(menuPosition);

	});

	// scroll to left
	$(rightPaddle).on('click', function () {
		$('.menu').animate({ scrollLeft: menuInvisibleSize }, scrollDuration);
	});

	// scroll to right
	$(leftPaddle).on('click', function () {
		$('.menu').animate({ scrollLeft: '0' }, scrollDuration);
	});
}

function renderInitMap() {

	var map;

	var locations = [
		['<b>Warehousing Solutions</b><br />Building No. 144, Street 10, <br /> Zone No. 57, Industrial Area<br />Tel: +974 4035 2439', 25.208514, 51.430180, 1]
	];

	map = new google.maps.Map(document.getElementById('map'), {
		center: new google.maps.LatLng(25.208, 51.430),
		zoom: 16,
		mapTypeId: google.maps.MapTypeId.ROADMAP
	});

	var infowindow = new google.maps.InfoWindow();

	var marker, i;

	for (i = 0; i < locations.length; i++) {
		marker = new google.maps.Marker({
			position: new google.maps.LatLng(locations[i][1], locations[i][2]),
			map: map,
			icon: '/MyAssets/resources/aab-marker.png'
		});

		google.maps.event.addListener(marker, 'click', (function (marker, i) {
			return function () {
				infowindow.setContent(locations[i][0]);
				infowindow.open(map, marker);
			}
		})(marker, i));
	} //Enf For

} //End Map

function renderScrollToSection(section) {
	//document.getElementById(section).scrollIntoView({ behavior: 'smooth' });
	//console.log('its scroll...');

	// Animated Scroll to Top Button
	//-----------------------------------------------------------
	var $scrollSection = $('.scroll-to-section');

	//var el = document.getElementsByClassName('scroll-to-section');
	//alert("....");
	//console.log($scrollSection);

	//if (el.addEventListener) {
		//alert("....");
	//} 
	//	el.addEventListener("click", function () {
	//		alert("clicked");
			document.getElementById(section).scrollIntoView({ behavior: 'smooth' });
	//	}, false);

	//$scrollSection.on('click', function (e) {
	//	//alert('Im loaded : ' + section + ' : ' + $scrollSection);

	//		document.getElementById(section).scrollIntoView({ behavior: 'smooth' });
	//});

	$scrollSection.on('click', function (e) {
			e.preventDefault();
			//$('html').velocity('scroll', {
			//	offset: 0,
			//	duration: 1200,
			//	easing: 'easeOutExpo',
			//	mobileHA: false
			//});
		//alert('can you scroll again : ' + section);
			document.getElementById(section).scrollIntoView({ behavior: 'smooth' });

		});
	//if ($scrollSection.length > 0) {
	//}
}

function renderScrollTopValue() {

	//var noScroll = "105px";
	//var withScroll = "64px";
	//if ($(this).scrollTop() > 0) {
	//	//alert($(this).scrollTop());
	//	$('ul.mega-menu').css('top', withScroll);
	//	return;
	//}

	//$(window).on('scroll', function () {
			
	//	if ($(this).scrollTop() == 10) {
	//		alert("jin " + $(this).scrollTop());
	//		$('ul.mega-menu').css('top', noScroll);
	//		return;
	//	}

	//	if ($(this).scrollTop() > 0) {
	//		//alert("else: " + $(this).scrollTop());
	//		$('ul.mega-menu').css('top', withScroll);
	//		return;
	//	}
		
	//});


    var defaultValue = "105px";
	var scrollValue = "64px";

	$(window).on('scroll', function () {
		//alert('add top0');
		if ($(this).scrollTop() == 0) {
			//$('#headMenu').removeClass('top-0');
			$('ul.mega-menu').css('top', defaultValue);
		} else {
			//$('#headMenu').addClass('top-0');
			$('ul.mega-menu').css('top', scrollValue);
		}		
	});

    if ($(this).scrollTop() > 0) {
        //alert($(this).scrollTop());
		$('ul.mega-menu').css('top', scrollValue);
        return;
	}
}

function renderCountTo() {
	function getScrollTop() {
		if (typeof pageYOffset != 'undefined') {
			return pageYOffset;
		}
		else {
			var b = document.body; //IE 'quirks'
			var d = document.documentElement; //IE with doctype
			d = (d.clientHeight) ? d : b;
			return d.scrollTop;
		}
	}    

	$(window).on("scroll", function () {
	//alert('go back only');
		if (getScrollTop() >= 2000) {

			//$(window).off("scroll");
			
			//-- Executing
			$('.number-counter').countTo();
		};
	});


	//-- Plugin implementation
	(function ($) {
		$.fn.countTo = function (options) {
			return this.each(function () {
				//-- Arrange
				var FRAME_RATE = 60; // Predefine default frame rate to be 60fps
				var $el = $(this);
				var countFrom = parseInt($el.attr('data-count-from'));
				var countTo = parseInt($el.attr('data-count-to'));
				var countSpeed = $el.attr('data-count-speed'); // Number increment per second

				//-- Action
				var rafId;
				var increment;
				var currentCount = countFrom;
				var countAction = function () {              // Self looping local function via requestAnimationFrame
					if (currentCount < countTo) {              // Perform number incremeant
						$el.text(Math.floor(currentCount));     // Update HTML display
						increment = countSpeed / FRAME_RATE;    // Calculate increment step
						currentCount += increment;              // Increment counter
						rafId = requestAnimationFrame(countAction);
					} else {                                  // Terminate animation once it reaches the target count number
						$el.text(countTo);                      // Set to the final value before everything stops
						//cancelAnimationFrame(rafId);
					}
				};
				rafId = requestAnimationFrame(countAction); // Initiates the looping function
			});
		};
	}(jQuery));

}



function renderCKEditorConfig() {
	/**
	* @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
	* For licensing, see LICENSE.md or http://ckeditor.com/license
	*/

	CKEDITOR.editorConfig = function (config) {
		// Define changes to default configuration here.
		// For complete reference see:
		// http://docs.ckeditor.com/#!/api/CKEDITOR.config

		// The toolbar groups arrangement, optimized for two toolbar rows.

		//config.uiColor = '#F7B42C';

		//config.toolbar = 'Full';
		//config.toolbar_Full =
		//[
		//    { name: 'document', items: ['Source', '-', 'Save', 'NewPage', 'DocProps', 'Preview', 'Print', '-', 'Templates'] },
		//    { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
		//    { name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'SpellChecker', 'Scayt'] },
		//    {
		//        name: 'forms', items: ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton',
		//          'HiddenField']
		//    },
		//    '/',
		//    { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
		//    {
		//        name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv',
		//        '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl']
		//    },
		//    { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
		//    { name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },
		//    '/',
		//    { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
		//    { name: 'colors', items: ['TextColor', 'BGColor'] },
		//    { name: 'tools', items: ['Maximize', 'ShowBlocks', '-', 'About'] }
		//];



		//config.toolbar = [
		//    { name: 'document', items: ['Source', '-', 'Save', 'NewPage', 'Preview', 'Print', '-', 'Templates'] },
		//    { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
		//    { name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'Scayt'] },
		//    { name: 'forms', items: ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'] },
		//    '/',
		//    { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
		//    { name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl', 'Language'] },
		//    { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
		//    { name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },
		//    '/',
		//    { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
		//    { name: 'colors', items: ['TextColor', 'BGColor'] },
		//    { name: 'tools', items: ['Maximize', 'ShowBlocks'] },
		//    { name: 'about', items: ['About'] }
		//];



		config.toolbarGroups = [
			{ name: 'clipboard', groups: ['clipboard', 'undo'] },
			{ name: 'editing', groups: ['find', 'selection', 'spellchecker'] },
			{ name: 'links' },
			{ name: 'insert' },
			{ name: 'forms' },
			{ name: 'tools' },
			{ name: 'document', groups: ['mode', 'document', 'doctools'] },
			{ name: 'others' },
			'/',
			{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
			{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
			{ name: 'styles' },
			{ name: 'colors' },
			{ name: 'about' }
		];







		// Remove some buttons provided by the standard plugins, which are
		// not needed in the Standard(s) toolbar.
		config.removeButtons = 'Underline,Subscript,Superscript';

		// Set the most common block elements.
		config.format_tags = 'p;h1;h2;h3;pre';

		// Simplify the dialog windows.
		config.removeDialogTabs = 'image:advanced;link:advanced';

		//// Simplify allow the inline CSS and Classes.
		//config.allowedContent = true;
	};
}

function renderCKEditorFileman1() {

	var roxyFileman1 = 'lib/fileman/index1.html';

	CKEDITOR.replace('Description', {
		filebrowserBrowseUrl: roxyFileman1,
		height: 400,
		allowedContent: true // Simply allow the inline CSS and Classes.
	});
}

function renderCKEditorFileman2() {
	var roxyFileman2 = 'lib/fileman/index2.html';

	CKEDITOR.replace('DescriptionAr', {
		filebrowserBrowseUrl: roxyFileman2,
		height: 400,
		allowedContent: true // Simply allow the inline CSS and Classes.
	});
}

function renderCustomImage1() {
	/*
  RoxyFileman - web based file manager. Ready to use with CKEditor, TinyMCE. 
  Can be easily integrated with any other WYSIWYG editor or CMS.

  Copyright (C) 2013, RoxyFileman.com - Lyubomir Arsov. All rights reserved.
  For licensing, see LICENSE.txt or http://RoxyFileman.com/license

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program.  If not, see <http://www.gnu.org/licenses/>.

  Contact: Lyubomir Arsov, liubo (at) web-lobby.com
*/

	function FileSelected(file) {
		/**
		 * file is an object containing following properties:
		 * 
		 * fullPath - path to the file - absolute from your site root
		 * path - directory in which the file is located - absolute from your site root
		 * size - size of the file in bytes
		 * time - timestamo of last modification
		 * name - file name
		 * ext - file extension
		 * width - if the file is image, this will be the width of the original image, 0 otherwise
		 * height - if the file is image, this will be the height of the original image, 0 otherwise
		 * 
		 */


		//alert('c# invoke');
		$(window.parent.document).find('#customImage1').attr('src', file.fullPath);
		$(window.parent.document).find('#ImageUrlToInput1').attr('value', file.fullPath);
		window.parent.closeCustomImage1();
	}
	function GetSelectedValue() {
		/**
		* This function is called to retrieve selected value when custom integration is used.
		* Url parameter selected will override this value.
		*/

		return "";
	}
}

function renderCustomImage2() {
	/*
  RoxyFileman - web based file manager. Ready to use with CKEditor, TinyMCE. 
  Can be easily integrated with any other WYSIWYG editor or CMS.

  Copyright (C) 2013, RoxyFileman.com - Lyubomir Arsov. All rights reserved.
  For licensing, see LICENSE.txt or http://RoxyFileman.com/license

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program.  If not, see <http://www.gnu.org/licenses/>.

  Contact: Lyubomir Arsov, liubo (at) web-lobby.com
*/

	function FileSelected(file) {
		/**
		 * file is an object containing following properties:
		 * 
		 * fullPath - path to the file - absolute from your site root
		 * path - directory in which the file is located - absolute from your site root
		 * size - size of the file in bytes
		 * time - timestamo of last modification
		 * name - file name
		 * ext - file extension
		 * width - if the file is image, this will be the width of the original image, 0 otherwise
		 * height - if the file is image, this will be the height of the original image, 0 otherwise
		 * 
		 */

		//alert('c# invoke');
		$(window.parent.document).find('#customImage2').attr('src', file.fullPath);
		$(window.parent.document).find('#ImageUrlToInput2').attr('value', file.fullPath);
		window.parent.closeCustomImage1();
	}
	function GetSelectedValue() {
		/**
		* This function is called to retrieve selected value when custom integration is used.
		* Url parameter selected will override this value.
		*/

		return "";
	}
}

function renderCustomImage3() {
	/*
  RoxyFileman - web based file manager. Ready to use with CKEditor, TinyMCE. 
  Can be easily integrated with any other WYSIWYG editor or CMS.

  Copyright (C) 2013, RoxyFileman.com - Lyubomir Arsov. All rights reserved.
  For licensing, see LICENSE.txt or http://RoxyFileman.com/license

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program.  If not, see <http://www.gnu.org/licenses/>.

  Contact: Lyubomir Arsov, liubo (at) web-lobby.com
*/

	function FileSelected(file) {
		/**
		 * file is an object containing following properties:
		 * 
		 * fullPath - path to the file - absolute from your site root
		 * path - directory in which the file is located - absolute from your site root
		 * size - size of the file in bytes
		 * time - timestamo of last modification
		 * name - file name
		 * ext - file extension
		 * width - if the file is image, this will be the width of the original image, 0 otherwise
		 * height - if the file is image, this will be the height of the original image, 0 otherwise
		 * 
		 */


		//alert('c# invoke');
		$(window.parent.document).find('#customImage3').attr('src', file.fullPath);
		$(window.parent.document).find('#ImageUrlToInput3').attr('value', file.fullPath);
		window.parent.closeCustomImage1();
	}
	function GetSelectedValue() {
		/**
		* This function is called to retrieve selected value when custom integration is used.
		* Url parameter selected will override this value.
		*/

		return "";
	}
}

function renderCustomImage4() {
	/*
  RoxyFileman - web based file manager. Ready to use with CKEditor, TinyMCE. 
  Can be easily integrated with any other WYSIWYG editor or CMS.

  Copyright (C) 2013, RoxyFileman.com - Lyubomir Arsov. All rights reserved.
  For licensing, see LICENSE.txt or http://RoxyFileman.com/license

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program.  If not, see <http://www.gnu.org/licenses/>.

  Contact: Lyubomir Arsov, liubo (at) web-lobby.com
*/

	function FileSelected(file) {
		/**
		 * file is an object containing following properties:
		 * 
		 * fullPath - path to the file - absolute from your site root
		 * path - directory in which the file is located - absolute from your site root
		 * size - size of the file in bytes
		 * time - timestamo of last modification
		 * name - file name
		 * ext - file extension
		 * width - if the file is image, this will be the width of the original image, 0 otherwise
		 * height - if the file is image, this will be the height of the original image, 0 otherwise
		 * 
		 */

		//alert('c# invoke');
		$(window.parent.document).find('#customImage4').attr('src', file.fullPath);
		$(window.parent.document).find('#ImageUrlToInput4').attr('value', file.fullPath);
		window.parent.closeCustomImage1();
	}
	function GetSelectedValue() {
		/**
		* This function is called to retrieve selected value when custom integration is used.
		* Url parameter selected will override this value.
		*/

		return "";
	}
}

function returnCustomImage1() {
	var retVal = $('#customImage1').attr('src');
	return retVal;
}

function returnCustomImage2() {
	var retVal = $('#customImage2').attr('src');
	return retVal;
}

function returnCustomImage3() {
	var retVal = $('#customImage3').attr('src');
	return retVal;
}

function returnCustomImage4() {
	var retVal = $('#customImage4').attr('src');
	return retVal;
}


function returnDescriptionValue() {
	var retVal = $('#Description').val();
	return retVal;
}




function returnRunReCaptcha() {

	return new Promise((resolve, reject) => {
		grecaptcha.ready(function () {
			grecaptcha.execute('6LemEtoaAAAAAD_ba4afxBPRBTXordqJCPe9A_cJ', { action: 'submit' }).then(function (token) {
				resolve(token);
			});
		});
	});

}



function dotnetTestInvokeAsync() {
	DotNet.invokeMethodAsync("AABWS", "GetCurrentCount")
		.then(result => {
			console.log("count from javascript Utilities : " + result);
		});
}

function dotnetInstanceInvocation(dotnetHelper) {
	dotnetHelper.invokeMethodAsync("IncrementCount");
}

function sayHello (dotnetHelper) {
	return dotnetHelper.invokeMethodAsync('SayHello')
		.then(r => console.log(r));
}

function sweetAlert(title, imgIcon, showDenyButton, showCancelButton, confirmButtonText, denyButtonText) {
	return Swal.fire({
		title: title,
		icon: imgIcon,
		showDenyButton: showDenyButton,
		showCancelButton: showCancelButton,
		confirmButtonText: confirmButtonText,
		denyButtonText: denyButtonText,
	})
		.then((result) => {
		/* Read more about isConfirmed, isDenied below */
		if (result.isConfirmed) {
			Swal.fire('Saved!', '', 'success')
			return true;
		} else if (result.isDenied) {
			Swal.fire('Changes are not saved', '', 'info')
			return false;
		}
	})
}