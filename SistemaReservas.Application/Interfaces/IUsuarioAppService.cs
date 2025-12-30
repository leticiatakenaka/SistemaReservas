using SistemaReservas.Domain.Common;
using SistemaReservas.Domain.Entities;

namespace SistemaReservas.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        Task<PagedResult<Usuario>> ObterUsuariosAsync(int page, int pageSize, string termo, bool? ativo);
    }
}
