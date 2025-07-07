namespace CinemaApp.Data.Repository.Interfaces
{
    using Models;

    public interface IManagerRepository
        : IRepository<Manager, Guid>, IAsyncRepository<Manager, Guid>
    {

    }
}
