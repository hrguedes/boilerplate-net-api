using Entities.Base;

namespace Entities.Autenticacao;

public class Regra : BaseAudityEntity
{
    public string Nome { get; set; }
    public string Chave { get; set; }
    public string Descricao { get; set; }

    public Regra(string usuarioId) : base(usuarioId)
    {
    }
}