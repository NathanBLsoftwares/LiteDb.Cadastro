using TesteBancoDeDados___LiteDB.Domain.Model.Data;

namespace TesteBancoDeDadosLiteDB.Domain.Model.Wrapper
{
    internal class FabricanteWrapper : ModelWrapper<Fabricante>
    {
        public FabricanteWrapper(Fabricante model) : base(model)
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

        public override string ToString()
        {
            return Nome;
        }
    }
}
