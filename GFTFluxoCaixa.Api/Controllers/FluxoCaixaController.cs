using System.Threading.Tasks;
using GFTFluxoCaixa.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GFTFluxoCaixa.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class FluxoCaixaController:ControllerBase
    {
        private readonly IFluxoCaixaService _fluxoCaixaService;
        public FluxoCaixaController(IFluxoCaixaService fluxoCaixaService)
        {
            _fluxoCaixaService = fluxoCaixaService;
        }

        [HttpGet("FluxoCaixaDiario")]
        public async Task<IActionResult> GetFluxoCaixaDiario()
        {
            var fluxoCaixa = await _fluxoCaixaService.GetFluxoCaixaDiario();
            return Ok(fluxoCaixa);
        }

        //[HttpGet("Mes/{mes}/Ano/{}")]
        //public async Task<IActionResult> GetFluxoCaixaMes(int mes, int ano)
        //{
        //    //TODO:precisa implementar
        //    var fluxoCaixa = await _fluxoCaixaService.GetFluxoCaixaDiario();
        //    return Ok(fluxoCaixa);
        //}

        //[HttpGet("Ano/{ano}")]
        //public async Task<IActionResult> GetFluxoCaixaAno(int ano)
        //{
        //    //TODO:precisa implementar
        //    var fluxoCaixa = await _fluxoCaixaService.GetFluxoCaixaDiario();
        //    return Ok(fluxoCaixa);
        //}
    }
}
