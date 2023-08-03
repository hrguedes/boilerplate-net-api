using Base;

namespace Dto.Autenticacao.Response;

public class ListarRegrasResponse : Pagination
{
    public List<RegraResponse> Data { get; set; }

}