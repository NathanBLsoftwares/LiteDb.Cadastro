using TesteBancoDeDados___LiteDB.Domain.Model.Data;

namespace TesteBancoDeDadosLiteDB.Domain.Model.Wrapper
{
    internal class ItemWrapper : ModelWrapper<Item>
    {
        public ItemWrapper(Item model) : base(model)
        {
        }

        public int Id
        {
            get { return Model.Id; }
            set { SetValue<int>(value); }
        }
        public string Nome
        {
            get { return Model.Nome; }
            set { SetValue<string>(value); }
        }
        public ItemDaLinha ItemLinha
        {
            get { return Model.ItemLinha; }
            set { SetValue<ItemDaLinha>(value); }
        }
        public EDiametros Diametro
        {
            get { return Model.Diametro; }
            set { SetValue<EDiametros>(value); }
        }
        public Bloco Blocos
        {
            get { return Model.Blocos; }
            set { SetValue<Bloco>(value); }
        }
        public override string ToString()
        {
            return Nome;
        }
    }
}
