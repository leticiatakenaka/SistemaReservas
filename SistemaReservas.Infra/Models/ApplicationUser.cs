using Microsoft.AspNetCore.Identity;

namespace SistemaReservas.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
    }
}
