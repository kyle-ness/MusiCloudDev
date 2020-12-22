$(document).ready(function () {
    fetchPlaylists();
})

function LoadPlaylists(res) {
    if (res.errorCode) {
        $('.playlist-area').html("No playlists");
    }

    else {
        content = '';
        res.playlists.forEach(x => {
            content +=
                '<div id="wrapper" class="mix col-lg-3 col-md-4 col-sm-6 movies">' +
                '<div id="playlist-item" class="playlist-item" >' +
                '<img src="/img/playlist/'+ x.imgUrl +'.jpg" alt="">' +
                ' <a href="/Playlists/Playlist/' + x.playlistId + '">' +
                '<h5>' + x.name + '</h5>' +
                '</a>' +
                '</div>' +
                '</div >';
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

$("#CreateSuggestedPlaylist").click(function () {
    CreateSuggestedPlaylist();
})


$("#CreatePlaylist").click(function () {
    CreatePlaylist();
})

$('#PlaylistName').keypress(function (e) {
    if (e.which == 13) {
        $('#CreatePlaylist').click();
    }
});

function CreateSuggestedPlaylist() {
    var playlist_data = {
        'Name': $("#CreateSuggestedPlaylist").val()
    };
    $.ajax({
        url: '/Playlists/CreatePlaylistAjax',
        data: playlist_data,
        success: function (res) {
            CreatePlaylistResult(res);
        }
    });
}




function CreatePlaylist() {
    var playlist_data = {
        'Name': $("#PlaylistName").val()
    };
    $("#PlaylistName").attr("placeholder", "Create more playlists").val("")
    $.ajax({
        url: '/Playlists/CreatePlaylistAjax',
        data: playlist_data,
        success: function (res) {
            CreatePlaylistResult(res);
        }
    });
}

function CreatePlaylistResult(res) {
    fetchPlaylists();
}

function FilterPlaylists() {
    var input, filter, playlist_wrapper, playlist_items, h5, i, j, txtValue;
    input = document.getElementById("PlaylistFilter");
    filter = input.value.toUpperCase();
    playlist_wrapper = document.getElementsByClassName("mix col-lg-3 col-md-4 col-sm-6 movies");

    for (i = 0; i < playlist_wrapper.length; i++) {

        playlist_items = playlist_wrapper[i].getElementsByTagName("div");
        for (j = 0; j < playlist_items.length; j++) {
            h5 = playlist_items[j].getElementsByTagName("h5")[0];
            txtValue = h5.textContent || h5.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                playlist_wrapper[i].style.display = "";
            } else {
                playlist_wrapper[i].style.display = "none";
            }
        }

    }
}