using Dto.Base;

namespace Dto.Autenticacao.Response;

public class MenuResponse : BaseDto
{
    public string Nome { get; set; }
    public string Url { get; set; }
    public string Icone { get; set; }
    public string Regra { get; set; }
    public List<MenuResponse> Items { get; set; }
}