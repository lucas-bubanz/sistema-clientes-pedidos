using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ClientesEProdutos.Interfaces;
using ClientesEProdutos.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClientesEProdutos.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ProdutosController : Controller
    {

        private readonly IProdutoRepository _repository;

        public ProdutosController(IProdutoRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        [Route("todosProdutos")]
        public IActionResult Get() => Ok(_repository.GetProdutos());

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