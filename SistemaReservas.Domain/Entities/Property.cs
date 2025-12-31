using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaReservas.Domain.Entities
{
    public class Property
    {
        public Guid Id { get; set; }
        public Guid HostId { get; set; }
        public virtual ApplicationUser Host { get; set; }

        public string Title { get; set; }        
        public string Description { get; set; }  

        public decimal PricePerNight { get; set; }
        public int MaxGuests { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
