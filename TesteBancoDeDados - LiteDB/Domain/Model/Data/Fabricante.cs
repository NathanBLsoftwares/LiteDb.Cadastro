using LiteDB;

namespace TesteBancoDeDados___LiteDB.Domain.Model.Data
{
    public class Fabricante
    {
        [BsonId]
        public int Id { get; set; }
        public string Nome { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}
