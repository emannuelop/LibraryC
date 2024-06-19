using AutoMapper;
using LibraryC.DTOs;
using LibraryC.Models;

namespace LibraryC.Mappings
{
    public class EntitiesToDTOMappingProfile : Profile
    {
        public EntitiesToDTOMappingProfile()
        {
            CreateMap<Autor, AutorResponseDTO>().ReverseMap();

            CreateMap<Autor, AutorDTO>().ReverseMap();

            CreateMap<Usuario, UsuarioResponseDTO>().ReverseMap();

            CreateMap<Usuario, UsuarioDTO>().ReverseMap();

            CreateMap<Cliente, ClienteResponseDTO>().ReverseMap();

            CreateMap<Cliente, ClienteDTO>().ReverseMap();

            CreateMap<Livro, LivroResponseDTO>().ReverseMap();

            CreateMap<Livro, LivroDTO>().ReverseMap();

            CreateMap<Multa, MultaResponseDTO>().ReverseMap();

            CreateMap<Multa, MultaDTO>().ReverseMap();

            CreateMap<Emprestimo, EmprestimoResponseDTO>().ReverseMap();

            CreateMap<Emprestimo, EmprestimoDTO>().ReverseMap();
        }
    }
}
