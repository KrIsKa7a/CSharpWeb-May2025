namespace CinemaApp.Web.ViewModels.Ticket
{
    using System.ComponentModel.DataAnnotations;

    public class BuyTicketInputModel
    {
        [Required]
        public string CinemaId { get; set; } = null!;
        
        [Required] 
        public string MovieId { get; set; } = null!;
        
        public int Quantity { get; set; } 
        
        [Required] 
        public string Showtime { get; set; } = null!;
    }
}
