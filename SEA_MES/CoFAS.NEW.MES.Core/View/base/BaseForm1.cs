using FarPoint.Win.Spread;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Business;
using DevExpress.Spreadsheet;
using System.Collections.Generic;
using System.Threading;
using FarPoint.Win.Spread.CellType;
using System.Reflection;
using System.Data.SqlClient;

namespace CoFAS.NEW.MES.Core
{
    public partial class BaseForm1 : frmBaseNone
    {


        #region ○ 변수선언


        public SystemLogEntity _SystemLogEntity = null;
        private bool _FirstYn = true;
        public string _UserAccount = string.Empty;
        private string _filename = string.Empty;
        public Loading waitform = new Loading();
        #endregion

        #region ○ 생성자

        public BaseForm1()
        {
           
            InitializeComponent();

            Load += new EventHandler(Form_Load);
            Activated += new EventHandler(Form_Activated);
            MdiChildActivate += new EventHandler(Form_Activated);
            FormClosing += new FormClosingEventHandler(Form_Closing);
        }

        #endregion

        #region ○ 폼 이벤트 영역

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void Form_Activated(object sender, EventArgs e)
        {
            Function.Core.InitializeButton(_pMenuSettingEntity, MainForm);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                if (this._pMenuSettingEntity != null)
                {
                    DataTable pDataTable =  new CoreBusiness().BASE_MENU_SETTING_R10(this._pMenuSettingEntity.MENU_WINDOW_NAME,fpMain,this._pMenuSettingEntity.BASE_TABLE.Split('/')[0]);

                    if (pDataTable != null)
                    {
                        //InitializeControl(pDataTable);

                        Function.Core.initializeSpread(pDataTable, fpMain, this._pMenuSettingEntity.MENU_WINDOW_NAME, MainForm.UserEntity.user_account);
                        Function.Core.InitializeControl(pDataTable, fpMain, this, _PAN_WHERE, _pMenuSettingEntity);
                        _FirstYn = false;
                        _UserAccount = MainForm.UserEntity.user_account;
                    }
                }
              

                _SystemLogEntity = new SystemLogEntity();

                // 버튼이벤트 생성
                SearchButtonClicked += new EventHandler(_SearchButtonClicked);
                PrintButtonClicked += new EventHandler(_PrintButtonClicked);
                DeleteButtonClicked += new EventHandler(_DeleteButtonClicked);
                SaveButtonClicked += new EventHandler(_SaveButtonClicked);
                ImportButtonClicked += new EventHandler(_ImportButtonClicked);
                ExportButtonClicked += new EventHandler(_ExportButtonClicked);
                InitialButtonClicked += new EventHandler(_InitialButtonClicked);
                AddItemButtonClicked += new EventHandler(_AddItemButtonClicked);
                CloseButtonClicked += new EventHandler(_CloseButtonClicked);

                fpMain._ChangeEventHandler += FpMain_Change;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region ○ 버튼 이벤트 영역

        public virtual void _SearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                MainFind_DisplayData();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        
        public virtual void _PrintButtonClicked(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        
        public virtual void _DeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                DialogResult _DialogResult1 = CustomMsg.ShowMessage("삭제하시겠습니까?", "Question", MessageBoxButtons.YesNo);
                if (_DialogResult1 == DialogResult.Yes)
                {
                    if (fpMain.Sheets[0].RowHeader.Cells[fpMain.Sheets[0].ActiveRowIndex, 0].Text == "입력")
                    {
                        FpSpreadManager.SpreadRowRemove(fpMain, 0, fpMain.Sheets[0].ActiveRowIndex);
                    }
                    else
                    {
                        fpMain.Sheets[0].RowHeader.Cells[fpMain.Sheets[0].ActiveRowIndex, 0].Text = "삭제";
                        fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "USE_YN", "N");

                        MainDelete_InputData();
                    }
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        
        public virtual void _SaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (Function.Core._SaveButtonClicked(fpMain))
                {

                    if (fpMain.Sheets[0].Rows.Count > 0)
                    {
                        MainSave_InputData();

                        CustomMsg.ShowMessage("저장되었습니다.");
                        DisplayMessage("저장 되었습니다.");
                    }

       
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        
        public virtual void _ImportButtonClicked(object sender, EventArgs e)
        {
            try
            {
             
                if (this.Text == "소모품입고관리" || this.Text == "소모품출고관리")
                {
                    OpenFileDialog openFileDialog1 = new OpenFileDialog();
                    openFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        _filename = openFileDialog1.FileName;

                        int lastrow = 0;

                        if (fpMain.Sheets[0].Rows.Count > 0)
                        {
                           lastrow = fpMain.Sheets[0].Rows.Count - 1;
                        }
                        

                        DevExpress.XtraSpreadsheet.SpreadsheetControl sc = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                        sc.LoadDocument(_filename, DocumentFormat.Xlsx);
                        List<CONSUMABLE_MST> list = new List<CONSUMABLE_MST>();
                        foreach (Worksheet sheet in sc.Document.Worksheets)
                        {
                            for (int i = 1; i < 10000; i++)
                            {
                                #region ○ 변수 정리
                                decimal dec = 0;
                                DateTime dateTime = DateTime.Now;
                                if (sheet.Rows[i][0].Value.ToString() == "")
                                {
                                    continue;
                                }
                                CONSUMABLE_MST CONSUMABLE_MST = new CONSUMABLE_MST();

                                if(this.Text == "소모품입고관리" && sheet.Rows[i][0].Value.ToString().Trim() == "입고")
                                {
                                    CONSUMABLE_MST.TYPE = sheet.Rows[i][0].Value.ToString().Trim();
                                }

                                else if (this.Text == "소모품출고관리" && sheet.Rows[i][0].Value.ToString().Trim() == "출고")
                                {
                                    CONSUMABLE_MST.TYPE = sheet.Rows[i][0].Value.ToString().Trim();
                                }

                                else if (this.Text == "소모품입고관리" && sheet.Rows[i][0].Value.ToString().Trim() != "입고")
                                {
                                    continue;
                                }

                                else if (this.Text == "소모품출고관리" && sheet.Rows[i][0].Value.ToString().Trim() != "출고")
                                {
                                    continue;
                                }

                                CONSUMABLE_MST.RESOURCE_NO = sheet.Rows[i][1].Value.ToString().Trim();
                                CONSUMABLE_MST.RESOURCE_TYPE = sheet.Rows[i][2].Value.ToString().Trim();

                                if (decimal.TryParse(sheet.Rows[i][3].Value.ToString().Trim(), out dec))
                                {
                                    CONSUMABLE_MST.USE_QTY = dec;
                                }
                                else
                                {
                                    CONSUMABLE_MST.USE_QTY = 0;
                                }

                                CONSUMABLE_MST.USE_LOCATION = sheet.Rows[i][4].Value.ToString().Trim();
                                
                                if (DateTime.TryParse(sheet.Rows[i][5].Value.ToString().Trim(), out dateTime))
                                {
                                    CONSUMABLE_MST.USE_TIME = dateTime;
                                }
                                else
                                {
                                    CONSUMABLE_MST.USE_TIME = DateTime.Now;
                                }

                                CONSUMABLE_MST.COMMENT = sheet.Rows[i][6].Value.ToString().Trim();

                                list.Add(CONSUMABLE_MST);
                                Function.Core._AddItemButtonClicked(fpMain, _UserAccount);
                                #endregion
                            }
                        }

                        fpMain.Sheets[0].Visible = false;
                        for (int i = 0; i < list.Count; i++)
                        {

                            foreach (PropertyInfo info in new CONSUMABLE_MST().GetType().GetProperties())
                            {
                                if (fpMain.Sheets[0].Columns[info.Name].CellType.GetType() == typeof(MYComboBoxCellType))
                                {
                                    fpMain.Sheets[0].SetText(i + lastrow, fpMain.Sheets[0].Columns[info.Name].Index, new CONSUMABLE_MST().GetType().GetProperty(info.Name).GetValue(list[i]).ToString());
                                }
                                else if (fpMain.Sheets[0].Columns[info.Name].CellType.GetType() == typeof(ComboBoxCellType))
                                {
                                    fpMain.Sheets[0].SetText(i + lastrow, fpMain.Sheets[0].Columns[info.Name].Index, new CONSUMABLE_MST().GetType().GetProperty(info.Name).GetValue(list[i]).ToString());
                                }
                                else
                                {
                                    fpMain.Sheets[0].SetValue(i + lastrow, info.Name, new CONSUMABLE_MST().GetType().GetProperty(info.Name).GetValue(list[i]));
                                }


                            }

                            fpMain.Sheets[0].RowHeader.Cells[i+ lastrow, 0].Text = "입력";
                            fpMain.Sheets[0].SetValue(i + lastrow, "USE_TIME", DateTime.Now);
                            fpMain.Sheets[0].SetValue(i + lastrow, "USE_YN", "Y");
                            fpMain.Sheets[0].SetValue(i + lastrow, "UP_USER", _UserAccount);
                            fpMain.Sheets[0].SetValue(i + lastrow, "UP_DATE", DateTime.Now);
                            fpMain.Sheets[0].SetValue(i + lastrow, "REG_USER", _UserAccount);
                            fpMain.Sheets[0].SetValue(i + lastrow, "REG_DATE", DateTime.Now);

                        }
                        fpMain.Sheets[0].Visible = true;
                    }
                }
                else if (this.Text == "생산계획")
                {
                  
                    OpenFileDialog openFileDialog1 = new OpenFileDialog();
                    openFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        waitform.Show(this, "준비중입니다.");
                        _filename = openFileDialog1.FileName;

                        DevExpress.XtraSpreadsheet.SpreadsheetControl sc = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                        sc.LoadDocument(_filename, DocumentFormat.Xlsx);

                        SqlConnection con;
                        con = new SqlConnection(DBManager.PrimaryConnectionString);

                        SqlCommand cmd;
                        cmd = new SqlCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();

                        cmd.CommandText = $@"DELETE FROM [dbo].[PRODUCTION_PLAN]
                                             DELETE FROM [dbo].[PRODUCTION_INSTRUCT]
                                             DELETE FROM [dbo].[PRODUCTION_RESULT]
                                             DELETE FROM [dbo].[EQUIPMENT_STOP]
                                             DELETE FROM IN_STOCK_DETAIL";
                        cmd.ExecuteNonQuery();
                        foreach (Worksheet sheet in sc.Document.Worksheets)
                        {
                            if (sheet.Name == "생산시간")
                            {
                                 Application.DoEvents();
                                for (int i = 1; i < 20000; i++)
                                {
                                    string line= "";
                                    if (sheet.Rows[i][0].Value.ToString().Trim() == "")
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        switch (sheet.Rows[i][0].Value.ToString().Trim())
                                        {
                                            case "1PC":
                                                line = "CD14001";
                                                break;
                                            case "2PC":
                                                line = "CD14002";
                                                break;
                                            default:
                                                line = "CD14003";
                                                break;

                                        }
                                    }

                                    DateTime ckdateTime;

                                    int ckint;

                                    if (!DateTime.TryParse(sheet.Rows[i][1].Value.ToString().Trim(), out ckdateTime))
                                    {
                                        continue;
                                    }
                                    if (!DateTime.TryParse(sheet.Rows[i][7].Value.ToString().Trim(), out ckdateTime))
                                    {
                                        continue;
                                    }
                                    if (!DateTime.TryParse(sheet.Rows[i][8].Value.ToString().Trim(), out ckdateTime))
                                    {
                                        continue;
                                    }
                                    if (!int.TryParse(sheet.Rows[i][6].Value.ToString().Trim(), out ckint))
                                    {
                                        continue;
                                    }
                                    if (!int.TryParse(sheet.Rows[i][9].Value.ToString().Trim(), out ckint))
                                    {
                                        continue;
                                    }
                                    DateTime 작업시작시간1 =   Convert.ToDateTime(sheet.Rows[i][7].Value.ToString().Trim());
                                    DateTime 작업종료시간1 =   Convert.ToDateTime(sheet.Rows[i][8].Value.ToString().Trim());
                                    #region ○ 변수 정리
                                    string 라인명          = line;
                                    DateTime 생산일자      = Convert.ToDateTime(sheet.Rows[i][1].Value.ToString().Trim());
                                    string 생산계획번호    = sheet.Rows[i][2].Value.ToString().Trim();
                                    string PARTNO          = sheet.Rows[i][3].Value.ToString().Trim();
                                    string PartName        = sheet.Rows[i][4].Value.ToString().Trim();
                                    DateTime 작업시작시간  = 생산일자.AddHours(작업시작시간1.Hour).AddMinutes(작업시작시간1.Minute);
                                    DateTime 작업종료시간;

                                    string ck1 = 작업시작시간1.ToString("tt");

                                    string ck2 = 작업종료시간1.ToString("tt");

                                    if (ck1 == "오후" && ck2 == "오전")
                                    {
                                         작업종료시간  = 생산일자.AddDays(1).AddHours(작업종료시간1.Hour).AddMinutes(작업종료시간1.Minute);
                                    }
                                    else
                                    {
                                         작업종료시간  = 생산일자.AddHours(작업종료시간1.Hour).AddMinutes(작업종료시간1.Minute);
                                    }

                                    int 작업량             = Convert.ToInt32(sheet.Rows[i][6].Value.ToString().Trim());
                                    int 작업인원           = Convert.ToInt32(sheet.Rows[i][9].Value.ToString().Trim());

                                    if(작업량 == 0)
                                    {
                                        continue;
                                    }

                                    cmd.CommandText = $@"IF(NOT EXISTS(SELECT 1 FROM [dbo].[PRODUCTION_PLAN] WHERE [OUT_CODE] =  '{생산계획번호}' and [PLAN_DATE] = '{생산일자.ToString("yyyy-MM-dd HH:mm:ss")}'))
                                                    BEGIN
                                                                       INSERT INTO [dbo].[PRODUCTION_PLAN]
                                                                      ([OUT_CODE]
                                                                      ,[PLAN_DATE]
                                                                      ,[LINE]
                                                                      ,[STOCK_MST_ID]
                                                                      ,[STOCK_MST_OUT_CODE]
                                                                      ,[STOCK_MST_STANDARD]
                                                                      ,[STOCK_MST_TYPE]
                                                                      ,[WORK_CAPA_STD_OPERATOR]
                                                                      ,[WORK_CAPA_WORKING_HR_SHIFT]
                                                                      ,[WORK_CAPA_AFTER_CAPACITY]
                                                                      ,[PLAN_QTY]
                                                                      ,[INSTRUCT_QTY]
                                                                      ,[REMAIN_QTY]
                                                                      ,[SORT]
                                                                      ,[COMMENT]
                                                                      ,[COMPLETE_YN]
                                                                      ,[USE_YN]
                                                                      ,[REG_USER]
                                                                      ,[REG_DATE]
                                                                      ,[UP_USER]
                                                                      ,[UP_DATE])
                                                                 select 
                                                                       '{생산계획번호}'
                                                                      ,'{생산일자.ToString("yyyy-MM-dd HH:mm:ss")}'
                                                                      ,'{라인명}'
                                                                      ,A.ID
                                                                      ,A.ID
                                                                      ,A.ID
                                                                      ,A.ID
                                                                      ,B.ID
                                                                      ,B.ID
                                                                      ,'{작업량}'
                                                                      ,'{작업량}'
                                                                      ,0
                                                                      ,0
                                                                      ,0
                                                                      ,''
                                                                      ,'N'
                                                                      ,'Y'
                                                                      ,'{MainForm.UserEntity.user_account}'
                                                                      ,GETDATE()
                                                                      ,'{MainForm.UserEntity.user_account}'
                                                                      ,GETDATE()
                                                                 from STOCK_MST A
                                                                 INNER JOIN [dbo].[WORK_CAPA] B ON A.COLUMN9 = B.CODE_MST_CODE AND B.STD_OPERATOR = '{작업인원}' and  WORKING_HR_SHIFT = '8'
                                                                 where A.OUT_CODE = '{PARTNO}'
                                                     END
                                                     ELSE
                                                     BEGIN
                                                              UPDATE [dbo].[PRODUCTION_PLAN]
                                                                 SET 
                                                                     [WORK_CAPA_AFTER_CAPACITY] = [WORK_CAPA_AFTER_CAPACITY] + {작업량}
                                                                    ,[PLAN_QTY] = [PLAN_QTY]  + {작업량}
                                                              WHERE [OUT_CODE] =  '{생산계획번호}' and [PLAN_DATE] = '{생산일자.ToString("yyyy-MM-dd HH:mm:ss")}'                   
                                                     END";

                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = $@"INSERT INTO [dbo].[PRODUCTION_INSTRUCT]
                                                                    ([PRODUCTION_PLAN_ID]
                                                                    ,[OUT_CODE]       
                                                                    ,[INSTRUCT_DATE]
                                                                    ,[STOCK_MST_ID]
                                                                    ,[STOCK_MST_OUT_CODE]
                                                                    ,[STOCK_MST_STANDARD]
                                                                    ,[STOCK_MST_TYPE]
                                                                    ,[WORK_CAPA_STD_OPERATOR]
                                                                    ,[WORK_CAPA_WORKING_HR_SHIFT]
                                                                    ,[INSTRUCT_QTY]
                                                                    ,[START_INSTRUCT_DATE]
                                                                    ,[END_INSTRUCT_DATE]
                                                                    ,[SORT]
                                                                    ,[COMMENT]
                                                                    ,[COMPLETE_YN]
                                                                    ,[USE_YN]
                                                                    ,[REG_USER]
                                                                    ,[REG_DATE]
                                                                    ,[UP_USER]
                                                                    ,[UP_DATE])
                                                         select  A.ID
                                                                ,A.OUT_CODE
                                                         	    ,A.[PLAN_DATE]
                                                                ,A.STOCK_MST_ID
                                                                ,A.STOCK_MST_ID
                                                                ,A.STOCK_MST_ID
                                                                ,A.STOCK_MST_ID
                                                                ,A.WORK_CAPA_STD_OPERATOR
                                                                ,A.WORK_CAPA_WORKING_HR_SHIFT
                                                                ,'{작업량}'
                                                                ,'{작업시작시간.ToString("yyyy-MM-dd HH:mm:ss")}'
                                                                ,'{작업종료시간.ToString("yyyy-MM-dd HH:mm:ss")}'
                                                                ,A.SORT
                                                                ,A.COMMENT
                                                                ,'N'
                                                                ,'Y'
                                                                ,A.REG_USER
                                                                ,A.REG_DATE
                                                                ,A.UP_USER
                                                                ,A.UP_DATE
                                                         from [dbo].[PRODUCTION_PLAN] A
                                                          WHERE [OUT_CODE] =  '{생산계획번호}' and [PLAN_DATE] = '{생산일자.ToString("yyyy-MM-dd HH:mm:ss")}'";

                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = $@"
                                                         INSERT INTO [dbo].[PRODUCTION_RESULT]
                                                                    ([LOT_NO]
                                                                    ,[PRODUCTION_INSTRUCT_ID]
                                                                    ,[STOCK_MST_ID]
                                                                    ,[STOCK_MST_OUT_CODE]
                                                                    ,[STOCK_MST_STANDARD]
                                                                    ,[STOCK_MST_TYPE]
                                                                    ,[WORK_TYPE]
                                                                    ,[RESULT_TYPE]
                                                                    ,[OK_QTY]
                                                                    ,[NG_QTY]
                                                                    ,[TOTAL_QTY]
                                                                    ,[START_DATE]
                                                                    ,[END_DATE]
                                                                    ,[COMMENT]
                                                                    ,[USE_YN]
                                                                    ,[REG_USER]
                                                                    ,[REG_DATE]
                                                                    ,[UP_USER]
                                                                    ,[UP_DATE])
                                                         SELECT A.ID
                                                         ,A.ID
                                                         ,A.STOCK_MST_ID
                                                         ,A.STOCK_MST_ID
                                                         ,A.STOCK_MST_ID
                                                         ,A.STOCK_MST_ID
                                                         ,''
                                                         ,'CD16001'
                                                         ,A.INSTRUCT_QTY
                                                         ,0
                                                         ,A.INSTRUCT_QTY
                                                         ,A.START_INSTRUCT_DATE
                                                         ,A.END_INSTRUCT_DATE
                                                         ,A.COMMENT
                                                         ,A.USE_YN
                                                         ,A.REG_USER
                                                         ,A.REG_DATE
                                                         ,A.UP_USER
                                                         ,A.UP_DATE
                                                         FROM [dbo].[PRODUCTION_INSTRUCT] A
                                                           WHERE 1=1
                                                           AND OUT_CODE =  '{생산계획번호}' 
                                                           AND INSTRUCT_DATE = '{생산일자.ToString("yyyy-MM-dd HH:mm:ss")}'
                                                           AND INSTRUCT_QTY = '{작업량}'";

                                    cmd.ExecuteNonQuery();
                                    #endregion
                                }
                               
                            }
                            else if (sheet.Name == "불량정보")
                            {
                                Application.DoEvents();
                                for (int i = 1; i < 20000; i++)
                                {
                                    #region ○ 변수 정리

                                    string line= "";

                                    if (sheet.Rows[i][0].Value.ToString().Trim() == "")
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        switch (sheet.Rows[i][3].Value.ToString().Trim())
                                        {
                                            case "1PC":
                                                line = "CD14001";
                                                break;
                                            case "2PC":
                                                line = "CD14002";
                                                break;
                                            default:
                                                line = "CD14003";
                                                break;

                                        }
                                    }

                                    DateTime ckdateTime;
                                    int ckint;

                                    if (!DateTime.TryParse(sheet.Rows[i][1].Value.ToString().Trim(), out ckdateTime))
                                    {
                                        continue;
                                    }

                                   

                                    if (!int.TryParse(sheet.Rows[i][6].Value.ToString().Trim(), out ckint))
                                    {
                                        continue;
                                    }
                    
                                    //string 라인명          = line;
                                    //DateTime 생산일자      = Convert.ToDateTime(sheet.Rows[i][1].Value.ToString().Trim());
                                    //string 생산계획번호    = sheet.Rows[i][2].Value.ToString().Trim();
                                    //string PARTNO          = sheet.Rows[i][3].Value.ToString().Trim();
                                    //string PartName        = sheet.Rows[i][4].Value.ToString().Trim();
                                    //DateTime 작업시작시간  = 생산일자.AddHours(작업시작시간1.Hour).AddMinutes(작업시작시간1.Minute);
                                    //DateTime 작업종료시간  = 생산일자.AddHours(작업종료시간1.Hour).AddMinutes(작업종료시간1.Minute);
                                    //int 작업량             = Convert.ToInt32(sheet.Rows[i][7].Value.ToString().Trim());
                                    //int 작업인원           = Convert.ToInt32(sheet.Rows[i][8].Value.ToString().Trim());

                                    string 라인명     = line;
                                    DateTime 생산일자 = Convert.ToDateTime(sheet.Rows[i][1].Value.ToString().Trim());
                                    string PARTNO     = sheet.Rows[i][0].Value.ToString().Trim();
                                    string notno      = sheet.Rows[i][7].Value.ToString().Trim();
                                    string STATION    = sheet.Rows[i][4].Value.ToString().Trim();
                                    string 유형       = sheet.Rows[i][5].Value.ToString().Trim();
                                    int 수량          = Convert.ToInt32(sheet.Rows[i][6].Value.ToString().Trim());


                                    cmd.CommandText = $@"INSERT INTO [dbo].[PRODUCTION_RESULT]
                                                         ([LOT_NO]
                                                         ,[PRODUCTION_INSTRUCT_ID]
                                                         ,[STOCK_MST_ID]
                                                         ,[STOCK_MST_OUT_CODE]
                                                         ,[STOCK_MST_STANDARD]
                                                         ,[STOCK_MST_TYPE]
                                                         ,[WORK_TYPE]
                                                         ,[RESULT_TYPE]
                                                         ,[OK_QTY]
                                                         ,[NG_QTY]
                                                         ,[TOTAL_QTY]
                                                         ,[START_DATE]
                                                         ,[END_DATE]
                                                         ,[COMMENT]
                                                         ,[USE_YN]
                                                         ,[REG_USER]
                                                         ,[REG_DATE]
                                                         ,[UP_USER]
                                                         ,[UP_DATE])
                                                         SELECT TOP 1 
                                                          A.ID
                                                         ,A.ID
                                                         ,A.STOCK_MST_ID
                                                         ,A.STOCK_MST_ID
                                                         ,A.STOCK_MST_ID
                                                         ,A.STOCK_MST_ID
                                                         ,''
                                                         ,'CD16002'
                                                         ,0
                                                         ,{수량}
                                                         ,{수량}
                                                         ,A.START_INSTRUCT_DATE
                                                         ,A.END_INSTRUCT_DATE
                                                         ,'{유형}'
                                                         ,A.USE_YN
                                                         ,A.REG_USER
                                                         ,A.REG_DATE
                                                         ,A.UP_USER
                                                         ,A.UP_DATE
                                                         FROM [dbo].[PRODUCTION_INSTRUCT] A
                                                         inner join [dbo].[STOCK_MST] B ON A.STOCK_MST_ID = B.ID AND B.OUT_CODE = '{PARTNO}'
                                                           WHERE 1=1
                                                           AND A.INSTRUCT_DATE = '{생산일자.ToString("yyyy-MM-dd HH:mm:ss")}'";

                                    cmd.ExecuteNonQuery();
                                    #endregion      
                                }
                            }
                            else if (sheet.Name == "비가동인원")
                            {
                                Application.DoEvents();
                                for (int i = 1; i < 10000; i++)
                                {
                                    if (sheet.Rows[i][0].Value.ToString().Trim() == "")
                                    {
                                        continue;
                                    }
                         
                                    DateTime ckdateTime;
                                    int ckint;

                                    if (!DateTime.TryParse(sheet.Rows[i][3].Value.ToString().Trim(), out ckdateTime))
                                    {
                                        continue;
                                    }
                                    DateTime 발생일T = DateTime.Parse(sheet.Rows[i][3].Value.ToString().Trim());
                                    DateTime 비가동시작;
                                    DateTime 비가동종료;


                                    if (!DateTime.TryParse(sheet.Rows[i][4].Value.ToString().Trim(), out ckdateTime))
                                    {
                                        continue;
                                    }
                                    if (!DateTime.TryParse(sheet.Rows[i][5].Value.ToString().Trim(), out ckdateTime))
                                    {
                                        continue;
                                    }
                                    비가동시작 = DateTime.Parse(sheet.Rows[i][4].Value.ToString().Trim());
                                    비가동종료 = DateTime.Parse(sheet.Rows[i][5].Value.ToString().Trim());

                                    //if (!DateTime.TryParse(sheet.Rows[i][4].Value.ToString().Trim(), out ckdateTime))
                                    //{
                                    //    비가동시작 = DateTime.Parse(sheet.Rows[i][4].Value.ToString().Trim());
                                    //}
                                    //else
                                    //{
                                    //    비가동시작 = DateTime.Parse(sheet.Rows[i][6].Value.ToString().Trim());
                                    //}

                                    //if (!DateTime.TryParse(sheet.Rows[i][5].Value.ToString().Trim(), out ckdateTime))
                                    //{
                                    //    비가동종료 = DateTime.Parse(sheet.Rows[i][4].Value.ToString().Trim());
                                    //}
                                    //else
                                    //{
                                    //    비가동종료 = DateTime.Parse(sheet.Rows[i][7].Value.ToString().Trim());
                                    //}
                                    비가동시작 = 발생일T.AddHours(비가동시작.Hour).AddMinutes(비가동시작.Minute);
                                    비가동종료 = 발생일T.AddHours(비가동종료.Hour).AddMinutes(비가동종료.Minute);
                                    #region ○ 변수 정리
                                    string 비가동코드   = sheet.Rows[i][0].Value.ToString().Trim();
                                    string 비가동원인   = sheet.Rows[i][1].Value.ToString().Trim();
                                    string 생산계획번호 = sheet.Rows[i][2].Value.ToString().Trim();
                                    //string 제품코드     = sheet.Rows[i][3].Value.ToString().Trim();
                                    string 발생일       = sheet.Rows[i][3].Value.ToString().Trim();
                                    cmd.CommandText = $@"INSERT INTO [dbo].[EQUIPMENT_STOP]
                                                        ([TYPE]
                                                        ,[PRODUCTION_INSTRUCT_ID]
                                                        ,[EQUIPMENT_ID]
                                                        ,[EQUIPMENT_NAME]
                                                        ,[START_TIME]
                                                        ,[END_TIME]                                                
                                                        ,[COMMENT]
                                                        ,[USE_YN]
                                                        ,[UP_USER]
                                                        ,[UP_DATE]
                                                        ,[REG_USER]
                                                        ,[REG_DATE])
		                                               select '{비가동코드}'
		                                               ,A.ID
		                                               ,0
		                                               ,0
		                                               ,'{비가동시작.ToString("yyyy-MM-dd HH:mm:ss")}'
		                                               ,'{비가동종료.ToString("yyyy-MM-dd HH:mm:ss")}'
		                                               ,'{비가동원인}'
		                                               ,'Y'
		                                               ,'이세민'
		                                               ,'{비가동시작.ToString("yyyy-MM-dd HH:mm:ss")}'
		                                               ,'이세민'
                                                       ,'{비가동시작.ToString("yyyy-MM-dd HH:mm:ss")}'
		                                               from [dbo].[PRODUCTION_INSTRUCT] A
                                                       INNER JOIN [dbo].[STOCK_MST] B ON A.STOCK_MST_ID = B.ID
		                                               where  A.OUT_CODE = '{생산계획번호}'
                                                       AND A.START_INSTRUCT_DATE  < '{비가동시작.ToString("yyyy-MM-dd HH:mm:ss")}' 
                                                       AND A.END_INSTRUCT_DATE    > '{비가동시작.ToString("yyyy-MM-dd HH:mm:ss")}' 
                                                       AND A.START_INSTRUCT_DATE  < '{비가동종료.ToString("yyyy-MM-dd HH:mm:ss")}' 
                                                       AND A.END_INSTRUCT_DATE    > '{비가동종료.ToString("yyyy-MM-dd HH:mm:ss")}'";
                                                       


                                      
                                    cmd.ExecuteNonQuery();
                                    #endregion
                                }
                            }

                        }

                        cmd.CommandText = $@"INSERT INTO [dbo].[EQUIPMENT_STOP]
           ([TYPE]
           ,[PRODUCTION_INSTRUCT_ID]
           ,[EQUIPMENT_ID]
           ,[EQUIPMENT_NAME]
           ,[START_TIME]
           ,[END_TIME]

           ,[COMMENT]
           ,[USE_YN]
           ,[UP_USER]
           ,[UP_DATE]
           ,[REG_USER]
           ,[REG_DATE])
SELECT DISTINCT　B.code 
,A.ID
,0
,0
,B.code_etc1
,B.code_etc2
,''
,'Y'
,'이세민'
,B.code_etc1
,'이세민'
,B.code_etc1
FROM [dbo].[PRODUCTION_INSTRUCT] A
INNER JOIN 
(
select a.ID, DATEADD(MINUTE ,code_etc1,a.INSTRUCT_DATE) as code_etc1
, DATEADD(MINUTE ,code_etc1 + code_etc2,a.INSTRUCT_DATE) as code_etc2
,code
from [dbo].[PRODUCTION_INSTRUCT] a
LEFT JOIN 
 (SELECT CONVERT(FLOAT,code_etc2) as code_etc2
    , CONVERT(INT,code_etc1) AS code_etc1
     , code_etc3
     , code
     FROM [dbo].[Code_Mst] 
     where 1=1 
      AND code_type = 'CD12'
      AND code_description = '고정비가동'
 AND CODE_ETC1 != ''
      ) b ON 1=1    
)B  ON  B.code_etc1 >= A.START_INSTRUCT_DATE   
	 AND B.code_etc1 < ISNULL(A.END_INSTRUCT_DATE,GETDATE());
 
UPDATE [dbo].[EQUIPMENT_STOP]
SET TYPE = B.CODE
from [dbo].[EQUIPMENT_STOP] A
inner join Code_Mst B ON A.TYPE = B.code_name;
UPDATE [dbo].[PRODUCTION_RESULT]

SET RESULT_TYPE = B.CODE
,COMMENT = ''
from [dbo].[PRODUCTION_RESULT] A
inner join Code_Mst B ON A.COMMENT = B.code_name;

UPDATE [dbo].[PRODUCTION_RESULT]
SET REG_DATE = B.START_INSTRUCT_DATE

FROM [dbo].[PRODUCTION_RESULT] A
INNER JOIN [dbo].[PRODUCTION_INSTRUCT] B ON A.PRODUCTION_INSTRUCT_ID =B.ID;";

                        //                        UPDATE[dbo].[EQUIPMENT_STOP]
                        //SET TYPE = B.CODE
                        //from[dbo].[EQUIPMENT_STOP] A
                        //inner join Code_Mst B ON A.TYPE = B.code_name;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                

                }
      
            }
            catch (Exception pExcption)
            {
                waitform.Close();
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
            finally
            {
                waitform.Close();
            }
        }
        
        public virtual void _ExportButtonClicked(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        
        public virtual void _InitialButtonClicked(object sender, EventArgs e)
        {
            try
            {
                DataTable pDataTable = new CoreBusiness().BASE_MENU_SETTING_R10(this._pMenuSettingEntity.MENU_WINDOW_NAME, fpMain, this._pMenuSettingEntity.BASE_TABLE.Split('/')[0]);

                Function.Core.InitializeControl(pDataTable, fpMain, this, _PAN_WHERE, _pMenuSettingEntity);
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        
        public virtual void _AddItemButtonClicked(object sender, EventArgs e)
        {
            try
            {
                Function.Core._AddItemButtonClicked(fpMain, MainForm.UserEntity.user_account);
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        
        public virtual void _CloseButtonClicked(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        

        #endregion

        #region ○ 스프레드 영역

        private void FpMain_Change(object sender, ChangeEventArgs e)
        {
            try
            {
                xFpSpread xFp = sender as xFpSpread;


                string pHeaderLabel = xFp.Sheets[0].RowHeader.Cells[e.Row, 0].Text;
                switch (pHeaderLabel)
                {
                    case "":
                        xFp.Sheets[0].RowHeader.Cells[e.Row, 0].Text = "수정";
                        break;
                    case "입력":
                        break;
                    case "수정":
                        break;
                    case "삭제":
                        break;
                }
                if (xFp.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(FarPoint.Win.Spread.CellType.NumberCellType))
                {
                    Function.Core._AddItemSUM(xFp);
                    xFp.ActiveSheet.SetActiveCell(e.Row, e.Column);
                    xFp.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Center, FarPoint.Win.Spread.HorizontalPosition.Center);
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region ○ 데이터 영역

        public virtual void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);
              
                DataTable _DataTable = new CoreBusiness().BaseForm1_R10(_PAN_WHERE,this._pMenuSettingEntity);

                Function.Core.DisplayData_Set(_DataTable, fpMain);

                //if (this.Text == "작업지시조회")
                //{
                //    DataTable _DataTable_order = new SI_Business().PRODUCTION_ORDER_R10(_PAN_WHERE);
                //    Function.Core.DisplayData_Set(_DataTable_order, fpMain);
                //}

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

        public virtual void MainSave_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Focus();


                 
                bool _Error = new CoreBusiness().BaseForm1_A10(this._pMenuSettingEntity,fpMain,this._pMenuSettingEntity.BASE_TABLE.Split('/')[0]);
                
                if (!_Error)
                {


                    fpMain.Sheets[0].Rows.Count = 0;
                    MainFind_DisplayData();
                }
            }
            catch (Exception pExcption)
            {
                int start = pExcption.Message.IndexOf(" (")+1;
                int end = pExcption.Message.IndexOf(")", start)+1;
                string constraintName = pExcption.Message.Substring(start, end - start);
                CustomMsg.ShowExceptionMessage($"중복 값을 입력 하실수 없습니다. 중복값 {constraintName} 입니다.", "Error", MessageBoxButtons.OK);
            }
            finally
            {
                DevExpressManager.SetCursor(this, Cursors.Default);
            }
        }

        public virtual void MainDelete_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Focus();

                bool _Error = new CoreBusiness().BaseForm1_A10(this._pMenuSettingEntity,fpMain,this._pMenuSettingEntity.BASE_TABLE.Split('/')[0]);
                if (!_Error)
                {
                    CustomMsg.ShowMessage("삭제되었습니다.");
                    DisplayMessage("삭제 되었습니다.");

                    fpMain.Sheets[0].Rows.Count = 0;
                    MainFind_DisplayData();
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

      
    }
}
