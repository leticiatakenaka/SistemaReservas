using SistemaReservas.Domain.Enums;
using SistemaReservas.Domain.Exceptions;

namespace SistemaReservas.Domain.Entities
{
    public class Booking
    {
        public Booking() { }

        public Guid Id { get; set; }

        public Guid PropertyId { get; set; }
        public virtual Property Property { get; set; }

        public Guid GuestId { get; set; }
        public virtual ApplicationUser Guest { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public decimal TotalPrice { get; set; }

        public BookingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public Booking(Guid propertyId, Guid guestId, DateTime checkIn, DateTime checkOut, decimal pricePerNight)
        {
            if (checkIn >= checkOut)
                throw new DomainException("A data de check-out deve ser posterior ao check-in.");

            if (checkIn.Date < DateTime.UtcNow.Date)
                throw new DomainException("Não é possível fazer reservas no passado.");

            Id = Guid.NewGuid();
            PropertyId = propertyId;
            GuestId = guestId;
            CheckInDate = checkIn;
            CheckOutDate = checkOut;
            Status = BookingStatus.Confirmed;
            CreatedAt = DateTime.UtcNow;

            CalcularPrecoTotal(pricePerNight);
        }

        private void CalcularPrecoTotal(decimal pricePerNight)
        {
            var dias = (CheckOutDate - CheckInDate).Days;
            if (dias < 1) dias = 1;

            TotalPrice = dias * pricePerNight;
        }
    }
}
