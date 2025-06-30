namespace CinemaApp.Services.Core
{
    using Microsoft.EntityFrameworkCore;
    
    using Data.Models;
    using Data.Repository.Interfaces;
    using Interfaces;
    using Web.ViewModels.Watchlist;
    using static GCommon.ApplicationConstants;

    public class WatchlistService : IWatchlistService
    {
        private readonly IWatchlistRepository watchlistRepository;

        public WatchlistService(IWatchlistRepository watchlistRepository)
        {
            this.watchlistRepository = watchlistRepository;
        }

        public async Task<IEnumerable<WatchlistViewModel>> GetUserWatchlistAsync(string userId)
        {
            // Due to the use of the built-in IdentityUser, we do not have direct navigation collection from the user side
            IEnumerable<WatchlistViewModel> userWatchlist = await this.watchlistRepository
                .GetAllAttached()
                .Include(aum => aum.Movie)
                .AsNoTracking()
                .Where(aum => aum.ApplicationUserId.ToLower() == userId.ToLower())
                .Select(aum => new WatchlistViewModel()
                {
                    MovieId = aum.MovieId.ToString(),
                    Title = aum.Movie.Title,
                    Genre = aum.Movie.Genre,
                    ReleaseDate = aum.Movie.ReleaseDate.ToString(AppDateFormat),
                    ImageUrl = aum.Movie.ImageUrl ?? $"/images/{NoImageUrl}"
                })
                .ToArrayAsync();

            return userWatchlist;
        }

        public async Task<bool> AddMovieToUserWatchlistAsync(string? movieId, string? userId)
        {
            bool result = false;
            if (movieId != null && userId != null)
            {
                bool isMovieIdValid = Guid.TryParse(movieId, out Guid movieGuid);
                if (isMovieIdValid)
                {
                    ApplicationUserMovie? userMovieEntry = await this.watchlistRepository
                        .GetAllAttached()
                        .IgnoreQueryFilters()
                        .SingleOrDefaultAsync(aum => aum.ApplicationUserId.ToLower() == userId &&
                                                     aum.MovieId.ToString() == movieGuid.ToString());
                    if (userMovieEntry != null)
                    {
                        userMovieEntry.IsDeleted = false;
                        result = 
                            await this.watchlistRepository.UpdateAsync(userMovieEntry);
                    }
                    else
                    {
                        userMovieEntry = new ApplicationUserMovie()
                        {
                            ApplicationUserId = userId,
                            MovieId = movieGuid,
                        };
                        
                        await this.watchlistRepository.AddAsync(userMovieEntry);
                        result = true;
                    }
                }
            }

            return result;
        }

        public async Task<bool> RemoveMovieFromWatchlistAsync(string? movieId, string? userId)
        {
            bool result = false;
            if (movieId != null && userId != null)
            {
                bool isMovieIdValid = Guid.TryParse(movieId, out Guid movieGuid);
                if (isMovieIdValid)
                {
                    ApplicationUserMovie? userMovieEntry = await this.watchlistRepository
                        .SingleOrDefaultAsync(aum => aum.ApplicationUserId.ToLower() == userId &&
                                                     aum.MovieId.ToString() == movieGuid.ToString());
                    if (userMovieEntry != null)
                    {
                        result = 
                            await this.watchlistRepository.DeleteAsync(userMovieEntry);
                    }
                }
            }

            return result;
        }

        public async Task<bool> IsMovieAddedToWatchlist(string? movieId, string? userId)
        {
            bool result = false;
            if (movieId != null && userId != null)
            {
                bool isMovieIdValid = Guid.TryParse(movieId, out Guid movieGuid);
                if (isMovieIdValid)
                {
                    ApplicationUserMovie? userMovieEntry = await this.watchlistRepository
                        .SingleOrDefaultAsync(aum => aum.ApplicationUserId.ToLower() == userId &&
                                                     aum.MovieId.ToString() == movieGuid.ToString());
                    if (userMovieEntry != null)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }
    }
}
