namespace CinemaApp.Data.Repository
{
    using Interfaces;
    using Models;

    public class MovieRepository : BaseRepository<Movie, Guid>, IMovieRepository
    {
        public MovieRepository(CinemaAppDbContext dbContext) 
            : base(dbContext)
        {

        }
    }
}
