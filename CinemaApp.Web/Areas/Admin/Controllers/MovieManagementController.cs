namespace CinemaApp.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Core.Admin.Interfaces;
    using ViewModels.Admin.MovieManagement;

    using static GCommon.ApplicationConstants;

    public class MovieManagementController : BaseAdminController
    {
        private readonly IMovieManagementService movieManagementService;
        private readonly ILogger<MovieManagementController> logger;

        public MovieManagementController(IMovieManagementService movieManagementService, 
            ILogger<MovieManagementController> logger)
        {
            this.movieManagementService = movieManagementService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            IEnumerable<MovieManagementIndexViewModel> allMovies = await this.movieManagementService
                .GetMovieManagementBoardDataAsync();

            return View(allMovies);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieFormInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                await this.movieManagementService.AddMovieAsync(inputModel);
                TempData[SuccessMessageKey] = "Movie added successfully!";

                return this.RedirectToAction(nameof(Manage));
            }
            catch (Exception e)
            {
                this.logger.LogCritical(e.Message);
                TempData[ErrorMessageKey] = "Fatal error occurred while adding your movie! Please try again later!";
                
                return this.RedirectToAction(nameof(Manage));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            try
            {
                MovieFormInputModel? editableMovie = await this.movieManagementService
                    .GetEditableMovieByIdAsync(id);
                if (editableMovie == null)
                {
                    return this.NotFound();
                }

                return this.View(editableMovie);
            }
            catch (Exception e)
            {
                this.logger.LogCritical(e.Message);
                TempData[ErrorMessageKey] = "Fatal error occurred while updating the movie! Please try again later!";

                return this.RedirectToAction(nameof(Manage));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MovieFormInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                bool editSuccess = await this.movieManagementService
                    .EditMovieAsync(inputModel);
                if (!editSuccess)
                {
                    TempData[ErrorMessageKey] = "Selected Cinema does not exist!";
                }
                else
                {
                    TempData[SuccessMessageKey] = "Cinema updated successfully!";
                }

                return this.RedirectToAction(nameof(Manage));
            }
            catch (Exception e)
            {
                this.logger.LogCritical(e.Message);
                TempData[ErrorMessageKey] = "Fatal error occurred while updating the movie! Please try again later!";

                return this.RedirectToAction(nameof(Manage));
            }
        }

        [HttpGet]
        public async Task<IActionResult> ToggleDelete(string? id)
        {
            Tuple<bool, bool> opResult = await this.movieManagementService
                .DeleteOrRestoreMovieAsync(id);
            bool success = opResult.Item1;
            bool isRestored = opResult.Item2;

            if (!success)
            {
                TempData[ErrorMessageKey] = "Movie could not be found and updated!";
            }
            else
            {
                string operation = isRestored ? "restored" : "deleted";

                TempData[SuccessMessageKey] = $"Movie {operation} successfully!";
            }

            return this.RedirectToAction(nameof(Manage));
        }
    }
}
