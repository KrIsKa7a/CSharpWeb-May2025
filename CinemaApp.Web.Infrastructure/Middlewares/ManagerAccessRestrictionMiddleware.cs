namespace CinemaApp.Web.Infrastructure.Middlewares
{
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    using Services.Core.Interfaces;
    using static GCommon.ApplicationConstants;

    public class ManagerAccessRestrictionMiddleware
    {
        private const int HttpForbiddenStatusCode = 403;

        private readonly RequestDelegate next;

        public ManagerAccessRestrictionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IManagerService managerService)
        {
            if (!(context.User.Identity?.IsAuthenticated ?? false))
            {
                // User log-out
                bool managerCookieExists = context
                    .Request
                    .Cookies
                    .ContainsKey(ManagerAuthCookie);
                if (managerCookieExists)
                {
                    context.Response.Cookies.Delete(ManagerAuthCookie);
                }
            }

            string requestPath = context.Request.Path.ToString().ToLower();
            if (requestPath.StartsWith("/manager"))
            {
                if (!(context.User.Identity?.IsAuthenticated ?? false))
                {
                    // Not authenticated user -> Practically could not be reached
                    context.Response.StatusCode = HttpForbiddenStatusCode;
                    return;
                }

                bool cookieValueObtained = context
                    .Request
                    .Cookies
                    .TryGetValue(ManagerAuthCookie, out string cookieValue);
                if (!cookieValueObtained)
                {
                    // Cookie does not exist
                    // 1. The user may not be a manager
                    // 2. Old cookie may have expired
                    string? userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    bool isAuthUserManager = await managerService
                        .ExistsByUserIdAsync(userId);
                    if (!isAuthUserManager)
                    {
                        // User is not a manager
                        context.Response.StatusCode = HttpForbiddenStatusCode;
                        return;
                    }

                    // Refresh the expired cookie
                    await this.AppendManagerAuthCookie(context, userId!);
                }
                else
                {
                    // Cookie exists and is obtained
                    string? userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (userId == null)
                    {
                        // Non authenticated user with stolen cookie
                        context.Response.StatusCode = HttpForbiddenStatusCode;
                        return;
                    }

                    // Check whether the cookie belongs to the current user
                    string hashedUserId = await this.Sha512OverString(userId);
                    if (hashedUserId.ToLower() != cookieValue.ToLower())
                    {
                        // This cookie does not belong to the current user
                        context.Response.StatusCode = HttpForbiddenStatusCode;
                        return;
                    }
                }
            }

            await this.next(context);

            // Here you can write logic for the returning path
        }

        private async Task AppendManagerAuthCookie(HttpContext context, string userId)
        {
            CookieBuilder cookieBuilder = new CookieBuilder()
            {
                Name = ManagerAuthCookie,
                SameSite = SameSiteMode.Strict,
                HttpOnly = true,
                SecurePolicy = CookieSecurePolicy.SameAsRequest,
                MaxAge = TimeSpan.FromHours(4)
            };

            CookieOptions cookieOptions = cookieBuilder.Build(context);
            string hashedUserId = await 
                this.Sha512OverString(userId);
            
            context.Response.Cookies.Append(ManagerAuthCookie, hashedUserId, cookieOptions);
        }

        private async Task<string> Sha512OverString(string userId)
        {
            using SHA512 sha512Manager = SHA512.Create();
            
            byte[] sha512HashBytes = await sha512Manager
                .ComputeHashAsync(new MemoryStream(Encoding.UTF8.GetBytes(userId)));
            string hashedString = BitConverter.ToString(sha512HashBytes)
                .Replace("-", "")
                .Trim()
                .ToLower();
            
            return hashedString;
        }
    }
}
