using SistemaReservas.Application.DTOs;
using SistemaReservas.Application.Interfaces;
using SistemaReservas.Domain.Common;
using SistemaReservas.Domain.Entities;
using SistemaReservas.Domain.Interfaces;

namespace SistemaReservas.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioAppService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<PagedResult<Usuario>> ObterUsuariosAsync(int page, int pageSize, string termo, bool? ativo)
        {
            return await _usuarioRepository.ObterUsuariosPaginadoAsync(page, pageSize, termo, ativo);
        }
    }
}
