using Microsoft.AspNetCore.Mvc;
using Prestamos.Business.Interfaces;
using Prestamos.Entities.DTOs;

namespace Prestamos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamosController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;

        public PrestamosController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        [HttpPost("calcular")]
        public async Task<IActionResult> Calcular([FromBody] CalcularPrestamoRequestDto request)
        {
            if (request == null)
            {
                return BadRequest("La solicitud es requerida.");
            }

            if (request.Monto <= 0)
            {
                return BadRequest("El monto debe ser mayor que cero.");
            }

            if (request.Meses <= 0)
            {
                return BadRequest("La cantidad de meses debe ser mayor que cero.");
            }

            var ipConsulta = HttpContext.Connection.RemoteIpAddress?.ToString();

            var result = await _prestamoService.CalcularPrestamoAsync(request, ipConsulta);

            return Ok(result);
        }
    }
}
