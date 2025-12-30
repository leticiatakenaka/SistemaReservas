using SistemaReservas.Domain.Common;
using SistemaReservas.Domain.Entities;

namespace SistemaReservas.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<PagedResult<Usuario>> ObterUsuariosPaginadoAsync(int page, int pageSize, string termo, bool? ativo);
        Task<Usuario?> ObterUsuarioPorIdAsync(Guid id);
        Task<Usuario?> EditarUsuario(Guid id, string email, string primeiroNome, string ultimoNome);
        Task<Usuario?> AlterarStatus(Guid id, bool ativo);
    }
}
