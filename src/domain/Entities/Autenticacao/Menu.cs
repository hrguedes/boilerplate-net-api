using Entities.Base;

namespace Entities.Autenticacao;

public class Menu : BaseAudityEntity
{
    public string Nome { get; set; }
    public string Url { get; set; }
    public string Icone { get; set; }
    public string Regra { get; set; }
    public List<Menu> Items { get; set; }
    
    public Menu(string usuarioId) : base(usuarioId)
    {
    }
}