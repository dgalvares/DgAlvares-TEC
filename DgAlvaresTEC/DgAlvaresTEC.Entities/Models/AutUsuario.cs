using System;
using System.Collections.Generic;

namespace DgAlvaresTEC.Entities.Models
{
    public partial class AutUsuario
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public int Status { get; set; }

        public virtual AutTbgen StatusNavigation { get; set; }
    }
}
