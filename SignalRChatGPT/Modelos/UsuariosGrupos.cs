using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoChat.Modelos
{
    public class UsuariosGrupos
    {
        public int Id { get; set; }
        public Usuarios IdUsuario { get; set; }
        public Grupos IdGrupo { get; set; }
        public DateTime Fecha { get; set; }
    }
}