// Minimal client-side adapter for PastYearAttribute
jQuery.validator.addMethod("pastyear", function (value, element, param) {
    if (!value) return true;

    var year = parseInt(value);
    var currentYear = new Date().getFullYear();
    var minYear = currentYear - param;

    return year >= minYear && year <= currentYear;
});

jQuery.validator.unobtrusive.adapters.addSingleVal("pastyear", "param");
