//FRONTEND KISMINDA JQUERY AJAX KULLANARAK YAZMIŞ OLDUĞUMUZ API DOKUMANLARINI FRONT KISMINA AŞAĞIDAKİ GİBİ ÇEKİYORUZ.
$(document).ready(function () {
	$("#baslikYazisi").text("Selamss");

	$.ajax({
		url: "http://localhost:5174/api/home",
		type: "get",
		success: function (data) {
			$(".brands").append("<ul class='list-group'>");
			$.each(data, function (i, v) {
				$(".brands").append("<li class='list-group-item'>" + v.name + "</li>");
			});
			$(".brands").append("<ul/>");
		}
	});
});
