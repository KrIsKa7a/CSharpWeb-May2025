namespace CinemaApp.Web.ViewModels.Admin.UserManagement
{
    public class UserManagementIndexViewModel
    {
        public string Id { get; set; } = null!;

        public string Email { get; set; } = null!;

        public IEnumerable<string> Roles { get; set; } = null!;
    }
}
