using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class Label_Scheduler : System.Windows.Forms.UserControl
    {
        #region .. Double Buffered function ..
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;
            System.Reflection.PropertyInfo aProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        #endregion
        public UserEntity _pUserEntity = null;
        DataRow _row = null;
        public Label_Scheduler(DataRow row, UserEntity pUserEntity)
        {
            _pUserEntity = pUserEntity;
            _row = row;
            InitializeComponent();
        }

        private void Label_Scheduler_Load(object sender, EventArgs e)
        {
            this.label1.MouseHover += Day_MouseHover;
            this.label1.MouseLeave += DayMouseLeave;

            this.label2.MouseHover += Day_MouseHover;
            this.label2.MouseLeave += DayMouseLeave;
            label2.BackColor = Color.FromArgb(
                  Convert.ToInt32(_row[9])
                , Convert.ToInt32(_row[10])
                , Convert.ToInt32(_row[11]));

        }

        private void DayMouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.Transparent;
        }

        private void Day_MouseHover(object sender, EventArgs e)
        {
            this.BackColor = Color.AliceBlue;
        }

    
    }
}
