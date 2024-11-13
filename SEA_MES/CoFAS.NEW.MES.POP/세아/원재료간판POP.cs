
using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.POP.Function;
using FarPoint.Excel;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP
{
    public partial class 원재료간판POP : Form
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
        #endregion

        #region ○ 생성자

        public 원재료간판POP(UserEntity pUserEntity)
        {
            InitializeComponent();
            _UserEntity = pUserEntity;


            _MY = utility.My_Settings_Get();

            Load += new EventHandler(Form_Load);

            this.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE, FontStyle.Bold);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
        }
        public 원재료간판POP()
        {
            InitializeComponent();



            _MY = utility.My_Settings_Get();

            Load += new EventHandler(Form_Load);

            this.Font = new Font(_MY.FONT_TYPE, _MY.FONT_SIZE, FontStyle.Bold);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
        }
        #endregion



        private void Form_Load(object sender, EventArgs e)
        {
            try
            {

                _품목콤보.AddValue(new CoreBusiness().Spread_ComboBox("RESOURCE_품목", "", ""), 0, 0, "", true);
                // _고객사.AddValue(new CoreBusiness().Spread_ComboBox("세아_거래처", "", ""), 0, 0, "", true);
                _품목콤보.ValueChanged += _품목콤보_EditValueChanged;

                txt_총중량.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
                txt_번들수.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
                txt_lot.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
                txt_비고.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
                _품목콤보.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
                _고객사.Font = new Font("맑은 고딕", 15, FontStyle.Bold);

                DataTable pDataTable1 = new CoreBusiness().BASE_MENU_SETTING_R10(this.Name, fpMain, "MATERIAL_BARCODE");

                if (pDataTable1 != null && pDataTable1.Rows.Count != 0)
                {
                    CoFAS.NEW.MES.Core.Function.Core.initializeSpread(pDataTable1, fpMain, this.Name, _UserEntity.user_account);
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
        private void _품목콤보_EditValueChanged(object pSender, EventArgs pEventArgs)
        {
            try
            {

                _고객사.AddValue(new CoreBusiness().Spread_ComboBox("RESOURCE_고객사", _품목콤보.Text, ""), 0, 0, "", true);

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            if (CustomMsg.ShowMessage("프로그램을 종료 하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();

                Application.Exit();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                int 총중량 = 0;
                int 번들수 = 0;

                string getLotNo = $@"SELECT A.LOT_NO                        
                                       FROM dbo.MATERIAL_BARCODE A
                                      INNER JOIN [sea_mfg].[dbo].[address] B ON A.VENDOR_NO = B.address_key
                                      WHERE A.LOT_NO = '{txt_lot.Text.ToString()}'
                                       ";

                DataTable dtLotNo = new MS_DBClass(utility.My_Settings_Get()).SELECT2(getLotNo);

                if (_품목콤보.GetValue() == "")
                {
                    CustomMsg.ShowMessage("품목명을 확인해 주세요.");
                    return;
                }

                if (_고객사.GetValue() == "")
                {
                    CustomMsg.ShowMessage("고객사를 확인해 주세요.");
                    return;
                }

                if (!int.TryParse(txt_총중량.Text, out 총중량))
                {
                    CustomMsg.ShowMessage("총중량을 확인해 주세요.");
                    return;
                }
                if (!int.TryParse(txt_번들수.Text, out 번들수))
                {
                    CustomMsg.ShowMessage("번들수을 확인해 주세요.");
                    return;
                }

                string msg = $"{번들수} 개 출력 하시겠습니까?";
                if (CustomMsg.ShowMessage(msg, "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    DataTable dt = new MS_DBClass(utility.My_Settings_Get()).USP_MaterialBarcode_A10(
                     _품목콤보.Text
                    , _고객사.GetValue().ToString()
                    , Convert.ToDecimal(txt_총중량.Text)
                    , Convert.ToInt32(txt_번들수.Text)//SplitQty (중량 분할)
                    , txt_lot.Text
                    , txt_비고.Text
                    );

                    if (dt != null)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            원재료간판라벨 라벨 = new 원재료간판라벨();

                            라벨.ResourceNo = item["ResourceNo  ".Trim()].ToString();
                            라벨.Vendor_No = item["name        ".Trim()].ToString();
                            라벨.BarcodeCount = item["BarcodeCount".Trim()].ToString();
                            라벨.LOT_NO = item["LOT_NO      ".Trim()].ToString();
                            라벨.COMMENT = item["COMMENT     ".Trim()].ToString();
                            라벨.LabelNo = item["LabelNo     ".Trim()].ToString();
                            라벨.BarcodeNo = item["BarcodeNo   ".Trim()].ToString();

                            print(라벨);
                        }
                        CustomMsg.ShowMessage("바코드 출력이 완료되었습니다.");
                        조회버튼_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMsg.ShowMessage($"ZPL 명령 전송 중 오류 발생: {ex.Message}");
            }
        }

        private void print(원재료간판라벨 라벨)
        {
            string printerName = "ZDesigner ZD230-203dpi ZPL";
            //string printerName = "SEC842519C27EA8(C56x Series)"; // 프린터 이름으로 변경하세요
            //string printerName = "ZDesigner GT800 (EPL)"; //세아 라벨 프린트
            string zplCommand = string.Empty;

            zplCommand = $@"^XA^BY2,2.0^FS^SEE:UHANGUL.DAT^FS^CW1,E:KFONT3.FNT^CI26^FS 
                            ^FO50,20^GB770,70,4^FS
                            ^FO50,20^GB770,140,4^FS
                            ^FO50,20^GB770,210,4^FS
                            ^FO50,20^GB770,270,4^FS
                            ^FO50,20^GB770,350,4^FS
                            ^FO50,20^GB150,270,4^FS
                            ^FO50,20^GB525,70,4^FS
                            ^FO570,225^GB0,65,4^FS
                            ^FO50,158^GB280,70,4^FS
                            ^FO50,158^GB350,70,4^FS
                            ^FO50,158^GB525,70,4^FS
                            ^FS^FO55,40^A1N,35,20^FD품번 : 
                            ^FS^FO55,110^A1N,35,20^FD거래처 : 
                            ^FS^FO55,180^A1N,35,15^FD중량 : 
                            ^FS^FO55,245^A1N,35,15^FDLOT : 
                            ^FS^FO400,40^A1N,36,20^FD{라벨.ResourceNo}
                            ^FS^FO400,110^A1N,36,20^FD{라벨.Vendor_No}
                            ^FS^FO210,180^A1N,36,20^FD{라벨.BarcodeCount}
                            ^FS^FO210,245^A1N,36,20^FD{라벨.LOT_NO}
                            ^FS^FO600,245^A1N,36,20^FD{라벨.COMMENT}
                            ^FS^FO335,180^ADN,36,20^FDKg
                            ^FS^FO400,180^A1N,36,20^FD발행일
                            ^FS^FO600,180^A1N,36,20^FD{DateTime.Now.ToString("yyyy-MM-dd")}
                            ^FS^FO600,40^A1N,36,20^FD{라벨.LabelNo}
                         

                            ^FS^BY2,3,50^FT 80, 350
                            ^BCN,40,N,N,Y^FD{라벨.BarcodeNo}^FS^XZ";


            try
            {
                PrintServer printServer = new PrintServer();
                PrintQueue printQueue = new PrintQueue(printServer, printerName, PrintSystemDesiredAccess.AdministratePrinter);
                printQueue.Purge();

                RawPrinterHelper.SendStringToPrinter(printerName, zplCommand);
                //RawPrinterHelper.SendStringToPrinter(printerName, zplCommand);
                // _lblMessage.Text = "라벨 출력이 완료되었습니다.";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                CustomMsg.ShowMessage($"ZPL 명령 전송 중 오류 발생: {ex.Message}");
            }
        }

        private void 조회버튼_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = $@"SELECT                         
                          'False' as CK
                          ,RESOURCE_NO
                          ,NAME AS VENDOR_NO
                          ,LOT_NO
                          ,BARCODE_DATE
                          ,RESOURCE_WEIGHT
                          ,SPLIT_QTY
                          ,BARCODE_NO
                          ,COMMENT
                       FROM [HS_MES].[dbo].[MATERIAL_BARCODE] A
                       INNER JOIN [sea_mfg].[dbo].[address] B ON A.VENDOR_NO = B.address_key
                       WHERE BARCODE_DATE BETWEEN '{base_FromtoDateTime1.StartValue.ToString("yyyy-MM-dd HH:mm:ss.fff")}' AND '{base_FromtoDateTime1.EndValue.ToString("yyyy-MM-dd HH:mm:ss.fff")}'";

                DataTable dt = new MS_DBClass(utility.My_Settings_Get()).SELECT2(sql);

                CoFAS.NEW.MES.Core.Function.Core.DisplayData_Set(dt, fpMain);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void 닫기_Click(object sender, EventArgs e)
        {
            try
            {
                if (CustomMsg.ShowMessage("프로그램을 종료 하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Close();

                    Application.Exit();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btn_출력_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                {
                    if (fpMain.Sheets[0].GetValue(i, "CK".Trim()).ToString() == "True")
                    {
                        원재료간판라벨 라벨 = new 원재료간판라벨();

                        라벨.ResourceNo = fpMain.Sheets[0].GetValue(i, "RESOURCE_NO  ".Trim()).ToString();
                        라벨.Vendor_No = fpMain.Sheets[0].GetValue(i, "VENDOR_NO    ".Trim()).ToString();
                        라벨.BarcodeCount = fpMain.Sheets[0].GetValue(i, "SPLIT_QTY    ".Trim()).ToString();
                        라벨.LOT_NO = fpMain.Sheets[0].GetValue(i, "LOT_NO       ".Trim()).ToString();
                        라벨.COMMENT = fpMain.Sheets[0].GetValue(i, "COMMENT      ".Trim()).ToString();
                        라벨.BarcodeNo = fpMain.Sheets[0].GetValue(i, "BARCODE_NO   ".Trim()).ToString();
                        라벨.LabelNo = fpMain.Sheets[0].GetValue(i, "BARCODE_NO   ".Trim()).ToString().Substring(라벨.BarcodeNo.Length - 3, 3);
                        print(라벨);
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string urlstring = $@"^XA^BY2,2.0^FS^SEE:UHANGUL.DAT^FS^CW1,E:KFONT3.FNT^CI26^FS 
                            ^FO50,20^GB770,70,4^FS
                            ^FO50,20^GB770,140,4^FS
                            ^FO50,20^GB770,210,4^FS
                            ^FO50,20^GB770,270,4^FS
                            ^FO50,20^GB770,350,4^FS
                            ^FO50,20^GB150,270,4^FS
                            ^FO50,20^GB525,70,4^FS
                            ^FO570,225^GB0,65,4^FS
                            ^FO50,158^GB280,70,4^FS
                            ^FO50,158^GB350,70,4^FS
                            ^FO50,158^GB525,70,4^FS
                            ^FS^FO55,40^A1N,35,20^FD품번 : 
                            ^FS^FO55,110^A1N,35,20^FD거래처 : 
                            ^FS^FO55,180^A1N,35,15^FD중량 : 
                            ^FS^FO55,245^A1N,35,15^FDLOT : 
                            ^FS^FO400,40^A1N,36,20^FD{""}
                            ^FS^FO400,110^A1N,36,20^FD{""}
                            ^FS^FO210,180^A1N,36,20^FD{""}
                            ^FS^FO210,245^A1N,36,20^FD{""}
                            ^FS^FO600,245^A1N,36,20^FD{""}
                            ^FS^FO335,180^ADN,36,20^FDKg
                            ^FS^FO400,180^A1N,36,20^FD발행일
                            ^FS^FO600,180^A1N,36,20^FD{DateTime.Now.ToString("yyyy-MM-dd")}

                            ^FS^FO600,40^A1N,36,20^FD{""}
                         

                            ^FS^BY2,3,50^FT 80, 350
                            ^BCN,40,N,N,Y^FD{""}^FS^XZ";

            Bitmap bmp = new CoFAS_Label().WebImageView(urlstring); // 이미지 가져오기

            pictureBox1.Image = bmp; // 이미지 적용
        }
    }
    public class 원재료간판라벨
    {

        public string ResourceNo { get; set; }
        public string Vendor_No { get; set; }
        public string BarcodeCount { get; set; }

        public string LOT_NO { get; set; }

        public string COMMENT { get; set; }

        public string LabelNo { get; set; }

        public string BarcodeNo { get; set; }
    }
}
