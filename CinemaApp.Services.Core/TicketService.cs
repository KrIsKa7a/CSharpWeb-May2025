namespace CinemaApp.Services.Core
{
    using Microsoft.EntityFrameworkCore;

    using Data.Repository.Interfaces;
    using Interfaces;
    using Web.ViewModels.Ticket;

    using static GCommon.ApplicationConstants;

    public class TicketService : ITicketService
    {
        private readonly ITicketRepository ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<TicketIndexViewModel>> GetUserTicketsAsync(string? userId)
        {
            IEnumerable<TicketIndexViewModel> userTickets = new List<TicketIndexViewModel>();
            if (!String.IsNullOrWhiteSpace(userId))
            {
                userTickets = await this.ticketRepository
                    .GetAllAttached()
                    .Where(t => t.UserId.ToLower() == userId.ToLower())
                    .Select(t => new TicketIndexViewModel()
                    {
                        MovieTitle = t.CinemaMovieProjection.Movie.Title,
                        MovieImageUrl = t.CinemaMovieProjection.Movie.ImageUrl ?? $"/images/{NoImageUrl}",
                        CinemaName = t.CinemaMovieProjection.Cinema.Name,
                        Showtime = t.CinemaMovieProjection.Showtime,
                        TicketCount = t.Quantity,
                        TicketPrice = t.Price.ToString("F2"),
                        TotalPrice = (t.Quantity * t.Price).ToString("F2"),
                    })
                    .ToArrayAsync();
            }

            return userTickets;
        }
    }
}
