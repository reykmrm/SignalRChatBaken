using System;
using System.Collections.Generic;

namespace SignalRChatGPT.Modelos
{
    public partial class UsuariosGrupo
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdGrupo { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Grupo IdGrupoNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
