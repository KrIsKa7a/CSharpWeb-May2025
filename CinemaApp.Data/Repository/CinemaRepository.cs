namespace CinemaApp.Data.Repository
{
    using Interfaces;
    using Models;

    public class CinemaRepository : BaseRepository<Cinema, Guid>, ICinemaRepository
    {
        public CinemaRepository(CinemaAppDbContext dbContext) 
            : base(dbContext)
        {

        }
    }
}
