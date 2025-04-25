// ========================== mobile/touch/browser detection ====================== //
var isMobile = navigator.userAgent.toLowerCase().match(/(iphone|ipod|ipad|android|blackberry|opera mini|iemobile|kindle|silk|mobile)/);

var isSafari = !!navigator.userAgent.match(/safari/i) && !navigator.userAgent.match(/chrome/i) && typeof document.body.style.webkitFilter !== "undefined" && !window.chrome;

//check if support matchmedia
function supportsMatchMedia() {
    return (typeof window.matchMedia != "undefined" || typeof window.msMatchMedia != "undefined");
}
//modular shortened mobile/device checker boolean
var isMobileDevice;
if (window.matchMedia('(max-width: 767px)').matches) {
    isMobileDevice = true;
} else {
    isMobileDevice = false;
}

//is webkit
var isWebkit = 'WebkitAppearance' in document.documentElement.style;
// Drag/Click Scrolling
// This is for the Meet in Person Component
// updated 3/8/22

const slider = document.getElementById("card-slider");
let isDown = false;
let startX;
let scrollLeft;
if (slider) {
    slider.addEventListener("mousedown", e => {
        isDown = true;
        slider.classList.add("active");
        startX = e.pageX - slider.offsetLeft;
        scrollLeft = slider.scrollLeft;
    });
    slider.addEventListener("mouseleave", () => {
        isDown = false;
        slider.classList.remove("active");
    });
    slider.addEventListener("mouseup", () => {
        isDown = false;
        slider.classList.remove("active");
    });
    slider.addEventListener("mousemove", e => {
        if (!isDown) return;
        e.preventDefault();
        const x = e.pageX - slider.offsetLeft;
        const walk = x - startX;
        slider.scrollLeft = scrollLeft - walk;
    });
}


// Active Toggle
// This is for the Meet the Team Bio Component
// updated 4/6/22

$(".btn-bio").bind("click", function () {
    $(".team-card-content").each(function () {
        if (!$(this).hasClass('d-none')) {
            $(this).addClass('d-none');
        }
    });
    $(".transparency").toggleClass("transition-active");
    $(".transparency-background").toggleClass("transparency-active");
    var $currentcard = $(this).closest('.team-card').toggleClass('active');
    var isCurrentCardActive = $currentcard.hasClass('active') ? true : false;
    if (isCurrentCardActive) {
        $currentcard.siblings().removeClass("active");
    }
    var $cardcontent = $(this).closest(".team-card").find(".team-card-content")[0].innerHTML;
    if (isMobileDevice) {
        //$mobilediv = $(this).closest(".team-card").find(".mobile-content");
        //$mobilediv.html($cardcontent)team-card-content
        var $teamcardcontent = $(this).closest(".team-card").find(".team-card-content");
        if (isCurrentCardActive) {
            $teamcardcontent.toggleClass('d-none')
        }
    }
    else {
        $('div.transparency').html('')
        $(this).parents('.card-list-panel').next('div.transparency').html($cardcontent)
    }
    

});
//Custom Validators for files
$.validator.unobtrusive.adapters.add('filetype', ['validtypes'], function (options) {
    options.rules['filetype'] = { validtypes: options.params.validtypes.split(',') };
    options.messages['filetype'] = options.message;
});

$.validator.addMethod("filetype", function (value, element, param) {
    for (var i = 0; i < element.files.length; i++) {
        var extension = getFileExtension(element.files[i].name);
        if ($.inArray(extension, param.validtypes) === -1) {
            return false;
        }
    }
    return true;
});

function getFileExtension(fileName) {
    if (/[.]/.exec(fileName)) {
        return /[^.]+$/.exec(fileName)[0].toLowerCase();
    }
    return null;
}
$.validator.unobtrusive.adapters.add('filesize', ['maxbytes'], function (options) {
    // Set up test parameters
    var params = {
        maxbytes: options.params.maxbytes
    };

    // Match parameters to the method to execute
    options.rules['filesize'] = params;
    if (options.message) {
        // If there is a message, set it for the rule
        options.messages['filesize'] = options.message;
    }
});

$.validator.addMethod("filesize", function (value, element, param) {
    if (value === "") {
        // no file supplied
        return true;
    }

    var maxBytes = parseInt(param.maxbytes);

    // use HTML5 File API to check selected file size
    // https://developer.mozilla.org/en-US/docs/Using_files_from_web_applications
    // http://caniuse.com/#feat=fileapi
    if (element.files != undefined && element.files[0] != undefined && element.files[0].size != undefined) {
        var filesize = parseInt(element.files[0].size);

        return filesize <= maxBytes;
    }

    // if the browser doesn't support the HTML5 file API, just return true
    // since returning false would prevent submitting the form
    return true;
});

// Focus Area Tabs
// 9/14/22 - Rory Lanam
if ($('#focus-tabs') != null && $('#focus-tabs').length) { 
    $("#focus-tabs").easyResponsiveTabs({
        type: 'default', //Types: default, vertical, accordion           
        width: 'auto', //auto or any custom width
        fit: true,   // 100% fits in a container
        closed: 'accordion', // Close the panels on start, the options 'accordion' and 'tabs' keep them closed in there respective view types
        activate: function () { }  // Callback function, gets called if tab is switched
    });
}
//overlays
//9/29/22 - SKB
var width = $(window).width();
$(window).resize(function () {
    if ($(this).width() != width) {
        width = $(this).width();
        FormHelper.sizeTheOverlays();
    }
});

//portfolio slider
//3/6/23 - AK
const swiper = new Swiper('.swiper', {
    // Optional parameters
    direction: 'horizontal',
    loop: false,
    slidesPerView: 1,
    breakpoints: {
        640: {
            slidesPerView: 2,
        },
        725: {
            slidesPerView: 2.5,
        },
        992: {
            slidesPerView: 3,
        },
        1500: {
            slidesPerView: 3.5,
        },
        2000: {
            slidesPerView: 4.5,
        },
    },

    // Navigation arrows
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },

    // And if we need scrollbar
    scrollbar: {
        el: '.swiper-scrollbar',
    },
});
