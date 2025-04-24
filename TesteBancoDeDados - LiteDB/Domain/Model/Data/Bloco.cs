using LiteDB;

namespace TesteBancoDeDados___LiteDB.Domain.Model.Data;

internal class Bloco
{
    [BsonId]
    public int Id { get; set; }
    public string Nome { get; set; }
}
