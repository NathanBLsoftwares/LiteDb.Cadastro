using LiteDB;
using TesteBancoDeDados___LiteDB.Mappers;

namespace TesteBancoDeDados___LiteDB.Domain.Model.Data
{
    public class Fabricante
    {
        [BsonId]
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
