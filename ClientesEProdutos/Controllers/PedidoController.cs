using ClientesEProdutos.Interfaces;
using ClientesEProdutos.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ClientesEProdutos.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
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
            var validacao = await ValidarModelo();
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

        [HttpDelete("cancelar-pre-pedido/{prePedidoId}")]
        public async Task<IActionResult> CancelarPrePedido(int prePedidoId)
        {
            var validacao = await ValidarModelo();
            if (validacao != null) return validacao;

            var existenciaPedido = await VerificarExistenciaPrePedido(prePedidoId);
            if (existenciaPedido != null) return existenciaPedido;

            return Ok(await _pedidoRepository.CancelarPrePedidoAsync(prePedidoId));
        }

        [HttpDelete("cancelar-pedido/{idPedido}")]
        public async Task<IActionResult> CancelarPedido(int idPedido)
        {
            var validacao = await ValidarModelo();
            if (validacao != null) return validacao;

            var existenciaPedido = await VerificarExistenciaPedido(idPedido);
            if (existenciaPedido != null) return existenciaPedido;

            // await _pedidoRepository.CancelarPedidoAsync(idPedido);
            return Ok(await _pedidoRepository.CancelarPedidoAsync(idPedido));
        }

        [HttpGet]
        public async Task<IActionResult> ListarPedidos([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Os parâmetros de paginação devem ser maiores que zero.");
            }

            var totalRegistros = await _pedidoRepository.ObterTotalPedidosAsync();
            var pedidos = await _pedidoRepository.ListarPedidosAsync(page, pageSize);

            var resposta = new
            {
                TotalRegistros = totalRegistros,
                TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize),
                PaginaAtual = page,
                Pedidos = pedidos
            };

            return Ok(resposta);
        }

        [HttpGet("listar-pre-pedidos")]
        public async Task<IActionResult> ListarPrePedidos([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Os parâmetros de paginação devem ser maiores que zero.");
            }

            var totalRegistros = await _pedidoRepository.ObterTotalPrePedidosAsync();
            var prePedidos = await _pedidoRepository.ListarPrePedidosAsync(page, pageSize);

            if (prePedidos == null || !prePedidos.Any())
            {
                return NotFound("Nenhum pré-pedido encontrado.");
            }

            var resposta = new
            {
                TotalRegistros = totalRegistros,
                TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize),
                PaginaAtual = page,
                PrePedidos = prePedidos
            };

            return Ok(resposta);
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

        private async Task<IActionResult> VerificarExistenciaPrePedido(int prePedidoId)
        {
            var prePedido = await _pedidoRepository.ConsultarPrePedidoAsync(prePedidoId);
            if (prePedido == null) return NotFound($"Pedido com ID {prePedidoId} não encontrado.");
            return null;
        }

        private Task<IActionResult> ValidarModelo()
        {
            if (!ModelState.IsValid)
            {
                return Task.FromResult<IActionResult>(BadRequest(ModelState));
            }
            return Task.FromResult<IActionResult>(null);
        }
    }

}