using System.Threading.Tasks;
using GFTFluxoCaixa.Domain.Model;
using GFTFluxoCaixa.Domain.Request;

namespace GFTFluxoCaixa.Service.Interface
{
    public interface IFluxoCaixaService
    {
        Task<FluxoCaixa> GetFluxoCaixaDiario();
    }
}
