namespace CinemaApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Core.Interfaces;
    using ViewModels.Ticket;

    public class TicketApiController : BaseInternalApiController
    {
        private readonly ITicketService ticketService;

        public TicketApiController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("Buy")]
        public async Task<ActionResult> BuyTicket([FromBody] BuyTicketInputModel ticketInputModel)
        {
            string? userId = this.GetUserId();
            bool result = await this.ticketService
                .AddTicketAsync(ticketInputModel.CinemaId, ticketInputModel.MovieId, ticketInputModel.Quantity, ticketInputModel.Showtime, userId);
            if (result == false)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}
