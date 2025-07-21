namespace CinemaApp.Services.Core.Admin.Interfaces
{
    using Web.ViewModels.Admin.UserManagement;

    public interface IUserService
    {
        Task<IEnumerable<UserManagementIndexViewModel>> GetUserManagementBoardDataAsync(string userId);
    }
}
