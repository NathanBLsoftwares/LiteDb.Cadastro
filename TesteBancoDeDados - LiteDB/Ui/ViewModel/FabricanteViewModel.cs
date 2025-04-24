using LiteDB;
using Microsoft.VisualStudio.Services.Common;
using System.Windows;
using TesteBancoDeDados___LiteDB.Domain.Model;
using TesteBancoDeDados___LiteDB.Domain.Model.Data;

namespace TesteBancoDeDados___LiteDB.Ui.ViewModel;

internal class FabricanteViewModel : Deriva
{
    private ILiteCollection<Fabricante> fabricantes;

    public FabricanteViewModel(ILiteCollection<Fabricante> fabricantes)
    {
        this.fabricantes = fabricantes;
    }

    private string nome;

    public string Nome { get => nome; set => SetProperty(ref nome, value); }

    private DelegateCommand salvar;
    public DelegateCommand Salvar =>
        salvar ?? (salvar = new DelegateCommand(ExecuteSalvar));

    void ExecuteSalvar()
    {
        if (string.IsNullOrEmpty(Nome))
        {
            MessageBox.Show("Nome Inválido");
            return;
        }
        if (fabricantes.Exists(x => x.Nome == Nome))
        {
            MessageBox.Show("Fabricante já Cadastrado");
            return;
        }

        fabricantes.Insert(new Fabricante { Nome = Nome });
    }
}
