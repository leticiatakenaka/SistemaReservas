using SistemaReservas.Domain.Entities;
using SistemaReservas.Domain.Interfaces;
using SistemaReservas.Infrastructure.Context;

namespace SistemaReservas.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly AppDbContext _context;

        public PropertyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Property> GetByIdAsync(Guid id)
        {
            return await _context.Properties.FindAsync(id);
        }
    }
}
