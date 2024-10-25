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
    public partial class BaseFormSetting : Form
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

        //UserEntity _UserEntity = new UserEntity();
        string _Menu_name = string.Empty;

        private bool _FirstYn = true;

        MenuSettingEntity _MenuSettingEntity = null;
        xFpSpread _xFpSpread = null;
        #endregion


        #region ○ 생성자

        public BaseFormSetting(string menu_name, xFpSpread _pxFpSpread, string userEntity)
        {
            _UserAccount = userEntity;
            _Menu_name = menu_name;

            _xFpSpread = _pxFpSpread;

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
                _MenuSettingEntity = new MenuSettingEntity();
                _MenuSettingEntity.BASE_ORDER = "order by seq";
                _MenuSettingEntity.MENU_WINDOW_NAME = this.Name;
                _MenuSettingEntity.BASE_TABLE = "BASE_MENU_SETTING";

                DataTable pDataTable =  new CoreBusiness().BASE_MENU_SETTING_R10(_MenuSettingEntity.MENU_WINDOW_NAME,fpMain,_MenuSettingEntity.BASE_TABLE.Split('/')[0]);
                if (pDataTable != null)
                {
                    //InitializeControl(pDataTable);

                    Function.Core.initializeSpread(pDataTable, fpMain, this.Name, _UserAccount);
                    InitializeControl(pDataTable);
                    //  _FirstYn = false;
                    //_UserAccount = MainForm.UserEntity.user_account;
                }


                //LeftFind_DisplayData();
                MainFind_DisplayData();


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
                if (_FirstYn)
                {
                    panel2.Name = _MenuSettingEntity.BASE_TABLE;
                    panel2.Tag = $"and MENU_NAME = '{_Menu_name}'";

                    int x = 5;
                    int y = 5;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["SEARCH_YN"].ToString() == "Y")
                        {
                            switch (dt.Rows[i]["CELLTYPE"].ToString())
                            {
                                case "텍스트":
                                    Base_textbox base_Textbox = new Base_textbox();
                                    base_Textbox.Name = dt.Rows[i]["COLUMNKEY"].ToString();
                                    base_Textbox.SearchName = dt.Rows[i]["HEADERNAME"].ToString();
                                    if (x >= panel2.Size.Width)
                                    {
                                        x = 5;
                                        y += 30;
                                        base_Textbox.Location = new Point(x, y);
                                    }
                                    else
                                    {
                                        base_Textbox.Location = new Point(x, y);
                                    }
                                    x += 310;
                                    panel2.Controls.Add(base_Textbox);
                                    break;
                                case "콤보박스":
                                    bool all_yn = true;

                                    if (dt.Rows[i]["ESSENTIAL_YN"].ToString() == "Y")
                                    {
                                        all_yn = false;
                                    }
                                    else
                                    {
                                        all_yn = true;
                                    }
                                    if (dt.Rows[i]["COLUMNKEY"].ToString() == "SPREAD_NAME")
                                    {
                                        dt.Rows[i]["CODENAME"] = _Menu_name;
                                    }

                                    Base_ComboBox base_ComboBox = new Base_ComboBox(dt.Rows[i],all_yn);


                                    base_ComboBox.Name = dt.Rows[i]["COLUMNKEY"].ToString();
                                    base_ComboBox.SearchName = dt.Rows[i]["HEADERNAME"].ToString();

                                    base_ComboBox._SearchCom.ValueChanged += _SearchCom_ValueChanged;
                                    if (x >= panel2.Size.Width)
                                    {
                                        x = 5;
                                        y += 30;
                                        base_ComboBox.Location = new Point(x, y);
                                    }
                                    else
                                    {
                                        base_ComboBox.Location = new Point(x, y);
                                    }
                                    x += 310;
                                    panel2.Controls.Add(base_ComboBox);
                                    break;

                                case "날짜시간":
                                    Base_FromtoDateTime base_datetime = new Base_FromtoDateTime();
                                    base_datetime.Name = dt.Rows[i]["COLUMNKEY"].ToString();
                                    base_datetime.SearchName = dt.Rows[i]["HEADERNAME"].ToString();
                                    if (x >= panel2.Size.Width)
                                    {
                                        x = 5;
                                        y += 30;
                                        base_datetime.Location = new Point(x, y);
                                    }
                                    else
                                    {
                                        base_datetime.Location = new Point(x, y);
                                    }
                                    x += 310;
                                    panel2.Controls.Add(base_datetime);
                                    break;
                                case "삭제":
                                    break;
                            }

                        }
                    }
                    if (panel2.Controls.Count == 0)
                    {

                        //_pnHeader.Size = new Size(1500, 0);
                    }
                }


            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
            finally
            {
                _FirstYn = false;
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

                DataTable _DataTable = new CoreBusiness().BaseForm1_R10(panel2,_MenuSettingEntity);

                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpMain.Sheets[0].Visible = false;
                    fpMain.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        foreach (DataColumn item in _DataTable.Columns)
                        {
                            fpMain.Sheets[0].SetValue(i, item.ColumnName, _DataTable.Rows[i][item.ColumnName].ToString());
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





        private void btn_추가_Click(object sender, EventArgs e)
        {
            try
            {

                Function.Core._AddItemButtonClicked(fpMain, _UserAccount);

                fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "MENU_NAME      ".Trim(), _Menu_name);

                fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "CELLTYPE      ".Trim(), "텍스트");


                for (int i = 0; i < panel2.Controls.Count; i++)
                {
                    if(panel2.Controls[i].Name == "SPREAD_NAME")
                    {
                        Base_ComboBox base_ComboBox = panel2.Controls[i] as Base_ComboBox;

                        if (base_ComboBox.SearchText.Length > 0)
                        {
                            fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "SPREAD_NAME      ".Trim(), base_ComboBox.SearchText);
                        }
                    }
                }


            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void btn_저장_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Focus();
                bool _Error = new CoreBusiness().BaseForm1_A10(_MenuSettingEntity,fpMain,_MenuSettingEntity.BASE_TABLE.Split('/')[0]);
                if (!_Error)
                {
                    CustomMsg.ShowMessage("저장되었습니다.");
                    // DisplayMessage("저장 되었습니다.");

                    fpMain.Sheets[0].Rows.Count = 0;
                    MainFind_DisplayData();

                    DataTable pDataTable =  new CoreBusiness().BASE_MENU_SETTING_R10(_Menu_name,_xFpSpread,"");

                    Function.Core.initializeSpread(pDataTable, _xFpSpread, _Menu_name, _UserAccount);


                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
    }
}
