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
        public IActionResult Get(int id)
        {
            var produto = _repository.GetPorId(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [HttpPost]
        [Route("adicionarProduto")]
        public async Task<IActionResult> PostAsync([FromBody] Produtos produto)
        {
            await _repository.AdicionarProdutoAsync(produto);
            return CreatedAtAction(nameof(Get), new { id = produto.Codigo_produto }, produto);
        }

        [HttpPut]
        [Route("atualizarProduto/{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Produtos produto)
        {
            if (id != produto.Codigo_produto) return BadRequest();
            await _repository.AtualizarProdutoAsync(produto);
            return NoContent();
        }

        [HttpDelete]
        [Route("removerProduto/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _repository.RemoverProdutoAsync(id);
            return NoContent();
        }
    }
}