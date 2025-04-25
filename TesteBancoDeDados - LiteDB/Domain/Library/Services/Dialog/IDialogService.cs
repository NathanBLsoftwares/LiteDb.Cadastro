namespace TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDialogService : IUserControlService
    {
        #region Public Propriedades

        /// <summary>
        /// 
        /// </summary>
        bool? DialogResult { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Title { get; set; }

        #endregion Public Propriedades

        #region Public Métodos

        /// <summary>
        /// 
        /// </summary>
        void Close();

        /// <summary>
        /// 
        /// </summary>
        void Show();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool? ShowDialog();

        #endregion Public Métodos
    }
}