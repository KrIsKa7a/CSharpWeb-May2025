namespace CinemaApp.Web.Controllers
{
    using ViewModels.Watchlist;

    using Microsoft.AspNetCore.Mvc;

    public class WatchlistController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Hardcoded empty watchlist for testing
            IEnumerable<WatchlistViewModel> emptyWatchlist = 
                new List<WatchlistViewModel>();
            return View(emptyWatchlist);
        }
    }
}
