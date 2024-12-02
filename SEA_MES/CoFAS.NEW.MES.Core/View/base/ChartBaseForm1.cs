using FarPoint.Win.Spread;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Business;
using FarPoint.Win.Spread.CellType;
using System.Text;
using DevExpress.XtraCharts;
using System.Linq;

namespace CoFAS.NEW.MES.Core
{
    public partial class ChartBaseForm1 : frmBaseNone
    {

        #region ○ 변수선언


        public SystemLogEntity _SystemLogEntity = null;
        public bool _FirstYn = true;
        XYDiagram diagram;
        public string _Mst_Id = string.Empty;
        //public MenuSettingEntity _MenuSettingEntity = null;
        #endregion

        #region ○ 생성자

        public ChartBaseForm1()
        {

            InitializeComponent();

            Load += new EventHandler(Form_Load);
            Activated += new EventHandler(Form_Activated);
            FormClosing += new FormClosingEventHandler(Form_Closing);
        }

        #endregion

        #region ○ 폼 이벤트 영역

        public void Form_Closing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        public void Form_Activated(object sender, EventArgs e)
        {
            InitializeButton();
        }

        public virtual void Form_Load(object sender, EventArgs e)
        {
            try
            {
                if (this._pMenuSettingEntity != null)
                {
                    string pBASE_TABLE1 = "";
                    string pBASE_TABLE2 = "";
                    if (_pMenuSettingEntity.BASE_TABLE != "")
                    {
                        pBASE_TABLE1 = this._pMenuSettingEntity.BASE_TABLE.Split('/')[0];
                        pBASE_TABLE2 = this._pMenuSettingEntity.BASE_TABLE.Split('/')[1];

                    }


                    DataTable pDataTable1 =  new CoreBusiness().BASE_MENU_SETTING_R10(this._pMenuSettingEntity.MENU_WINDOW_NAME,fpMain,pBASE_TABLE1);
                    if (pDataTable1 != null)
                    {

                        Function.Core.initializeSpread(pDataTable1, fpMain, this._pMenuSettingEntity.MENU_WINDOW_NAME, MainForm.UserEntity.user_account);
                        Function.Core.InitializeControl(pDataTable1, fpMain, this, _PAN_WHERE, _pMenuSettingEntity);
                    }



                }

                //chart2.ChartAreas[0].AxisX.Interval = 1;

                _SystemLogEntity = new SystemLogEntity();

                // 버튼이벤트 생성
                SearchButtonClicked -= new EventHandler(_SearchButtonClicked);
                PrintButtonClicked -= new EventHandler(_PrintButtonClicked);
                DeleteButtonClicked -= new EventHandler(_DeleteButtonClicked);
                SaveButtonClicked -= new EventHandler(_SaveButtonClicked);
                ImportButtonClicked -= new EventHandler(_ImportButtonClicked);
                ExportButtonClicked -= new EventHandler(_ExportButtonClicked);
                InitialButtonClicked -= new EventHandler(_InitialButtonClicked);
                AddItemButtonClicked -= new EventHandler(_AddItemButtonClicked);
                CloseButtonClicked -= new EventHandler(_CloseButtonClicked);

                SearchButtonClicked += new EventHandler(_SearchButtonClicked);
                PrintButtonClicked += new EventHandler(_PrintButtonClicked);
                DeleteButtonClicked += new EventHandler(_DeleteButtonClicked);
                SaveButtonClicked += new EventHandler(_SaveButtonClicked);
                ImportButtonClicked += new EventHandler(_ImportButtonClicked);
                ExportButtonClicked += new EventHandler(_ExportButtonClicked);
                InitialButtonClicked += new EventHandler(_InitialButtonClicked);
                AddItemButtonClicked += new EventHandler(_AddItemButtonClicked);
                CloseButtonClicked += new EventHandler(_CloseButtonClicked);

                fpMain._ChangeEventHandler += Change;

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

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
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
                _Mst_Id = string.Empty;
                DataTable pDataTable = new CoreBusiness().BASE_MENU_SETTING_R10(this._pMenuSettingEntity.MENU_WINDOW_NAME, fpMain, this._pMenuSettingEntity.BASE_TABLE.Split('/')[0]);
                InitializeControl(pDataTable);
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

        private void InitializeButton()
        {
            try
            {
                MainFormButtonSetting _MainFormButtonSetting = new MainFormButtonSetting();

                DataTable _DataTable = new MenuButton_Business().MenuButton_Select(_pMenuSettingEntity.MENU_NO, MainForm.UserEntity.user_authority);
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    _MainFormButtonSetting.UseYNSearchButton = _DataTable.Rows[0]["menu_search"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNPrintButton = _DataTable.Rows[0]["menu_print"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNDeleteButton = _DataTable.Rows[0]["menu_delete"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNSaveButton = _DataTable.Rows[0]["menu_save"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNImportButton = _DataTable.Rows[0]["menu_import"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNExportButton = _DataTable.Rows[0]["menu_export"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNInitialButton = _DataTable.Rows[0]["menu_initialize"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNAddItemButton = _DataTable.Rows[0]["menu_newadd"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNFormCloseButton = true;
                }
                else
                {
                    _MainFormButtonSetting.UseYNSearchButton = false;
                    _MainFormButtonSetting.UseYNPrintButton = false;
                    _MainFormButtonSetting.UseYNDeleteButton = false;
                    _MainFormButtonSetting.UseYNSaveButton = false;
                    _MainFormButtonSetting.UseYNImportButton = false;
                    _MainFormButtonSetting.UseYNExportButton = false;
                    _MainFormButtonSetting.UseYNInitialButton = false;
                    _MainFormButtonSetting.UseYNAddItemButton = false;
                    _MainFormButtonSetting.UseYNFormCloseButton = true;
                }

                MainForm.SetButtonSetting(_MainFormButtonSetting);
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region ○ 초기화 영역


        public void InitializeControl(DataTable dt)
        {
            try
            {
                if (_FirstYn)
                {

                    Function.Core.InitializeControl(dt, fpMain, this,_PAN_WHERE, this._pMenuSettingEntity);
                }

                chartControl1.Series.Clear();
     


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
        #endregion

        #region ○ 스프레드 영역

     

        public void Change(object sender, ChangeEventArgs e)
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
                    //xFp.ShowActiveCell(e.Row, e.Column);
                    //xFp.ActiveSheet.Cells[e.Row, e.Column].ay();
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
                Base_textbox txtMachine = _PAN_WHERE.Controls[0] as Base_textbox;
                Base_textbox txtPartNo  = _PAN_WHERE.Controls[1] as Base_textbox;
                Base_textbox txtItem    = _PAN_WHERE.Controls[1] as Base_textbox;
                Base_FromtoDateTime datetime = _PAN_WHERE.Controls[3] as Base_FromtoDateTime;
                string strTime = datetime.StartValue.ToString("yyyy-MM-dd");
                string endTime = datetime.EndValue.ToString("yyyy-MM-dd");

                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                if (chartControl1.Series.Count > 0)
                {
                    chartControl1.Series.Clear();
                }

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"SELECT 
		ROW_NUMBER() OVER (ORDER BY A.ID) AS RowNum,
	   D.[description]   AS 'D.description'
      ,J.[RESOURCE_NO]  AS 'J.RESOURCE_NO'
      ,J.[LOT_NO]       AS 'J.LOT_NO'
      ,B.[description]  AS 'B.설비명'
      ,A.REG_DATE   AS 'A.REG_DATE'
      ,J.REG_DATE   AS 'J.REG_DATE'
      ,ISNULL(F.cavity,'0')          AS 'F.CAVITY'   
      ,A.[ELECTRICAL_ENERGY] AS 'A.전력사용량'
      ,A.[CYCLE_TIME] AS 'A.CYCLE_TIME'
	  ,CONVERT(NUMERIC(18,2), ROUND(ISNULL(J.WORK_TIME,0)/60,2)) AS 'WORK_TIME'
	  ,ISNULL(FORMAT(ROUND((CONVERT(decimal(18,4), G.HOURLY_WAGE_PER_SECOND) * CONVERT(DECIMAL(18,2),A.CYCLE_TIME) * convert(int,J.IN_PER) /CONVERT(INT,F.cavity)),2),'0.00'),0) AS '직접노무비'
     ,ISNULL(FORMAT(ROUND((CONVERT(decimal(18,4), G.HOURLY_WAGE_PER_SECOND) * CONVERT(DECIMAL(18,2),A.CYCLE_TIME) * convert(int,J.IN_PER) /CONVERT(INT,F.cavity)),2) * ((CONVERT(decimal(18,4) ,G.INDIRECT_LABOR_RATIO)) / 100   ),'0.00'),0)      AS '간접노무비'
     ,ISNULL(FORMAT(CONVERT(INT ,H.EQUIPMENT_COST) / CONVERT(INT, H.EQUIPMENT_USE_YEAR) / CONVERT(INT, H.EQUIPMENT_OPERATION) /CONVERT(INT,H.EQUIPMENT_OPERATION_DAY) /CONVERT(INT,H.EQUIPMENT_OPERATION_TIME) * CONVERT(DECIMAL(18,2),CONVERT(DECIMAL(18,2),A.CYCLE_TIME)) /   CONVERT(INT,F.cavity),'0.00'),0)         AS '설비감상비'
     ,ISNULL(FORMAT(CONVERT(DECIMAL(18,2),H.EQUIPMENT_AREA) * CONVERT(DECIMAL(18,2),H.UNIT_PRICE_PER_PYEONG) * 5.5 /40 / CONVERT(INT, H.EQUIPMENT_OPERATION) /CONVERT(INT,H.EQUIPMENT_OPERATION_DAY) /CONVERT(INT,H.EQUIPMENT_OPERATION_TIME) * CONVERT(DECIMAL(18,2),A.CYCLE_TIME) /   CONVERT(INT,F.cavity),'0.00') ,0)        AS '건물감상비'
     ,ISNULL(FORMAT((CONVERT(INT ,H.EQUIPMENT_COST) / CONVERT(INT, H.EQUIPMENT_USE_YEAR) / CONVERT(INT, H.EQUIPMENT_OPERATION) /CONVERT(INT,H.EQUIPMENT_OPERATION_DAY) /CONVERT(INT,H.EQUIPMENT_OPERATION_TIME) * CONVERT(DECIMAL(18,2),CONVERT(DECIMAL(18,2),A.CYCLE_TIME)) /   CONVERT(INT,F.cavity) + CONVERT(DECIMAL(18,2),H.EQUIPMENT_AREA) * CONVERT(INT ,H.EQUIPMENT_COST)/(CONVERT (DECIMAL(18,2),H.EQUIPMENT_AREA)/3.3) * 5.5 /40 / CONVERT(INT, H.EQUIPMENT_OPERATION) /CONVERT(INT,H.EQUIPMENT_OPERATION_DAY) /CONVERT(INT,H.EQUIPMENT_OPERATION_TIME) *CONVERT(DECIMAL(18,2),CONVERT(DECIMAL(18,2),A.CYCLE_TIME)) /   CONVERT(INT,F.cavity)) * CONVERT(DECIMAL(18,2),G.REPAIR_RATE)/100,'0.00'),0) AS '수선비'
     ,ISNULL(FORMAT(CONVERT(DECIMAL(18,2),ISNULL(ELECTRICAL_ENERGY,0)) * convert(int,(H.[EQUIPMENT_POWER_RATIO]))  ,'0.00'),0)    AS '개별전력비'
	  ,ISNULL(FORMAT(CONVERT(DECIMAL(18,2),ISNULL(NULLIF(I.ELEC_USE, ''), '0')) * convert(int,(H.[EQUIPMENT_POWER_RATIO]))  ,'0.00') ,0)   AS '전체전력비'
     ,ISNULL(FORMAT((CONVERT(INT ,H.EQUIPMENT_COST) / CONVERT(INT, H.EQUIPMENT_USE_YEAR) / CONVERT(INT, H.EQUIPMENT_OPERATION) /CONVERT(INT,H.EQUIPMENT_OPERATION_DAY) /CONVERT(INT,H.EQUIPMENT_OPERATION_TIME) * CONVERT(DECIMAL(18,2),CONVERT(DECIMAL(18,2),A.CYCLE_TIME)) /   CONVERT(INT,F.cavity)
+ 
CONVERT(DECIMAL(18,2),H.EQUIPMENT_AREA) * CONVERT(DECIMAL(18,2),H.UNIT_PRICE_PER_PYEONG) * 5.5 /40 / CONVERT(INT, H.EQUIPMENT_OPERATION) /CONVERT(INT,H.EQUIPMENT_OPERATION_DAY) /CONVERT(INT,H.EQUIPMENT_OPERATION_TIME) * CONVERT(DECIMAL(18,2),A.CYCLE_TIME) /   CONVERT(INT,F.cavity)
+
(CONVERT(INT ,H.EQUIPMENT_COST) / CONVERT(INT, H.EQUIPMENT_USE_YEAR) / CONVERT(INT, H.EQUIPMENT_OPERATION) /CONVERT(INT,H.EQUIPMENT_OPERATION_DAY) /CONVERT(INT,H.EQUIPMENT_OPERATION_TIME) * CONVERT(DECIMAL(18,2),CONVERT(DECIMAL(18,2),A.CYCLE_TIME)) /   CONVERT(INT,F.cavity)
+
CONVERT(DECIMAL(18,2),H.EQUIPMENT_AREA) * CONVERT(DECIMAL(18,2),H.UNIT_PRICE_PER_PYEONG) * 5.5 /40 / CONVERT(INT, H.EQUIPMENT_OPERATION) /CONVERT(INT,H.EQUIPMENT_OPERATION_DAY) /CONVERT(INT,H.EQUIPMENT_OPERATION_TIME) * CONVERT(DECIMAL(18,2),A.CYCLE_TIME) /   CONVERT(INT,F.cavity)
* CONVERT(DECIMAL(18,2),G.REPAIR_RATE)/100)
+
CONVERT(DECIMAL(18,2),ISNULL(ELECTRICAL_ENERGY,0)) * convert(int,(H.[EQUIPMENT_POWER_RATIO]))) 
*
CONVERT(DECIMAL(18,2),INDIRECT_EXPENSE_RATIO)/100 , '0.00'),0)
	 
   AS '간접경비'
     ,ISNULL(FORMAT((ROUND((CONVERT(decimal(18,4), G.HOURLY_WAGE_PER_SECOND) * CONVERT(DECIMAL(18,2),A.CYCLE_TIME) * convert(int,J.IN_PER) /CONVERT(INT,F.cavity)),2))
+
(ROUND((CONVERT(decimal(18,4), G.HOURLY_WAGE_PER_SECOND) * CONVERT(DECIMAL(18,2),A.CYCLE_TIME) * convert(int,J.IN_PER) /CONVERT(INT,F.cavity)),2) *  ((CONVERT(decimal(18,4) ,G.INDIRECT_LABOR_RATIO)) / 100   ))
+
(CONVERT(INT ,H.EQUIPMENT_COST) / CONVERT(INT, H.EQUIPMENT_USE_YEAR) / CONVERT(INT, H.EQUIPMENT_OPERATION) /CONVERT(INT,H.EQUIPMENT_OPERATION_DAY) /CONVERT(INT,H.EQUIPMENT_OPERATION_TIME) * CONVERT(DECIMAL(18,2),CONVERT(DECIMAL(18,2),A.CYCLE_TIME)) /   CONVERT(INT,F.cavity))
+
(CONVERT(DECIMAL(18,2),H.EQUIPMENT_AREA) * CONVERT(DECIMAL(18,2),H.UNIT_PRICE_PER_PYEONG) * 5.5 /40 / CONVERT(INT, H.EQUIPMENT_OPERATION) /CONVERT(INT,H.EQUIPMENT_OPERATION_DAY) /CONVERT(INT,H.EQUIPMENT_OPERATION_TIME) * CONVERT(DECIMAL(18,2),A.CYCLE_TIME) /   CONVERT(INT,F.cavity))
+
(CONVERT(INT ,H.EQUIPMENT_COST) / CONVERT(INT, H.EQUIPMENT_USE_YEAR) / CONVERT(INT, H.EQUIPMENT_OPERATION) /CONVERT(INT,H.EQUIPMENT_OPERATION_DAY) /CONVERT(INT,H.EQUIPMENT_OPERATION_TIME) * CONVERT(DECIMAL(18,2),CONVERT(DECIMAL(18,2),A.CYCLE_TIME)) /   CONVERT(INT,F.cavity)
+
CONVERT(DECIMAL(18,2),H.EQUIPMENT_AREA) * CONVERT(DECIMAL(18,2),H.UNIT_PRICE_PER_PYEONG) * 5.5 /40 / CONVERT(INT, H.EQUIPMENT_OPERATION) /CONVERT(INT,H.EQUIPMENT_OPERATION_DAY) /CONVERT(INT,H.EQUIPMENT_OPERATION_TIME) * CONVERT(DECIMAL(18,2),A.CYCLE_TIME) /   CONVERT(INT,F.cavity)
* CONVERT(DECIMAL(18,2),G.REPAIR_RATE)/100)
+
(CONVERT(DECIMAL(18,2),ISNULL(ELECTRICAL_ENERGY,0)) * convert(int,(H.[EQUIPMENT_POWER_RATIO])))
+
((CONVERT(INT ,H.EQUIPMENT_COST) / CONVERT(INT, H.EQUIPMENT_USE_YEAR) / CONVERT(INT, H.EQUIPMENT_OPERATION) /CONVERT(INT,H.EQUIPMENT_OPERATION_DAY) /CONVERT(INT,H.EQUIPMENT_OPERATION_TIME) * CONVERT(DECIMAL(18,2),CONVERT(DECIMAL(18,2),A.CYCLE_TIME)) /   CONVERT(INT,F.cavity)
+ 
CONVERT(DECIMAL(18,2),H.EQUIPMENT_AREA) * CONVERT(DECIMAL(18,2),H.UNIT_PRICE_PER_PYEONG) * 5.5 /40 / CONVERT(INT, H.EQUIPMENT_OPERATION) /CONVERT(INT,H.EQUIPMENT_OPERATION_DAY) /CONVERT(INT,H.EQUIPMENT_OPERATION_TIME) * CONVERT(DECIMAL(18,2),A.CYCLE_TIME) /   CONVERT(INT,F.cavity)
+
(CONVERT(INT ,H.EQUIPMENT_COST) / CONVERT(INT, H.EQUIPMENT_USE_YEAR) / CONVERT(INT, H.EQUIPMENT_OPERATION) /CONVERT(INT,H.EQUIPMENT_OPERATION_DAY) /CONVERT(INT,H.EQUIPMENT_OPERATION_TIME) * CONVERT(DECIMAL(18,2),CONVERT(DECIMAL(18,2),A.CYCLE_TIME)) /   CONVERT(INT,F.cavity)
+
CONVERT(DECIMAL(18,2),H.EQUIPMENT_AREA) * CONVERT(DECIMAL(18,2),H.UNIT_PRICE_PER_PYEONG) * 5.5 /40 / CONVERT(INT, H.EQUIPMENT_OPERATION) /CONVERT(INT,H.EQUIPMENT_OPERATION_DAY) /CONVERT(INT,H.EQUIPMENT_OPERATION_TIME) * CONVERT(DECIMAL(18,2),A.CYCLE_TIME) /   CONVERT(INT,F.cavity)
* CONVERT(DECIMAL(18,2),G.REPAIR_RATE)/100)
+
CONVERT(DECIMAL(18,2),ISNULL(ELECTRICAL_ENERGY,0)) * convert(int,(H.[EQUIPMENT_POWER_RATIO]))) 
*
CONVERT(DECIMAL(18,2),INDIRECT_EXPENSE_RATIO)/100) ,'0,00')  ,0)
	 AS '공정비'
	,A.REG_DATE  
	,J.REG_DATE  
	,convert(decimal(18,2),(CONVERT(decimal(18,2),isnull((SELECT  top 1 [qty_per]  FROM [sea_mfg].[dbo].[cproduct_defn] where resource_no = A.RESOURCE_NO and ENG_CHG_CODE ='A' ),'0'))) * CONVERT(int,isnull((SELECT TOP 1 [price] FROM [sea_mfg].[dbo].[prices] where resource_no = (SELECT  top 1 [resource_used]  FROM [sea_mfg].[dbo].[cproduct_defn] where resource_no = A.RESOURCE_NO and ENG_CHG_CODE ='A') order by update_date desc),'0')) * (   (convert(int,G.MATERIAL_COST_PER) /100.0 +1) )  ) AS '재료비'
  FROM [HS_MES].[dbo].[ELEC_SHOT] AS A

  INNER JOIN (SELECT REG_DATE, RESOURCE_NO , LOT_NO  ,SUM(CONVERT(DECIMAL(18,2),IN_PER)) AS IN_PER  , SUM( CONVERT(DECIMAL(18,2),WORK_TIME)) AS WORK_TIME  FROM [HS_MES].[dbo].[WORK_PERFORMANCE] GROUP BY REG_DATE, RESOURCE_NO, LOT_NO  ) AS J
  ON A.RESOURCE_NO = J.RESOURCE_NO
  AND A.LOT_NO = J.LOT_NO

   INNER JOIN [sea_mfg].[dbo].[resource] AS B WITH (NOLOCK)
  ON B.resource_no  = A.resource_no
  INNER JOIN [sea_mfg].[dbo].[schedrtg] AS C  WITH (NOLOCK)
  ON C.order_no = A.resource_no 
  AND C.lot = A.LOT_NO
  INNER JOIN [sea_mfg].[dbo].[resource] AS D  WITH (NOLOCK)
  ON C.workcenter =D.resource_no
  INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E  WITH (NOLOCK)
  ON A.RESOURCE_NO = E.order_no
  AND A.LOT_NO = E.lot
  LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F  WITH (NOLOCK)
  ON E.code_md = F.code_md 
  LEFT OUTER JOIN [HS_MES].[dbo].[MATERIALCOST_PROCESS] AS G
  ON G.PROCESS_ID = '주조'
  INNER JOIN MATERIALCOST_EQUIPMENT AS H
  ON H.EQUIPMENT_ID ='설비850TON'
  LEFT OUTER JOIN [HS_MES].[dbo].[ELECTRIC_USE] AS I
  ON A.RESOURCE_NO = I.RESOURCE_NO
   
  WHERE    CONVERT(DECIMAL(18,2),ELECTRICAL_ENERGY) <100 AND A.REG_DATE > '2024-11-05 23:59:59' ";

                //.
                StringBuilder sb = new StringBuilder();
                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString();


                DataTable _DataTable = new CoreBusiness().SELECT(sql);
                RemoveDuplicateRows(_DataTable, new string[] { "A.REG_DATE", "D.description", "J.RESOURCE_NO", "J.LOT_NO" }); //"D.호기", "J.RESOURCE_NO", "J.LOT_NO", 
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {   
                    fpMain.Sheets[0].Visible = false;
                    fpMain.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        foreach (DataColumn item in _DataTable.Columns)
                        {
                            fpMain.Sheets[0].SetValue(i, item.ColumnName.ToUpper(), _DataTable.Rows[i][item.ColumnName]);

                            if (item.ColumnName == "COMPLETE_YN")
                            {
                                switch (_DataTable.Rows[i][item.ColumnName].ToString())
                                {
                                    case "Y":
                                        fpMain.Sheets[0].Rows[i].BackColor = Color.FromArgb(198, 239, 206);
                                        fpMain.Sheets[0].Rows[i].Locked = true;
                                        break;
                                    case "W":
                                        fpMain.Sheets[0].Rows[i].BackColor = Color.LightBlue;
                                        fpMain.Sheets[0].Rows[i].Locked = true;
                                        break;
                                }
                            }
                        }

                    }
                    Function.Core._AddItemSUM(fpMain);
                    //Function.Core._AddItemAGV(fpMain);
                    fpMain.Sheets[0].Visible = true;

                    //chart2.Series[0].Points.AddXY(0, 0);

                    DataTable mac_dt = new DataTable();

                    mac_dt = _DataTable;

                    mac_dt = mac_dt.DefaultView.ToTable(true, "D.description");

                    //for (int i = 0; i < mac_dt.Rows.Count; i++)
                    //{
                    //string name = "";
                    //name = mac_dt.Rows[i]["D.호기"].ToString();
                    Series series_first = new Series();
                    series_first.Name = "공정비";

                    Series series_second = new Series();
                    series_second.Name = "개별전력비";

                    Series series_third = new Series();
                    series_third.Name = "재료비";

                    //Series series_second = new Series();
                    //series_second.Name = "간접노무비";

                    //Series series_third = new Series();
                    //series_third.Name = "설비감상비";

                    //Series series_4 = new Series();
                    //series_4.Name = "건물감상비";

                    //Series series_5 = new Series();
                    //series_5.Name = "수선비";

                    //Series series_6 = new Series();
                    //series_6.Name = "개별전력비";

                    //Series series_7 = new Series();
                    //series_7.Name = "간접경비";

                    //Series series_8 = new Series();
                    //series_8.Name = "직접노무비";

                    chartControl1.Series.Add(series_first);
                    chartControl1.Series.Add(series_second);
                    chartControl1.Series.Add(series_third);
                    //chartControl1.Series.Add(series_4);
                    //chartControl1.Series.Add(series_5);
                    //chartControl1.Series.Add(series_6);
                    //chartControl1.Series.Add(series_7);
                    //chartControl1.Series.Add(series_8);
                    chartControl1.Series[0].View = new LineSeriesView();
                    chartControl1.Series[1].View = new LineSeriesView();
                    chartControl1.Series[2].View = new LineSeriesView();
                    //chartControl1.Series[3].View = new LineSeriesView();
                    //chartControl1.Series[4].View = new LineSeriesView();
                    //chartControl1.Series[5].View = new LineSeriesView();
                    //chartControl1.Series[6].View = new LineSeriesView();
                    //chartControl1.Series[7].View = new LineSeriesView();
                    //}

                    if (_DataTable != null && _DataTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < _DataTable.Rows.Count; i++)
                        {
                            chartControl1.Series[0].Points.Add(new SeriesPoint(_DataTable.Rows[i]["RowNum"].ToString(), _DataTable.Rows[i]["공정비"].ToString()));
                            chartControl1.Series[1].Points.Add(new SeriesPoint(_DataTable.Rows[i]["RowNum"].ToString(), _DataTable.Rows[i]["개별전력비"].ToString()));
                            chartControl1.Series[2].Points.Add(new SeriesPoint(_DataTable.Rows[i]["RowNum"].ToString(), _DataTable.Rows[i]["재료비"].ToString()));
                            //chartControl1.Series[0].Points.Add(new SeriesPoint(_DataTable.Rows[i]["REG_DATE"].ToString(), _DataTable.Rows[i]["직접노무비"].ToString()));
                            //chartControl1.Series[1].Points.Add(new SeriesPoint(_DataTable.Rows[i]["REG_DATE"].ToString(), _DataTable.Rows[i]["간접노무비"].ToString()));
                            //chartControl1.Series[2].Points.Add(new SeriesPoint(_DataTable.Rows[i]["REG_DATE"].ToString(), _DataTable.Rows[i]["설비감상비"].ToString()));
                            //chartControl1.Series[3].Points.Add(new SeriesPoint(_DataTable.Rows[i]["REG_DATE"].ToString(), _DataTable.Rows[i]["건물감상비"].ToString()));
                            //chartControl1.Series[4].Points.Add(new SeriesPoint(_DataTable.Rows[i]["REG_DATE"].ToString(), _DataTable.Rows[i]["수선비"].ToString()));
                            //chartControl1.Series[5].Points.Add(new SeriesPoint(_DataTable.Rows[i]["REG_DATE"].ToString(), _DataTable.Rows[i]["개별전력비"].ToString()));
                            //chartControl1.Series[6].Points.Add(new SeriesPoint(_DataTable.Rows[i]["REG_DATE"].ToString(), _DataTable.Rows[i]["간접경비"].ToString()));
                            //chartControl1.Series[7].Points.Add(new SeriesPoint(_DataTable.Rows[i]["REG_DATE"].ToString(), _DataTable.Rows[i]["공정비"].ToString()));
                        }
                    }
                    chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    chartControl1.Legend.MarkerMode = LegendMarkerMode.CheckBoxAndMarker;
                    chartControl1.Series[1].CheckedInLegend = false;
                    chartControl1.Series[2].CheckedInLegend = false;

                    diagram = (XYDiagram)chartControl1.Diagram;
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

        public virtual void MainSave_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                //fpMain.Focus();
                //bool _Error = new CoreBusiness().BaseForm1_A10(this._pMenuSettingEntity,fpMain,this._pMenuSettingEntity.BASE_TABLE.Split('/')[0]);
                //if (!_Error)
                //{
                //    fpMain.Sheets[0].Rows.Count = 0;
                //    MainFind_DisplayData();
                //   // SubFind_DisplayData(_Mst_Id);
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
        public static void RemoveDuplicateRows(DataTable table, string[] columnsToCompare)
        {
            var distinctRows = table.AsEnumerable()
                .GroupBy(row => string.Join(",", columnsToCompare.Select(col => row[col].ToString())))
                .Select(g => g.First())  // 각 그룹에서 첫 번째 행을 선택
                .CopyToDataTable();  // 결과를 새 DataTable로 복사

            // 기존 테이블을 새로 갱신
            table.Clear();
            foreach (DataRow row in distinctRows.Rows)
            {
                table.ImportRow(row);
            }
        }






        #endregion

        #region ○ 기타이벤트 영역

        private void txtEdit_KeyDown(object obj, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                MainFind_DisplayData();
        }

        private void _LookupEdit_ValueChanged(object obj, EventArgs e)
        {
            if (!_FirstYn)
            {
                MainFind_DisplayData();
            }
        }


        #endregion


     




    }
}
