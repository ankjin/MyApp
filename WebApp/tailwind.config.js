const colors = require('tailwindcss/colors');

module.exports = {
    mode: 'jit',
    purge: {
        //content: ['./**/*.html', './**/*.razor'],
        content: ['./**/*.html', './**/*.cshtml', './**/*.razor', './Pages/**/*.razor', './Shared/**/*.razor'],
    },
    darkMode: false, // or 'media' or 'class'
    theme: {
        extend: {
            colors: {
                appWhite: '#fdfefe',
                appBlack: '#00254b',
                appGray: '#f2f5f8',
                appBlue1: '#8fa9ce',
                appBlue2: '#2e79a8',
            },
        },
    },
    variants: {
        extend: {},
    },
    plugins: [
        require('@tailwindcss/forms'),
    ],
}