using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using SignalRChatGPT.Modelos;
using SignalRChatGPT.Modelos.DTOs;
using SignalRChatGPT.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;

namespace SignalRChatGPT.Hubs
{
    public class LoginHub : Hub
    {
        private IConfiguration _config;
        private readonly UserService _userService;
        public LoginHub(UserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }
        public async Task Login(UsuariosDTO user)
        {
            Usuario userByLogin = await _userService.GetUserByLogin(user.Usuario, user.Clave);
            if (userByLogin != null) 
            {
                string token = generateToken(user);

                await Clients.All.SendAsync("token", token);
                 return;
            }


            await Clients.All.SendAsync("token", "false");
        }

        private string generateToken(UsuariosDTO admin)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, admin.Nombre),
            new Claim(ClaimTypes.Email, admin.Usuario),
            //new Claim("AdminType", admin.AdminType),
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }
    }
}
