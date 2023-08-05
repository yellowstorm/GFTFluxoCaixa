using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GFTFluxoCaixa.Domain.Model;

namespace GFTFluxoCaixa.Infrastructure.Data.Interface
{
    public interface ITransacaoRepository
    {
        Task Create(Transacao transacao);
        Task<IEnumerable<Transacao>> GetAllByDataHoje(DateTime now);
    }
}
