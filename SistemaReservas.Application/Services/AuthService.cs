using SistemaReservas.Application.DTOs;
using SistemaReservas.Application.Interfaces;

namespace SistemaReservas.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthGateway _authGateway; 

        public AuthService(IAuthGateway authGateway)
        {
            _authGateway = authGateway;
        }

        public async Task<string?> Login(LoginDto dto)
        {
            var token = await _authGateway.Login(dto.Email, dto.Senha);

            if (token == null)
                return null;

            return token;
        }

        public async Task<OperationResultDto<string>> Register(RegisterUserRequest request)
        {
            return await _authGateway.Register(request);
        }
    }
}
