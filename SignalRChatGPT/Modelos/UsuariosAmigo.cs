using System;
using System.Collections.Generic;

namespace SignalRChatGPT.Modelos
{
    public partial class UsuariosAmigo
    {
        public int Id { get; set; }
        public int IdUsuario1 { get; set; }
        public int IdUsuario2 { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Usuario IdUsuario1Navigation { get; set; } = null!;
        public virtual Usuario IdUsuario2Navigation { get; set; } = null!;
    }
}
