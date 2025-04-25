
$(function () {
    var formname = "contact-form";
    var messageboxid = "#formConfirmation";
    if ($('#contact-form') != null && $('#contact-form').length) {
        //clear the form
        $('#contact-form').find("input:not([type='radio']), textarea").val("");
        $(".loader-icon-wrapper").hide();
        //radio buttons
        $('input[name="CurrentRole"]').on("change", function () {
            $('#SelectedCurrentRole').val($(this).val());
            $('#SelectedCurrentRole').valid();
            FormHelper.errorStateRadio("CurrentRole", 1);
        });
        
        $('input[name="Topic"]').on("change", function () {
            $('#SelectedTopic').val($(this).val());
            $('#SelectedTopic').valid();
            FormHelper.errorStateRadio("Topic", 1);
        });
        //input validation 
        $('#contact-form').find("input:not([type='radio']), textarea").on("keyup keypress blur change", function () {
            var fieldname = $(this).attr('name');
            var isValid = $(this).valid();
            if (isValid) {
                FormHelper.errorStateRadio(fieldname, 1);
            }
            else {
                FormHelper.errorStateRadio(fieldname, 0);
            }
            
        });

        $('.contact-submit').on('click', function (e) {
            e.preventDefault();
            var $captchaelement = $('.g-recaptcha');
            $captchaelement.removeClass("captcha-error");
            var formid = "#" + formname;
            var validator = $(formid).validate();
            if ($(formid).valid() && FormHelper.IsradiobuttonValid('CurrentRole') && FormHelper.IsradiobuttonValid('Topic')) {
                let success = (async () => await FormHelper.validateCaptcha())();
                success.then(function (data) {
                    if (data) {
                        let ret = (async () => await FormHelper.submit(formname, messageboxid))();
                        ret.then(function () {
                            FormHelper.scrolltoformTop();
                            return true;
                        });
                    }
                    else {
                        $captchaelement.addClass("captcha-error");
                        return false;
                    }
                    
                });
            }
            else {
                FormHelper.setlabelerror();
                FormHelper.IsradiobuttonValid('CurrentRole');
                FormHelper.IsradiobuttonValid('Topic');
                validator.focusInvalid();
            }

        });
    }
});


var FormHelper = {
    preventBack: function () {
        window.location.hash = "no-back-button";
        window.location.hash = "No-back-button";//again because google chrome don't insert first hash into history
        window.onhashchange = function () { window.location.hash = "no-back-button"; };
    },
    handleformprecondition: function () {
        $('#formsubmit').attr("disabled", true);
        $('.btn-back').attr("disabled", true);
        FormHelper.sizeTheOverlays();
        $('#formLoader').show();
        $('#message').hide();
    },
    handleErrorResponse: function (formid, messageboxid, errorThrown) {
        console.log(errorThrown);
        if ($('#formLoader').length) { $('#formLoader').hide(); }
        $(formid + "Panel").toggle();
        $('.form-panel-text').toggle();
        $(messageboxid).toggle();
        $('#success').hide();
        $('#error').show();
        FormHelper.preventBack();
    },
    handleSuccessResponse: function (formid, messageboxid, data, status, result) {
        $('#message').html(result);
        $('#formLoader').hide();
        $(formid + "Panel").hide();
        $('.form-panel-text').hide();
        $(messageboxid).toggle();
        $('#message').show();
        $(formid).trigger("reset");
        FormHelper.preventBack();
    },
    IsradiobuttonValid: function (name) {
        var $selecteditem = $('#Selected' + name);
        var val1 = $selecteditem.valid();
        var val2 = true;
        if ($selecteditem.length) {
            val2 = $selecteditem.valid();
        }
        if (val1) {
            errorStateRadio(name, 1);
        }
        else {
            errorStateRadio(name, 0);
        }

        return val1 && val2;
    },
    errorStateRadio: function (name, valid) {
        if (valid) {
            $("label[for^=" + name + "]").each(function () {
                $(this).removeClass("error");
            });
        }
        else {
            $("label[for^=" + name + "]").each(function () {
                $(this).addClass("error");
            });
        }
    },
    setlabelerror: function () {
        $('.field-validation-error').each(function () {
            var $currentelem = $(this).parent().find('label:first-child');
            if (!$currentelem.hasClass('error')) {
                $currentelem.addClass('error');
            }
            
        })
    },
    IsradiobuttonValid: function (name) {
        var $selecteditem = $('#Selected' + name);
        var val1 = $selecteditem.valid();
        var val2 = true;
        if ($selecteditem.length) {
            val2 = $selecteditem.valid();
        }
        if (val1) {
            FormHelper.errorStateRadio(name, 1);
        }
        else {
            FormHelper.errorStateRadio(name, 0);
        }

        return val1 && val2;
    },
    stepNumber: function () {
        var name = $('li.resp-tab-active').text();

        if (name) {
            var stepNumber = parseInt(name.charAt(0), 10);
            if (!isNaN(stepNumber)) {
                return stepNumber;
            }
        }

        return 0;
    },
    validateCaptcha: async function () {
        try {
            let success = false;
            if (grecaptcha.getResponse() == '') {
                $('html,body').animate({ scrollTop: $('.g-recaptcha').offset().top - 20 }, 'slow');
                return false;
            }
            else {
                try {
                    success = await $.ajax({
                        type: 'POST',
                        url: 'api/Captcha/ValidResponse',
                        data: grecaptcha.getResponse(),
                        contentType: 'application/json; charset=utf-8',
                        success: function (response) {
                            if (eval(response)) {
                                return true;
                            }
                            else {
                                grecaptcha.reset();
                                return false;
                            }
                        },
                        error: function (response) {
                            console.log(response);
                            console.log('Error: ValidCaptchaResponse');
                            return false;
                        }
                    });
                    return success;
                }
                catch (error) {
                    console.error(error);
                    return false;
                }
            }
        }
        catch (error) {
            console.error(error.message);
            return false;
        };
    },
    submit: async function (formname, messageboxid) {
        let result;
        var formid = "#" + formname;
        var form = $(formid);
        var url = form.attr('action');
        FormHelper.handleformprecondition();
        var formData = new FormData(document.getElementById(formname));
        try {
            result = await $.ajax({
                url: url,
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: formData,
                cache: false,
                success: function (data, status, xhr) {

                    if (data.success === false) {
                        FormHelper.handleErrorResponse(formid, messageboxid, data.result);
                    }
                    else {
                        FormHelper.handleSuccessResponse(formid, messageboxid, data, status, data.result);
                        FormHelper.preventBack();
                    }
                    return true;
                },
                error: function (err) {
                    FormHelper.handleErrorResponse(formid, messageboxid, err.statusText);
                    return false;
                }

            });
            return result;
        }
        catch (error) {
            console.error(error);
            return false;
        }
    },
    sizeTheOverlays: function () {
        var $overlay = $(".loader-icon-wrapper");
        if ($overlay.length) {
            var $parent = $('.contact-us-form-section');
            $overlay.resize().each(function () {
                var h = $parent.outerHeight();
                var w = $parent.outerWidth();
                var t = $parent.position().top;
                $overlay.css("height", h).css("width", w).css("top", t);
            });
        }
    },
    scrolltoformTop: function () {
        var top = $('.contact-us-form-section').position().top;
        $("html, body").animate({ scrollTop: top });
    }
};
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


$(function () {
    if ($('#partnershipIntake-form') != null && $('#partnershipIntake-form').length) {

        var formname = "partnershipIntake-form";
        var messageboxid = "#formConfirmation";
        $(".loader-icon-wrapper").hide();
        $("#partnershipIntake-form").easyResponsiveTabs({
            type: 'default', //Types: default, vertical, accordion           
            width: 'auto', //auto or any width like 600px
            fit: true,   // 100% fit in a container
            closed: 'accordion', // Start closed if in accordion view
            activate: function (event) { }// Callback function if tab is switched                            
        });
        

        var activeContent = $('.resp-tab-content-active');
        var openTab = $('.resp-tab-active');
        var firstTab = $('[aria-controls="tab_item-0"]');
        var firstContainer = $('[aria-labelledby="tab_item-0"]');

        if (isMobileDevice) {
            //make the first tab active
            $(firstTab).addClass('resp-tab-active');
            //open the firest tab content
            $(firstContainer).addClass('resp-tab-content-active').show();
        }
        var inputsChanged = false;
        $('#partnershipIntake-form').find('input, select').change(function () {
            inputsChanged = true;
        });

        $(window).on('beforeunload', function () {
            if (inputsChanged === true && window.location.hash !== "#no-back-button") {
                return 'Are you sure you want to leave?';
            }
        });
        $(window).on('load', function () {
            //close any active tabs/acordions
            $(activeContent).removeClass('resp-tab-content-active').hide();
            $(openTab).removeClass('resp-tab-active');
            
            //clear the form
            $('#partnershipIntake-form').find("input:not([type='radio']), textarea, select").val("");
            //make the first tab active
            $(firstTab).addClass('resp-tab-active');
            $(firstContainer).addClass('resp-tab-content-active').show();
            //remove hash
            history.pushState('', document.title, window.location.pathname);
        });

        $('input[name="Option"]').on("change", function () {
            $('#SelectedOption').val($(this).val());
            $('#SelectedOption').valid();
            FormHelper.errorStateRadio("Option", 1);
        });
        //input validation 
        $('#partnershipIntake-form').find("input:not([type='radio']), textarea, select").on("keyup keypress blur change", function () {
            var fieldname = $(this).attr('name');
            var isValid = $(this).valid();
            if (isValid) {
                FormHelper.errorStateRadio(fieldname, 1);
            }
            else {
                FormHelper.errorStateRadio(fieldname, 0);
            }

        });

        // --------validate steps of form ------- //

        var currentPageUrl = document.location.href;
        // next button
        $('.btn-next').click(function (e) {
            var fieldset = $(this).closest('.resp-tab-content-active').find('fieldset')[0];
            var activeTab = $('.resp-tab-active');
            var relOffset = $(this).closest('.resp-tab-content-active').siblings('.resp-tab-active').data('reltop');// get relative top offset from data
            var isValid = $(fieldset.form).valid();//to do only validate the fieldset
            //var tabName = "step " + $('li.resp-tab-active').text().toLowerCase() + " complete";
            //if (FormHelper.stepNumber() === 4) {
            //    isValid = isStep4Valid() && isValid;
            //}
            FormHelper.setlabelerror();

            if (isValid) {
                $(activeTab).addClass('finish');
                $(activeTab).find('.bi').removeClass();
                $(activeTab).find('.resp-arrow').remove();
                $(activeTab).find("span").addClass('bi bi-check-circle-fill');
                //dataLayer.push({
                //    event: 'Partneship intake form',
                //    eventCategory: 'Partneship intake form',
                //    eventAction: currentPageUrl,
                //    eventLabel: tabName
                //});
                $(activeTab).next('li').trigger('click');
                if (isMobileDevice) {
                    $('html, body').animate({
                        scrollTop: $(".resp-tabs-container").offset().top + (relOffset - 55)
                    }, 250);
                } else {
                    $('html, body').animate({
                        scrollTop: 0
                    }, 250);
                }
            } else {
                isValid = false;
                return false;
            }
            e.preventDefault();
        });

        //back button
        $('.btn-back').click(function (e) {
            var relOffset = $(this).closest('.resp-tab-content-active').siblings('.resp-tab-active').data('reltop') - 53;// get relative top offset from data
            $('.resp-tab-active').prev('li').trigger('click');
            if (isMobileDevice) {
                $('html, body').animate({
                    scrollTop: $(".resp-tabs-container").offset().top + (relOffset - 55)
                }, 250);
            } else {
                $('html, body').animate({
                    scrollTop: 0
                }, 250);
            }
            e.preventDefault();
        });

    //submit button
    $('#formsubmit').on('click', function (e) {
            e.preventDefault();
        ValidateFormandSubmit(formname, messageboxid);
            return false;
        });
    }

    var isStep4Valid = function () {
        return FormHelper.IsradiobuttonValid('Option');
    };
});

function ValidateFormandSubmit(formname, messageboxid) {

    var validator = $("#" + formname).validate();
    var isformValid = $("#" + formname).valid();
    FormHelper.setlabelerror();
    if (isformValid) {
        let ret = (async () => await FormHelper.submit(formname, messageboxid))();
        ret.then(function (data) {
            if (data) {
                FormHelper.scrolltoformTop()
                return true;
            }
            else {
                return false;
            }
        });
    }
    else {
        validator.focusInvalid();
    }
}

function submit(formname, messageboxid) {
    var formid = "#" + formname;
    var form = $(formid);
    var url = form.attr('action');
    FormHelper.handleformprecondition();
    var formData = new FormData(document.getElementById(formname));
    $.ajax({
        url: url,
        type: "POST",
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: formData,
        cache: false,
        success: function (data, status, xhr) {

            if (data.success === false) {
                FormHelper.handleErrorResponse(formid, messageboxid, data.result);
            }
            else {
                FormHelper.handleSuccessResponse(formid, messageboxid, data, status, data.result);
                FormHelper.preventBack();
            }
        },
        error: function (err) {
            FormHelper.handleErrorResponse(formid, messageboxid, err.statusText);
        }  
        
    });
}

$('.partnership-intake form textarea').keyup(function () {
    let maxLength = 500;
    let idNumber = this.dataset.id;
    let length = $(this).val().length;
    length = maxLength - length;
    $('#chars' + idNumber).text(length);
})