using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class BaseQueryPopup : Form
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
        public string _CD { get; set; }
        public string _CD_NAME { get; set; }

        #endregion

        public xFpSpread pfpSpread = new xFpSpread();
        public DataTable _pDataTable = null;
        public DataRow _pdataRow = null;
        public bool _ck = true;
        public string _out_code = string.Empty;

        #region ○ 생성자

        public BaseQueryPopup()
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


                //if (_ck == true)
                //{
                //    if (_out_code != string.Empty)
                //    {
                //        foreach (Control item in SEARCH_PAN.Controls)
                //        {
                //            if (item.GetType() == typeof(Base_textbox))
                //            {
                //                Base_textbox base_Textbox = item as Base_textbox;

                //                if (base_Textbox.Name == "STOCK_MST_OUT_CODE")
                //                {
                //                    base_Textbox.SearchText = _out_code;

                //                    button1.Enabled = false;
                //                    button2.Enabled = false;
                //                }

                //            }

                //        }
                //    }
                //}

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
        //private void initializeSpread(DataTable dt)
        //{


        //    // fpMain ---------------------------------------------------------------------------------------------------------------------------------------------------
        //    // 콤보가 있을때 반드시 설정
        //    FpSpreadManager.pConnectionString = ConfigrationManager.ConnectionString;

        //    // 스프레드 기본설정
        //    FpSpreadManager.pOperationType = OperationMode.Normal; // 읽기전용설정
        //    FpSpreadManager.SpreadSetStyle(fpMain);                 // 스프레드 전체설정      
        //    FpSpreadManager.SpreadSetSheetStyle(fpMain, 0);         // 스프레드 쉬트설정

        //    // 스프레드 쉬트 설정

        //   // FpSpreadManager.BaseSpreadSetHeader(fpMain, 0, dt, this.Name, _UserAccount);
        //}

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
                        Base_textbox base_Textbox = item as Base_textbox;

                        if (base_Textbox.SearchText.Length != 0)
                        {
                            sql += $"and {base_Textbox.Name} like '%{base_Textbox.SearchText}%'";
                        }
                    }
                    else if (item.GetType() == typeof(Base_ComboBox))
                    {

                        Base_ComboBox base_ComboBox = item as Base_ComboBox;

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

        private void MainSave_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);


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

        #region ○ 기타이벤트

        #endregion

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelControl4_Click(object sender, EventArgs e)
        {
            BaseFormSetting baseFormSetting = new BaseFormSetting(this.Name,_UserAccount);

            baseFormSetting.Show();
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

        // ■ 버튼 클릭시
        #region ○ 버튼 클릭시 - btnCmd_Click(object pSender, EventArgs pEventArgs)
        private void btnCmd_Click(object pSender, EventArgs pEventArgs)
        {
            try
            {
                Button pCmd = pSender as Button;
                string sCls = pCmd.Name.Substring(6, 2);
                switch (sCls)
                {
                    case "01": // 초기화
                        DataTable pDataTable = new CoreBusiness().BASE_MENU_SETTING_R10(this.Name, fpMain, "");
                        InitializeControl(pDataTable);
                        MainFind_DisplayData();
                        break;
                    case "02": // 조회
                        MainFind_DisplayData();
                        break;
                    case "03": // 검증
                       
                        break;
                    case "04": // 저장
                        try
                        {
                            //if (YDSCore._SaveButtonClicked(fpMain))
                            //{
                            //    if (fpMain.Sheets[0].Rows.Count > 0)
                            //        MainSave_InputData();
                            //}
                        }
                        catch (Exception pExcption)
                        {
                            CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
                        }
                        break;
                    case "05": // 닫기
                        this.Close();
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion






        private void fpMain_CellClick(object sender, CellClickEventArgs e)
        {
            _CD = fpMain.Sheets[0].GetValue(e.Row, "CD").ToString();
            _CD_NAME = fpMain.Sheets[0].GetValue(e.Row, "CD_NM").ToString();
            if (_CD != "")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
       
    }
}



