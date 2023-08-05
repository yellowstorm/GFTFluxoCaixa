using System;

namespace GFTFluxoCaixa.Domain.Request
{
    public class TransacaoRequest
    {
        public int IdTipoConta { get; set; }
        public string Nome { get; set; }
        public Int64 IdProduto { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
    }
}
