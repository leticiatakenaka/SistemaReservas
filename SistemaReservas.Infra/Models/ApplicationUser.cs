using Microsoft.AspNetCore.Identity;

namespace SistemaReservas.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        protected ApplicationUser() { }

        public ApplicationUser(string primeiroNome, string ultimoNome, string email)
        {
            Id = Guid.NewGuid();
            PrimeiroNome = primeiroNome;
            Email = email;
            UserName = email;
            UltimoNome = ultimoNome;     
            DataCadastro = DateTime.UtcNow;
            Ativo = true;
        }

        public string PrimeiroNome { get; private set; }
        public string UltimoNome { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public void AtualizarDados(string primeiroNome, string ultimoNome, string email)
        {
            if (string.IsNullOrEmpty(primeiroNome)) throw new ArgumentNullException("Nome inválido");

            PrimeiroNome = primeiroNome;
            UltimoNome = ultimoNome;
            Email = email;
            UserName = email;
            NormalizedEmail = email.ToUpper();
            NormalizedUserName = email.ToUpper();
        }

        public void DesativoUsuario()
        {
            Ativo = false;
        }

        public void ativoUsuario()
        {
            Ativo = true;
        }
    }
}