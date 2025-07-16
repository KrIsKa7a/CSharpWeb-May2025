namespace CinemaApp.Data.Repository
{
    using Interfaces;
    using Models;

    public class CinemaMovieRepository 
        : BaseRepository<CinemaMovie, Guid>, ICinemaMovieRepository
    {
        public CinemaMovieRepository(CinemaAppDbContext dbContext) 
            : base(dbContext)
        {

        }
    }
}
