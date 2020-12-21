$("#WeatherLookup").unbind("click").click(function (e) {
	e.preventDefault();
	var cityname = $("#CityName").val();
	if (cityname.length > 0) {
		$.ajax({
			url: "/Concerts/WeatherDetail/?City=" + cityname,
			type: "POST",
			async: false,
			crossDomain: true,
			dataType: 'json',
			success: function (rsltval) {
				var dataBefore = JSON.stringify(rsltval);
				var data = JSON.parse(dataBefore);
				if (data !== "{ }") {
					$(".the-forecast").css("display", "block");
					$("#lblError").text("");
					$("#lblDescription").text(data.Description);
					$("#lblHumidity").text(data.Humidity);
					$("#lblTemp").text(data.Temp);
					$("#imgWeatherIconUrl").attr("src", "http://openweathermap.org/img/w/" + data.WeatherIcon + ".png");
				}
				else {
					$("#lblError").text("This place does not exist");
					$(".the-forecast").css("display", "none");
					$("#place-error").text("");
				}

			},
			error: function () {
				console.log("error - problem forecast");
			}
		});
	}
	else {
		alert("City Not Found");
	}
});


