using System.ComponentModel.DataAnnotations;

namespace SistemaReservas.Application.DTOs
{
    public class RegistrarUsuarioRequest
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email não é válido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public required string Senha { get; set; }

        [Required(ErrorMessage = "O campo Primeiro Nome é obrigatório.")]
        public required string PrimeiroNome { get; set; }

        [Required(ErrorMessage = "O campo Último Nome é obrigatório.")]
        public required string UltimoNome { get; set; }

        [Required(ErrorMessage = "O campo Nome de Usuário é obrigatório.")]
        public required string Username { get; set; }
    }
}
