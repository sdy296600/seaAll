using CoFAS.NEW.MES.Core;
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

namespace CoFAS.NEW.MES.POP
{
    public partial class PM_LIST : Form
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
        #endregion


        #region ○ 생성자

        public PM_LIST(string userEntity)
        {
            _UserAccount = userEntity;

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
                _MenuSettingEntity.BASE_ORDER = "";
                _MenuSettingEntity.MENU_WINDOW_NAME = this.Name;
                _MenuSettingEntity.BASE_TABLE = "";

                DataTable pDataTable =  new CoreBusiness().BASE_MENU_SETTING_R10(_MenuSettingEntity.MENU_WINDOW_NAME,fpMain,_MenuSettingEntity.BASE_TABLE.Split('/')[0]);
                ///InitializeControl(pDataTable);
                if (pDataTable != null)
                {

                    CoFAS.NEW.MES.Core.Function.Core.initializeSpread(pDataTable, fpMain, this.Name, "admin");
                    //Function.Core.InitializeControl(pDataTable1, fpMain, this, panel1, _MenuSettingEntity);
                }

                fpMain.ZoomFactor = 2;

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

                string sql = $@"SELECT COLUMN1
                                 ,BASE_DATE
                                 ,BASE_DATE_SEQ ,
                                 
                                 (select COUNT(*)
                                 from [dbo].[OPC_MST_OK] 
                                 where READ_DATE > (
                                 select BASE_DATE
                                 from EQUIPMENT_PREVENTION_DETAIL
                                 where ID =1 ))
                                 + 
                                 (select COUNT(*)
                                 from [dbo].[OPC_MST_NG] 
                                 where READ_DATE > (
                                 select BASE_DATE
                                 from EQUIPMENT_PREVENTION_DETAIL
                                 where ID =1 )) AS 현재값
                                 FROM EQUIPMENT_PREVENTION_DETAIL A
                                 WHERE A.ID = 1

                                 UNION ALL
                                 
                                 SELECT COLUMN1,BASE_DATE,BASE_DATE_SEQ ,ISNULL(SUM(DATEDIFF(second, B.START_INSTRUCT_DATE, B.END_INSTRUCT_DATE)),0)/36000AS 현재값
                                 FROM EQUIPMENT_PREVENTION_DETAIL A
                                 LEFT JOIN [dbo].[PRODUCTION_INSTRUCT] B ON A.BASE_DATE < B.INSTRUCT_DATE
                                 AND B.USE_YN = 'Y'
                                 AND B.START_INSTRUCT_DATE IS NOT NULL
                                 AND B.END_INSTRUCT_DATE IS NOT NULL
                                 WHERE 1=1
                                 AND A.ID = 2
                                 GROUP BY COLUMN1,BASE_DATE,BASE_DATE_SEQ

                                 UNION ALL

                                 SELECT COLUMN1,BASE_DATE,BASE_DATE_SEQ ,ISNULL(DATEDIFF(DAY, BASE_DATE, GETDATE()),0) AS 현재값
                                 FROM EQUIPMENT_PREVENTION_DETAIL A
                                 WHERE 1=1 AND A.ID = 3";

                DataTable _DataTable =  new CoreBusiness().SELECT(sql);

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
            try
            {
                this.Close();
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }


        private void label5_Click(object sender, EventArgs e)
        {
            //BaseFormSetting baseFormSetting = new BaseFormSetting(this.Name,_UserAccount);

            //baseFormSetting.Show();
        }
    }
}
