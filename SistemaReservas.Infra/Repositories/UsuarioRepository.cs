using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SistemaReservas.Domain.Common;
using SistemaReservas.Domain.Entities;
using SistemaReservas.Domain.Interfaces;
using SistemaReservas.Infrastructure.Context;
using SistemaReservas.Infrastructure.Models;

namespace SistemaReservas.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsuarioRepository(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<PagedResult<Usuario>> ObterUsuariosPaginadoAsync(int page, int pageSize, string termo, bool? ativo)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(termo))
            {
                query = query.Where(u => u.PrimeiroNome.Contains(termo) ||
                                         u.UltimoNome.Contains(termo) ||
                                         u.UserName.Contains(termo) ||
                                         u.Email.Contains(termo));
            }

            if (ativo.HasValue)
            {
                query = query.Where(u => u.Ativo == ativo.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new Usuario(
                    u.Id,
                    $"{u.PrimeiroNome} {u.UltimoNome}",
                    u.Email,
                    u.Ativo,
                    u.DataCadastro
                ))
                .ToListAsync();

            return new PagedResult<Usuario>(items)
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<Usuario?> ObterUsuarioPorIdAsync(Guid id)
        {
            var identityUser = await _context.Users.FindAsync(id);

            if (identityUser == null) return null;

            return new Usuario(
                identityUser.Id,
                $"{identityUser.PrimeiroNome} {identityUser.UltimoNome}",
                identityUser.Email,
                identityUser.Ativo,
                identityUser.DataCadastro
            );
        }

        public async Task<Usuario?> EditarUsuario(Guid id, string email, string primeiroNome, string ultimoNome)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return null;

            user.AtualizarDados(primeiroNome, ultimoNome, email);

            await _userManager.SetEmailAsync(user, email);
            await _userManager.SetUserNameAsync(user, email);

            await _userManager.UpdateAsync(user);

            return new Usuario(user.Id, $"{user.PrimeiroNome} {user.UltimoNome}", user.Email);
        }

        public async Task<Usuario?> AlterarStatus(Guid id, bool ativo)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return null;

            if (ativo)
                user.ativoUsuario();
            else
                user.DesativoUsuario();

            await _userManager.UpdateAsync(user);

            return new Usuario(user.Id, $"{user.PrimeiroNome} {user.UltimoNome}", user.Email, user.Ativo);
        }
    }
}
