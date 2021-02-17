using System;
using System.Collections.Generic;

namespace DgAlvaresTEC.Entities.Models
{
    public partial class AutPessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Status { get; set; }

        public virtual AutTbgen StatusNavigation { get; set; }
    }
}
