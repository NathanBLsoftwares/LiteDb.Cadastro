using LiteDB;
using System.Windows;
using TesteBancoDeDados___LiteDB.Domain.Model.Data;

namespace TesteBancoDeDados___LiteDB.Ui.ViewModel;

internal class GrupoViewModel : BindableBase
{
    #region PROPRIEDADES E ATRIBUTOS

    private ILiteCollection<Grupo>? gruposDB;
    private DelegateCommand<Domain.Library.Services.Dialog.IDialogService> salvar;
    private Grupo? gruposSelecionados;
    private Fabricante fabricante;
    private string nomeGrupo;
    


    public Fabricante FabricanteSelecionado { get => fabricante; private set => fabricante = value; }
    public DelegateCommand<Domain.Library.Services.Dialog.IDialogService> Salvar => salvar ?? (salvar = new DelegateCommand<Domain.Library.Services.Dialog.IDialogService>(SalvarGrupos));
    public bool SalvouGrupoComSuceso { get; private set; } = false;
    public string NomeGrupo { get => nomeGrupo; set => SetProperty(ref nomeGrupo, value); }

    #endregion PROPRIEDADES E ATRIBUTOS

    #region CONSTRUTORA
    public GrupoViewModel(ILiteCollection<Grupo> _grupoDB, Fabricante fabricante, Grupo _grupo = null)
    {
        this.gruposDB = _grupoDB;
        gruposSelecionados = _grupo;
        FabricanteSelecionado = fabricante;
    }
    #endregion CONSTRUTORA

    #region COMANDO SALVAR
    private void SalvarGrupos(Domain.Library.Services.Dialog.IDialogService service)
    {
        if (service == null)
        {
            service.DialogResult = false;
            return;
        }
        else
        {
            if (string.IsNullOrEmpty(nomeGrupo))
            {
                MessageBox.Show("O campo Nome deve ser preenchido", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (gruposDB.Exists(x => x.Nome == nomeGrupo))
            {
                MessageBox.Show("Grupo já cadastrado", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (gruposSelecionados != null)
            {
                gruposSelecionados.Nome = nomeGrupo;
                if (!gruposDB.Update(gruposSelecionados))
                {
                    MessageBox.Show("Erro ao alterar o nome do grupo", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            else
            {
                var grupo = new Grupo();
                grupo.Fabricante = FabricanteSelecionado;
                grupo.Nome = nomeGrupo;
                gruposDB.Insert(grupo);
            }
            SalvouGrupoComSuceso = true;
            service.DialogResult = true;
            service.Close();
        }
    }
    #endregion COMANDO SALVAR
}
