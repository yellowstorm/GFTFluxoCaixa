using Dapper;
using GFTFluxoCaixa.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using GFTFluxoCaixa.Infrastructure.Data.Interface;

namespace GFTFluxoCaixa.Infrastructure.Data.Repository
{
    public class TipoContaRepository : ITipoContaRepository
    {
        private DataContext _dataContext;
        public TipoContaRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<TipoConta>> GetAll()
        {
            using var connection = _dataContext.CreateConnection();
            var sql = "SELECT * FROM TipoConta";
            return await connection.QueryAsync<TipoConta>(sql);
        }

        public async Task<TipoConta> GetById(Int64 id)
        {
            using var connection = _dataContext.CreateConnection();
            var sql = "SELECT * FROM TipoConta WHERE Id = @id";
            return await connection.QuerySingleOrDefaultAsync<TipoConta>(sql, new { id });
        }

        public async Task<TipoConta> GetByNome(string nome)
        {
            using var connection = _dataContext.CreateConnection();
            var sql = "SELECT * FROM TipoConta WHERE Nome = @nome";
            return await connection.QuerySingleOrDefaultAsync<TipoConta>(sql, new { nome });
        }

        public async Task Create(TipoConta tipoConta)
        {
            using var connection = _dataContext.CreateConnection();
            var sql = " INSERT INTO TipoConta (Nome)VALUES (@Nome)";
            await connection.ExecuteAsync(sql, tipoConta.Nome);
        }

        public async Task Update(TipoConta tipoConta)
        {
            using var connection = _dataContext.CreateConnection();
            var sql = $@"UPDATE TipoConta 
                        SET Nome = @Nome,
                       WHERE Id = @Id";
            await connection.ExecuteAsync(sql, tipoConta);
        }

        public async Task Delete(Int64 id)
        {
            using var connection = _dataContext.CreateConnection();
            var sql = "DELETE FROM TipoConta WHERE Id = @id";
            await connection.ExecuteAsync(sql, new { id });
        }
    }
}
