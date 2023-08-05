using AutoMapper;
using GFTFluxoCaixa.Domain.Model;
using GFTFluxoCaixa.Domain.Request;

namespace GFTFluxoCaixa.Infrastructure.Data.Mapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProdutoRequest, Produto>();
            CreateMap<TipoContaRequest, TipoConta>();
            CreateMap<TransacaoRequest, Transacao>();
        }
    }
}
