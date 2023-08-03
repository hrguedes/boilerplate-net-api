using Entities.Autenticacao;
using Entities.Autenticacao.Interfaces;
using MongoDB.Driver;

namespace Data.Repositories.Autenticacao;

public class UsuarioRepository  : BaseRepository<Usuario>, IUsuarioRepository
{
    public async Task<Usuario> BuscarUsuarioPorLogin(string login)
    {
        var filter = Builders<Usuario>.Filter.Eq(q => q.Login, login);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }
    
    public async Task<Usuario> BuscarUsuarioFiltro(string nome, string email, string usuarioWindows)
    {
        var filter = Builders<Usuario>.Filter.Where(q => q.Nome == nome || 
                                                         q.Email == email || 
                                                         q.UsuarioDominio == usuarioWindows);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }
    
    public async Task<List<Usuario>> ListarUsuarios(int rows, int page)
    {
        var filter = Builders<Usuario>.Filter.Eq(q => q.RegistroRemovido, false);
        return await Collection.Find(filter).Skip((page - 1) * rows).Limit(rows).ToListAsync();
    }
}