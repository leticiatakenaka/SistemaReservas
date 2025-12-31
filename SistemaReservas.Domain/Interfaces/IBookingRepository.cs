using SistemaReservas.Domain.Common;
using SistemaReservas.Domain.Entities;

namespace SistemaReservas.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task AddAsync(Booking booking);
        Task CommitAsync();
        Task<bool> IsPropertyAvailableAsync(Guid propertyId, DateTime checkIn, DateTime checkOut);
        Task<PagedResult<Booking>> GetPagedBookingAsync(int page, int pageSize, string searchTerm, Guid hostId);
    }
}
