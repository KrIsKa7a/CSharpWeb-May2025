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

        public async Task<CinemaManagementEditFormModel?> GetCinemaEditFormModelAsync(string? id)
        {
            CinemaManagementEditFormModel? formModel = null;
            if (!String.IsNullOrWhiteSpace(id))
            {
                Cinema? cinemaToEdit = await this.cinemaRepository
                    .GetAllAttached()
                    .Include(c => c.Manager)
                    .ThenInclude(m => m.User)
                    .IgnoreQueryFilters()
                    .SingleOrDefaultAsync(c => c.Id.ToString().ToLower() == id.ToLower());
                if (cinemaToEdit != null)
                {
                    formModel = new CinemaManagementEditFormModel()
                    {
                        Id = cinemaToEdit.Id.ToString(),
                        Name = cinemaToEdit.Name,
                        Location = cinemaToEdit.Location,
                        ManagerEmail = cinemaToEdit.Manager != null ?
                            cinemaToEdit.Manager.User.Email ?? string.Empty : string.Empty,
                    };
                }
            }

            return formModel;
        }

        public async Task<bool> EditCinemaAsync(CinemaManagementEditFormModel? inputModel)
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
                    Cinema? cinemaToEdit = await this.cinemaRepository
                        .SingleOrDefaultAsync(c => c.Id.ToString().ToLower() == inputModel.Id.ToLower());
                    if (manager != null &&
                        cinemaToEdit != null)
                    {
                        cinemaToEdit.Name = inputModel.Name;
                        cinemaToEdit.Location = inputModel.Location;
                        cinemaToEdit.Manager = manager;

                        result = await this.cinemaRepository
                            .UpdateAsync(cinemaToEdit);
                    }
                }
            }

            return result;
        }
    }
}
