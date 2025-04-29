using LiteDB;
using TesteBancoDeDados___LiteDB.Mappers;
using TesteBancoDeDadosLiteDB.Domain.Model;

namespace TesteBancoDeDados___LiteDB.Domain.Model.Data
{
    internal class ItemDaLinha
    {
        [BsonId]
        public int Id { get; set; }
        public string? Nome { get; set; }

        public ETipoItem TipoDeItem { get; set; }

        [BsonRef($"{MapDataBase.Linha}")]
        public Linha? Linha { get; set; }


        public override string ToString()
        {
            return Nome;
        }
    }
}


