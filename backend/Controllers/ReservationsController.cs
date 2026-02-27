using Microsoft.AspNetCore.Mvc;
using Ristorante.Models;
using Ristorante.Services.Interfaces;

namespace Ristorante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _service;

        public ReservationsController(IReservationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CreateAsync(reservation);

            return result.Success
                ? Ok(result)
                : result.Data == null
                    ? NotFound(result)
                    : BadRequest(result);
        }
    }
}
