namespace CinemaApp.Services.Core
{
    using Microsoft.EntityFrameworkCore;

    using Data.Models;
    using Data.Repository.Interfaces;
    using Interfaces;
    using Web.ViewModels.Cinema;

    using static GCommon.ApplicationConstants;

    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository cinemaRepository;

        public CinemaService(ICinemaRepository cinemaRepository)
        {
            this.cinemaRepository = cinemaRepository;
        }

        public async Task<IEnumerable<UsersCinemaIndexViewModel>> GetAllCinemasUserViewAsync()
        {
            IEnumerable<UsersCinemaIndexViewModel> allCinemasUsersView = await this.cinemaRepository
                .GetAllAttached()
                .Select(c => new UsersCinemaIndexViewModel()
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Location = c.Location,
                })
                .ToArrayAsync();
            
            return allCinemasUsersView;
        }

        public async Task<CinemaProgramViewModel?> GetCinemaProgramAsync(string? cinemaId)
        {
            CinemaProgramViewModel? cinemaProgram = null;
            if (!String.IsNullOrWhiteSpace(cinemaId))
            {
                Cinema? cinema = await this.cinemaRepository
                    .GetAllAttached()
                    .Include(c => c.CinemaMovies)
                    .ThenInclude(cm => cm.Movie)
                    .SingleOrDefaultAsync(c => c.Id.ToString().ToLower() == cinemaId.ToLower());
                if (cinema != null)
                {
                    cinemaProgram = new CinemaProgramViewModel()
                    {
                        CinemaId = cinema.Id.ToString(),
                        CinemaName = cinema.Name,
                        CinemaData = cinema.Name + " - " + cinema.Location,
                        Movies = cinema.CinemaMovies
                            .Select(cm => cm.Movie)
                            .Select(m => new CinemaProgramMovieViewModel()
                            {
                                Id = m.Id.ToString(),
                                Title = m.Title,
                                ImageUrl = m.ImageUrl ?? $"/images/{NoImageUrl}",
                                Director = m.Director
                            })
                            .ToArray(),
                    };
                }
            }

            return cinemaProgram;
        }
    }
}
