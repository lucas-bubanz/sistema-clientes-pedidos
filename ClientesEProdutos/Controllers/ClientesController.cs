using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClientesEProdutos.Interfaces;
using ClientesEProdutos.Models.Entities;

namespace ClientesEProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Adiciona proteção em todas as rotas
    public class ClientesController : Controller
    {
        private readonly IClienteRepository _repository;

        public ClientesController(IClienteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("listarClientes")]
        // [Route("listarClientes")]
        public async Task<IActionResult> GetAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Os parâmetros de paginação devem ser maiores que zero.");
            }

            var totalRegistros = await _repository.ObterTotalClientesAsync();
            var clientes = await _repository.ListarClientesAsync(page, pageSize);

            var resposta = new
            {
                TotalRegistros = totalRegistros,
                TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize),
                PaginaAtual = page,
                Clientes = clientes
            };

            return Ok(resposta);
        }

        [HttpGet("listarClientePorId/{id}")]
        // [Route("listarClientePorId/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var cliente = await _repository.ListarClientePorIdAsync(id);
            if (cliente == null) return NotFound(id);
            return Ok(cliente);
        }

        [HttpPost("adicionarCliente")]
        // [Route("adicionarCliente")]
        public async Task<IActionResult> PostAsync([FromBody] Clientes clientes)
        {
            var validacao = await ValidarModelo();
            if (validacao != null) return validacao;

            await _repository.AdicionarCleinteAsync(clientes);
            return Created();
        }

        [HttpPut("atualizarCliente/{id}")]
        // [Route("atualizarCliente/{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Clientes clientes)
        {
            var validacao = await ValidarModelo();
            if (validacao != null) return validacao;

            if (id != clientes.Codigo_cliente) return BadRequest("O ID fornecido não corresponde ao cliente.");

            var existencia = await VerificarExistenciaCliente(id);
            if (existencia != null) return existencia;

            await _repository.AtualizarClienteAsync(clientes);
            return NoContent();
        }

        [HttpDelete("removerCliente/{id}")]
        // [Route("removerCliente/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var existencia = await VerificarExistenciaCliente(id);
            if (existencia != null) return existencia;

            await _repository.RemoverClienteAsync(id);
            return NoContent();
        }

        private Task<IActionResult> ValidarModelo()
        {
            if (!ModelState.IsValid)
            {
                return Task.FromResult<IActionResult>(BadRequest(ModelState));
            }
            return Task.FromResult<IActionResult>(null);
        }

        private async Task<IActionResult> VerificarExistenciaCliente(int id)
        {
            var cliente = await _repository.ListarClientePorIdAsync(id);
            if (cliente == null)
            {
                return NotFound($"Cliente com ID {id} não encontrado.");
            }
            return null;
        }
    }
}