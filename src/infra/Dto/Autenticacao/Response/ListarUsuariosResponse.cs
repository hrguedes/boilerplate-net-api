using Base;

namespace Dto.Autenticacao.Response;

public class ListarUsuariosResponse : Pagination
{
    public List<UsuarioResponse> Data { get; set; }
}