setInterval(function () {
    $.get('indexpartial').done(function (response) {
        $('.indexpartial').html(response);
    });
}, 5000);
