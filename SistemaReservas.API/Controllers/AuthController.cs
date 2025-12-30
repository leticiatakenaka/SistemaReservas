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
        private readonly IAuthAppService authAppService;

        public AuthController(IAuthAppService authService)
            => this.authAppService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var login = new LoginDto { Email = request.Email, Senha = request.Password };

            var token = await authAppService.Login(login);

            if (token == null) return Unauthorized(new
            {
                success = false,
                errors = "Email ou senha inválida."
            });

            return Ok(new { Token = token });
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    success = false,
                    errors
                });
            }

            var resultado = await authAppService.Registrar(request);

            if (!resultado.Success)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = resultado.Errors
                });
            }

            return Ok(new { success = true, token = resultado.Data });
        }
    }
}
