using CoFAS.NEW.MES.Core;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP
{
    public partial class 이앤아이비_작업종료 : Form
    {
        #region ○ 폼 이동

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);


        private void tspMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
                //this.WindowState = FormWindowState.Maximized;
                //this.Refresh();
                //this.Invalidate();
                //this.SetStyle(ControlStyles.ResizeRedraw, true);
            }
        }

        #endregion

        #region ○ 변수선언

     

        #endregion
  
        #region ○ 생성자

        public 이앤아이비_작업종료()
        {
          
            InitializeComponent();

            Load += new EventHandler(Form_Load);


        }

        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                de_시간.DateTime = DateTime.Now;
                //this.WindowState = FormWindowState.Maximized;
                this.MinimumSize = this.Size;
                this.MaximumSize = this.Size;

                de_시간.Font = this.Font;

                de_시간.Margin = new Padding(3, 3, 3, 3);

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }





        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

      
    }
}



