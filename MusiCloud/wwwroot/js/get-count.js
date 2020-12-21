getCount = function () {
    $.ajax({
        url: '/Carts/GetCount',
        success: function (data) {
            $('.count').html(data);
        },
    });
}

$(document).ready(getCount());

