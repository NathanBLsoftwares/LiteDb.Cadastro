using LiteDB;
using System.Collections.ObjectModel;
using TesteBancoDeDados___LiteDB.Domain.Model.Data;
using TesteBancoDeDados___LiteDB.Ui.View;

namespace TesteBancoDeDados___LiteDB.Ui.ViewModel;

internal class CadastroLinhasViewModel : BindableBase
{
    public LiteDatabase Db { get; private set; }
    public ILiteCollection<Fabricante> FabricantesDb { get; private set; }
    public ILiteCollection<Grupo> GruposDb { get; private set; }
    public ILiteCollection<Linha> LinhasDb { get; private set; }
    public ObservableCollection<Fabricante> Fabricantes { get; set; }

    public CadastroLinhasViewModel()
    {
        Fabricantes = new ObservableCollection<Fabricante>();
        Db = new LiteDatabase("Banco.db");
        FabricantesDb = Db.GetCollection<Fabricante>(Mappers.MapDataBase.Fabricante);
        if(FabricantesDb.Count() > 0)
        {
            foreach(var item in FabricantesDb.FindAll())
            {
                Fabricantes.Add(item);
            }
        }
        GruposDb = Db.GetCollection<Grupo>(Mappers.MapDataBase.Grupo);
        LinhasDb = Db.GetCollection<Linha>(Mappers.MapDataBase.Linha);

    }
    ~CadastroLinhasViewModel()
    {
        Db.Dispose();
    }

    private DelegateCommand botaoAdicionarFabricante;
    public DelegateCommand BotaoAdicionarFabricante =>
        botaoAdicionarFabricante ?? (botaoAdicionarFabricante = new DelegateCommand(ExecuteBotaoAdicionarFabricante));

    void ExecuteBotaoAdicionarFabricante()
    {
        var dc = new FabricanteViewModel(FabricantesDb);
        var dlg = new FabricanteView { DataContext = dc };
        dlg.ShowDialog();
    }
}
