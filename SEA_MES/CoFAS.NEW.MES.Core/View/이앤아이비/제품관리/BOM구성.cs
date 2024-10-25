using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using DevExpress.Spreadsheet;
using FarPoint.Win.Spread;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class BOM구성 : DoubleBaseForm5
    {
        public BOM구성()
        {
            InitializeComponent();


        }

        private void DoubleBaseForm2_Load(object sender, EventArgs e)
        {
            splitContainer2.Orientation = Orientation.Vertical;

            splitContainer2.SplitterDistance = 600;

            splitContainer1.SplitterDistance = 150;

        }
        public override void _AddItemButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (_Mst_Id != string.Empty)
                {
                    Function.Core._AddItemButtonClicked(fpSub, MainForm.UserEntity.user_account);

                    int row = 0;

                    for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                    {
                        if (fpMain.Sheets[0].GetValue(i, "ID").ToString() == _Mst_Id)
                        {
                            row = i;
                        }
                    }

                    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "A.STOCK_MST_ID ".Trim(), fpMain.Sheets[0].GetValue(row, "ID".Trim()));
                    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "B.OUT_CODE ".Trim(), fpMain.Sheets[0].GetValue(row, "OUT_CODE".Trim()));

                }

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
        public override void _DeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        public override void _ImportButtonClicked(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    waitform.Show(this, "준비중입니다.");
                    DevExpress.XtraSpreadsheet.SpreadsheetControl sc = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                    sc.LoadDocument(openFileDialog1.FileName, DocumentFormat.Xlsx);

                    List<Code_mst> product_list = CoFAS.NEW.MES.Core.Function.Core.GET_PRODUCT_TYPE();

                    List<Code_mst> process_list = CoFAS.NEW.MES.Core.Function.Core.GET_PROCESS();

                    SqlConnection con;
                    con = new SqlConnection(DBManager.PrimaryConnectionString);

                    SqlCommand cmd;
                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();

                    foreach (Worksheet sheet in sc.Document.Worksheets)
                    {
                        switch (sheet.Name)
                        {
                            case "제품":
                            case "반제품":
                            case "원자재":
                            case "부자재":
                                for (int i = 1; i < 10000; i++)
                                {
                                    Application.DoEvents();
                                    #region ○ 변수 정리

                                    if (sheet.Rows[i][0].Value.ToString().Trim() == "")
                                    {
                                        i = 10000;
                                        continue;
                                    }

                                    string 제품타입 = sheet.Rows[i][3].Value.ToString().Trim();
                                    Code_mst code = product_list.Find(x => x.CODE_NAME == 제품타입);
                                    if (code == null)
                                    {
                                        continue;

                                    }
                                    else
                                    {
                                        제품타입 = code.CODE;
                                    }

                                    string 제품코드 = sheet.Rows[i][1].Value.ToString().Trim();
                                    string 제품명   = sheet.Rows[i][2].Value.ToString().Trim();
                                    string 제품단위 = sheet.Rows[i][4].Value.ToString().Trim();
                                    string 제품규격 = sheet.Rows[i][5].Value.ToString().Trim();
                                    string 제품재질 = sheet.Rows[i][6].Value.ToString().Trim();
                                    string 사용유무 = "Y";


                                    #endregion

                                    cmd.CommandText = $@"
                                                    IF EXISTS (SELECT 1 FROM [dbo].[STOCK_MST] WHERE OUT_CODE = @제품코드)
                                                    BEGIN
                                                        -- 존재하면 업데이트
                                                        UPDATE[dbo].[STOCK_MST]
                                                       SET[OUT_CODE]  = @제품코드
                                                          ,[NAME]     = @제품명
                                                          ,[TYPE]     = @제품타입
                                                          ,[STANDARD] = @제품규격
                                                          ,[UNIT]     =  ISNULL((select TOP 1 code
                                                                                   from Code_Mst 
                                                                                   where 1=1
                                                                                   and code_type = 'CD04'
                                                                                   and code_name = @제품단위),'CD04001')
                                                          ,[TYPE2] = @제품재질
                                                          ,[USE_YN]   = @사용유무
                                                          ,[UP_USER]  = '{MainForm.UserEntity.user_account}'
                                                          ,[UP_DATE]  = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'             
                                                      WHERE OUT_CODE  = @제품코드;
                                                    
                                                                                        END
                                                                                        ELSE
                                                    BEGIN
                                                     INSERT INTO [dbo].[STOCK_MST]
                                                         ([OUT_CODE]
                                                         ,[NAME]
                                                         ,[TYPE]
                                                         ,[STANDARD]
                                                         ,[UNIT]           
                                                         ,[TYPE2]
                                                         ,[CAPA]
                                                         ,[USE_YN]
                                                         ,[UP_USER]
                                                         ,[UP_DATE]
                                                         ,[REG_USER]
                                                         ,[REG_DATE])
                                                   VALUES
                                                         (@제품코드
                                                         ,@제품명
                                                         ,@제품타입
                                                         ,@제품규격
                                                         ,ISNULL((select TOP 1 code_name
                                                           from Code_Mst 
                                                           where 1=1
                                                           and code_type = @제품단위
                                                           and code_name = 'EA'),'CD04001')
                                                         ,@제품재질
                                                         ,60
                                                         ,@사용유무
                                                         ,'{MainForm.UserEntity.user_account}'
                                                         ,'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                                         ,'{MainForm.UserEntity.user_account}'
                                                         ,'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')
                                                                                        END";

                                    SqlParameter[] pDataParameters = new SqlParameter[]
                                    {
                                             new SqlParameter("@제품코드   ".Trim(), SqlDbType.VarChar, 500),
                                             new SqlParameter("@제품명     ".Trim(), SqlDbType.VarChar, 500),
                                             new SqlParameter("@제품단위   ".Trim(), SqlDbType.VarChar, 500),
                                             new SqlParameter("@제품규격   ".Trim(), SqlDbType.VarChar, 500),
                                             new SqlParameter("@제품재질   ".Trim(), SqlDbType.VarChar, 500),
                                             new SqlParameter("@사용유무   ".Trim(), SqlDbType.VarChar, 500),
                                             new SqlParameter("@제품타입   ".Trim(), SqlDbType.VarChar, 500),
                                    };

                                    pDataParameters[0].Value = 제품코드;
                                    pDataParameters[1].Value = 제품명 ;
                                    pDataParameters[2].Value = 제품단위;
                                    pDataParameters[3].Value = 제품규격;
                                    pDataParameters[4].Value = 제품재질;
                                    pDataParameters[5].Value = 사용유무;
                                    pDataParameters[6].Value = 제품타입;

                                    foreach (SqlParameter pDbParameter in pDataParameters)
                                    {
                                        cmd.Parameters.Add(pDbParameter);
                                    }

                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear(); 


                                }
                                break;
                            case "금형":
                                for (int i = 1; i < 10000; i++)
                                {
                                    Application.DoEvents();
                                    #region ○ 변수 정리

                                    if (sheet.Rows[i][0].Value.ToString().Trim() == "")
                                    {
                                        i = 10000;
                                        continue;
                                    }

                    
                                    string 금형코드 = sheet.Rows[i][1].Value.ToString().Trim();
                                    string 제품명   = sheet.Rows[i][2].Value.ToString().Trim();
                                    string 규격     = sheet.Rows[i][3].Value.ToString().Trim();


                                    #endregion

                                    cmd.CommandText = $@"
                                                    IF EXISTS (SELECT 1 FROM [dbo].[MOLD_MST] WHERE OUT_CODE = @금형코드)
                                                    BEGIN
                                                        -- 존재하면 업데이트
                                                     UPDATE[dbo].[MOLD_MST]
                                                       SET [MOLD_NAME]         = @제품명
                                                          ,[MOLD_STANDARD]     = @규격
                                                      WHERE OUT_CODE           = @금형코드;
                                                    
                                                    END
                                                    ELSE
                                                    BEGIN
                                                   INSERT INTO [dbo].[MOLD_MST]
                                                         ([OUT_CODE]
                                                         ,[MOLD_NAME]
                                                         ,[MOLD_STANDARD]
                                                         ,[MOLD_TYPE]
                                                         ,[MOLD_MANUFACTURER]
                                                         ,[MOLD_START_DATE]
                                                         ,[MOLD_END_DATE]
                                                         ,[MOLD_USE_QTY]
                                                         ,[MOLD_SIZE]
                                                         ,[MOLD_RANKING]
                                                         ,[USE_YN]
                                                         ,[UP_USER]
                                                         ,[UP_DATE]
                                                         ,[REG_USER]
                                                         ,[REG_DATE])
                                                   VALUES
                                                         (@금형코드
                                                         ,@제품명
                                                         ,@규격
                                                         ,'CD26001'
                                                         ,NULL
                                                         ,'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                                         ,'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                                         ,0
                                                         ,NULL
                                                         ,'CD19001'
                                                         ,'Y'
                                                         ,'{MainForm.UserEntity.user_account}'
                                                         ,'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                                         ,'{MainForm.UserEntity.user_account}'
                                                         ,'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')
                                                         END";

                                    SqlParameter[] pDataParameters = new SqlParameter[]
                                    {
                                             new SqlParameter("@금형코드   ".Trim(), SqlDbType.VarChar, 500),
                                             new SqlParameter("@제품명     ".Trim(), SqlDbType.VarChar, 500),
                                             new SqlParameter("@규격       ".Trim(), SqlDbType.VarChar, 500),                                 
                                    };

                                    pDataParameters[0].Value = 금형코드;
                                    pDataParameters[1].Value = 제품명;
                                    pDataParameters[2].Value = 규격;
                                    foreach (SqlParameter pDbParameter in pDataParameters)
                                    {
                                        cmd.Parameters.Add(pDbParameter);
                                    }

                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();
                                }
                                break;

                            case "사출BOM":
                            case "압출BOM":
                            case "외주BOM":
                            case "조립BOM":
                            case "포장BOM":
                                
                                for (int i = 1; i < 100000; i++)
                                {
                                    #region ○ 변수 정리

                                    if (sheet.Rows[i][0].Value.ToString().Trim() == "")
                                    {
                                        i = 100000;
                                        continue;
                                    }
                                    string 상위   = sheet.Rows[i][1].Value.ToString().Trim();
                                    string 하위   = sheet.Rows[i][3].Value.ToString().Trim();
                                    string 소모량 = sheet.Rows[i][5].Value.ToString().Trim();
                                    string 공정   = sheet.Rows[i][6].Value.ToString().Trim();
                                    string 금형   = sheet.Rows[i][7].Value.ToString().Trim();
                                    string 캐비티 = sheet.Rows[i][8].Value.ToString().Trim();


                                    #endregion
                                    Application.DoEvents();
                                    cmd.CommandText = $@"DELETE 
                                                    FROM BOM 
                                                    WHERE STOCK_MST_ID =
                                                    (
                                                    SELECT ID 
                                                    FROM STOCK_MST
                                                    WHERE OUT_CODE = @상위
                                                    ) AND FORMAT(REG_DATE, 'yyyy-MM-dd') != FORMAT(GETDATE(), 'yyyy-MM-dd') ;


                                                        IF NOT EXISTS (SELECT 1 FROM [dbo].[BOM] WHERE STOCK_MST_ID = 
                                                        (SELECT TOP 1 ID
                                                        FROM [dbo].[STOCK_MST]
                                                        WHERE OUT_CODE= @상위)
                                                        )
                                                       BEGIN
                                                       
                                                           INSERT INTO [dbo].[BOM]
                                                                  ([STOCK_MST_ID]
                                                                  ,[SUB_STOCK_MST_ID]
                                                                  ,[SEQ]
                                                                  ,[PRODUCTION_QTY]
                                                                  ,[CONSUME_QTY]
                                                                  ,[COMMENT]
                                                                  ,[USE_YN]
                                                                  ,[REG_USER]
                                                                  ,[REG_DATE]
                                                                  ,[UP_USER]
                                                                  ,[UP_DATE])
                                                               SELECT TOP 1 
                                                                ID
                                                               ,ID
                                                               ,0
                                                               ,1
                                                               ,0
                                                               ,''
                                                               ,'Y'
                                                               ,'{MainForm.UserEntity.user_account}'
                                                               ,'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                                               ,'{MainForm.UserEntity.user_account}'
                                                               ,'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                                               FROM [dbo].[STOCK_MST]
                                                               WHERE OUT_CODE= @상위;
                                                         
                                                       END
                                                         IF EXISTS (
                                                          SELECT TOP 1 ID
                                                          FROM [dbo].[STOCK_MST]
                                                          WHERE OUT_CODE= @하위
                                                          )
                                                          BEGIN
                                                           INSERT INTO [dbo].[BOM]
                                                                  ([STOCK_MST_ID]
                                                                  ,[SUB_STOCK_MST_ID]
                                                                  ,[SEQ]
                                                                  ,[PRODUCTION_QTY]
                                                                  ,[CONSUME_QTY]
                                                                  ,[COMMENT]
                                                                  ,[USE_YN]
                                                                  ,[REG_USER]
                                                                  ,[REG_DATE]
                                                                  ,[UP_USER]
                                                                  ,[UP_DATE])
                                                               SELECT TOP 1 
                                                                ID
                                                               ,(SELECT TOP 1 ID
                                                                   FROM [dbo].[STOCK_MST]
                                                                  WHERE OUT_CODE= @하위)
                                                               ,1
                                                               ,0
                                                               ,@소모량
                                                               ,''
                                                               ,'Y'
                                                               ,'{MainForm.UserEntity.user_account}'
                                                               ,'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                                               ,'{MainForm.UserEntity.user_account}'
                                                               ,'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                                               FROM [dbo].[STOCK_MST]
                                                               WHERE OUT_CODE= @상위;
                                                          END 

                                                          update [dbo].[STOCK_MST]
                                                          set PROCESS_ID  = ISNULL((SELECT TOP 1 ID FROM PROCESS  WHERE OUT_CODE = @공정),null)
                                                             ,MOLD_MST_ID = ISNULL((SELECT TOP 1 ID FROM MOLD_MST WHERE OUT_CODE = @금형),null)
                                                             ,MOLD_QTY    = @캐비티
                                                          where ID =(SELECT TOP 1 ID FROM STOCK_MST WHERE OUT_CODE = @상위)";

                                    SqlParameter[] pDataParameters = new SqlParameter[]
                                    {
                                             new SqlParameter("@상위    ".Trim(), SqlDbType.VarChar, 500),
                                             new SqlParameter("@하위    ".Trim(), SqlDbType.VarChar, 500),
                                             new SqlParameter("@소모량  ".Trim(), SqlDbType.VarChar, 500),
                                             new SqlParameter("@공정    ".Trim(), SqlDbType.VarChar, 500),
                                             new SqlParameter("@금형    ".Trim(), SqlDbType.VarChar, 500),
                                             new SqlParameter("@캐비티  ".Trim(), SqlDbType.VarChar, 500),
                                    };

                                    pDataParameters[0].Value = 상위  ;
                                    pDataParameters[1].Value = 하위  ;
                                    pDataParameters[2].Value = 소모량;
                                    pDataParameters[3].Value = 공정  ;
                                    pDataParameters[4].Value = 금형  ;
                                    pDataParameters[5].Value = 캐비티;
                                    foreach (SqlParameter pDbParameter in pDataParameters)
                                    {
                                        cmd.Parameters.Add(pDbParameter);
                                    }
                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();
                                }
                                break;


                        }
                    }

                    con.Close();
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

        public override void _InitialButtonClicked(object sender, EventArgs e)
        {
            try
            {
                _Mst_Id = string.Empty;
                treeView1.Nodes.Clear();
                DataTable pDataTable = new CoreBusiness().BASE_MENU_SETTING_R10(this._pMenuSettingEntity.MENU_WINDOW_NAME, fpMain, this._pMenuSettingEntity.BASE_TABLE.Split('/')[0]);

                Function.Core.InitializeControl(pDataTable, fpMain, this, _PAN_WHERE, _pMenuSettingEntity);
                fpMain.Sheets[0].Rows.Count = 0;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        public override void SubFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub.Sheets[0].Rows.Count = 0;

                string sql = $@"select A.ID				   AS 'ID'				 
                                      ,A.STOCK_MST_ID	   AS 'A.STOCK_MST_ID'	 
	                                  ,B.OUT_CODE		   AS 'B.OUT_CODE'		 
                                      ,A.SUB_STOCK_MST_ID  AS 'A.SUB_STOCK_MST_ID'
	                                  ,C.OUT_CODE		   AS 'C.OUT_CODE'		 
                                      ,A.SEQ			   AS 'A.SEQ'			 
                                      ,A.PRODUCTION_QTY	   AS 'A.PRODUCTION_QTY'	 
                                      ,A.CONSUME_QTY	   AS 'A.CONSUME_QTY'	 
                                      ,A.COMMENT		   AS 'A.COMMENT'		 
                                      ,A.USE_YN			   AS 'A.USE_YN'			 
                                      ,A.REG_USER		   AS 'A.REG_USER'		 
                                      ,A.REG_DATE		   AS 'A.REG_DATE'		 
                                      ,A.UP_USER		   AS 'A.UP_USER'		 
                                      ,A.UP_DATE		   AS 'A.UP_DATE'		 
                                  from BOM A
                                  INNER JOIN [dbo].[STOCK_MST] B ON A.STOCK_MST_ID = B.ID
                                  INNER JOIN [dbo].[STOCK_MST] C ON A.SUB_STOCK_MST_ID = C.ID
                                  WHERE 1=1
                                  AND A.USE_YN ='Y'
                                  AND A.STOCK_MST_ID = '{_Mst_Id}'";
                                  
                DataTable _DataTable = new CoreBusiness().SELECT(sql);


                Function.Core.DisplayData_Set(_DataTable, fpSub);
                //DataTable _DataTable = new CoreBusiness().DoubleBaseForm_R10(_Mst_Id,this._pMenuSettingEntity.BASE_TABLE);
                //Function.Core.DisplayData_Set(_DataTable, fpSub);
                //if (_DataTable != null && _DataTable.Rows.Count > 0)
                //{
                //    fpSub.Sheets[0].Visible = false;
                //    fpSub.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                //    for (int i = 0; i < _DataTable.Rows.Count; i++)
                //    {
                //        foreach (DataColumn item in _DataTable.Columns)
                //        {
                //            fpSub.Sheets[0].SetValue(i, item.ColumnName, _DataTable.Rows[i][item.ColumnName]);
                //        }

                //    }

                //    fpSub.Sheets[0].Visible = true;


                //}
                //else
                //{
                //    Function.Core._AddItemButtonClicked(fpSub, MainForm.UserEntity.user_account);

                //    string mst=  this._pMenuSettingEntity.BASE_TABLE.Split('/')[0] +"_ID";
                //    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_ID", _Mst_Id);
                //    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "SUB_STOCK_MST_ID", _Mst_Id);
                //    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "PRODUCTION_QTY", 1);
                //    //CustomMsg.ShowMessage("조회 내역이 없습니다.");
                //}

                Function.Core.DisplayBOM_Set(_Mst_Id,treeView1);



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


     
    }
    public class BOMNode
    {
        public string 상위 { get; set; }
        public string 하위 { get; set; }
        public int Quantity { get; set; }
        public int 소요량 { get; set; }
        public string 파트코드 { get; set; }
        public string 파트명 { get; set; }
        public string 규격 { get; set; }
        public string 타입 { get; set; }
        public string 공정 { get; set; }
        public BOMNode(string name, int quantity)
        {
            파트코드 = name;
            Quantity = quantity;
        }

        public List<BOMNode> Children { get; set; } = new List<BOMNode>();

    }

}
