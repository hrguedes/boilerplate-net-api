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
    public virtual List<Regra> Regras { get; set; }
    
    public virtual List<Menu> Menus { get; set; }

    public Usuario(string usuarioId) : base(usuarioId)
    {
        Regras = new List<Regra>();
    }
}