using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.POP.Function;
using FarPoint.Win.Spread;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP
{
    public partial class 간판POP : Form
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


        public string _p실적 = string.Empty;
        public string _pLOT = string.Empty;
        public string prodBcdQty = string.Empty;
        #endregion

        #region ○ 생성자

        public 간판POP(UserEntity pUserEntity,
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
            _MY = utility.My_Settings_Get();

            Load += new EventHandler(Form_Load);

            this.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE, FontStyle.Bold);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
        }
        public 간판POP( )
        {
            InitializeComponent();
          


            _MY = utility.My_Settings_Get();

            Load += new EventHandler(Form_Load);

            this.Font = new Font(_MY.FONT_TYPE, _MY.FONT_SIZE, FontStyle.Bold);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
        }
        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                txt_포장수량.Font = new Font("맑은 고딕", 18, FontStyle.Bold);
                txt_비고.Font = new Font("맑은 고딕", 18, FontStyle.Bold);

                DataTable pDataTable1 =  new CoreBusiness().BASE_MENU_SETTING_R10(this.Name,fpMain,"PRODUCT_BARCODE");

                if (pDataTable1 != null)
                {

                    CoFAS.NEW.MES.Core.Function.Core.initializeSpread(pDataTable1, fpMain, this.Name, "admin");
                    //Function.Core.InitializeControl(pDataTable1, fpMain, this, panel1, _MenuSettingEntity);
                }

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


        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                
                int 포장수량 = 0;
                prodBcdQty = "0";

                if (!int.TryParse(txt_포장수량.Text, out 포장수량))
                {
                    return;
                }

                string msg = $"라벨 출력 하시겠습니까?";
                if (CustomMsg.ShowMessage(msg, "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    DataTable dt = new MS_DBClass(utility.My_Settings_Get()).USP_ProductBarcode_A10(
                    lbl_품번.Text
                   ,""
                   ,_pLOT
                   ,"주조"
                   ,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                   ,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                   ,""
                   , txt_포장수량.Text.ToString()
                   ,txt_비고.Text.ToString()
                   ,_UserEntity.user_account
                   ,_p실적
                    );
                        
                    if (dt.Rows.Count != 0)
                    {

                        foreach (DataRow item in dt.Rows)
                        {

                            공정이동표 라벨 = new 공정이동표();

                            라벨.LOT_NO       =  item["LOT_NO      ".Trim()].ToString();
                            라벨.품목명       = lbl_품목명.Text;
                            라벨.BARCODE_DATE =  item["BARCODE_DATE".Trim()].ToString();
                            라벨.MOVE_DATE    =  item["MOVE_DATE   ".Trim()].ToString();
                            라벨.ID           =  item["ID          ".Trim()].ToString();
                            라벨.P_QTY        =  item["P_QTY       ".Trim()].ToString();
                            라벨.RESOURCE_NO  =  item["RESOURCE_NO ".Trim()].ToString();
                            라벨.BARCODE_NO   =  item["BARCODE_NO   ".Trim()].ToString();
                            print(라벨);
                        }

                        string sql = $@"SELECT                       
                            ISNULL(SUM(ISNULL(P_QTY,0)),0) AS P_QTY
                            FROM [HS_MES].[dbo].[PRODUCT_BARCODE]
                            WHERE 1=1 
                            AND WORK_PERFORMANCE_ID = {_p실적}";

                        /// INNER JOIN [sea_mfg].[dbo].[address] B ON A.VENDOR_NO = B.address_key";
                        //WHERE BARCODE_DATE >= '{DateTime.Now.ToString("yyyy-MM-dd")}'";

                        DataTable dt2 = new MS_DBClass(utility.My_Settings_Get()).SELECT2(sql);

                        sql = $@"UPDATE [dbo].[WORK_PERFORMANCE]
                                    SET 
                                        [QTY_COMPLETE] = '{dt2.Rows[0]["P_QTY"].ToString()}'
                                        ,[UP_DATE]  = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                WHERE ID = '{_p실적}'";
                        DataTable _DataTable = new CoreBusiness().SELECT(sql);

                        button5_Click(sender, e);       
                        }
                    }
            }
            catch (Exception err)
            {

            }

        }

        //private void print(DataRow dr)
        //{ 
        //    string printerName = "ZDesigner GT800 (EPL)"; // 프린터 이름으로 변경하세요
        //    string zplCommand = string.Empty;

        //    zplCommand = $@"^XA
        //                    ^BY2,2.0^FS
        //                    ^SEE:UHANGUL.DAT^FS
        //                    ^CW1,E:KFONT3.FNT^CI26^FS
        //                    ^FO35,20^GB250,90,4^FS
        //                    ^FO35,20^GB520,90,4^FS
        //                    ^FO35,20^GB750,90,4^FS
        //                    ^FO35,20^GB1050,90,4^FS
                            
        //                    ^FO35,20^GB250,180,4^FS
        //                    ^FO35,20^GB1050,180,4^FS
                            
        //                    ^FO35,20^GB250,270,4^FS
        //                    ^FO35,198^GB520,90,4^FS
        //                    ^FO35,198^GB750,90,4^FS
        //                    ^FO35,20^GB1050,270,4^FS

        //                    ^FO35,20^GB250,360,4^FS
        //                    ^FO35,287^GB520,90,4^FS
        //                    ^FO35,287^GB750,90,4^FS
        //                    ^FO35,20^GB1050,360,4^FS
                            
        //                    ^FO35,20^GB250,450,4^FS
        //                    ^FO35,376^GB750,90,4^FS
                            
        //                    ^FO35,20^GB250,540,4^FS
        //                    ^FO35,376^GB750,180,4^FS
        //                    ^FO35,20^GB1050,540,4^FS
                            
        //                    ^FO50,50^A1N,36,16^FD고객사^FS
        //                    ^FO300,50^A1N,36,16^FD ^FS
        //                    ^FO580,50^A1N,36,16^FDLOT No.^FS
        //                    ^FO850,50^A1N,36,16^FD{dr["LOT_NO"].ToString()}^FS
                            
        //                    ^FO50,140^A1N,36,16^FD품명^FS
        //                    ^FO290,140^A1N,36,16^FD{lbl_품목명.Text}^FS
                            
        //                    ^FO50,230^A1N,36,16^FD생산일자^FS
        //                    ^FO290,230^A1N,36,16^FD{dr["BARCODE_DATE"].ToString()}^FS
        //                    ^FO565,230^A1N,36,16^FD이동일자^FS
        //                    ^FO790,230^A1N,36,16^FD{dr["MOVE_DATE"].ToString()}^FS
                            
        //                    ^FO50,320^A1N,36,16^FD전공정^FS
        //                    ^FO290,320^A1N,36,16^FD주조^FS
        //                    ^FO560,320^A1N,36,16^FD재고기록표^FS
        //                    ^FO870,320^A1N,36,16^FD{dr["ID"].ToString()}^FS
                            
        //                    ^FO50,410^A1N,36,16^FD수량^FS
        //                    ^FO425,410^A1N,36,20^FD{dr["P_QTY"].ToString()}^FS
                            
        //                    ^FO50,500^A1N,36,16^FD품번^FS
        //                    ^FO300,500^A1N,36,16^FD{dr["RESOURCE_NO"].ToString()}^FS
                            
        //                    ^FO850,390^BON,7,7
                            
        //                    ^BQN,2,6^FDMM,B0024 {dr["BARCODE_NO"].ToString()}^F
                            
        //                    ^XZ";


        //    try
        //    {
        //        RawPrinterHelper.SendStringToPrinter(printerName, zplCommand);
        //       // _lblMessage.Text = "라벨 출력이 완료되었습니다.";
        //    }
        //    catch (Exception ex)
        //    {
        //        //_lblMessage.Text = $"ZPL 명령 전송 중 오류 발생: {ex.Message}";
        //    }
        //}

        private void button5_Click(object sender, EventArgs e)
        {
            prodBcdQty = "0";

            string sql = $@"SELECT                       
                           'False' as CK
                          ,A.*
						  ,B.DESCRIPTION  
                           FROM [HS_MES].[dbo].[PRODUCT_BARCODE] A
                            INNER JOIN [sea_mfg].[dbo].[resource] AS B
                             ON A.resource_no = B.resource_no
                           WHERE 1=1 
                           AND BARCODE_DATE BETWEEN '{base_FromtoDateTime1.StartValue.ToString("yyyy-MM-dd HH:mm:ss.fff")}' AND '{base_FromtoDateTime1.EndValue.ToString("yyyy-MM-dd HH:mm:ss.fff")}'
                         AND WORK_PERFORMANCE_ID = {_p실적} "; ;

                        /// INNER JOIN [sea_mfg].[dbo].[address] B ON A.VENDOR_NO = B.address_key";
                       //WHERE BARCODE_DATE >= '{DateTime.Now.ToString("yyyy-MM-dd")}'";

            DataTable dt = new MS_DBClass(utility.My_Settings_Get()).SELECT2(sql);

            Core.Function.Core.DisplayData_Set(dt, fpMain);

            int row;
            for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
            {
                if (fpMain.Sheets[0].GetValue(i, "WORK_PERFORMANCE_ID   ".Trim()).ToString().Trim() == _p실적 &&
                    fpMain.Sheets[0].GetValue(i, "LOT           ".Trim()).ToString().Trim() == _pLOT)
                {
                    row = i;
                    prodBcdQty = fpMain.Sheets[0].GetValue(row, "P_QTY   ".Trim()).ToString();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
            {
                if (fpMain.Sheets[0].GetValue(i, "CK".Trim()).ToString() == "True")
                {
                    공정이동표 라벨 = new 공정이동표();

                    라벨.LOT_NO           = fpMain.Sheets[0].GetValue(i, "LOT_NO      ".Trim()).ToString();
                    라벨.품목명           = fpMain.Sheets[0].GetValue(i, "DESCRIPTION ".Trim()).ToString();
                    라벨.BARCODE_DATE     = fpMain.Sheets[0].GetValue(i, "BARCODE_DATE".Trim()).ToString();
                    라벨.MOVE_DATE        = fpMain.Sheets[0].GetValue(i, "MOVE_DATE   ".Trim()).ToString();
                    라벨.ID               = fpMain.Sheets[0].GetValue(i, "ID          ".Trim()).ToString();
                    라벨.P_QTY            = fpMain.Sheets[0].GetValue(i, "P_QTY       ".Trim()).ToString();
                    라벨.RESOURCE_NO      = fpMain.Sheets[0].GetValue(i, "RESOURCE_NO ".Trim()).ToString();
                    라벨.BARCODE_NO       = fpMain.Sheets[0].GetValue(i, "BARCODE_NO  ".Trim()).ToString();

                    print(라벨);
                }
            }
        }

        private void print(공정이동표 라벨)
        {
            string printerName = "ZDesigner ZD230-203dpi ZPL";
            //string printerName = "SEC842519C27EA8(C56x Series)"; // 프린터 이름으로 변경하세요
            //string printerName = "ZDesigner GT800 (EPL)"; //세아 라벨 프린트
            string zplCommand = string.Empty;

            zplCommand = $@"^XA
                                ^SEE:UHANGUL.DAT^FS
                                ^CW1,E:KFONT3.FNT^CI26^FS


                                ^FO30,30^GB730,360,3^FS

                                ^FO170,30^GB3,360,3^FS
                                ^FO400,30^GB3,60,3^FS
                                ^FO500,30^GB3,60,3^FS

                                ^FO30,90^GB730,3,3^FS
                                ^FO30,150^GB730,3,3^FS
                                ^FO30,210^GB730,3,3^FS
                                ^FO30,270^GB730,3,3^FS
                                ^FO30,330^GB470,3,3^FS

                                ^FO400,150^GB3,120,3^FS
                                ^FO500,150^GB3,240,3^FS

                                 ^FO50,50^A1N,36,16^FD고객사^FS
                                 ^FO50,110^A1N,36,16^FD품명^FS
                                 ^FO50,170^A1N,36,16^FD생산일자^FS
                                 ^FO50,230^A1N,36,16^FD전공정^FS
                                 ^FO50,290^A1N,36,16^FD수량^FS
                                 ^FO50,350^A1N,36,16^FD품번^FS

                                 ^FO180,50^A1N,36,16^FD^FS
                                 ^FO180,110^A1N,36,16^FD{라벨.품목명}^FS
                                 ^FO180,170^A1N,36,16^FD{라벨.BARCODE_DATE}^FS
                                 ^FO180,230^A1N,36,16^FD주조^FS
                                 ^FO180,290^A1N,36,16^FD{라벨.P_QTY}^FS
                                 ^FO180,350^A1N,36,16^FD{라벨.RESOURCE_NO}^FS

                                 ^FO410,50^A1N,36,16^FDLOT NO.^FS
                                 ^FO410,170^A1N,36,16^FD이동일자^FS
                                 ^FO410,230^A1N,36,16^FD재고기록표^FS

                                 ^FO510,50^A1N,36,16^FD{라벨.LOT_NO}^FS
                                 ^FO510,170^A1N,36,16^FD{라벨.MOVE_DATE}^FS
                                 ^FO510,230^A1N,36,16^FD{라벨.ID}^FS



                                 ^FO580,290^BON,7,7
                            
                                 ^BXN,4,200^FDMM,B0024 {라벨.BARCODE_NO}^F
                            
                                 ^XZ";
            try
            {
                PrintServer printServer = new PrintServer();
                PrintQueue printQueue = new PrintQueue(printServer, printerName, PrintSystemDesiredAccess.AdministratePrinter);
                printQueue.Purge();

                RawPrinterHelper.SendStringToPrinter(printerName, zplCommand);

                RawPrinterHelper.SendStringToPrinter(printerName, zplCommand);
                // _lblMessage.Text = "라벨 출력이 완료되었습니다.";
            }
            catch (Exception ex)
            {
                //_lblMessage.Text = $"ZPL 명령 전송 중 오류 발생: {ex.Message}";
            }
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





