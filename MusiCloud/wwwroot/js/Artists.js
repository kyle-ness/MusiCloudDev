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
        content = '<h3>Upcoming concert</h3>';
    }

    else {
        content = '<a href="/Concerts/Show/' + $("#ArtistId").val() + '">' + '<h3>Upcoming concert</h3>' + '</a>';
        content += '<ul>'
        res.concerts.forEach(x => {
            content +=
                '<li><strong>' + x.name + ', ' + x.country + ', ' + x.city + ', ' + x.address + '</strong>' +
                '<div class="clearfix"></div>';

            var date = new Date(x.date);

            content += '<span>' + date.getMonth() + '/' + date.getDay() + '/' + date.getFullYear() + '</span></li>';
        });
        content += '</ul>';
    }
    $('#concertDetails').html(content);
}

$(document).ready(function () {
    fetchConcerts();
})