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
    
    public ObservableCollection<Fabricante> Fabricantes { get; set; }
    public Fabricante Fabricante
    {
        get { return fabricante; }
        set 
        { 
            SetProperty(ref fabricante, value); 
            if (value != null)
            {
                CarregarGruposDB(value);
            }
        }
    }


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
    public Grupo Grupo
    
    {
        get { return grupo; }
        set 
        { 
            SetProperty(ref grupo, value);
            CarregarLinhasDB(value);
        }
    }

    public DelegateCommand BotaoAdicionarGrupo => botaoAdicionarGrupo ?? (botaoAdicionarGrupo = new DelegateCommand(AdicionarGrupo));
    public DelegateCommand BotaoExcluirGrupo => botaoExcluirGrupo ?? (botaoExcluirGrupo = new DelegateCommand(ExcluirGrupo));
    public DelegateCommand BotaoEditarGrupo => botaoEditarGrupo ?? (botaoEditarGrupo = new DelegateCommand(EditarGrupo));
    #endregion ATRIBUTO GRUPO

    #region ATRIBUTOS LINHA
    private DelegateCommand botaoAdicionarLinha;
    private DelegateCommand botaoExcluirLinha;
    private DelegateCommand botaoEditarLinha;
    private Linha linha;


    public ILiteCollection<Linha> LinhasDb { get; private set; }
    public ObservableCollection<Linha> Linhas { get; private set; }
    

    public Linha Linha
    {
        get
        {
            return linha;
        }

        set
        {
            linha = value;
            CarregarItemsDaLinhaDB();
        }
    }


    public DelegateCommand BotaoAdicionarLinha => botaoAdicionarLinha ?? (botaoAdicionarLinha = new DelegateCommand(AdicionarLinha));
    public DelegateCommand BotaoEditarLinha => botaoEditarLinha ?? (botaoEditarLinha = new DelegateCommand(EditarLinha));
    public DelegateCommand BotaoExcluirLinha => botaoExcluirLinha ?? (botaoExcluirLinha = new DelegateCommand(ExcluirLinha));


    #endregion ATRIBUTOS LINHA

    #region ATRIBUTO ITEM LINHA
    private DelegateCommand adicionarItemLinha;
    private DelegateCommand excluirItemLinha;
    private ItemDaLinha itemLinha;


    public ILiteCollection<ItemDaLinha> ItemDaLinhaDB { get; private set; }
    public ItemDaLinha ItemLinha
    {
        get { return itemLinha; }
        set { SetProperty(ref itemLinha, value); }
    }
    public ObservableCollection<ItemDaLinha> ItemsLinhas {  get; private set; }
    public DelegateCommand AdicionarItemLinha => adicionarItemLinha ?? (adicionarItemLinha = new DelegateCommand(MAdicionarItemLinha));
    public DelegateCommand ExcluirItemLinha => excluirItemLinha ?? (excluirItemLinha = new DelegateCommand(MExcluirItemLinha));
    #endregion ATRIBUTO ITEM LINHA




    #region CONSTRUTORA E DESTRUTORA
    public CadastroLinhasViewModel()
    {
        Fabricantes = new ObservableCollection<Fabricante>();
        Grupos = new ObservableCollection<Grupo>();
        Linhas = new ObservableCollection<Linha>();
        ItemsLinhas = new ObservableCollection<ItemDaLinha>();
        Db = new LiteDatabase("Banco.db");
        CarregaFabricantesDB();
        CarregarGruposDB();
        CarregarLinhasDB();
        CarregarItemsDaLinhaDB();
    }

    ~CadastroLinhasViewModel()
    {
        Db.Dispose();
    }
    #endregion CONSTRUTORA E DESTRUTORA

    #region CARREGAR BANCO DE DADOS

    public void CarregarItemsDaLinhaDB(Linha _linhaSelecionada = null)
    {
        ItemsLinhas.Clear();
        ItemLinha = null;
        ItemDaLinhaDB = Db.GetCollection<ItemDaLinha>(Mappers.MapDataBase.ItemLinha);
        if(ItemDaLinhaDB.Count() > 0)
        {
            foreach(var item in ItemDaLinhaDB.FindAll())
            {
                if(_linhaSelecionada != null && item.Linha.Id == _linhaSelecionada.Id)
                {
                    ItemsLinhas.Add(item);
                }
            }
        }
;   }
    public void CarregarLinhasDB(Grupo grupo = null)
    {
        Linhas.Clear();
        Linha = null;
        LinhasDb = Db.GetCollection<Linha>(Mappers.MapDataBase.Linha);
        if(LinhasDb.Count() > 0)
        {
            foreach(var item in LinhasDb.FindAll())
            {
                if (grupo != null && item.Grupo.Id == grupo.Id)
                {
                    Linhas.Add(item);
                }
            }
        }
    }

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

    private void CarregarGruposDB(Fabricante fabricante = null)
    {
        Grupos.Clear();
        Grupo = null!;
        GruposDb = Db.GetCollection<Grupo>(Mappers.MapDataBase.Grupo);
        if(GruposDb.Count() > 0)
        {
            foreach(Grupo item in GruposDb.FindAll())
            {
                if (fabricante != null && item.Fabricante.Id == fabricante.Id)
                {
                    Grupos.Add(item);
                }
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
    
    void EditarGrupo()
    {
        if( Grupo == null)
        {
            return;
        }
        var dc = new GrupoViewModel(GruposDb, Fabricante, Grupo!)
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

    void AdicionarGrupo()
    {
        if (Fabricante == null)
        {
            MessageBox.Show("Selecione um Fabricante", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        var dc = new GrupoViewModel(GruposDb, Fabricante);
        var dlg = new GrupoView { DataContext = dc };
        dlg.ShowDialog();
        if (dc.SalvouGrupoComSuceso)
        {
            var novoGrupo = GruposDb.FindOne(x => x.Nome == dc.NomeGrupo);
            if (novoGrupo != null)
                Grupos.Add(novoGrupo);
        }
    }

    #endregion COMANDOS GRUPOS

    #region COMANDOS LINHA
    void AdicionarLinha()
    {
        if(Grupo == null)
        {
            MessageBox.Show("Selecione um Grupo", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        var dc = new LinhaViewModel(LinhasDb, Grupo);
        var dlg = new LinhaView {  DataContext = dc };
        dlg.ShowDialog();
        if (dc.SalvouLinhaCoMSucesso)
        {
            var novaLinha = LinhasDb.FindOne(linha => linha.Nome == dc.NomeLinha);
            if (novaLinha != null)
                Linhas.Add(novaLinha);
        }
    }

    void EditarLinha()
    {
        if (Linha == null)
            return;
        var dc = new LinhaViewModel(LinhasDb, Grupo, Linha)
        {
            NomeLinha = Linha.Nome
        };
        var dlg = new LinhaView { DataContext = dc };
        if (dlg.ShowDialog() != true)
            return;
        CarregarLinhasDB();
    }

    void ExcluirLinha()
    {
        if (Linha == null)
            return;
        var resultadoExcluirLinha = MessageBox.Show($"Tem certeza que deseja Excluir a linha '{Linha}'?", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if(resultadoExcluirLinha == MessageBoxResult.Yes)
        {
            LinhasDb.Delete(Linha.Id);
            var linhaRemovida = Linhas.FirstOrDefault(x => x.Nome == Linha.Nome);
            if(linhaRemovida != null)
            {
                Linhas.Remove(linhaRemovida);
            }
            Linha = null;
        }
    }

    #endregion COMANDOS LINHA
    
    #region COMANDOS ITEM LINHA
    void MAdicionarItemLinha()
    {
        if (Linha == null)
        {
            MessageBox.Show("Selecione uma linha", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        var dc = new ItemLinhaViewModel(ItemDaLinhaDB, Linha);
        var dlg = new ItemLinhaView { DataContext = dc };
        dlg.ShowDialog();
        if (dc.SalvouItemLinhaCoMSucesso)
        {
            ItemDaLinha NovoItemLinha = ItemDaLinhaDB.FindAll();
        }

    }
    void MExcluirItemLinha()
    {

    }
    #endregion COMANDOS ITEM LINHAS

}
