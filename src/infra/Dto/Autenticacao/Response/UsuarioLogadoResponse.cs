namespace Dto.Autenticacao.Response;

public class UsuarioLogadoResponse
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string UsuarioWindows { get; set; }
    public string Token { get; set; }
    public DateTime ValidoAte { get; set; }
    public string SessaoId { get; set; }
    public List<RegraResponse> Regras { get; set; }
    public List<TelaResponse> Telas { get; set; }
}