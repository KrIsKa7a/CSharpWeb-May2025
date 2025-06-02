namespace CinemaApp.Services.Core
{
    using System.Globalization;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using Interfaces;
    using Web.ViewModels.Movie;
    using static GCommon.ApplicationConstants;

    public class MovieService : IMovieService
    {
        private readonly CinemaAppDbContext dbContext;

        public MovieService(CinemaAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync()
        {
            IEnumerable<AllMoviesIndexViewModel> allMovies = await this.dbContext
                .Movies
                .AsNoTracking()
                .Select(m => new AllMoviesIndexViewModel()
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    Genre = m.Genre,
                    ReleaseDate = m.ReleaseDate.ToString(AppDateFormat),
                    Director = m.Director,
                    ImageUrl = m.ImageUrl,
                })
                .ToListAsync();
            foreach (AllMoviesIndexViewModel movie in allMovies)
            {
                if (String.IsNullOrEmpty(movie.ImageUrl))
                {
                    movie.ImageUrl = $"~/images/{NoImageUrl}.jpg";
                }
            }

            return allMovies;
        }

        public async Task AddMovieAsync(MovieFormInputModel inputModel)
        {
            Movie newMovie = new Movie()
            {
                Title = inputModel.Title,
                Genre = inputModel.Genre,
                Director = inputModel.Director,
                Description = inputModel.Description,
                Duration = inputModel.Duration,
                ImageUrl = inputModel.ImageUrl,
                ReleaseDate = DateOnly
                    .ParseExact(inputModel.ReleaseDate, AppDateFormat,
                        CultureInfo.InvariantCulture, DateTimeStyles.None),
            };

            await this.dbContext.Movies.AddAsync(newMovie);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
