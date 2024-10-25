using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core.Popup
{
    public partial class frmPopupSum : Form
    {
        public frmPopupSum()
        {
            InitializeComponent();
        }

        public frmPopupSum(DataTable dt)
        {
            InitializeComponent();
            SetInit(dt);
        }

        public void SetInit(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                try
                {

                    txtMax.Text = Math.Round((decimal)dt.Compute("max(calc)", ""), 4).ToString();
                    txtMin.Text = Math.Round((decimal)dt.Compute("min(calc)", ""), 4).ToString();
                    txtAvg.Text = Math.Round((decimal)dt.Compute("avg(calc)", ""), 4).ToString();
                    txtSum.Text = Math.Round((decimal)dt.Compute("sum(calc)", ""), 4).ToString();
                    txtCnt.Text = dt.Compute("COUNT(calc)", "").ToString();

                    //txtMax.Text = dt.Compute("max(calc)", "").ToString();
                    //txtMin.Text = dt.Compute("min(calc)", "").ToString();
                    //txtAvg.Text = dt.Compute("avg(calc)", "").ToString();
                    //txtSum.Text = dt.Compute("sum(calc)", "").ToString();
                    //txtCnt.Text = dt.Compute("COUNT(calc)", "").ToString();
                }
                catch (Exception pExcption)
                {
                    CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
                }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox tt = (TextBox)this.Controls.Find("txt" + ((Button)sender).Name.Substring(3, 3), true)[0];

                Clipboard.SetData(DataFormats.Text, (Object)tt.Text);
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
    }
}
