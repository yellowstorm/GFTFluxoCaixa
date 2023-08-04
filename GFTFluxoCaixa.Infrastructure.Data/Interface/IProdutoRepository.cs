using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GFTFluxoCaixa.Domain.Model;

namespace GFTFluxoCaixa.Infrastructure.Data.Interface
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(Int64 id);
        Task Create(Produto user);
        Task Update(Produto user);
        Task Delete(Int64 id);
        Task<Produto> GetByNome(string nome);
    }
}
