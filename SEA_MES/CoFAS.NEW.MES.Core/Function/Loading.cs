using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core.Function
{
    public class Loading
    {
        WaitForm loadingForm;
        Thread loadthread;
        string msg;
        public void Show()
        {
            loadthread = new Thread(new ThreadStart(LoadingProcessEx));
            loadthread.Start();
        }
        public void Show(Form parent)
        {
            loadthread = new Thread(new ParameterizedThreadStart(LoadingProcessEx));
            loadthread.Start(parent);
        }

        public void Show(Form parent, string message)
        {
            loadthread = new Thread(new ParameterizedThreadStart(LoadingProcessEx));
            msg = message;
            loadthread.Start(parent);
        }

        public void MsgChange(string message)
        {
            ControlManager.Invoke_Control_Text(loadingForm.lblMessage, message);
        }

        public void Close()
        {
            if (loadingForm != null)
            {
                try
                {
                    loadingForm.BeginInvoke(new System.Threading.ThreadStart(loadingForm.CloseLoadingForm));
                    loadingForm = null;
                    loadthread = null;
                }
                catch
                {

                }
            }
        }
        private void LoadingProcessEx()
        {
            loadingForm = new WaitForm();
            loadingForm.ShowDialog();
        }
        private void LoadingProcessEx(object parent)
        {
            Form Cparent = parent as Form;
            loadingForm = new WaitForm(Cparent);
            if (msg != null && msg != "")
                ControlManager.Invoke_Control_Text(loadingForm.lblMessage, msg);
            loadingForm.ShowDialog();
        }

    }
 
}
