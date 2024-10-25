using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
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
    public partial class 작업종료 : Form
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

        public UserEntity _UserEntity = new UserEntity();

        public string _ID = string.Empty;

        public DateTime _ENDdate = new DateTime();

        public List<BAD_ITEM> _list = new List<BAD_ITEM>();
        #endregion

        #region ○ 생성자

     
        public 작업종료(string _pID, DateTime _pEND_DateTime, UserEntity _pUserEntity)
        {
            InitializeComponent();
            _ID = _pID;
            _ENDdate = _pEND_DateTime;
            _UserEntity = _pUserEntity;
            Load += new EventHandler(Form_Load);

            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1280, 1024);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1280, 1024);
            this.Font = new Font("맑은 고딕", 18, FontStyle.Bold);
        }
        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                _불량구분.AddValue(new CoreBusiness().Spread_ComboBox("불량구분", "", ""), 0, 0, "", true);
                _불량구분.Font = 정상수량.Font;

                btn_0.Click += NUMBER_Click;
                btn_1.Click += NUMBER_Click;
                btn_2.Click += NUMBER_Click;
                btn_3.Click += NUMBER_Click;
                btn_4.Click += NUMBER_Click;
                btn_5.Click += NUMBER_Click;
                btn_6.Click += NUMBER_Click;
                btn_7.Click += NUMBER_Click;
                btn_8.Click += NUMBER_Click;
                btn_9.Click += NUMBER_Click;
                btn_C.Click += btn_C_Click;
                btn_del.Click += btn_del_Click;

                string str = $@"SELECT A.*
                                      ,B.OUT_CODE	 AS '제품코드'
                                      ,B.NAME      AS '제품명'
                                      ,생산수량     
                                      FROM [dbo].[PRODUCTION_RESULT] A
                                      INNER JOIN [dbo].[STOCK_MST] B ON A.STOCK_MST_ID = B.ID
                                       LEFT JOIN
                                                 (
                                                  select COUNT(UNIQ_NO) AS 생산수량,A.ID
                                                  from [dbo].[PRODUCTION_RESULT] A
                                                  INNER JOIN EQUIPMENT B ON A.EQUIPMENT_ID = B.ID
                                                  left join [dbo].[CCS_INJ_SPC_DATA] C ON C.SPC_DATETIME Between  A.START_DATE AND '{_ENDdate.ToString("yyyy-MM-dd HH:mm:ss")}'
                                                  where 1=1
                                                  AND A.ID = {_ID}
                                                  GROUP BY  A.ID
                                                 ) C ON A.ID = C.ID
                                             where 1=1

                                      AND A.ID = {_ID}";


                DataTable _DataTable = new CoreBusiness().SELECT(str);

                foreach (Control item in tableLayoutPanel5.Controls)
                {
                    if(item.GetType() == typeof(Label))
                    {
                        Label label = item as Label;

                        foreach (DataColumn column in _DataTable.Columns)
                        {
                            if(label.Name == column.ColumnName)
                            {
                                label.Text = _DataTable.Rows[0][column.ColumnName].ToString();
                            }
                        } 
                    }
                    else if (item.GetType() == typeof(TextBox))
                    {
                        TextBox textbox = item as TextBox;

                        foreach (DataColumn column in _DataTable.Columns)
                        {
                            if (textbox.Name == column.ColumnName)
                            {
                                textbox.Text = _DataTable.Rows[0][column.ColumnName].ToString();
                            }
                        }
                    }
                }

                정상수량.Text = 생산수량.Text;

           
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
        private void NUMBER_Click(object sender, EventArgs e)
        {
            try
            {
                if(생산수량.Text == "0")
                {
                    생산수량.Text = "";
                }
                if (생산수량.Text.Length == 10)
                {
                    return;
                }

                Button btn = sender as Button;

                생산수량.Text += btn.Text;
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
                if (생산수량.Text.Length != 0)
                {
                    생산수량.Text = 생산수량.Text.Substring(0, 생산수량.Text.Length - 1);
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
                생산수량.Text = "";
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void btn_종료_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

       
        private void Setting_Result()
        {
            try
            {
                panel1.Controls.Clear();

                DataTable  dt = new CoreBusiness().Spread_ComboBox("불량구분", "", "");

                for (int i = 0; i < _list.Count; i++)
                {
                    BAD_ITEM entity = _list[i];

                    int loc = 100 * i;

                    Button btn = new Button();

                    btn.Name = entity.KEY;           

                    btn.Text += "실적유형 : " + dt.Select($"CD ='{entity.BAD_TYPE}'")[0]["CD_NM"].ToString() + "\n\r";

                    btn.Text += "수량 : " + entity.BAD_QTY;

                    btn.Font = this.Font;

                    btn.Location = new Point(5, 10 + loc);

                    btn.Size = new Size((panel1.Size.Width - 40), 100);

                    btn.Click += ProductionResult_Click;

                    btn.TextAlign = ContentAlignment.MiddleLeft;

                    panel1.Controls.Add(btn);
                }

                if(_list.Count ==0 )
                {
                    btn_0.Enabled = true;
                    btn_1.Enabled = true;
                    btn_2.Enabled = true;
                    btn_3.Enabled = true;
                    btn_4.Enabled = true;
                    btn_5.Enabled = true;
                    btn_6.Enabled = true;
                    btn_7.Enabled = true;
                    btn_8.Enabled = true;
                    btn_9.Enabled = true;
                    btn_C.Enabled = true;
                    btn_del.Enabled = true;
                }
                else
                {
                    btn_0.Enabled = false;
                    btn_1.Enabled = false;
                    btn_2.Enabled = false;
                    btn_3.Enabled = false;
                    btn_4.Enabled = false;
                    btn_5.Enabled = false;
                    btn_6.Enabled = false;
                    btn_7.Enabled = false;
                    btn_8.Enabled = false;
                    btn_9.Enabled = false;
                    btn_C.Enabled = false;
                    btn_del.Enabled = false;
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

                    정상수량.Text = (int.Parse(정상수량.Text) + entity.BAD_QTY).ToString();

                    _list.Remove(entity);

                    Setting_Result();

                }

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void btn_추가_Click(object sender, EventArgs e)
        {
            try
            {
                string 불량유형 = _불량구분.GetValue();
                if (불량유형 == "")
                {
                    CustomMsg.ShowMessage("불량 유형을 입력해주세요");
                    return;
                }
                else
                {
                    using (from_키패드 popup = new from_키패드())
                    {
                        if (popup.ShowDialog() == DialogResult.OK)
                        {
                            int badQty = int.Parse(   popup._code);

                            if (badQty <= 0)
                            {
                                CustomMsg.ShowMessage("불량 수량을 입력해주세요");
                                return;
                            }
                            int okqty = int.Parse(정상수량.Text);

                            if (okqty < badQty)
                            {
                                CustomMsg.ShowMessage("불량 수량은 생산수량보다 많을수 없습니다.");                     
                                return;
                            }
                            else
                            {
                                정상수량.Text = (okqty - badQty).ToString();

                                BAD_ITEM itme = new BAD_ITEM();
                                itme.KEY = DateTime.Now.ToString();
                                itme.BAD_QTY = badQty;
                                itme.BAD_TYPE = 불량유형;

                                _list.Add(itme);

                                Setting_Result();
                            }
                        }
                    }
                }

            

              
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void 생산수량_TextChanged(object sender, EventArgs e)
        {
            정상수량.Text = 생산수량.Text;
        }

        private void btn_저장_Click(object sender, EventArgs e)
        {
            try
            {
                //if (LOT_NO.Text.Length == 0)
                //{
                //    CustomMsg.ShowMessage("제품 Lot 정보을 입력해 주세요.",Color.Black);
                //    return;
                //}
                string mes = "저장 하시겠습니까?";

                if (CustomMsg.ShowMessage(mes, "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int okqty = int.Parse(정상수량.Text);

                    if (okqty > 0)
                    {
                        CustomMsg.ShowMessage("정상수량이 0개 입니다.");
                        return;

                    }
                    string str1 = $@"UPDATE [dbo].[PRODUCTION_RESULT]
                                           SET [LOT_NO]     = '{LOT_NO.Text}'
                                              ,[SUB_LOT_NO] = '{SUB_LOT_NO.Text}'
                                              ,[MFR]        = '{MFR.Text}'
                                              ,[QTY]        = {okqty}              
                                              ,[END_DATE]   = '{_ENDdate.ToString("yyyy-MM-dd HHH:mm:ss")}'
                                              ,[COMMENT]    = '{비고.Text}'
                                              ,[UP_USER]    = '{_UserEntity.user_account}'
                                              ,[UP_DATE]    = GETDATE()
                                               where 1=1
                                               AND ID = {_ID}";


                    new CoreBusiness().SELECT(str1);


                    foreach (BAD_ITEM item in _list)
                    {
                        string str = $@"INSERT INTO [dbo].[PRODUCTION_RESULT]
                                                   ([LOT_NO]
                                                   ,[SUB_LOT_NO]
                                                   ,[MFR]
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
                                              SELECT [LOT_NO]
                                                    ,[SUB_LOT_NO]
                                                    ,[MFR]
                                                    ,[WORKER_NAME]
                                                    ,[PRODUCTION_INSTRUCT_ID]
                                                    ,[STOCK_MST_ID]
                                                    ,[EQUIPMENT_ID]
                                                    ,'{item.BAD_TYPE}'
                                                    ,{item.BAD_QTY}
                                                    ,[START_DATE]
                                                    ,[END_DATE]
                                                    ,[COMMENT]
                                                    ,[COMPLETE_YN]
                                                    ,[USE_YN]
                                                    ,[REG_USER]
                                                    ,[REG_DATE]
                                                    ,[UP_USER]
                                                    ,[UP_DATE]
                                                FROM PRODUCTION_RESULT
                                               where 1=1
                                               AND ID = {_ID}";


                       new CoreBusiness().SELECT(str);
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
    }



}





