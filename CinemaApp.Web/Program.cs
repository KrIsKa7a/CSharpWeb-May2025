namespace CinemaApp.Web
{
    using Data;
    using Data.Repository.Interfaces;
    using Infrastructure.Extensions;
    using Services.Core.Interfaces;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
										?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // TODO: Implement extension methods for adding DbContext, Identity
            builder.Services
	            .AddDbContext<CinemaAppDbContext>(options =>
	            {
		            options.UseSqlServer(connectionString);
	            });
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services
	            .AddDefaultIdentity<IdentityUser>(options =>
	            {
		            options.SignIn.RequireConfirmedEmail = false;
		            options.SignIn.RequireConfirmedAccount = false;
		            options.SignIn.RequireConfirmedPhoneNumber = false;

		            options.Password.RequiredLength = 3;
		            options.Password.RequireNonAlphanumeric = false;
		            options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredUniqueChars = 0;
	            })
                .AddEntityFrameworkStores<CinemaAppDbContext>();

            builder.Services.AddRepositories(typeof(IMovieRepository).Assembly);
            builder.Services.AddUserDefinedServices(typeof(IMovieService).Assembly);

            builder.Services.AddControllersWithViews();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseManagerAccessRestriction();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
