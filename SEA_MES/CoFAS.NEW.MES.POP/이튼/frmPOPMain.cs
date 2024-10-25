using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.POP.Function;
using System;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static CoFAS.NEW.MES.POP.Barcode_Class;

namespace CoFAS.NEW.MES.POP
{
    public partial class frmPOPMain : Form
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

        private SystemLogEntity _SystemLogEntity = new SystemLogEntity();

        public DataRow _ProductionInstruct = null;

        public System.Threading.Timer _timer;


        #endregion

        #region ○ 생성자

        public frmPOPMain(UserEntity pUserEntity, SystemLogEntity pSystemLogEntity)
        {
            InitializeComponent();
            _UserEntity = pUserEntity;
            _SystemLogEntity = pSystemLogEntity;

            _MY = utility.My_Settings_Get();

            Load += new EventHandler(Form_Load);
      
            this.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            _uc_Clock1.Font = this.Font;
            dtp_start_work.DateEdit_Font = this.Font;
            dtp_end_work.DateEdit_Font = this.Font;
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;

            dtp_start_work._pDateEdit.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            dtp_start_work._pDateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtp_end_work._pDateEdit.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            dtp_end_work._pDateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                this.Size = new Size(1280, 1024);
                this.FormClosing += Form_Closing;


                btn_SA_Equipment_Check.Text = "설비\n\r안전점검";
                btn_비가동등록.Text = "비가동\n\r등록";
                dtp_start_work.DateTime = DateTime.Now;
                dtp_end_work.DateTime = DateTime.Now;


                DataTable pDataTable1 =  new CoreBusiness().BASE_MENU_SETTING_R10(this.Name,fpPOP,"EQUIPMENT_STOP");

                if (pDataTable1 != null)
                {
                    CoFAS.NEW.MES.Core.Function.Core.initializeSpread(pDataTable1, fpPOP, this.Name, _UserEntity.user_account);

                }

                //fpPOP.ZoomFactor = 1.25f;




                _timer = new System.Threading.Timer(CallBack);

                _timer.Change(0, 30 * 1000);


            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }



        #region ○ Form_Closing

        private void Form_Closing(object pSender, FormClosingEventArgs pFormClosingEventArgs)
        {
            try
            {
                //화면 레이아웃 저장 ?
                _timer.Dispose();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);

            }
        }

        #endregion

        private void Close_Click(object sender, EventArgs e)
        {
            if (CustomMsg.ShowMessage("프로그램을 종료 하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();

                Application.Exit();
            }
        }

        #endregion

        #region ○ 데이터 세팅 
        private void Set_ProductionInstruct()
        {
            try
            {
                fpPOP.Sheets[0].Rows.Count = 0;

                lbl_STOCK_OUT_CODE.Text = "";

                lbl_STOCK_NAME.Text = "-";

                lbl_STOCK_STANDARD.Text = "-";

                lbl_ProductionInstruct_Qty.Text = "0";

                lbl_Now_OK_Qty.Text = "0";

                lbl_Now_NG_Qty.Text = "0";

                lbl_all_Qty.Text = "0";

                lbl_Now_Qty.Text = "0";

                lbl_작업LOT.Text = "";

                lbl_최신수집LOT.Text = "";

                lbl_Instruc_date.Text = "-";

                lbl_Line.Text = "-";

                txt_coment.Text = "";

                if (_ProductionInstruct == null)
                {
                    return;
                }
                else
                {

                    DataTable dt = new MS_DBClass(_MY).instruct_Get(Convert.ToInt32(_ProductionInstruct["ID"]));

                    _ProductionInstruct = dt.Rows[0];

                    lbl_STOCK_OUT_CODE.Text = _ProductionInstruct["STOCK_OUT_CODE"].ToString();
                    lbl_STOCK_NAME.Text = _ProductionInstruct["NAME"].ToString();
                    lbl_STOCK_STANDARD.Text = _ProductionInstruct["STANDARD"].ToString();
                    lbl_ProductionInstruct_Qty.Text = _ProductionInstruct["INSTRUCT_QTY"].ToString();
                    lbl_Now_OK_Qty.Text = _ProductionInstruct["OK_QTY"].ToString();
                    lbl_Now_NG_Qty.Text = _ProductionInstruct["NG_QTY"].ToString();
                    lbl_all_Qty.Text = _ProductionInstruct["TOTAL_QTY"].ToString();
                    lbl_작업LOT.Text = _ProductionInstruct["OUT_CODE"].ToString();
                    lbl_Instruc_date.Text = _ProductionInstruct["INSTRUCT_DATE"].ToString();
                    lbl_Line.Text = _ProductionInstruct["LINE"].ToString();
                    txt_coment.Text = _ProductionInstruct["COMMENT"].ToString();


                    #region ○  작업 시작 종료 데이터 바인딩

                    //작업 시작을 한 경우
                    if (_ProductionInstruct["START_INSTRUCT_DATE"] != DBNull.Value)
                    {
                        if (_ProductionInstruct["END_INSTRUCT_DATE"] == DBNull.Value)
                        {
                            //btn_start_work.ForeColor = Color.White;
                            btn_start_work.BackColor = Color.FromArgb(82, 60, 216);

                            dtp_start_work.DateTime = Convert.ToDateTime(_ProductionInstruct["START_INSTRUCT_DATE"]);
                            //dtp_start_work.Enabled = false;
                            //btn_start_work.Enabled = false;
                            //btn_start_work.ForeColor = Color.FromArgb(82, 60, 216);
                            //btn_start_work.BackColor = Color.FromArgb(82, 60, 216);
                            //btn_start_work.ForeColor = Color.FromArgb(82, 60, 216);

                            btn_start_work.Text = "진행중";

                            btn_end_work.Enabled = true;
                            dtp_end_work.Enabled = true;
                            dtp_start_work.Enabled = false;
                            btn_start_work.Enabled = false;
                        }
                        else

                        {
                            btn_start_work.BackColor = btn_end_work.BackColor;
                            btn_start_work.ForeColor = Color.Black;

                            dtp_start_work.Enabled = false;
                            btn_start_work.Enabled = false;

                            dtp_end_work.Enabled = false;

                            btn_end_work.Enabled = false;


                            dtp_start_work.DateTime = Convert.ToDateTime(_ProductionInstruct["START_INSTRUCT_DATE"]);

                            dtp_end_work.DateTime = Convert.ToDateTime(_ProductionInstruct["END_INSTRUCT_DATE"]);
                        }

                    }
                    //작업 시작을 안한경우
                    else
                    {
                        dtp_start_work.Enabled = true;
                        dtp_start_work.DateTime = DateTime.Now;

                        dtp_end_work.Enabled = true;
                        dtp_end_work.DateTime = DateTime.Now;

                        btn_start_work.Enabled = true;
                        btn_start_work.BackColor = btn_end_work.BackColor;
                        btn_start_work.Text = "시작";
                        btn_end_work.Enabled = false;
                    }

                    #endregion



                    CallBack(null);


                }




            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }


        public void CallBack(Object state)
        {
            try
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    label18.Text = "0";
                    //작업지시 미선택
                    if (_ProductionInstruct == null)
                    {
                        return;
                    }
                    //작업 시작 미실시
                    if (dtp_start_work.Enabled == true)
                    {
               
                        return;
                    }
                    if (dtp_start_work.Enabled == false)
                    {
                        if (dtp_end_work.Enabled == false)
                        {
                            string str = $@"SELECT 
                                             B.code_name  AS 비가동유형
                                             ,A.COMMENT	  AS 이상사항
                                             ,A.COMMENT2  AS 조치사항
                                             ,DATEDIFF(MINUTE, A.START_TIME, ISNULL(A.END_TIME,GETDATE()))  AS STOP_TIME
                                                   FROM EQUIPMENT_STOP A
                                             INNER JOIN CODE_MST B ON A.TYPE = B.code AND A.USE_YN = 'Y'
                                             WHERE A.PRODUCTION_INSTRUCT_ID = { Convert.ToInt32(_ProductionInstruct["ID"])}";

                            DataTable dtt = new CoreBusiness().SELECT(str);
                            CoFAS.NEW.MES.Core.Function.Core.DisplayData_Set(dtt, fpPOP);
                            return;
                        }
                        else
                        {
                            dtp_end_work.DateTime = DateTime.Now;
                        }
                    }

                    if (_ProductionInstruct["LINE_CODE"].ToString() == "CD14002")
                    {
                        DataSet ds = new MS_DBClass(_MY).CallBack(Convert.ToInt32(_ProductionInstruct["ID"]));

                        DataTable db = ds.Tables[0];

                        DataTable db1 = ds.Tables[1];

                        DataTable db2 = ds.Tables[2];
                        if (db1.Rows.Count != 0)
                        {

                            lbl_최신데이터.Text = db2.Rows[0][0].ToString();
                            lbl_최신수집LOT.Text = db1.Rows[0][1].ToString();

                            TimeSpan 시간차이 =  DateTime.Now - Convert.ToDateTime(db2.Rows[0][0]);

                            //label4.Text = 시간차이.TotalMinutes.ToString();

                            if (시간차이.TotalMinutes >= 5)
                            {

                                lbl_비가동여부.BackColor = Color.FromArgb(82, 60, 216);
                            }
                            else
                            {
                                lbl_비가동여부.BackColor = Color.Green;
                            }

                            int 시간 =  Convert.ToInt32(시간차이.TotalMinutes);

                            if (시간 < 60)//초
                            {
                                lbl_비가동여부.Text = 시간.ToString() + "분";
                            }
                            else
                            {
                                시간 = 시간 / 60;

                                if (시간 < 60)//분
                                {
                                    lbl_비가동여부.Text = 시간.ToString() + "시간";
                                }
                                else
                                {
                                    시간 = 시간 / 24;//시
                                    lbl_비가동여부.Text = 시간.ToString() + "일";
                                }
                            }

                            //lbl_OPC.Text = db1.Rows[0][0].ToString();
                        }
                        else
                        {
                            lbl_비가동여부.BackColor = Color.White;
                            lbl_최신데이터.Text = "";
                            lbl_비가동여부.Text = "";
                        }

                        if (db == null)
                        {
                            return;
                        }
                        if (db.Rows.Count == 0)
                        {
                            return;
                        }
                        if (db.Rows[0][0] == DBNull.Value)
                        {
                            return;
                        }

                        double qty = db.Rows.Count;

                        lbl_Now_Qty.Text = qty.ToString();


                        string str = $@"SELECT 
                                             B.code_name  AS 비가동유형
                                             ,A.COMMENT	  AS 이상사항
                                             ,A.COMMENT2  AS 조치사항
                                             ,DATEDIFF(MINUTE, A.START_TIME, ISNULL(A.END_TIME,GETDATE()))  AS STOP_TIME
                                                   FROM EQUIPMENT_STOP A
                                             INNER JOIN CODE_MST B ON A.TYPE = B.code AND A.USE_YN = 'Y'
                                             WHERE A.PRODUCTION_INSTRUCT_ID = { Convert.ToInt32(_ProductionInstruct["ID"])}";

                        DataTable dtt = new CoreBusiness().SELECT(str);

                        CoFAS.NEW.MES.Core.Function.Core.DisplayData_Set(dtt, fpPOP);


                        Fixed_Stop(Convert.ToInt32(_ProductionInstruct["ID"]));

                        string sql = @"SET ANSI_WARNINGS OFF
                                        SET ARITHIGNORE ON
                                        SET ARITHABORT OFF                                          
                                       SELECT 
                                       
                                        ISNULL(SUM(AA.효율생산수량),0)         AS 효율생산수량
                                       FROM
                                       (
                                       SELECT
                                       (((DATEDIFF(SS,B.START_INSTRUCT_DATE,isnull(B.END_INSTRUCT_DATE,GETDATE())))-ISNULL(고정비가동.STOP_TIME,0))/D.CYCLE_TIME) * 
                                       (CASE 
                                        WHEN D.PERFORMANCE = 0
                                        THEN 1
                                        ELSE D.PERFORMANCE END) AS 효율생산수량
                                       from 
                                       (
                                        SELECT  CASE 
                                          WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                          THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
                                          ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END AS 'NOWTIME'
                                       ) A
                                       INNER JOIN [dbo].[PRODUCTION_INSTRUCT] B ON A.NOWTIME = FORMAT(B.INSTRUCT_DATE, 'yyyy-MM-dd') AND B.USE_YN = 'Y'  AND B.START_INSTRUCT_DATE IS NOT NULL
                                       INNER JOIN [dbo].[PRODUCTION_PLAN] C ON B.PRODUCTION_PLAN_ID = C.ID AND C.LINE ='CD14002'
                                       INNER JOIN [dbo].[WORK_CAPA] D ON B.WORK_CAPA_STD_OPERATOR = D.ID
                                       LEFT JOIN 
                                       (
                                       SELECT SUM(STOP_TIME) AS STOP_TIME 
                                       ,PRODUCTION_INSTRUCT_ID
                                       FROM [dbo].[EQUIPMENT_STOP] a
                                       inner join Code_Mst b on a.TYPE = b.code and b.code_description =''
                                       WHERE a.USE_YN = 'Y'
                                       GROUP BY PRODUCTION_INSTRUCT_ID
                                       )비가동 ON  B.ID = 비가동.PRODUCTION_INSTRUCT_ID
                                       LEFT JOIN 
                                       (
                                       SELECT SUM(STOP_TIME) AS STOP_TIME 
                                       ,PRODUCTION_INSTRUCT_ID
                                       FROM [dbo].[EQUIPMENT_STOP] a
                                       inner join Code_Mst b on a.TYPE = b.code and b.code_description ='고정비가동'
                                       WHERE a.USE_YN = 'Y'
                                       GROUP BY PRODUCTION_INSTRUCT_ID
                                       )고정비가동 ON  B.ID = 고정비가동.PRODUCTION_INSTRUCT_ID
                                       ) AA;";

                        DataTable dt1 = new CoreBusiness().SELECT(sql);

                        label18.Text = Convert.ToDecimal(dt1.Rows[0][0]).ToString("F2");
                    }
                    else
                    {
                        lbl_비가동여부.BackColor = Color.White;
                        lbl_최신데이터.Text = "";
                        lbl_비가동여부.Text = "";

                    }


                }));
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }

        #endregion

        private void btn_ProductionInstruct_select_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmInstructSelect popup = new frmInstructSelect(_UserEntity))
                {
                    if (popup.ShowDialog() == DialogResult.OK)
                    {

                        DataTable dt = new MS_DBClass(_MY).instruct_Get(popup._ProductionInstruct_id.Value);

                        _ProductionInstruct = dt.Rows[0];

                        Set_ProductionInstruct();

                    }
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void btn_ManualResultt_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ProductionInstruct == null)
                {
                    CustomMsg.ShowMessage("선택된 작업지시가 없습니다");
                    return;
                }

                using (frmManualResult popup = new frmManualResult(_UserEntity, _ProductionInstruct))
                {
                    if (popup.ShowDialog() == DialogResult.OK)
                    {
                        Set_ProductionInstruct();
                    }
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #region ○ 메모저장 버튼
        private void btn_coment_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ProductionInstruct == null)
                {
                    CustomMsg.ShowMessage("선택된 작업지시가 없습니다");
                    return;
                }

                DataTable dt =new MS_DBClass(_MY).btn_coment_Click(Convert.ToInt32(_ProductionInstruct["ID"]),txt_coment.Text);

                Set_ProductionInstruct();

                CustomMsg.ShowMessage("저장 되었습니다.");
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        #endregion

        #region ○ 작업 시작 버튼
        private void btn_start_work_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ProductionInstruct == null)
                {
                    CustomMsg.ShowMessage("선택된 작업지시가 없습니다");
                    return;
                }

                string sql = $@" SELECT count(id)
                     FROM [dbo].[PRODUCTION_INSTRUCT] A
                   where 1=1
                   AND A.USE_YN = 'Y'
                   AND A.START_INSTRUCT_DATE IS NOT NULL
                   AND A.END_INSTRUCT_DATE IS NULL
                   AND A.ID != {Convert.ToInt32(_ProductionInstruct["ID"])}"; ;



                DataTable dt1 = new CoreBusiness().SELECT(sql);




                if (dt1.Rows[0][0].ToString() != "0")
                {
                    CustomMsg.ShowMessage($"직전 생산 아이템 {dt1.Rows[0][0].ToString()} 건 이 종료 되지 않았습니다.");
                    return;
                }

                //DataTable ck = new MS_DBClass(_MY).START_CHECK(Convert.ToInt32(_ProductionInstruct["ID"]));

                //int ck1 = Convert.ToInt32(ck.Rows[0][0]);
                //int ck2 = Convert.ToInt32(ck.Rows[1][0]);

                //if (ck1 != ck2)
                //{
                //    CustomMsg.ShowMessage("점검 이력이 없습니다. 점검등록후 작업을 시작 할수있습니다.");
                //    return;
                //}


                DataTable dt =new MS_DBClass(_MY).btn_start_work_Click(Convert.ToInt32(_ProductionInstruct["ID"]),dtp_start_work.DateTime.ToString("yyyy-MM-dd HH:mm:ss"));


                Set_ProductionInstruct();

                CustomMsg.ShowMessage("저장되었습니다");

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        #endregion

        #region ○ 작업 종료 버튼
        private void btn_end_work_Click(object sender, EventArgs e)
        {
            try
            {

                if (_ProductionInstruct == null)
                {
                    CustomMsg.ShowMessage("선택된 작업지시가 없습니다");
                    return;
                }

                DataSet ds = new MS_DBClass(_MY).GET_QTY(Convert.ToInt32(_ProductionInstruct["ID"]),dtp_end_work.DateTime);



                DataTable db = ds.Tables[0];
                int text = Convert.ToInt32(db.Rows[0][0]);
                if (text == 0)
                {
                    if (CustomMsg.ShowMessage("작업 수량이 없습니다. 작업종료시간을 입력하시겠습니까?  \n\r 실적 수량은 수동실적을 통해 입력이 되어야합니다.", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        new MS_DBClass(_MY).btn_end_Click(Convert.ToInt32(_ProductionInstruct["ID"]), dtp_end_work.DateTime);
                        Set_ProductionInstruct();
                    }

                    return;
                }

                using (frmEndWork popup = new frmEndWork(text, _ProductionInstruct, _UserEntity, dtp_end_work.DateTime))
                {

                    if (popup.ShowDialog() == DialogResult.OK)
                    {
                        new MS_DBClass(_MY).btn_end_Click(Convert.ToInt32(_ProductionInstruct["ID"]), dtp_end_work.DateTime);
                    }
                }



                Set_ProductionInstruct();




            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region ○ 강제 종료 버튼
        private void btn_end_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ProductionInstruct == null)
                {
                    CustomMsg.ShowMessage("선택된 작업지시가 없습니다");
                    return;
                }
                string mes = "강제 종료 하시겠습니까?\n\r실적은 저장 되지 않습니다.";

                if (CustomMsg.ShowMessage(mes, "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    new MS_DBClass(_MY).btn_end_Click(Convert.ToInt32(_ProductionInstruct["ID"]));

                    Set_ProductionInstruct();

                    CustomMsg.ShowMessage("저장되었습니다");
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #endregion

        private void btn_Equipment_Check_Click(object sender, EventArgs e)
        {
            try
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    string name = (sender as Button).Text;
                    using (frmEquipmentCheck popup = new frmEquipmentCheck(_UserEntity, "SD18003", name))
                    {
                        if (popup.ShowDialog() == DialogResult.OK)
                        {
                            Set_ProductionInstruct();
                        }
                    }
                }));

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void btn_SA_Equipment_Check_Click(object sender, EventArgs e)
        {
            try
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    string name = (sender as Button).Text;
                    using (frmEquipmentCheck popup = new frmEquipmentCheck(_UserEntity, "SD18002", name))
                    {
                        if (popup.ShowDialog() == DialogResult.OK)
                        {
                            Set_ProductionInstruct();
                        }
                    }
                }));
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }

        private void btn_PR_Equipment_Check_Click(object sender, EventArgs e)
        {
            try
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    string name = (sender as Button).Text;
                    using (frmEquipmentCheck popup = new frmEquipmentCheck(_UserEntity, "SD18001", name))
                    {
                        if (popup.ShowDialog() == DialogResult.OK)
                        {
                            Set_ProductionInstruct();
                        }
                    }
                }));
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    using (frmProductionProgressStatus popup = new frmProductionProgressStatus(_UserEntity))
                    {
                        if (popup.ShowDialog() == DialogResult.OK)
                        {
                            Set_ProductionInstruct();
                        }
                    }
                }));
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }

    

        private void Fixed_Stop(int id)
        {
            try
            {
                DataTable dt =new MS_DBClass(_MY).Fixed_Stop(id);

                if (dt == null)
                {
                    return;
                }
                foreach (DataRow item in dt.Rows)
                {
                    if (item["code_etc1"] != DBNull.Value)
                    {
                        if (item["TYPE"] == DBNull.Value)
                        {
                            EQUIPMENT_STOP _STOP = new EQUIPMENT_STOP();


                            _STOP.EQUIPMENT_ID = "0";
                            _STOP.EQUIPMENT_NAME = "0";
                            _STOP.TYPE = item["code"].ToString();
                            _STOP.PRODUCTION_INSTRUCT_ID = item["ID"].ToString();
                            _STOP.START_TIME = Convert.ToDateTime(item["code_etc1"]);
                            _STOP.END_TIME = Convert.ToDateTime(item["code_etc1"]).AddMinutes(Convert.ToInt32(item["code_etc2"]));
                            _STOP.COMMENT = "고정비가동";
                            _STOP.USE_YN = "Y";
                            _STOP.UP_USER = _UserEntity.user_account;
                            _STOP.UP_DATE = DateTime.Now;
                            _STOP.REG_USER = _UserEntity.user_account;
                            _STOP.REG_DATE = DateTime.Now;

                            MS_DBClass db = new MS_DBClass(_MY);
                            db.EQUIPMENT_STOP_INSERT(_STOP);
                        }
                    }
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }




        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    if (_ProductionInstruct == null)
                    {
                        CustomMsg.ShowMessage("선택된 작업지시가 없습니다");
                        return;
                    }
                    using (frmEQUIPMENT_STOP popup = new frmEQUIPMENT_STOP(_UserEntity, _ProductionInstruct))
                    {
                        if (popup.ShowDialog() == DialogResult.OK)
                        {
                            Set_ProductionInstruct();
                        }
                    }
                }));
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
                using (PM_LIST popup = new PM_LIST(_UserEntity.user_account))
                {
                    popup.ShowDialog();
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }



        private void btn_start_work_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                // 버튼 기본 배경 그리기
                SolidBrush backgroundBrush = new SolidBrush(btn.BackColor);
                e.Graphics.FillRectangle(backgroundBrush, btn.ClientRectangle);
                backgroundBrush.Dispose();

                if (btn.Enabled != false && btn.BackColor == Color.FromArgb(82, 60, 216))
                {
                    TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, btn.ClientRectangle, Color.Black); // 원하는 색상으로 변경
                }

                else
                {
                    // 버튼 텍스트 그리기
                    TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, btn.ClientRectangle, Color.White); // 원하는 색상으로 변경
                }
            }
        }


    }
}





