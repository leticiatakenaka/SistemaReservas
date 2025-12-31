namespace SistemaReservas.Domain.Entities
{
    public class PropertyImage
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public required string Url { get; set; }
        public bool IsCover { get; set; }
    }
}
