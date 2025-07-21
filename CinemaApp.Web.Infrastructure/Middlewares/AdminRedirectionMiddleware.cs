namespace CinemaApp.Web.Infrastructure.Middlewares
{
    using Microsoft.AspNetCore.Http;

    using static GCommon.ApplicationConstants;

    public class AdminRedirectionMiddleware
    {
        private const string IndexPath = "/";
        private const string AdminIndexPath = "/Admin";

        private readonly RequestDelegate next;

        public AdminRedirectionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated ?? false)
            {
                if (context.Request.Path == IndexPath &&
                    context.User.IsInRole(AdminRoleName))
                {
                    context.Response.Redirect(AdminIndexPath);

                    return;
                }
            }

            await this.next(context);
        }
    }
}
