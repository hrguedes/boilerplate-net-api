using Base;
using Dto.Autenticacao.Request;
using Dto.Autenticacao.Response;
using Entities.Autenticacao;

namespace Application.Autenticacao.Interfaces;

public interface IBaseService
{

    Task<List<Tela>> ValidarTelas(string[] telas);
    Task<List<Regra>> ValidarRegras(string[] regras);
    
    #region Regras (CRUD)
    Task<ReturnOk<RegraResponse>> DetalhesRegra(string id);
    Task<ReturnOk<ListarRegrasResponse>> ListarRegras(ListarRegrasRequest request);
    Task<ReturnOk<RegraResponse>> CadastrarRegra(CadastrarOuEditarRegraRequest request, string userId);
    Task<ReturnOk<RegraResponse>> EditarRegra(CadastrarOuEditarRegraRequest request, string userId);
    Task<ReturnOk<RegraResponse>> RemoverRegra(string regraId, string userId);
    #endregion
    
    #region Telas (CRUD)
    Task<ReturnOk<TelaResponse>> DetalhesTela(string id);
    Task<ReturnOk<ListarTelasResponse>> ListarTelas(ListarTelasRequest request);
    Task<ReturnOk<TelaResponse>> CadastrarTela(CadastrarOuEditarTelaRequest request, string userId);
    Task<ReturnOk<TelaResponse>> EditarTela(CadastrarOuEditarTelaRequest request, string userId);
    Task<ReturnOk<TelaResponse>> RemoverTela(string id, string userId);
    #endregion
}