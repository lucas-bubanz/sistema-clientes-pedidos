using AutoMapper;
using ClientesEProdutos.Models;
using ClientesEProdutos.Models.DTOs;
using ClientesEProdutos.Models.Entities;

namespace ClientesEProdutos.Mapper
{
    public class PrePedidoMappingProfile : Profile
    {
        public PrePedidoMappingProfile()
        {
            CreateMap<PrePedido, PrePedidoDto>().ReverseMap();
            CreateMap<PrePedidoProduto, ProdutoEntityDto>().ReverseMap();
        }
    }
}