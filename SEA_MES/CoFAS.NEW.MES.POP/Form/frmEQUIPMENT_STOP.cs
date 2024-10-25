using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.POP.Function;
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
    public partial class frmEQUIPMENT_STOP : Form
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
                this.WindowState = FormWindowState.Maximized;
                this.Refresh();
                this.Invalidate();
                this.SetStyle(ControlStyles.ResizeRedraw, true);
            }
        }

        #endregion

        #region ○ 변수선언
        public My_Settings _MY = null;

        private UserEntity _UserEntity = new UserEntity();

        private DataRow _ProductionInstruct = null;
        #endregion

        #region ○ 생성자

        public frmEQUIPMENT_STOP(UserEntity pUserEntity, DataRow pProductionInstruct)
        {
            InitializeComponent();
            _ProductionInstruct = pProductionInstruct;
            _UserEntity = pUserEntity;
     
            _MY = utility.My_Settings_Get();

            Load += new EventHandler(Form_Load);

            this.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE, FontStyle.Bold);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
        }

        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable pDataTable =  new CoreBusiness().BASE_MENU_SETTING_R10(this.Name ,fpMain,"EQUIPMENT_STOP");

                if (pDataTable != null)
                {
                    //InitializeControl(pDataTable);

                    Core.Function.Core.initializeSpread(pDataTable, fpMain, this.Name, _UserEntity.user_account);
                    fpMain._ChangeEventHandler += FpMain_Change;
                    //fpMain.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE, FontStyle.Bold);


                }
                //START_TIME.DateTime = DateTime.Now;
                //END_TIME.Value = DateTime.Now;
                //RESULT_TYPE.AddValue(new CoreBusiness().Spread_ComboBox("CODE", "CD12", ""), 0, 0, "", true);

                //EQUIPMENT.AddValue(new CoreBusiness().Spread_ComboBox("설비명", "CD12", ""), 0, 0, "", true);
                //RESULT_TYPE.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE, FontStyle.Bold);
                //EQUIPMENT.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE, FontStyle.Bold);

                EQUIPMENT_STOP_Setting();

                fpMain.ZoomFactor = 1.5F;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }




        #endregion
        private void FpMain_Change(object sender, ChangeEventArgs e)
        {
            try
            {
                xFpSpread xFp = sender as xFpSpread;

                for (int i = 0; i < xFp.Sheets[0].Columns.Count; i++)
                {
                    if (xFp.Sheets[0].Columns[i].Tag.ToString() == "COMPLETE_YN")
                    {
                        if (xFp.Sheets[0].GetValue(e.Row, i).ToString() != "N")
                        {
                            return;
                        }
                    }
                }
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
                    Core.Function.Core._AddItemSUM(xFp);
                    xFp.ActiveSheet.SetActiveCell(e.Row, e.Column);
                    xFp.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Center, FarPoint.Win.Spread.HorizontalPosition.Center);
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        private void EQUIPMENT_STOP_Setting()
        {
            try
            {
                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"SELECT * 
                                  FROM EQUIPMENT_STOP 
                                  WHERE 1=1
                                  AND USE_YN = 'Y'
                                  AND PRODUCTION_INSTRUCT_ID = {_ProductionInstruct["ID"].ToString()}";
                DataTable _DataTable = new CoreBusiness().SELECT(str);

                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpMain.Sheets[0].Visible = false;
                    fpMain.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        foreach (DataColumn item in _DataTable.Columns)
                        {
                            fpMain.Sheets[0].SetValue(i, item.ColumnName, _DataTable.Rows[i][item.ColumnName]);
                        }
                        if (fpMain.Sheets[0].GetValue(i, "ID").ToString() == "0")
                        {
                            fpMain.Sheets[0].SetValue(i, "UP_USER", _UserEntity.user_account);
                            fpMain.Sheets[0].SetValue(i, "REG_USER", _UserEntity.user_account);
                            fpMain.Sheets[0].RowHeader.Cells[i, 0].Text = "입력";
                        }

                    }

                    //lab_CD19.Text = _DataTable.Rows[0]["CHECK_USER_NAME"].ToString();
                    //lab_최종_1.Text = _DataTable.Rows[0]["CATEGORY7"].ToString();
                    //lab_CD20_1.Text = _DataTable.Rows[0]["CATEGORY4"].ToString();
                    //lab_CD20_2.Text = _DataTable.Rows[0]["CATEGORY5"].ToString();
                    //lab_CD20_3.Text = _DataTable.Rows[0]["CATEGORY6"].ToString();
                    //lab_최종_2.Text = _DataTable.Rows[0]["CATEGORY7"].ToString();
                    fpMain.Sheets[0].Visible = true;


                }
                else
                {
                    fpMain.Sheets[0].Rows.Count = 0;
                    //CustomMsg.ShowMessage("조회 내역이 없습니다.");
                }




            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }



        private void btn_reset_Click(object sender, EventArgs e)
        {
            try
            {
             
                //START_TIME.DateTime = DateTime.Now;

                EQUIPMENT_STOP_Setting();


            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CoFAS.NEW.MES.Core.Function.Core._AddItemButtonClicked(fpMain, _UserEntity.user_account);

                //string mst=  this._pMenuSettingEntity.BASE_TABLE.Split('/')[0] +"_ID";
                fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "PRODUCTION_INSTRUCT_ID", _ProductionInstruct["ID"].ToString());

                fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "END_TIME", DateTime.Now);
   
                //fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "EQUIPMENT_ID          ".Trim(), fpMain.Sheets[0].GetValue(row, "ID".Trim()));
                //fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "EQUIPMENT_NAME        ".Trim(), fpMain.Sheets[0].GetValue(row, "ID".Trim()));

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     

        private void button2_Click(object sender, EventArgs e)
        {
            fpMain.Focus();

            MenuSettingEntity _pMenuSettingEntity = new MenuSettingEntity();
            bool _Error = new CoreBusiness().BaseForm1_A10(_pMenuSettingEntity,fpMain,"EQUIPMENT_STOP");
            if (!_Error)
            {
                CustomMsg.ShowMessage("저장되었습니다.");
                //DisplayMessage("저장 되었습니다.");

                EQUIPMENT_STOP_Setting();

                //MainFind_DisplayData();
            }
        }
    }

    public class EQUIPMENT_STOP
    {
        public string TYPE { get; set; }
        public string PRODUCTION_INSTRUCT_ID { get; set; }
        public string EQUIPMENT_ID { get; set; }
        public string EQUIPMENT_NAME { get; set; }
        public DateTime START_TIME { get; set; }
        public DateTime END_TIME { get; set; }
        public string COMMENT { get; set; }
        public string USE_YN { get; set; }
        public string UP_USER { get; set; }
        public DateTime UP_DATE { get; set; }
        public string REG_USER { get; set; }
        public DateTime REG_DATE { get; set; }
    }

}





