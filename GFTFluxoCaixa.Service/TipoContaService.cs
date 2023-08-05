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
    public class TipoContaService : ITipoContaService
    {
        private ITipoContaRepository _tipoContaRepository;
        private readonly IMapper _mapper;
        public TipoContaService(ITipoContaRepository tipoContaRepository, IMapper mapper)
        {
            _tipoContaRepository = tipoContaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TipoConta>> GetAll()
        {
            return await _tipoContaRepository.GetAll();
        }

        public async Task<TipoConta> GetById(int id)
        {
            var produto = await _tipoContaRepository.GetById(id);

            if (produto == null)
                throw new KeyNotFoundException("Produto não encontrado.");

            return produto;
        }

        public async Task Create(TipoContaRequest request)
        {
            if (await _tipoContaRepository.GetByNome(request.Nome!) != null)
                throw new AppException("Produto de nome '" + request.Nome + "' já existe.");

            var tipoConta = _mapper.Map<TipoConta>(request);

            await _tipoContaRepository.Create(tipoConta);
        }

        public async Task Update(long id, TipoContaRequest request)
        {
            var tipoConta = await _tipoContaRepository.GetById(id);

            if (tipoConta == null)
                throw new KeyNotFoundException("User not found");

            if (await _tipoContaRepository.GetByNome(request.Nome!) != null)
                throw new AppException("Produto de nome '" + request.Nome + "' já existe.");

            _mapper.Map(request, tipoConta);

            await _tipoContaRepository.Update(tipoConta);
        }

        public async Task Delete(long id)
        {
            await _tipoContaRepository.Delete(id);
        }        
    }
}
