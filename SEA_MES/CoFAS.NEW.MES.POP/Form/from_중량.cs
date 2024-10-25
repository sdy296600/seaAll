using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static CoFAS.NEW.MES.POP.Barcode_Class;

namespace CoFAS.NEW.MES.POP
{
    public partial class from_중량 : Form
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

        public from_중량()
        {

            InitializeComponent();

            Load += new EventHandler(Form_Load);


        }

        #endregion

        Barcode_Class _중량저울 = null;

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                _중량저울_Open();
                this.MinimumSize = this.Size;
                this.MaximumSize = this.Size;
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


        private void _중량저울_Open()
        {
            string sql = $@"SELECT COM
                            FROM [dbo].[SERIAL_SETTING]
                            WHERE WINDOW_CODE = '중량저울'";

            DataTable dt =  new CoreBusiness().SELECT(sql);
            _중량저울 = new Barcode_Class(dt.Rows[0][0].ToString());
            if (_중량저울 != null)
            {
                if (_중량저울._port.IsOpen) //연결
                {
                    _중량저울.Readed += _중량저울_BarCode;
                }
            }
        }
        private void _중량저울_BarCode(object sender, ReadEventArgs e)
        {
            try
            {
                BeginInvoke(new MethodInvoker(delegate ()
                {

                    string msg = e.ReadMsg.Trim();

                    string [] msgs = msg.Split(' ');

                    decimal wt = 0;

                    decimal ck = 0;
                    //??+??-1/-#1?s#9-!#?????????????QumqoW??????????????????Q5-1/????????|?1???????????W=5??????????1??????????q!??????????1????
                    foreach (string item in msgs)
                    {
                        if (decimal.TryParse(item, out ck))
                        {
                            wt = decimal.Parse(item);
                        }
                    }

                    _중량.Text = wt.ToString();

                    button1.PerformClick();

                }));
            }
            catch (Exception err)
            {

            }

        }


        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            _중량저울.Port_Close();
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void _중량_Click(object sender, EventArgs e)
        {
            using (from_키패드 popup = new from_키패드())
            {
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    _중량.Text = popup._code;
                }
            }
        }
    }
}



