namespace CinemaApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
