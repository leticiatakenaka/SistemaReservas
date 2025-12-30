using Microsoft.EntityFrameworkCore;
using SistemaReservas.Domain.Common;
using SistemaReservas.Domain.Entities;
using SistemaReservas.Domain.Interfaces;
using SistemaReservas.Infrastructure.Context;

namespace SistemaReservas.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
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
    }
}
