namespace CinemaApp.Services.Core.Admin
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using Data.Models;
    using Data.Repository.Interfaces;
    using Interfaces;
    using Web.ViewModels.Admin.UserManagement;

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IManagerRepository managerRepository;

        public UserService(UserManager<ApplicationUser> userManager, IManagerRepository managerRepository)
        {
            this.userManager = userManager;
            this.managerRepository = managerRepository;
        }

        public async Task<IEnumerable<UserManagementIndexViewModel>> GetUserManagementBoardDataAsync(string userId)
        {
            IEnumerable<UserManagementIndexViewModel> users = await this.userManager
                .Users
                .Where(u => u.Id.ToLower() != userId.ToLower())
                .Select(u => new UserManagementIndexViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    Roles = userManager.GetRolesAsync(u)
                        .GetAwaiter()
                        .GetResult()
                })
                .ToArrayAsync();

            return users;
        }

        public async Task<IEnumerable<string>> GetManagerEmailsAsync()
        {
            IEnumerable<string> managerEmails = await this.managerRepository
                .GetAllAttached()
                .Where(m => m.User.UserName != null)
                .Select(m => (string)m.User.UserName!)
                .ToArrayAsync();

            return managerEmails;
        }
    }
}
