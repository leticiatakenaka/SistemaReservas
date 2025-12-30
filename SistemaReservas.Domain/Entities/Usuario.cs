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
        protected Usuario() { }

        public Usuario(Guid? id, string nome, string email, bool ativo = true)
        {
            Id = id ?? Guid.NewGuid();
            Nome = nome;
            Email = email;
            DataCadastro = DateTime.UtcNow;
            Ativo = ativo;
        }

        public Usuario(Guid id, string nome, string email, bool ativo, DateTime dataCadastro)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Ativo = ativo;
            DataCadastro = dataCadastro;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCadastro { get; private set; }
    }
}

