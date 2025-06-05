// Load plugins
var gulp = require('gulp');
var sass = require('gulp-sass')(require('sass'));
var cleanCss = require('gulp-clean-css');
var rename = require('gulp-rename');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
const path = require('path');

// Rollup
const { rollup } = require('rollup');
const resolve = require('@rollup/plugin-node-resolve').nodeResolve;
const commonjs = require('@rollup/plugin-commonjs');
const terser = require('@rollup/plugin-terser');

function processArea(area) {
    const base = `Areas/${area}`;
    const scssPath = `${base}/Content/scss/style.scss`;
    const cssDest = `${base}/Content/dist/css/`;
    const jsPath = `${base}/Scripts/js/*.js`;
    const jsDest = `${base}/Content/dist/js/`;
    const jsEntry = `${base}/Scripts/js/script.js`

    const useRollup = area.toLowerCase() === 'westhealth';

    return {
        styles: function() {
            return gulp.src(scssPath)
                .pipe(sass({
                    includePaths:['node_modules']
                }).on('error', sass.logError))
                .pipe(gulp.dest(cssDest));
        },
        minifyCss: function() {
            return gulp.src(`${cssDest}style.css`, { allowEmpty: true })
                .pipe(cleanCss())
                .pipe(rename({ suffix: '.min' }))
                .pipe(gulp.dest(cssDest));
        },
        scripts: function () {
            if (useRollup) {
                return rollup({
                    input: jsEntry,
                    plugins: [resolve(), commonjs()]
                }).then(bundle => {
                    return bundle.write({
                        file: path.join(jsDest, 'bundle.min.js'),
                        format: 'esm',
                        sourcemap: true
                    });
                });
            } else {
                return gulp.src(jsPath)
                    .pipe(concat('bundle.js'))
                    .pipe(gulp.dest(jsDest));
            }
        },
        minifyJs: function () {
            if (useRollup) {
                return rollup({
                    input: jsEntry,
                    plugins: [resolve(), commonjs(), terser()]
                }).then(bundle => {
                    return bundle.write({
                        file: path.join(jsDest, 'bundle.min.js'),
                        format: 'esm',
                        sourcemap: true
                    });
                });
            } else {
                return gulp.src(`${jsDest}bundle.js`, { allowEmpty: true })
                    .pipe(uglify())
                    .pipe(rename({ suffix: '.min' }))
                    .pipe(gulp.dest(jsDest));
            }
        },
        watchPaths: {
            scss: `${base}/Content/scss/**/*.scss`,
            css: `${cssDest}style.css`,
            js: useRollup ? `${base}/Scripts/**/*.js` : jsPath,
            jsBundle: `${jsDest}bundle.js`
        }
    }
}

const areas = ['Innovation', 'westhealth'];
const processedAreas = areas.map(processArea);

/*gulp.task('styles', function (done) {
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
});*/

/*gulp.task('styles', gulp.parallel(innovation.styles, westhealth.styles));
gulp.task('minify-css', gulp.parallel(innovation.minifyCss, westhealth.minifyCss));
gulp.task('scripts', gulp.parallel(innovation.scripts, westhealth.scripts));
gulp.task('minify-js', gulp.parallel(innovation.minifyJs, westhealth.minifyJs));*/

gulp.task('styles', gulp.parallel(...processedAreas.map(a => a.styles)));
gulp.task('minify-css', gulp.parallel(...processedAreas.map(a => a.scripts)));
gulp.task('scripts', gulp.parallel(...processedAreas.map(a => a.minifyCss)));
gulp.task('minify-js', gulp.parallel(...processedAreas.map(a => a.minifyJs)));

//gulp.task('watch', function () {
//    gulp.watch('Areas/Innovation/Content/scss/**/*.scss', gulp.series('styles'));
//    gulp.watch('Areas/Innovation/Content/dist/css/style.css', gulp.series('minify-css'));
//    gulp.watch('Areas/Innovation/Scripts/**/*.js', gulp.series('scripts'));
//    gulp.watch('Areas/Innovation/Content/dist/js/bundle.js', gulp.series('minify-js'));
//});

gulp.task('watch', function () {
    processedAreas.forEach(area => {
        gulp.watch(area.watchPaths.scss, area.styles);
        gulp.watch(area.watchPaths.css, area.minifyCss);
        gulp.watch(area.watchPaths.js, area.scripts);
        gulp.watch(area.watchPaths.jsBundle, area.minifyJs);
    });
});

const buildAll = gulp.series(
    gulp.series('styles'),
    gulp.series('scripts'),
    gulp.series('minify-css'),
    gulp.series('minify-js')
);

exports.default = buildAll;