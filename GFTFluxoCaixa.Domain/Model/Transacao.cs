using System;

namespace GFTFluxoCaixa.Domain.Model
{
    public class Transacao
    {
        public Int64 Id { get; set; }
        public int IdTipoConta { get; set; }
        public Int64 IdProduto { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
    }
}
