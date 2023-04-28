using Microsoft.EntityFrameworkCore;
using SignalRChatGPT.Modelos;
using SignalRChatGPT.Modelos.DTOs;

namespace SignalRChatGPT.Services
{
    public class UserService
    {
        private readonly ChatBacapContext _context;

        public UserService(ChatBacapContext context)
        {
            _context = context;
        }

        public async Task<List<UsuariosDTO>> GetAllUsers()
        {
            var users = await _context.Usuarios
                .Select(u => new UsuariosDTO
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Usuario = u.Usuario1,
                    Clave = u.Clave,
                    Imagen = u.Imagen
                })
                .ToListAsync();

            return users;
        }
    }
}
