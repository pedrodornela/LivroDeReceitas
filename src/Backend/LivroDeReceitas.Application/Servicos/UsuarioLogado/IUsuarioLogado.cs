using LivroDeReceitas.Domain.Entidades;

namespace LivroDeReceitas.Application.Servicos.UsuarioLogado;

public interface IUsuarioLogado
{
    Task<Usuario> RecuperarUsuario();
}
