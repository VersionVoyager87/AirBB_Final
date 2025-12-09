// Minimal client-side adapter for HalfStepNumberAttribute
jQuery.validator.addMethod("halfstepnumber", function (value, element) {
    if (!value) return true;

    var num = parseFloat(value);
    return (num * 10) % 5 === 0;   // 0.0 or 0.5 steps
});

jQuery.validator.unobtrusive.adapters.addBool("halfstepnumber");
