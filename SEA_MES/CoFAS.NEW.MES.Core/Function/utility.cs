using CoFAS.NEW.MES.Core.Popup;
using CoFAS.NEW.MES.Core.Properties;
using FarPoint.Win.Spread.CellType;

using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;


namespace CoFAS.NEW.MES.Core.Function
{
    public class utility
    {
        public void ContextMenuStrip_Setting(xFpSpread pfpSpread)
        {
            try
            {
                ContextMenuStrip m = new ContextMenuStrip();

                ToolStripMenuItem mVisible = new ToolStripMenuItem("숨김");
                ToolStripMenuItem mfilter = new ToolStripMenuItem("필터");
                ToolStripMenuItem mfix = new ToolStripMenuItem("고정");

                for (int i = 0; i < pfpSpread.Sheets[0].ColumnCount; i++)
                {
                    if (pfpSpread.Sheets[0].Columns[i].Visible)
                    {
                        string name = pfpSpread.Sheets[0].Columns[i].Label;
                        if (pfpSpread.Sheets[0].Columns[i].CellType != null)
                        {
                            if (!name.Contains("*") &&
                                pfpSpread.Sheets[0].Columns[i].CellType.ToString() != "CheckBoxCellType"
                                )
                            {
                                ToolStripMenuItem visible = new ToolStripMenuItem(name);

                                visible.Name = pfpSpread.Sheets[0].Columns[i].Tag.ToString();
                                visible.Click += new EventHandler(delegate
                                {
                                    try
                                    {
                                        if (pfpSpread.Sheets[0].Columns[visible.Name].Visible)
                                        {
                                            pfpSpread.Sheets[0].Columns[visible.Name].Visible = false;
                                            visible.Image = Resources.btn_spread_check_01;
                                        }
                                        else
                                        {
                                            pfpSpread.Sheets[0].Columns[visible.Name].Visible = true;
                                            visible.Image = null;
                                        }
                                        //new MenuSave_Business().MenuSave_A10(pfpSpread);
                                    }
                                    catch (Exception err)
                                    {

                                    }
                                });

                                mVisible.DropDownItems.Add(visible);
                                mVisible.Image = Resources.hide;
                            }
                            ToolStripMenuItem filter = new ToolStripMenuItem(name);
                            filter.Name = pfpSpread.Sheets[0].Columns[i].Tag.ToString();
                            filter.Click += new EventHandler(delegate
                            {
                                try
                                {
                                    if (pfpSpread.Sheets[0].Columns[filter.Name].AllowAutoFilter)
                                    {
                                        pfpSpread.Sheets[0].Columns[filter.Name].AllowAutoFilter = false;
                                        filter.Image = null;
                                    }
                                    else
                                    {
                                        pfpSpread.Sheets[0].Columns[filter.Name].AllowAutoFilter = true;
                                        filter.Image = Resources.btn_spread_check_01;
                                    }
                                    //new MenuSave_Business().MenuSave_A10(pfpSpread);

                                }
                                catch (Exception err)
                                {

                                }
                            });
                            mfilter.DropDownItems.Add(filter);
                            mfilter.Image = Resources.filter_16;

                            ToolStripMenuItem fix = new ToolStripMenuItem(name);


                            fix.Name = pfpSpread.Sheets[0].Columns[i].Tag.ToString();
                            fix.Click += new EventHandler(delegate

                            {
                                try
                                {
                                    for (int k = 0; k < mfix.DropDownItems.Count; k++)
                                    {
                                        if (mfix.DropDownItems[k].Image != null)
                                        {
                                            mfix.DropDownItems[k].Image = null;
                                        }
                                    }

                                    if (pfpSpread.ActiveSheet.FrozenColumnCount != pfpSpread.ActiveSheet.Columns[fix.Name].Index + 1)
                                    {
                                        fix.Image = Resources.btn_spread_check_01;
                                        pfpSpread.ActiveSheet.FrozenColumnCount = pfpSpread.ActiveSheet.Columns[fix.Name].Index + 1;
                                    }
                                    else
                                    {
                                        pfpSpread.ActiveSheet.FrozenColumnCount = 0;
                                    }

                                }
                                catch (Exception err)
                                {

                                }
                            });
                            mfix.DropDownItems.Add(fix);
                            mfix.Image = Resources.Pin2__16;
                        }
                    }
                }

                m.Items.Add(mVisible);
                m.Items.Add(mfilter);
                m.Items.Add(mfix);

                //FarPoint.Win.Spread.ConditionalFormattingIconSetStyle
                m.Items.Add("Excel Save", Resources.Excel_16, new EventHandler((delegate
                {
                    Loading waitform = new Loading();
                    try
                    {
                        SaveFileDialog saveFile = new SaveFileDialog();
                        saveFile.InitialDirectory = @"c:";
                        saveFile.Title = "데이터 Excel 저장 위치 지정";
                        saveFile.DefaultExt = "xlsx";
                        saveFile.Filter = "Xlsx Files(*.xlsx)|*.xlsx";
                        if (saveFile.ShowDialog() == DialogResult.OK)
                        {

                            Microsoft.Office.Interop.Excel.Application excelApp1 = GetRunningExcelApplication();

                            if (excelApp1 != null)
                            {
                                bool isFileOpen = IsFileOpen(excelApp1,  saveFile.FileName);

                                if (isFileOpen)
                                {
                                    foreach (Microsoft.Office.Interop.Excel.Workbook workbook1 in excelApp1.Workbooks)
                                    {
                                        if (string.Equals(workbook1.FullName, saveFile.FileName, StringComparison.OrdinalIgnoreCase))
                                        {
                                            // 파일을 저장하지 않고 닫습니다.
                                            workbook1.Close(false);
                                            Marshal.ReleaseComObject(workbook1);
                                            break;
                                        }
                                    }

                                }

                            }



                            waitform.Show(pfpSpread.FindForm(), "준비중입니다.");

                            Microsoft.Office.Interop.Excel.Application excelApp = new  Microsoft.Office.Interop.Excel.Application();
                            excelApp.Visible = false; // Excel 창을 보이지 않게 설정
                            excelApp.ScreenUpdating = false;
                            //excelApp.Calculation = XlCalculation.xlCalculationManual;
                            // 새 워크북 추가
                            Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Add();
                            Microsoft.Office.Interop.Excel.Worksheet worksheet = ( Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];



                            object[,] dataArray = new object[pfpSpread.Sheets[0].RowCount+1, pfpSpread.Sheets[0].ColumnCount+1];

                            for (int x = 0; x < pfpSpread.Sheets[0].RowCount; x++)
                            {

                                waitform.MsgChange(x + " / " + (pfpSpread.Sheets[0].RowCount - 1));
                                int a = 1;

                                for (int i = 0; i < pfpSpread.Sheets[0].ColumnCount; i++)
                                {

                                    if (pfpSpread.Sheets[0].Columns[i].Visible == true &&
                                        pfpSpread.Sheets[0].Columns[i].CellType.ToString() != "ButtonCellType" &&
                                        pfpSpread.Sheets[0].Columns[i].CellType.ToString() != "FileButtonCellType" &&
                                        pfpSpread.Sheets[0].Columns[i].CellType.ToString() != "ImageCellType"

                                    )
                                    {
                                        if (x == 0)
                                        {                                  
                                            dataArray[0, a] = pfpSpread.Sheets[0].Columns[i].Label;
                                        }
                                       
                                        dataArray[x+1, a] = pfpSpread.Sheets[0].GetText(x, i);
                                        a++;
                                    }


                                }
                            }

                            // 워크시트의 범위에 데이터를 한 번에 쓰기new object[pfpSpread.Sheets[0].RowCount, pfpSpread.Sheets[0].ColumnCount];
                            Microsoft.Office.Interop.Excel.Range range = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[pfpSpread.Sheets[0].RowCount + 1, pfpSpread.Sheets[0].ColumnCount]];
                            range.Value = dataArray;


                            // 데이터 추가1



                            // 파일 저장
                            string filePath = saveFile.FileName;
                            workbook.SaveAs(filePath);

                            // 종료 및 정리
                            workbook.Close(false);
                            excelApp.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                            //Console.WriteLine("Excel 파일이 저장되었습니다.");
                            waitform.Close();

                            CustomMsg.ShowMessageLink("Excel 파일로 저장 하였습니다.\r\n" + saveFile.FileName.ToString(), "Excel 저장 알림");
                        }
                    }
                    catch (Exception pExcption)
                    {
                        waitform.Close();

                        CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
                    }
                })));


                bool ck = true;
                for (int i = 0; i < pfpSpread.Sheets[0].ColumnCount; i++)
                {
                    if (pfpSpread.Sheets[0].Columns[i].Visible == true)
                    {
                        if (pfpSpread.Sheets[0].Columns[i].Locked != true &&
                            pfpSpread.Sheets[0].Columns[i].CellType.GetType() != typeof(ButtonCellType))
                        {
                            ck = false;
                        }
                    }
                }
                if (ck == false)
                {
                    m.Items.Add("삭제", Resources.close_16x16, new EventHandler((delegate
                    {
                        try
                        {
                            DialogResult _DialogResult1 = CustomMsg.ShowMessage("삭제하시겠습니까?", "Question", MessageBoxButtons.YesNo);
                            if (_DialogResult1 == DialogResult.Yes)
                            {

                                FarPoint.Win.Spread.Model.CellRange[] selectedRanges =  pfpSpread.Sheets[0].GetSelections();
                                foreach (FarPoint.Win.Spread.Model.CellRange range in selectedRanges)
                                {
                                    for (int i = range.Row; i < range.Row + range.RowCount; i++)
                                    {
                                        if (pfpSpread.Sheets[0].RowHeader.Cells[i, 0].Text != "입력")
                                        {
                                            pfpSpread.Sheets[0].RowHeader.Cells[i, 0].Text = "삭제";
                                            for (int a = 0; a < pfpSpread.Sheets[0].Columns.Count; a++)
                                            {
                                                if (pfpSpread.Sheets[0].Columns[a].Tag.ToString().Contains("USE_YN"))
                                                {
                                                    pfpSpread.Sheets[0].SetValue(i, pfpSpread.Sheets[0].Columns[a].Tag.ToString(), "N");
                                                }
                                            }

                                        }
                                    }
                                }
                                foreach (FarPoint.Win.Spread.Model.CellRange range in selectedRanges)
                                {
                                    int del = 0;

                                    for (int i = range.Row; i < range.Row + range.RowCount; i++)
                                    {
                                        //MessageBox.Show((i + del).ToString());

                                        if (pfpSpread.Sheets[0].RowHeader.Cells[(i + del), 0].Text == "입력")
                                        {
                                            FpSpreadManager.SpreadRowRemove(pfpSpread, 0, (i + del));
                                            del--;
                                        }
                                    }
                                }

                            }

                        }
                        catch (Exception err)
                        {
                            MessageBox.Show(err.Message);
                        }
                    })));

                }
                ///20230316 방찬석 추가 -- 검색어 찾기 기능
                m.Items.Add("검색어로 찾기", Resources.find, new EventHandler((delegate
                {
                    try
                    {
                        foreach (Form openForm in System.Windows.Forms.Application.OpenForms)
                        {
                            if (openForm.Name == "FindSpread") // 열린 폼의 이름 검사
                            {
                                if (openForm.WindowState == FormWindowState.Minimized)
                                {  // 폼을 최소화시켜 하단에 내려놓았는지 검사
                                    openForm.WindowState = FormWindowState.Normal;
                                    //openForm.Location = new Point(this.Location.X + this.Width, this.Location.Y);
                                }
                                //((frmPopupSum)openForm).SetInit(pfpSpread);
                                openForm.Activate();
                                return;
                            }

                        }

                        FindSpread popup = new FindSpread(pfpSpread);
                        popup.Show(); //showDialog 시 Main 시트 클릭이 안되어 Show로 진행. 차후 변경 가능.
                    }
                    catch (Exception err)
                    {

                    }
                })));
                m.Items.Add("합계", Resources.calculator16, new EventHandler((delegate
                {
                    try
                    {
                        //합계...
                        FarPoint.Win.Spread.Model.CellRange cr;
                        cr = pfpSpread.ActiveSheet.GetSelection(0);

                        //Cell acell, mycell;
                        //acell = pfpSpread.ActiveSheet.Cells[cr.Row, cr.Column];
                        //mycell = pfpSpread.ActiveSheet.Cells[cr.Row + cr.RowCount, cr.Column];
                        if (cr == null)
                        {
                            return;
                        }
                        if (pfpSpread.ActiveSheet.Columns[cr.Column].CellType == null)
                        {
                            return;
                        }

                        if (pfpSpread.ActiveSheet.Columns[cr.Column].CellType.ToString() == "NumberCellType")
                        {

                            //pfpSpread.ActiveSheet.ColumnFooter.Cells[0, cr.Column].CellType 

                            DataTable clacDT = new DataTable();
                            clacDT.Columns.Add("calc", typeof(decimal));

                            for (int i = 0; i < cr.RowCount; i++)
                            {
                                DataRow dr = clacDT.NewRow();
                                dr["calc"] = pfpSpread.ActiveSheet.Cells[cr.Row + i, cr.Column].Value;
                                clacDT.Rows.Add(dr);
                            }

                            //FindSpread popup = new FindSpread(pfpSpread);
                            //popup.Show();

                            foreach (Form openForm in Application.OpenForms)
                            {
                                if (openForm.Name == "frmPopupSum") // 열린 폼의 이름 검사
                                {
                                    if (openForm.WindowState == FormWindowState.Minimized)
                                    {  // 폼을 최소화시켜 하단에 내려놓았는지 검사
                                        openForm.WindowState = FormWindowState.Normal;
                                        //openForm.Location = new Point(this.Location.X + this.Width, this.Location.Y);
                                    }
                                    ((frmPopupSum)openForm).SetInit(clacDT);
                                    openForm.Activate();
                                    return;
                                }
                            }
                            frmPopupSum popup = new frmPopupSum(clacDT);
                            popup.Show();

                        }
                        else
                        {
                            CustomMsg.ShowMessage("숫자 형식 Cell만 사용 하실 수 있습니다.", "합계 불가 알림");
                        }


                    }
                    catch (Exception err)
                    {
                        CustomMsg.ShowMessage("숫자 형식 Cell만 사용 하실 수 있습니다.", "합계 불가 알림");
                    }
                })));
                m.Items.Add("옵션", Resources.Pin2__16, new EventHandler((delegate
                {
                    try
                    {
                        BaseFormSetting baseFormSetting = new BaseFormSetting( pfpSpread._menu_name,pfpSpread,pfpSpread._user_account);

                        baseFormSetting.Show();
                    }
                    catch (Exception err)
                    {

                    }
                })));
                pfpSpread.ContextMenuStrip = m;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
           

        }

        public static Microsoft.Office.Interop.Excel.Application GetRunningExcelApplication()
        {
            Microsoft.Office.Interop.Excel.Application excelApp = null;
            foreach (var process in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
            {
                try
                {
                    // Excel 프로세스의 COM 객체를 얻으려 시도합니다.
                    excelApp = (Microsoft.Office.Interop.Excel.Application)Marshal.GetActiveObject("Excel.Application");
                    break;
                }
                catch (COMException)
                {
                    // Excel 프로세스는 있지만 Excel 애플리케이션 객체를 가져올 수 없는 경우
                }
            }
            return excelApp;
        }

        public static bool IsFileOpen(Microsoft.Office.Interop.Excel.Application excelApp, string filePath)
        {
            foreach (Microsoft.Office.Interop.Excel.Workbook workbook in excelApp.Workbooks)
            {
                if (string.Equals(workbook.FullName, filePath, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }






    }



}
