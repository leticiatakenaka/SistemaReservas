using SistemaReservas.Domain.Entities;

namespace SistemaReservas.Domain.Interfaces
{
    public interface IPropertyRepository
    {
        Task<Property> GetByIdAsync(Guid id);
    }
}
