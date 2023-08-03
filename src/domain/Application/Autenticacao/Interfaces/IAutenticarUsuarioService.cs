using Base;
using Dto.Autenticacao.Request;
using Dto.Autenticacao.Response;

namespace Application.Autenticacao.Interfaces;

public interface IAutenticarUsuarioService
{
    Task SalvarTokenNaSessao(string id, string token);
    Task<ReturnOk<UsuarioLogadoResponse>> ObterUsuarioPorLogin(AutenticarUsuarioRequest request);
}