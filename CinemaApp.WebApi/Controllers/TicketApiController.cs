namespace CinemaApp.WebApi.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Core.Interfaces;

    [Route("[controller]")]
    [ApiController]
    public class TicketApiController : ControllerBase
    {
        private readonly ITicketService ticketService;

        public TicketApiController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("Buy")]
        public async Task<ActionResult> BuyTicket([Required]string cinemaId, 
            [Required]string movieId, int quantity, [Required]string showtime)
        {
            string? userId = this.GetUserId();
            bool result = await this.ticketService
                .AddTicketAsync(cinemaId, movieId, quantity, showtime, userId);
            if (result == false)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        // TODO: Refactor into BaseApiController
        private bool IsUserAuthenticated()
        {
            bool retRes = false;
            if (this.User.Identity != null)
            {
                retRes = this.User.Identity.IsAuthenticated;
            }

            return retRes;
        }

        private string? GetUserId()
        {
            string? userId = null;
            if (this.IsUserAuthenticated())
            {
                userId = this.User
                    .FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return userId;
        }
    }
}
