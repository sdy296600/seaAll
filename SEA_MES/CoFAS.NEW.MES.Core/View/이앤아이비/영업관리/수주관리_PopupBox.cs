using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using DevExpress.Spreadsheet;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 수주관리_PopupBox : Form
    {
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
            }
        }

        #endregion

        Loading waitform = new Loading();
        public string _UserAccount = string.Empty;
        public string _UserAuthority = string.Empty;
        public string _pFileName = string.Empty;

        #region ○ 생성자

        public 수주관리_PopupBox(string pFileName)
        {

            InitializeComponent();

            _pFileName = pFileName;

            Load += new EventHandler(Form_Load);
        }

        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                waitform.Show(this, "준비중입니다.");
                fpMain._ChangeEventHandler += FpMain_Change;


                DataTable pDataTable =  new CoreBusiness().BASE_MENU_SETTING_R10(this.Name,fpMain,"");
                if (pDataTable != null)
                {
                    //InitializeControl(pDataTable);

                    Function.Core.initializeSpread(pDataTable, fpMain, this.Name, _UserAccount);
               
                }

              

                DevExpress.XtraSpreadsheet.SpreadsheetControl sc = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                sc.LoadDocument(_pFileName, DocumentFormat.Xlsx);
                List<이앤아이비수주> list = new List<이앤아이비수주>();
                foreach (Worksheet sheet in sc.Document.Worksheets)
                {
        



                    for (int i = 1; i < 10000; i++)
                    {
                        #region ○ 변수 정리

                        DateTime dateTime = DateTime.Now;
                        if (!DateTime.TryParse(sheet.Rows[i][2].Value.ToString().Trim(), out dateTime))
                        {
                            continue;
                        }

                        이앤아이비수주 수주 = new 이앤아이비수주();

                        수주.구분 = sheet.Rows[i][1].Value.ToString().Trim();
                        수주.발주일자 = DateTime.Parse(sheet.Rows[i][2].Value.ToString().Trim());

                        if (DateTime.TryParse(sheet.Rows[i][3].Value.ToString().Trim(), out dateTime))
                        {
                            수주.납기일자 = dateTime;
                        }
                        else
                        {
                            수주.납기일자 = DateTime.Now;
                        }
                        수주.고객사 = sheet.Rows[i][4].Value.ToString().Trim();
                        수주.발주번호 = sheet.Rows[i][5].Value.ToString().Trim();
                        수주.프로젝트명 = sheet.Rows[i][6].Value.ToString().Trim();
                        //수주.STOCK_MST_TYPE2 = sheet.Rows[i][7].Value.ToString().Trim();
                        수주.STOCK_MST_OUT_CODE = sheet.Rows[i][8].Value.ToString().Trim();
                        //수주.STOCK_MST_ID = sheet.Rows[i][9].Value.ToString().Trim();
                        //수주.STOCK_MST_STANDARD = sheet.Rows[i][10].Value.ToString().Trim();
                        수주.단위 = sheet.Rows[i][11].Value.ToString().Trim();
                        decimal dec = 0;

                        if (decimal.TryParse(sheet.Rows[i][12].Value.ToString().Trim(), out dec))
                        {
                            수주.수량 = dec;
                        }
                        else
                        {
                            수주.수량 = 0;
                        }

                        if (decimal.TryParse(sheet.Rows[i][13].Value.ToString().Trim(), out dec))
                        {
                            수주.단가 = dec;
                        }
                        else
                        {
                            수주.단가 = 0;
                        }
                        if (decimal.TryParse(sheet.Rows[i][14].Value.ToString().Trim(), out dec))
                        {
                            수주.합계 = dec;
                        }
                        else
                        {
                            수주.합계 = 0;
                        }

                        수주.진행상황 = sheet.Rows[i][16].Value.ToString().Trim();
                        if (decimal.TryParse(sheet.Rows[i][17].Value.ToString().Trim(), out dec))
                        {
                            수주.공급가액1 = dec;
                        }
                        else
                        {
                            수주.공급가액1 = 0;
                        }
                        if (sheet.Rows[i][18].Value.ToString().Trim() == "")
                        {
                            수주.공급가액2 = 0;
                        }
                        else
                        {
                            수주.공급가액2 = decimal.Parse(sheet.Rows[i][18].Value.ToString().Trim());
                        }
                        list.Add(수주);

                        #endregion
                    }
                }





                fpMain.Sheets[0].Visible = false;
                fpMain.Sheets[0].Rows.Count = list.Count;
                //MyCore._AddItemButtonClicked(fpMain, _UserAccount);
                for (int i = 0; i < list.Count; i++)
                {

                    foreach (PropertyInfo info in new 이앤아이비수주().GetType().GetProperties())
                    {
                        if (fpMain.Sheets[0].Columns[info.Name].CellType != null)
                            {
                            if (fpMain.Sheets[0].Columns[info.Name].CellType.GetType() == typeof(MYComboBoxCellType))
                            {
                                fpMain.Sheets[0].SetText(i, fpMain.Sheets[0].Columns[info.Name].Index, new 이앤아이비수주().GetType().GetProperty(info.Name).GetValue(list[i]).ToString());
                            }
                            else if (fpMain.Sheets[0].Columns[info.Name].CellType.GetType() == typeof(NEW_ComboBoxCellType))
                            {
                                fpMain.Sheets[0].SetText(i, fpMain.Sheets[0].Columns[info.Name].Index, new 이앤아이비수주().GetType().GetProperty(info.Name).GetValue(list[i]).ToString());
                            }
                            else
                            {
                                fpMain.Sheets[0].SetValue(i, info.Name, new 이앤아이비수주().GetType().GetProperty(info.Name).GetValue(list[i]));
                            }
                        }
                    }

                    fpMain.Sheets[0].RowHeader.Cells[i, 0].Text = "입력";
                    fpMain.Sheets[0].SetValue(i, "USE_YN", "Y");
                    fpMain.Sheets[0].SetValue(i, "UP_USER", _UserAccount);
                    fpMain.Sheets[0].SetValue(i, "UP_DATE", DateTime.Now);
                    fpMain.Sheets[0].SetValue(i, "REG_USER", _UserAccount);
                    fpMain.Sheets[0].SetValue(i, "REG_DATE", DateTime.Now);

                    string id =  fpMain.Sheets[0].GetValue(i,"STOCK_MST_OUT_CODE").ToString();
                    string company = fpMain.Sheets[0].GetValue(i, "고객사").ToString();
                    fpMain.Sheets[0].SetValue(i, "STOCK_MST_ID      ".Trim(), id);
                    fpMain.Sheets[0].SetValue(i, "STOCK_MST_STANDARD".Trim(), id);
                    fpMain.Sheets[0].SetValue(i, "STOCK_MST_TYPE2   ".Trim(), id);
                    //fpMain.Sheets[0].SetValue(i, "STOCK_MST_UNIT   ".Trim(), id);

                    if (fpMain.Sheets[0].GetValue(i, "프로젝트명").ToString() == "")
                    {
                        fpMain.Sheets[0].Rows[i].BackColor = Color.Coral;
                    }

                    if (fpMain.Sheets[0].GetValue(i, "발주번호").ToString() == "")
                    {
                        fpMain.Sheets[0].Rows[i].BackColor = Color.Coral;
                    }

                    if (id == "")
                    {
                        fpMain.Sheets[0].Rows[i].BackColor = Color.Coral;
                    }

                    if (company == "")
                    {
                        fpMain.Sheets[0].Rows[i].BackColor = Color.Coral;
                    }

                }
                fpMain.Sheets[0].Visible = true;


                waitform.Close();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region ○ 초기화 영역


     

        private void FpMain_Change(object sender, ChangeEventArgs e)
        {
            try
            {

                string pHeaderLabel = fpMain.Sheets[0].RowHeader.Cells[e.Row, 0].Text;
                switch (pHeaderLabel)
                {
                    case "":
                        fpMain.Sheets[0].RowHeader.Cells[e.Row, 0].Text = "수정";
                        break;
                    case "입력":
                        break;
                    case "수정":
                        break;
                    case "삭제":
                        break;
                }

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #endregion


        #region ○ 기타이벤트

        #endregion

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DevExpressManager.SetCursor(this, Cursors.WaitCursor);

            if (Function.Core._SaveButtonClicked(fpMain))
            {
              
                if (fpMain.Sheets[0].Rows.Count > 0)

                    fpMain.Focus();
                waitform.Show(this, "저장중입니다.");
                bool _Error = new SI_Business().수주관리_PopupBox(fpMain);

                waitform.Close();
                if (!_Error)
                {
                    CustomMsg.ShowMessage("저장되었습니다.");

                    this.DialogResult = DialogResult.OK;
                    this.Close();

                }
            }


        }

        private void btn_미입력제거_Click(object sender, EventArgs e)
        {
            for (int i = fpMain.Sheets[0].RowCount-1; i >= 0; i--)
            {
                if (fpMain.Sheets[0].Rows[i].BackColor == Color.Coral)
                {
                    //MessageBox.Show((i + del).ToString());

                    FpSpreadManager.SpreadRowRemove(fpMain, 0, (i));
                }
            }
        }
    }
}



