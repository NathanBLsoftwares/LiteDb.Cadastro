using LiteDB;
using System.Windows;
using TesteBancoDeDados___LiteDB.Domain.Model.Data;

namespace TesteBancoDeDados___LiteDB.Ui.ViewModel;

internal class LinhaViewModel : BindableBase
{
    #region ATRIBUTOS E PROPRIEDADES
    private ILiteCollection<Linha>? linhaDB;
    private DelegateCommand<Domain.Library.Services.Dialog.IDialogService> salvarLinha;
    private Linha? linhaSelecionada;
    private Grupo grupoSelecionado;
    private string? nomeLinha;


    public DelegateCommand<Domain.Library.Services.Dialog.IDialogService> SalvarLinha => salvarLinha ?? (salvarLinha = new DelegateCommand<Domain.Library.Services.Dialog.IDialogService>(VSalvarLinha));
    public bool SalvouLinhaCoMSucesso { get; private set; } = false;
    public Grupo GrupoSelecionado { get { return grupoSelecionado; } set { SetProperty(ref grupoSelecionado, value); } }
    public string NomeLinha { get => nomeLinha!; set => SetProperty(ref nomeLinha, value); }
    #endregion ATRIBUTOS E PROPRIEDADES

    #region CONSTRUTORA
    public LinhaViewModel(ILiteCollection<Linha> _LinhasDB, Grupo _grupo, Linha _Linha = null)
    {
        linhaDB = _LinhasDB;
        GrupoSelecionado = _grupo;
        linhaSelecionada = _Linha;
    }
    #endregion CONSTRUTORA

    #region COMANDO SALVAR
    void VSalvarLinha(Domain.Library.Services.Dialog.IDialogService service)
    {
        if (service == null)
            return;
        else
        {
            if (string.IsNullOrEmpty(nomeLinha))
            {
                MessageBox.Show("O campo Nome deve ser preenchido", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (linhaDB!.Exists(linha1 => linha1.Nome == nomeLinha))
            {
                MessageBox.Show("Linha já cadastrada", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (linhaSelecionada != null)
            {
                linhaSelecionada.Nome = nomeLinha;
                if (!linhaDB.Update(linhaSelecionada))
                {
                    MessageBox.Show("Erro ao alterar o nome da linha", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            else
            {
                var linha = new Linha();
                linha.Grupo = GrupoSelecionado;
                linha.Nome = nomeLinha;
                linhaDB.Insert(linha);

            }
            SalvouLinhaCoMSucesso = true;
            service.DialogResult = true;
            service.Close();
        }
    }
    #endregion COMANDO SALVAR
}
