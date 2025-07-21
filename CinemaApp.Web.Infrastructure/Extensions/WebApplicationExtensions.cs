namespace CinemaApp.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    using Data.Seeding.Interfaces;
    using Middlewares;

    public static class WebApplicationExtensions
    {
        public static IApplicationBuilder UseManagerAccessRestriction(this IApplicationBuilder app)
        {
            app.UseMiddleware<ManagerAccessRestrictionMiddleware>();

            return app;
        }

        public static IApplicationBuilder SeedDefaultIdentity(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            IServiceProvider serviceProvider = scope.ServiceProvider;

            IIdentitySeeder identitySeeder = serviceProvider
                .GetRequiredService<IIdentitySeeder>();
            identitySeeder
                .SeedIdentityAsync()
                .GetAwaiter()
                .GetResult();

            return app;
        }
    }
}
