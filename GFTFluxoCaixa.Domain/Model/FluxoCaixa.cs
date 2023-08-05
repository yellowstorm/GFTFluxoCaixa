using System;
using System.Collections.Generic;

namespace GFTFluxoCaixa.Domain.Model
{
    public class FluxoCaixa
    {
        public List<Transacao> TransacoesEntrada;
        public List<Transacao> TransacoesSaida;
        public Double TotalEntrada;
        public Double TotalSaida;
        public Double TotalFluxoCaixa;
    }
}
