
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