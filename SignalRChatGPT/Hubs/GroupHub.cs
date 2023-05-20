using Microsoft.AspNetCore.SignalR;
using SignalRChatGPT.Modelos;
using SignalRChatGPT.Modelos.DTOs;
using SignalRChatGPT.Services;
using System.Text.RegularExpressions;

namespace SignalRChatGPT.Hubs
{
    public class GroupHub:Hub
    {
        private readonly GroupService _groupService;
        public GroupHub(GroupService groupService )
        {
            _groupService = groupService;
        }
        public async Task GetAllGroups()
        {
            var result = await _groupService.GetAllGroups();
            await Clients.All.SendAsync("GetAllGroupsClient", result);
        }

        public async Task GetGroupById(int id)
        {
            var GroupDTO = await _groupService.GetGroupById(id);
            await Clients.All.SendAsync("GroupById", GroupDTO);
        }

        public async Task CreateGroup(GroupDTO groupDTO)
        {
            Grupo grupo = new Grupo()
            {
                Nombre = groupDTO.Nombre,
                Descripcion = groupDTO.Descripcion,
            };
            bool save =await _groupService.Create(grupo);
            if(save==false)
            {
                await Clients.All.SendAsync("CreateGroup", "Error al crear el grupo ");
            }
            await Clients.All.SendAsync("CreateGroup", "Grupo creado correctamente");
        }

        public async Task EditGroup(int id,GroupDTO groupDTO)
        {
            Grupo editGroup=await _groupService.GetGroupById(id);
            if (editGroup != null)
            {
                editGroup.Nombre= groupDTO.Nombre;
                editGroup.Descripcion= groupDTO.Descripcion;
                
                bool edit =await _groupService.Update(editGroup);
                if (edit == false)
                {
                    await Clients.All.SendAsync("EditGroup", "Grupo no pudo ser editado correctamente");
                }
                await Clients.All.SendAsync("EditGroup", "Grupo Editado correctamente");
            }
        }

        public async Task DeleteGroup(int id)
        {
            Grupo deleteGroup = await _groupService.GetGroupById(id);
            if(deleteGroup != null)
            {
                bool delete=await _groupService.Delete(deleteGroup);
                if (delete == false)
                {
                    await Clients.All.SendAsync("DeleteGroup", "El grupo no pudo ser eliminado");
                }
                await Clients.All.SendAsync("DeleteGroup", "El grupo fue eliminado");
            }
            await Clients.All.SendAsync("DeleteGroup", "El grupo no pudo encontrar");
        }

    }
}
