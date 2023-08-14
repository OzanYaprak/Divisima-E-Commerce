$(document).ready(function () {
    //$(".display-4").text("Hoşgeldiniz....");
    $.ajax({
        url: "http://localhost:5174/api/home",
        type: "get",
        success: function (data) {
            $(".brands").append("<ul class='list-group'>");
            $.each(data, function (i, v) {
                $(".brands").append("<li class='list-group-item'>"+v.name+"</li>");
            });
            $(".brands").append("</ul>");
        },
        error: function (e) {

            alert(e.RequestText);
        }
    });
});