using LiteDB;
using TesteBancoDeDados___LiteDB.Mappers;

namespace TesteBancoDeDados___LiteDB.Domain.Model.Data
{
    internal class Grupo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [BsonRef ($"{MapDataBase.Fabricante}")]
        public Fabricante Fabricante { get; set; }
        public override string ToString()
        {
            if( Fabricante != null) 
            {
                return $"{Fabricante} -> {Nome}";
            }
            return Nome;
        }
    }
}
