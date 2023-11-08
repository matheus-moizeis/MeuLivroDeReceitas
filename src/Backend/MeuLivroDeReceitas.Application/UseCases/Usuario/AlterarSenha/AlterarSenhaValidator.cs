using FluentValidation;
using MeuLivroDeReceitas.Comunicacao.Requisicoes;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;

internal class AlterarSenhaValidator : AbstractValidator<RequisicaoAlteraSenhaJson>
{
    public AlterarSenhaValidator()
    {
        RuleFor(c => c.NovaSenha).SetValidator(new SenhaValidator());
    }
}
