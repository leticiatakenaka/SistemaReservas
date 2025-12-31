using SistemaReservas.Domain.Common;
using SistemaReservas.Domain.Entities;

namespace SistemaReservas.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsAsync(Guid id);
        Task<ApplicationUser?> GetByIdAsync(Guid id);
        Task<ApplicationUser?> GetByEmailAsync(string email);
    }
}
