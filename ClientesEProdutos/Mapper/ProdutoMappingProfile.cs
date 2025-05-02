using AutoMapper;
using ClientesEProdutos.Models;
using ClientesEProdutos.Models.Entities;

namespace ClientesEProdutos.Mapper
{
    public class ProdutoMappingProfile : Profile
    {
        public ProdutoMappingProfile()
        {
            CreateMap<Produtos, ProdutoEntityDto>().ReverseMap();
        }

    }
}