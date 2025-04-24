using LiteDB;
using TesteBancoDeDados___LiteDB.Mappers;

namespace TesteBancoDeDados___LiteDB.Domain.Model.Data;

internal class ItemDaLinha
{
    [BsonId]
    public int Id { get; set; }
    public string Nome { get; set; }
    [BsonRef($"{MapDataBase.Linha}")]
    public Linha Linha { get; set; }
}
