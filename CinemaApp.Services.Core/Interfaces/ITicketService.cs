namespace CinemaApp.Services.Core.Interfaces
{
    using Web.ViewModels.Ticket;

    public interface ITicketService
    {
        Task<IEnumerable<TicketIndexViewModel>> GetUserTicketsAsync(string? userId);

        Task<bool> AddTicketAsync(string? cinemaId, string? movieId, int quantity, string? showtime, string? userId);
    }
}
