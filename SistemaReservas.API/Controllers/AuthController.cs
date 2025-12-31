using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SistemaReservas.Application.DTOs;
using SistemaReservas.Application.Interfaces;

namespace SistemaReservas.API.Controllers
{

    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
            => _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var login = new LoginDto { Email = request.Email, Senha = request.Password };

            var tokenString = await _authService.Login(login);

            if (tokenString == null) return Unauthorized();

            return Ok(new { token = tokenString });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var resultado = await _authService.Register(request);

            if (!resultado.Success)
            {
                return BadRequest(new { errors = resultado.Errors });
            }

            return Ok(resultado.Data);
        }
    }
}
