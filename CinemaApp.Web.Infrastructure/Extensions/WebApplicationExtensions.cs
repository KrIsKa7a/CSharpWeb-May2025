namespace CinemaApp.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;

    using Middlewares;

    public static class WebApplicationExtensions
    {
        public static IApplicationBuilder UseManagerAccessRestriction(this IApplicationBuilder app)
        {
            app.UseMiddleware<ManagerAccessRestrictionMiddleware>();

            return app;
        }
    }
}
