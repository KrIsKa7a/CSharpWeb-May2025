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
            try
            {
                IEnumerable<UsersCinemaIndexViewModel> allCinemasUserView = await this.cinemaService
                    .GetAllCinemasUserViewAsync();

                return View(allCinemasUserView);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return this.RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Program(string? id)
        {
            try
            {
                // TODO: Implement showing Showtimes on Program and choosing Showtime when buying a Ticket
                CinemaProgramViewModel? cinemaProgram = await this.cinemaService
                    .GetCinemaProgramAsync(id);
                if (cinemaProgram == null)
                {
                    return this.RedirectToAction(nameof(Index));
                }

                return this.View(cinemaProgram);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return this.RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string? id)
        {
            try
            {
                CinemaDetailsViewModel? cinemaProgram = await this.cinemaService
                    .GetCinemaDetailsAsync(id);
                if (cinemaProgram == null)
                {
                    return this.RedirectToAction(nameof(Index));
                }

                return this.View(cinemaProgram);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return this.RedirectToAction(nameof(Index));
            }
        }
    }
}
