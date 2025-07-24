namespace CinemaApp.Services.Core.Interfaces
{
    using CinemaApp.Web.ViewModels.Admin.MovieManagement;
    using Web.ViewModels.Movie;

    public interface IMovieService
    {
        Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync();

        Task AddMovieAsync(MovieFormInputModel inputModel);

        Task<MovieDetailsViewModel?> GetMovieDetailsByIdAsync(string? id);

        Task<MovieFormInputModel?> GetEditableMovieByIdAsync(string? id);

        Task<bool> EditMovieAsync(MovieFormInputModel inputModel);

        Task<bool> SoftDeleteMovieAsync(string? id);

        Task<bool> DeleteMovieAsync(string? id);
    }
}
