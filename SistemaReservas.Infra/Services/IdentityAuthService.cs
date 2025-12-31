using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SistemaReservas.Application.DTOs;
using SistemaReservas.Application.Interfaces;
using SistemaReservas.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaReservas.Infrastructure.Services
{
    public class IdentityAuthService : IAuthGateway
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public IdentityAuthService(UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                return await GerarJwtTokenAsync(user);
            }

            return null;
        }

        public async Task<OperationResultDto<string>> Register(RegisterUserRequest request)
        {
            var user = new ApplicationUser
            (
                request.FirstName,
                request.LastName,
                request.Email,
                request.Type
            );

            try
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(MapearIdentityErrorParaMensagem).ToList();
                    return OperationResultDto<string>.Fail(errors.ToArray());
                }

                {
                    var token = await GerarJwtTokenAsync(user);
                    return OperationResultDto<string>.Ok(token);
                }
            }
            catch (Exception)
            {
                var erro = "Erro inesperado.";
                return OperationResultDto<string>.Fail(erro);
            }
        }

        private async Task<string?> GerarJwtTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email.ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

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
