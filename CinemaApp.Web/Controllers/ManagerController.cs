namespace CinemaApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ManagerController : BaseController
    {
        public IActionResult Index()
        {
            return this.Ok("I am manager!!!");
        }
    }
}
