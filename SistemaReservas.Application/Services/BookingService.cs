using SistemaReservas.Application.DTOs;
using SistemaReservas.Application.Interfaces;
using SistemaReservas.Domain.Common;
using SistemaReservas.Domain.Entities;
using SistemaReservas.Domain.Exceptions;
using SistemaReservas.Domain.Interfaces;

namespace SistemaReservas.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUserRepository _userRepository;

        public BookingService(
            IBookingRepository bookingRepository,
            IPropertyRepository propertyRepository,
            IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _propertyRepository = propertyRepository;
            _userRepository = userRepository;
        }

        public async Task<OperationResultDto<Guid>> CreateBookingAsync(CreateBookingDto dto)
        {
            var property = await _propertyRepository.GetByIdAsync(dto.PropertyId);
            if (property == null)
                return OperationResultDto<Guid>.Fail("Propriedade não encontrada.");

            var guestExists = await _userRepository.ExistsAsync(dto.GuestId);
            if (!guestExists)
                return OperationResultDto<Guid>.Fail("Usuário (Hóspede) não encontrado.");

            var isAvailable = await _bookingRepository.IsPropertyAvailableAsync(
                dto.PropertyId, dto.CheckInDate, dto.CheckOutDate);

            if (!isAvailable)
                return OperationResultDto<Guid>.Fail("Esta propriedade não está disponível nas datas selecionadas.");

            try
            {
                var booking = new Booking(
                    dto.PropertyId,
                    dto.GuestId,
                    dto.CheckInDate,
                    dto.CheckOutDate,
                    property.PricePerNight
                );

                await _bookingRepository.AddAsync(booking);
                await _bookingRepository.CommitAsync();

                return OperationResultDto<Guid>.Ok(booking.Id);
            }
            catch (DomainException ex)
            {
                return OperationResultDto<Guid>.Fail(ex.Message);
            }
        }

        public async Task<PagedResult<BookingDto>> GetPagedBookingAsync(int page, int pageSize, string searchTerm, Guid hostId)
        {
            var resultado = await _bookingRepository.GetPagedBookingAsync(page, pageSize, searchTerm, hostId);

            var bookingDtos = resultado.Items
                .Select(u => new BookingDto(
                    u.Id,
                    u.CheckInDate,
                    u.CheckOutDate,
                    u.TotalPrice,
                    u.Property?.Title ?? "Imóvel desconhecido",
                    $"{u.Guest?.FirstName} {u.Guest?.LastName}" ?? "Usuário desconhecido",
                    u.Status
                    )
                )
                .ToList();

            return new PagedResult<BookingDto>(bookingDtos)
            {
                TotalCount = resultado.TotalCount,
                Page = resultado.Page,
                PageSize = resultado.PageSize
            };
        }
    }
}