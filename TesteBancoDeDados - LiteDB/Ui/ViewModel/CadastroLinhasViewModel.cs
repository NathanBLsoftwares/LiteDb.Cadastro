using LiteDB;
using System.Collections.ObjectModel;
using System.Windows;
using TesteBancoDeDados___LiteDB.Domain.Model.Data;
using TesteBancoDeDados___LiteDB.Ui.View;

namespace TesteBancoDeDados___LiteDB.Ui.ViewModel;

internal class CadastroLinhasViewModel : BindableBase
{
    #region ATRIBUTOS FABRICANTE
    private DelegateCommand botaoAdicionarFabricante;
    private DelegateCommand botaoExcluirFabricante;
    private DelegateCommand botaoEditarFabricante;
    private Fabricante fabricante;



    public LiteDatabase Db { get; private set; }
    public ILiteCollection<Fabricante> FabricantesDb { get; private set; }
    public ILiteCollection<Linha> LinhasDb { get; private set; }
    public ObservableCollection<Fabricante> Fabricantes { get; set; }
    public Fabricante Fabricante { get => fabricante; set => fabricante = value; }



    public DelegateCommand BotaoAdicionarFabricante => botaoAdicionarFabricante ?? (botaoAdicionarFabricante = new DelegateCommand(AdicionarFabricante));
    public DelegateCommand BotaoExcluirFabricante => botaoExcluirFabricante ?? (botaoExcluirFabricante = new DelegateCommand(ExcluirFabricante));
    public DelegateCommand BotaoEditarFabricante => botaoEditarFabricante ?? (botaoEditarFabricante = new DelegateCommand(EditarFabricante));

    #endregion ATRIBUTOS FABRICANTE

    #region ATRIBUTOS GRUPO
    private DelegateCommand botaoAdicionarGrupo;
    private DelegateCommand botaoExcluirGrupo;
    private DelegateCommand botaoEditarGrupo;
    private Grupo grupo;


    public ILiteCollection<Grupo> GruposDb { get; private set; }
    public ObservableCollection<Grupo> Grupos { get; private set; }
    public Grupo Grupo { get => grupo; set => grupo = value; }


    public DelegateCommand BotaoAdicionarGrupo => botaoAdicionarGrupo ?? (botaoAdicionarGrupo = new DelegateCommand(AdicionarGrupo));
    public DelegateCommand BotaoExcluirGrupo => botaoExcluirGrupo ?? (botaoExcluirGrupo = new DelegateCommand(ExcluirGrupo));
    public DelegateCommand BotaoEditarGrupo => botaoEditarGrupo ?? (botaoEditarGrupo = new DelegateCommand(EditarGrupo));
    #endregion ATRIBUTO GRUPO


    #region CONSTRUTORA E DESTRUTORA
    public CadastroLinhasViewModel()
    {
        Fabricantes = new ObservableCollection<Fabricante>();
        Grupos = new ObservableCollection<Grupo>();
        Db = new LiteDatabase("Banco.db");
        CarregaFabricantesDB();
        CarregarGruposDB();
        LinhasDb = Db.GetCollection<Linha>(Mappers.MapDataBase.Linha);
    }

    ~CadastroLinhasViewModel()
    {
        Db.Dispose();
    }
    #endregion CONSTRUTORA E DESTRUTORA

    #region CARREGAR BANCO DE DADOS
    private void CarregaFabricantesDB()
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

    private void CarregarGruposDB()
    {
        Grupos.Clear();
        Grupo = null!;
        GruposDb = Db.GetCollection<Grupo>(Mappers.MapDataBase.Grupo);
        if(GruposDb.Count() > 0)
        {
            foreach(var item in GruposDb.FindAll())
            {
                Grupos.Add(item);
            }        
        }
        Grupo = Grupos.FirstOrDefault();
    }
    #endregion CARREGAR BANCO DE DADOS


    #region COMANDOS FABRICANTES

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

        CarregaFabricantesDB();
    }


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

    #endregion COMANDOS FABRICANTES

    #region COMANDOS GRUPOS
    void AdicionarGrupo()
    {
        var dc = new GrupoViewModel(GruposDb);
        var dlg = new GrupoView { DataContext = dc };
        dlg.ShowDialog();
        if (dc.SalvouGrupoComSuceso)
        {
            var novoGrupo = GruposDb.FindOne(x => x.Nome == dc.NomeGrupo);
            if(novoGrupo != null)
                Grupos.Add(novoGrupo);
        }
    }
  
    void EditarGrupo()
    {
        if( Grupo == null)
        {
            return;
        }
        var dc = new GrupoViewModel(GruposDb, Grupo!)
        {
            NomeGrupo = Grupo!.Nome
        };
        var dlg = new GrupoView { DataContext = dc };
        if (dlg.ShowDialog() != true)
            return;
        CarregarGruposDB();
    }

    
    

    void ExcluirGrupo()
    {
        if (Grupo == null)
            return;
        var resultadoGrupoExcluir = MessageBox.Show($"Tem certeza que deseja excluir '{Grupo}'?", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if( resultadoGrupoExcluir == MessageBoxResult.Yes)
        {
            GruposDb.Delete(Grupo.Id);
            var grupoRemovido = Grupos.FirstOrDefault(x => x.Nome == Grupo.Nome);
            if(grupoRemovido != null)
            {
                Grupos.Remove(grupoRemovido);
            }
            Grupo = null;
        }
    }
    #endregion COMANDOS GRUPOS
}
