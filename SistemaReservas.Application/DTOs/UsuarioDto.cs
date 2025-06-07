using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservas.Application.DTOs
{
    public class UsuarioDto
    {
        public string NomeDeUsuario { get; set; }
        public string Email { get; set; }
        public int PerfilId { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string Password { get; set; }
    }
}
