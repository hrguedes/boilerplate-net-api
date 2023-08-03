using Entities.Autenticacao;
using Entities.Autenticacao.Interfaces;
using MongoDB.Driver;

namespace Data.Repositories.Autenticacao;

public class RegraRepository : BaseRepository<Regra>, IRegraRepository
{
    public async Task<Regra> BuscarRegraPorNome(string nome)
    {
        var filter = Builders<Regra>.Filter.Eq(q => q.Nome, nome);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }
    
    public async Task<List<Regra>> ListarRegras(int rows, int page)
    {
        var filter = Builders<Regra>.Filter.Eq(q => q.RegistroRemovido, false);
        return await Collection.Find(filter).Skip((page - 1) * rows).Limit(rows).ToListAsync();
    }
}