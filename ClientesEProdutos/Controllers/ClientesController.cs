using Microsoft.AspNetCore.Mvc;
using ClientesEProdutos.Interfaces;
using System.Threading.Tasks;
using ClientesEProdutos.Models.Entities;

namespace ClientesEProdutos.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ClientesController : Controller
    {
        private readonly IClienteRepository _repository;

        public ClientesController(IClienteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("listarClientes")]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _repository.GetClientes());
        }

        [HttpGet]
        [Route("listarClientePorId/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var cliente = await _repository.GetClientePorId(id);
            if (cliente == null) return NotFound(id);
            return Ok(cliente);
        }

        [HttpPost]
        [Route("adicionarCliente")]
        public async Task<IActionResult> PostAsync([FromBody] Clientes clientes)
        {
            await _repository.AdicionarCleinteAsync(clientes);
            return Created();
        }

        [HttpPut]
        [Route("atualizarCliente/{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Clientes clientes)
        {
            if (id != clientes.Codigo_cliente) return BadRequest();
            await _repository.AtualizarClienteAsync(clientes);
            return NoContent();
        }

        [HttpDelete]
        [Route("removerCliente/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var clienteExistente = await _repository.GetClientePorId(id);
            if (clienteExistente == null) return NotFound();
            await _repository.RemoverClienteAsync(id);
            return NoContent();
        }
    }
}