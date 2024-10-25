using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;

namespace CoFAS.NEW.MES.POP
{
    public partial class from_압출종료 : Form
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



        #endregion

        #region ○ 생성자
        string _pSTOCK_SMT_ID = string.Empty;
        string _pOUT_CODE    = string.Empty;
        public from_압출종료(string pSTOCK_SMT_ID, string pOUT_CODE)
        {

            InitializeComponent();

            Load += new EventHandler(Form_Load);
            _pSTOCK_SMT_ID = pSTOCK_SMT_ID;
            _pOUT_CODE     = pOUT_CODE;
        }

        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                this.fpMain.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpMain_CellClick);
                de_시간.DateTime = DateTime.Now;
                //this.WindowState = FormWindowState.Maximized;
                this.MinimumSize = this.Size;
                this.MaximumSize = this.Size;

                de_시간.Font = this.Font;


                de_시간.Margin = new Padding(3, 3, 3, 3);

                DataTable pDataTable =  new CoreBusiness().BASE_MENU_SETTING_R10(this.Name,fpMain,"STOCK_MST");

                if (pDataTable != null)
                {
                    CoFAS.NEW.MES.Core.Function.Core.initializeSpread(pDataTable, fpMain, this.Name, "admin");
                    CoFAS.NEW.MES.Core.Function.Core.InitializeControl(pDataTable, fpMain, this, _PAN_WHERE, null);
                }

                lbl_제품코드.Text = _pOUT_CODE;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }





        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btn_조회_Click(object sender, EventArgs e)
        {
            try
            {

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@" SELECT  ID
			                            ,NAME	
			 	                        ,TYPE2  
			 	                        ,OUT_CODE
			 	                        ,UNIT    
			 	                        ,PRICE    
			 	                        ,STANDARD 
			 	                        ,TYPE         
			 	                        ,QTY
			 	                        ,IN_SCHEDULE
			 	                        ,OUT_SCHEDULE			 	                      		 	                 
                                         FROM [dbo].[STOCK_MST]
			                                  WHERE 1=1
			                                    AND TYPE like '%SD04%'
			                                    AND USE_YN = 'Y'
                                                AND PROCESS_ID = 7";
                                      
                //LEFT JOIN [dbo].[OUT_STOCK_DETAIL]  E ON C.ID = E.IN_STOCK_DETAIL_ID AND A.ID = E.PRODUCTION_INSTRUCT_ID AND E.USE_YN = 'Y'
                StringBuilder sb = new StringBuilder();

                CoFAS.NEW.MES.Core.Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString();



                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                CoFAS.NEW.MES.Core.Function.Core.DisplayData_Set(_DataTable, fpMain);
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        private void fpMain_CellClick(object sender, CellClickEventArgs e)
        {
            try
            {
                _pSTOCK_SMT_ID    = fpMain.Sheets[0].GetValue(e.Row, "ID").ToString();
                lbl_제품코드.Text = fpMain.Sheets[0].GetValue(e.Row, "OUT_CODE").ToString();
            }
            catch(Exception err)
            {

            }
  
        }
    }
}



