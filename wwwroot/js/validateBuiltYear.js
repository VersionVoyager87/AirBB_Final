// validateBuiltYear.js
document.addEventListener("DOMContentLoaded", function () {
    const builtYearInput = document.getElementById("BuiltYear");

    if (builtYearInput) {
        builtYearInput.addEventListener("blur", function () {
            const year = parseInt(this.value);
            const currentYear = new Date().getFullYear();
            const minYear = currentYear - 150;

            const errorSpan = document.querySelector("span[data-valmsg-for='BuiltYear']");

            if (isNaN(year)) {
                showError("Please enter a valid year.");
            } else if (year > currentYear) {
                showError("Built year must be in the past.");
            } else if (year < minYear) {
                showError(`Built year cannot be more than 150 years ago.`);
            } else {
                clearError();
            }

            function showError(message) {
                errorSpan.textContent = message;
                errorSpan.classList.add("text-danger");
            }

            function clearError() {
                errorSpan.textContent = "";
                errorSpan.classList.remove("text-danger");
            }
        });
    }
});
