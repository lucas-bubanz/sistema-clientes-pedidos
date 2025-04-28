using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ClientesEProdutos.Interfaces;
using ClientesEProdutos.Models.Entities;
using ClientesEProdutos.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClientesEProdutos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        [HttpPost("pre-pedido")]
        public async Task<IActionResult> CriarPrePedido([FromBody] PrePedido prePedido)
        {
            var validacao = ValidarModelo();
            if (validacao != null) return validacao;

            var novoPrePedido = await _pedidoRepository.CriarPrePedidoAsync(prePedido);
            return CreatedAtAction(nameof(CriarPrePedido), new { id = novoPrePedido.IdPrePedido }, novoPrePedido);
        }

        [HttpPost("confirmar/{prePedidoId}")]
        public async Task<IActionResult> ConfirmarPrePedido(int prePedidoId)
        {
            var pedido = await _pedidoRepository.ConfirmarPrePedidoAsync(prePedidoId);
            if (pedido == null)
                return NotFound("Pré-pedido não encontrado.");

            return Ok(pedido);
        }

        [HttpGet]
        public async Task<IActionResult> ListarPedidos()
        {
            var pedidos = await _pedidoRepository.ListarPedidosAsync();
            return Ok(pedidos);
        }

        [HttpGet("{pedidoId}")]
        public async Task<IActionResult> ConsultarPedido(int pedidoId)
        {
            var existencia = await VerificarExistenciaPedido(pedidoId);
            if (existencia != null) return existencia;

            var pedido = await _pedidoRepository.ConsultarPedidoAsync(pedidoId);
            if (pedido == null)
                return NotFound("Pedido não encontrado.");

            return Ok(pedido);
        }

        private async Task<IActionResult> VerificarExistenciaPedido(int pedidoId)
        {
            var pedido = await _pedidoRepository.ConsultarPedidoAsync(pedidoId);
            if (pedido == null)
            {
                return NotFound($"Pedido com ID {pedidoId} não encontrado.");
            }
            return null; // Indica que o pedido existe
        }

        private IActionResult ValidarModelo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return null;
        }
    }

}