using System.Windows;

namespace TesteBancoDeDados___LiteDB.Ui.View
{
    /// <summary>
    /// Lógica interna para CadastroLinhasView.xaml
    /// </summary>
    public partial class CadastroLinhasView : Window, TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog.IDialogService
    {
        public CadastroLinhasView()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }
    }
}
