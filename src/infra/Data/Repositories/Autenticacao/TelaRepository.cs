using Entities.Autenticacao;
using Entities.Autenticacao.Interfaces;
using MongoDB.Driver;

namespace Data.Repositories.Autenticacao;

public class TelaRepository : BaseRepository<Tela>, ITelaRepository
{
    public async Task<Tela> BuscarTelaPorNome(string nome)
    {
        var filter = Builders<Tela>.Filter.Eq(q => q.Nome, nome);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }
    
    public async Task<Tela> BuscarUsuarioPorCaminhho(string caminho)
    {
        var filter = Builders<Tela>.Filter.Eq(q => q.Caminho, caminho);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }
    
    public async Task<List<Tela>> ListarTelas(int rows, int page)
    {
        var filter = Builders<Tela>.Filter.Eq(q => q.RegistroRemovido, false);
        return await Collection.Find(filter).Skip((page - 1) * rows).Limit(rows).ToListAsync();
    }
}