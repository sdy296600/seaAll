using System.Drawing;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public static class CustomMsg
    {
        public static DialogResult ShowMessage(string Message)
        {
            DialogResult pDialogResult = DialogResult.None;
            using (frmMessageBox pOK = new frmMessageBox())
            {
                pOK.Title = "Information";
                pOK.Message = Message;
                pDialogResult = pOK.ShowDialog();
            }
            return pDialogResult;
        }
     
        public static DialogResult ShowMessage(string Message, string Caption)
        {
            DialogResult pDialogResult = DialogResult.None;
            using (frmMessageBox pOK = new frmMessageBox())
            {
                pOK.Title = Caption;
                pOK.Message = Message;
                pDialogResult = pOK.ShowDialog();
            }
            return pDialogResult;
        }

        public static DialogResult ShowMessageLink(string Message, string Caption)
        {
            DialogResult pDialogResult = DialogResult.None;
            using (frmMessageBox_Link pOK = new frmMessageBox_Link())
            {
                pOK.Title = Caption;
                pOK.Message = Message;
                pDialogResult = pOK.ShowDialog();
            }
            return pDialogResult;
        }
        public static DialogResult ShowMessage(string message, string caption, MessageBoxButtons button)
        {
            DialogResult pDialogResult = DialogResult.None;
            switch (button)
            {
                case MessageBoxButtons.OK:
                    using (frmMessageBox pOK = new frmMessageBox())
                    {
                        pOK.Title = caption;
                   
                        pOK.Message = message;
                        pDialogResult = pOK.ShowDialog();
                    }
                    break;
                case MessageBoxButtons.YesNo:
                    using (frmMessageBoxYN pYesNo = new frmMessageBoxYN())
                    {
                        pYesNo.Title = caption;
                        pYesNo.Message = message;
                        pDialogResult = pYesNo.ShowDialog();
                    }
                    break;
            }

            return pDialogResult;
        }
      

        public static DialogResult ShowExceptionMessage(string message, string caption, MessageBoxButtons button)
        {
            DialogResult pDialogResult = DialogResult.None;
            switch (button)
            {
                case MessageBoxButtons.OK:
                    using (frmMessageBox pOK = new frmMessageBox())
                    {
                        pOK.Title = caption;
                        pOK.Message = message;
                        pDialogResult = pOK.ShowDialog();
                    }
                    break;
                case MessageBoxButtons.YesNo:
                    using (frmMessageBoxYN pYesNo = new frmMessageBoxYN())
                    {
                        pYesNo.Title = caption;
                        pYesNo.Message = message;
                        pDialogResult = pYesNo.ShowDialog();
                    }
                    break;
            }

            return pDialogResult;
        }

    }
}
