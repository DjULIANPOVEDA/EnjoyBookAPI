using AutoMapper;
using EnjoyBookAPI.Models;
using EnjoyBookAPI.Models.Request;
using EnjoyBookAPI.Models.Response;

namespace EnjoyBookAPI.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<LibroRequest, Libro>();
            CreateMap<Libro, LibroWithUserResponse>();
            CreateMap<Libro, LibroResponse>();
            CreateMap<Usuario, UsuarioResponse>();
            CreateMap<RegisterRequest, Usuario>();
            CreateMap<RentaRequest, Renta>();

            CreateMap<Libro,LibroRequest>();
        }
    }
}
