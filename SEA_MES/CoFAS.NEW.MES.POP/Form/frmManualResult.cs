using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.POP.Function;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP
{
    public partial class frmManualResult : Form
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

        public DataRow _ProductionInstruct = null;
        #endregion

        #region ○ 생성자

        public frmManualResult(UserEntity pUserEntity,DataRow pProductionInstruct)
        {
            InitializeComponent();
            _UserEntity = pUserEntity;

            _ProductionInstruct = pProductionInstruct;
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
                lbl_product_name.Text = _ProductionInstruct["NAME"].ToString();
                lbl_product_code.Text = _ProductionInstruct["STOCK_OUT_CODE"].ToString();

                btn_0.Click += button_Click;
                btn_1.Click += button_Click;
                btn_2.Click += button_Click;
                btn_3.Click += button_Click;
                btn_4.Click += button_Click;
                btn_5.Click += button_Click;
                btn_6.Click += button_Click;
                btn_7.Click += button_Click;
                btn_8.Click += button_Click;
                btn_9.Click += button_Click;

                btn_C.Click += btn_C_Click;
                btn_del.Click += btn_del_Click;

                //DataTable  dt1 = new MS_DBClass(_MY).Spread_ComobBox("CODE","CD15","");     
                //foreach (DataRow item in dt1.Rows)
                //{
                //    ComboboxItem coitme = new ComboboxItem(){ };
                //    coitme.Text = item["CD_NM"].ToString();
                //    coitme.Value = item["CD"].ToString();

                //    WORK_TYPE.Items.Add(coitme);
                //}  string lot = "";
                string lot = "";
                if (DateTime.Now.Hour < 20)
                {
                    lot = "A";
                }
                else
                {
                    lot = "B";
                }

                제품Lot정보.Text = _ProductionInstruct["NAME"].ToString() + "-" + DateTime.Now.ToString("yyMMdd") + lot;
                제품Lot정보.Font = this.Font; 

                DataTable  dt2 = new MS_DBClass(_MY).Spread_ComobBox("CODE","CD16","");

                //comboBox1.Items.Add(new ComboboxItem() { Text = "all", Value = "" });
                foreach (DataRow item in dt2.Rows)
                {
                    ComboboxItem coitme = new ComboboxItem(){ };
                    coitme.Text = item["CD_NM"].ToString();
                    coitme.Value = item["CD"].ToString();

                    RESULT_TYPE.Items.Add(coitme);
                }
                RESULT_TYPE.SelectedIndex = 0;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
        #endregion


        private void button_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbl_qty.Text.Length == 10)
                {
                    return;
                }

                Button btn = sender as Button;

                lbl_qty.Text += btn.Text;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        private void btn_del_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbl_qty.Text.Length != 0)
                {
                    lbl_qty.Text = lbl_qty.Text.Substring(0, lbl_qty.Text.Length - 1);
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        private void btn_C_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_qty.Text = "";
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
                if (제품Lot정보.Text.Length == 0)
                {
                    CustomMsg.ShowMessage("제품 Lot 정보을 입력해 주세요.");
                    return;
                }
                string mes = "저장 하시겠습니까?";

                if (CustomMsg.ShowMessage(mes, "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int qty = int.Parse(lbl_qty.Text);

                    List<PRODUCTION_RESULT> list = new List<PRODUCTION_RESULT>();
                    if (qty > 0)
                    {
                        PRODUCTION_RESULT entity = new PRODUCTION_RESULT();
                        entity.LOT_NO = 제품Lot정보.Text;
                        entity.PRODUCTION_INSTRUCT_ID = Convert.ToInt32(_ProductionInstruct["ID"]);
                        entity.STOCK_MST_ID = Convert.ToInt32(_ProductionInstruct["STOCK_MST_ID"]);
                        entity.STOCK_MST_OUT_CODE = Convert.ToInt32(_ProductionInstruct["STOCK_MST_ID"]);
                        entity.STOCK_MST_STANDARD = Convert.ToInt32(_ProductionInstruct["STOCK_MST_ID"]);
                        entity.STOCK_MST_TYPE = Convert.ToInt32(_ProductionInstruct["STOCK_MST_ID"]);
                    
                        entity.RESULT_TYPE = (RESULT_TYPE.SelectedItem as ComboboxItem).Value.ToString();

                        if (entity.RESULT_TYPE == "CD16001")
                        {
                            entity.OK_QTY = qty;
                        }
                        else
                        {
                            entity.NG_QTY = qty;
                        }
                        entity.TOTAL_QTY = 0;
                        entity.COMMENT = 비고.Text;
                        entity.USE_YN = "Y";
                        entity.REG_USER = _UserEntity.user_account;
                        entity.REG_DATE = DateTime.Now;
                        entity.UP_USER = _UserEntity.user_account;
                        entity.UP_DATE = DateTime.Now;

                        list.Add(entity);
                    }


                    new MS_DBClass(_MY).PRODUCTION_RESULT(list);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
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
    }
}





