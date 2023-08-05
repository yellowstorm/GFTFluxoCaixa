using GFTFluxoCaixa.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace GFTFluxoCaixa.Infrastructure.Data.Interface
{
    public interface ITipoContaRepository
    {
        Task<IEnumerable<TipoConta>> GetAll();

        Task<TipoConta> GetById(Int64 id);

        Task<TipoConta> GetByNome(string nome);

        Task Create(TipoConta tipoConta);

        Task Update(TipoConta tipoConta);

        Task Delete(Int64 id);

    }
}
