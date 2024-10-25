using FarPoint.Win.Spread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core.Popup
{
    public partial class FindSpread : Form
    {
        #region ○ 폼 이동

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void tspTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        #endregion

        FpSpread parentSpread = null;
        List<int> searchedCol = new List<int>();
        List<int> searchedRow = new List<int>();

        public FindSpread(FpSpread pfpSpread)
        {
            InitializeComponent();
            parentSpread = pfpSpread;
            this.CenterToScreen();
            //this.StartPosition = FormStartPosition.CenterParent;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchedCol = new List<int>();
            searchedRow = new List<int>();
            Search(0,0,true);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (searchedCol.Count > 0)
                Search(searchedRow.Last(), searchedCol.Last() + 1,true);
            else
                CustomMsg.ShowMessage("찾기를 먼저 진행해 주세요.", "안내");
        }

        private void btnbefore_Click(object sender, EventArgs e)
        {
            if (searchedCol.Count > 1)
            {
                searchedCol.RemoveAt(searchedCol.Count - 1);
                searchedRow.RemoveAt(searchedRow.Count - 1);
                //searchedRow.Remove(searchedRow.Count - 1);
                //searchedCol.Remove(searchedCol.Count - 1);

                Search(searchedRow.Last(), searchedCol.Last(),false);
            }
            else if (searchedCol.Count == 1)
            { }
            else
               CustomMsg.ShowMessage("찾기를 먼저 진행해 주세요.", "안내");
        }


        private void Search(int startrowindex, int startcolindex, bool click)
        {
            int iRow = -1;
            int iCol = -1;
            
            parentSpread.Search(0, txtSearch.Text, false, false, false, true,startrowindex,startcolindex,ref iRow, ref iCol);

            if (iRow > -1)
            {
                parentSpread.ActiveSheet.SetActiveCell(iRow, iCol, false);
               
                //parentSpread.ActiveSheet.Rows[iRow].BackColor = Color.Red;
                //activecell 로 이동, scroll 처리
                parentSpread.ShowRow(0, iRow, FarPoint.Win.Spread.VerticalPosition.Nearest);     //자동스크롤
                parentSpread.ShowColumn(0, iCol, FarPoint.Win.Spread.HorizontalPosition.Nearest);
                if (click)
                {
                    searchedCol.Add(iCol);
                    searchedRow.Add(iRow);
                }
            }
            else
                CustomMsg.ShowMessage("더 이상 찾는 결과가 없습니다.", "안내");
        }

        private void lbl_Click(object obj, EventArgs e)
        {
            Label pCmd = obj as Label;
            switch (pCmd.Text)
            {
                case "X":
                    this.Close();
                    break;
            }
        }

        private void txtBox_KeyDown(object obj, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (searchedRow.Count==0)
                    btnSearch.PerformClick();
                else
                    btnNext.PerformClick();
            }
        }

      
    }
}
