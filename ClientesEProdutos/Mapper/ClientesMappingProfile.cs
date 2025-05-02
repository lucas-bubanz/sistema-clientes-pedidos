using AutoMapper;
using ClientesEProdutos.Models;
using ClientesEProdutos.Models.Entities;

namespace ClientesEProdutos.Mapper
{
    public class ClientesMappingProfile : Profile
    {
        public ClientesMappingProfile()
        {
            CreateMap<Clientes, ClienteEntityDto>().ReverseMap();
        }
    }
}