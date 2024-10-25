using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Upload
{
    public partial class frmWaitAsyncForm : Form
    {
        public Action Worker { get; set; }

        public frmWaitAsyncForm(Action worker)
        {
            InitializeComponent();

            Worker = worker;
        }

        private void frmWaitAsyncForm_Load(object sender, EventArgs e)
        {
            Task.Factory.StartNew(Worker).ContinueWith(t => { this.Close(); }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
