using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.POP.Function;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP
{
    public partial class BaseImagePopupBox : Form
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
                //this.WindowState = FormWindowState.Minimized;
                this.Refresh();
                this.Invalidate();
                this.SetStyle(ControlStyles.ResizeRedraw, true);
            }
        }

        #endregion

        #region ○ 변수선언

        string _name = string.Empty;
        string _id   = string.Empty;
        string _line   = string.Empty;
        #endregion

        #region ○ 생성자

        public BaseImagePopupBox(string name ,string id,string line)
        {
          
            InitializeComponent();
            _name = name;
            _id = id;
            _line = line;
            Load += new EventHandler(Form_Load);

            _Title.Text = name;

          
        }

        #endregion

        public System.Threading.Timer _timer;

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                //this.WindowState = FormWindowState.Maximized;
                //this.MinimumSize = this.Size;
                //this.WindowState = FormWindowState.Minimized;

                this.TopMost = true;
                this.Activate();

                if (_line == "CD14001")
                {
                    _timer = new System.Threading.Timer(CallBack1);
                }
                else
                {
                    _timer = new System.Threading.Timer(CallBack);
                }
                _timer.Change(0, 30 * 1000);
            }
            catch (Exception pExcption)
            {
                //CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        public void CallBack(Object state)
        {
            try
            {
                BeginInvoke(new MethodInvoker(delegate ()            
                {
                    string sql = $@"SELECT TOP 1 
                                    IMAGE
                                    FROM [dbo].[EQUIPMENT] A
                                    LEFT JOIN [dbo].[WORK_STANDARDS] B ON A.ID = B.EQUIPMENT_NAME
                                    INNER JOIN [dbo].[PRODUCTION_INSTRUCT] C ON B.STOCK_MST_ID = C.STOCK_MST_ID
                                     WHERE 1=1
									and C.USE_YN = 'Y'
									and C.START_INSTRUCT_DATE IS NOT NULL
									AND C.END_INSTRUCT_DATE IS NULL
                                    AND A.ID = {_id} 
                                    ORDER BY START_INSTRUCT_DATE";

                    DataTable dt = new CoreBusiness().SELECT(sql);

                    if(dt == null)
                    {
                        return;
                    }
                    if (dt.Rows.Count != 0)
                    {
                        byte[] bys = dt.Rows[0]["IMAGE"] as byte[];

                        if (bys == null)
                        {
                            pictureBox1.Image = null;
                        }
                        else
                        {
                            using (MemoryStream ms = new MemoryStream(bys))
                            {
                                pictureBox1.Image = Image.FromStream(ms);
                            }
                        }
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                    

                }));
            }
            catch (Exception pExcption)
            {
                //BeginInvoke(new MethodInvoker(delegate ()
                //{
                //    //pictureBox1.Image = null;
                //}));
                //CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
        public void CallBack1(Object state)
        {
            try
            {
                BeginInvoke(new MethodInvoker(delegate ()
                {
                    string sql = $@"SELECT TOP 1 
                                    IMAGE
                                    FROM [dbo].[EQUIPMENT] A
                                    LEFT JOIN [dbo].[WORK_STANDARDS] B ON A.ID = B.EQUIPMENT_NAME
                                    INNER JOIN [dbo].[Code_Mst] C ON B.STOCK_MST_ID = C.code_name AND C.CODE  ='SD21001' and A.ID = {_id}";

                    
                    DataTable dt = new CoreBusiness().SELECT(sql);

                    if (dt == null)
                    {
                        return;
                    }
                    if (dt.Rows.Count != 0)
                    {
                        byte[] bys = dt.Rows[0]["IMAGE"] as byte[];



                        if (bys == null)
                        {
                            pictureBox1.Image = null;
                        }
                        else
                        {
                            using (MemoryStream ms = new MemoryStream(bys))
                            {
                                pictureBox1.Image = Image.FromStream(ms);
                            }
                        }
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }


                }));
            }
            catch (Exception pExcption)
            {
                //BeginInvoke(new MethodInvoker(delegate ()
                //{
                //    //pictureBox1.Image = null;
                //}));
                //CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
        private void lblClose_Click(object sender, EventArgs e)
        {
            _timer.Dispose();
            this.Close();
        }



        #endregion

    

     

      

      
    }
}



