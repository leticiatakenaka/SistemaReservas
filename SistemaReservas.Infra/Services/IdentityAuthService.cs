using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SistemaReservas.Application.DTOs;
using SistemaReservas.Application.Interfaces;
using SistemaReservas.Infrastructure.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaReservas.Infrastructure.Services
{
    public class IdentityAuthService : IAuthGateway
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public IdentityAuthService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<string> Autenticar(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            if (!result.Succeeded) return null;

            return await GerarJwtTokenAsync(user);
        }

        public async Task<OperationResultDto<string>> Registrar(RegistrarUsuarioRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.NomeDeUsuario,
                Email = request.Email,
                PrimeiroNome = request.PrimeiroNome,
                UltimoNome = request.UltimoNome
            };

            try
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(MapearIdentityErrorParaMensagem).ToList();
                    return OperationResultDto<string>.Fail(errors.ToArray());
                }
            }
            catch (Exception)
            {
                var erro = "Erro inesperado.";
                return OperationResultDto<string>.Fail(erro);
            }

            var token = await GerarJwtTokenAsync(user);
            return OperationResultDto<string>.Ok(token);
        }

        private async Task<string?> GerarJwtTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string MapearIdentityErrorParaMensagem(IdentityError error)
        {
            return error.Code switch
            {
                "DuplicateUserName" => "Este nome de usuário já está em uso.",
                "DuplicateEmail" => "Este e-mail já está registrado.",
                "PasswordTooShort" => "A senha é muito curta. O mínimo é 6 caracteres.",
                "PasswordRequiresNonAlphanumeric" => "A senha precisa conter pelo menos um caractere especial.",
                "PasswordRequiresDigit" => "A senha precisa conter pelo menos um número.",
                "PasswordRequiresUpper" => "A senha precisa conter pelo menos uma letra maiúscula.",
                _ => "Erro desconhecido ao criar usuário."
            };
        }
    }
}
