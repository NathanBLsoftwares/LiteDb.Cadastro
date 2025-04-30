using TesteBancoDeDados___LiteDB.Domain.Model.Data;

namespace TesteBancoDeDadosLiteDB.Domain.Model.Wrapper
{
    internal class GrupoWrapper : ModelWrapper<Grupo>
    {
        public GrupoWrapper(Grupo model) : base(model)
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

        public Fabricante Fabricante
        {
            get { return Model.Fabricante; }
            set { SetValue<Fabricante>(value); }
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}
