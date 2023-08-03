namespace Dto.Autenticacao.Request;

public class CadastrarUsuarioRequest
{
    public string? Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? UsuarioDominio { get; set; }
    public List<string> Regras { get; set; }
    public List<string> Telas { get; set; }
}