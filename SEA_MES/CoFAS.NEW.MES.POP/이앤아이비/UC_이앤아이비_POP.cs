using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP
{
    public partial class UC_이앤아이비_POP : UserControl
    {
        #region .. Double Buffered function ..
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;
            System.Reflection.PropertyInfo aProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void SetDoubleBuffered_Control(Control.ControlCollection controls)
        {
            foreach (Control item in controls)
            {
                if (item.Controls.Count != 0)
                {
                    SetDoubleBuffered_Control(item.Controls);
                }

                SetDoubleBuffered(item);
            }
        }
        #endregion

        DataRow _DataRow = null;

        DataTable _DataTable = null;

        UserEntity _UserEntity = null;

        string _TYPE = string.Empty;

        public UC_이앤아이비_POP(DataRow _pDataRow, DataTable _pDataTable, UserEntity _pUserEntity,string _pType)
        {
            InitializeComponent();

            _DataRow = _pDataRow;

            _DataTable = _pDataTable;

            _UserEntity = _pUserEntity;

            _TYPE = _pType;

            btn_최소최대화.Image = global::CoFAS.NEW.MES.POP.Properties.Resources.import;

            this.Size = new Size(this.Size.Width, 30);

          
        }

        private void UC_이앤아이비_POP_Load(object sender, EventArgs e)
        {
            fpPOP._ChangeEventHandler += fpPOP_Change;
            fpPOP._EditorNotifyEventHandler -= fpMain_ButtonClicked;
            fpPOP._EditorNotifyEventHandler += fpMain_ButtonClicked;

            if (_DataRow != null)
            {
                label1.Text = _DataRow["NAME"].ToString();
            }
            
            switch (_TYPE)
            {
                case "사출POP":
                  
                    break;
                case "압출POP":
              
                    break;

                case "조립POP":
                    this.Dock = DockStyle.Fill;
                    btn_최소최대화_Click(null, null);
                    break;

                case "포장POP":
                    this.Dock = DockStyle.Fill;
                    btn_최소최대화_Click(null, null);
                    break;
                default:
                    return;
                    break;
            }

        }
        private void fpPOP_Change(object sender, ChangeEventArgs e)
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
                    CoFAS.NEW.MES.Core.Function.Core._AddItemSUM(xFp);
                    xFp.ActiveSheet.SetActiveCell(e.Row, e.Column);
                    xFp.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Center, FarPoint.Win.Spread.HorizontalPosition.Center);
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void btn_최소최대화_Click(object sender, EventArgs e)
        {
            try
            {
                if (_DataTable != null)
                {
                    CoFAS.NEW.MES.Core.Function.Core.initializeSpread(_DataTable, fpPOP, _TYPE, _UserEntity.user_account);
                }
                if (this.Size.Height == 200)
                {
                    btn_최소최대화.Image = global::CoFAS.NEW.MES.POP.Properties.Resources.import;
                    this.Size = new Size(this.Size.Width, 30);
                }
                else
                {

                    btn_최소최대화.Image = global::CoFAS.NEW.MES.POP.Properties.Resources.export;
                    this.Size = new Size(this.Size.Width, 200);
                }
                btn_새로고침_Click(null, null);
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
        private void btn_새로고침_Click(object sender, EventArgs e)
        {
            try
            {

                fpPOP.Sheets[0].Rows.Count = 0;


                string str = string.Empty;

                switch (_TYPE)
                {
                    case "사출POP":
                        str = $@"SELECT
                                A.ID                             AS ID
                               ,A.PRODUCTION_PLAN_ID             AS 'A.PRODUCTION_PLAN_ID'
                               ,A.INSTRUCT_DATE                  AS 'A.INSTRUCT_DATE'
                               ,A.STOCK_MST_ID                   AS 'A.STOCK_MST_ID'
                               ,B.OUT_CODE                       AS 'B.OUT_CODE'
                               ,B.NAME                           AS 'B.NAME'
                               ,B.STANDARD                       AS 'B.STANDARD'
                               ,B.TYPE                           AS 'B.TYPE'
                               ,A.PROCESS_ID                     AS 'A.PROCESS_ID'
                               ,A.INSTRUCT_QTY                   AS 'A.INSTRUCT_QTY'
                               ,ISNULL(A.DEMAND_DATE, GETDATE()) AS 'A.DEMAND_DATE'
                               ,A.MATERIAL                       AS 'A.MATERIAL'
                               ,A.COMPANY_ID                     AS 'A.COMPANY_ID'
                               ,C.NAME                           AS 'C.NAME'
                               ,A.SORT                           AS 'A.SORT'
                               ,A.COMMENT                        AS 'A.COMMENT'
                               ,A.COMPLETE_YN                    AS 'A.COMPLETE_YN'
                               ,A.USE_YN                         AS 'A.USE_YN'
                               ,A.REG_USER                       AS 'A.REG_USER'
                               ,A.REG_DATE                       AS 'A.REG_DATE'
                               ,A.UP_USER                        AS 'A.UP_USER'
                               ,A.UP_DATE                        AS 'A.UP_DATE'
                               ,B.QTY                            AS 'B.QTY'
                               ,B.IN_SCHEDULE                    AS 'B.IN_SCHEDULE'
                               ,B.OUT_SCHEDULE                   AS 'B.OUT_SCHEDULE'
                               ,D.CONSUME_QTY                    AS 'D.CONSUME_QTY'
                               ,(A.INSTRUCT_QTY * D.CONSUME_QTY) AS 소요량
                               ,A.EQUIPMENT_ID                   AS 'A.EQUIPMENT_ID'
							   ,E.START_DATE                     AS	'E.START_DATE'
							   ,E.END_DATE	                     AS	'E.END_DATE'
                               ,ISNULL(E.진행수량,0)             AS '진행수량'
                               ,ISNULL(E.WORKER_NAME,'')         AS 'WORKER_NAME'
                               ,ISNULL(F.OK_QTY,0)               AS 'OK_QTY'
                               ,ISNULL(G.NG_QTY,0)               AS 'NG_QTY'
                               ,ISNULL(E.ID,'')                  AS 'E.ID'
                               ,E.WORKER_NAME                    AS 'E.WORKER_NAME'
                                FROM[dbo].[PRODUCTION_INSTRUCT] A
                               INNER JOIN STOCK_MST B on A.STOCK_MST_ID = B.id
                                LEFT JOIN COMPANY C ON A.COMPANY_ID = C.ID
                               INNER JOIN 
                                        (
                                          select MAX(B.REG_DATE) AS REG_DATE
										,STOCK_MST_ID
										,B.CONSUME_QTY
                                        from STOCK_MST A
                                        INNER JOIN BOM B ON A.ID = B.STOCK_MST_ID AND B.USE_YN = 'Y'
                                        INNER JOIN STOCK_MST C ON C.ID = B.SUB_STOCK_MST_ID AND B.USE_YN = 'Y'
										where  1=1
										and A.PROCESS_ID = '5'
										AND B.STOCK_MST_ID != B.SUB_STOCK_MST_ID
										and C.TYPE = 'SD03001'
                                        GROUP BY STOCK_MST_ID,B.CONSUME_QTY
                                        )D ON A.STOCK_MST_ID = D.STOCK_MST_ID
	                          LEFT JOIN 
								       (
							            select A.*,진행수량
                                             from [dbo].[PRODUCTION_RESULT] A                                    
                                             LEFT JOIN
                                                     (
                                                      select COUNT(UNIQ_NO) AS 진행수량,A.ID,A.WORKER_NAME
                                                      from [dbo].[PRODUCTION_RESULT] A
                                                      INNER JOIN EQUIPMENT B ON A.EQUIPMENT_ID = B.ID AND B.ID = {_DataRow["ID"].ToString()}
                                                       LEFT JOIN [dbo].[CCS_INJ_SPC_DATA] C ON C.SPC_DATETIME Between  A.START_DATE AND ISNULL(A.END_DATE,GETDATE()) 
                                                      where 1=1
                                                      AND A.USE_YN = 'Y'                                             
                                                      AND A.START_DATE IS NOT NULL
                                                      AND A.END_DATE IS NULL
                                                      GROUP BY  A.ID,A.WORKER_NAME
                                                     ) C ON A.ID = C.ID
                                             where 1=1
                                             AND A.USE_YN = 'Y'                                             
                                             AND A.START_DATE IS NOT NULL
                                             AND A.END_DATE IS NULL
								       ) E ON A.ID = E.PRODUCTION_INSTRUCT_ID
                                LEFT JOIN
                                      (
                                          select SUM(QTY) AS OK_QTY
                                                ,PRODUCTION_INSTRUCT_ID
                                          from [dbo].[PRODUCTION_RESULT]
                                          where 1=1
                                          AND USE_YN ='Y'
                                          AND EQUIPMENT_ID = {_DataRow["ID"].ToString()}
                                          AND RESULT_TYPE = 'CD16001'
                                          GROUP BY PRODUCTION_INSTRUCT_ID
                                      ) F ON A.ID = F.PRODUCTION_INSTRUCT_ID
                                LEFT JOIN
                                      (
                                          select SUM(QTY) AS NG_QTY
                                                ,PRODUCTION_INSTRUCT_ID
                                          from [dbo].[PRODUCTION_RESULT]
                                          where 1=1
                                          AND USE_YN ='Y'
                                          AND EQUIPMENT_ID = {_DataRow["ID"].ToString()}
                                          AND RESULT_TYPE != 'CD16001'
                                          GROUP BY PRODUCTION_INSTRUCT_ID
                                      ) G ON A.ID = G.PRODUCTION_INSTRUCT_ID
                                WHERE 1 = 1
			                    AND A.USE_YN ='Y'
								AND A.COMPLETE_YN != 'Y'
                                AND A.PROCESS_ID  = '5'
                                AND A.EQUIPMENT_ID = {_DataRow["ID"].ToString()}";
                        break;
                    case "압출POP":
                        str = $@"SELECT
                                A.ID                             AS ID
                               ,A.PRODUCTION_PLAN_ID             AS 'A.PRODUCTION_PLAN_ID'
                               ,A.INSTRUCT_DATE                  AS 'A.INSTRUCT_DATE'
                               ,A.STOCK_MST_ID                   AS 'A.STOCK_MST_ID'
                               ,B.OUT_CODE                       AS 'B.OUT_CODE'
                               ,B.NAME                           AS 'B.NAME'
                               ,B.STANDARD                       AS 'B.STANDARD'
                               ,B.TYPE                           AS 'B.TYPE'
                               ,A.PROCESS_ID                     AS 'A.PROCESS_ID'
                               ,A.INSTRUCT_QTY                   AS 'A.INSTRUCT_QTY'
                               ,ISNULL(A.DEMAND_DATE, GETDATE()) AS 'A.DEMAND_DATE'
                               ,A.MATERIAL                       AS 'A.MATERIAL'
                               ,A.COMPANY_ID                     AS 'A.COMPANY_ID'
                               ,C.NAME                           AS 'C.NAME'
                               ,A.SORT                           AS 'A.SORT'
                               ,A.COMMENT                        AS 'A.COMMENT'
                               ,A.COMPLETE_YN                    AS 'A.COMPLETE_YN'
                               ,A.USE_YN                         AS 'A.USE_YN'
                               ,A.REG_USER                       AS 'A.REG_USER'
                               ,A.REG_DATE                       AS 'A.REG_DATE'
                               ,A.UP_USER                        AS 'A.UP_USER'
                               ,A.UP_DATE                        AS 'A.UP_DATE'
                               ,B.QTY                            AS 'B.QTY'
                               ,B.IN_SCHEDULE                    AS 'B.IN_SCHEDULE'
                               ,B.OUT_SCHEDULE                   AS 'B.OUT_SCHEDULE'                       
                               ,A.EQUIPMENT_ID                   AS 'A.EQUIPMENT_ID'
							   ,E.START_DATE                     AS	'E.START_DATE'
							   ,E.END_DATE	                     AS	'E.END_DATE'
                               ,ISNULL(E.생산수량,0)             AS 생산수량
                               ,ISNULL(E.ID,'')                  AS 'E.ID'
                               ,E.WORKER_NAME                    AS 'E.WORKER_NAME'
                                FROM[dbo].[PRODUCTION_INSTRUCT] A
                               INNER JOIN STOCK_MST B on A.STOCK_MST_ID = B.id
                                LEFT JOIN COMPANY C ON A.COMPANY_ID = C.ID     
	                            LEFT JOIN 
								       (
							            select A.*,생산수량
                                             from [dbo].[PRODUCTION_RESULT] A                                    
                                             LEFT JOIN
                                                     (
                                                      select COUNT(UNIQ_NO) AS 생산수량,A.ID
                                                      from [dbo].[PRODUCTION_RESULT] A
                                                      INNER JOIN EQUIPMENT B ON A.EQUIPMENT_ID = B.ID
                                                      left join [dbo].[CCS_INJ_SPC_DATA] C ON C.SPC_DATETIME Between  A.START_DATE AND ISNULL(A.END_DATE,GETDATE()) 
                                                      where 1=1
                                                      AND A.USE_YN = 'Y'                                             
                                                      AND A.START_DATE IS NOT NULL
                                                      AND A.END_DATE IS NULL
                                                      GROUP BY  A.ID
                                                     ) C ON A.ID = C.ID
                                             where 1=1
                                             AND A.USE_YN = 'Y'                                             
                                             AND A.START_DATE IS NOT NULL
                                             AND A.END_DATE IS NULL
								       ) E ON A.ID = E.PRODUCTION_INSTRUCT_ID

                                WHERE 1 = 1
			                    AND A.USE_YN ='Y'
								AND A.COMPLETE_YN != 'Y'
                                AND A.PROCESS_ID  = '7'              
                                AND A.EQUIPMENT_ID = '{_DataRow["ID"].ToString()}'";
                        break;
                    case "조립POP":
                        str = $@"SELECT
                                A.ID                             AS ID
                               ,A.PRODUCTION_PLAN_ID             AS 'A.PRODUCTION_PLAN_ID'
                               ,A.INSTRUCT_DATE                  AS 'A.INSTRUCT_DATE'
                               ,A.STOCK_MST_ID                   AS 'A.STOCK_MST_ID'
                               ,B.OUT_CODE                       AS 'B.OUT_CODE'
                               ,B.NAME                           AS 'B.NAME'
                               ,B.STANDARD                       AS 'B.STANDARD'
                               ,B.TYPE                           AS 'B.TYPE'
                               ,A.PROCESS_ID                     AS 'A.PROCESS_ID'
                               ,A.INSTRUCT_QTY                   AS 'A.INSTRUCT_QTY'
                               ,ISNULL(A.DEMAND_DATE, GETDATE()) AS 'A.DEMAND_DATE'
                               ,A.MATERIAL                       AS 'A.MATERIAL'
                               ,A.COMPANY_ID                     AS 'A.COMPANY_ID'
                               ,C.NAME                           AS 'C.NAME'
                               ,A.SORT                           AS 'A.SORT'
                               ,A.COMMENT                        AS 'A.COMMENT'
                               ,A.COMPLETE_YN                    AS 'A.COMPLETE_YN'
                               ,A.USE_YN                         AS 'A.USE_YN'
                               ,A.REG_USER                       AS 'A.REG_USER'
                               ,A.REG_DATE                       AS 'A.REG_DATE'
                               ,A.UP_USER                        AS 'A.UP_USER'
                               ,A.UP_DATE                        AS 'A.UP_DATE'
                               ,B.QTY                            AS 'B.QTY'
                               ,B.IN_SCHEDULE                    AS 'B.IN_SCHEDULE'
                               ,B.OUT_SCHEDULE                   AS 'B.OUT_SCHEDULE'                       
                               ,A.EQUIPMENT_ID                   AS 'A.EQUIPMENT_ID'
							   ,E.START_DATE                     AS	'E.START_DATE'
							   ,E.END_DATE	                     AS	'E.END_DATE'
                               ,ISNULL(E.ID,'')                  AS 'E.ID'
                               ,E.WORKER_NAME                    AS 'E.WORKER_NAME'
                                FROM[dbo].[PRODUCTION_INSTRUCT] A
                               INNER JOIN STOCK_MST B on A.STOCK_MST_ID = B.id
                                LEFT JOIN COMPANY C ON A.COMPANY_ID = C.ID     
	                            LEFT JOIN 
								       (
							          select A.*
                                             from [dbo].[PRODUCTION_RESULT] A                                    
                                             where 1=1
                                             AND A.USE_YN = 'Y'                                             
                                             AND A.START_DATE IS NOT NULL
                                             AND A.END_DATE IS NULL
								       ) E ON A.ID = E.PRODUCTION_INSTRUCT_ID

                                WHERE 1 = 1
			                    AND A.USE_YN ='Y'
								AND A.COMPLETE_YN != 'Y'
                                AND A.PROCESS_ID  = '8'";              
                          
                        break;
                    case "포장POP":
                        str = $@"SELECT
                                A.ID                             AS ID
                               ,A.PRODUCTION_PLAN_ID             AS 'A.PRODUCTION_PLAN_ID'
                               ,A.INSTRUCT_DATE                  AS 'A.INSTRUCT_DATE'
                               ,A.STOCK_MST_ID                   AS 'A.STOCK_MST_ID'
                               ,B.OUT_CODE                       AS 'B.OUT_CODE'
                               ,B.NAME                           AS 'B.NAME'
                               ,B.STANDARD                       AS 'B.STANDARD'
                               ,B.TYPE                           AS 'B.TYPE'
                               ,A.PROCESS_ID                     AS 'A.PROCESS_ID'
                               ,A.INSTRUCT_QTY                   AS 'A.INSTRUCT_QTY'
                               ,ISNULL(A.DEMAND_DATE, GETDATE()) AS 'A.DEMAND_DATE'
                               ,A.MATERIAL                       AS 'A.MATERIAL'
                               ,A.COMPANY_ID                     AS 'A.COMPANY_ID'
                               ,C.NAME                           AS 'C.NAME'
                               ,A.SORT                           AS 'A.SORT'
                               ,A.COMMENT                        AS 'A.COMMENT'
                               ,A.COMPLETE_YN                    AS 'A.COMPLETE_YN'
                               ,A.USE_YN                         AS 'A.USE_YN'
                               ,A.REG_USER                       AS 'A.REG_USER'
                               ,A.REG_DATE                       AS 'A.REG_DATE'
                               ,A.UP_USER                        AS 'A.UP_USER'
                               ,A.UP_DATE                        AS 'A.UP_DATE'
                               ,B.QTY                            AS 'B.QTY'
                               ,B.IN_SCHEDULE                    AS 'B.IN_SCHEDULE'
                               ,B.OUT_SCHEDULE                   AS 'B.OUT_SCHEDULE'                       
                               ,A.EQUIPMENT_ID                   AS 'A.EQUIPMENT_ID'
							   ,E.START_DATE                     AS	'E.START_DATE'
							   ,E.END_DATE	                     AS	'E.END_DATE'     
                               ,ISNULL(E.ID,'')                  AS 'E.ID'
                               ,E.WORKER_NAME                    AS 'E.WORKER_NAME'
                                FROM[dbo].[PRODUCTION_INSTRUCT] A
                               INNER JOIN STOCK_MST B on A.STOCK_MST_ID = B.id
                                LEFT JOIN COMPANY C ON A.COMPANY_ID = C.ID     
	                            LEFT JOIN 
								       (
							            select A.*
                                             from [dbo].[PRODUCTION_RESULT] A                                    
                                             where 1=1
                                             AND A.USE_YN = 'Y'                                             
                                             AND A.START_DATE IS NOT NULL
                                             AND A.END_DATE IS NULL
								       ) E ON A.ID = E.PRODUCTION_INSTRUCT_ID

                                WHERE 1 = 1
			                    AND A.USE_YN ='Y'
								AND A.COMPLETE_YN != 'Y'
                                AND A.PROCESS_ID  = '9'";
                        break;
                    default:
                        return;
                        break;
                }
         


                DataTable _DataTable = new CoreBusiness().SELECT(str);
                //fpPOP.DataSource = _DataTable;
                CoFAS.NEW.MES.Core.Function.Core.DisplayData_Set(_DataTable, fpPOP);


            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        private void btn_저장_Click(object sender, EventArgs e)
        {
            try
            {
                if (CoFAS.NEW.MES.Core.Function.Core._SaveButtonClicked(fpPOP))
                {

                    if (fpPOP.Sheets[0].Rows.Count > 0)
                        MainSave_InputData();
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        public void MainSave_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpPOP.Focus();
                MenuSettingEntity _pMenuSettingEntity = new MenuSettingEntity();

                bool _Error = new CoreBusiness().BaseForm1_A10(_pMenuSettingEntity,fpPOP,"PRODUCTION_INSTRUCT");

                if (!_Error)
                {
                    CustomMsg.ShowMessage("저장되었습니다.");


                    fpPOP.Sheets[0].Rows.Count = 0;

                    btn_새로고침_Click(null, null);
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
        private void fpMain_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            try
            {
                xFpSpread pfpSpread = sender as xFpSpread;

                if (e.EditingControl == null)
                {
                    return;
                }

                if (pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString() != "0")
                {
                    if (pfpSpread != null && pfpSpread.Sheets[0].Columns[e.Column].CellType != null)
                    {
                        if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(ButtonCellType))
                        {
                            string id = pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString();
                            string id_RESULT = pfpSpread.Sheets[0].GetValue(e.Row, "E.ID").ToString();
                            if (e.EditingControl.Text == "작업시작")
                            {
                                string sql = $@"select ID
                                                  from [dbo].[PRODUCTION_RESULT]
                                                  where 1=1
                                                  AND USE_YN = 'Y'
                                                  AND EQUIPMENT_ID = {_DataRow["ID"].ToString()}
                                                  AND START_DATE IS NOT NULL
                                                  AND END_DATE IS NULL";

                                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                                if (_DataTable.Rows.Count != 0)
                                {
                                    CustomMsg.ShowMessage("현재 작업중인 설비 입니다.");
                                    return;
                                }
                                if (pfpSpread.Sheets[0].GetValue(e.Row, "시작시간").ToString() != "")
                                {
                                    CustomMsg.ShowMessage("시작된 작업지시 입니다.");
                                    return;
                                }
                                else
                                {
                                    이앤아이비_작업시작 이앤아이비_작업시작 = new 이앤아이비_작업시작();
                                    이앤아이비_작업시작.lbl_시간.Text = "시작시간";
                                    if (이앤아이비_작업시작.ShowDialog() == DialogResult.OK)
                                    {
                                        DateTime time;
                                        if (DateTime.TryParse(이앤아이비_작업시작.de_시간.DateTime.ToString(), out time))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, "시작시간", time);
                                            sql = $@"INSERT INTO [dbo].[PRODUCTION_RESULT]
                                                           ([LOT_NO]
                                                           ,[WORKER_NAME]
                                                           ,[PRODUCTION_INSTRUCT_ID]
                                                           ,[STOCK_MST_ID]
                                                           ,[EQUIPMENT_ID]
                                                           ,[RESULT_TYPE]
                                                           ,[QTY]
                                                           ,[START_DATE]
                                                           ,[END_DATE]
                                                           ,[COMMENT]
                                                           ,[COMPLETE_YN]
                                                           ,[USE_YN]
                                                           ,[REG_USER]
                                                           ,[REG_DATE]
                                                           ,[UP_USER]
                                                           ,[UP_DATE])                                   
													 select ''
                                                           ,'{이앤아이비_작업시작.txt_작업자.Text}'
													       ,ID
														   ,STOCK_MST_ID
														   ,EQUIPMENT_ID
														   ,'CD16001'
                                                           ,0
                                                           ,'{time.ToString("yyyy-MM-dd HH:mm:ss")}'
                                                           , null
                                                           ,''
                                                           ,'N'
                                                           ,'Y'
                                                           ,'{_UserEntity.user_account}'
                                                           ,GETDATE()
                                                           ,'{_UserEntity.user_account}'
                                                           ,GETDATE()
													  FROM [dbo].[PRODUCTION_INSTRUCT]
													  WHERE ID = {id}";


                                            new CoreBusiness().SELECT(sql);

                                            btn_새로고침_Click(null, null);
                                        }

                                    }
                                }


                            }
                            else if (e.EditingControl.Text == "작업종료")
                            {


                                if (id_RESULT == "")
                                {
                                    CustomMsg.ShowMessage("현재 작업중인 설비가 아닙니다.");
                                    return;
                                }
                                else
                                {
                                    string sql = $@"select ID
                                                  from [dbo].[PRODUCTION_RESULT]
                                                  where 1=1
                                                  AND ID = {id_RESULT}
                                                  AND USE_YN = 'Y'
                                                  AND START_DATE IS NOT NULL
                                                  AND END_DATE IS NULL";

                                    DataTable _DataTable = new CoreBusiness().SELECT(sql);

                                    if (_DataTable.Rows.Count == 0)
                                    {
                                        CustomMsg.ShowMessage("현재 작업중인 작업지시가 아닙니다.");
                                        return;
                                    }
                                    else
                                    {
                                        if (_TYPE == "압출POP")

                                        {
                                            string stock_mst_id = pfpSpread.Sheets[0].GetValue(e.Row, "A.STOCK_MST_ID").ToString();
                                            string out_code = pfpSpread.Sheets[0].GetValue(e.Row, "B.OUT_CODE").ToString();
                                            from_압출종료 from_압출종료 = new from_압출종료(stock_mst_id,out_code);

                                            from_압출종료.lbl_시간.Text = "종료시간";

                                            if (from_압출종료.ShowDialog() == DialogResult.OK)
                                            {
                                                DateTime time;
                                                if (DateTime.TryParse(from_압출종료.de_시간.DateTime.ToString(), out time))
                                                {
                                                    작업종료 작업종료 = new 작업종료
                                                    (
                                                    id_RESULT
                                                    ,time
                                                    ,_UserEntity

                                                    );
                                                    if (작업종료.ShowDialog() == DialogResult.OK)
                                                    {

                                                        btn_새로고침_Click(null, null);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            이앤아이비_작업종료 이앤아이비_작업종료 = new 이앤아이비_작업종료();
                                            이앤아이비_작업종료.lbl_시간.Text = "종료시간";
                                            if (이앤아이비_작업종료.ShowDialog() == DialogResult.OK)
                                            {
                                                DateTime time;
                                                if (DateTime.TryParse(이앤아이비_작업종료.de_시간.DateTime.ToString(), out time))
                                                {
                                                    작업종료 작업종료 = new 작업종료
                                                    (
                                                    id_RESULT
                                                    ,time
                                                    ,_UserEntity

                                                    );
                                                    if (작업종료.ShowDialog() == DialogResult.OK)
                                                    {

                                                        btn_새로고침_Click(null, null);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (e.EditingControl.Text == "초중종")
                            {
                                초중종입력 초중종입력 = new 초중종입력(_UserEntity);
                                초중종입력._pPRODUCTION_INSTRUCT_ID = id;
                                초중종입력._pSTOCK_MST_ID = pfpSpread.Sheets[0].GetValue(e.Row, "A.STOCK_MST_ID").ToString();
                                if (초중종입력.ShowDialog() == DialogResult.OK)
                                {


                                }

                            }
                            else if (e.EditingControl.Text == "설비변경")
                            {
                                string sql = $@"select ID
                                                  from [dbo].[PRODUCTION_RESULT]
                                                  where 1=1
                                                  AND USE_YN = 'Y'
                                                  AND PRODUCTION_INSTRUCT_ID = {id}
                                                  AND START_DATE IS NOT NULL
                                                  AND END_DATE IS NULL";

                                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                                if (_DataTable.Rows.Count != 0)
                                {
                                    CustomMsg.ShowMessage("현재 작업중인 작업지시 입니다.");
                                    return;
                                }
                                string code = "";

                                if (_TYPE == "사출POP")
                                {
                                    code = "CD14001";
                                }
                                else
                                {
                                    code = "CD14002";
                                }
                                DataTable pDataTable = new CoreBusiness().Spread_ComboBox("설비2", code, "");

                                BasePopupBox basePopupBox = new BasePopupBox();
                                basePopupBox.Name = "BaseEquipmentPopupBox";
                                basePopupBox._pDataTable = pDataTable;

                                basePopupBox._UserAccount = pfpSpread._user_account;
                                if (basePopupBox.ShowDialog() == DialogResult.OK)
                                {


                                    sql = $@"UPDATE [dbo].[PRODUCTION_INSTRUCT]
                                                 SET EQUIPMENT_ID = {basePopupBox._pdataRow["CD"].ToString()}
                                               WHERE ID = {id}";

                                    new CoreBusiness().SELECT(sql);
                                    btn_새로고침_Click(null, null);
                                }






                            }
                            else if (e.EditingControl.Text == "Loss등록")
                            {
                                if (pfpSpread != null && pfpSpread.Sheets[0].Columns[e.Column].CellType != null)
                                {
                                    if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(ButtonCellType))
                                    {
                                        외주작업지시_반출 basePopupBox = new 외주작업지시_반출(
                                                pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString()
                                                , pfpSpread._user_account
                                                , pfpSpread.Sheets[0].GetText(e.Row, "B.OUT_CODE").ToString() + " , " + pfpSpread.Sheets[0].GetText(e.Row, "B.NAME").ToString()
                                                , pfpSpread.Sheets[0].GetValue(e.Row, "A.INSTRUCT_QTY").ToString());

                                        //basePopupBox.Size = new Size(1280, basePopupBox.Size.Height);
                                        basePopupBox.Size = new Size(1280, 1024);
                                        basePopupBox._Title.Text = "Loss등록";
                                        basePopupBox._출고타입 = "SD14007";
                                        if (basePopupBox.ShowDialog() == DialogResult.OK)
                                        {

                                        }

                                    }

                                }
                            }
                        }
                    }
                }

            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
            finally
            {
                DevExpressManager.SetCursor(this, Cursors.Default);
            }

        }


    }
}
