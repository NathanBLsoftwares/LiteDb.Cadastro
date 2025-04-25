using LiteDB;
using System.Collections.ObjectModel;
using System.Windows;
using TesteBancoDeDados___LiteDB.Domain.Model.Data;
using TesteBancoDeDados___LiteDB.Ui.View;

namespace TesteBancoDeDados___LiteDB.Ui.ViewModel;

internal class CadastroLinhasViewModel : BindableBase
{

    private DelegateCommand botaoAdicionarFabricante;
    private DelegateCommand botaoExcluirFabricante;
    private DelegateCommand botaoEditarFabricante;
    private Fabricante fabricante;



    public LiteDatabase Db { get; private set; }
    public ILiteCollection<Fabricante> FabricantesDb { get; private set; }
    public ILiteCollection<Grupo> GruposDb { get; private set; }
    public ILiteCollection<Linha> LinhasDb { get; private set; }
    public ObservableCollection<Fabricante> Fabricantes { get; set; }
    public Fabricante Fabricante { get => fabricante; set => fabricante = value; }



    public DelegateCommand BotaoAdicionarFabricante => botaoAdicionarFabricante ?? (botaoAdicionarFabricante = new DelegateCommand(AdicionarFabricante));
    public DelegateCommand BotaoExcluirFabricante => botaoExcluirFabricante ?? (botaoExcluirFabricante = new DelegateCommand(ExcluirFabricante));
    public DelegateCommand BotaoEditarFabricante => botaoEditarFabricante ?? (botaoEditarFabricante = new DelegateCommand(EditarFabricante));



    #region CONSTRUTORA E DESTRUTORA
    public CadastroLinhasViewModel()
    {
        Fabricantes = new ObservableCollection<Fabricante>();
        Db = new LiteDatabase("Banco.db");
        CarregaFabricantes();
        GruposDb = Db.GetCollection<Grupo>(Mappers.MapDataBase.Grupo);
        LinhasDb = Db.GetCollection<Linha>(Mappers.MapDataBase.Linha);
    }

    private void CarregaFabricantes()
    {
        Fabricantes.Clear();
        Fabricante = null;
        FabricantesDb = Db.GetCollection<Fabricante>(Mappers.MapDataBase.Fabricante);
        if (FabricantesDb.Count() > 0)
        {
            foreach (var item in FabricantesDb.FindAll())
            {
                Fabricantes.Add(item);
            }
        }

        Fabricante = Fabricantes.FirstOrDefault();
    }

    ~CadastroLinhasViewModel()
    {
        Db.Dispose();
    }
    #endregion CONSTRUTORA E DESTRUTORA

    #region FABRICANTES
    void AdicionarFabricante()
    {
        var dc = new FabricanteViewModel(FabricantesDb);
        var dlg = new FabricanteView { DataContext = dc };
        dlg.ShowDialog();
        if (dc.SalvouComSucesso)
        {
            var novo = FabricantesDb.FindOne(x => x.Nome == dc.Nome);
            if (novo != null)
                Fabricantes.Add(novo);
        }
    }
    void ExcluirFabricante()
    {
        if (Fabricante == null) 
            return;

        var resultado = MessageBox.Show($"Deseja realmente excluir o fabricante '{Fabricante}'?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (resultado == MessageBoxResult.Yes)
        {
            FabricantesDb.Delete(Fabricante.Id);
            var fabricanteremovido = Fabricantes.FirstOrDefault(x => x.Nome == Fabricante.Nome);
            if(fabricanteremovido != null)
            {
                Fabricantes.Remove(fabricanteremovido);
            }
            Fabricante = null;
        }
    }


    void EditarFabricante()
    {
        if(Fabricante == null)
        {
            return;
        }

        var dc = new FabricanteViewModel(FabricantesDb, Fabricante)
        {
            Nome = Fabricante.Nome,
        };
        var dlg = new FabricanteView {DataContext = dc };
        if (dlg.ShowDialog() != true)
            return;

        CarregaFabricantes();
    }

    #endregion FABRICANTES
}
