using Base;

namespace Dto.Autenticacao.Response;

public class ListarTelasResponse : Pagination
{
    public List<TelaResponse> Data { get; set; }
}