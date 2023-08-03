namespace Dto.Autenticacao.Request;

public class EditarUsuarioRequest
{
    public string? Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? UsuarioDominio { get; set; }
}