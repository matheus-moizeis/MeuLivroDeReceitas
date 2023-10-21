using AutoMapper;
using MeuLivroDeReceitas.Comunicacao.Requisicoes;

namespace MeuLivroDeReceitas.Application.Servicos.Automapper;

public class AutoMapperConfiguracao : Profile
{
    public AutoMapperConfiguracao()
    {
        CreateMap<RequisicaoRegistrarUsuarioJson, Domain.Entidades.Usuario>()
            .ForMember(destino => destino.Senha, config => config.Ignore());
    }
}
