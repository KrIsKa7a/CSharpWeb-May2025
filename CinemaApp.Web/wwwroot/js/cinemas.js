document.addEventListener("DOMContentLoaded", function () {
    let searchBar = document.getElementById("searchBar");
    let cityFilter = document.getElementById("cityFilter");

    function filterCinemas() {
        let searchValue = searchBar.value.toLowerCase();
        let cityValue = cityFilter.value.toLowerCase();
        let cinemaCards = document.querySelectorAll(".cinema-card");

        cinemaCards.forEach(cinema => {
            let cinemaName = cinema.querySelector(".card-title").textContent.toLowerCase();
            let cinemaCity = cinema.getAttribute("data-city").toLowerCase();

            let matchesSearch = searchValue === "" || cinemaName.includes(searchValue);
            let matchesCity = cityValue === "" || cinemaCity === cityValue;

            if (matchesSearch && matchesCity) {
                cinema.style.display = "block";
            } else {
                cinema.style.display = "none";
            }
        });
    }

    searchBar.addEventListener("keyup", filterCinemas);
    cityFilter.addEventListener("change", filterCinemas);
});
