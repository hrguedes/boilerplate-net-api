using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Base;

public class BaseAudityEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public DateTime DataCricao { get; set; }
    public DateTime? DataRemovido { get; set; }
    public DateTime DataAtualizacao { get; set; }
    public bool RegistroRemovido { get; set; }
    public string? UsuarioCriacaoId { get; set; }
    public string? UsuarioAtualizacaoId { get; set; }

    public BaseAudityEntity(string usuarioId)
    {
        if (Id != null)
        {
            UsuarioAtualizacaoId = usuarioId;
            DataAtualizacao = DateTime.Now;
        }
        else
        {
            UsuarioCriacaoId = usuarioId;
            RegistroRemovido = false;
            DataAtualizacao = DateTime.Now;
            DataCricao = DateTime.Now;
        }
    }
}