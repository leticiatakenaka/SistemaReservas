using SistemaReservas.Application.DTOs;

namespace SistemaReservas.Application.Interfaces
{
    public interface IAuthGateway 
    {
        Task<string> Autenticar(string email, string password);
        Task<OperationResultDto<string>> Registrar(RegistrarUsuarioRequest request);
    }
}
