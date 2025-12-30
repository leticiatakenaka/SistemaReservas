using SistemaReservas.Domain.Common;
using SistemaReservas.Domain.Entities;

namespace SistemaReservas.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<PagedResult<Usuario>> ObterUsuariosPaginadoAsync(int page, int pageSize, string termo, bool? ativo);
    }
}
