using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoChat.Modelos
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string? Imagen { get; set; }        
    }
}