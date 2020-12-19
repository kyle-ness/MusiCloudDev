$.ajax({
        url: '/Home/GetMapMarker',
    type: 'GET',
    success: function (data) {
        //create a blank array
        var markers = [];

        //loop the list of addresses returned from Ajax request
        $.each(data, function (index, item) {
            //create a blank array of address
            var marker = { };

            //fill data
            marker["title"] = item.AddressName;
            marker["lat"] = item.Lat;
            marker["lng"] = item.Long;
            marker["description"] = item.Description;

            //push the current marker details in markers array
            markers.push(marker);
        })

        //call Map function with all markers passed as list of arrays
        initializeMap(markers);

    }
});

$(document).ready(function () {
    initializeMap();
})

function initializeMap(markers) {
        //you can check your marker data in console
        console.log(markers);
    var uluru = {lat: 37.774929, lng: -122.419416 };
    //Create Google map options
    var GoogleMapOptions = {
        center: new google.maps.LatLng(37.774929, -122.419416),
        zoom: 10,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };


    //create a variable of InfoWindow type to show data on clicking map icon
    var infoWindow = new google.maps.InfoWindow();
    var map = new google.maps.Map(document.getElementById("MapDiv"), GoogleMapOptions);

    //loop through each marker data
    for (i = 0; i < markers.length; {
        var data = markers[i]
        //set lat long of current marker
        var myLatlng = new google.maps.LatLng(data.lat, data.lng);

        var marker = new google.maps.Marker({
        position: uluru,
            map: map,
            title: data.title
        });

        (function (marker, data) {
        // add a on marker click event
        google.maps.event.addListener(marker, "click", function (e) {
            //show description
            infoWindow.setContent(data.description);
            infoWindow.open(map, marker);
        });
        })
            (marker, data);
    }


}
