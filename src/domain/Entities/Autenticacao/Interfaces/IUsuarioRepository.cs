

using Entities.Intefaces;

namespace Entities.Autenticacao.Interfaces;

public interface IUsuarioRepository : IBaseRepository<Usuario>
{
    Task<Usuario> BuscarUsuarioPorLogin(string login);
    Task<List<Usuario>> ListarUsuarios(int rows, int page);
    Task<Usuario> BuscarUsuarioFiltro(string nome, string email, string usuarioWindows);
}