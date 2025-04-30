using System.Collections.ObjectModel;
using System.Text;
using TesteBancoDeDados___LiteDB.Domain.Model.Data;
using TesteBancoDeDadosLiteDB.Domain.Model;

namespace TesteBancoDeDados___LiteDB.Ui.ViewModel;

internal class ItemLinhaViewModel : BindableBase
{

    private DelegateCommand<Domain.Library.Services.Dialog.IDialogService> salvarItemCadastrado;
    private ETipoItem tipoDoItemDaLinhaSelecionado;
    private Linha? linhaSelecionada;
    private string? nomeItemDalinha;


    public DelegateCommand<Domain.Library.Services.Dialog.IDialogService> SalvarItemCadastrado => salvarItemCadastrado ?? (salvarItemCadastrado = new DelegateCommand<Domain.Library.Services.Dialog.IDialogService>(SalvarItemCadastradoandName));
    public ObservableCollection<ETipoItem> TiposItemsEscolha { get; set; }
    public IEnumerable<ItemDaLinha>? ItemsDasLinhas { get; set; }
    public bool Editar { get; }


    public ETipoItem TipoDoItemDaLinhaSelecionado
    {
        get { return tipoDoItemDaLinhaSelecionado; }
        set {  SetProperty(ref tipoDoItemDaLinhaSelecionado, value); }
    }

    public Linha LinhaSelecionada
    {
        get
        {
            return linhaSelecionada!;
        }

        set
        {
            if (linhaSelecionada != value)
            {
                linhaSelecionada = value;
                RaisePropertyChanged(nameof(linhaSelecionada));
            }
        }
    }


    public string NomeItemDalinha
    {
        get
        {
            return nomeItemDalinha!;
        }
        set { SetProperty(ref nomeItemDalinha, value); }
    }


    #region CONSTRUTORA
    public ItemLinhaViewModel(IEnumerable<ItemDaLinha> _itemsDasLinhas, Linha _linha, bool _editar = false)
    {
        TiposItemsEscolha = new ObservableCollection<ETipoItem>(GetTipos());
        ItemsDasLinhas = _itemsDasLinhas;
        LinhaSelecionada = _linha;
        Editar = _editar;
    }
    #endregion CONSTRUTORA

    #region COMANDO SALVAR
    void SalvarItemCadastradoandName(Domain.Library.Services.Dialog.IDialogService service)
    {
        var ret = true;
        var sb = new StringBuilder();
        if (string.IsNullOrEmpty(NomeItemDalinha))
        {
            sb.AppendLine("O nome deve ser preenchido");
            ret = false;
        }
        if (!Editar && ItemsDasLinhas.Any(item => item.Nome == nomeItemDalinha))
        {
            sb.AppendLine("O nome já existe no contexto atual");
            ret = false;
        }
        service.DialogResult = true;
        service.Close();
    }

    private List<ETipoItem> GetTipos()
    {
        var lista = new List<ETipoItem>();
        foreach (ETipoItem tipoLinha in Enum.GetValues(typeof(ETipoItem)))
            lista.Add(tipoLinha);
        return lista;
    }
    #endregion COMANDO SALVAR
}
