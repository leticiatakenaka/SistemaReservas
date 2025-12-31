using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaReservas.Application.DTOs;
using SistemaReservas.Application.Interfaces;
using SistemaReservas.Domain.Common;
using System.Security.Claims;

namespace SistemaReservas.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/booking")]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService) => _bookingService = bookingService;

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateBookingDto dto)
        {
            var resultado = await _bookingService.CreateBookingAsync(dto);

            if (resultado.Success)
            {
                return CreatedAtAction(nameof(Create), new { id = resultado.Data }, resultado.Data);
            }

            return BadRequest(new { errors = resultado.Errors });
        }

        [HttpGet()] 
        [ProducesResponseType(typeof(PagedResponse<BookingDto>), 200)]
        public async Task<IActionResult> GetBookings(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string searchTerm = "")
        {
            var hostIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(hostIdString, out Guid userId))
            {
                return Unauthorized("Usuário não identificado ou ID inválido.");
            }

            var resultado = await _bookingService.GetPagedBookingAsync(page, pageSize, searchTerm, userId);

            var response = new PagedResponse<BookingDto>(
                resultado.Items,
                resultado.TotalCount,
                resultado.Page,
                resultado.PageSize
            );

            return Ok(response);
        }
    }
}
