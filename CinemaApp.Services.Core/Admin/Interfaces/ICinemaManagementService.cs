namespace CinemaApp.Services.Core.Admin.Interfaces
{
    using Web.ViewModels.Admin.CinemaManagement;

    public interface ICinemaManagementService
    {
        Task<IEnumerable<CinemaManagementIndexViewModel>> GetCinemaManagementBoardDataAsync();

        Task<bool> AddCinemaAsync(CinemaManagementAddFormModel? inputModel);
    }
}
