namespace CinemaApp.Web.ViewModels.Ticket
{
    public class TicketIndexViewModel
    {
        public string MovieTitle { get; set; } = null!;

        public string MovieImageUrl { get; set; } = null!;

        public string CinemaName { get; set; } = null!;

        public string Showtime { get; set; } = null!;

        public int TicketCount { get; set; }

        public string TicketPrice { get; set; } = null!;

        public string TotalPrice { get; set; } = null!;
    }
}
