using System.Windows;

namespace TesteBancoDeDadosLiteDB.Ui.View
{
    /// <summary>
    /// Lógica interna para ItemView.xaml
    /// </summary>
    public partial class ItemView : Window, TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog.IDialogService
    {
        public ItemView()
        {
            InitializeComponent();
        }
    }
}
