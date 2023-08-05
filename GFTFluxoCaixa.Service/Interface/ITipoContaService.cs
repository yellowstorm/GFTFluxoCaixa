using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GFTFluxoCaixa.Domain.Model;
using GFTFluxoCaixa.Domain.Request;

namespace GFTFluxoCaixa.Service.Interface
{
    public interface ITipoContaService
    {
        Task Create(TipoContaRequest request);
        Task Delete(long id);
        Task<IEnumerable<TipoConta>> GetAll();
        Task<TipoConta> GetById(int id);
        Task Update(long id, TipoContaRequest request);
    }
}
