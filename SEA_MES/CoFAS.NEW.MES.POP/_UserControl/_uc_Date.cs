using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP._UserControl
{
    public partial class _uc_Date : UserControl
    {
        public _uc_Date(DataTable dt)
        {
            tableLayoutPanel1.ColumnCount = dt.Columns.Count;
            InitializeComponent();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataColumn item = dt.Columns[i];
                Label label = new Label();

                label.Text = item.ColumnName;
                label.Margin = new Padding(0, 0, 0, 0);
                label.TextAlign = ContentAlignment.MiddleCenter;
                tableLayoutPanel1.Controls.Add(label, i, 0);

            }

        }
    }
}
