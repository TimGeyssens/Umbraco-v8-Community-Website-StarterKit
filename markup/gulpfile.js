/// <binding ProjectOpened='watcher' />
const { src, dest, parallel, series, watch } = require('gulp');
const sourcemaps = require('gulp-sourcemaps');
const postCSS = require('gulp-postcss');
const purgeCSS = require('gulp-purgecss');

const minifyJS = require('gulp-uglify');

const concat = require('gulp-concat');
const rename = require('gulp-rename');

function css() {
    return src('./styles/main.css')
        .pipe(sourcemaps.init())
        .pipe(postCSS([
            require('tailwindcss'),
            require('cssnano')
        ]))
        .pipe(purgeCSS({
            content: ['./Views/**/*.cshtml', 'html/**/*.html']
        }))
        .pipe(rename("main.min.css"))
        .pipe(sourcemaps.write('.', { includeContent: false, sourceRoot: '../src/styles' }))
        .pipe(dest('./css'));
}

function js() {
    return src('./scripts/**/*.js')
        .pipe(sourcemaps.init())
        .pipe(concat('main.js'))
        .pipe(minifyJS())
        .pipe(rename("main.min.js"))
        .pipe(sourcemaps.write('.', { includeContent: false, sourceRoot: '../src/scripts' }))
        .pipe(dest('./js'));
}

function watcher() {
    watch(['./styles/main.css', './Views/**/*.cshtml', './html/**/*.html'], css);
    watch('./scripts/**/*.js', js);
}

exports.js = js;
exports.css = css;
exports.default = parallel(css, js);
exports.watcher = series(parallel(css, js), watcher);
