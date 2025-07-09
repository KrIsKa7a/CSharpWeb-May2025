document.addEventListener("DOMContentLoaded", function () {
    console.log("✅ DOM fully loaded and parsed.");

    const modalElement = document.getElementById("movieDetailsModal");
    if (!modalElement) {
        console.error("❌ Error: Modal element #movieDetailsModal not found!");
        return;
    }

    const movieDetailsModal = new bootstrap.Modal(modalElement);
    const detailsContainer = document.getElementById("movieDetailsContent");

    // Attach event listeners to all 'View Details' buttons
    const viewDetailsButtons = document.querySelectorAll(".view-details-btn");
    if (viewDetailsButtons.length === 0) {
        console.warn("⚠️ No '.view-details-btn' elements found on the page.");
    }

    viewDetailsButtons.forEach(button => {
        button.addEventListener("click", function (event) {
            event.preventDefault();

            let movieId = this.getAttribute("data-movie-id");
            console.log(`🎬 Fetching details for movie ID: ${movieId}`);

            fetch(`/Movie/DetailsPartial/${movieId}`)
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`❌ Failed to load movie details! Status: ${response.status}`);
                    }
                    return response.text();
                })
                .then(data => {
                    console.log("📥 Movie details received:", data);

                    detailsContainer.innerHTML = data;

                    let movieTitleElement = detailsContainer.querySelector("h3");
                    let movieTitle = movieTitleElement ? movieTitleElement.textContent : "Movie Details";
                    document.getElementById("movieDetailsLabel").textContent = movieTitle;

                    let modalWatchlistBtn = document.getElementById("add-to-watchlist-btn");

                    if (modalWatchlistBtn) {
                        modalWatchlistBtn.setAttribute("data-movie-id", movieId);

                        // Check if the current page is Watchlist, then hide the button
                        if (window.location.pathname.includes("/Watchlist")) {
                            modalWatchlistBtn.style.display = "none";
                        } else {
                            // Make AJAX request to check if the movie is already in the watchlist
                            fetch(`/Watchlist/IsMovieInWatchlist/${movieId}`)
                                .then(response => response.json())
                                .then(isInWatchlist => {
                                    if (isInWatchlist) {
                                        modalWatchlistBtn.style.display = "none";
                                    } else {
                                        modalWatchlistBtn.style.display = "inline-block";
                                    }
                                })
                                .catch(error => {
                                    console.error("⚠️ Error checking watchlist status:", error);
                                });
                        }
                    }

                    detailsContainer.style.display = "block";
                    movieDetailsModal.show();

                    attachDynamicEventListeners();
                })
                .catch(error => {
                    console.error("❌ Error loading movie details:", error);
                    detailsContainer.innerHTML = `<p class="text-danger">Failed to load movie details. Please try again later.</p>`;
                });
        });
    });

    function attachDynamicEventListeners() {
        setTimeout(() => {
            console.log("🔄 Attaching dynamic event listeners...");

            $("#add-to-watchlist-btn").off("click").on("click", function () {
                let movieId = $(this).data("movie-id");

                if (!movieId) {
                    Swal.fire("Error!", "Movie ID is missing.", "error");
                    return;
                }

                $.post("/Watchlist/AddToWatchlist", { movieId: movieId })
                    .done(function () {
                        Swal.fire({
                            title: "Added!",
                            text: "The movie has been added to your watchlist.",
                            icon: "success",
                            confirmButtonColor: "#28a745"
                        });

                        $("#add-to-watchlist-btn").hide(); // Hide button after adding
                    })
                    .fail(function () {
                        Swal.fire({
                            title: "Error!",
                            text: "Failed to add the movie to your watchlist. Please try again.",
                            icon: "error",
                            confirmButtonColor: "#dc3545"
                        });
                    });
            });

        }, 200);
    }
});

// jQuery for "Add to Watchlist" buttons in the Movie List
$(document).ready(function () {
    $(".add-to-watchlist-btn").on("click", function () {
        const movieId = $(this).data("movie-id");

        if (!movieId) {
            Swal.fire("Error!", "Movie ID is missing.", "error");
            return;
        }

        $.post("/Watchlist/AddToWatchlist", { movieId: movieId })
            .done(function () {
                Swal.fire({
                    title: "Added!",
                    text: "The movie has been added to your watchlist.",
                    icon: "success",
                    confirmButtonColor: "#28a745"
                });
            })
            .fail(function () {
                Swal.fire({
                    title: "Error!",
                    text: "Failed to add the movie to your watchlist. Please try again.",
                    icon: "error",
                    confirmButtonColor: "#dc3545"
                });
            });
    });
});
