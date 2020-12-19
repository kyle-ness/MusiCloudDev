$(document).ready(function () {
    fetchPlaylists();
})

function LoadPlaylists(res) {
    if (res.errorCode) {
        $('.playlist-area').html("No playlists");
    }

    else {
        content = '';
        res.recipe.forEach(x => {
            content +=
                '<div class="mix col-lg-3 col-md-4 col-sm-6 movies">' +
                    '<div class="playlist-item" >' +
                        '<img src="~/img/playlist/3.jpg" alt="">' +
                        '<h5>' + x.Name + '</h5>' +
                    '</div>' +
                '</div >'
        });

        $('.playlist-area').html(content);
    }

}


function fetchPlaylists() {
    $.ajax({
        type: 'GET',
        url: '/Playlists/List',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: LoadPlaylists
    });
}


