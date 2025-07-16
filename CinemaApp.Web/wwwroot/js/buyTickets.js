$(document).ready(function () {
    $(".buy-ticket-btn").on("click", function () {
        const cinemaId = $(this).attr("data-cinema-id");
        const cinemaName = $(this).attr("data-cinema-name");
        const movieId = $(this).attr("data-movie-id");
        const movieName = $(this).attr("data-movie-name");

        console.log("Cinema ID:", cinemaId);
        console.log("Cinema Name:", cinemaName);
        console.log("Movie ID:", movieId);
        console.log("Movie Name:", movieName);

        if (!cinemaId || !movieId) {
            Swal.fire("Error!", "Missing Cinema ID or Movie ID.", "error");
            return;
        }

        $("#cinemaId").val(cinemaId);
        $("#movieId").val(movieId);
        $("#cinemaNamePlaceholder").text(cinemaName);

        $("#buyTicketModalLabel").html(`Buy Ticket - ${cinemaName} <br><small class="text-muted">${movieName}</small>`);

        $.ajax({
            url: "https://localhost:7165/CinemaMovieApi/Showtimes",
            method: "GET",
            data: {
                cinemaId: cinemaId,
                movieId: movieId,
            },
            success: function (showtimes) {
                console.log("Success Response:", showtimes); // ✅ Log the success response

                const showtimesSelector = document.getElementById('showtimeSelect');
                showtimesSelector.innerHTML = '<option value="" disabled selected>Choose a time</option>';
                showtimes.forEach(time => {
                    const optionSubtag = document.createElement('option');

                    optionSubtag.value = time;
                    optionSubtag.textContent = time;

                    showtimesSelector.appendChild(optionSubtag);
                });

                $("#buyTicketModal").modal("show");
            },
            error: function (xhr) {
                let errorMessage = "An error occurred while fetching showtimes.";
                console.error("Raw Response:", xhr.responseText); // Log the raw response

                try {
                    if (xhr.responseJSON) {
                        errorMessage = xhr.responseJSON.title || xhr.responseJSON.message || errorMessage;
                    } else if (xhr.responseText) {
                        errorMessage = xhr.responseText; // Use response as-is if it's plain text
                    }
                } catch (e) {
                    console.error("Error processing response:", e);
                }

                Swal.fire("Error!", errorMessage, "error");
            }
        })
    });

    $('#showtimeSelect').change(function () {
        let selectedVal = $(this).val();

        if (selectedVal !== "") {
            $("#availableTicketsDiv").prop("hidden", false);

            $.ajax({
                url: "https://localhost:7165/CinemaMovieApi/AvailableTickets",
                method: "GET",
                data: {
                    cinemaId: cinemaId,
                    movieId: movieId,
                    showtime: selectedVal,
                },
                success: function (availableTicketsCount) {
                    console.log("Success Response:", availableTicketsCount); // ✅ Log the success response

                    $('#availableTicketsCount').text(availableTicketsCount);
                    $('#buyTicketButton').prop("disabled", false);
                },
                error: function (xhr) {
                    let errorMessage = "An error occurred while fetching showtimes.";
                    console.error("Raw Response:", xhr.responseText); // Log the raw response

                    try {
                        if (xhr.responseJSON) {
                            errorMessage = xhr.responseJSON.title || xhr.responseJSON.message || errorMessage;
                        } else if (xhr.responseText) {
                            errorMessage = xhr.responseText; // Use response as-is if it's plain text
                        }
                    } catch (e) {
                        console.error("Error processing response:", e);
                    }

                    Swal.fire("Error!", errorMessage, "error");
                }
            });
        }
    });

    $("#buyTicketButton").on("click", function () {
        const requestData = {
            cinemaId: $("#cinemaId").val().trim(),
            movieId: $("#movieId").val().trim(),
            showtime: $("#showtimeSelect").val().trim(),
            quantity: parseInt($("#quantity").val(), 10)
        };

        console.log("Submitting Request:", requestData); // ✅ Added for Debugging

        if (!requestData.quantity || requestData.quantity < 1) {
            $("#errorMessage").text("Please enter a valid ticket quantity.").removeClass("d-none");
            return;
        }

        $.ajax({
            url: "https://localhost:7165/TicketApi/Buy",
            method: "POST",
            contentType: "text/plain",
            data: JSON.stringify(requestData),
            xhrFields: {
                withCredentials: true,
            },
            crossDomain: true,
            success: function (response) {
                console.log("Success Response:", response); // ✅ Log the success response
                Swal.fire("Success!", "Your ticket has been purchased successfully!", "success");
                $("#buyTicketModal").modal("hide");
            },
            error: function (xhr) {
                let errorMessage = "An error occurred while purchasing tickets.";
                console.error("Raw Response:", xhr.responseText); // Log the raw response

                try {
                    if (xhr.responseJSON) {
                        errorMessage = xhr.responseJSON.title || xhr.responseJSON.message || errorMessage;
                    } else if (xhr.responseText) {
                        errorMessage = xhr.responseText; // Use response as-is if it's plain text
                    }
                } catch (e) {
                    console.error("Error processing response:", e);
                }

                Swal.fire("Error!", errorMessage, "error");
            }

        });
    });
});
