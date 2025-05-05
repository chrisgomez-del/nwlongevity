
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
            if ($(formid).valid() ) {
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
