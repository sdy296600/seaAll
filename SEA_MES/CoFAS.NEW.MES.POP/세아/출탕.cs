
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
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP
{
    public partial class 출탕 : Form
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

        string command = "";
        public string _pLOT  = string.Empty;

        public string _p품번 = string.Empty;
        public string _p실적 = string.Empty;

        #endregion

        #region ○ 생성자

        public 출탕(UserEntity pUserEntity
            , string p용탕
            , string p로드셀
            , string p품목코드
            , string p품목명
            )
        {
            InitializeComponent();

            _UserEntity = pUserEntity;

            lbl_총중량_용탕.Text = p용탕;
            lbl_총중량_로드셀.Text = p로드셀;

            lbl_용탕_품목코드.Text = p품목코드;
            lbl_용탕_품목명.Text = p품목명;

            lbl_로드셀_품목코드.Text = p품목코드;
            lbl_로드셀_품목명.Text = p품목명;


            _MY = utility.My_Settings_Get();

            Load += new EventHandler(Form_Load);

            this.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE, FontStyle.Bold);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
        }
        public 출탕()
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

                //_품목콤보_용탕.AddValue(new CoreBusiness().Spread_ComboBox("RESOURCE_품목", "", ""), 0, 0, "", true);
                //_품목콤보_로드셀.AddValue(new CoreBusiness().Spread_ComboBox("RESOURCE_품목", "", ""), 0, 0, "", true);
                _고객사콤보_로드셀.AddValue(new CoreBusiness().Spread_ComboBox("세아_거래처", "", ""), 0, 0, "", true);


                lbl_총중량_용탕.Font = new Font("맑은 고딕", 18, FontStyle.Bold);
                txt_번들수_용탕.Font = new Font("맑은 고딕", 18, FontStyle.Bold);
                txt_lot_용탕.Font = new Font("맑은 고딕", 18, FontStyle.Bold);
                _비고_용탕.Font = new Font("맑은 고딕", 18, FontStyle.Bold);

                lbl_용탕_품목코드.Font = new Font("맑은 고딕", 18, FontStyle.Bold);
                lbl_용탕_품목명.Font = new Font("맑은 고딕", 18, FontStyle.Bold);
                lbl_로드셀_품목코드.Font = new Font("맑은 고딕", 18, FontStyle.Bold);
                lbl_로드셀_품목명.Font = new Font("맑은 고딕", 18, FontStyle.Bold);

                _고객사콤보_로드셀.Font = new Font("맑은 고딕", 18, FontStyle.Bold);
                _LOT.Font = new Font("맑은 고딕", 18, FontStyle.Bold);
                lbl_총중량_로드셀.Font = new Font("맑은 고딕", 18, FontStyle.Bold);
                _비고_로드셀.Font = new Font("맑은 고딕", 18, FontStyle.Bold);


                DataTable pDataTable1 =  new CoreBusiness().BASE_MENU_SETTING_R10(this.Name,fpMain,"MATERIAL_BARCODE");

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

        #endregion



        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btn_생성_Click(object sender, EventArgs e)
        {
            if (ck_용탕_Yes.Checked == true)
            {
                int 총중량 = 0;
                int 번들수 = 0;

                if (!int.TryParse(lbl_총중량_용탕.Text, out 총중량))
                {
                    return;
                }
                if (!int.TryParse(txt_번들수_용탕.Text, out 번들수))
                {
                    return;
                }

                string msg = $"{번들수} 개 출력 하시겠습니까?";
                if (CustomMsg.ShowMessage(msg, "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {


                    DataTable dt = new MS_DBClass(utility.My_Settings_Get()).USP_MaterialBarcode_A10(
                 lbl_용탕_품목코드.Text
                ,lbl_용탕.Text
                ,lbl_총중량_용탕.Text
                ,txt_번들수_용탕.Text
                ,txt_lot_용탕.Text
                ,_비고_용탕.Text
                ); ;
                    if (dt.Rows.Count != 0)
                    {

                        foreach (DataRow item in dt.Rows)
                        {
                            string sql = $@"INSERT INTO [dbo].[IN_BARCODE] 
                                    ([WORK_PERFORMANCE_ID]
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
                                    ,'용탕_출탕'
                                    ,GETDATE()
                                    ,'{_p품번}'
                                    ,'{_pLOT}'
                                    ,'{item["BarcodeNo"].ToString()}'
                                    ,'{Convert.ToInt32(item["BarcodeCount"].ToString())*-1}'
                                    ,GETDATE()); ";

                            new CoreBusiness().SELECT(sql);
                           
                            출탕_CLASS 라벨 = new 출탕_CLASS();
                            라벨.ResourceNo = item["ResourceNo"].ToString();
                            라벨.name = item["name"].ToString();
                            라벨.BarcodeCount = item["BarcodeCount"].ToString();
                            라벨.LOT_NO = item["LOT_NO"].ToString();
                            라벨.COMMENT = item["COMMENT"].ToString();
                            라벨.BarcodeNo = item["BarcodeNo"].ToString();
                            라벨.LabelNo = item["LabelNo"].ToString();

                            print(라벨);


                        }

                    }

                }
            }
            if (ck_로드셀_Yes.Checked == true)
            {

                if (_고객사콤보_로드셀.GetValue() == "")
                {
                    return;
                }

                string msg = $"로드셀 바코드 출력 하시겠습니까?";
                if (CustomMsg.ShowMessage(msg, "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {


                    DataTable dt = new MS_DBClass(utility.My_Settings_Get()).USP_MaterialBarcode_A10(
                lbl_로드셀_품목코드.Text
                ,_고객사콤보_로드셀.GetValue()
                ,lbl_총중량_로드셀.Text
                ,"1"
                ,_LOT.Text
                ,_비고_로드셀.Text
                ); ;
                    if (dt.Rows.Count != 0)
                    {

                        foreach (DataRow item in dt.Rows)
                        {
                            string sql = $@"INSERT INTO [dbo].[IN_BARCODE] 
                                    ([WORK_PERFORMANCE_ID]
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
                                    ,'로드셀_출탕'
                                    ,GETDATE()
                                    ,'{_p품번}'
                                    ,'{_pLOT}'
                                    ,'{item["BarcodeNo"].ToString()}'
                                    ,'{Convert.ToInt32(item["BarcodeCount"].ToString())*-1}'
                                    ,GETDATE()); ";

                            new CoreBusiness().SELECT(sql);

                            출탕_CLASS 라벨 = new 출탕_CLASS();
                            라벨.ResourceNo = item["ResourceNo"].ToString();
                            라벨.name = item["name"].ToString();
                            라벨.BarcodeCount = item["BarcodeCount"].ToString();
                            라벨.LOT_NO = item["LOT_NO"].ToString();
                            라벨.COMMENT = item["COMMENT"].ToString();
                            라벨.BarcodeNo = item["BarcodeNo"].ToString();
                            라벨.LabelNo = item["LabelNo"].ToString();

                            print(라벨);



                        }

                    }

                }
            }
        }

        private void print(출탕_CLASS 라벨)
        {
            string printerName = "ZDesigner GT800 (EPL)"; // 프린터 이름으로 변경하세요
            string zplCommand = string.Empty;

            zplCommand = $@"^XA^BY2,2.0^FS^SEE:UHANGUL.DAT^FS^CW1,E:KFONT3.FNT^CI26^FS 
                            ^FO50,20^GB800,70,4^FS
                            ^FO50,20^GB800,140,4^FS
                            ^FO50,20^GB800,210,4^FS
                            ^FO50,20^GB800,270,4^FS
                            ^FO50,20^GB800,350,4^FS
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
                            ^FS^FO400,110^A1N,36,20^FD{라벨.name}
                            ^FS^FO210,180^A1N,36,20^FD{라벨.BarcodeCount}
                            ^FS^FO210,245^A1N,36,20^FD{라벨.LOT_NO}
                            ^FS^FO600,245^A1N,36,20^FD{라벨.COMMENT}
                            ^FS^FO335,180^ADN,36,20^FDKg
                            ^FS^FO400,180^A1N,36,20^FD발행일
                            ^FS^FO600,180^A1N,36,20^FD{DateTime.Now.ToString("yyyy-MM-dd")}

                            ^FS^FO600,40^A1N,36,20^FD{라벨.LabelNo}
                         
                            ^FS^BY2,3,50^FT 80, 350
                            ^BCN,40,N,N,Y^FD{라벨.BarcodeNo}^FS^XZ";

            command = zplCommand;


            try
            {
                RawPrinterHelper.SendStringToPrinter(printerName, zplCommand);

                RawPrinterHelper.SendStringToPrinter(printerName, zplCommand);
                // _lblMessage.Text = "라벨 출력이 완료되었습니다.";
            }
            catch (Exception ex)
            {
                //_lblMessage.Text = $"ZPL 명령 전송 중 오류 발생: {ex.Message}";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sql = $@"SELECT                         
                          'False' as CK
                          ,RESOURCE_NO
                          ,VENDOR_NO 
                          ,LOT_NO
                          ,BARCODE_DATE
                          ,RESOURCE_WEIGHT
                          ,SPLIT_QTY
                          ,BARCODE_NO
                          ,COMMENT
                       FROM [HS_MES].[dbo].[MATERIAL_BARCODE] A
                      WHERE  VENDOR_NO LIKE '%출탕%'";
            //WHERE BARCODE_DATE >= '{DateTime.Now.ToString("yyyy-MM-dd")}'";

            DataTable dt = new MS_DBClass(utility.My_Settings_Get()).SELECT2(sql);

            CoFAS.NEW.MES.Core.Function.Core.DisplayData_Set(dt, fpMain);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                command = "test";

                if (!string.IsNullOrEmpty(command))
                {
                    string urlstring = $@"
                            ^XA^BY2,2.0^FS^SEE:UHANGUL.DAT^FS^CW1,E:KFONT3.FNT^CI26^FS 
                            ^FO50,20^GB800,70,4^FS
                            ^FO50,20^GB800,140,4^FS
                            ^FO50,20^GB800,210,4^FS
                            ^FO50,20^GB800,270,4^FS
                            ^FO50,20^GB800,350,4^FS
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
        }

        private void btn_닫기_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_출력_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
            {
                if (fpMain.Sheets[0].GetValue(i, "CK".Trim()).ToString() == "True")
                {
                    출탕_CLASS 라벨 = new 출탕_CLASS();

                    라벨.ResourceNo = fpMain.Sheets[0].GetValue(i, "RESOURCE_NO  ".Trim()).ToString();
                    라벨.name = fpMain.Sheets[0].GetValue(i, "VENDOR_NO    ".Trim()).ToString();
                    라벨.BarcodeCount = fpMain.Sheets[0].GetValue(i, "SPLIT_QTY    ".Trim()).ToString();
                    라벨.LOT_NO = fpMain.Sheets[0].GetValue(i, "LOT_NO       ".Trim()).ToString();
                    라벨.COMMENT = fpMain.Sheets[0].GetValue(i, "COMMENT      ".Trim()).ToString();
                    라벨.BarcodeNo = fpMain.Sheets[0].GetValue(i, "BARCODE_NO   ".Trim()).ToString();
                    라벨.LabelNo = fpMain.Sheets[0].GetValue(i, "BARCODE_NO   ".Trim()).ToString().Substring(라벨.BarcodeNo.Length - 3, 3);
                    print(라벨);
                }
            }
        }
    }

    public class 출탕_CLASS
    {
        public string ResourceNo { get; set; }
        public string name { get; set; }
        public string BarcodeCount { get; set; }
        public string LOT_NO { get; set; }
        public string COMMENT { get; set; }
        public string LabelNo { get; set; }
        public string BarcodeNo { get; set; }

    }

}





