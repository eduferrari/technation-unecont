// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#TipoFiltro').on('change', function () {
    if ($(this).val() == 4) {
        $('#filtroMes').hide();
        $('#filtroStatus').show();
    } else {
        $('#filtroMes').show();
        $('#filtroStatus').hide();
    }
});