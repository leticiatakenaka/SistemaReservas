using SistemaReservas.Application.DTOs;
using SistemaReservas.Domain.Common;

namespace SistemaReservas.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        Task<PagedResult<UsuarioDto>> ObterUsuarios(int page, int pageSize, string termo, bool? ativo);
        Task<UsuarioDto> ObterUsuarioPorId(Guid id);
        Task<UsuarioDto> EditarUsuario(Guid id, string email, string primeiroNome, string ultimoNome);
        Task<UsuarioDto> AlterarStatus(Guid id, bool ativo);
    }
}
