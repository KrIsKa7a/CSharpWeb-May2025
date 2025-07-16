namespace CinemaApp.Data.Repository.Interfaces
{
    using Models;

    public interface ICinemaMovieRepository
        : IRepository<CinemaMovie, Guid>, IAsyncRepository<CinemaMovie, Guid>
    {

    }
}
