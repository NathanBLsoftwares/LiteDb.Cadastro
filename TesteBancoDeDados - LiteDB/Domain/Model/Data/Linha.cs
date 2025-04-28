using LiteDB;
using TesteBancoDeDados___LiteDB.Mappers;

namespace TesteBancoDeDados___LiteDB.Domain.Model.Data;

internal class Linha
{
    [BsonId]
    public int Id { get; set; }
    public string Nome { get; set; }


    [BsonRef($"{MapDataBase.Grupo}")]
    public Grupo Grupo { get; set; }
    public override string ToString()
    {
        return Nome;
    }
}
