using System.Collections.ObjectModel;
using System.Text;
using TesteBancoDeDados___LiteDB.Domain.Model.Data;
using TesteBancoDeDadosLiteDB.Domain.Model;
using TesteBancoDeDadosLiteDB.Domain.Model.Wrapper;

namespace TesteBancoDeDadosLiteDB.Ui.ViewModel;

internal class ItemViewModel : BindableBase
{
    #region PROPRIEDADES
    private DelegateCommand<TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog.IDialogService>? adicionarDiametro;
    private DelegateCommand<TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog.IDialogService>? excluirBloco;
    private DelegateCommand<TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog.IDialogService>? adicionarBloco;
    private DelegateCommand<TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog.IDialogService>? salvarItem;
    private EDiametros diametroItem;
    private ItemDaLinha? itemDaLinhaSelecionada;
    private string? nomeItem;


    public DelegateCommand<TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog.IDialogService> AdicionarDiametro => adicionarDiametro ?? (adicionarDiametro = new DelegateCommand<TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog.IDialogService>(ExecuteAdicionarDiametro));
    public DelegateCommand<TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog.IDialogService> ExcluirBloco => excluirBloco ?? (excluirBloco = new DelegateCommand<TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog.IDialogService>(ExecuteExcluirBloco));
    public DelegateCommand<TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog.IDialogService> SalvarItem => salvarItem ?? (salvarItem = new DelegateCommand<TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog.IDialogService>(ExecuteSalvarItem));
    public DelegateCommand<TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog.IDialogService> AdicionarBloco => adicionarBloco ?? (adicionarBloco = new DelegateCommand<TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog.IDialogService>(ExecuteAdicionarBloco));


    public ObservableCollection<EDiametros> DiametrosItems { get; set; }
    public IEnumerable<Item> ListaDeItens { get; set; }
    public bool Editar { get; set; }


    public EDiametros DiametroItem
    {
        get { return diametroItem; }
        set { SetProperty(ref diametroItem, value); }
    }

    public ItemDaLinha ItemDaLinhaSelecionada
    {
        get { return itemDaLinhaSelecionada; }
        set
        {
            if (itemDaLinhaSelecionada != value)
            {
                itemDaLinhaSelecionada = value;
                SetProperty(ref itemDaLinhaSelecionada, value);
            }
        }
    }

    public string? NomeItem
    {
        get { return nomeItem; }
        set { SetProperty(ref nomeItem, value); }
    }
    #endregion PROPRIEDADES

    #region CONSTRUTOR
    public ItemViewModel(IEnumerable<Item> _listaDeItems, ItemDaLinha _itemDaLinha, bool _editar = false)
    {
        DiametrosItems = new ObservableCollection<EDiametros>(GetDiametros());
        ListaDeItens = _listaDeItems;
        ItemDaLinhaSelecionada = _itemDaLinha;
        Editar = _editar;
    }
    #endregion CONSTRUTOR

    #region COMANDOS
    void ExecuteAdicionarDiametro(TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog.IDialogService service1)
    {

    }
    void ExecuteExcluirBloco(TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog.IDialogService service2)
    {

    }
    void ExecuteAdicionarBloco(TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog.IDialogService service3)
    {

    }
    void ExecuteSalvarItem(TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog.IDialogService service4)
    {
        var ret = true;
        var sb = new StringBuilder();
        if (string.IsNullOrWhiteSpace(NomeItem))
        {
            sb.AppendLine("O nome deve ser preenchido");
            ret = false;
        }
        if(!Editar && ListaDeItens.Any(x => x.ItemLinha.Nome == NomeItem))
        {
            sb.AppendLine("Nome já cadastrado");
            ret = false;
        }
        service4.DialogResult = true;
        service4.Close();
    }
    private List<EDiametros> GetDiametros()
    {
        var lista = new List<EDiametros>();
        foreach (EDiametros diametro in Enum.GetValues(typeof(EDiametros)))
            lista.Add(diametro);
        return lista;
    }
    #endregion COMANDOS
}
