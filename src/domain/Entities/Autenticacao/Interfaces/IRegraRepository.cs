using Entities.Intefaces;

namespace Entities.Autenticacao.Interfaces;

public interface IRegraRepository : IBaseRepository<Regra>
{
    Task<Regra> BuscarRegraPorNome(string nome);
    Task<List<Regra>> ListarRegras(int rows, int page);
}