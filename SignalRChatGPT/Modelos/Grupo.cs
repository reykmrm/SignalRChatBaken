using System;
using System.Collections.Generic;

namespace SignalRChatGPT.Modelos
{
    public partial class Grupo
    {
        public Grupo()
        {
            UsuariosGrupos = new HashSet<UsuariosGrupo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<UsuariosGrupo> UsuariosGrupos { get; set; }
    }
}
