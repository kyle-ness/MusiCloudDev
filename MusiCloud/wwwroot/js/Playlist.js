$(document).ready(function () {
    $.ajax({
        url: "Playlist/update.html",
        context: document.body,
        success: function () {
            alert("done");
        }
    });