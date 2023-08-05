using GFTFluxoCaixa.Domain.Request;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using GFTFluxoCaixa.Service.Interface;

namespace GFTFluxoCaixa.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class TipoContaController : ControllerBase
    {
        ITipoContaService _tipoContaService;
        public TipoContaController(ITipoContaService tipoContaService)
        {
            _tipoContaService = tipoContaService; 
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tipoContas = await _tipoContaService.GetAll();
            return Ok(tipoContas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tipoConta = await _tipoContaService.GetById(id);
            return Ok(tipoConta);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]TipoContaRequest request)
        {
            await _tipoContaService.Create(request);
            return Ok(new { message = "Tipo de conta criado." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Int64 id, TipoContaRequest request)
        {
            await _tipoContaService.Update(id, request);
            return Ok(new { message = "Tipo de conta atualizado." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Int64 id)
        {
            await _tipoContaService.Delete(id);
            return Ok(new { message = "Tipo de Conta deletado." });
        }
    }
}
