$("#fechaFin").parent().datepicker({
    format: 'dd/mm/yyyy',
    language: 'es',
    autoclose: true,
    startDate: "tomorrow"
});

$('#inputFecha').parent().datepicker({
    format: 'dd/mm/yyyy',
    language: 'es',
    autoclose: true,
    endDate: new Date()
});

var inputFechaNacimiento = $("#inputFechaNacimiento");

inputFechaNacimiento.parent().datepicker({
    format: 'dd/mm/yyyy',
    language: 'es',
    autoclose: true
});

inputFechaNacimiento.mask("00r00r0000", {
    translation: {
        'r': {
            pattern: /[\/]/,
            fallback: '/'
        },
        placeholder: "__/__/____"
    }
});
