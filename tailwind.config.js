/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./Views/**/*.cshtml",
        "./Pages/**/*.cshtml",
        "./wwwroot/css/**/*.css",
        "./Controllers/**/*.cs",
        "./Models/**/*.cs"
    ],
    theme: {
        extend: {},
    },
    plugins: [],
}
