namespace MeuLivroDeReceitas.Domain.Repositorios;

public interface IUsuarioReadOnlyRepositorio
{
    Task<bool> ExisteUsuarioComEmail(string email);
    Task<Domain.Entidades.Usuario> RecuperarPorEmailSenha(string email, string senha);
}
