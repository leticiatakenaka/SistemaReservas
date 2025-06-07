using SistemaReservas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservas.Domain.Entities
{
    public class Recurso
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public TipoRecursoEnum Tipo { get; set; } // Sala, Equipamento, Evento
        public required string Localizacao { get; set; }
    }
}
