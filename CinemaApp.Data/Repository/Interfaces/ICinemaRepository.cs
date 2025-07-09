namespace CinemaApp.Data.Repository.Interfaces
{
    using Models;

    public interface ICinemaRepository
        : IRepository<Cinema, Guid>, IAsyncRepository<Cinema, Guid>
    {

    }
}
