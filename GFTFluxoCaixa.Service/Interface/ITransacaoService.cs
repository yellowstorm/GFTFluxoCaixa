using System.Threading.Tasks;
using GFTFluxoCaixa.Domain.Request;

namespace GFTFluxoCaixa.Service.Interface
{
    public interface ITransacaoService
    {
        Task AddTransacao(TransacaoRequest transacao);
    }
}
