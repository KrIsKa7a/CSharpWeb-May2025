namespace CinemaApp.Web.ViewModels.Admin.CinemaManagement
{
    public class CinemaManagementIndexViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public string? ManagerName { get; set; }
    }
}
