namespace SistemaReservas.Application.DTOs
{
    public class EditarUsuarioDto
    {
        public required string Email { get; set; }
        public required string PrimeiroNome { get; set; }
        public required string UltimoNome { get; set; }
    }
}
