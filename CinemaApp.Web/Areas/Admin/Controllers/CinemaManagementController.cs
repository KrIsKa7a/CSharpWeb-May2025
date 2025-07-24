namespace CinemaApp.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Core.Admin.Interfaces;
    using ViewModels.Admin.CinemaManagement;

    public class CinemaManagementController : BaseAdminController
    {
        private readonly ICinemaManagementService cinemaManagementService;

        public CinemaManagementController(ICinemaManagementService cinemaManagementService)
        {
            this.cinemaManagementService = cinemaManagementService;
        }

        public async Task<IActionResult> Manage()
        {
            IEnumerable<CinemaManagementIndexViewModel> allCinemas = await this.cinemaManagementService
                .GetCinemaManagementBoardDataAsync();
            
            return View(allCinemas);
        }
    }
}
