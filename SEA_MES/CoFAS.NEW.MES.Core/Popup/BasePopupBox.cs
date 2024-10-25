using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using System;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class BasePopupBox : Form
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
            }
        }

        #endregion

        #region ○ 변수선언

        public string _UserAccount = string.Empty;
        public string _UserAuthority = string.Empty;
        public string _CD { get; set; } = string.Empty;
        public string _CD_NAME { get; set; } = string.Empty;

        #endregion

        public xFpSpread pfpSpread = new xFpSpread();
        public DataTable _pDataTable = null;
        public DataRow _pdataRow = null;
        public string _out_code = string.Empty;

        public bool _ck = true;
        #region ○ 생성자

        public BasePopupBox(string str)
        {
            _out_code = str;
            InitializeComponent();

            Load += new EventHandler(Form_Load);
        }

        public BasePopupBox(bool ck)
        {
            _ck = ck;
            InitializeComponent();

            Load += new EventHandler(Form_Load);
        }
        public BasePopupBox()
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
                fpMain._ChangeEventHandler += FpMain_Change;

                DataTable pDataTable =  new CoreBusiness().BASE_MENU_SETTING_R10(this.Name,fpMain,"");
                               
                if (pDataTable != null)
                {
                    Function.Core.initializeSpread(pDataTable, fpMain, this.Name, _UserAccount);
                    InitializeControl(pDataTable);
                


                    //LeftFind_DisplayData();
                    MainFind_DisplayData();
                }


            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }



        #endregion

        #region ○ 초기화 영역

        private void InitializeControl(DataTable dt)
        {
            try
            {
                int w = 0;

                foreach (DataRow item in dt.Rows)
                {
                    if (item["VISIBLE"].ToString() == "Y")
                    {
                        w += Convert.ToInt32(item["WIDTH"]);
                    }
                }
                if (this.Size.Width > (w + 100))
                {
                    this.Size = new System.Drawing.Size(w + 100, this.Size.Height);
                }

                Function.Core.InitializeControl(dt, fpMain, this, SEARCH_PAN, new MenuSettingEntity() { BASE_TABLE = "" });
     
          
                if (_ck == true)
                {
                    if (_out_code != string.Empty)
                    {
                        foreach (Control item in SEARCH_PAN.Controls)
                        {
                            if (item.GetType() == typeof(Base_textbox))
                            {
                                Base_textbox base_Textbox  =  item as Base_textbox;

                                if (base_Textbox.Name == "STOCK_MST_OUT_CODE")
                                {
                                    base_Textbox.SearchText = _out_code;

                                    button1.Enabled = false;
                                    button2.Enabled = false;
                                }

                            }

                        }
                    }
                }

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
            finally
            {

            }
        }
        private void _SearchCom_ValueChanged(object sender, EventArgs e)
        {
            MainFind_DisplayData();
        }
      

        private void FpMain_Change(object sender, ChangeEventArgs e)
        {
            try
            {

                string pHeaderLabel = fpMain.Sheets[0].RowHeader.Cells[e.Row, 0].Text;
                switch (pHeaderLabel)
                {
                    case "":
                        fpMain.Sheets[0].RowHeader.Cells[e.Row, 0].Text = "수정";
                        break;
                    case "입력":
                        break;
                    case "수정":
                        break;
                    case "삭제":
                        break;
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region ○ 데이터 영역


        private void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string sql = " 1=1 ";
                foreach (Control item in SEARCH_PAN.Controls)
                {
                    if (item.GetType() == typeof(Base_textbox))
                    {
                        Base_textbox base_Textbox  =  item as Base_textbox;

                        if (base_Textbox.SearchText.Length != 0)
                        {
                            sql += $"and {base_Textbox.Name} like '%{base_Textbox.SearchText}%'";
                        }
                    }
                    else if (item.GetType() == typeof(Base_ComboBox))
                    {

                        Base_ComboBox base_ComboBox  =  item as Base_ComboBox;

                        if (base_ComboBox.SearchText.Length != 0)
                        {
                            sql += $"and {base_ComboBox.Name} = '{base_ComboBox.SearchText}'";
                        }
                    }
                }

                if (pfpSpread._menu_name != null)
                {
                    if (pfpSpread._menu_name.ToString() == "외주작업지시등록")
                    {
                        sql += $"and PROCESS_ID = '6'";
                    }

                    if (pfpSpread._menu_name.ToString() == "사출조건현황")
                    {
                        sql += $"and TYPE = 'CD14001'";
                    }

                    if (pfpSpread._menu_name.ToString() == "압출조건현황")
                    {
                        sql += $"and TYPE = 'CD14001'";
                    }
                }

                DataRow[] drs = _pDataTable.Select(sql);

                DataTable dt = null;

                if (drs.Length != 0)
                {
                    dt = drs.CopyToDataTable();
                }
             

                if (dt != null && dt.Rows.Count > 0)
                {

                    fpMain.Sheets[0].Visible = false;
                    fpMain.Sheets[0].Rows.Count = dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        foreach (DataColumn item in dt.Columns)
                        {
                            fpMain.Sheets[0].SetValue(i, item.ColumnName, dt.Rows[i][item.ColumnName]);
                        }

                    }

                    fpMain.Sheets[0].Visible = true;


                }
                else
                {
                    fpMain.Sheets[0].Rows.Count = 0;
                    CustomMsg.ShowMessage("조회 내역이 없습니다.");
                }


            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
            finally
            {
                DevExpressManager.SetCursor(this, Cursors.Default);
            }
        }

       
        #endregion

       

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

   

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable pDataTable =  new CoreBusiness().BASE_MENU_SETTING_R10(this.Name,fpMain,"");
            InitializeControl(pDataTable);
            MainFind_DisplayData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainFind_DisplayData();
        }

        private void fpMain_CellClick(object sender, CellClickEventArgs e)
        {
            if (fpMain.Sheets[0].GetValue(e.Row, "CD").ToString() == "")
            {
                return;
            }
            if (_CD      != fpMain.Sheets[0].GetValue(e.Row, "CD").ToString() &&
                _CD_NAME != fpMain.Sheets[0].GetValue(e.Row, "CD_NM").ToString())
            {
                _CD = fpMain.Sheets[0].GetValue(e.Row, "CD").ToString();
                _CD_NAME = fpMain.Sheets[0].GetValue(e.Row, "CD_NM").ToString();
            }
            else
            {
                _CD = fpMain.Sheets[0].GetValue(e.Row, "CD").ToString();
                _CD_NAME = fpMain.Sheets[0].GetValue(e.Row, "CD_NM").ToString();

                _pdataRow = _pDataTable.Select($"CD = '{_CD}' AND CD_NM = '{_CD_NAME}'")[0];

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}



