namespace CinemaApp.Web.ViewModels.Admin.CinemaManagement
{
    using System.ComponentModel.DataAnnotations;

    using static Data.Common.EntityConstants.Cinema;
    using static Data.Common.EntityConstants.Manager;
    using static ValidationMessages.Cinema;

    public class CinemaManagementAddFormModel
    {
        [Required(ErrorMessage = NameRequiredMessage)]
        [MinLength(NameMinLength, ErrorMessage = NameMinLengthMessage)]
        [MaxLength(NameMaxLength, ErrorMessage = NameMaxLengthMessage)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = LocationRequiredMessage)]
        [MinLength(LocationMinLength, ErrorMessage = LocationMinLengthMessage)]
        [MaxLength(LocationMaxLength, ErrorMessage = LocationMaxLengthMessage)]
        public string Location { get; set; } = null!;

        public IEnumerable<string>? AppManagerEmails { get; set; }

        [Required]
        public string ManagerEmail { get; set; } = null!;
    }
}
