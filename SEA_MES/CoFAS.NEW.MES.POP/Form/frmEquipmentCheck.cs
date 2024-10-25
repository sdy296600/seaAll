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
    public partial class frmEquipmentCheck : Form
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

        public string _btn_Name= string.Empty;
        public string _TYPE = string.Empty;
        #endregion

        #region ○ 생성자

        public frmEquipmentCheck(UserEntity pUserEntity,string pTYPE,string pNAME)
        {
            InitializeComponent();

            _UserEntity = pUserEntity;

            _TYPE = pTYPE;
            //SD18001 생산점검
            //SD18002 안전점검
            //SD18003 설비점검
            switch (_TYPE)
            {
                case "SD18001":
                    _설비점검.Visible = false;

                    break;
                case "SD18002":
                case "SD18003":
                    _초품점검.Visible = false;

                    break;
             
                default:
                    break;
            }
 
            
            label1.Text = pNAME;

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
                DataTable pDataTable =  new CoreBusiness().BASE_MENU_SETTING_R10(this.Name ,fpMain,"EQUIP_INSPECTION_LIST");

                if (pDataTable != null)
                {
                    //InitializeControl(pDataTable);

                    Core.Function.Core.initializeSpread(pDataTable, fpMain, this.Name, _UserEntity.user_account);
                    fpMain._ChangeEventHandler += FpMain_Change;
                    //fpMain.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE, FontStyle.Bold);


                }

                Equipment_Setting();

            
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
        private void Equipment_Setting()
        {
            try
            {
                int x = 0;

                int y = 1;

                int btnY = panel1.Height / 3 - 5;

                int btnX = panel1.Width;

                DataTable  dt = new MS_DBClass(_MY).Equipment_Setting();

                foreach (DataRow item in dt.Rows)
                {
                    Button btn = new Button();

                    btn.Name = item["ID"].ToString().Trim();

                    btn.Text = item["NAME"].ToString().Trim();


                    btn.Font = new Font(this.Font.Name, 12,FontStyle.Bold);
                    btn.TextAlign = ContentAlignment.MiddleLeft;
                    if (x > btnX - 210)
                    {
                        y += btnY;
                        x = 0;
                    }

                    btn.Location = new Point(1 + x, y);

                    x += 210;

                    btn.Size = new Size(210, btnY);

                    btn.Click += Equipment_Click;

                    btn.TextAlign = ContentAlignment.MiddleCenter;

                    btn.ForeColor = Color.White;
                    btn.BackColor = Color.FromArgb(116, 142, 172);

                    panel1.Controls.Add(btn);
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void Equipment_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn1 = sender as Button;
                _btn_Name = btn1.Name;
                EquipmentCheck_Setting(btn1.Name);

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void EquipmentCheck_Setting(string pEquipment)
        {
            try
            {
                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"SELECT 
                                            ISNULL(D.ID ,'')             AS ID
                                           ,A.TYPE
                                            ,A.ID                         AS EQUIP_INSPECTION_ID
                                           ,A.EQUIPMENT_ID   
                                           ,A.EQUIPMENT_NAME   
                                           ,ISNULL(CHECK_DATE,GETDATE()) AS CHECK_DATE
                                           ,B.code_name                  AS CATEGORY1
                                           ,C.code_name　                AS CATEGORY2 
                                           ,C.code_description           AS CATEGORY3 
                                           ,ISNULL(D.CHECK_USER_NAME,'') AS CHECK_USER_NAME
                                           ,ISNULL(D.CHECK_YN ,'')       AS CHECK_YN
                                           ,ISNULL(D.USER_NAME,'')       AS USER_NAME
                                           ,ISNULL(D.CATEGORY4,'')       AS CATEGORY4
                                           ,ISNULL(D.CATEGORY5,'')       AS CATEGORY5 
                                           ,ISNULL(D.CATEGORY6,'')       AS CATEGORY6 
                                           ,ISNULL(D.CATEGORY7,'')       AS CATEGORY7 
                                           ,D.COMMENT
                                           ,ISNULL(D.USE_YN  ,'Y')      AS USE_YN  
                                           ,ISNULL(D.UP_USER ,'')       AS UP_USER 
                                           ,ISNULL(D.UP_DATE ,GETDATE())AS UP_DATE 
                                           ,ISNULL(D.REG_USER,'')       AS REG_USER
                                           ,ISNULL(D.REG_DATE,GETDATE())AS REG_DATE         
                                      FROM [dbo].[EQUIP_INSPECTION] A
                                     inner join [dbo].[Code_Mst] B ON A.Category１ = B.code
                                     inner join [dbo].[Code_Mst] C ON A.Category２ = C.code
                                      left join [dbo].[EQUIP_INSPECTION_LIST] D ON A.EQUIPMENT_ID = D.EQUIPMENT_ID and A.ID = D.EQUIP_INSPECTION_ID AND D.CHECK_DATE = FORMAT(GETDATE(), 'yyyy-MM-dd')
                                     WHERE 1=1
                                       AND A.EQUIPMENT_ID = {pEquipment} 
                                       AND A.TYPE = '{_TYPE}'
                                     
                                     ORDER BY B.code_name";
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

                    lab_CD19.Text   = _DataTable.Rows[0]["CHECK_USER_NAME"].ToString();
                    lab_최종_1.Text = _DataTable.Rows[0]["CATEGORY7"].ToString();
                    lab_CD20_1.Text = _DataTable.Rows[0]["CATEGORY4"].ToString();
                    lab_CD20_2.Text = _DataTable.Rows[0]["CATEGORY5"].ToString();
                    lab_CD20_3.Text = _DataTable.Rows[0]["CATEGORY6"].ToString();
                    lab_최종_2.Text = _DataTable.Rows[0]["CATEGORY7"].ToString();
                    fpMain.Sheets[0].Visible = true;


                }
                else
                {
                    fpMain.Sheets[0].Rows.Count = 0;
                    CustomMsg.ShowMessage("조회 내역이 없습니다.");
                }




            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                //DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                if (_btn_Name != string.Empty)
                {
                    fpMain.Focus();

                    MenuSettingEntity _pMenuSettingEntity = new MenuSettingEntity();
                    bool _Error = new CoreBusiness().BaseForm1_A10(_pMenuSettingEntity,fpMain,"EQUIP_INSPECTION_LIST");
                    if (!_Error)
                    {
                        CustomMsg.ShowMessage("저장되었습니다.");
                        //DisplayMessage("저장 되었습니다.");

                        EquipmentCheck_Setting(_btn_Name);

                        //MainFind_DisplayData();
                    }
                }
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

    

        private void lab_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;


            string str = label.Name.Split('_')[1];
            using (from_PWD popup = new from_PWD(str))
            {
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    string name = "";

                    switch (str)
                    {
                        case "최종":
                            name = "CATEGORY7";
                            break;
                        case "CD19":
                            name = "CHECK_USER_NAME";
                            break;
                        case "CD20":                      
                            switch (label.Name.Split('_')[2])
                            {
                                case "1":
                                    name = "CATEGORY4";
                                    break;
                                case "2":
                                    name = "CATEGORY5";
                                    break;
                                case "3":
                                    name = "CATEGORY6";
                                    break;
                                default:
                                    break;
                            }                
                            break;
                        case "CD21":
                            switch (label.Name.Split('_')[2])
                            {
                                case "1":
                                    name = "CATEGORY8";
                                    break;
                                case "2":
                                    name = "CATEGORY9";
                                    break;
                                case "3":
                                    name = "CATEGORY10";
                                    break;
                                default:
                                    break;
                            }
                            break;


                        default:
                            break;
                    }

                    for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                    {
                        fpMain.Sheets[0].SetValue(i, name, popup._code);
                        if (fpMain.Sheets[0].RowHeader.Cells[i, 0].Text != "입력")
                        {
                            fpMain.Sheets[0].RowHeader.Cells[i, 0].Text = "수정";
                        }
                    }
                    label.Text = popup._code;
                }
            }

           
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    public class EQUIP_INSPECTION_LIST
    {
        public string TYPE { get; set; }
        public int EQUIP_INSPECTION_ID { get; set; }
        public int EQUIPMENT_ID { get; set; }
        public int EQUIPMENT_NAME { get; set; }     
        public string CHECK_DATE { get; set; }
        public string CATEGORY1 { get; set; }
        public string CATEGORY2 { get; set; }
        public string CATEGORY3 { get; set; }
        public string CHECK_USER_NAME { get; set; }
        public string CHECK_YN { get; set; }
        public string USER_NAME { get; set; }
        public string CATEGORY4 { get; set; }
        public string CATEGORY5 { get; set; }
        public string CATEGORY6 { get; set; }
        public string COMMENT { get; set; }
        public string USE_YN { get; set; }
        public string REG_USER { get; set; }
        public string REG_DATE { get; set; }
        public string UP_USER { get; set; }
        public string UP_DATE { get; set; }
    }
}





