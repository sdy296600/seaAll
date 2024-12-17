using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP
{
    public partial class Data_Mon_POP : Form
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
        //public My_Settings _MY = null;

        private UserEntity _UserEntity = new UserEntity();


        public string _p실적 = string.Empty;
        public string _pLOT = string.Empty;
        public string prodBcdQty = string.Empty;
        #endregion

        #region ○ 생성자

        public Data_Mon_POP(UserEntity pUserEntity,
             string 품번
            ,string 품목명
            ,string 지시수량
            ,string 양품수량
            ,string LOT
            ,string 실적 )
        {
            InitializeComponent();

            _UserEntity = pUserEntity;

            lbl_품번.Text     = 품번.Trim();
            lbl_품목명.Text   = 품목명.Trim();
            lbl_지시수량.Text = Convert.ToDecimal(지시수량.Trim()).ToString("F0");
            lbl_양품수량.Text = Convert.ToDecimal(양품수량.Trim()).ToString("F0");
            _pLOT = LOT;
            _p실적 = 실적;
            //_MY = utility.My_Settings_Get();

            Load += new EventHandler(Form_Load);

            this.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE, FontStyle.Bold);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
        }
        public Data_Mon_POP( )
        {
            InitializeComponent();
          


            //_MY = utility.My_Settings_Get();s

            Load += new EventHandler(Form_Load);

            //this.Font = new Font(_MY.FONT_TYPE, _MY.FONT_SIZE, FontStyle.Bold);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
        }
        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                //txt_포장수량.Font = new Font("맑은 고딕", 18, FontStyle.Bold);
                //txt_비고.Font = new Font("맑은 고딕", 18, FontStyle.Bold);

               //DataTable pDataTable1 =  new CoreBusiness().BASE_MENU_SETTING_R10(this.Name,fpMain,"PRODUCT_BARCODE");

                //if (pDataTable1 != null)
                //{

                    //CoFAS.NEW.MES.Core.Function.Core.initializeSpread(pDataTable1, fpMain, this.Name, "admin");
                    //Function.Core.InitializeControl(pDataTable1, fpMain, this, panel1, _MenuSettingEntity);
                //}

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
      
        #endregion

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //생성
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                
                int 포장수량 = 0;
                prodBcdQty = "0";

                //if (!int.TryParse(txt_포장수량.Text, out 포장수량))
                //{
                //    CustomMsg.ShowMessage("포장할 양품이 없습니다.");
                //    return;
                //}

                if (Convert.ToInt32(lbl_양품수량.Text) <= 0 || Convert.ToInt32(lbl_양품수량.Text) < 포장수량 )
                {
                    CustomMsg.ShowMessage("포장 수장은 양품수량보다 클 수 없습니다. 양품수량에 맞게 포장해 주세요.");
                    return;
                }


                string msg = $"라벨 출력 하시겠습니까?";
                if (CustomMsg.ShowMessage(msg, "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    //DataTable dt = new MS_DBClass(utility.My_Settings_Get()).USP_ProductBarcode_A10( lbl_품번.Text
                    //                                                                               , ""
                    //                                                                               , _pLOT
                    //                                                                               , "주조"
                    //                                                                               , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    //                                                                               , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    //                                                                               , ""
                    //                                                                               , txt_포장수량.Text.ToString()
                    //                                                                               , txt_비고.Text.ToString()
                    //                                                                               , _UserEntity.user_account
                    //                                                                               , _p실적
                    //                                                                               );
                        
                    //if (dt.Rows.Count != 0)
                    //{

                    //    foreach (DataRow item in dt.Rows)
                    //    {

                    //        공정이동표 라벨 = new 공정이동표();

                    //        라벨.LOT_NO       = item["LOT_NO      ".Trim()].ToString();
                    //        라벨.품목명        = lbl_품목명.Text;
                    //        라벨.BARCODE_DATE = item["BARCODE_DATE".Trim()].ToString();
                    //        라벨.MOVE_DATE    = item["MOVE_DATE   ".Trim()].ToString();
                    //        라벨.ID           = item["ID          ".Trim()].ToString();
                    //        라벨.P_QTY        = item["P_QTY       ".Trim()].ToString();
                    //        라벨.RESOURCE_NO  = item["RESOURCE_NO ".Trim()].ToString();
                    //        라벨.BARCODE_NO   = item["BARCODE_NO   ".Trim()].ToString();
                    //        print(라벨);
                    //    }

                    //    string sql = $@"SELECT ISNULL(SUM(ISNULL(P_QTY,0)),0) AS P_QTY
                    //                      FROM HS_MES.dbo.PRODUCT_BARCODE
                    //                     WHERE WORK_PERFORMANCE_ID = {_p실적}";
                    //    DataTable dt2 = new MS_DBClass(utility.My_Settings_Get()).SELECT2(sql);

                    //    sql = $@"UPDATE dbo.WORK_PERFORMANCE
                    //                SET QTY_COMPLETE = '{dt2.Rows[0]["P_QTY"].ToString()}'
                    //                  , UP_DATE      = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                    //              WHERE ID           = '{_p실적}'";
                    //    DataTable _DataTable = new CoreBusiness().SELECT(sql);


                    //    lbl_양품수량.Text = (Convert.ToInt32(lbl_양품수량.Text) - Convert.ToInt32(txt_포장수량.Text)).ToString(); 
                    //    txt_포장수량.Text = "";

                    //    button5_Click(sender, e);

                    //    }
                    }
            }
            catch (Exception ex)
            {
                CustomMsg.ShowExceptionMessage(ex.ToString(), "Error발생 관리자에게 문의 해 주세요.", MessageBoxButtons.OK);
            }

        }

        //조회버튼
        private void button5_Click(object sender, EventArgs e)
        {
            //prodBcdQty = "0";
            //string GET_PACK_INFO = $@" SELECT 'False' as CK
            //                                , A.*
						      //              , B.DESCRIPTION  
            //                             FROM HS_MES.dbo.PRODUCT_BARCODE   A
            //                                , sea_mfg.dbo.resource    B
            //                            WHERE A.resource_no = B.resource_no
            //                              AND A.WORK_PERFORMANCE_ID = {_p실적} 
            //                              AND A.BARCODE_DATE BETWEEN '{base_FromtoDateTime1.StartValue.ToString("yyyy-MM-dd HH:mm:ss.fff")}' AND '{base_FromtoDateTime1.EndValue.ToString("yyyy-MM-dd HH:mm:ss.fff")}'
            //                         "; 
            //DataTable dtGetPackInfo = new MS_DBClass(utility.My_Settings_Get()).SELECT2(GET_PACK_INFO);
            //Core.Function.Core.DisplayData_Set(dtGetPackInfo, fpMain);

            //int row;
            //for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
            //{
            //    if (fpMain.Sheets[0].GetValue(i, "WORK_PERFORMANCE_ID   ".Trim()).ToString().Trim() == _p실적 &&
            //        fpMain.Sheets[0].GetValue(i, "LOT           ".Trim()).ToString().Trim() == _pLOT)
            //    {
            //        row = i;
            //        prodBcdQty = fpMain.Sheets[0].GetValue(row, "P_QTY   ".Trim()).ToString();
            //    }
            //}

            //lbl_양품수량.Text = ;
            //int packQty = Convert.ToInt16(dtGetPackInfo.Rows[0]["P_QTY"].ToString());
            //prodBcdQty = Convert.ToInt16(prodBcdQty) - packQty;

        }


        private void print(공정이동표 라벨)
        {
            string printerName = "ZDesigner GT800 (EPL)"; //세아 라벨 프린트 원자재간판은 ZPL이고 그냥 간판은 EPL -> 무슨 기준??
            string printerName1 = "ZDesigner ZD230-203dpi ZPL";
            string zplCommand = string.Empty;

            zplCommand = $@"^XA
                            ^SEE:UHANGUL.DAT^FS
                            ^CW1,E:KFONT3.FNT^CI26^FS
                            ^FO30,30^GB730,360,3^FS
                            ^FO30,90^GB730,3,3^FS
                            ^FO30,150^GB730,3,3^FS
                            ^FO30,210^GB730,3,3^FS
                            ^FO30,270^GB730,3,3^FS
                            ^FO30,330^GB470,3,3^FS
                            ^FO50,50^A1N,36,16^FD고객사^FS
                            ^FO50,110^A1N,36,16^FD품명^FS
                            ^FO50,170^A1N,36,16^FD생산일자^FS
                            ^FO50,230^A1N,36,16^FD전공정^FS
                            ^FO50,290^A1N,36,16^FD수량^FS
                            ^FO50,350^A1N,36,16^FD품번^FS

                            ^FO170,30^GB3,360,3^FS
                            ^FO180,230^A1N,36,16^FD주조^FS
                            ^FO415,50^A1N,36,16^FDLOT^FS
                            ^FO415,170^A1N,36,16^FD이동일자^FS
                            ^FO415,230^A1N,36,16^FD재고기록표^FS

                            ^FO180,50^A1N,36,16^FD^FS
                            ^FO185,110^A1N,36,16^FD{라벨.품목명}^FS
                            ^FO185,170^A1N,36,16^FD{라벨.BARCODE_DATE}^FS
                            ^FO185,290^A1N,36,16^FD{라벨.P_QTY}^FS
                            ^FO185,350^A1N,36,16^FD{라벨.RESOURCE_NO}^FS

                            ^FO400,30^GB3,60,3^FS
                            ^FO400,150^GB3,120,3^FS
                            ^FO500,30^GB3,60,3^FS
                            ^FO500,150^GB3,240,3^FS

                            ^FO515,50^A1N,36,16^FD{라벨.LOT_NO}^FS
                            ^FO515,170^A1N,36,16^FD{라벨.MOVE_DATE}^FS
                            ^FO515,230^A1N,36,16^FD{라벨.ID}^FS

                            ^FO580,290^BON,7,7
                            ^BXN,4,200^FDMM,B0024 {라벨.BARCODE_NO}^F
                            ^XZ";
            try
            {
                //PrintServer printServer = new PrintServer();

                //if (printServer.Name.ToString() == "\\\\YOUNG")
                //{
                //    PrintQueue printQueue1 = new PrintQueue(printServer, printerName1, PrintSystemDesiredAccess.AdministratePrinter);
                //    RawPrinterHelper.SendStringToPrinter(printerName1, zplCommand);
                //}
                //else
                //{
                //    PrintQueue printQueue = new PrintQueue(printServer, printerName, PrintSystemDesiredAccess.AdministratePrinter);
                //    RawPrinterHelper.SendStringToPrinter(printerName, zplCommand);
                //    //printQueue.Purge();
                //}

                //CustomMsg.ShowMessage("라벨출력이 완료되었습니다.");
                // _lblMessage.Text = "라벨 출력이 완료되었습니다.";
            }
            catch (Exception ex)
            {
                //_lblMessage.Text = $"ZPL 명령 전송 중 오류 발생: {ex.Message}";
            }
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public class 공정이동표
    {
        public string LOT_NO        { get; set; }
        public string 품목명        { get; set; }
        public string BARCODE_DATE { get; set; }
        public string MOVE_DATE    { get; set; }
        public string ID           { get; set; }
        public string P_QTY         {  get; set; }
        public string RESOURCE_NO   { get; set; }
        public string BARCODE_NO   { get; set; }
    }
}





