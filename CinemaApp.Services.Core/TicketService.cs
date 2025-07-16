namespace CinemaApp.Services.Core
{
    using Data.Models;
    using Microsoft.EntityFrameworkCore;

    using Data.Repository.Interfaces;
    using Interfaces;
    using Web.ViewModels.Ticket;

    using static GCommon.ApplicationConstants;

    public class TicketService : ITicketService
    {
        private readonly ITicketRepository ticketRepository;
        private readonly ICinemaMovieRepository cinemaMovieRepository;

        public TicketService(ITicketRepository ticketRepository,
            ICinemaMovieRepository cinemaMovieRepository)
        {
            this.ticketRepository = ticketRepository;
            this.cinemaMovieRepository = cinemaMovieRepository;
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

        public async Task<bool> AddTicketAsync(string? cinemaId, string? movieId, int quantity, string? showtime, string? userId)
        {
            bool result = false;
            if (!String.IsNullOrWhiteSpace(cinemaId) &&
                !String.IsNullOrWhiteSpace(movieId) &&
                !String.IsNullOrWhiteSpace(showtime) &&
                !String.IsNullOrWhiteSpace(userId) &&
                quantity > 0)
            {
                CinemaMovie? projection = await this.cinemaMovieRepository
                    .SingleOrDefaultAsync(cm => cm.CinemaId.ToString().ToLower() == cinemaId.ToLower() &&
                                                cm.MovieId.ToString().ToLower() == movieId.ToLower() &&
                                                cm.Showtime == showtime);
                if (projection != null &&
                    projection.AvailableTickets >= quantity)
                {
                    // TODO: Implement ticket pricing elsewhere
                    Ticket newTicket = new Ticket()
                    {
                        Quantity = quantity,
                        CinemaMovieProjection = projection,
                        UserId = userId,
                        Price = 5,
                    };

                    await this.ticketRepository.AddAsync(newTicket);

                    projection.AvailableTickets -= quantity;
                    result = await this.cinemaMovieRepository.UpdateAsync(projection);
                }
            }

            return result;
        }
    }
}
