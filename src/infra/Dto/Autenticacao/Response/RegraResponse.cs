using Dto.Base;

namespace Dto.Autenticacao.Response;

public class RegraResponse : BaseDto
{
    public string Nome { get; set; }
    public string Chave { get; set; }
    public string Descricao { get; set; }
}