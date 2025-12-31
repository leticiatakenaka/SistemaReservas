using SistemaReservas.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaReservas.Application.DTOs
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email não é válido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "O campo Password é obrigatório.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "O campo FirstName é obrigatório.")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "O campo LastName é obrigatório.")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "O campo Type é obrigatório.")]
        public UserType Type { get; set; }
    }
}
