using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservas.Application.DTOs
{
    public class RegistrarUsuarioRequest
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo Primeiro Nome é obrigatório.")]
        public string PrimeiroNome { get; set; }

        [Required(ErrorMessage = "O campo Último Nome é obrigatório.")]
        public string UltimoNome { get; set; }

        [Required(ErrorMessage = "O campo Nome de Usuário é obrigatório.")]
        public string NomeDeUsuario { get; set; }
    }
}
