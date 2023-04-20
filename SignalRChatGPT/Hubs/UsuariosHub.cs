using Dapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using ProyectoChat.Modelos;
using SignalRChatGPT.Modelos.DTOs;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace SignalRChatGPT.Hubs
{
    public class UsuariosHub:Hub
    {
        public readonly IConfiguration _configuration;
        public UsuariosHub(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task CreateUser(UsuariosDTO user)
        {
            Usuarios usuario = new Usuarios()
            {
                Nombre=user.Nombre,
                Clave=user.Clave,
                Usuario=user.Usuario,
                Imagen=user.Imagen
            };
            bool saveUser= await RegisterUser(usuario);
            if (saveUser==true)
            {
                string message = "Usuario registrado";
                await Clients.All.SendAsync("UserRegistrado"," "+ message);
            }
            else
            {
                string message = "Problemas al registrar el Usuario ";
                await Clients.All.SendAsync("UserRegistrado", " " + message);
            }
             
        }

        

        public async Task<bool> RegisterUser(Usuarios user)
        {
            //using (IDbConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            using (SqlConnection conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Open();
                var parametros = new DynamicParameters();

                parametros.Add("Nombre", user.Nombre);
                parametros.Add("Usuario", user.Usuario);
                parametros.Add("Clave", user.Clave);
                parametros.Add("Imagen", user.Imagen);

                var result = await conexion.ExecuteAsync("dbo.CreateUser", param: parametros, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }
    }
}
