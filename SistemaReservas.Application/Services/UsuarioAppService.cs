using SistemaReservas.Application.DTOs;
using SistemaReservas.Application.Interfaces;
using SistemaReservas.Domain.Common;
using SistemaReservas.Domain.Exceptions;
using SistemaReservas.Domain.Extensions;
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

        public async Task<PagedResult<UsuarioDto>> ObterUsuarios(int page, int pageSize, string termo, bool? ativo)
        {
            var resultado = await _usuarioRepository.ObterUsuariosPaginadoAsync(page, pageSize, termo, ativo);

            return resultado.Map(u => new UsuarioDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                Ativo = u.Ativo,
                DataCadastro = u.DataCadastro
            });
        }

        public async Task<UsuarioDto> ObterUsuarioPorId(Guid id)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorIdAsync(id);

            if (usuario == null)
            {
                throw new EntidadeNaoEncontradaException("Usuário não encontrado.");
            }

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Ativo = usuario.Ativo,
                DataCadastro = usuario.DataCadastro
            };
        }

        public async Task<UsuarioDto> EditarUsuario(Guid id, string email, string primeiroNome, string ultimoNome)
        {
            var usuario = await _usuarioRepository.EditarUsuario(id, email, primeiroNome, ultimoNome);

            if (usuario == null)
            {
                throw new EntidadeNaoEncontradaException("Usuário não encontrado.");
            }

            return new UsuarioDto
            {
                Email = usuario.Email,
                Ativo = usuario.Ativo,
                DataCadastro = usuario.DataCadastro,
                Id = usuario.Id,
                Nome = usuario.Nome
            };
        }

        public async Task<UsuarioDto> AlterarStatus(Guid id, bool ativo)
        {
            var usuario = await _usuarioRepository.AlterarStatus(id, ativo);

            if (usuario == null)
            {
                throw new EntidadeNaoEncontradaException("Usuário não encontrado.");
            }

            return new UsuarioDto
            {
                Email = usuario.Email,
                Ativo = usuario.Ativo,
                DataCadastro = usuario.DataCadastro,
                Id = usuario.Id,
                Nome = usuario.Nome
            };
        }
    }
}
