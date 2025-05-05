// Load plugins
var gulp = require('gulp');
var sass = require('gulp-sass')(require('sass'));
var cleanCss = require('gulp-clean-css');
var rename = require('gulp-rename');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');

gulp.task('styles', function (done) {
    gulp.src('Areas/Innovation/Content/scss/style.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(gulp.dest('Areas/Innovation/Content/dist/css/'));

    done();
});

gulp.task('minify-css', function () {
    return gulp.src('Areas/Innovation/Content/dist/css/style.css')
        .pipe(cleanCss())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest('Areas/Innovation/Content/dist/css/'));
});

gulp.task('scripts', function () {
    return gulp.src(['Areas/Innovation/Scripts/js/*.js'])
        .pipe(concat({ path: 'bundle.js' }))
        .pipe(gulp.dest('Areas/Innovation/Content/dist/js/'));
});

gulp.task('minify-js', function () {
    return gulp.src('Areas/Innovation/Content/dist/js/bundle.js')
        .pipe(uglify())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest('Areas/Innovation/Content/dist/js/'));
});


gulp.task('watch', function () {
    gulp.watch('Areas/Innovation/Content/scss/**/*.scss', gulp.series('styles'));
    gulp.watch('Areas/Innovation/Content/dist/css/style.css', gulp.series('minify-css'));
    gulp.watch('Areas/Innovation/Scripts/**/*.js', gulp.series('scripts'));
    gulp.watch('Areas/Innovation/Content/dist/js/bundle.js', gulp.series('minify-js'));
});

const buildAll = gulp.series(
    gulp.series('styles'),
    gulp.series('scripts'),
    gulp.series('minify-css'),
    gulp.series('minify-js')
);

exports.default = buildAll;