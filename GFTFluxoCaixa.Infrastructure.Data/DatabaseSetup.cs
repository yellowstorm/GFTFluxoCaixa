using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Dapper;
using GFTFluxoCaixa.Domain.Model;
using GFTFluxoCaixa.Infrastructure.Data.Interface;
using static System.Net.Mime.MediaTypeNames;

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

            var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Produto';");
            var tableName = table.FirstOrDefault();
            if (!string.IsNullOrEmpty(tableName) && tableName == "Produto")
                return;

            
            await connection.ExecuteAsync($@"CREATE TABLE Produto(
                            Id    INTEGER NOT NULL UNIQUE,
                            Nome  TEXT NOT NULL,
                            PRIMARY KEY(Id AUTOINCREMENT)
                        ); ");

            await connection.ExecuteAsync($@"Create Table TipoConta(
                Id    INTEGER NOT NULL UNIQUE,
                            Nome  TEXT NOT NULL,
                            PRIMARY KEY(Id AUTOINCREMENT)
                );");

            await connection.ExecuteAsync($@"Create Table FluxoCaixa(
                            Id    INTEGER NOT NULL UNIQUE,
                            Nome  TEXT NOT NULL,
                            IdTipoConta INTEGER NOT NULL,
                            IdProduto INTEGER NOT NULL,
                            Quantidade INTEGER NOT NULL,
                            ValorUnitario decimal not null, DataCriacao datetime, primary key(Id Autoincrement));");


            await connection.ExecuteAsync("INSERT INTO Produto (Nome) VALUES ('Macarrão');");
            await connection.ExecuteAsync("INSERT INTO Produto (Nome) VALUES ('Suco de laranja');");
            await connection.ExecuteAsync("INSERT INTO Produto (Nome) VALUES ('Iorgute');");
            await connection.ExecuteAsync("INSERT INTO Produto (Nome) VALUES ('Papel higiênico');");

            await connection.ExecuteAsync("INSERT INTO TipoConta (Nome) VALUES ('Entrada');");
            await connection.ExecuteAsync("INSERT INTO TipoConta (Nome) VALUES ('Saída');");
        }
    }
}
