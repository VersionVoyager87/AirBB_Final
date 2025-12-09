$(document).ready(function () {
    $("form").submit(function () {
        var dobInput = $("#DOB").val();
        if (!dobInput) return true; 

        var dob = new Date(dobInput);
        var today = new Date();
        var age = today.getFullYear() - dob.getFullYear();
        var m = today.getMonth() - dob.getMonth();

        if (m < 0 || (m === 0 && today.getDate() < dob.getDate())) {
            age--;
        }

        if (age < 13) {
            alert("You must be at least 13 years old.");
            return false;
        }

        return true;
    });
});
