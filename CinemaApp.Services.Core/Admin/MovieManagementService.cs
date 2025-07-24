namespace CinemaApp.Services.Core.Admin
{
    using Microsoft.EntityFrameworkCore;

    using Data.Repository.Interfaces;
    using Interfaces;
    using Web.ViewModels.Admin.MovieManagement;

    using static GCommon.ApplicationConstants;

    public class MovieManagementService : MovieService, IMovieManagementService

    {
        private readonly IMovieRepository movieRepository;

        public MovieManagementService(IMovieRepository movieRepository) 
            : base(movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieManagementIndexViewModel>> GetMovieManagementBoardDataAsync()
        {
            IEnumerable<MovieManagementIndexViewModel> allMovies = await this.movieRepository
                .GetAllAttached()
                .IgnoreQueryFilters()
                .Select(m => new MovieManagementIndexViewModel()
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    Genre = m.Genre,
                    Director = m.Director,
                    Duration = m.Duration,
                    IsDeleted = m.IsDeleted,
                    ReleaseDate = m.ReleaseDate.ToString(AppDateFormat)
                })
                .ToArrayAsync();

            return allMovies;
        }
    }
}
