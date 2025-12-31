using SistemaReservas.Domain.Enums;

namespace SistemaReservas.Application.DTOs
{
    public class CreateBookingDto
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public Guid GuestId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
