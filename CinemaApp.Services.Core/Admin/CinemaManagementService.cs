namespace CinemaApp.Services.Core.Admin
{
    using Microsoft.EntityFrameworkCore;

    using Data.Repository.Interfaces;
    using Interfaces;
    using Web.ViewModels.Admin.CinemaManagement;

    public class CinemaManagementService : ICinemaManagementService
    {
        private readonly ICinemaRepository cinemaRepository;

        public CinemaManagementService(ICinemaRepository cinemaRepository)
        {
            this.cinemaRepository = cinemaRepository;
        }

        public async Task<IEnumerable<CinemaManagementIndexViewModel>> GetCinemaManagementBoardDataAsync()
        {
            IEnumerable<CinemaManagementIndexViewModel> allCinemas = await this.cinemaRepository
                .GetAllAttached()
                .IgnoreQueryFilters()
                .Select(c => new CinemaManagementIndexViewModel()
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Location = c.Location,
                    IsDeleted = c.IsDeleted,
                })
                .ToArrayAsync();

            return allCinemas;
        }
    }
}
