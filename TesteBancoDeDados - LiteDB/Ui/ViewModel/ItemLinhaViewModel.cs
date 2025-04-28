using LiteDB;
using System.Windows;
using TesteBancoDeDados___LiteDB.Domain.Model.Data;

namespace TesteBancoDeDados___LiteDB.Ui.ViewModel;

internal class ItemLinhaViewModel : BindableBase
{
    #region ATRIBUTOS E PROPRIEDADES
    private ILiteCollection<ItemDaLinha>? itemDaLinhaDB;
    private DelegateCommand<Domain.Library.Services.Dialog.IDialogService> salvarItemCadastrado;
    private ItemDaLinha? itemDaLinhaSelecionado;
    private Linha? linhaSelecionada;
    private string? nomeItemDalinha;
    




    public ILiteCollection<ItemDaLinha> ItemDaLinhaDB { get { return itemDaLinhaDB!; } set { SetProperty(ref itemDaLinhaDB, value); } }
    public Linha LinhaSelecionada { get { return linhaSelecionada!; } set { SetProperty(ref linhaSelecionada, value); } }
    public string NomeItemDalinha { get { return nomeItemDalinha!; } set { SetProperty(ref nomeItemDalinha, value); } }
    public ItemDaLinha ItemDaLinhaSelecionado { get { return itemDaLinhaSelecionado!; } set { SetProperty(ref itemDaLinhaSelecionado, value); } }
    public bool SalvouItemLinhaCoMSucesso { get; private set; } = false;
    public DelegateCommand<Domain.Library.Services.Dialog.IDialogService> CommSalvarItemCadastradoandName => salvarItemCadastrado ?? (salvarItemCadastrado = new DelegateCommand<Domain.Library.Services.Dialog.IDialogService>(SalvarItemCadastradoandName));
    #endregion ATRIBUTOS E PROPRIEDADES


    #region CONSTRUTORA
    public ItemLinhaViewModel(ILiteCollection<ItemDaLinha> _ItemDaLinhaDB, Linha _LinhaPertencente, ItemDaLinha _ItemDaLinha = null)
    {
        ItemDaLinhaDB = _ItemDaLinhaDB;
        LinhaSelecionada = _LinhaPertencente;
        ItemDaLinhaSelecionado = _ItemDaLinha;

    }
    #endregion CONSTRUTORA

    #region COMANDO SALVAR
    void SalvarItemCadastradoandName(Domain.Library.Services.Dialog.IDialogService service)
    {
        if (service == null)
            return;
        else
        {
            if (string.IsNullOrEmpty(NomeItemDalinha))
            {
                MessageBox.Show("Todos os campos devem ser preenchidos", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if(ItemDaLinhaDB.Exists(item => item.Nome == nomeItemDalinha))
            {
                MessageBox.Show("O item já existe", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if(ItemDaLinhaSelecionado != null)
            {
                ItemDaLinhaSelecionado.Nome = NomeItemDalinha;
                if (!ItemDaLinhaDB.Update(ItemDaLinhaSelecionado))
                {
                    MessageBox.Show("Erro ao alterar o nome do Item", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            else
            {
                var itemLinha = new ItemDaLinha();
                itemLinha.Linha = linhaSelecionada;
                itemLinha.Nome = nomeItemDalinha;
                ItemDaLinhaDB.Insert(itemLinha);
            }
            SalvouItemLinhaCoMSucesso = true;
            service.DialogResult = true;
            service.Close();

        }
    }
    #endregion COMANDO SALVAR
}
