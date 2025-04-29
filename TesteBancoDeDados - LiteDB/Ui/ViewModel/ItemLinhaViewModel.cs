using System.Text;
using TesteBancoDeDados___LiteDB.Domain.Model.Data;
using TesteBancoDeDadosLiteDB.Domain.Model;

namespace TesteBancoDeDados___LiteDB.Ui.ViewModel;

internal class ItemLinhaViewModel : BindableBase
{

    private DelegateCommand<Domain.Library.Services.Dialog.IDialogService> salvarItemCadastrado;
    private Linha? linhaSelecionada;
    private IEnumerable<ETipoItem> tipoDoItemDaLinhaSelecionado;
    private string? nomeItemDalinha;



    public DelegateCommand<Domain.Library.Services.Dialog.IDialogService> SalvarItemCadastrado => salvarItemCadastrado ?? (salvarItemCadastrado = new DelegateCommand<Domain.Library.Services.Dialog.IDialogService>(SalvarItemCadastradoandName));


    public IEnumerable<ItemDaLinha>? ItemsDasLinhas { get; }
    public bool Editar { get; }


    public IEnumerable<ETipoItem> TipoDoItemDaLinhaSelecionado
    {
        get { return tipoDoItemDaLinhaSelecionado; }
        set { SetProperty(ref tipoDoItemDaLinhaSelecionado, value); }
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
        TipoDoItemDaLinhaSelecionado = Enum.GetValues(typeof(ItemDaLinha)).Cast<ItemDaLinha>();
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
        if (!Editar && ItemsDasLinhas.Any(item => item.Nome == nomeItemDalinha ))
        {
            sb.AppendLine("O nome já existe no contexto atual");
            ret = false;
        }
        service.DialogResult = true;
        service.Close();
    }
    #endregion COMANDO SALVAR
}
