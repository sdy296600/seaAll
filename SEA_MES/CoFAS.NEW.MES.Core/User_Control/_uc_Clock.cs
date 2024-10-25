using System;

using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class _uc_Clock : UserControl
    {
        private System.Threading.Timer _tmrNow;
        public _uc_Clock()
        {
            InitializeComponent();
        }

        private void _uc_Clock_Load(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.DayOfWeek.ToString();
            _tmrNow = new System.Threading.Timer(Now_DateTime, null, 0, 1000);
            label2.ForeColor = this.ForeColor;
            label3.ForeColor = this.ForeColor;
            label4.ForeColor = this.ForeColor;

            label2.Font = this.Font;
            label3.Font = this.Font;
            label4.Font = this.Font;
        }

        private void Now_DateTime(object obj)
        {
            try
            {
                BeginInvoke(new MethodInvoker(delegate ()
                {

                    DateTime nowDt = DateTime.Now;
                    label3.Text = nowDt.ToString("yyyy.MM.dd");
                    label2.Text = nowDt.ToString("HH:mm:ss");
                    //label4.Text = dt.DayOfWeek.ToString();

                    string str = "";

                    if (nowDt.DayOfWeek == DayOfWeek.Monday)
                        str = "월요일";

                    else if (nowDt.DayOfWeek == DayOfWeek.Tuesday)
                        str = "화요일";
                    else if (nowDt.DayOfWeek == DayOfWeek.Wednesday)
                        str = "수요일";
                    else if (nowDt.DayOfWeek == DayOfWeek.Thursday)
                        str = "목요일";
                    else if (nowDt.DayOfWeek == DayOfWeek.Friday)
                        str = "금요일";
                    else if (nowDt.DayOfWeek == DayOfWeek.Saturday)
                        str = "토요일";
                    else if (nowDt.DayOfWeek == DayOfWeek.Sunday)
                        str = "일요일";

                    label4.Text = str;

                }));
            }
            catch (Exception err)
            {
                _tmrNow.Dispose();
            }
        }

        private void _uc_Clock_FontChanged(object sender, EventArgs e)
        {
            label2.ForeColor = this.ForeColor;
            label3.ForeColor = this.ForeColor;
            label4.ForeColor = this.ForeColor;

            label2.Font = this.Font;
            label3.Font = this.Font;
            label4.Font = this.Font;
        }
    }
}
