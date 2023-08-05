using GFTFluxoCaixa.Domain.Model;
using GFTFluxoCaixa.Domain.Request;
using System.Threading.Tasks;
using GFTFluxoCaixa.Infrastructure.Data.Interface;
using GFTFluxoCaixa.Service.Interface;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using GFTFluxoCaixa.Infrastructure.CrossCutting;
using System.Reflection;

namespace GFTFluxoCaixa.Service
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly ITipoContaRepository _tipoContaRepository;
        private readonly IProdutoRepository _produtoRepository;

        private readonly IMapper _mapper;

        public TransacaoService(ITransacaoRepository transacaoRepository,
            ITipoContaRepository tipoContaRepository,
            IProdutoRepository produtoRepository,
            IMapper mapper)
        {
            _transacaoRepository = transacaoRepository;
            _tipoContaRepository = tipoContaRepository;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task AddTransacao(TransacaoRequest request)
        {
            //TODO: precisa validar os campos
            var tipoConta = await _tipoContaRepository.GetById(request.IdTipoConta);
            if (tipoConta == null)
                throw new AppException("Tipo de conta não existente.");

            var produto = await _produtoRepository.GetById(request.IdProduto);
            if (produto == null)
                throw new AppException("Produto não existente.");

            var transacao = _mapper.Map<Transacao>(request);

            if (tipoConta.Id != 1 && tipoConta.Nome.Equals("Entrada"))
            {
                if (transacao.ValorUnitario > 0)
                    throw new AppException($"Valor {transacao.ValorUnitario} deve ser positivo.");
            }

            if (tipoConta.Id != 2 && tipoConta.Nome.Equals("Saída"))
            {
                if (transacao.ValorUnitario < 0)
                    throw new AppException($"Valor {transacao.ValorUnitario} deve ser negativo.");
            }

            await _transacaoRepository.Create(transacao);
        }
    }
}
