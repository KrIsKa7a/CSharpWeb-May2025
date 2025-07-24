namespace CinemaApp.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Core.Admin.Interfaces;
    using ViewModels.Admin.CinemaManagement;

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
                    return this.BadRequest();
                }

                return this.RedirectToAction(nameof(Manage));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
