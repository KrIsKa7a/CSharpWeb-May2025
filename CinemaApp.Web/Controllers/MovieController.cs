namespace CinemaApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Core.Interfaces;
    using ViewModels.Movie;
    using static ViewModels.ValidationMessages.Movie;

    public class MovieController : Controller
    {
        private readonly IMovieService movieService;

        // Constructor of the Controller is invoked by ASP.NET Core
        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllMoviesIndexViewModel> allMovies = await this.movieService
                .GetAllMoviesAsync();

            return View(allMovies);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(MovieFormInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                await this.movieService.AddMovieAsync(inputModel);

                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                // TODO: Implement it with the ILogger
                Console.WriteLine(e.Message);

                this.ModelState.AddModelError(string.Empty, ServiceCreateError);
                return this.View(inputModel);
            }
        }
    }
}
