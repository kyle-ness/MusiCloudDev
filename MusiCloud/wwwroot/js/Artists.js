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
    content = '<h3>Upcoming concert</h3>';

    if (!res.errorCode) {
        content += '<ul>'
        res.concerts.forEach(x => {
            content +=
                '<li><strong>' + x.name + ', ' + x.country + ', ' + x.city + ', ' + x.address + '</strong>' +
                '<span>' + x.date + '</span></li>';
        });
        content += '</ul>';
    }
    $('#concertDetails').html(content);
}

$(document).ready(function () {
    fetchConcerts();
})