using Entities.Base;

namespace Entities.Autenticacao;

public class Tela : BaseAudityEntity
{
    public string Nome { get; set; }
    public string Caminho { get; set; }

    public Tela(string usuarioId) : base(usuarioId)
    {
    }
}