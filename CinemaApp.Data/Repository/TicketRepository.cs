namespace CinemaApp.Data.Repository
{
    using Interfaces;
    using Models;

    public class TicketRepository : BaseRepository<Ticket, Guid>, ITicketRepository
    {
        public TicketRepository(CinemaAppDbContext dbContext) 
            : base(dbContext)
        {

        }
    }
}
