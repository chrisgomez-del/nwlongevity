if($('.expandall').length)
{
$('.expandall').on('click', function () {
    $accordion = $(this).closest('.accordion');
    
    if (!$(this).hasClass('collapseall')) {
        $(this).text('collapse all');
        $(this).toggleClass('collapseall');
        $accordion.find('.accordion-btn').each(function () {
            if ($(this).hasClass('collapsed')) {
                $(this).removeClass('collapsed');
            }
        });
        $accordion.find('.collapse').each(function () {
            if (!$(this).hasClass('show')) {
                $(this).addClass('show');
            }
        });

    }
    else {
        $(this).text('expand all');
        $(this).toggleClass('collapseall');
        $accordion.find('.accordion-btn').each(function () {
            if (!$(this).hasClass('collapsed')) {
                $(this).addClass('collapsed');
            }
        });
        $accordion.find('.collapse').each(function () {
            if ($(this).hasClass('show')) {
                $(this).removeClass('show');
            }
        });

    }

});
}