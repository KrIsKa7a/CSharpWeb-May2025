namespace CinemaApp.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Core.Admin.Interfaces;
    using ViewModels.Admin.CinemaManagement;

    using static GCommon.ApplicationConstants;

    public class CinemaManagementController : BaseAdminController
    {
        private readonly ICinemaManagementService cinemaManagementService;
        private readonly IUserService userService;

        public CinemaManagementController(ICinemaManagementService cinemaManagementService,
            IUserService userService)
        {
            this.cinemaManagementService = cinemaManagementService;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            IEnumerable<CinemaManagementIndexViewModel> allCinemas = await this.cinemaManagementService
                .GetCinemaManagementBoardDataAsync();
            
            return View(allCinemas);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CinemaManagementAddFormModel viewModel = new CinemaManagementAddFormModel()
            {
                AppManagerEmails = await this.userService.GetManagerEmailsAsync(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CinemaManagementAddFormModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                bool success = await this.cinemaManagementService
                    .AddCinemaAsync(inputModel);
                if (!success)
                {
                    TempData[ErrorMessageKey] = "Error occurred while adding the cinema! Ensure to select a valid manager!";
                }
                else
                {
                    TempData[SuccessMessageKey] = "Cinema created successfully!";
                }

                return this.RedirectToAction(nameof(Manage));
            }
            catch (Exception e)
            {
                TempData[ErrorMessageKey] =
                    "Unexpected error occurred while adding the cinema! Please contact developer team!";

                return this.RedirectToAction(nameof(Manage));
            }
        }
    }
}
