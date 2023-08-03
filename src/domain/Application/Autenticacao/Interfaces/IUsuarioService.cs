using Base;
using Dto.Autenticacao.Request;
using Dto.Autenticacao.Response;

namespace Application.Autenticacao.Interfaces;

public interface IUsuarioService
{
    Task<ReturnOk<UsuarioResponse>> DetalhesUsuario(string id);
    Task<ReturnOk<ListarUsuariosResponse>> ListarUsuarios(ListarUsuariosRequest request);
    Task<ReturnOk<UsuarioResponse>> CadastrarNovoUsuario(CadastrarUsuarioRequest request, string userId);
    Task<ReturnOk<bool>> EditarUsuario(EditarUsuarioRequest request, string userId);
    Task<ReturnOk<bool>> RemoverUsuario(string usuarioRemoverId, string userId);
}