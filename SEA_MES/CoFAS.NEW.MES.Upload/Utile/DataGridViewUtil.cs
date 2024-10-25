
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Upload
{
    public class DataGridViewUtil
    {
        /// <summary>
        /// 그리드뷰 컬럼 추가 메서드
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="headerText"></param>
        /// <param name="dataPropertyName"></param>
        /// <param name="visibility"></param>
        /// <param name="colWidth"></param>
        /// <param name="textAlign"></param>
        /// <param name="readOnly"></param>
        public static void AddNewColumnToDataGridView(DataGridView dgv, string headerText, string dataPropertyName, bool visibility, int colWidth = 100, DataGridViewContentAlignment textAlign = DataGridViewContentAlignment.MiddleCenter, bool readOnly = true)
        {
            DataGridViewTextBoxColumn gridCol = new DataGridViewTextBoxColumn();
            gridCol.HeaderText = headerText;
            gridCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            gridCol.Resizable = DataGridViewTriState.False;
            gridCol.DataPropertyName = dataPropertyName;
            gridCol.Width = colWidth;
            gridCol.Visible = visibility;
            //gridCol.ValueType = typeof(string);
            gridCol.ReadOnly = readOnly;
            gridCol.DefaultCellStyle.Alignment = textAlign;
            gridCol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(gridCol);
        }
        /// <summary>
        /// 그리드뷰 초기 설정 메서드
        /// </summary>
        /// <param name="dgv"></param>
        public static void InitSettingGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;

            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgv.RowsDefaultCellStyle.SelectionBackColor = Color.AliceBlue;
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.OrangeRed;
         
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.DodgerBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.RowHeadersDefaultCellStyle.BackColor = Color.DodgerBlue;

            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;     
            dgv.EnableHeadersVisualStyles = false;

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;

            //dgv.RowTemplate.Height = 30;
            //dgv.ColumnHeadersHeight = 30;

            dgv.AllowUserToResizeRows = false;
            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        


        }

       

        /// <summary>
        /// 그리드뷰 버튼컬럼 추가 메서드
        /// </summary>
        /// <param name="headerText"></param>
        /// <param name="dgv"></param>
        /// <param name="topMargin"></param>
        /// <param name="bottomMargin"></param>
        /// <returns></returns>
        public static int DataGridViewBtnSet(string headerText, DataGridView dgv, int topMargin = 37, int bottomMargin = 37)
        {
            DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
            btn1.HeaderText = headerText;
            btn1.Text = headerText;
            btn1.Width = 200;
            btn1.DefaultCellStyle.Padding = new Padding(5, topMargin, 5, bottomMargin);
            btn1.UseColumnTextForButtonValue = true;
            return dgv.Columns.Add(btn1);
        }
        /// <summary>
        /// 그리드뷰 버튼컬럼 추가 메서드
        /// </summary>
        /// <param name="headerText"></param>
        /// <param name="dgv"></param>
        /// <param name="topMargin"></param>
        /// <param name="bottomMargin"></param>
        /// <returns></returns>
        public static int DataGridViewBtnSet(string headerText, string text, DataGridView dgv, int topMargin = 37, int bottomMargin = 37)
        {
            DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
            btn1.HeaderText = headerText;
            btn1.Text = text;
            btn1.FlatStyle = FlatStyle.Flat;
            btn1.DefaultCellStyle.BackColor = Color.RoyalBlue;
            btn1.DefaultCellStyle.BackColor = Color.White;
            btn1.Width = 80;
            btn1.DefaultCellStyle.Padding = new Padding(5, topMargin, 5, bottomMargin);
            btn1.UseColumnTextForButtonValue = true;
            return dgv.Columns.Add(btn1);
        }
        /// <summary>
        /// 그리드뷰 이미지컬럼 추가 메서드
        /// </summary>
        /// <param name="headerText"></param>
        /// <param name="propertyName"></param>
        /// <param name="width"></param>
        /// <param name="dgv"></param>
        public static void DataGridViewImageSet(string headerText, string propertyName, int width, DataGridView dgv)
        {
            DataGridViewImageColumn img = new DataGridViewImageColumn();
            img.HeaderText = headerText;
            img.DataPropertyName = propertyName;
            img.ImageLayout = DataGridViewImageCellLayout.Zoom;
            img.Width = width;
            dgv.Columns.Add(img);
        }
        /// <summary>
        /// 그리드뷰 체크박스 컬럼 추가 메서드
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int DataGridViewCheckBoxSet(DataGridView dgv, string name)
        {
            DataGridViewCheckBoxColumn chb1 = new DataGridViewCheckBoxColumn();
            chb1.HeaderText = "        ";
            chb1.Name = name;
            chb1.Width = 60;
            chb1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            chb1.DefaultCellStyle.Padding = new Padding(0, 0, 0, 0);
            chb1.FlatStyle = FlatStyle.Flat;
            return dgv.Columns.Add(chb1);
        }
        /// <summary>
        /// 그리드뷰 체크박스 컬럼 추가 메서드
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int DataGridViewCheckBoxSet(DataGridView dgv, string name, string HeaderText)
        {
            DataGridViewCheckBoxColumn chb1 = new DataGridViewCheckBoxColumn();
            chb1.HeaderText = "        ";
            chb1.Name = name;
            chb1.Width = 60;
            chb1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            chb1.DefaultCellStyle.Padding = new Padding(0, 0, 0, 0);
            chb1.FlatStyle = FlatStyle.Flat;
            return dgv.Columns.Add(chb1);
        }
        /// <summary>
        /// 그리드뷰 체크박스 컬럼 추가 메서드(컬럼헤더추가) -OJH
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="name"></param>
        /// <param name="HeaderText"></param>
        /// <param name="Width"></param>
        /// <returns></returns>
        public static int DataGridViewCheckBoxSet(DataGridView dgv, string name, string HeaderText = "        ", int Width = 100)
        {
            DataGridViewCheckBoxColumn chb1 = new DataGridViewCheckBoxColumn();
            chb1.HeaderText = HeaderText;
            chb1.Name = name;
            chb1.Width = Width;
            chb1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            chb1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            chb1.DefaultCellStyle.Padding = new Padding(0, 0, 0, 0);
            chb1.FlatStyle = FlatStyle.Flat;
            return dgv.Columns.Add(chb1);
        }
        /// <summary>
        /// 그리드뷰 행번호 추가 메서드 -OJH
        /// </summary>
        /// <param name="dataGridView"></param>
        public static void DataGridViewRowNumSet(DataGridView dataGridView)
        {
            dataGridView.RowPostPaint += DataGridView_RowPostPaint;
        }
        /// <summary>
        /// 그리드뷰 행번호 추가 이벤트 -OJH
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();
            var centerFormat = new StringFormat()
            { // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, grid.Font, SystemBrushes.ControlText, headerBounds, centerFormat); ;

        }
    }
}
