using AutoMapper;
using HashidsNet;
using MeuLivroDeReceitas.Comunicacao.Requisicoes;
using MeuLivroDeReceitas.Comunicacao.Respostas;

namespace MeuLivroDeReceitas.Application.Servicos.Automapper;

public class AutoMapperConfiguracao : Profile
{
    private readonly IHashids _hashids;
    public AutoMapperConfiguracao(IHashids hashids)
    {
        _hashids = hashids;

        RequisicaoParaEntidade();
        EntidadeParaResposta();
    }

    private void RequisicaoParaEntidade()
    {
        CreateMap<RequisicaoRegistrarUsuarioJson, Domain.Entidades.Usuario>()
            .ForMember(destino => destino.Senha, config => config.Ignore());

        CreateMap<RequisicaoReceitaJson, Domain.Entidades.Receita>();
        CreateMap<RequisicaoIngredienteJson, Domain.Entidades.Ingrediente>();
    }

    private void EntidadeParaResposta()
    {
        CreateMap<Domain.Entidades.Receita, RespostaReceitaJson>()
            .ForMember(destino => destino.Id, config => config.MapFrom(origem => _hashids.EncodeLong(origem.Id)));


        CreateMap<Domain.Entidades.Ingrediente, RespostaIngredienteJson>()
            .ForMember(destino => destino.Id, config => config.MapFrom(origem => _hashids.EncodeLong(origem.Id)));
    }
}
