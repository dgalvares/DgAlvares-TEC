using System;
using System.Collections.Generic;

namespace DgAlvaresTEC.Entities.Models
{
    public partial class AutTbgen
    {
        public AutTbgen()
        {
            AutPessoa = new HashSet<AutPessoa>();
            AutUsuario = new HashSet<AutUsuario>();
        }

        public int Id { get; set; }
        public int Tipo { get; set; }
        public int Ordem { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<AutPessoa> AutPessoa { get; set; }
        public virtual ICollection<AutUsuario> AutUsuario { get; set; }
    }
}
