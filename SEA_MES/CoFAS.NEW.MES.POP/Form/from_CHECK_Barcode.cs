using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.POP.Function;
using FarPoint.Win.Spread;
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
    public partial class from_CHECK_Barcode : Form
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

        public string _품번 = string.Empty;

        public string _LOT = string.Empty;

        public string _p실적 = string.Empty;


        #endregion

        #region ○ 생성자

        public from_CHECK_Barcode()
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

                DataTable pDataTable1 = new CoreBusiness().BASE_MENU_SETTING_R10(this.Name, fpMain, "");

                if (pDataTable1 != null)
                {

                    CoFAS.NEW.MES.Core.Function.Core.initializeSpread(pDataTable1, fpMain, this.Name, "admin");
                    //Function.Core.InitializeControl(pDataTable1, fpMain, this, panel1, _MenuSettingEntity);
                }


                ucTextEdit1.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
                this.MinimumSize = this.Size;
                this.MaximumSize = this.Size;


                string sql = $@"select * 
                                from [dbo].[IN_BARCODE]
                                where 1=1
                                and RESOURCE_NO         = '{_품번}'
                                and LOT_NO              = '{_LOT}'
                                and WORK_PERFORMANCE_ID = '{_p실적}' ";
                DataTable dt2 = new CoreBusiness().SELECT(sql);


                Set_Spread_Date(fpMain, dt2);

                ucTextEdit1.Focus();

                _로드셀_Open();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }


        public void Set_Spread_Date(xFpSpread xFpSpread, DataTable dt)
        {
            xFpSpread.Sheets[0].Rows.Count = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                xFpSpread.Sheets[0].Visible = false;
                xFpSpread.Sheets[0].Rows.Count = dt.Rows.Count;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    foreach (DataColumn item in dt.Columns)
                    {
                        xFpSpread.Sheets[0].SetValue(i, item.ColumnName, dt.Rows[i][item.ColumnName]);
                    }
                }
                Core.Function.Core._AddItemSUM(fpMain);
                xFpSpread.Sheets[0].Visible = true;
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


        Barcode_Class _로드셀 = null;
        public string _bar;


        private void button1_Click(object sender, EventArgs e)
        {
            if (ucTextEdit1.Text == "")
            {
                CustomMsg.ShowMessage("입력된 바코드정보가 없습니다.");
                return;
            }
            string[] bar = ucTextEdit1.Text.Split('|');


            if (bar.Length != 0)
            {
                string sql = $@"SELECT [resource_no]
                                        ,[resource_used]
                                        ,[qty_per]
                                        ,[operation]
                                        ,[scrap_pct]                                      
                                    FROM [sea_mfg].[dbo].[cproduct_defn] with (nolock)
                                    where resource_no = '{_품번}' AND resource_used ='{bar[0]}'";

                DataTable dt = new MS_DBClass(utility.My_Settings_Get()).SELECT2(sql);

                if (dt.Rows.Count == 0)
                {
                    ucTextEdit1.Text = "";
                    CustomMsg.ShowMessage("일치 하지 않은 원재료 입니다.");
                }
                else
                {
                    label1.Text = "정상 원재료 입니다.";

                    ucTextEdit1.Enabled = false;
                }
            }
        }


        private void _로드셀_Open()
        {

            string sql = $@"SELECT COM
                            FROM [dbo].[SERIAL_SETTING]
                            WHERE WINDOW_CODE = '로드셀'";

            DataTable dt = new CoreBusiness().SELECT(sql);

            _로드셀 = new Barcode_Class(dt.Rows[0][0].ToString());
            if (_로드셀 != null)
            {
                if (_로드셀._port.IsOpen) //연결
                {
                    _로드셀.Readed += _로드셀_BarCode;
                }
            }
        }

        private void _로드셀_BarCode(object sender, ReadEventArgs e)
        {
            try
            {
                BeginInvoke(new MethodInvoker(delegate ()
                {
                    //string[] msg = e.ReadMsg.Trim().Split(',');

                    //for (int i = 0; i < msg.Length; i++)
                    //{
                    //    MessageBox.Show(msg[i]);
                    //}
                    //MessageBox.Show(msg);
                    string msg = e.ReadMsg.Trim();

                    string[] msgs = msg.Split(' ');

                    decimal wt = 0;
                    decimal ck = 0;
                    foreach (string item in msgs)
                    {
                        if (decimal.TryParse(item, out ck))
                        {
                            wt = decimal.Parse(item);
                        }
                    }
                    foreach (string item in msgs)
                    {
                        //_중량.Text = wt.ToString();
                    }


                }));
            }
            catch (Exception err)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //BaseFormSetting baseFormSetting = new BaseFormSetting(this.Name, ,"admin");

            //baseFormSetting.Show();
        }

        private void from_CHECK_Barcode_FormClosed(object sender, FormClosedEventArgs e)
        {
            _로드셀.Port_Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (ucTextEdit1.Text == "")
            {
                CustomMsg.ShowMessage("입력된 바코드 정보가 없습니다.");
                return;
            }

            string sql = $@"INSERT INTO [dbo].[IN_BARCODE] 
                                    ([WORK_PERFORMANCE_ID]
                                    ,[TYPE]
                                    ,[DATE]
                                    ,[RESOURCE_NO]
                                    ,[LOT_NO]
                                    ,[BARCODE_NO]
                                    ,[WEIGHT]
                                    ,[REG_DATE])
                              VALUES
                                    (
                                    {_p실적}
                                    ,'검증'
                                    ,GETDATE()
                                    ,'{_품번}'
                                    ,'{_LOT}'
                                    ,'{ucTextEdit1.Text}'
                                    ,'0'
                                    ,GETDATE());     ";            

            DataTable dt1 = new CoreBusiness().SELECT(sql);


            sql = $@"select * 
                     from[dbo].[IN_BARCODE]
                     where 1 = 1
                     and RESOURCE_NO              = '{_품번}'
                     and LOT_NO                   = '{_LOT}'
                     and WORK_PERFORMANCE_ID      = '{_p실적}'";
            DataTable dt2 = new CoreBusiness().SELECT(sql);


            Set_Spread_Date(fpMain, dt2);
            CustomMsg.ShowMessage("저장 되었습니다.");
            ////CustomMsg.ShowMessage("저장 되었습니다.");

            //_bar = ucTextEdit1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}



