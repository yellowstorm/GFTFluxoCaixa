using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GFTFluxoCaixa.Domain.Model;
using GFTFluxoCaixa.Domain.Request;
using GFTFluxoCaixa.Infrastructure.CrossCutting;
using GFTFluxoCaixa.Infrastructure.Data.Interface;
using GFTFluxoCaixa.Service.Interface;

namespace GFTFluxoCaixa.Service
{
    public class ProdutoService : IProdutoService
    {
        private IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _produtoRepository.GetAll();
        }

        public async Task<Produto> GetById(Int64 id)
        {
            var produto = await _produtoRepository.GetById(id);

            if (produto == null)
                throw new KeyNotFoundException("Produto não encontrado.");

            return produto;
        }

        public async Task Create(ProdutoRequest request)
        {
            if (await _produtoRepository.GetByNome(request.Nome!) != null)
                throw new AppException("Produto de nome '" + request.Nome + "' já existe.");

            var produto = _mapper.Map<Produto>(request);

            await _produtoRepository.Create(produto);
        }

        public async Task Update(Int64 id, ProdutoRequest request)
        {
            var produto = await _produtoRepository.GetById(id);

            if (produto == null)
                throw new KeyNotFoundException("User not found");

            if (await _produtoRepository.GetByNome(request.Nome!) != null)
                throw new AppException("Produto de nome '" + request.Nome + "' já existe.");

            _mapper.Map(request, produto);

            await _produtoRepository.Update(produto);
        }

        public async Task Delete(Int64 id)
        {
            await _produtoRepository.Delete(id);
        }
    }
}
