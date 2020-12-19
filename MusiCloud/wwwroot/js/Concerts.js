function Concerts() {
    $.ajax({
        type: 'GET',
        url: '/Playlists/List',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: LoadPlaylists
    });
}

// Initialize and add the map
function initMap() {
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