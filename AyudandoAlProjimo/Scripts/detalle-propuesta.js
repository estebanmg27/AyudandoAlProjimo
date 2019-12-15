
$('#like').on('click', function (event) {
    event.preventDefault();

    event.target.classList.add('btn-primary');
    event.target.classList.remove('btn-secondary');
    $('#dislike').addClass('btn-secondary');
    $('#dislike').removeClass('btn-primary');

    var urlAjax = event.currentTarget.href;

    $.post(urlAjax, function (data) {
        $('#valoracion').html(data);
    });
})


$('#dislike').on('click', function (event) {
    event.preventDefault();
    event.target.classList.add('btn-primary');
    event.target.classList.remove('btn-secondary');
    $('#like').addClass('btn-secondary');
    $('#like').removeClass('btn-primary');

    var urlAjax = event.currentTarget.href;

    $.post(urlAjax, function (data) {
        $('#valoracion').html(data);
    });
})