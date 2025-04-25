using System.Windows;
using System.Windows.Input;

namespace TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog
{
    public interface IUserControlService
    {
        /// <summary>
        /// 
        /// </summary>
        Cursor Cursor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Visibility Visibility { get; set; }
    }
}
