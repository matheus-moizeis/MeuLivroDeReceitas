﻿using MeuLivroDeReceitas.Application.Servicos.Token;
using MeuLivroDeReceitas.Comunicacao.Respostas;
using MeuLivroDeReceitas.Domain.Repositorios.Usuario;
using MeuLivroDeReceitas.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace MeuLivroDeReceitas.Api.Filtros;

public class UsuarioAutenticado : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly TokenController _tokenController;
    private readonly IUsuarioReadOnlyRepositorio _repositorio;

    public UsuarioAutenticado(TokenController tokenController, IUsuarioReadOnlyRepositorio repositorio)
    {
        _tokenController = tokenController;
        _repositorio = repositorio;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenNaRequisicao(context);
            var emailUsuario = _tokenController.RecuperarEmail(token);
            var usuario = await _repositorio.RecuperarPorEmail(emailUsuario);

            if (usuario is null)
            {
                throw new Exception();
            }
        }
        catch (SecurityTokenExpiredException)
        {
            TokenExpirado(context);
        }
        catch 
        {
            UsuarioSemPermissao(context);
        }

    }

    private string TokenNaRequisicao(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authorization) )
        {
            throw new Exception();
        }


        return authorization["Bearer".Length..].Trim();
    }

    private void TokenExpirado(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceMensagensDeErro.TOKEN_EXPIRADO));
    }
    private void UsuarioSemPermissao(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceMensagensDeErro.USUARIO_SEM_PERMISSAO));
    }
}
