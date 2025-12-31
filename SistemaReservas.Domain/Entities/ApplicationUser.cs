using Microsoft.AspNetCore.Identity;
using SistemaReservas.Domain.Enums;

namespace SistemaReservas.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        protected ApplicationUser() { }

        public ApplicationUser(string firstName, string lastName, string email, UserType type)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = email;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            Type = type;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public UserType Type { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}