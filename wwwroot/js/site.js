// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



$(function () {
    $('input[name="Criteria.StartDate"], input[name="Criteria.EndDate"]').daterangepicker({
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Clear'
        }
    });

    $('input[name="Criteria.StartDate"], input[name="Criteria.EndDate"]').on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format('MM/DD/YYYY'));
    });

    $('input[name="Criteria.StartDate"], input[name="Criteria.EndDate"]').on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });
});
