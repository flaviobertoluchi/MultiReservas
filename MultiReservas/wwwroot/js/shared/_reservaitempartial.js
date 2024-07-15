$(function () {
    $('.remover').on('click', function (e) {
        e.preventDefault();
        let reservaid = $('.reservaid').val();
        let reservaitemid = $(this).parent().find('.reservaitemid').val();

        if (parseInt(reservaid) > 0 && parseInt(reservaitemid) > 0) {
            $.ajax({
                url: '/reserva/reservaitem/' + reservaid + '?reservaitemid=' + reservaitemid,
                type: 'DELETE'
            })
                .done(function (response) {
                    $('.reservaitempartial').html(response);
                });
        }
    });
});
