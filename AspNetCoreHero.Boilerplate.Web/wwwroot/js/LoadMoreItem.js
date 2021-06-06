
var pageSize = 30;
var pageIndex = 1;
var isLoading = false;
var total = @totalPages;
$(document).ready(function () {
    //GetData();

    $(window).scroll(function () {
        if ($(window).scrollTop() ==
            $(document).height() - $(window).height()) {
            GetData();
        }
    });
});

function GetData() {
    console.log(total);
    if (isLoading === false) {
        $.ajax({
            type: 'GET',
            url: '/LoadMore' + window.location.pathname,
            data: { "page": pageIndex, "size": pageSize },
            success: function (html) {
                $("#listArticle").append(html);
                pageIndex++;
            },
            beforeSend: function () {
                //$("#progress").show();
                isLoading = true;
            },
            complete: function () {
                //$("#progress").hide();
                isLoading = false;
            },
            error: function (error) {
                //alert("Error while retrieving data!");
                console.log(error);
            }
        });
    }

}