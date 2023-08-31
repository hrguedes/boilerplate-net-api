using Base;
using Dto.Autenticacao.Request;
using Dto.Autenticacao.Response;
using Entities.Autenticacao;

namespace Application.Autenticacao.Interfaces;

public interface IBaseService
{
    Task<List<Regra>> ValidarRegras(string[] regras);
    
    #region Regras (CRUD)
    Task<ReturnOk<RegraResponse>> DetalhesRegra(string id);
    Task<ReturnOk<ListarRegrasResponse>> ListarRegras(ListarRegrasRequest request);
    Task<ReturnOk<RegraResponse>> CadastrarRegra(CadastrarOuEditarRegraRequest request, string userId);
    Task<ReturnOk<RegraResponse>> EditarRegra(CadastrarOuEditarRegraRequest request, string userId);
    Task<ReturnOk<RegraResponse>> RemoverRegra(string regraId, string userId);
    #endregion
}