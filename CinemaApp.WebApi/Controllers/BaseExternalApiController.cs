namespace CinemaApp.WebApi.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public abstract class BaseExternalApiController : ControllerBase
    {
        protected bool IsUserAuthenticated()
        {
            bool retRes = false;
            if (this.User.Identity != null)
            {
                retRes = this.User.Identity.IsAuthenticated;
            }

            return retRes;
        }

        protected string? GetUserId()
        {
            string? userId = null;
            if (this.IsUserAuthenticated())
            {
                userId = this.User
                    .FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return userId;
        }
    }
}
