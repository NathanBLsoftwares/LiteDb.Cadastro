using LiteDB;
using TesteBancoDeDados___LiteDB.Domain.Model.Data;
using TesteBancoDeDados___LiteDB.Mappers;

namespace TesteBancoDeDadosLiteDB.Domain.Model.Wrapper
{
    internal class ItemDaLinhaWrapper : ModelWrapper<ItemDaLinha>
    {
        public ItemDaLinhaWrapper(ItemDaLinha model) : base(model)
        {
        }

        [BsonId]
        public int Id
        {
            get { return Model.Id; }
            set { SetValue<int>(value); }
        }
        public string? Nome
        {
            get { return Model.Nome; }
            set { SetValue<string>(value); }
        }
        public ETipoItem TipoDeItem
        {
            get { return Model.TipoDeItem; }
            set { SetValue<ETipoItem>(value); }
        }
        public Linha? Linha
        {
            get { return Model.Linha; }
            set { SetValue<Linha>(value); }
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}
