using Dto.Base;

namespace Dto.Autenticacao.Response;

public class TelaResponse : BaseDto
{
    public string Nome { get; set; }
    public string Caminho { get; set; }
}