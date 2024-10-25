using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Monitoring
{
    public partial class _uc_Date : UserControl
    {
        public _uc_Date(DataTable dt)
        {

            InitializeComponent();
            tableLayoutPanel1.ColumnCount = dt.Columns.Count;

            TableLayoutColumnStyleCollection styles = this.tableLayoutPanel1.ColumnStyles;

            styles.Clear();
            foreach (DataColumn itme in dt.Columns)
            {
                ColumnStyle style = new ColumnStyle();
                style.SizeType = SizeType.Percent;
                if (itme.ColumnName == "NAME")
                {
                    style.Width = 30;
                }
                else
                {
                    style.Width = 20;
                }
                styles.Add(style);
            }
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataColumn item = dt.Columns[i];
                Label label = new Label();

                label.Text = item.ColumnName;
                label.ForeColor = Color.White;
                label.Margin = new Padding(0, 0, 0, 0);
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Dock = DockStyle.Fill;
                label.Font = new Font("맑은고딕", 10, FontStyle.Bold);
                tableLayoutPanel1.Controls.Add(label, i, 0);

            }

        }

        public _uc_Date(DataRow dr)
        {

            InitializeComponent();
            tableLayoutPanel1.ColumnCount = dr.Table.Columns.Count;

            TableLayoutColumnStyleCollection styles = this.tableLayoutPanel1.ColumnStyles;

            styles.Clear();
            foreach (DataColumn itme in dr.Table.Columns)
            {
                ColumnStyle style = new ColumnStyle();
                style.SizeType = SizeType.Percent;
                if (itme.ColumnName == "NAME")
                {
                    style.Width = 30;
                }
                else
                {
                    style.Width = 20;
                }
                styles.Add(style);
            }
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                Label label = new Label();
                label.Text = dr[i].ToString();
                label.ForeColor = Color.White;
                label.Margin = new Padding(0, 0, 0, 0);
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Dock = DockStyle.Fill;
                label.Font = new Font("맑은고딕", 10, FontStyle.Bold);
                tableLayoutPanel1.Controls.Add(label, i, 0);

            }

        }
        
    }
}
