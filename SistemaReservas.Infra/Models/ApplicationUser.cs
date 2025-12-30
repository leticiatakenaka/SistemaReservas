using Microsoft.AspNetCore.Identity;

namespace SistemaReservas.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        protected ApplicationUser() { }

        public ApplicationUser(string userName, string primeiroNome, string ultimoNome, string email)
        {
            Id = Guid.NewGuid();
            UserName = userName;    
            PrimeiroNome = primeiroNome; 
            UltimoNome = ultimoNome;     
            DataCadastro = DateTime.UtcNow;
            Ativo = true;
        }

        public string PrimeiroNome { get; private set; }
        public string UltimoNome { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCadastro { get; private set; }
    }
}