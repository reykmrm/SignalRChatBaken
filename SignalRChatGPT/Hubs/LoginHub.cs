using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRChatGPT.Modelos;
using SignalRChatGPT.Modelos.DTOs;
using SignalRChatGPT.Services;

namespace SignalRChatGPT.Hubs
{
    public class LoginHub : Hub
    {
        private readonly UserService _userService;
        public LoginHub(UserService userService)
        {
            _userService = userService;
        }
        public async Task Login(UsuariosDTO user)
        {
            Usuario userByLogin = await _userService.GetUserByLogin(user.Usuario, user.Clave);
            if (userByLogin != null) 
            {

            }
        }
    }
}
