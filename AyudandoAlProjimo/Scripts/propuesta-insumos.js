const agregarInsumo = () => {
    const nuevoInsumoSnippet = '<div class="form-row insumo"><div class="form-group col-md-3"><label for="Cantidad_{numeroInsumo}_">Cantidad:</label><input class="form-control" id="Cantidad_{numeroInsumo}_" name="Cantidad[{numeroInsumo}]" type="number" value=""></div><div class="form-group col-md-9"><label for="Nombres_{numeroInsumo}_">Nombre insumo:</label><input class="form-control" id="Nombres_{numeroInsumo}_" name="Nombres[{numeroInsumo}]" type="text" value=""></div></div>';

    const numeroInsumo = $('.insumo').length;
    $('#contenedor-insumos').append(nuevoInsumoSnippet.replace(/{numeroInsumo}/g, numeroInsumo));
    $('#input-cantidad-insumos').val($('.insumo').length);
    $('#boton-quitar-insumo').removeAttr('disabled');
};

const quitarInsumo = () => {
    $('.insumo').last().remove();
    if ($('.insumo').length === 1) $('#boton-quitar-insumo').attr('disabled', 'disabled');
    $('#input-cantidad-insumos').val($('.insumo').length);
};