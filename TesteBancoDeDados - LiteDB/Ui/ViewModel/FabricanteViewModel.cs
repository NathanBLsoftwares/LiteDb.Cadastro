using LiteDB;
using System.Windows;
using TesteBancoDeDados___LiteDB.Domain.Model.Data;

namespace TesteBancoDeDados___LiteDB.Ui.ViewModel;

internal class FabricanteViewModel : BindableBase
{
    #region ATRIBUTOS E PROPRIEDADES

    private ILiteCollection<Fabricante> fabricantes;
    private Fabricante fabricanteSelecionado;
    private DelegateCommand<Domain.Library.Services.Dialog.IDialogService> salvar;
    private string nome;


    public bool SalvouComSucesso { get; private set; } = false;
    public DelegateCommand<Domain.Library.Services.Dialog.IDialogService> Salvar => salvar ?? (salvar = new DelegateCommand<Domain.Library.Services.Dialog.IDialogService>(ExecuteSalvar));
    public string Nome { get => nome; set => SetProperty(ref nome, value); }

    #endregion ATRIBUTOS E PROPRIEDADES

    #region CONSTRUTORA
    public FabricanteViewModel(ILiteCollection<Fabricante> fabricantes, Fabricante fabricante = null)
    {
        this.fabricantes = fabricantes;
        fabricanteSelecionado = fabricante;
    }
    #endregion CONSTRUTORA

    #region COMANDO SALVAR
    void ExecuteSalvar(Domain.Library.Services.Dialog.IDialogService service)
    {
        if (service == null)
        {
            service.DialogResult = false;
            return;
        }
        else
        {
            if (string.IsNullOrWhiteSpace(Nome))
            {
                MessageBox.Show("O campo Nomes deve ser preenchido", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (fabricantes.Exists(x => x.Nome == Nome))
            {
                MessageBox.Show("Fabricante já Cadastrado", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (fabricanteSelecionado != null)
            {
                fabricanteSelecionado.Nome = Nome;
                if (!fabricantes.Update(fabricanteSelecionado))
                {
                    MessageBox.Show("Erro ao alterar nome do Fabricante", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            else
            {
                fabricantes.Insert(new Fabricante { Nome = Nome });
            }

            SalvouComSucesso = true;
            service.DialogResult = true;
            service.Close();
        }
    }
    #endregion COMANDO SALVAR
}