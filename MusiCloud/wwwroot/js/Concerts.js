function fetchConcerts() {
    artist_id = $("#ArtistId").val();
    $.ajax({
    type: 'GET',
        url: '/Concerts/GetConcerts?id=' + artist_id,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: LoadConcerts
    });
}

function LoadConcerts(res) {
    if (res.errorCode) {
        const uluru = { lat: 40.70027290768944, lng: -74.0442543305885 };

        // The map, centered at Uluru
        const map = new google.maps.Map(document.getElementById("MapDiv"), {
            zoom: 4,
            center: uluru,
        });

        // The marker, positioned at Uluru
        const marker = new google.maps.Marker({
            position: uluru,
            map: map,
        });
    }

    else {

        const uluru = { lat: 40.70027290768944, lng: -74.0442543305885 };

        // The map, centered at Uluru
        const map = new google.maps.Map(document.getElementById("MapDiv"), {
            zoom: 4,
            center: uluru,
        });

        content = ''

        res.concerts.forEach(x => {
            var uluru = { lat: x.lat, lng: x.lng }; 
            new google.maps.Marker({
                position: uluru,
                map: map,
            });

            var date = new Date(x.date);

            content +=
                '<li Location: ><strong>' + x.name + ', ' + x.country + ', ' + x.city + ', ' +
                '<li Address: ><strong>' + x.address + '</strong></li>' +
                '<li Date: ><strong>' + + date.getMonth() + '/' + date.getDay() + '/' + date.getFullYear() + '</strong></li>' +
                '<div class="clearfix"></div>';
        });

        $('#concertDetails').html(content);

    }

}

function initMap() {
    fetchConcerts();
}



// Initialize and add the map
function initMapBackup() {
    // The location of Uluru

    const uluru = { lat: 51.502201219453774, lng: -0.14011938911621313};
    //const uluru = { lat: -25.344, lng: 131.036 };
    // The map, centered at Uluru
    const map = new google.maps.Map(document.getElementById("MapDiv"), {
        zoom: 4,
        center: uluru,
    });
    // The marker, positioned at Uluru
    const marker = new google.maps.Marker({
        position: uluru,
        map: map,
    });
}