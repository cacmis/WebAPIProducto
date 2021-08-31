using AutoMapper;
using MiPrimerWebApi.Dtos;
using MiPrimerWebApi.Models;

namespace MiPrimerWebApi.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ProductoCreateDto,Producto>();
            CreateMap<ProductoUpdateDto,Producto>();
            CreateMap<UsuarioRegisterDto,Usuario>();
            CreateMap<UsuarioLoginDto,Usuario>();
            CreateMap<Usuario,UsuarioListDto>();
        }
    }
}