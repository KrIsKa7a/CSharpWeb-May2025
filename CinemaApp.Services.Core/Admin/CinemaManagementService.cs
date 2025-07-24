namespace CinemaApp.Services.Core.Admin
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using Data.Models;
    using Data.Repository.Interfaces;
    using Interfaces;
    using Web.ViewModels.Admin.CinemaManagement;

    public class CinemaManagementService : ICinemaManagementService
    {
        private readonly ICinemaRepository cinemaRepository;
        private readonly IManagerRepository managerRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CinemaManagementService(ICinemaRepository cinemaRepository, 
            IManagerRepository managerRepository, UserManager<ApplicationUser> userManager)
        {
            this.cinemaRepository = cinemaRepository;
            this.managerRepository = managerRepository;
            this.userManager = userManager;
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
                    ManagerName = c.Manager != null ?
                        c.Manager.User.UserName : null,
                })
                .ToArrayAsync();

            return allCinemas;
        }

        public async Task<bool> AddCinemaAsync(CinemaManagementAddFormModel? inputModel)
        {
            bool result = false;
            if (inputModel != null)
            {
                ApplicationUser? managerUser = await this.userManager
                    .FindByNameAsync(inputModel.ManagerEmail);
                if (managerUser != null)
                {
                    Manager? manager = await this.managerRepository
                        .GetAllAttached()
                        .SingleOrDefaultAsync(m => m.UserId.ToLower() == managerUser.Id.ToLower());
                    if (manager != null)
                    {
                        Cinema newCinema = new Cinema()
                        {
                            Name = inputModel.Name,
                            Location = inputModel.Location,
                            Manager = manager,
                        };

                        await this.cinemaRepository.AddAsync(newCinema);

                        result = true;
                    }
                }
            }

            return result;
        }
    }
}
