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
        public async Task<UsuariosDTO> GetUserById(int id)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            UsuariosDTO userDto = new UsuariosDTO()
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Usuario = user.Usuario1,
                Clave = user.Clave,
                Imagen = user.Imagen
            };
            return userDto;
        }
        public async Task<Usuario> GetById(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);                      
        }
        public async Task<bool> Create(Usuario user)
        {
            await _context.Usuarios.AddAsync(user);
            return await Save();
        }
        public async Task<bool> Update(Usuario user)
        {
             _context.Usuarios.Update(user);
            return await Save();
        }
        public async Task<bool> Delete(Usuario user)
        {
            _context.Usuarios.Remove(user);
            return await Save();
        }
        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
