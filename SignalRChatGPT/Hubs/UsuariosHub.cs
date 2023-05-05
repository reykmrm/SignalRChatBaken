using Dapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SignalRChatGPT.Modelos;
using SignalRChatGPT.Modelos.DTOs;
using SignalRChatGPT.Services;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace SignalRChatGPT.Hubs
{
    public class UsuariosHub : Hub
    {
        public readonly IConfiguration _configuration;
        private readonly UserService _userService;
        public UsuariosHub(IConfiguration configuration, UserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }
       
        public async Task GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            await Clients.All.SendAsync("GetAllUsersClient", result);
        }
        public async Task GetUserById(int id)
        {
            UsuariosDTO userDTO = await _userService.GetUserById(id);
            await Clients.All.SendAsync("UserById", userDTO);
        }
        public async Task CreateUser(UsuariosDTO user)
        {
            Usuario usuario = new Usuario()
            {
                Nombre = user.Nombre,
                Clave = user.Clave,
                Usuario1 = user.Usuario,
                Imagen = user.Imagen
            };
            bool saveUser = await RegisterUser(usuario);
            if (saveUser == true)
            {
                string message = "Usuario registrado";
                await Clients.All.SendAsync("UserRegistrado", " " + message);
            }
            else
            {
                string message = "Problemas al registrar el Usuario ";
                await Clients.All.SendAsync("UserRegistrado", " " + message);
            }

        }
        public async Task EditUser(int id, UsuariosDTO userEdit)
        {
            var user=await _userService.GetById(id);
            user.Nombre = userEdit.Nombre;
            user.Usuario1=userEdit.Usuario;
            user.Clave=userEdit.Clave;
            user.Imagen=userEdit.Imagen;            
            //tranasformo a usuario           
            try
            {               
               bool save= await _userService.Update(user);
                if (save == true)
                {
                    string message = "Usuario Editado Correctamente";
                    await Clients.All.SendAsync("UserEditado", message);
                }
                else
                {
                    string message = "El Usuario no pudo ser editado";
                    await Clients.All.SendAsync("UserEditado", message);
                }                
            }
            catch(Exception e)
            {
                string message = e.Message;
                await Clients.All.SendAsync("UserEditado", message);
            }

        }
        public async Task EliminarUser(int id)
        {
            var userDelete = await _userService.GetById(id);
            try
            {
                bool delete =await _userService.Delete(userDelete);
                if (delete == true)
                {
                    string message = "Usuario Eliminado Correctamente";
                    await Clients.All.SendAsync("UserEliminado", message);
                }
                else
                {
                    string message = "El Usuario no pudo ser Eliminado";
                    await Clients.All.SendAsync("UserEliminado", message);
                }
            }
            catch(Exception e)
            {
                string message =e.Message;
                await Clients.All.SendAsync("UserEliminado", message);
            }
        }
                      
        public async Task<bool> RegisterUser(Usuario user)
        {
            //using (IDbConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
            using (SqlConnection conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Open();
                var parametros = new DynamicParameters();

                parametros.Add("Nombre", user.Nombre);
                parametros.Add("Usuario", user.Usuario1);
                parametros.Add("Clave", user.Clave);
                parametros.Add("Imagen", user.Imagen);

                var result = await conexion.ExecuteAsync("dbo.CreateUser", param: parametros, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }       
    }
}
