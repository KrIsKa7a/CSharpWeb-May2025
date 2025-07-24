namespace CinemaApp.Services.Core.Admin
{
    using Microsoft.EntityFrameworkCore;

    using Data.Models;
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

        public async Task<Tuple<bool, bool>> DeleteOrRestoreMovieAsync(string? id)
        {
            bool result = false;
            bool isRestored = false;
            if (!String.IsNullOrWhiteSpace(id))
            {
                Movie? movie = await this.movieRepository
                    .GetAllAttached()
                    .IgnoreQueryFilters()
                    .SingleOrDefaultAsync(m => m.Id.ToString().ToLower() == id.ToLower());
                if (movie != null)
                {
                    if (movie.IsDeleted)
                    {
                        isRestored = true;
                    }

                    movie.IsDeleted = !movie.IsDeleted;

                    result = await this.movieRepository
                        .UpdateAsync(movie);
                }
            }

            return new Tuple<bool, bool>(result, isRestored);
        }
    }
}
