
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