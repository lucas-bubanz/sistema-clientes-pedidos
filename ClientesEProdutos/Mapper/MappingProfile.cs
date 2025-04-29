using AutoMapper;
using ClientesEProdutos.Models;
using ClientesEProdutos.Models.DTOs;
using ClientesEProdutos.Models.Entities;

namespace ClientesEProdutos.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Produtos, ProdutoEntityDto>().ReverseMap();

            CreateMap<Pedido, PedidoEntityDto>().ReverseMap();
            CreateMap<PedidoProduto, ProdutoEntityDto>().ReverseMap();

            CreateMap<PrePedido, PrePedidoDto>().ReverseMap();
            CreateMap<PrePedidoProduto, ProdutoEntityDto>().ReverseMap();

            CreateMap<Clientes, ClienteEntityDto>().ReverseMap();
        }
    }
}