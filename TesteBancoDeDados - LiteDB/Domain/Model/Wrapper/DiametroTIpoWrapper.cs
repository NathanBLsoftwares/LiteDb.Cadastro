using TesteBancoDeDados___LiteDB.Domain.Model.Data;

namespace TesteBancoDeDadosLiteDB.Domain.Model.Wrapper
{
    internal class DiametroTIpoWrapper : ModelWrapper<DiametroTipo>
    {
        public DiametroTIpoWrapper(DiametroTipo model) : base(model)
        {
        }

        public int Id
        {
            get { return Model.Id; }
            set { SetValue<int>(value); }
        }
        public string Descricao
        {
            get { return Model.Descricao; }
            set { SetValue<string>(value); }
        }
        public override string ToString()
        {
            return Descricao;
        }
    }
}
