using ClientesEProdutos.Interfaces;
using ClientesEProdutos.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientesEProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Adiciona proteção em todas as rotas
    public class ProdutosController : ControllerBase
    {

        private readonly IProdutoRepository _repository;

        public ProdutosController(IProdutoRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        [Route("todosProdutos")]
        public async Task<IActionResult> ListarProdutos([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Os parâmetros de paginação devem ser maiores que zero.");
            }

            var totalRegistros = await _repository.ObterTotalProdutosAsync();
            var produtos = await _repository.ListarProdutosAsync(page, pageSize);

            var resposta = new
            {
                TotalRegistros = totalRegistros,
                TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize),
                PaginaAtual = page,
                Produtos = produtos
            };

            return Ok(resposta);
        }

        [HttpGet]
        [Route("produtosId/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var existencia = await VerificarExistenciaProduto(id);
            if (existencia != null) return existencia;

            var produto = await _repository.GetPorIdAsync(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [HttpPost]
        [Route("adicionarProduto")]
        public async Task<IActionResult> PostAsync([FromBody] Produtos produto)
        {
            var validacao = ValidarModelo();
            if (validacao != null) return validacao;

            await _repository.AdicionarProdutoAsync(produto);
            return CreatedAtAction(nameof(Get), new { id = produto.Codigo_produto }, produto);
        }

        [HttpPut]
        [Route("atualizarProduto/{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Produtos produto)
        {
            var validacao = ValidarModelo();
            if (validacao != null) return validacao;

            if (id != produto.Codigo_produto) return BadRequest("O ID fornecido não corresponde ao produto.");

            var existencia = await VerificarExistenciaProduto(id);
            if (existencia != null) return existencia;

            await _repository.AtualizarProdutoAsync(produto);
            return NoContent();
        }

        [HttpDelete]
        [Route("removerProduto/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var existencia = await VerificarExistenciaProduto(id);
            if (existencia != null) return existencia;

            await _repository.RemoverProdutoAsync(id);
            return NoContent();
        }

        private IActionResult ValidarModelo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return null;
        }

        private async Task<IActionResult> VerificarExistenciaProduto(int id)
        {
            var produto = await _repository.GetPorIdAsync(id);
            if (produto == null)
            {
                return NotFound($"Produto com ID {id} não encontrado.");
            }
            return null;
        }
    }
}