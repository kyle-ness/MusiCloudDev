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


function fetchSongs() {
    artist_id = $("#ArtistId").val();
    $.ajax({
        type: 'GET',
        url: '/Songs/GetSongsOfArtistAjax?id=' + artist_id,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: fetchPlaylists
    });
}

function fetchPlaylists(songs) {
    $.ajax({
        type: 'GET',
        url: '/Playlists/List',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (res) {

            dropdown = '<div class="dropdown"><ul>';

            res.playlists.forEach(x => {
                dropdown += '<li>' +
                    '<a onclick="foo(' + x.playlistId + ', song_id)">' + x.name + '</a>' +
                    '</li>';
            });

            dropdown += '</ul></div>';
            LoadSongs(songs, dropdown)
        }
    });

}
function LoadSongs(res, dropdown) {
    content = ''
   
    if (res.errorCode) {
        content = '<h3>No songs for this artist :/</h3>';
    }

    else {
        res.songs.forEach(x => {
            content += '    <div class="container">' +
                '        <!-- song -->' +
                '        <div class="song-item">' +
                '            <div class="row">' +
                '                <div class="col-lg-4">' +
                '                    <div class="song-info-box">' +
                '                        <img src="' + x.imgLink + '" alt="">' +
                '                        <div class="song-info">' +
                '                            <h4>'+ x.name +'</h4>' +
                '                            <p>' + x.album + '</p>' +
                '                        </div>' +
                '                    </div>' +
                '                </div>' +
                '                <div class="col-lg-6">' +
                '                    <div class="single_player_container">' +
                '                        <div class="single_player">' +
                '                            <div class="jp-jplayer jplayer" data-ancestor=".jp_container_1" data-url="' + x.songLink + '"></div>' +
                '                            <div class="jp-audio jp_container_1" role="application" aria-label="media player">' +
                '                                <div class="jp-gui jp-interface">' +

                '                                    <!-- Player Controls -->' +
                '                                    <div class="player_controls_box">' +
                '                                        <button class="jp-prev player_button" tabindex="0"></button>' +
                '                                        <button class="jp-play player_button" tabindex="0"></button>' +
                '                                        <button class="jp-next player_button" tabindex="0"></button>' +
                '                                        <button class="jp-stop player_button" tabindex="0"></button>' +
                '                                        <button onclick="openDropDown()" class="jp-add player_button" tabindex="0">+</button>' + dropdown.replace('song_id', x.songId) +
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
        });

        $('#LoadSongs').html(content);

        initSinglePlayer();
    }

}

function openDropDown() {
    $(".dropdown").toggleClass('active');
}

$(document).ready(function () {
    fetchConcerts();
    fetchSongs();
})