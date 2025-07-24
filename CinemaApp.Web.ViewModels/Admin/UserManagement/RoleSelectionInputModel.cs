namespace CinemaApp.Web.ViewModels.Admin.UserManagement
{
    using System.ComponentModel.DataAnnotations;

    public class RoleSelectionInputModel
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;
    }
}
