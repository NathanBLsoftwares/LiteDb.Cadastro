using LiteDB;
using System.Windows;
using TesteBancoDeDados___LiteDB.Domain.Model.Data;

namespace TesteBancoDeDados___LiteDB.Ui.ViewModel;

internal class GrupoViewModel : BindableBase
{
    private DelegateCommand<Domain.Library.Services.Dialog.IDialogService> salvar;
    private ILiteCollection<Grupo>? gruposDB;
    private Grupo? gruposSelecionados;
    private string nomeGrupo;


    public DelegateCommand<Domain.Library.Services.Dialog.IDialogService> Salvar => salvar ?? (salvar = new DelegateCommand<Domain.Library.Services.Dialog.IDialogService>(SalvarGrupos));
    public bool SalvouGrupoComSuceso { get; private set; } = false;
    public string NomeGrupo { get => nomeGrupo; set => SetProperty(ref nomeGrupo, value); }



    public GrupoViewModel(ILiteCollection<Grupo> _grupoDB, Grupo _grupo = null)
    {
        this.gruposDB = _grupoDB;
        gruposSelecionados = _grupo;
    }

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
                gruposDB.Insert(new Grupo { Nome = nomeGrupo });
            }
            SalvouGrupoComSuceso = true;
            service.DialogResult = true;
            service.Close();
        }
    }
}
