using System;
using System.Collections.Generic;

namespace SignalRChatGPT.Modelos
{
    public partial class Usuario
    {
        public Usuario()
        {
            UsuariosAmigoIdUsuario1Navigations = new HashSet<UsuariosAmigo>();
            UsuariosAmigoIdUsuario2Navigations = new HashSet<UsuariosAmigo>();
            UsuariosGrupos = new HashSet<UsuariosGrupo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Usuario1 { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public string Imagen { get; set; } = null!;

        public virtual ICollection<UsuariosAmigo> UsuariosAmigoIdUsuario1Navigations { get; set; }
        public virtual ICollection<UsuariosAmigo> UsuariosAmigoIdUsuario2Navigations { get; set; }
        public virtual ICollection<UsuariosGrupo> UsuariosGrupos { get; set; }
    }
}
