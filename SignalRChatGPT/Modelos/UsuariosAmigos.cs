using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoChat.Modelos
{
    public class UsuariosAmigos
    {
        public int Id { get; set; }
        public Usuarios IdUsuario1 { get; set; }
        public Usuarios IdUsuario2 { get; set; }
        public DateTime Fecha { get; set; }        
    }
}