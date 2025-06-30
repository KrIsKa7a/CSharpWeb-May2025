namespace CinemaApp.Data.Repository.Interfaces
{
    using Models;

    public interface IMovieRepository 
        : IRepository<Movie, Guid>, IAsyncRepository<Movie, Guid>
    {

    }
}
