using System.Text;
using System.Windows;
using TesteBancoDeDados___LiteDB.Domain.Model.Data;

namespace TesteBancoDeDados___LiteDB.Ui.ViewModel;

internal class FabricanteViewModel : BindableBase
{
    #region ATRIBUTOS E PROPRIEDADES
    public DelegateCommand<Domain.Library.Services.Dialog.IDialogService> Salvar => salvar ?? (salvar = new DelegateCommand<Domain.Library.Services.Dialog.IDialogService>(ExecuteSalvar));
    private DelegateCommand<Domain.Library.Services.Dialog.IDialogService> salvar;


    private string nome;


    public IEnumerable<Fabricante> Fabricantes { get; }
    public bool Editar { get; }


    public string Nome
    {
        get
        {
            return nome;
        }
        set => SetProperty(ref nome, value);
    }
    #endregion ATRIBUTOS E PROPRIEDADES

    #region CONSTRUTORA
    public FabricanteViewModel(IEnumerable<Fabricante> fabricantes, bool editar = false)
    {
        Fabricantes = fabricantes;
        Editar = editar;
    }
    #endregion CONSTRUTORA

    #region COMANDO SALVAR
    void ExecuteSalvar(Domain.Library.Services.Dialog.IDialogService service)
    {
        var ret = true;
        var sb = new StringBuilder();
        if (string.IsNullOrWhiteSpace(Nome))
        {
            sb.AppendLine("O campo Nomes deve ser preenchido!");
            ret = false;
        }
        if (!Editar && Fabricantes.Any(x => x.Nome == Nome))
        {
            sb.AppendLine("Fabricante já Cadastrado!");
            ret = false;
        }

        if (!ret)
        {
            MessageBox.Show(sb.ToString(), "Erros", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        service.DialogResult = true;
        service.Close();
    }
    #endregion COMANDO SALVAR
}