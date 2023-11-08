using MeuLivroDeReceitas.Api.Filtros;
using MeuLivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;
using MeuLivroDeReceitas.Comunicacao.Requisicoes;
using MeuLivroDeReceitas.Comunicacao.Respostas;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.Api.Controllers
{
    public class UsuarioController : MeuLivroDeReceitasController
    {
        [HttpPost]
        [ProducesResponseType(typeof(RespostaUsuarioRegistradoJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegistrarUsuario([FromServices] IRegistrarUsuarioUseCase useCase, [FromBody] RequisicaoRegistrarUsuarioJson request)
        {
            var resultado = await useCase.Executar(request);

            return Created(string.Empty, resultado);
        }

        [HttpPut]
        [Route("alterar-senha")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ServiceFilter(typeof(UsuarioAutenticado))]
        public async Task<IActionResult> AlterarSenha(
        [FromServices] IAlterarSenhaUseCase useCase,
        [FromBody] RequisicaoAlteraSenhaJson request)
        {
            await useCase.Executar(request);

            return NoContent();
        }
    };
}