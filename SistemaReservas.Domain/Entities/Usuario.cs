using SistemaReservas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservas.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string NomeDeUsuario { get; set; }
        public string Email { get; set; }
        public int PerfilId { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}
