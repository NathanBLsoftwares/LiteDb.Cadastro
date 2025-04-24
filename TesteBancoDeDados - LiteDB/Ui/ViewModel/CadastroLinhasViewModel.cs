

using Prism.Commands;
using System.Windows.Input;
using TesteBancoDeDados___LiteDB.Domain.Model.Data;
using TesteBancoDeDados___LiteDB.Ui.View;

namespace TesteBancoDeDados___LiteDB.Ui.ViewModel;

internal class CadastroLinhasViewModel : BindableBase
{
    private DelegateCommand botaoAdicionarLinha;
    public ICommand BotaoAdicionarLinha => botaoAdicionarLinha ??= new DelegateCommand(AdicionarLinha);

    private void AdicionarLinha()
    {
        var janelalinha = new AdicionarLinhaView();
        if (janelalinha.ShowDialog() != true)
            return;
    }
}
