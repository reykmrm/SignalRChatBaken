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

        //public async Task GetAllUsers()
        //{
        //    List<Usuario> users = new List<Usuario>();
        //    using var connection = new SqlConnection(System.Configuration.GetConnectionString("DefaultConnection"));
        //        var query = "SELECT * FROM Usuarios";
        //    users = connection.Query<Usuario>(query).ToList();
        //    await Clients.All.SendAsync("GetAllUsersClient", users);
        //}

        public async Task GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            await Clients.All.SendAsync("GetAllUsersClient", result);
        }





        //public async Task GetUserById(int id)
        //{
        //    Usuario usuario = new Usuario();
        //    usuario = await _contex.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        //    string message = "";
        //    if (usuario is not null)
        //    {
        //        message = "Encontrado";
        //        await Clients.All.SendAsync("GetUserById", " " + usuario, message);
        //    }
        //    message = "No se encontro el usuario";
        //    await Clients.All.SendAsync("GetUserById", " " + usuario, message);
        //}
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
        

        //public async Task EditUser()
        //{

        //}

        

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


        //public async Task<Usuarios>GetById(int id)
        //{
        //    //using (IDbConnection conexion = new SqlConnection(ObtenerCadenaConexion()))
        //    using (SqlConnection conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        conexion.Open();
        //        var parametros = new DynamicParameters();

        //        parametros.Add("idUser", id);

        //        var result = await conexion.QueryAsync<Usuarios > ("dbo.GetUserById", param: parametros, commandType: CommandType.StoredProcedure);
        //        return result;
        //    }

        //}
    }
}
