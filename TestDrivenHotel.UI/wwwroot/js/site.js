// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

<script>
    document.addEventListener("DOMContentLoaded", function () {
        // Hämta formuläret och input-fälten
        var form = document.getElementById("bookingForm");
    var startingDateInput = document.getElementById("starting-date-input");
    var endingDateInput = document.getElementById("ending-date-input");

    // Hämta de sparade värdena från lokal lagring
    var storedStartingDate = localStorage.getItem("startingDate");
    var storedEndingDate = localStorage.getItem("endingDate");

    // Om sparade värden finns, tilldela dem till respektive input-fält
    if (storedStartingDate) {
        startingDateInput.value = storedStartingDate;
        }

    if (storedEndingDate) {
        endingDateInput.value = storedEndingDate;
        }

    // Lyssna på formulärets submit-händelse
    form.addEventListener("submit", function () {
        // Spara värdena i lokal lagring
        localStorage.setItem("startingDate", startingDateInput.value);
    localStorage.setItem("endingDate", endingDateInput.value);
        });
    });
</script>

