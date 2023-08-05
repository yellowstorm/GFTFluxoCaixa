using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GFTFluxoCaixa.Domain.Model;
using GFTFluxoCaixa.Domain.Request;
using GFTFluxoCaixa.Infrastructure.Data.Interface;
using GFTFluxoCaixa.Service.Interface;

namespace GFTFluxoCaixa.Service
{
    public class FluxoCaixaService : IFluxoCaixaService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IMapper _mapper;

        public FluxoCaixaService(ITransacaoRepository transacaoRepository, IMapper mapper)
        {
            _transacaoRepository = transacaoRepository;
            _mapper = mapper;
        }        

        public async Task<FluxoCaixa> GetFluxoCaixaDiario()
        {
            FluxoCaixa fluxoCaixa = new FluxoCaixa();
            fluxoCaixa.TransacoesEntrada = new List<Transacao>();
            fluxoCaixa.TransacoesSaida = new List<Transacao>();

            var transacaoList = await _transacaoRepository.GetAllByDataHoje(DateTime.Now);

            double totalEntradas = 0;
            double totalSaidas = 0;
            foreach (var item in transacaoList)
            {
                if (item.IdTipoConta==1)
                {
                    var valor = item.Quantidade * item.ValorUnitario;
                    fluxoCaixa.TotalEntrada += valor;
                    fluxoCaixa.TransacoesEntrada.Add(item);
                }
                else
                {
                    var valor = item.Quantidade * item.ValorUnitario;
                    fluxoCaixa.TotalSaida += valor;
                    fluxoCaixa.TransacoesSaida.Add(item);
                }
            }

            fluxoCaixa.TotalFluxoCaixa = Math.Round(fluxoCaixa.TotalEntrada + fluxoCaixa.TotalSaida,2);
            return fluxoCaixa;
        }
    }
}
