using AutoMapper;
using TareasMVC_NetCore.Entidades;
using TareasMVC_NetCore.Models;

namespace TareasMVC_NetCore.Servicios
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Tarea, TareaDTO>();
        }
    }
}
