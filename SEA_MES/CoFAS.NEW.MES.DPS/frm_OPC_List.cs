using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.DPS
{
    public partial class frm_OPC_List : Form
    {
        public frm_OPC_List()
        {
            InitializeComponent();
        }

        string _sqlcon = "Server=172.22.4.11,60901;Database=HS_MES;UID=MesConnection;PWD=8$dJ@-!W3b-35;";

        //string _sqlcon = "Server=127.0.0.1;Database=HS_MES;UID=sa;PWD=coever1191!;";
        private void frm_OPC_List_Load(object sender, EventArgs e)
        {
            DataGridView1ColumnSet();

            dataGridView1.DataSource = Get_OPC();
        }

        private void DataGridView1ColumnSet()
        {
            try
            {
                //DataGridViewUtil.InitSettingGridView(dataGridView1);
                //DataGridViewUtil.AddNewColumnToDataGridView(dataGridView1, "설비명     ".Trim(), "설비명     ".Trim(), true, 500, DataGridViewContentAlignment.MiddleCenter);
                //DataGridViewUtil.AddNewColumnToDataGridView(dataGridView1, "대분류     ".Trim(), "대분류     ".Trim(), true, 500, DataGridViewContentAlignment.MiddleCenter);
                //DataGridViewUtil.AddNewColumnToDataGridView(dataGridView1, "소분류     ".Trim(), "소분류     ".Trim(), true, 500, DataGridViewContentAlignment.MiddleCenter);
                //DataGridViewUtil.AddNewColumnToDataGridView(dataGridView1, "VALUE      ".Trim(), "VALUE      ".Trim(), true, 250, DataGridViewContentAlignment.MiddleCenter);
          
                //dataGridView1.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE - 5, FontStyle.Bold);
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        public DataTable Get_OPC()
        {
            DataTable dt = new DataTable();
            try
            {


                SqlConnection conn = new SqlConnection(_sqlcon);

                //// DB 연결
                //string strSql_Insert = $@"SELECT *
                //                           FROM [dbo].[Code_Mst]
                //                          WHERE code_type = '{code_type}'";
                string strSql_Insert = $@"select C.NAME as 설비명
                                                 , B.code_name as 대분류
                                                 ,A.code_name  as 소분류
                                                 ,D.VALUE
                                                 from [dbo].[Code_Mst] A
                                                 inner join [dbo].[Code_Mst] B ON A.code_type = B.code
                                                 inner join [dbo].[EQUIPMENT] C ON A.code_etc1 = C.OUT_CODE
                                                 LEFT JOIN 
                                                 (
                                                 SELECT A.*
                                                 FROM [dbo].[OPC_MST] A
                                                 INNER JOIN 
                                                 (
                                                 SELECT NAME ,MAX(READ_DATE) AS READ_DATE
                                                 FROM [dbo].[OPC_MST]
                                                 GROUP BY NAME 
                                                 ) B ON A.NAME = B.NAME AND  A.READ_DATE  = B.READ_DATE
                                                 ) D ON A.code_name = D.NAME
                                                 where  A.code_type like '%OP%'
                                                 and A.code_type !='OPC00'";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);



                conn.Open();

                System.Data.SqlClient.SqlDataReader dr;

                dt = new System.Data.DataTable();

                dr = cmd_Insert.ExecuteReader();

                dt.Load(dr);

                conn.Close();

                return dt;

            }
            catch (Exception err)
            {
                return dt;
            }
        }
    }
}
