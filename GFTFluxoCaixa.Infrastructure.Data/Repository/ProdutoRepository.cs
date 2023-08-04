using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using GFTFluxoCaixa.Domain.Model;
using GFTFluxoCaixa.Infrastructure.Data.Interface;

namespace GFTFluxoCaixa.Infrastructure.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private DataContext _dataContext;
        public ProdutoRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            using var connection = _dataContext.CreateConnection();
            var sql = "SELECT * FROM Produto";
            return await connection.QueryAsync<Produto>(sql);
        }

        public async Task<Produto> GetById(Int64 id)
        {
            using var connection = _dataContext.CreateConnection();
            var sql = "SELECT * FROM Users WHERE Id = @id";
            return await connection.QuerySingleOrDefaultAsync<Produto>(sql, new { id });
        }

        public async Task<Produto> GetByNome(string nome)
        {
            using var connection = _dataContext.CreateConnection();
            var sql = "SELECT * FROM Users WHERE Name = @nome";
            return await connection.QuerySingleOrDefaultAsync<Produto>(sql, new { nome });
        }

        public async Task Create(Produto produto)
        {
            using var connection = _dataContext.CreateConnection();
            var sql = " INSERT INTO Produto (Name)VALUES (@Name)";
            await connection.ExecuteAsync(sql, produto.Name);
        }

        public async Task Update(Produto produto)
        {
            using var connection = _dataContext.CreateConnection();
            var sql = $@"UPDATE Produto 
                        SET Name = @Name,
                       WHERE Id = @Id";
            await connection.ExecuteAsync(sql, produto);
        }

        public async Task Delete(Int64 id)
        {
            using var connection = _dataContext.CreateConnection();
            var sql = "DELETE FROM Produto WHERE Id = @id";
            await connection.ExecuteAsync(sql, new { id });
        }       
    }
}
