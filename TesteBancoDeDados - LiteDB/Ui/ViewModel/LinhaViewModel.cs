using System.Text;
using TesteBancoDeDados___LiteDB.Domain.Model.Data;

namespace TesteBancoDeDados___LiteDB.Ui.ViewModel;

internal class LinhaViewModel : BindableBase
{
    #region ATRIBUTOS E PROPRIEDADES
    private DelegateCommand<Domain.Library.Services.Dialog.IDialogService> salvarLinha;
    private Grupo grupoSelecionado;
    private string? nomeLinha;


    public DelegateCommand<Domain.Library.Services.Dialog.IDialogService> SalvarLinha => salvarLinha ?? (salvarLinha = new DelegateCommand<Domain.Library.Services.Dialog.IDialogService>(VSalvarLinha));
    public IEnumerable<Linha> Linhas { get; }
    public bool Editar { get; }

    public Grupo GrupoSelecionado
    {
        get
        {
            return grupoSelecionado;
        }

        set
        {
            if(grupoSelecionado != value)
            {
                grupoSelecionado = value;
                RaisePropertyChanged(nameof(GrupoSelecionado));
            }
        }
    }
    public string NomeLinha
    {
        get
        {
            return nomeLinha!;
        }

        set
        {
            SetProperty(ref nomeLinha, value);
        }
    }
    #endregion ATRIBUTOS E PROPRIEDADES

    #region CONSTRUTORA
    public LinhaViewModel(IEnumerable<Linha> linhas, Grupo _grupo, bool editar = false)
    {
        Linhas = linhas;
        GrupoSelecionado = _grupo;
        Editar = editar;
    }
    #endregion CONSTRUTORA

    #region COMANDO SALVAR
    void VSalvarLinha(Domain.Library.Services.Dialog.IDialogService service)
    {
        var ret = true;
        var sb = new StringBuilder();
        if (string.IsNullOrEmpty(nomeLinha))
        {
            sb.AppendLine("O campo nome deve ser preenchido");
            ret = false;
        }
        if (!Editar && Linhas!.Any(linha1 => linha1.Nome == nomeLinha))
        {
            sb.AppendLine($"{NomeLinha} já existe no contexto atual!");
            ret = false;
        }
        service.DialogResult = true;
        service.Close();
    }
    #endregion COMANDO SALVAR
}
