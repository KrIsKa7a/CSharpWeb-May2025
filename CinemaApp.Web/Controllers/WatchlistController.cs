namespace CinemaApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Core.Interfaces;
    using ViewModels.Watchlist;

    public class WatchlistController : BaseController
    {
        private readonly IWatchlistService watchlistService;

        public WatchlistController(IWatchlistService watchlistService)
        {
            this.watchlistService = watchlistService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                string? userId = this.GetUserId();
                if (userId == null)
                {
                    return this.Forbid();
                }

                IEnumerable<WatchlistViewModel> userWatchlist = await this.watchlistService
                    .GetUserWatchlistAsync(userId);
                return View(userWatchlist);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(string? movieId)
        {
            try
            {
                string? userId = this.GetUserId();
                if (userId == null)
                {
                    // Not a valid case, added as defensive mechanism
                    return this.Forbid();
                }

                bool result = await this.watchlistService
                    .AddMovieToUserWatchlistAsync(movieId, userId);
                if (result == false)
                {
                    // TODO: Add JS notifications
                    return this.RedirectToAction(nameof(Index), "Movie");
                }

                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Remove(string? movieId)
        {
            try
            {
                string? userId = this.GetUserId();
                if (userId == null)
                {
                    // Not a valid case, added as defensive mechanism
                    return this.Forbid();
                }

                bool result = await this.watchlistService
                    .RemoveMovieFromWatchlistAsync(movieId, userId);
                if (result == false)
                {
                    // TODO: Add JS notifications
                    return this.RedirectToAction(nameof(Index));
                }

                return this.RedirectToAction(nameof(Index), "Movie");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}
