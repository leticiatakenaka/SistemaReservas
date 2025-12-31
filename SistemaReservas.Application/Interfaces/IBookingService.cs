using SistemaReservas.Application.DTOs;
using SistemaReservas.Domain.Common;

namespace SistemaReservas.Application.Interfaces
{
    public interface IBookingService
    {
        Task<OperationResultDto<Guid>> CreateBookingAsync(CreateBookingDto dto);
        Task<PagedResult<BookingDto>> GetPagedBookingAsync(int page, int pageSize, string searchTerm, Guid hostId);
    }
}
