function fetchConcerts() {
    artist_id = $("#ArtistId").val();
    $.ajax({
    type: 'GET',
        url: '/Concerts/GetConcertsData?id=' + artist_id,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: LoadConcerts
    });
}

function LoadConcerts(res) {
    alert(res)
}