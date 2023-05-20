using Microsoft.EntityFrameworkCore;
using SignalRChatGPT.Modelos;
using System.Collections.Immutable;

namespace SignalRChatGPT.Services
{
    public class GroupService
    {
        private readonly ChatBacapContext _context;

        public GroupService(ChatBacapContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Grupo>> GetAllGroups()
        {
            return await _context.Grupos.ToListAsync();
        }

        public async Task<Grupo> GetGroupById(int id)
        {
            return await _context.Grupos.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<bool> Create(Grupo grupo)
        {
            await _context.Grupos.AddAsync(grupo);
            return await Save();
        }
        public async Task<bool> Update(Grupo grupo)
        {
            _context.Grupos.Update(grupo);
            return await Save();
        }
        public async Task<bool> Delete(Grupo grupo)
        {
            _context.Grupos.Remove(grupo);
            return await Save();
        }
        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
