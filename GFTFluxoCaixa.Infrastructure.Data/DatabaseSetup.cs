using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GFTFluxoCaixa.Infrastructure.Data.Interface;

namespace GFTFluxoCaixa.Infrastructure.Data
{
    public class DatabaseSetup:IDatabaseSetup
    {
        private readonly DataContext _dataContext;
        public DatabaseSetup(DataContext dataContext) 
        { 
            _dataContext = dataContext;
        }

        public async Task Setup()
        {
            using var connection = _dataContext.CreateConnection();

            var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Product';");
            var tableName = table.FirstOrDefault();
            if (!string.IsNullOrEmpty(tableName) && tableName == "Produto")
                return;

            await connection.ExecuteAsync("Create Table Produto(" +
                "Id bigint primary key autoincrement," +
                "Nome varchar(200) not null);");

            await connection.ExecuteAsync("Create Table TipoConta(" +
                "Id int primary key autoincrement," +
                "Nome varchar(200) not null);");

            await connection.ExecuteAsync("Create Table FluxoCaixa(" +
                "Id bigint primary key autoincrement," +
                "IdTipoConta int not null," +
                "IdProduto bigint not null," +
                "Quantidade int not null," +
                "ValorUnitario decimal not null);");


            await connection.ExecuteAsync("INSERT INTO Produto (Nome) VALUES ('Macarrão');");
            await connection.ExecuteAsync("INSERT INTO Produto (Nome) VALUES ('Suco de laranja');");
            await connection.ExecuteAsync("INSERT INTO Produto (Nome) VALUES ('Iorgute');");
            await connection.ExecuteAsync("INSERT INTO Produto (Nome) VALUES ('Papel higiênico');");

            await connection.ExecuteAsync("INSERT INTO TipoConta (Nome) VALUES ('Entrada');");
            await connection.ExecuteAsync("INSERT INTO TipoConta (Nome) VALUES ('Saída');");
        }
    }
}
