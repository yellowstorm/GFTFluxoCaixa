using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GFTFluxoCaixa.Domain.Model;
using GFTFluxoCaixa.Domain.Request;

namespace GFTFluxoCaixa.Service.Interface
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(Int64 id);
        Task Create(ProdutoRequest request);
        Task Delete(Int64 id);
        
        
        Task Update(Int64 id, ProdutoRequest request);
    }
}
