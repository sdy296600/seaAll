using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.POP.Function;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP
{
    public partial class 작업표준서 : Form
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
        public My_Settings _MY =  utility.My_Settings_Get();

        private UserEntity _UserEntity = new UserEntity();

        public string _pcode = null;

        string _line   = string.Empty;
        #endregion

        #region ○ 생성자

        public 작업표준서(UserEntity pUserEntity,string code)
        {
            InitializeComponent();
            _UserEntity = pUserEntity;
            _pcode = code;

           _MY = utility.My_Settings_Get();

            Load += new EventHandler(Form_Load);

            this.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE, FontStyle.Bold);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
        }
        public 작업표준서( )
        {
            InitializeComponent();
          
            Load += new EventHandler(Form_Load);

            this.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
        }
        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                라인_Setting();
 
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
        #endregion

        private void 라인_Setting()
        {
            try
            {
                int x = 0;

                int y = 1;

                int btnY = _pna_라인.Height / 1 - 5;

                int btnX = _pna_라인.Width;

                string sql = @"select code      AS ID
                                    ,code_name  AS NAME
                                    from Code_Mst
                                    where 1 = 1
                                    and code_type = 'CD14'
                                    and use_yn = 'Y'";

                DataTable  dt = new CoreBusiness().SELECT(sql);

                //DataTable  dt = new MS_DBClass(_MY).Equipment_Setting();

                foreach (DataRow item in dt.Rows)
                {
                    Button btn = new Button();

                    btn.Name = item["ID"].ToString().Trim();

                    btn.Text = item["NAME"].ToString().Trim();


                    btn.Font = new Font(this.Font.Name, 12, FontStyle.Bold);
                    btn.TextAlign = ContentAlignment.MiddleLeft;
                    if (x > btnX - 210)
                    {
                        y += btnY;
                        x = 0;
                    }

                    btn.Location = new Point(1 + x, y);

                    x += 210;

                    btn.Size = new Size(210, btnY);

                    btn.Click += 라인_Click;

                    btn.TextAlign = ContentAlignment.MiddleCenter;

                    btn.ForeColor = Color.White;
                    btn.BackColor = Color.FromArgb(116, 142, 172);

                    _pna_라인.Controls.Add(btn);
                }

                btn_1pc.Visible = false;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        private void 라인_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn1 = sender as Button;

                _pna_설비.Controls.Clear();
                int x = 0;

                int y = 1;

                int btnY = _pna_설비.Height / 3 - 5;

                int btnX = _pna_설비.Width;

                string sql = $@"SELECT ID 
                               ,NAME
                               FROM EQUIPMENT
                               WHERE 1=1
                               AND TYPE = '{btn1.Name}'
                               AND USE_YN = 'Y'";

                DataTable  dt = new CoreBusiness().SELECT(sql);

                //DataTable  dt = new MS_DBClass(_MY).Equipment_Setting();

                foreach (DataRow item in dt.Rows)
                {
                    Button btn = new Button();

                    btn.Name = item["ID"].ToString().Trim();

                    btn.Text = item["NAME"].ToString().Trim();


                    btn.Font = new Font(this.Font.Name, 12, FontStyle.Bold);
                    btn.TextAlign = ContentAlignment.MiddleLeft;
                    if (x > btnX - 210)
                    {
                        y += btnY;
                        x = 0;
                    }

                    btn.Location = new Point(1 + x, y);

                    x += 210;

                    btn.Size = new Size(210, btnY);

                    btn.Click += 설비_Click;

                    btn.TextAlign = ContentAlignment.MiddleCenter;

                    btn.ForeColor = Color.White;
                    btn.BackColor = Color.FromArgb(116, 142, 172);

                    _pna_설비.Controls.Add(btn);
                }
                _line = btn1.Name;

                if (btn1.Name != "CD14001")
                {
                    btn_1pc.Visible = false;
                }
                else
                {
                    btn_1pc.Visible = true;
                }
                //BaseImagePopupBox baseImagePopupBox = new BaseImagePopupBox(btn1.Text,btn1.Name);

                //baseImagePopupBox.Show();


            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void 설비_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn1 = sender as Button;

                BaseImagePopupBox baseImagePopupBox = new BaseImagePopupBox(btn1.Text,btn1.Name,_line);

                baseImagePopupBox.Show();


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

        private void btn_1pc_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = $@"    SELECT  CAST(ID       AS VARCHAR) AS CD
				                           ,CAST(NAME	    AS VARCHAR)	AS CD_NM
				                           ,CAST(OUT_CODE AS VARCHAR) AS STOCK_MST_OUT_CODE
				                           ,CAST(UNIT     AS VARCHAR) AS STOCK_MST_UNIT    
				                           ,CAST(PRICE    AS VARCHAR) AS STOCK_MST_PRICE    
				                           ,CAST(STANDARD AS VARCHAR) AS STOCK_MST_STANDARD 
				                           ,CAST(TYPE     AS VARCHAR) AS STOCK_MST_TYPE 
				                           ,CAST(ID       AS VARCHAR) AS STOCK_MST_ID 
				                           ,QTY
                                       FROM [dbo].[STOCK_MST]
			                           WHERE 1=1
			                             AND TYPE like '%SD04%'
			                             AND USE_YN = 'Y'
                                         AND LINE = 'CD14001'
                                   ORDER BY ID	";

                DataTable pDataTable = new CoreBusiness().SELECT(sql);
                BasePopupBox basePopupBox = new BasePopupBox();
                basePopupBox.Name = "BaseProductPopupBox";
                basePopupBox._pDataTable = pDataTable;

                basePopupBox._UserAccount = "admin";
                if (basePopupBox.ShowDialog() == DialogResult.OK)
                {
                    DialogResult _DialogResult1 = CustomMsg.ShowMessage($"1PC LINE 에 " +
                          $"{basePopupBox._pdataRow["STOCK_MST_OUT_CODE"].ToString()} " + " / "+
                          $"{basePopupBox._pdataRow["CD_NM"].ToString()} " +
                          $"제품을 생산 하시겠습니까?", "Question", MessageBoxButtons.YesNo);

                    if (_DialogResult1 == DialogResult.Yes)
                    {
                        sql = $@"UPDATE [dbo].[Code_Mst]
                                         SET code_name = '{basePopupBox._pdataRow["STOCK_MST_ID"].ToString()}'
                                         WHERE code = 'SD21001'";

                        new CoreBusiness().SELECT(sql);

                    }


                }
            }
            catch(Exception err)
            {

            }
        }
    }
}





