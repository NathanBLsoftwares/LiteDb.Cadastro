using Microsoft.Win32;

namespace TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog
{
    public class OpenFileDialogService : FileDialogService
    {
        #region Public Construtoras

        public OpenFileDialogService() : base(new OpenFileDialog())
        {
        }

        #endregion Public Construtoras
    }
}