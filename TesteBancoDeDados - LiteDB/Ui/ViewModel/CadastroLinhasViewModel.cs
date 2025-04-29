using LiteDB;
using System.Collections.ObjectModel;
using System.Windows;
using TesteBancoDeDados___LiteDB.Domain.Model.Data;
using TesteBancoDeDados___LiteDB.Mappers;
using TesteBancoDeDados___LiteDB.Ui.View;

namespace TesteBancoDeDados___LiteDB.Ui.ViewModel;

internal class CadastroLinhasViewModel : BindableBase
{

    private DelegateCommand botaoAdicionarFabricante;
    private DelegateCommand botaoExcluirFabricante;
    private DelegateCommand botaoEditarFabricante;
    private DelegateCommand botaoAdicionarGrupo;
    private DelegateCommand botaoExcluirGrupo;
    private DelegateCommand botaoEditarGrupo;
    private DelegateCommand botaoAdicionarLinha;
    private DelegateCommand botaoExcluirLinha;
    private DelegateCommand botaoEditarLinha;
    private DelegateCommand adicionarItemLinha;
    private DelegateCommand excluirItemLinha;


    private Fabricante fabricante;
    private Grupo grupo;
    private Linha linha;
    private ItemDaLinha itemLinha;



    public LiteDatabase Db { get; private set; }
    public ILiteCollection<Fabricante> FabricanteRepository { get; private set; }
    public ILiteCollection<Grupo> GrupoRepository { get; private set; }
    public ILiteCollection<Linha> LinhaRepository { get; private set; }
    public ILiteCollection<ItemDaLinha> ItemDaLinhaRepository { get; private set; }



    public ObservableCollection<Fabricante> Fabricantes { get; set; }
    public ObservableCollection<Grupo> Grupos { get; set; }
    public ObservableCollection<Linha> Linhas { get; set; }
    public ObservableCollection<ItemDaLinha> ItemsLinhas { get; set; }



    public DelegateCommand BotaoAdicionarFabricante => botaoAdicionarFabricante ?? (botaoAdicionarFabricante = new DelegateCommand(AdicionarFabricante));
    public DelegateCommand BotaoExcluirFabricante => botaoExcluirFabricante ?? (botaoExcluirFabricante = new DelegateCommand(ExcluirFabricante));
    public DelegateCommand BotaoEditarFabricante => botaoEditarFabricante ?? (botaoEditarFabricante = new DelegateCommand(EditarFabricante));
    public DelegateCommand BotaoAdicionarGrupo => botaoAdicionarGrupo ?? (botaoAdicionarGrupo = new DelegateCommand(AdicionarGrupo));
    public DelegateCommand BotaoExcluirGrupo => botaoExcluirGrupo ?? (botaoExcluirGrupo = new DelegateCommand(ExcluirGrupo));
    public DelegateCommand BotaoEditarGrupo => botaoEditarGrupo ?? (botaoEditarGrupo = new DelegateCommand(EditarGrupo));
    public DelegateCommand BotaoAdicionarLinha => botaoAdicionarLinha ?? (botaoAdicionarLinha = new DelegateCommand(AdicionarLinha));
    public DelegateCommand BotaoEditarLinha => botaoEditarLinha ?? (botaoEditarLinha = new DelegateCommand(EditarLinha));
    public DelegateCommand BotaoExcluirLinha => botaoExcluirLinha ?? (botaoExcluirLinha = new DelegateCommand(ExcluirLinha));
    public DelegateCommand AdicionarItemLinha => adicionarItemLinha ?? (adicionarItemLinha = new DelegateCommand(MAdicionarItemLinha));
    public DelegateCommand ExcluirItemLinha => excluirItemLinha ?? (excluirItemLinha = new DelegateCommand(MExcluirItemLinha));



    public Fabricante Fabricante
    {
        get { return fabricante; }
        set
        {
            if (!SetProperty(ref fabricante, value))
            {
                return;
            }
            LoadGrupoByFabricante(value);
        }
    }

    public Grupo Grupo

    {
        get { return grupo; }
        set
        {
            if (!SetProperty(ref grupo, value))
            {
                return;
            }
            LoadLinhasByGrupo(value);

        }
    }

    public Linha Linha
    {
        get
        {
            return linha;
        }

        set
        {
            if (!SetProperty(ref linha, value))
            {
                return;
            }
            LoadItemDaLinhsByLinhas(value);
        }
    }

    public ItemDaLinha ItemLinha
    {
        get { return itemLinha; }
        set { SetProperty(ref itemLinha, value); }
    }



    #region CONSTRUTORA E DESTRUTORA
    public CadastroLinhasViewModel()
    {
        Db = new LiteDatabase("Banco.db");
        CarregaDadosDB();
        Fabricantes = new ObservableCollection<Fabricante>(FabricanteRepository!.FindAll());
        Grupos = new ObservableCollection<Grupo>(GrupoRepository!.FindAll());
        Linhas = new ObservableCollection<Linha>(LinhaRepository!.FindAll());
        ItemsLinhas = new ObservableCollection<ItemDaLinha>(ItemDaLinhaRepository!.FindAll());

        Fabricante = Fabricantes.FirstOrDefault()!;
        Grupo = Grupos.FirstOrDefault()!;
        Linha = Linhas.FirstOrDefault()!;
        ItemLinha = ItemsLinhas.FirstOrDefault()!;
    }

    ~CadastroLinhasViewModel()
    {
        Db.Dispose();
    }
    #endregion CONSTRUTORA E DESTRUTORA

    #region CARREGAR BANCOS DE DADOS 
    private void CarregaDadosDB()
    {
        FabricanteRepository = Db.GetCollection<Fabricante>(MapDataBase.Fabricante);
        GrupoRepository = Db.GetCollection<Grupo>(MapDataBase.Grupo);
        LinhaRepository = Db.GetCollection<Linha>(MapDataBase.Linha);
        ItemDaLinhaRepository = Db.GetCollection<ItemDaLinha>(MapDataBase.ItemLinha);
    }
    private void LoadGrupoByFabricante(Fabricante value)
    {
        if (Grupos.Count > 0)
        {
            Grupos.Clear();
        }
        if (value == null)
        {
            return;
        }
        var lista = GrupoRepository.Find(x => x.Fabricante.Id == value.Id);
        foreach (var item in lista)
        {
            Grupos.Add(item);
        }
    }
    private void LoadLinhasByGrupo(Grupo value)
    {
        if (Linhas.Count > 0)
            Linhas.Clear();
        if (value == null)
            return;
        var lista = LinhaRepository.Find(x => x.Grupo.Id == value.Id);
        foreach (var item in lista)
        {
            Linhas.Add(item);
        }
    }
    private void LoadItemDaLinhsByLinhas(Linha value)
    {
        if (ItemsLinhas.Count > 0)
            ItemsLinhas.Clear();
        if (value == null)
            return;
        var lista = ItemDaLinhaRepository.Find(x => x.Linha.Id == value.Id);
        foreach (var item in lista)
            ItemsLinhas.Add(item);
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
            FabricanteRepository.Delete(Fabricante.Id);
            var fabricanteremovido = Fabricantes.FirstOrDefault(x => x.Nome == Fabricante.Nome);
            if (fabricanteremovido != null)
            {
                Fabricantes.Remove(fabricanteremovido);
            }
            Fabricante = null;
        }
    }

    void EditarFabricante()
    {
        if (Fabricante == null)
        {
            return;
        }
        var dc = new FabricanteViewModel(Fabricantes, true)
        {
            Nome = Fabricante.Nome,
        };
        var dlg = new FabricanteView { DataContext = dc };
        if (dlg.ShowDialog() != true)
            return;
        Fabricante.Nome = dc.Nome;
        FabricanteRepository.Update(dc.Fabricantes);
    }


    void AdicionarFabricante()
    {
        var dc = new FabricanteViewModel(Fabricantes);
        var dlg = new FabricanteView { DataContext = dc };

        if (dlg.ShowDialog() != true)
        {
            return;
        }
        var novoFabricante = new Fabricante { Nome = dc.Nome };
        BsonValue ret = FabricanteRepository.Insert(novoFabricante);
        Fabricantes.Add(novoFabricante);
        Fabricante = Fabricantes.LastOrDefault()!;
    }

    #endregion COMANDOS FABRICANTES

    #region COMANDOS GRUPOS

    void EditarGrupo()
    {
        if (Grupo == null)
        {
            return;
        }
        var dc = new GrupoViewModel(Fabricante, Grupos, true)
        {
            NomeGrupo = Grupo!.Nome
        };
        var dlg = new GrupoView { DataContext = dc };
        if (dlg.ShowDialog() != true)
            return;
        Grupo.Nome = dc.NomeGrupo;
        GrupoRepository.Update(dc.Grupos);
    }

    void ExcluirGrupo()
    {
        if (Grupo == null)
            return;
        var resultadoGrupoExcluir = MessageBox.Show($"Tem certeza que deseja excluir '{Grupo}'?", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (resultadoGrupoExcluir == MessageBoxResult.Yes)
        {
            GrupoRepository.Delete(Grupo.Id);
            var grupoRemovido = Grupos.FirstOrDefault(x => x.Nome == Grupo.Nome);
            if (grupoRemovido != null)
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
        var dc = new GrupoViewModel(Fabricante, Grupos);
        var dlg = new GrupoView { DataContext = dc };
        if (dlg.ShowDialog() != true)
            return;
        var novoGrupo = new Grupo { Nome = dc.NomeGrupo, Fabricante = Fabricante };
        var ret = GrupoRepository.Insert(novoGrupo);
        Grupos.Add(novoGrupo);
        Grupo = Grupos.FirstOrDefault()!;
    }

    #endregion COMANDOS GRUPOS

    #region COMANDOS LINHA
    void AdicionarLinha()
    {
        if (Grupo == null)
        {
            MessageBox.Show("Selecione um Grupo", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        var dc = new LinhaViewModel(Linhas, Grupo);
        var dlg = new LinhaView { DataContext = dc };
        if (dlg.ShowDialog() != true)
            return;
        var novaLinha = new Linha { Nome = dc.NomeLinha, Grupo = dc.GrupoSelecionado };
        var ret = LinhaRepository.Insert(novaLinha);
        Linhas.Add(novaLinha);
        Linha = Linhas.FirstOrDefault()!;
    }

    void EditarLinha()
    {
        if (Linha == null)
            return;
        var dc = new LinhaViewModel(Linhas, Grupo, true)
        {
            NomeLinha = Linha.Nome
        };
        var dlg = new LinhaView { DataContext = dc };
        if (dlg.ShowDialog() != true)
            return;
        linha.Nome = dc.NomeLinha;
        LinhaRepository.Update(dc.Linhas);
    }

    void ExcluirLinha()
    {
        if (Linha == null)
            return;
        var resultadoExcluirLinha = MessageBox.Show($"Tem certeza que deseja Excluir a linha '{Linha}'?", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Information);
        if (resultadoExcluirLinha == MessageBoxResult.Yes)
        {
            LinhaRepository.Delete(Linha.Id);
            var linhaRemovida = Linhas.FirstOrDefault(x => x.Nome == Linha.Nome);
            if (linhaRemovida != null)
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
        var dc = new ItemLinhaViewModel(ItemsLinhas, Linha);
        var dlg = new ItemLinhaView { DataContext = dc };
        if (dlg.ShowDialog() != true)
            return;
        var novoItemLinha = new ItemDaLinha { Nome = dc.NomeItemDalinha, Linha = dc.LinhaSelecionada, TipoDeItem = dc.TipoDoItemDaLinhaSelecionado};
        var ret = ItemDaLinhaRepository.Insert(novoItemLinha);
        ItemsLinhas.Add(novoItemLinha);
        ItemLinha = ItemsLinhas.FirstOrDefault()!;

    }
    void MExcluirItemLinha()
    {
        if (ItemLinha == null)
            return;
        var resultadoExcluir = MessageBox.Show($"Tem certeza que deseja excluir o item '{ItemLinha}' da linha '{Linha}'?", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Information);
        if(resultadoExcluir == MessageBoxResult.Yes)
        {
            ItemDaLinhaRepository.Delete(ItemLinha.Id);
            var itemDaLinhaRemovida = ItemsLinhas.FirstOrDefault(x => x.Nome == ItemLinha.Nome);
            if(itemDaLinhaRemovida != null)
            {
                ItemsLinhas.Remove(itemDaLinhaRemovida);
            }
            itemLinha = null;
        }
    }
    #endregion COMANDOS ITEM LINHAS

}
