namespace CinemaApp.Web.ViewModels.Admin.CinemaManagement
{
    using System.ComponentModel.DataAnnotations;

    public class CinemaManagementEditFormModel : CinemaManagementAddFormModel
    {
        [Required]
        public string Id { get; set; } = null!;
    }
}
