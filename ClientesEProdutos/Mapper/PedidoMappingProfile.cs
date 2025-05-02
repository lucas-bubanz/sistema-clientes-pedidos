using AutoMapper;
using ClientesEProdutos.Models;
using ClientesEProdutos.Models.DTOs;
using ClientesEProdutos.Models.Entities;

namespace ClientesEProdutos.Mapper
{
    public class PedidoMappingProfile : Profile
    {
        public PedidoMappingProfile()
        {
            CreateMap<Pedido, PedidoEntityDto>().ReverseMap();
            CreateMap<PedidoProduto, ProdutoEntityDto>().ReverseMap();
        }
    }
}