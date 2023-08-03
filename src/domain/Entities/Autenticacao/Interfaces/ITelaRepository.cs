

using Entities.Intefaces;

namespace Entities.Autenticacao.Interfaces;

public interface ITelaRepository : IBaseRepository<Tela>
{
    Task<Tela> BuscarTelaPorNome(string nome);
    Task<Tela> BuscarUsuarioPorCaminhho(string caminho);
    Task<List<Tela>> ListarTelas(int rows, int page);
}