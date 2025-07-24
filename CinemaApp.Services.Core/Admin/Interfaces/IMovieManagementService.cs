namespace CinemaApp.Services.Core.Admin.Interfaces
{
    using Core.Interfaces;
    using Web.ViewModels.Admin.MovieManagement;

    public interface IMovieManagementService : IMovieService
    {
        Task<IEnumerable<MovieManagementIndexViewModel>> GetMovieManagementBoardDataAsync();

        Task<Tuple<bool, bool>> DeleteOrRestoreMovieAsync(string? id);
    }
}
