namespace CinemaApp.Data.Repository
{
    using Interfaces;
    using Models;

    public class ManagerRepository : BaseRepository<Manager, Guid>, IManagerRepository
    {
        public ManagerRepository(CinemaAppDbContext dbContext) 
            : base(dbContext)
        {

        }
    }
}
