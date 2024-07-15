$(function () {
    $('.adicionar').on('click', function (e) {
        e.preventDefault();
        let reservaid = $('.reservaid').val();
        let itemid = $('.item').val();
        let quantidade = $('.quantidade').val();
        if (parseInt(reservaid) > 0 && parseInt(itemid) > 0 && parseInt(quantidade) > 0) {
            $.post('/reserva/reservaitem', { reservaid, itemid, quantidade })
                .done(function (response) {
                    $('.reservaitempartial').html(response);
                });
        }
    });
});
