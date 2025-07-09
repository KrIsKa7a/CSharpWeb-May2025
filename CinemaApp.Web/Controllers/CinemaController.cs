namespace CinemaApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Core.Interfaces;
    using ViewModels.Cinema;

    public class CinemaController : BaseController
    {
        private readonly ICinemaService cinemaService;

        public CinemaController(ICinemaService cinemaService)
        {
            this.cinemaService = cinemaService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<UsersCinemaIndexViewModel> allCinemasUserView = await this.cinemaService
                .GetAllCinemasUserViewAsync();
            
            return View(allCinemasUserView);
        }

        [HttpGet]
        public async Task<IActionResult> Program(string? id)
        {

        }
    }
}
