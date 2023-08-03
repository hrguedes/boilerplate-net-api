using Entities.Base;

namespace Entities.Autenticacao;

public class Sessao : BaseAudityEntity
{
    public DateTime Inicio { get; set; }
    public DateTime Termino { get; set; }
    public string Token { get; set; }

    public Sessao(string usuarioId) : base(usuarioId)
    {
    }
}