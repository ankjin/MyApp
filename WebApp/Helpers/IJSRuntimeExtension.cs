using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace WebApp.Helpers
{
    public static class IJSRuntimeExtension
    {

        public static async ValueTask<bool> Confirm(this IJSRuntime js, string msg)
        {
            return await js.InvokeAsync<bool>("confirm", msg);
        }
        public static async ValueTask ConsoleLog(this IJSRuntime js, string msg)
        {
            await js.InvokeVoidAsync("console.log", msg);
        }

        public static async ValueTask LanguageDropdown(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("renderLanguageDropdown");
        }
        public static async ValueTask ToolbarDropdown(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("renderToolbarDropdown");
        }
        public static async ValueTask PhotoSwipe(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("renderPhotoSwipe");
        }
        public static async ValueTask ProductGallery(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("renderProductGallery");
        }
        public static async ValueTask ScrollToTop(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("renderScrollToTop");
        }
        public static async ValueTask ScrollToSection(this IJSRuntime js, string section)
        {
            await js.InvokeVoidAsync("renderScrollToSection", section);
        }
        public static async ValueTask ScrollTopValue(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("renderScrollTopValue");
        }
        public static async ValueTask CountTo(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("renderCountTo");
        }




        #region CKEditor Fileman

        public static async ValueTask CKEditorConfig(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("renderCKEditorConfig");
        }
        public static async ValueTask CKEditorFileman1(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("renderCKEditorFileman1");
        }
        public static async ValueTask CKEditorFileman2(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("renderCKEditorFileman2");
        }

        public static async ValueTask CustomImage1(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("renderCustomImage1");
        }
        public static async ValueTask CustomImage2(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("renderCustomImage2");
        }
        public static async ValueTask CustomImage3(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("renderCustomImage3");
        }
        public static async ValueTask CustomImage4(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("renderCustomImage4");
        }
        public static async ValueTask<string> ReturnCustomImage1(this IJSRuntime js)
        {
            return await js.InvokeAsync<string>("returnCustomImage1");
        }
        public static async ValueTask<string> ReturnCustomImage2(this IJSRuntime js)
        {
            return await js.InvokeAsync<string>("returnCustomImage2");
        }
        public static async ValueTask<string> ReturnCustomImage3(this IJSRuntime js)
        {
            return await js.InvokeAsync<string>("returnCustomImage3");
        }
        public static async ValueTask<string> ReturnCustomImage4(this IJSRuntime js)
        {
            return await js.InvokeAsync<string>("returnCustomImage4");
        }
        public static async ValueTask<string> ReturnDescriptionValue(this IJSRuntime js)
        {
            return await js.InvokeAsync<string>("returnDescriptionValue");
        }
        #endregion


        #region Google ReCaptcha
        public static async ValueTask<string> RunReCaptcha(this IJSRuntime js)
        {
            return await js.InvokeAsync<string>("returnRunReCaptcha");
        }

        #endregion





        public static async ValueTask HeroSlider(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("renderHeroSlider");
        }

        public static async ValueTask HorizontalScroll(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("renderHorizontalScroll");
        }


        public static async ValueTask InitMap(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("renderInitMap");
        }

        public static async ValueTask HorizontalScrollTwo(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("renderHorizontalScrollTwo");
        }

        public static async ValueTask<bool> ConfirmMessage(this IJSRuntime js, string title, string confirmButtonText, string denyButtonText)
        {
            return await js.InvokeAsync<bool>("sweetAlert", title, "question", true, false, confirmButtonText, denyButtonText);
        }
    }
}
