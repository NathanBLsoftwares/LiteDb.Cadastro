using LiteDB;

namespace TesteBancoDeDados___LiteDB.Domain.Model.Data;

internal class DiametroTipo
{
    [BsonId]
    public int Id { get; set; }
    public string Descricao { get; set; }
    public override string ToString()
    {
        return Descricao;
    }
}
