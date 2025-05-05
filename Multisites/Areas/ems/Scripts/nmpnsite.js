$(document).ready(function () {
    updateBodyMargin();
});


$(document).resize(function () {
    updateBodyMargin();
});

function updateBodyMargin() {
    $('main').css({ 'margin-top': $('header.container-flex').height() });
}