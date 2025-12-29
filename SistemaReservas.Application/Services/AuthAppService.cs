using SistemaReservas.Application.DTOs;
using SistemaReservas.Application.Interfaces;

namespace SistemaReservas.Application.Services
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IAuthGateway _authGateway; 

        public AuthAppService(IAuthGateway authGateway)
        {
            _authGateway = authGateway;
        }

        public async Task<string?> Login(LoginDto dto)
        {
            var token = await _authGateway.Autenticar(dto.Email, dto.Senha);

            if (token == null)
                return null;

            return token;
        }

        public async Task<OperationResultDto<string>> Registrar(RegistrarUsuarioRequest request)
        {
            return await _authGateway.Registrar(request);
        }
    }
}
