namespace CinemaApp.Data.Repository.Interfaces
{
    using Models;

    public interface ITicketRepository
        : IRepository<Ticket, Guid>, IAsyncRepository<Ticket, Guid>
    {

    }
}
