using System;
using System.Collections.Generic;
using System.Reflection;
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

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
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

            var user = _mapper.Map<Produto>(request);

            await _produtoRepository.Create(user);
        }

        public async Task Update(Int64 id, ProdutoRequest request)
        {
            var user = await _produtoRepository.GetById(id);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            // validate
            if (await _produtoRepository.GetByNome(request.Nome!) != null)
                throw new AppException("Produto de nome '" + request.Nome + "' já existe.");

            // copy model props to user
            _mapper.Map(request, user);

            // save user
            await _produtoRepository.Update(user);
        }

        public async Task Delete(Int64 id)
        {
            await _produtoRepository.Delete(id);
        }
    }
}
