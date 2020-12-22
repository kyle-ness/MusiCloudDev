function fetchSongs() {
    $.ajax({
        type: 'GET',
        url: '/Playlists/GetPlaylistSongsAjax?playlistId=' + $("#playlist_id").val(),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: LoadSongs
    });
}
function LoadSongs(res) {
    content = ''
    playlistId = $("#playlist_id").val();

    console.log(res.songs.length)


    if (res.errorCode) {
        content = '<h3>No songs in this playlist :/</h3>';
    }

    else if (res.songs.length == 0) {
        content = '<h3>No songs in this playlist :/</h3>';
    }

    else {
        var count_from_zero = 0;
        var count_from_one = 1;
        res.songs.forEach(x => {

            content += '    <div class="container">' +
                '        <!-- song -->' +
                '        <div class="song-item">' +
                '            <div class="row">' +
                '                <div class="col-lg-4">' +
                '                    <div class="song-info-box">' +
                '                        <img src="' + x.imgLink + '" alt="">' +
                '                        <div class="song-info">' +
                '                            <a href="/Artists/Artist/' + x.artistId + '">' +
                '                            <h4>' + x.name + '</h4>' +
                '                            <p>' + x.album + '</p>' +
                '                            </a>' +
                '                        </div>' +
                '                    </div>' +
                '                </div>' +
                '                <div class="col-lg-6">' +
                '                    <div class="single_player_container">' +
                '                        <div class="single_player">' +
                '                            <div class="jp-jplayer jplayer" data-ancestor=".jp_container_' + count_from_one + '" data-url="' + x.songLink + '"></div>' +
                '                            <div class="jp-audio jp_container_' + count_from_one + '" role="application" aria-label="media player">' +
                '                                <div class="jp-gui jp-interface">' +

                '                                    <!-- Player Controls -->' +
                '                                    <div class="player_controls_box">' +
                '                                        <button class="jp-prev player_button" tabindex="' + count_from_zero + '"></button>' +
                '                                        <button class="jp-play player_button" tabindex="' + count_from_zero + '"></button>' +
                '                                        <button class="jp-next player_button" tabindex="' + count_from_zero + '"></button>' +
                '                                        <button class="jp-stop player_button" tabindex="' + count_from_zero + '"></button>' +
                '                                        <button onclick="deleteSong(' + playlistId + ',' + x.songId + ')" class="jp-add player_button" tabindex="' + count_from_zero + '">-</button>' +
                '                                        </div>' +
                '                                    <!-- Progress Bar -->' +
                '                                    <div class="player_bars">' +
                '                                        <div class="jp-progress">' +
                '                                            <div class="jp-seek-bar">' +
                '                                                <div>' +
                '                                                    <div class="jp-play-bar"><div class="jp-current-time" role="timer" aria-label="time">0:00</div></div>' +
                '                                                </div>' +
                '                                            </div>' +
                '                                        </div>' +
                '                                        <div class="jp-duration ml-auto" role="timer" aria-label="duration">00:00</div>' +
                '                                    </div>' +
                '                                </div>' +
                '                            </div>' +
                '                        </div>' +
                '                    </div>' +
                '                </div>' +
                '            </div>' +
                '        </div>' +
                '    </div>';

            count_from_zero++;
            count_from_one++;

        });

        $('#LoadSongs').html(content);

        initSinglePlayer();
    }

}


function deleteSong(playlist_id, song_id) {
    $.ajax({
        type: 'GET',
        url: '/SongToPlaylists/DeleteSongFromPlaylistAjax?playlistId=' + playlist_id + '&songId=' + song_id,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (res) {
            fetchSongs();

            if (res.success != true) {
                alert('Cannot delete the song')
            }

        }
    });
}

$(document).ready(function () {
    fetchSongs();
})