$(document).ready(function () {
    //searchpagination
    pageSize = 10;
    $(function () {
        var pageCount = Math.ceil($(".searchitem").size() / pageSize);

        for (var i = 0; i < pageCount; i++) {
            if (i == 0)
                $("#searchpagin").append('<li><a class="paginationcurrent" href="javascript:void(0);">' + (i + 1) + '</a></li>');
            else
                $("#searchpagin").append('<li><a href="javascript:void(0);">' + (i + 1) + '</a></li>');
        }
        showPage(1);
        $("#searchpagin li a").click(function () {
            $("#searchpagin li a").removeClass("paginationcurrent");
            $(this).addClass("paginationcurrent");
            showPage(parseInt($(this).text()))
        });

    })
    showPage = function (page) {
        $(".searchitem").hide();

        $(".searchitem").each(function (n) {
            if (n >= pageSize * (page - 1) && n < pageSize * page)
                $(this).show();
        });
    }
});