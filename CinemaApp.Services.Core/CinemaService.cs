namespace CinemaApp.Services.Core
{
    using Microsoft.EntityFrameworkCore;

    using Data.Repository.Interfaces;
    using Interfaces;
    using Web.ViewModels.Cinema;

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
    }
}
