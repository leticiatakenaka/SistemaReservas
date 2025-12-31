using SistemaReservas.Application.DTOs;

namespace SistemaReservas.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string?> Login(LoginDto dto);
        Task<OperationResultDto<string>> Register(RegisterUserRequest request);
    }
}
