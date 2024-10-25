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
    public partial class frmEndWork : Form
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

        public DateTime _ENDdate = new DateTime();
        public int _Qty;

        public List<BAD_ITEM> _list = new List<BAD_ITEM>();
        #endregion

        #region ○ 생성자

        public frmEndWork(int pQty, DataRow pProductionInstruct, UserEntity pUserEntity,DateTime pEND_TIME)
        {
            InitializeComponent();

            _UserEntity = pUserEntity;

            _ProductionInstruct = pProductionInstruct;

            _Qty = pQty;

            _ENDdate = pEND_TIME;

            lbl_ok_qty.Text = pQty.ToString();

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
                textBox1.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE, FontStyle.Bold);
                DataTable dt =new MS_DBClass(_MY).instruct_Get(Convert.ToInt32(_ProductionInstruct["ID"]));

                _ProductionInstruct = dt.Rows[0];

                lbl_product_code.Text = _ProductionInstruct["STOCK_OUT_CODE"].ToString();

                lbl_product_name.Text = _ProductionInstruct["NAME"].ToString();

                lbl_product_qty.Text = _Qty.ToString();

                lbl_ok_qty.Text = _Qty.ToString();

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



                DataTable  dt1 = new MS_DBClass(_MY).Spread_ComobBox("불량구분","","");

                //comboBox1.Items.Add(new ComboboxItem() { Text = "all", Value = "" });
                foreach (DataRow item in dt1.Rows)
                {
                    ComboboxItem coitme = new ComboboxItem(){ };
                    coitme.Text = item["CD_NM"].ToString();
                    coitme.Value = item["CD"].ToString();

                    comboBox1.Items.Add(coitme);
                }
                comboBox1.SelectedIndex = 0;


                string lot = "";
                if (DateTime.Now.Hour < 20)
                {
                    lot = "A";
                }
                else
                {
                    lot = "B";
                }

                textBox1.Text = _ProductionInstruct["NAME"].ToString() + "-"+DateTime.Now.ToString("yyMMdd") + lot;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
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
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbl_qty.Text.Length == 0)
                {
                    CustomMsg.ShowMessage("불량 수량을 입력해주세요");
                    return;
                }

                int badQty = int.Parse(lbl_qty.Text);

                if (badQty <= 0)
                {
                    CustomMsg.ShowMessage("불량 수량을 입력해주세요");
                    return;
                }

                int okqty = int.Parse(lbl_ok_qty.Text);

                if (okqty < badQty)
                {
                    CustomMsg.ShowMessage("불량 수량은 생산수량보다 많을수 없습니다.");
                    lbl_qty.Text = "";
                    return;
                }
                else
                {
                    lbl_ok_qty.Text = (okqty - badQty).ToString();

                    BAD_ITEM itme = new BAD_ITEM();
                    itme.KEY = DateTime.Now.ToString();
                    itme.BAD_QTY = badQty;
                    itme.BAD_TYPE = (comboBox1.SelectedItem as ComboboxItem).Value.ToString();

                    _list.Add(itme);

                    Setting_Result();
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        private void Setting_Result()
        {
            try
            {
                panel1.Controls.Clear();

                DataTable  dt = new MS_DBClass(_MY).Spread_ComobBox("불량구분","","");

                for (int i = 0; i < _list.Count; i++)
                {
                    BAD_ITEM entity = _list[i];

                    int loc = 100 * i;

                    Button btn = new Button();

                    btn.Name = entity.KEY;

                    btn.Text += lbl_product_code.Text + "\n\r";

                    btn.Text += "실적유형 : " + dt.Select($"CD ='{entity.BAD_TYPE}'")[0]["CD_NM"].ToString() + "\n\r";

                    btn.Text += "수량 : " + entity.BAD_QTY;

                    btn.Font = this.Font;

                    btn.Location = new Point(5, 10 + loc);

                    btn.Size = new Size((panel1.Size.Width - 40), 100);

                    btn.Click += ProductionResult_Click;

                    btn.TextAlign = ContentAlignment.MiddleLeft;

                    panel1.Controls.Add(btn);
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        private void ProductionResult_Click(object sender, EventArgs e)
        {
            try
            {
                if (CustomMsg.ShowMessage("삭제 하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Button btn1 = sender as Button;

                    BAD_ITEM entity = _list.Find(x => x.KEY == btn1.Name);

                    lbl_ok_qty.Text = (int.Parse(lbl_ok_qty.Text) + entity.BAD_QTY).ToString();

                    _list.Remove(entity);

                    Setting_Result();

                }

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(textBox1.Text.Length == 0)
                {
                    CustomMsg.ShowMessage("제품 Lot 정보을 입력해 주세요.");
                    return;
                }
                string mes = "저장 하시겠습니까?";

                if (CustomMsg.ShowMessage(mes, "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int okqty = int.Parse(lbl_ok_qty.Text);

                    List<PRODUCTION_RESULT> list = new List<PRODUCTION_RESULT>();
                    if (okqty > 0)
                    {
                        PRODUCTION_RESULT entity = new PRODUCTION_RESULT();
                        entity.LOT_NO = textBox1.Text;
                        entity.PRODUCTION_INSTRUCT_ID = Convert.ToInt32(_ProductionInstruct["ID"]);
                        entity.STOCK_MST_ID = Convert.ToInt32(_ProductionInstruct["STOCK_MST_ID"]);
                        entity.STOCK_MST_OUT_CODE = Convert.ToInt32(_ProductionInstruct["STOCK_MST_ID"]);
                        entity.STOCK_MST_STANDARD = Convert.ToInt32(_ProductionInstruct["STOCK_MST_ID"]);
                        entity.STOCK_MST_TYPE = Convert.ToInt32(_ProductionInstruct["STOCK_MST_ID"]);
                        entity.RESULT_TYPE = "CD16001";
                        entity.OK_QTY = okqty;
                        entity.NG_QTY = 0;
                        entity.TOTAL_QTY = okqty;
                        entity.START_DATE = Convert.ToDateTime(_ProductionInstruct["START_INSTRUCT_DATE"]);
                        entity.END_DATE = _ENDdate;
                        entity.COMMENT = textBox2.Text;
                        entity.USE_YN = "Y";
                        entity.REG_USER = _UserEntity.user_account;
                        entity.REG_DATE = DateTime.Now;
                        entity.UP_USER = _UserEntity.user_account;
                        entity.UP_DATE = DateTime.Now;

                        list.Add(entity);
                    }

                    foreach (BAD_ITEM item in _list)
                    {
                        PRODUCTION_RESULT entity = new PRODUCTION_RESULT();
                        entity.LOT_NO = textBox1.Text;
                        entity.PRODUCTION_INSTRUCT_ID = Convert.ToInt32(_ProductionInstruct["ID"]);
                        entity.STOCK_MST_ID = Convert.ToInt32(_ProductionInstruct["STOCK_MST_ID"]);
                        entity.STOCK_MST_OUT_CODE = Convert.ToInt32(_ProductionInstruct["STOCK_MST_ID"]);
                        entity.STOCK_MST_STANDARD = Convert.ToInt32(_ProductionInstruct["STOCK_MST_ID"]);
                        entity.STOCK_MST_TYPE = Convert.ToInt32(_ProductionInstruct["STOCK_MST_ID"]);
                        entity.RESULT_TYPE = item.BAD_TYPE;
                        entity.OK_QTY = 0;
                        entity.NG_QTY = item.BAD_QTY;
                        entity.TOTAL_QTY = item.BAD_QTY;
                        entity.START_DATE = Convert.ToDateTime(_ProductionInstruct["START_INSTRUCT_DATE"]);
                        entity.END_DATE = _ENDdate;
                        entity.COMMENT = textBox2.Text;
                        entity.USE_YN = "Y";
                        entity.REG_USER = _UserEntity.user_account;
                        entity.REG_DATE = DateTime.Now;
                        entity.UP_USER = _UserEntity.user_account;
                        entity.UP_DATE = DateTime.Now;

                        list.Add(entity);
                    }
                    new MS_DBClass(_MY).PRODUCTION_RESULT(list);

                    string mes2 = "청소시간을 등록 하시겠습니까?";

                    if (CustomMsg.ShowMessage(mes2, "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        EQUIPMENT_STOP _STOP = new EQUIPMENT_STOP();

                        _STOP.TYPE = "CD12015";
                        _STOP.EQUIPMENT_ID = "";
                        _STOP.EQUIPMENT_NAME = "";
                        _STOP.PRODUCTION_INSTRUCT_ID = _ProductionInstruct["ID"].ToString();
                        _STOP.START_TIME = _ENDdate.AddMinutes(-5);
                        _STOP.END_TIME = _ENDdate;
                        _STOP.COMMENT = "";
                        _STOP.USE_YN = "Y";
                        _STOP.UP_USER = _UserEntity.user_account;
                        _STOP.UP_DATE = DateTime.Now;
                        _STOP.REG_USER = _UserEntity.user_account;
                        _STOP.REG_DATE = DateTime.Now;

                        MS_DBClass db = new MS_DBClass(_MY);
                        db.EQUIPMENT_STOP_INSERT(_STOP);
                    }
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
            try
            {
                this.Close();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
    }

    public class BAD_ITEM
    {
        public string KEY { get; set; }
        public int BAD_QTY { get; set; }
        public string BAD_TYPE { get; set; }
    }

    public class PRODUCTION_RESULT
    {
        public string LOT_NO { get; set; }
        public int PRODUCTION_INSTRUCT_ID { get; set; }
        public int STOCK_MST_ID { get; set; }
        public int STOCK_MST_OUT_CODE { get; set; }
        public int STOCK_MST_STANDARD { get; set; }
        public int STOCK_MST_TYPE { get; set; }
        public string WORK_TYPE { get; set; }
        public string RESULT_TYPE { get; set; }
        public decimal OK_QTY { get; set; }
        public decimal NG_QTY { get; set; }
        public decimal TOTAL_QTY { get; set; }

        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public string COMMENT { get; set; }
        public string USE_YN { get; set; }
        public string REG_USER { get; set; }
        public DateTime REG_DATE { get; set; }
        public string UP_USER { get; set; }
        public DateTime UP_DATE { get; set; }

    }
    public class PACK_PROD
    {
        public int WORK_PERFORMANCE_ID { get; set; }
        public string MACHINE_NO { get; set; }
        public string ORDER_NO { get; set; }
        public string RESOURCE_NO { get; set; }
        public string LOT_NO { get; set; }
        public int COMPLETE_QTY { get; set; }
        public int QTY { get; set; }
        public DateTime START_TIME { get; set; }
        public DateTime END_TIME { get; set; }
        public string REG_USER { get; set; }
        public DateTime REG_DATE { get; set; }
        public string UP_USER { get; set; }
        public DateTime UP_DATE { get; set; }

    }

}





