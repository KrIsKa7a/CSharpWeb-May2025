namespace CinemaApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Core.Interfaces;
    using ViewModels.Movie;

    public class MovieController : BaseController
    {
        private readonly IMovieService movieService;
        private readonly IWatchlistService watchlistService;

        // Constructor of the Controller is invoked by ASP.NET Core
        public MovieController(IMovieService movieService, IWatchlistService watchlistService)
        {
            this.movieService = movieService;
            this.watchlistService = watchlistService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllMoviesIndexViewModel> allMovies = await this.movieService
                .GetAllMoviesAsync();
            if (this.IsUserAuthenticated())
            {
                foreach (AllMoviesIndexViewModel movieIndexVM in allMovies)
                {
                    movieIndexVM.IsAddedToUserWatchlist = await this.watchlistService
                        .IsMovieAddedToWatchlist(movieIndexVM.Id, this.GetUserId());
                }
            }

            return View(allMovies);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string? id)
        {
            try
            {
                MovieDetailsViewModel? movieDetails = await this.movieService
                    .GetMovieDetailsByIdAsync(id);
                if (movieDetails == null)
                {
                    // TODO: Custom 404 page
                    return this.RedirectToAction(nameof(Index));
                }

                return this.View(movieDetails);
            }
            catch (Exception e)
            {
                // TODO: Implement it with the ILogger
                // TODO: Add JS bars to indicate such errors
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DetailsPartial(string? id)
        {
            // TODO: Refactor the functionality to use WebAPI
            try
            {
                MovieDetailsViewModel? movieDetails = await this.movieService
                    .GetMovieDetailsByIdAsync(id);
                if (movieDetails == null)
                {
                    // TODO: Custom 404 page
                    return this.RedirectToAction(nameof(Index));
                }

                return this.View("_MovieDetailsPartial", movieDetails);
            }
            catch (Exception e)
            {
                // TODO: Implement it with the ILogger
                // TODO: Add JS bars to indicate such errors
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index));
            }
        }
    }
}
