using System.Threading.Tasks;
using GFTFluxoCaixa.Domain.Model;

namespace GFTFluxoCaixa.Service.Interface
{
    public interface IFluxoCaixaService
    {
        Task<FluxoCaixa> GetFluxoCaixaDiario();
    }
}
