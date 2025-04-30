using TesteBancoDeDados___LiteDB.Domain.Model.Data;

namespace TesteBancoDeDadosLiteDB.Domain.Model.Wrapper
{
    internal class LinhaWrapper : ModelWrapper<Linha>
    {
        public LinhaWrapper(Linha model) : base(model)
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
        public Grupo Grupo
        {
            get { return Model.Grupo; }
            set { SetValue<Grupo>(value); }
        }
        public override string ToString()
        {
            return Nome;
        }
    }
}
