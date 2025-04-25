using System;
using System.Windows.Input;

namespace TesteBancoDeDados___LiteDB.Domain.Library.Services.Dialog
{
    public enum Cursores
    {
        None,
        ScrollSW,
        ScrollNE,
        ScrollNW,
        ScrollE,
        ScrollW,
        ScrollS,
        ScrollN,
        ScrollAll,
        ScrollWE,
        ScrollNS,
        Pen,
        Hand,
        Wait,
        UpArrow,
        SizeWE,
        SizeNWSE,
        SizeNS,
        SizeNESW,
        SizeAll,
        IBeam,
        Help,
        Cross,
        AppStarting,
        Arrow,
        No,
        ScrollSE,
        ArrowCD,
    }

    public class TrocaCursor : IDisposable
    {
        #region Private Campos

        private Cursor cursorOriginal;
        private IDialogService dialog;
        private Cursores novoCursor;

        #endregion Private Campos

        #region Public Construtoras

        public TrocaCursor(IDialogService dialog, Cursores novoCursor = Cursores.Wait)
        {
            this.dialog = dialog;
            cursorOriginal = dialog.Cursor;
            this.novoCursor = novoCursor;
            dialog.Cursor = GetCursor(novoCursor);
        }

        #endregion Public Construtoras

        #region Public Métodos

        public void Dispose()
        {
            SetCursorOriginal();
        }

        public void SetCursorOriginal()
        {
            dialog.Cursor = cursorOriginal;
        }

        #endregion Public Métodos

        #region Private Métodos

        private Cursor GetCursor(Cursores novoCursor)
        {
            switch (novoCursor)
            {
                case Cursores.None:
                    return Cursors.None;

                case Cursores.ScrollSW:
                    return Cursors.ScrollSW;

                case Cursores.ScrollNE:
                    return Cursors.ScrollNE;

                case Cursores.ScrollNW:
                    return Cursors.ScrollNW;

                case Cursores.ScrollE:
                    return Cursors.ScrollE;

                case Cursores.ScrollW:
                    return Cursors.ScrollW;

                case Cursores.ScrollS:
                    return Cursors.ScrollS;

                case Cursores.ScrollN:
                    return Cursors.ScrollN;

                case Cursores.ScrollAll:
                    return Cursors.ScrollAll;

                case Cursores.ScrollWE:
                    return Cursors.ScrollWE;

                case Cursores.ScrollNS:
                    return Cursors.SizeNS;

                case Cursores.Pen:
                    return Cursors.Pen;

                case Cursores.Hand:
                    return Cursors.Hand;

                case Cursores.Wait:
                    return Cursors.Wait;

                case Cursores.UpArrow:
                    return Cursors.UpArrow;

                case Cursores.SizeWE:
                    return Cursors.SizeWE;

                case Cursores.SizeNWSE:
                    return Cursors.SizeNWSE;

                case Cursores.SizeNS:
                    return Cursors.SizeNS;

                case Cursores.SizeNESW:
                    return Cursors.SizeNESW;

                case Cursores.SizeAll:
                    return Cursors.SizeAll;

                case Cursores.IBeam:
                    return Cursors.IBeam;

                case Cursores.Help:
                    return Cursors.Help;

                case Cursores.Cross:
                    return Cursors.Cross;

                case Cursores.AppStarting:
                    return Cursors.AppStarting;

                case Cursores.Arrow:
                    return Cursors.Arrow;

                case Cursores.No:
                    return Cursors.No;

                case Cursores.ScrollSE:
                    return Cursors.ScrollSE;

                case Cursores.ArrowCD:
                    return Cursors.ArrowCD;

                default:
                    return Cursors.Arrow;
            }
        }

        #endregion Private Métodos
    }
}