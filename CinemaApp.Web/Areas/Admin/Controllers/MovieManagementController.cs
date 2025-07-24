namespace CinemaApp.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Core.Admin.Interfaces;
    using ViewModels.Admin.MovieManagement;

    public class MovieManagementController : BaseAdminController
    {
        private readonly IMovieManagementService movieManagementService;

        public MovieManagementController(IMovieManagementService movieManagementService)
        {
            this.movieManagementService = movieManagementService;
        }

        public async Task<IActionResult> Manage()
        {
            IEnumerable<MovieManagementIndexViewModel> allMovies = await this.movieManagementService
                .GetMovieManagementBoardDataAsync();

            return View(allMovies);
        }
    }
}
