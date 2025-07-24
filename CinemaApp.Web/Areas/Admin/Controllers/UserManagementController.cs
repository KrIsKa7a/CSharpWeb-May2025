namespace CinemaApp.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using CinemaApp.Web.ViewModels.Admin.UserManagement;
    using Services.Core.Admin.Interfaces;

    public class UserManagementController : BaseAdminController
    {
        private readonly IUserService userService;

        public UserManagementController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<UserManagementIndexViewModel> allUsers = await this.userService
                .GetUserManagementBoardDataAsync(this.GetUserId()!);
            
            return View(allUsers);
        }
    }
}
