using Dto.Base;

namespace Dto.Autenticacao.Response;

public class UsuarioResponse : BaseDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string UsuarioDominio { get; set; }
    public string Login { get; set; }
    public List<RegraResponse> Regras { get; set; }
}