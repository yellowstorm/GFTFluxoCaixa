using GFTFluxoCaixa.Domain.Request;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GFTFluxoCaixa.Service.Interface;

namespace GFTFluxoCaixa.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class TransacaoController:ControllerBase
    {
        private readonly ITransacaoService _transacaoService;

        public TransacaoController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTransacao([FromBody]TransacaoRequest transacao)
        {
            await _transacaoService.AddTransacao(transacao);
            return Ok(new { message = "Transação gravada com sucesso." });
        }
    }
}
