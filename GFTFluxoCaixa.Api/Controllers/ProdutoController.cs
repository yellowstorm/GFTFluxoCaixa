using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GFTFluxoCaixa.Domain.Model;
using GFTFluxoCaixa.Domain.Request;
using GFTFluxoCaixa.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GFTFluxoCaixa.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProdutoController:ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var produtos = await _produtoService.GetAll();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var produto = await _produtoService.GetById(id);
            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProdutoRequest request)
        {
            await _produtoService.Create(request);
            return Ok(new { message = "Produto criado." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Int64 id, ProdutoRequest request)
        {
            await _produtoService.Update(id, request);
            return Ok(new { message = "Produto atualizado." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Int64 id)
        {
            await _produtoService.Delete(id);
            return Ok(new { message = "Produto deletado." });
        }
    }
}
