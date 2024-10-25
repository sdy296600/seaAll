﻿using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.POP.Function;
using FarPoint.Win.Spread;
using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static CoFAS.NEW.MES.POP.Barcode_Class;

namespace CoFAS.NEW.MES.POP
{
    public partial class 세아 : Form
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
        public string _p품번 = string.Empty;

        public string _p호기 = string.Empty;

        public string _pLOT  = string.Empty;

        public string _p실적 = string.Empty;

        Barcode_Class _로드셀 = null;


        #endregion

        #region ○ 생성자

        public 세아(UserEntity pUserEntity, SystemLogEntity pSystemLogEntity)
        {
            InitializeComponent();
            _UserEntity = pUserEntity;
            _SystemLogEntity = pSystemLogEntity;

            _MY = utility.My_Settings_Get();

            Load += new EventHandler(Form_Load);

            this.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE - 5, FontStyle.Bold);

            this.Name = "세아POP메인";


        }

        #endregion

        public void Set_Spread_Date(xFpSpread xFpSpread, DataTable dt)
        {
            xFpSpread.Sheets[0].Rows.Count = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                xFpSpread.Sheets[0].Visible = false;
                xFpSpread.Sheets[0].Rows.Count = dt.Rows.Count;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    foreach (DataColumn item in dt.Columns)
                    {
                        xFpSpread.Sheets[0].SetValue(i, item.ColumnName, dt.Rows[i][item.ColumnName]);


                    }


                }
                //Core.Function.Core._AddItemSUM(fpMain);
                xFpSpread.Sheets[0].Visible = true;


            }
        }

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {

                // MessageBox.Show(this.Name);
                //base_ComboBox1._SearchCom.AddValue(new CoreBusiness().Spread_ComboBox("세아_작업장", "", ""), 0, 0, "", true);

                DataTable pDataTable1 =  new CoreBusiness().BASE_MENU_SETTING_R10(this.Name,fpMain,"");
                DataTable pDataTable2 =  new CoreBusiness().BASE_MENU_SETTING_R10(this.Name,fpSub,"WORK_PERFORMANCE");
                DataTable pDataTable3 =  new CoreBusiness().BASE_MENU_SETTING_R10(this.Name,fpSub2,"BAD_PERFORMANCE");
                DataTable pDataTable4 =  new CoreBusiness().BASE_MENU_SETTING_R10(this.Name,fpSub3,"IN_BARCODE");
                if (pDataTable1 != null && pDataTable1.Rows.Count != 0)
                {
                    CoFAS.NEW.MES.Core.Function.Core.initializeSpread(pDataTable1, fpMain, this.Name, _UserEntity.user_account);
                    CoFAS.NEW.MES.Core.Function.Core.InitializeControl(pDataTable1, fpMain, this, _PAN_WHERE ,new MenuSettingEntity() { BASE_TABLE =""});
                }
                if (pDataTable2 != null && pDataTable2.Rows.Count != 0)
                {
                    CoFAS.NEW.MES.Core.Function.Core.initializeSpread(pDataTable2, fpSub, this.Name, _UserEntity.user_account);
                }
                if (pDataTable3 != null && pDataTable3.Rows.Count != 0)
                {
                    CoFAS.NEW.MES.Core.Function.Core.initializeSpread(pDataTable3, fpSub2, this.Name, _UserEntity.user_account);
                }
                if (pDataTable4 != null && pDataTable4.Rows.Count != 0)
                {
                    CoFAS.NEW.MES.Core.Function.Core.initializeSpread(pDataTable4, fpSub3, this.Name, _UserEntity.user_account);
                }

                this.FormClosing += Form_Closing;


                _작업일자._pDateEdit.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE - 5, FontStyle.Bold);
                _시작._pDateEdit.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE - 5, FontStyle.Bold);
                _종료._pDateEdit.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE - 5, FontStyle.Bold);
                _미포장수량.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE - 5, FontStyle.Bold);
                txt_작업인원.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE - 5, FontStyle.Bold);

                _작업일자._pDateEdit.BackColor = Color.Yellow;
                _시작._pDateEdit.BackColor = Color.Yellow;
                _종료._pDateEdit.BackColor = Color.Yellow;
                txt_작업인원.BackColor = Color.Yellow;

                _작업일자._pDateEdit.Properties.Mask.EditMask = "yyyy-MM-dd";
                _작업일자._pDateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
                _시작._pDateEdit.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
                _시작._pDateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
                _종료._pDateEdit.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
                _종료._pDateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;

                this.fpMain.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpMain_CellClick);

                _로드셀_Open();


                fpMain._ChangeEventHandler += FpMain_Change;

                foreach (Control item in _PAN_WHERE.Controls)
                {
                    if (item.GetType().ToString() == "CoFAS.NEW.MES.Core.Base_ComboBox")
                    {
                        if (item.Name == "C.WORKCENTER")
                        {
                            CoFAS.NEW.MES.Core.Base_ComboBox ComboBox = item as CoFAS.NEW.MES.Core.Base_ComboBox;

                            ComboBox._SearchCom.ValueChanged += _ComboBox_EditValueChanged;
                        }
                    }
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void _ComboBox_EditValueChanged(object pSender, EventArgs pEventArgs)
        {
            try
            {
                조회.PerformClick();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void FpMain_Change(object sender, ChangeEventArgs e)
        {
            try
            {
                xFpSpread xFp = sender as xFpSpread;

                if (xFp.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(FarPoint.Win.Spread.CellType.CheckBoxCellType))
                {
                    new MS_DBClass(utility.My_Settings_Get()).USP_WORK_RECYCLE_POP_A10(
                     fpMain.Sheets[0].GetValue(e.Row, " RESOURCE_NO        ".Trim()).ToString()
                    , fpMain.Sheets[0].GetValue(e.Row, " LOT                ".Trim()).ToString()
                    , Convert.ToDateTime(fpMain.Sheets[0].GetValue(e.Row, " ORDER_DATE         ".Trim())).ToString("yyyy-MM-dd HH:mm:ss")
                    , fpMain.Sheets[0].GetValue(e.Row, " RE_SCRAP_STATUS    ".Trim()).ToString()
                    , fpMain.Sheets[0].GetValue(e.Row, " RE_GROSS_STATUS    ".Trim()).ToString()
                    , fpMain.Sheets[0].GetValue(e.Row, " RE_GATE_STATUS     ".Trim()).ToString()
                    , _UserEntity.user_account
                    , _UserEntity.user_account
                    );
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void _로드셀_Open()
        {

            string sql = $@"SELECT COM
                            FROM [dbo].[SERIAL_SETTING]
                            WHERE WINDOW_CODE = '로드셀'";

            DataTable dt =  new CoreBusiness().SELECT(sql);

            _로드셀 = new Barcode_Class(dt.Rows[0][0].ToString());
            if (_로드셀 != null)
            {
                if (_로드셀._port.IsOpen) //연결
                {
                    _로드셀.Readed += _로드셀_BarCode;
                }
            }
        }

        private void _로드셀_BarCode(object sender, ReadEventArgs e)
        {
            try
            {
                BeginInvoke(new MethodInvoker(delegate ()
                {
                    if (_p품번 != string.Empty &&
                                 _pLOT != string.Empty &&
                                 _p실적 != string.Empty)
                    {
                        string msg = e.ReadMsg.Trim();

                        string [] msgs = msg.Split(' ');

                        decimal wt = 0;
                        decimal ck = 0;
                        foreach (string item in msgs)
                        {
                            if (decimal.TryParse(item, out ck))
                            {
                                wt = decimal.Parse(item);
                            }
                        }
                        if (_로드셀중량.Text == "0")
                        {
                            _로드셀중량.Text = wt.ToString();
                            //foreach (string item in msgs)
                            //{
                            //    _로드셀중량.Text = wt.ToString();
                            //}
                            //msgs = msgs[3].Split(' ');
                        }
                        else
                        {
                            decimal 이전중량 = Convert.ToDecimal(_로드셀중량.Text);
                            decimal 최신중량 =  wt;

                            _로드셀중량.Text = 최신중량.ToString();

                            string sql = $@"INSERT INTO [dbo].[IN_BARCODE]
                                            (
                                             [WORK_PERFORMANCE_ID]
                                            ,[TYPE]
                                            ,[DATE]
                                            ,[RESOURCE_NO]
                                            ,[LOT_NO]
                                            ,[BARCODE_NO]
                                            ,[WEIGHT]
                                            ,[REG_DATE])
                                      VALUES
                                            (
                                            {_p실적}
                                            ,'실투입'
                                            ,GETDATE()
                                            ,'{_p품번}'
                                            ,'{_pLOT}'
                                            ,''
                                            ,'{이전중량-최신중량}'
                                            ,GETDATE());
                                        select *
                                        from [dbo].[IN_BARCODE]
                                        where 1=1
                                        and RESOURCE_NO = '{_p품번}'
                                        and LOT_NO      = '{_pLOT}'
                                        and TYPE        = '실투입'";

                            DataTable dt =  new CoreBusiness().SELECT(sql);

                            Set_Spread_Date(fpSub3, dt);

                            for (int i = 0; i < fpSub.Sheets[0].RowCount; i++)
                            {
                                if (fpSub.Sheets[0].GetValue(i, "ID           ".Trim()).ToString().Trim() == _p실적)
                                {

                                    fpSub_CellClick(fpMain, new CellClickEventArgs(null, i, 0, 0, 0, System.Windows.Forms.MouseButtons.Left, false, false));
                                }
                            }
                        }

                        //}
                        //}
                    }
                }));
            }
            catch (Exception err)
            {

            }
        }


        public virtual void fpMain_CellClick(object sender, CellClickEventArgs e)
        {
            try
            {
                _p품번 = fpMain.Sheets[0].GetValue(e.Row, "RESOURCE_NO").ToString().Trim();
                _pLOT = fpMain.Sheets[0].GetValue(e.Row, "LOT").ToString().Trim();
                _p호기 = fpMain.Sheets[0].GetValue(e.Row, "WORKCENTER").ToString().Trim();
                run(_p품번, _pLOT, _p호기);
                fpMain.ActiveSheet.SetActiveCell(e.Row, e.Column);
                fpMain.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Center, FarPoint.Win.Spread.HorizontalPosition.Center);
                fpMain._SelectionChangedEvent(fpMain, null);
            }
            catch (Exception _Exception)
            {
                //CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        public void run(string _p품번, string _pLOT, string _p호기) 
        {

            _p실적 = string.Empty;

            string sql = $@"SELECT *
                                 FROM[dbo].[WORK_PERFORMANCE]
                                 where 1 = 1
                                 AND LOT_NO = '{_pLOT}'
                                 AND RESOURCE_NO ='{_p품번}'";

            DataTable pDataTable = new CoreBusiness().SELECT(sql);

            fpSub.Sheets[0].Rows.Count = 0;

            if (pDataTable != null && pDataTable.Rows.Count > 0)
            {
                fpSub.Sheets[0].Visible = false;
                fpSub.Sheets[0].Rows.Count = pDataTable.Rows.Count;

                for (int i = 0; i < pDataTable.Rows.Count; i++)
                {
                    foreach (DataColumn item in pDataTable.Columns)
                    {
                        if (item.ColumnName == "WORK_TIME")
                        {

                            Double totalSeconds = Convert.ToDouble(pDataTable.Rows[i][item.ColumnName].ToString());
                            TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);

                            // TimeSpan을 사용하여 시간, 분, 초 형식으로 설정
                            fpSub.Sheets[0].SetValue(i, item.ColumnName, $"{timeSpan.Hours}시간 {timeSpan.Minutes}분 {timeSpan.Seconds}초");
                        }
                        else
                        {
                            fpSub.Sheets[0].SetValue(i, item.ColumnName, pDataTable.Rows[i][item.ColumnName]);
                        }
                    }
                }
                //Core.Function.Core._AddItemSUM(fpSub);
                fpSub.Sheets[0].Visible = true;


            }

            _품번.Text = "-";
            _품목명.Text = "-";

            _LOT.Text = "-";
            _로드셀중량.Text = "0";

            _작업일자.DateTime = DateTime.Now;
            _지시수량.Text = "-";

            _시작.DateTime = DateTime.Now;
            _종료.DateTime = DateTime.Now;

            _포장수량.Text = "0";
            _미포장수량.Text = "0";
            _총미포장.Text = "0";

            txt_작업인원.Text = "0";

            _간판발행수.Text = "-";
            _lbl_간판발행.Text = "-";
            _교대조.Text = "-";

            _작업코드.Text = "-";
            _금형.Text = "-"; ;

            _상태.Text = "-";

            _lbl_총생산량.Text = "0";

            _양품사용중량.Text = "0";
            _CAV.Text = "0";
            _lbl_양품.Text = "0";
            _lbl_예열타.Text = "0";
            _lbl_불량.Text = "0";

            _포장수량.Text = "0";
            _간판발행수.Text = "0";
            _lbl_간판발행.Text = "0";
            _용탕투입중량.Text = "0";
            _미포장수량.Text = "0";

            //그리드 합계 행 추가   
            //Core.Function.Core._AddItemSUM(fpMain);

           
        }
        public virtual void fpSub_CellClick(object sender, CellClickEventArgs e)
        {
            try
            {
                _p실적 = fpSub.Sheets[0].GetValue(e.Row, "ID    ".Trim()).ToString();
                set_Data();

                int row = 0;
                for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                {
                    if (fpMain.Sheets[0].GetValue(i, "RESOURCE_NO   ".Trim()).ToString().Trim() == _p품번 &&
                        fpMain.Sheets[0].GetValue(i, "LOT           ".Trim()).ToString().Trim() == _pLOT)
                    {
                        row = i;
                    }
                }

                //그리드 합계 행 추가   
                Core.Function.Core._AddItemSUM(fpSub);

                _lbl_총생산량.Text = "0";
                _lbl_양품.Text = "0";
                _lbl_예열타.Text = "0";
                _lbl_불량.Text = "0";
                _미포장수량.Text = "0";
                _총미포장.Text = "0";

                _품번.Text = fpMain.Sheets[0].GetValue(row, "RESOURCE_NO ".Trim()).ToString().Trim();
                _품목명.Text = fpMain.Sheets[0].GetValue(row, "DESCRIPTION ".Trim()).ToString().Trim();
                _금형.Text = fpMain.Sheets[0].GetValue(row, "CODE_MD ".Trim()).ToString().Trim();
                _CAV.Text = fpMain.Sheets[0].GetValue(row, "CAV     ".Trim()).ToString().Trim();
                _LOT.Text = fpMain.Sheets[0].GetValue(row, "LOT         ".Trim()).ToString().Trim();
                _작업일자.Text = fpMain.Sheets[0].GetValue(row, "ORDER_DATE   ".Trim()).ToString();
                _지시수량.Text = Convert.ToInt32(fpMain.Sheets[0].GetValue(row, "ORDER_QTY    ".Trim())).ToString("N0");
                _로드셀중량.Text = "0";

                string sql = $@"SELECT [ID]
                           ,[ORDER_NO]
                           ,[RESOURCE_NO]
                           ,[LOT_NO]
                           ,[ORDER_DATE]
                           ,[SHIFT]
                           ,[WORK_CODE]
                           ,[QTY_COMPLETE]
                           ,[WORK_TIME]
                           ,[START_TIME]
                           ,ISNULL([END_TIME],GETDATE()) AS END_TIME
                           ,[REG_USER]
                           ,[REG_DATE]
                           ,[UP_USER]
                           ,[UP_DATE]
                           ,[IN_PER]
                       from [dbo].[WORK_PERFORMANCE]
                       where 1=1
                       and RESOURCE_NO = '{_p품번}'
                       and LOT_NO = '{_pLOT}'
  　　　　　　　　　　 and ID = '{_p실적}'";

                DataTable pDataTable4 = new CoreBusiness().SELECT(sql);
                sql = $@"SELECT(
                            SELECT ISNULL(SUM(P_QTY),0)  AS 포장수량
                                FROM [dbo].[PRODUCT_BARCODE] 
                                WHERE 1=1 AND RESOURCE_NO = '{_p품번}'
                                AND LOT_NO = '{_pLOT}')";

                DataTable pDataTable10 = new CoreBusiness().SELECT(sql);
                if (pDataTable4.Rows.Count != 0)
                {

                    string sql1 = $@"SELECT 
                                     ifnull(WORK_WARMUPCNT,0) as WORK_WARMUPCNT
                                    ,ifnull(WORK_ERRCOUNT,0)  as WORK_ERRCOUNT
                                    ,ifnull(WORK_OKCNT,0)     as WORK_OKCNT
                                    ,ifnull((WORK_ERRCOUNT+WORK_OKCNT),0) AS all_Qty
                               FROM work_performance
                              where RESOURCE_NO = '{_p품번}' AND LOT_NO ='{_pLOT}' AND WORK_PERFORMANCE_ID = '{_p실적}'";

                    DataTable pDataTable5 = new MY_DBClass().SELECT_DataTable(sql1);

                    sql1 = $@"SELECT 
                                    SUM(ifnull(WORK_OKCNT,0))     as WORK_OKCNT
                               FROM work_performance
                              where RESOURCE_NO = '{_p품번}' AND LOT_NO ='{_pLOT}'";

                    DataTable pDataTable11 = new MY_DBClass().SELECT_DataTable(sql1);
                    if (pDataTable5.Rows.Count != 0)
                    {
                        _lbl_총생산량.Text = pDataTable5.Rows[0]["all_Qty"].ToString();
                        _lbl_양품.Text = pDataTable5.Rows[0]["WORK_OKCNT"].ToString();
                        _lbl_예열타.Text = pDataTable5.Rows[0]["WORK_WARMUPCNT"].ToString();
                        _lbl_불량.Text = pDataTable5.Rows[0]["WORK_ERRCOUNT"].ToString();
                        _미포장수량.Text = _lbl_양품.Text;
                        if (pDataTable10.Rows.Count != 0 && pDataTable11.Rows.Count != 0)
                        {
                            double data1 = 0;
                            double data2 = 0;
                            if (!double.TryParse(pDataTable11.Rows[0]["WORK_OKCNT"].ToString(), out data1))
                            {
                                data1 = 0;
                            };
                            if (!double.TryParse(pDataTable10.Rows[0]["COLUMN1"].ToString(), out data2))
                            {
                                data2 = 0;
                            };
                            double 미포장수량 = data1 - data2;
                            _총미포장.Text = 미포장수량.ToString();

                        }
                    }
                    else
                    {
                        _lbl_총생산량.Text = "0";
                        _lbl_양품.Text = "0";
                        _lbl_예열타.Text = "0";
                        _lbl_불량.Text = "0";
                        _미포장수량.Text = "0";
                    }
                }



                _교대조.Text = "주간";

                _작업코드.Text = "정상작업";

                txt_작업인원.Text = pDataTable4.Rows[0]["IN_PER"].ToString();

                _상태.Text = fpMain.Sheets[0].GetValue(row, "DEAMND_STATUS    ".Trim()).ToString();

                _시작.DateTime = Convert.ToDateTime(fpSub.Sheets[0].GetValue(e.Row, "START_TIME    ".Trim()));

                if (fpSub.Sheets[0].GetValue(e.Row, "END_TIME    ".Trim()).ToString() != "")
                {
                    _종료.DateTime = Convert.ToDateTime(fpSub.Sheets[0].GetValue(e.Row, "END_TIME    ".Trim()));
                }
                else
                {
                    _종료.DateTime = DateTime.Now;
                }

                if (_시작.DateTime.Hour >= 8 && _시작.DateTime.Hour < 20)
                {
                    _교대조.Text = "주간";
                }
                else
                {
                    _교대조.Text = "야간";
                }

                sql = $@"SELECT(SELECT ISNULL(SUM(P_QTY),0)  AS 포장수량     FROM [dbo].[PRODUCT_BARCODE] WHERE 1=1 AND RESOURCE_NO = '{_p품번}'AND LOT_NO = '{_pLOT}' AND WORK_PERFORMANCE_ID = '{_p실적}')   AS 포장수량
                              ,(SELECT ISNULL(COUNT(1),0)    AS 간판발행수   FROM [dbo].[PRODUCT_BARCODE] WHERE 1=1 AND RESOURCE_NO = '{_p품번}'AND LOT_NO = '{_pLOT}' AND WORK_PERFORMANCE_ID = '{_p실적}')   AS 간판발행수";



                DataTable pDataTable6 = new CoreBusiness().SELECT(sql);

                if (pDataTable6.Rows.Count != 0)
                {

                    _포장수량.Text = pDataTable6.Rows[0]["포장수량       ".Trim()].ToString();
                    _간판발행수.Text = pDataTable6.Rows[0]["간판발행수    ".Trim()].ToString();
                    _lbl_간판발행.Text = pDataTable6.Rows[0]["간판발행수    ".Trim()].ToString();
                    _미포장수량.Text = (Convert.ToInt32(_lbl_양품.Text) - Convert.ToInt32(_포장수량.Text)).ToString();

                    sql = $@"SELECT ISNULL(SUM(WEIGHT),0) AS 중량,TYPE
                               FROM [dbo].[IN_BARCODE]      
                              WHERE 1=1 
                               AND RESOURCE_NO = '{_p품번}'
                               AND LOT_NO = '{_pLOT}' 
                               AND WORK_PERFORMANCE_ID = '{_p실적}'
                               GROUP BY TYPE";

                    DataTable pDataTable7 = new CoreBusiness().SELECT(sql);

                    decimal 용탑투입 = 0;

                    decimal 로드셀중량 = 0;

                    for (int i = 0; i < pDataTable7.Rows.Count; i++)
                    {
                        switch (pDataTable7.Rows[i]["TYPE"].ToString())
                        {
                            case "로드셀":
                                로드셀중량 += Convert.ToDecimal(pDataTable7.Rows[i]["중량"]);
                                break;
                            case "로드셀_출탕":
                                로드셀중량 += Convert.ToDecimal(pDataTable7.Rows[i]["중량"]);
                                break;
                            case "실투입":
                                용탑투입 += Convert.ToDecimal(pDataTable7.Rows[i]["중량"]);
                                break;
                            case "용탕_출탕":
                                용탑투입 += Convert.ToDecimal(pDataTable7.Rows[i]["중량"]);
                                break;
                            default:
                                break;
                        }

                    }
                    // 인바코드에 실투입
                    _용탕투입중량.Text = 용탑투입.ToString();
                    // 저울에서 드러온 데이터
                    _로드셀중량.Text = 로드셀중량.ToString();

                }

                //sql = $@"SELECT  resource_no,resource_used,qty_per
                //           FROM [sea_mfg].[dbo].[cproduct_defn]
                //            where resource_no = '{_p품번}'
                //           order by resource_no";


                sql = $@"select ISNULL(AVG(CAST(B.VALUE AS DECIMAL(10, 2))),AVG(CAST(C.VALUE AS DECIMAL(10, 2)))) AS VALUE,A.code_name as TYPE
                           from [dbo].[Code_Mst] A
                         left join [dbo].[WORK_RECYCLE_DETAIL] B ON A.code_name = B.TYPE AND  CAST(B.VALUE AS DECIMAL(10, 2)) > 0　AND B.WORK_PERFORMANCE_ID ='{_p실적}'
                         left join [dbo].[WORK_RECYCLE_DETAIL] C ON A.code_name = C.TYPE AND  CAST(C.VALUE AS DECIMAL(10, 2)) > 0　AND C.RESOURCE_NO = '{_p품번}'
                         WHERE A.code_type = 'CD20'
                         GROUP BY A.code_name";

                DataTable pDataTable8 = new CoreBusiness().SELECT(sql);

                if (pDataTable8.Rows.Count != 0)
                {
                    decimal 샷CAV = (Convert.ToDecimal(_lbl_양품.Text));

                    decimal 추가중량 = 0;

                    for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                    {
                        if (fpMain.Sheets[0].GetValue(i, "RESOURCE_NO   ".Trim()).ToString().Trim() == _p품번 &&
                            fpMain.Sheets[0].GetValue(i, "LOT           ".Trim()).ToString().Trim() == _pLOT)
                        {
                            string ck1 = fpMain.Sheets[0].GetValue(i, "RE_GROSS_STATUS           ".Trim()).ToString();
                            string ck2 = fpMain.Sheets[0].GetValue(i, "RE_GATE_STATUS           ".Trim()).ToString();

                            if (ck1 == "True" && ck2 == "True")
                            {
                                for (int x = 0; x < pDataTable8.Rows.Count; x++)
                                {
                                    if (pDataTable8.Rows[x]["TYPE"].ToString() == "Net")
                                    {
                                        추가중량 += Convert.ToDecimal(pDataTable8.Rows[x]["VALUE"]);
                                    }

                                }


                            }
                            else if (ck1 == "True" && ck2 == "False")
                            {
                                for (int x = 0; x < pDataTable8.Rows.Count; x++)
                                {
                                    if (pDataTable8.Rows[x]["TYPE"].ToString() == "Net")
                                    {
                                        추가중량 += Convert.ToDecimal(pDataTable8.Rows[x]["VALUE"]);
                                    }
                                    if (pDataTable8.Rows[x]["TYPE"].ToString() == "Gete")
                                    {
                                        추가중량 += Convert.ToDecimal(pDataTable8.Rows[x]["VALUE"]);
                                    }
                                }

                            }
                            else if (ck1 == "False" && ck2 == "True")
                            {

                                for (int x = 0; x < pDataTable8.Rows.Count; x++)
                                {
                                    if (pDataTable8.Rows[x]["TYPE"].ToString() == "Net")
                                    {
                                        추가중량 += Convert.ToDecimal(pDataTable8.Rows[x]["VALUE"]);
                                    }
                                    if (pDataTable8.Rows[x]["TYPE"].ToString() == "Scrap")
                                    {
                                        추가중량 += Convert.ToDecimal(pDataTable8.Rows[x]["VALUE"]);
                                    }
                                }
                            }
                            else if (ck1 == "False" && ck2 == "False")
                            {

                                for (int x = 0; x < pDataTable8.Rows.Count; x++)
                                {
                                    if (pDataTable8.Rows[x]["TYPE"].ToString() == "Net")
                                    {
                                        추가중량 += Convert.ToDecimal(pDataTable8.Rows[x]["VALUE"]);
                                    }
                                    if (pDataTable8.Rows[x]["TYPE"].ToString() == "Gete")
                                    {
                                        추가중량 += Convert.ToDecimal(pDataTable8.Rows[x]["VALUE"]);
                                    }
                                    if (pDataTable8.Rows[x]["TYPE"].ToString() == "Scrap")
                                    {
                                        추가중량 += Convert.ToDecimal(pDataTable8.Rows[x]["VALUE"]);
                                    }
                                }
                            }
                        }
                    }

                    if (추가중량 != 0)
                    {
                        _양품사용중량.Text = (샷CAV * 추가중량).ToString("F2");
                    }
                    else
                    {
                        sql = $@"SELECT  resource_no,resource_used,qty_per
                                   FROM [sea_mfg].[dbo].[cproduct_defn]
                                    where resource_no = '{_p품번}'
                                   order by resource_no";

                        DataTable pDataTable9 = new CoreBusiness().SELECT(sql);

                        _양품사용중량.Text = (샷CAV * (Convert.ToDecimal(pDataTable8.Rows[0]["qty_per"]))).ToString("F2");
                    }




                }

                fpSub.ActiveSheet.SetActiveCell(e.Row, e.Column);

                fpSub.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Center, FarPoint.Win.Spread.HorizontalPosition.Center);

                fpSub._SelectionChangedEvent(fpSub, null);
            }
            catch (Exception _Exception)
            {
                //CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        private void set_Data()
        {

            string sql = $@"SELECT *
                                 FROM[dbo].[BAD_PERFORMANCE]
                                 where 1 = 1
                                 AND LOT_NO              = '{_pLOT}'
                                 AND RESOURCE_NO         = '{_p품번}'
                                 AND WORK_PERFORMANCE_ID = '{_p실적}'";

            DataTable pDataTable2 =  new CoreBusiness().SELECT(sql);

            fpSub2.Sheets[0].Rows.Count = 0;

            if (pDataTable2 != null && pDataTable2.Rows.Count > 0)
            {
                fpSub2.Sheets[0].Visible = false;

                fpSub2.Sheets[0].Rows.Count = pDataTable2.Rows.Count;

                for (int i = 0; i < pDataTable2.Rows.Count; i++)
                {
                    foreach (DataColumn item in pDataTable2.Columns)
                    {
                        fpSub2.Sheets[0].SetValue(i, item.ColumnName, pDataTable2.Rows[i][item.ColumnName]);
                    }
                }
                //MyCore._AddItemSUM(fpMain, "admin");
                fpSub2.Sheets[0].Visible = true;
            }
            sql = $@"SELECT *
                       FROM [dbo].[IN_BARCODE]
                      WHERE 1 = 1
                        AND LOT_NO              = '{_pLOT}'
                        AND RESOURCE_NO         = '{_p품번}'
                        AND WORK_PERFORMANCE_ID = '{_p실적}'  
                        AND TYPE !='검증'
                   ORDER BY TYPE";

            DataTable pDataTable3 =  new CoreBusiness().SELECT(sql);

            fpSub3.Sheets[0].Rows.Count = 0;

            if (pDataTable3 != null && pDataTable3.Rows.Count > 0)
            {
                fpSub3.Sheets[0].Visible = false;

                fpSub3.Sheets[0].Rows.Count = pDataTable3.Rows.Count;

                for (int i = 0; i < pDataTable3.Rows.Count; i++)
                {
                    foreach (DataColumn item in pDataTable3.Columns)
                    {
                        fpSub3.Sheets[0].SetValue(i, item.ColumnName, pDataTable3.Rows[i][item.ColumnName]);
                    }
                }

                fpSub3.Sheets[0].Visible = true;
            }


        }

        #region ○ Form_Closing

        private void Form_Closing(object pSender, FormClosingEventArgs pFormClosingEventArgs)
        {
            try
            {

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);

            }
        }

        #endregion

        private void lblClose_Click(object sender, EventArgs e)
        {
            if (CustomMsg.ShowMessage("프로그램을 종료 하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }
        }

        #endregion


        private void 조회_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"select * from 
(SELECT   A.order_date                   AS 'ORDER_DATE'
                                          ,A.resource_no                  AS 'RESOURCE_NO'
                                          ,B.[description]                AS 'DESCRIPTION'
                                          ,A.lot                          AS 'LOT'
                                          ,A.order_qty                    AS 'ORDER_QTY'
                                          ,ISNULL(F.qty_complete,'0')     AS 'FIRST_QTY'
                                          ,(A.order_qty - A.qty_complete) AS 'REMAIN_QTY'
                                          ,A.qty_complete                 AS 'QTY_COMPLETE'
                                          ,A.demand_status                AS 'DEAMND_STATUS'
                                          ,C.workcenter                   AS 'WORKCENTER'
                                          ,D.gate_use                     AS 'GATE_USE'
                                          ,D.overflow_use                 AS 'OVERFLOW_USE'
                                          ,ISNULL(E.RE_GATE_STATUS ,'N')  AS 'RE_GATE_STATUS'
                                          ,ISNULL(E.RE_GROSS_STATUS,'N')  AS 'RE_GROSS_STATUS'
                                          ,ISNULL(E.RE_SCRAP_STATUS,'N')  AS 'RE_SCRAP_STATUS'
                                          ,ISNULL((select top 1 detail.cavity as cavity  from [sea_mfg].[dbo].[DEMAND_MSTR_ext] as mst with (nolock) inner join [sea_mfg].[dbo].[md_mst] detail on mst.code_md = detail.code_md where mst.order_no = A.resource_no and mst.lot = A.lot),0) as 'CAV'
                                          ,ISNULL((select top 1 code_md  from [sea_mfg].[dbo].[DEMAND_MSTR_ext] with (nolock) where order_no = A.resource_no and lot = A.lot),'') as 'CODE_MD'                
                                   FROM [sea_mfg].[dbo].[DEMAND_MSTR] AS A WITH (NOLOCK)
                                   INNER JOIN [sea_mfg].[dbo].[resource] AS B WITH (NOLOCK)
                                   ON B.resource_no  = A.resource_no
                                   INNER JOIN [sea_mfg].[dbo].[schedrtg] AS C  WITH (NOLOCK)
                                   ON C.order_no = A.resource_no 
                                   AND C.lot = A.lot
                                   INNER JOIN [sea_mfg].[dbo].[demand_repl_review]AS D  WITH (NOLOCK)
                                   ON D.order_no = A.resource_no
                                   AND D.lot = A.lot
								   and d.dr_type = 'D'
                           LEFT OUTER JOIN [HS_MES].[dbo].[WORK_RECYCLE] AS E WITH (NOLOCK)
                           ON A.resource_no = E.RESOURCE_NO
						   
                           AND A.lot = E.LOT_NO
                     LEFT OUTER JOIN (SELECT RESOURCE_NO,LOT_NO,SUM(qty_complete) AS qty_complete FROM  WORK_PERFORMANCE   WITH (NOLOCK) GROUP BY RESOURCE_NO , LOT_NO) AS F
                       ON A.resource_no = F.RESOURCE_NO
                           AND A.lot = F.LOT_NO
                   
                                   where 1=1
                                   AND A.order_no like '%DIE%'
                                   AND D.gate_use is not null
                                   AND D.overflow_use is not null 
								   and a.demand_status ='R'
								 ) as A

								  where CAV >0 ";

                StringBuilder sb = new StringBuilder();
                Core.Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString() + "order by A.ORDER_DATE";

                DataTable _DataTable = new CoreBusiness().SELECT(sql);
                fpMain.Sheets[0].Rows.Count = 0;
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


                    }
                    //Core.Function.Core._AddItemSUM(fpMain);
                    fpMain.Sheets[0].Visible = true;


                }
                else
                {
                    fpMain.Sheets[0].Rows.Count = 0;
                    CustomMsg.ShowMessage("조회 내역이 없습니다.");
                }
            }
            catch (Exception _Exception)
            {
                // CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
            finally
            {
                DevExpressManager.SetCursor(this, Cursors.Default);
            }
        }
        private void 원자재투입_Click(object sender, EventArgs e)
        {
            try
            {

                if (_p품번 != string.Empty &&
                    _pLOT != string.Empty &&
                    _p실적 != string.Empty)
                {
                    _로드셀.Port_Close();
                    using (from_CHECK_Barcode popup = new from_CHECK_Barcode())
                    {
                        popup._품번 = _p품번;
                        popup._LOT = _pLOT;
                        popup._p실적 = _p실적;
                        if (popup.ShowDialog() == DialogResult.OK)
                        {

                        }


                        for (int i = 0; i < fpSub.Sheets[0].RowCount; i++)
                        {
                            if (fpSub.Sheets[0].GetValue(i, "ID           ".Trim()).ToString().Trim() == _p실적)
                            {

                                fpSub_CellClick(fpMain, new CellClickEventArgs(null, i, 0, 0, 0, System.Windows.Forms.MouseButtons.Left, false, false));
                            }
                        }
                    }
                    _로드셀_Open(); ;
                }
                else
                {

                }
            }
            catch (Exception _Exception)
            {
                //CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
        private void 간판_Click(object sender, EventArgs e)
        {
            if (_p품번 != string.Empty &&
                 _pLOT != string.Empty &&
                 _p실적 != string.Empty)
            {
                int row = 0;

                for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                {
                    if (fpMain.Sheets[0].GetValue(i, "RESOURCE_NO   ".Trim()).ToString().Trim() == _p품번 &&
                        fpMain.Sheets[0].GetValue(i, "LOT           ".Trim()).ToString().Trim() == _pLOT)
                    {
                        row = i;
                    }
                }

                using (간판POP popup = new 간판POP(_UserEntity
                , fpMain.Sheets[0].GetValue(row, "RESOURCE_NO   ".Trim()).ToString()
                , fpMain.Sheets[0].GetValue(row, "DESCRIPTION ".Trim()).ToString()
                , fpMain.Sheets[0].GetValue(row, "ORDER_QTY   ".Trim()).ToString()
                , _미포장수량.Text
                , _pLOT
                , _p실적))

                {
                    if (popup.ShowDialog() == DialogResult.OK)

                    for (int i = 0; i < fpSub.Sheets[0].RowCount; i++)
                    {
                        if (fpSub.Sheets[0].GetValue(i, "ID           ".Trim()).ToString().Trim() == _p실적)
                        {

                            fpSub_CellClick(fpMain, new CellClickEventArgs(null, i, 0, 0, 0, System.Windows.Forms.MouseButtons.Left, false, false));
                        }
                    }
                    조회.PerformClick();
                    run(_p품번, _pLOT, _p호기);
                }

                this.간판_Click(_p실적,e);
            }
        }
        private void 저장_Click(object sender, EventArgs e)
        {
            if (_p품번 != string.Empty && _pLOT != string.Empty)
            {


            }

        }
        private void 시작_Click(object sender, EventArgs e)
        {
            if (_p품번 != string.Empty && _pLOT != string.Empty)
            {

                bool ck = true;
                for (int i = 0; i < fpSub.Sheets[0].RowCount; i++)
                {
                    if (fpSub.Sheets[0].GetValue(i, "END_TIME   ".Trim()).ToString().Trim() == "")
                    {
                        ck = false;
                    }
                }

                if (ck == false)
                {
                    CustomMsg.ShowMessage("아직 종료되지 않은 실적이 존재합니다. 실적 종료후 새로운 실적을 시작해주세요.");
                    return;
                }

                int planQty = 0;

                int row = 0;

                for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                {
                    if (fpMain.Sheets[0].GetValue(i, "RESOURCE_NO   ".Trim()).ToString().Trim() == _p품번 &&
                        fpMain.Sheets[0].GetValue(i, "LOT           ".Trim()).ToString().Trim() == _pLOT)
                    {
                        row = i;
                        planQty = Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "ORDER_QTY           ".Trim()));
                    }
                }

                _시작.DateTime = DateTime.Now;

                if (_시작.DateTime.Hour >= 8 && _시작.DateTime.Hour < 20)
                {
                    _교대조.Text = "주간";
                }
                else
                {
                    _교대조.Text = "야간";
                }

                int comQty =  Convert.ToInt32(_포장수량.Text);
                DateTime dateTime = DateTime.Now;
                DataTable dt = new MS_DBClass(utility.My_Settings_Get()).USP_WorkPerformance_A10(
                  _p호기  
                , _작업일자.DateTime.ToString("yyyyMMdd")
                , _p품번
                , _pLOT
                , _교대조.Text
                , _작업코드.Text
                , comQty
                , "0"
                , _시작.DateTime.ToString("yyyy-MM-dd HH:mm:ss")
                , null
                , "admin"
                , dateTime.ToString("yyyy-MM-dd HH:mm:ss")
                , "admin"
                , dateTime.ToString("yyyy-MM-dd HH:mm:ss")
                , txt_작업인원.Text);

                string id = dt.Rows[0]["ID"].ToString();

                string sql = $@"INSERT INTO WORK_PERFORMANCE
                               (
                                MACHINE_NO
                               ,ORDER_NO
                               ,RESOURCE_NO
                               ,LOT_NO
                               ,ORDER_DATE
                               ,SHIFT
                               ,PLAN_PERFORMANCE
                               ,IS_WORKING
                               ,WORK_CODE
                               ,QTY_COMPLETE
                               ,WORK_TIME
                               ,START_TIME
                               ,END_TIME
                               ,REG_USER
                               ,REG_DATE
                               ,UP_USER
                               ,UP_DATE
                               ,WORK_PERFORMANCE_ID)
                         VALUES
                               ('{dt.Rows[0]["MACHINE_NO"].ToString()}'
                               ,'{dt.Rows[0]["ORDER_NO"].ToString()}'
                               ,'{dt.Rows[0]["RESOURCE_NO"].ToString()}'
                               ,'{dt.Rows[0]["LOT_NO"].ToString()}'
                               ,'{Convert.ToDateTime(dt.Rows[0]["ORDER_DATE"]).ToString("yyyy-MM-dd HH:mm:ss")}'
                               ,'{dt.Rows[0]["SHIFT"].ToString()}'
                               ,'{planQty.ToString()}'
                               ,'가동'
                               ,'{dt.Rows[0]["WORK_CODE"].ToString()}'
                               ,'{dt.Rows[0]["QTY_COMPLETE"].ToString()}'
                               ,'{dt.Rows[0]["WORK_TIME"].ToString()}'
                               ,'{Convert.ToDateTime(dt.Rows[0]["START_TIME"]).ToString("yyyy-MM-dd HH:mm:ss")}'
                               ,'{Convert.ToDateTime(dt.Rows[0]["START_TIME"]).ToString("yyyy-MM-dd HH:mm:ss")}'
                               ,'{dt.Rows[0]["REG_USER"].ToString()}'
                               ,'{Convert.ToDateTime(dt.Rows[0]["REG_DATE"]).ToString("yyyy-MM-dd HH:mm:ss")}'
                               ,'{dt.Rows[0]["UP_USER"].ToString()}'
                               ,'{Convert.ToDateTime(dt.Rows[0]["UP_DATE"]).ToString("yyyy-MM-dd HH:mm:ss")}'
                               ,'{id}')";
                            //여기에 cavity 추가 해서 처리 ? 하는게 가장 깔끔하지 않을까? 싶음
                DataTable pDataTable5 = new MY_DBClass().SELECT_DataTable(sql);

                for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                {
                    if (fpMain.Sheets[0].GetValue(i, "RESOURCE_NO   ".Trim()).ToString().Trim() == _p품번 &&
                        fpMain.Sheets[0].GetValue(i, "LOT           ".Trim()).ToString().Trim() == _pLOT)
                    {
                        fpMain_CellClick(fpMain, new CellClickEventArgs(null, i, 0, 0, 0, System.Windows.Forms.MouseButtons.Left, false, false));
                    }
                }

                for (int i = 0; i < fpSub.Sheets[0].RowCount; i++)
                {
                    if (fpSub.Sheets[0].GetValue(i, "END_TIME           ".Trim()).ToString().Trim() == "")
                    {

                        fpSub_CellClick(fpSub, new CellClickEventArgs(null, i, 0, 0, 0, System.Windows.Forms.MouseButtons.Left, false, false));
                    }
                }


                CustomMsg.ShowMessage("저장 되었습니다.");


            }
        }
        private void 완료_Click(object sender, EventArgs e)
        {
            if (_p실적 != string.Empty)
            {
                int row = 0;
                for (int i = 0; i < fpSub.Sheets[0].RowCount; i++)
                {
                    if (fpSub.Sheets[0].GetValue(i, "ID".Trim()).ToString() == _p실적)
                    {
                        row = i;
                        if (fpSub.Sheets[0].GetValue(i, "END_TIME    ".Trim()).ToString() != "")
                        {
                            CustomMsg.ShowMessage("종료된 작업 실적 입니다.");
                            return;
                        }
                    }
                }


                int ok_qty =0;

                if (!int.TryParse(_lbl_양품.Text, out ok_qty))
                {
                    CustomMsg.ShowMessage("양품수량을 입력해주세요.");
                    return;
                }
                if (ok_qty == 0)
                {
                    CustomMsg.ShowMessage("양품수량을 입력해주세요.");
                    return;
                }

                String comQty = (Convert.ToInt32(_간판발행수.Text) * Convert.ToInt32(_포장수량.Text)).ToString();
                DateTime startTime = _시작.DateTime;
                DateTime endTime = _종료.DateTime;

                // 두 날짜/시간 사이의 시간 차이 계산
                TimeSpan difference = endTime - startTime;
                string sql =$@"UPDATE [dbo].[WORK_PERFORMANCE]
                                  SET 
                                      [QTY_COMPLETE] = '{comQty}'
                                     ,[WORK_TIME]    = '{difference.TotalSeconds}'
                                     ,[END_TIME]     = '{_종료.DateTime.ToString("yyyy-MM-dd HH:mm:ss")}'
                                WHERE ID = '{_p실적}'";
                DataTable _DataTable = new CoreBusiness().SELECT(sql);



                 new MS_DBClass(utility.My_Settings_Get()).USP_BadPerformance_A10(
                     DateTime.Now.ToString("yyyyMMdd")
                    ,fpSub.Sheets[0].GetValue(row, "RESOURCE_NO    ".Trim()).ToString()
                    ,"Y"
                    , comQty
                    , "T"
                    ,fpSub.Sheets[0].GetValue(row, "ORDER_NO    ".Trim()).ToString()
                    ,fpSub.Sheets[0].GetValue(row, "LOT_NO    ".Trim()).ToString()
                    ,"ADMIN"
                    ,"ADMIN"
                    );

                


                sql = $@"UPDATE work_performance
                         SET END_TIME = '{startTime.ToString("yyyy-MM-dd HH:mm:ss")}'
                            ,IS_WORKING = '비가동'
                         WHERE 1 = 1
                         AND RESOURCE_NO = '{fpSub.Sheets[0].GetValue(row, "RESOURCE_NO    ".Trim()).ToString()}'
                         AND LOT_NO      = '{fpSub.Sheets[0].GetValue(row, "LOT_NO         ".Trim()).ToString()}'";

                new MY_DBClass().SELECT_DataTable(sql);




                sql = $@"select ISNULL(AVG(CAST(B.VALUE AS DECIMAL(10, 2))),0) AS VALUE,A.code_name as TYPE
                           from [dbo].[Code_Mst] A
                         left join [dbo].[WORK_RECYCLE_DETAIL] B ON A.code_name = B.TYPE AND  CAST(B.VALUE AS DECIMAL(10, 2)) > 0 AND B.WORK_PERFORMANCE_ID  ='{_p실적}'
                         WHERE A.code_type = 'CD20' AND A.code_name ='Scrap'
                         GROUP BY A.code_name";

                DataTable pDataTable8 = new CoreBusiness().SELECT(sql);


             
                if (pDataTable8.Rows.Count != 0)
                {
                    decimal 이동중량 = 0;

                    decimal 샷CAV = (Convert.ToDecimal(_lbl_양품.Text));
                
                    decimal 스크랩중량 = Convert.ToDecimal(pDataTable8.Rows[0]["VALUE"]); 

                    if (스크랩중량 != 0)
                    {
                        이동중량 = (샷CAV * 스크랩중량);
                    }
                    else
                    {
                        sql = $@"SELECT  resource_no,resource_used,qty_per
                                   FROM [sea_mfg].[dbo].[cproduct_defn]
                                    where resource_no = '{_p품번}'
                                   order by resource_no";

                        DataTable bom_dt = new CoreBusiness().SELECT(sql);
                        이동중량 = (샷CAV * (Convert.ToDecimal(bom_dt.Rows[0]["qty_per"])));
                    }

                    sql = $@"INSERT INTO [dbo].[SCRAP_MOVE_MST]
                                    ([WORK_PERFORMANCE_ID]
                                    ,[RESOURCE_NO]
                                    ,[LOT_NO]
                                    ,[DATE]
                                    ,[QTY]
                                    ,[COMMENT]
                                    ,[USE_YN]
                                    ,[UP_USER]
                                    ,[UP_DATE]
                                    ,[REG_USER]
                                    ,[REG_DATE])
                              VALUES
                                    ('{_p실적}'
                                    ,'{_p품번}'
                                    ,'{_pLOT}'
                                    ,GETDATE()
                                    ,{이동중량}
                                    ,''
                                    ,'Y'
                                    ,'{_UserEntity.user_account}'
                                    ,GETDATE()
                                    ,'{_UserEntity.user_account}'
                                    ,GETDATE())";

                    new MY_DBClass().SELECT_DataTable(sql);
                }



                CustomMsg.ShowMessage("저장 되었습니다.");

                for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                {
                    if (fpMain.Sheets[0].GetValue(i, "RESOURCE_NO   ".Trim()).ToString().Trim() == _p품번 &&
                        fpMain.Sheets[0].GetValue(i, "LOT           ".Trim()).ToString().Trim() == _pLOT)
                    {
                        fpMain_CellClick(fpMain, new CellClickEventArgs(null, i, 0, 0, 0, System.Windows.Forms.MouseButtons.Left, false, false));
                    }
                }

                for (int i = 0; i < fpSub.Sheets[0].RowCount; i++)
                {
                    if (fpSub.Sheets[0].GetValue(i, "ID           ".Trim()).ToString().Trim() == _p실적)
                    {
                        fpSub_CellClick(fpMain, new CellClickEventArgs(null, i, 0, 0, 0, System.Windows.Forms.MouseButtons.Left, false, false));
                    }
                }
            }
        }
        private void btn_중량검사_Click(object sender, EventArgs e)
        {
            if (_p품번 != string.Empty &&
                 _pLOT != string.Empty &&
                _p실적 != string.Empty)
            {
                using (from_세아초중종 from = new from_세아초중종())
                {
                    from._p품번 = _p품번;
                    from._pLOT = _pLOT;
                    from._p실적 = _p실적;
                    from.ShowDialog();



                    for (int i = 0; i < fpSub.Sheets[0].RowCount; i++)
                    {
                        if (fpSub.Sheets[0].GetValue(i, "ID           ".Trim()).ToString().Trim() == _p실적)
                        {

                            fpSub_CellClick(fpMain, new CellClickEventArgs(null, i, 0, 0, 0, System.Windows.Forms.MouseButtons.Left, false, false));
                        }
                    }
                }
            }
            else
            {
                CustomMsg.ShowMessage("선택된 작업지시가 없습니다.");
            }
        }
        private void btn_출탕_Click(object sender, EventArgs e)
        {
            if (_p품번 != string.Empty &&
                 _pLOT != string.Empty &&
                 _p실적 != string.Empty
                )
            {
                if (_로드셀중량.Text != "-")
                {

                    string sql = $@"SELECT  resource_no,resource_used,qty_per
                                   FROM [sea_mfg].[dbo].[cproduct_defn]
                                    where resource_no = '{_p품번}'
                                   order by resource_no";

                    DataTable pDataTable9 = new CoreBusiness().SELECT(sql);


                    using (출탕 from = new 출탕(_UserEntity
                          , _용탕투입중량.Text
                         , _로드셀중량.Text
                         , pDataTable9.Rows[0]["resource_used"].ToString()
                         , pDataTable9.Rows[0]["resource_used"].ToString())
                        )
                    {



                        from._pLOT = _pLOT;
                        from._p품번 = pDataTable9.Rows[0]["resource_used"].ToString();
                        from._p실적 = _p실적;

                        from.ShowDialog();



                        for (int i = 0; i < fpSub.Sheets[0].RowCount; i++)
                        {
                            if (fpSub.Sheets[0].GetValue(i, "ID           ".Trim()).ToString().Trim() == _p실적)
                            {

                                fpSub_CellClick(fpMain, new CellClickEventArgs(null, i, 0, 0, 0, System.Windows.Forms.MouseButtons.Left, false, false));
                            }
                        }
                    }
                }
                else
                {
                    CustomMsg.ShowMessage("등록된 중량이 없습니다.");
                }

            }
        }
        private void btn_COM설정_Click(object sender, EventArgs e)
        {
            //BaseFormSetting baseFormSetting = new BaseFormSetting(this.Name,"admin");

            //baseFormSetting.Show();

        }
        private void _로드셀중량_Click(object sender, EventArgs e)
        {
            using (from_키패드 popup = new from_키패드())
            {
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ReadEventArgs readEvent = new ReadEventArgs();
                    readEvent.ReadMsg = popup._code;
                    _로드셀_BarCode(null, readEvent);

                }

            }
        }
        private void btn_불량실적_Click(object sender, EventArgs e)
        {
            if (_p품번 != string.Empty &&
                   _pLOT != string.Empty &&
                  _p실적 != string.Empty)
            {
                using (from_불량입력 from = new from_불량입력(_UserEntity))
                {
         

                    from._p품번 = _p품번;
                    from._p품명 = _품목명.Text;
                    from._pLOT = _pLOT;
                    from._p실적 = _p실적;
                    from.ShowDialog();



                    for (int i = 0; i < fpSub.Sheets[0].RowCount; i++)
                    {
                        if (fpSub.Sheets[0].GetValue(i, "ID           ".Trim()).ToString().Trim() == _p실적)
                        {

                            fpSub_CellClick(fpMain, new CellClickEventArgs(null, i, 0, 0, 0, System.Windows.Forms.MouseButtons.Left, false, false));
                        }
                    }
                }
            }
            else
            {
                CustomMsg.ShowMessage("선택된 작업지시가 없습니다.");
            }


        }
        private void 정지_Click(object sender, EventArgs e)
        {
            if (_p실적 != string.Empty)
            {
                int row = 0;
                for (int i = 0; i < fpSub.Sheets[0].RowCount; i++)
                {
                    if (fpSub.Sheets[0].GetValue(i, "ID".Trim()).ToString() == _p실적)
                    {
                        row = i;
                        if (fpSub.Sheets[0].GetValue(i, "END_TIME    ".Trim()).ToString() != "")
                        {
                            CustomMsg.ShowMessage("종료된 작업 실적 입니다.");
                            return;
                        }
                    }
                }

                DateTime startTime = _시작.DateTime;
                DateTime endTime = _종료.DateTime;

                // 두 날짜/시간 사이의 시간 차이 계산
                TimeSpan difference = endTime - startTime;

                string sql =$@"UPDATE [dbo].[WORK_PERFORMANCE]
                                  SET 
                                      [QTY_COMPLETE] = '{0}'
                                     ,[WORK_TIME]    = '{difference.TotalSeconds}'
                                     ,[END_TIME]     = '{_종료.DateTime.ToString("yyyy-MM-dd HH:mm:ss")}'
                                WHERE ID = '{_p실적}'";
                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                DataTable dt = new MS_DBClass(utility.My_Settings_Get()).USP_BadPerformance_A10(
                     DateTime.Now.ToString("yyyyMMdd")
                    ,fpSub.Sheets[0].GetValue(row, "RESOURCE_NO    ".Trim()).ToString()
                    ,"Y"
                    ,_lbl_양품.Text
                    ,"T"
                    ,fpSub.Sheets[0].GetValue(row, "ORDER_NO    ".Trim()).ToString()
                    ,fpSub.Sheets[0].GetValue(row, "LOT_NO    ".Trim()).ToString()
                    ,"ADMIN"
                    ,"ADMIN"
                    );

                sql = $@"UPDATE work_performance
                         SET END_TIME = '{startTime.ToString("yyyy-MM-dd HH:mm:ss")}'
                            ,IS_WORKING = '비가동'
                         WHERE 1 = 1
                         AND RESOURCE_NO = '{fpSub.Sheets[0].GetValue(row, "RESOURCE_NO    ".Trim()).ToString()}'
                         AND LOT_NO = '{fpSub.Sheets[0].GetValue(row, "LOT_NO    ".Trim()).ToString()}'";

                new MY_DBClass().SELECT_DataTable(sql);

                CustomMsg.ShowMessage("저장 되었습니다.");

                for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                {
                    if (fpMain.Sheets[0].GetValue(i, "RESOURCE_NO   ".Trim()).ToString().Trim() == _p품번 &&
                        fpMain.Sheets[0].GetValue(i, "LOT           ".Trim()).ToString().Trim() == _pLOT)
                    {
                        fpMain_CellClick(fpMain, new CellClickEventArgs(null, i, 0, 0, 0, System.Windows.Forms.MouseButtons.Left, false, false));
                    }
                }

                for (int i = 0; i < fpSub.Sheets[0].RowCount; i++)
                {
                    if (fpSub.Sheets[0].GetValue(i, "ID           ".Trim()).ToString().Trim() == _p실적)
                    {

                        fpSub_CellClick(fpMain, new CellClickEventArgs(null, i, 0, 0, 0, System.Windows.Forms.MouseButtons.Left, false, false));
                    }
                }
            }
        }
        private void txt_작업인원_Click(object sender, EventArgs e)
        {
            if (_p품번 != string.Empty &&
              _pLOT != string.Empty &&
             _p실적 != string.Empty)
            {
                using (from_키패드 popup = new from_키패드())
                {
                    if (popup.ShowDialog() == DialogResult.OK)
                    {

                        string sql  = $@"UPDATE [dbo].[WORK_PERFORMANCE]
                                        SET [IN_PER] = {popup._code}
                                       WHERE ID = '{_p실적}'";

                        DataTable dt = new CoreBusiness().SELECT(sql);

                        txt_작업인원.Text = popup._code;
                    }
                }



            }
        }
    }
}






