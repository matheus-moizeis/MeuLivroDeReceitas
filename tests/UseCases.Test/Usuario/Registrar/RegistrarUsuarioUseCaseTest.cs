using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;
using Utilitario.ParaOsTestes.Criptografia;
using Utilitario.ParaOsTestes.Mapper;
using Utilitario.ParaOsTestes.Repositorios;
using Utilitario.ParaOsTestes.Requisicoes;
using Utilitario.ParaOsTestes.Token;
using Xunit;

namespace UseCases.Test.Usuario.Registrar;

public class RegistrarUsuarioUseCaseTest
{
    [Fact]
    public async Task Validar_Sucesso()
    {
        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();

        var useCase = CriarUseCase();

        var resposta = await useCase.Executar(requisicao);

        resposta.Should().NotBeNull();
        resposta.Token.Should().NotBeNullOrWhiteSpace();
    }

    private static RegistrarUsuarioUseCase CriarUseCase(string email = "")
    {
        var mapper = MapperBuilder.Instancia();
        var repositorio = UsuarioWriteOnlyRepositorioBuilder.Instancia().Construir();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instancia().Construir();
        var encriptador = EncriptadorDeSenhaBuilder.Instancia();
        var token = TokenControllerBuilder.Instancia();
        var repositorioReadOnly = UsuarioReadOnlyRepositorioBuilder.Instancia().ExisteUsuarioComEmail(email).Construir();

        return new RegistrarUsuarioUseCase(repositorio, mapper, unidadeDeTrabalho, encriptador, token, repositorioReadOnly);
    }
}
