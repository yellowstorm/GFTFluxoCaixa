using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using GFTFluxoCaixa.Domain.Model;
using GFTFluxoCaixa.Infrastructure.Data.Interface;

namespace GFTFluxoCaixa.Infrastructure.Data.Repository
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private DataContext _dataContext;
        public TransacaoRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(Transacao transacao)
        {
            using var connection = _dataContext.CreateConnection();
            var sql = " INSERT INTO Transacao (Nome, IdTipoConta, IdProduto, Quantidade, ValorUnitario, DataCriacao) VALUES (@Nome, @IdTipoConta, @IdProduto, @Quantidade, @ValorUnitario, @DataCriacao)";
            await connection.ExecuteAsync(sql, transacao);
        }

        public async Task<IEnumerable<Transacao>> GetAllByDataHoje(DateTime now)
        {
            using var connection = _dataContext.CreateConnection();

            DateTime dataInicio = now.AddDays(-1);
            dataInicio = new DateTime(dataInicio.Year, dataInicio.Month, dataInicio.Day, 23, 59, 59);

            DateTime dataFim = new DateTime(now.Year, now.Month, now.Day,23,59,59);

            var sql = "Select * from Transacao where DataCriacao>@dataInicio and DataCriacao<=@dataFim";
            return await connection.QueryAsync<Transacao>(sql, new { dataInicio, dataFim});
        }
    }
}
