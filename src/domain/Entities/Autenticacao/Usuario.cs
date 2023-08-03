using Entities.Base;

namespace Entities.Autenticacao;

public class Usuario : BaseAudityEntity
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string UsuarioDominio { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
    public virtual List<Tela> Telas { get; set; }
    public virtual List<Regra> Regras { get; set; }

    public Usuario(string usuarioId) : base(usuarioId)
    {
        Telas = new List<Tela>();
        Regras = new List<Regra>();
    }
}