using System.Text;
using TesteBancoDeDados___LiteDB.Domain.Model.Data;
using TesteBancoDeDadosLiteDB.Domain.Model.Wrapper;

namespace TesteBancoDeDados___LiteDB.Ui.ViewModel;

internal class GrupoViewModel : BindableBase
{
    #region PROPRIEDADES E ATRIBUTOS


    private DelegateCommand<Domain.Library.Services.Dialog.IDialogService> salvar;
    private FabricanteWrapper fabricante;
    private string nomeGrupo;


    public DelegateCommand<Domain.Library.Services.Dialog.IDialogService> Salvar => salvar ?? (salvar = new DelegateCommand<Domain.Library.Services.Dialog.IDialogService>(SalvarGrupos));
    public IEnumerable<Grupo> Grupos { get; }
    public bool Editar { get; }


    public string NomeGrupo
    {
        get
        {
            return nomeGrupo;
        }
        set => SetProperty(ref nomeGrupo, value);
    }
    public FabricanteWrapper FabricanteSelecionado
    {
        get
        {
            return fabricante;
        }

        set
        {
            if (fabricante != value)
            {
                fabricante = value;
                RaisePropertyChanged(nameof(FabricanteSelecionado));
            }
        }
    }

    #endregion PROPRIEDADES E ATRIBUTOS

    #region CONSTRUTORA
    public GrupoViewModel(FabricanteWrapper fabricante,IEnumerable<Grupo> grupos, bool editar = false)
    {
        Editar = editar;
        Grupos = grupos;
        FabricanteSelecionado = fabricante;
    }
    #endregion CONSTRUTORA

    #region COMANDO SALVAR
    private void SalvarGrupos(Domain.Library.Services.Dialog.IDialogService service)
    {
        var ret = true;
        var sb = new StringBuilder();
        if (string.IsNullOrEmpty(NomeGrupo))
        {
            sb.AppendLine("O campo Nome deve ser preenchido");
            return;
        }
        if (!Editar && Grupos.Any(x => x.Nome == NomeGrupo))
        {
            sb.AppendLine($"{NomeGrupo} já existe no contexto atual");
            return;
        }
        service.DialogResult = true;
        service.Close();
    }
    #endregion COMANDO SALVAR
}
