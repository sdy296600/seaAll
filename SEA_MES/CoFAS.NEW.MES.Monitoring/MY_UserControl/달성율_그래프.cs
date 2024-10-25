using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Monitoring.MY_UserControl
{
    public partial class 달성율_그래프 : UserControl
    {
        public 달성율_그래프()
        {
            InitializeComponent();

        }

        private void 달성율_그래프_FontChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in chart1.Titles)
                {
                    item.Font = new Font(this.Font.Name, this.Font.Size - 5, this.Font.Style);
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
          
        }

        public void SET_DATE(double act, double plan,string unit)
        {
            try
            {
                chart1.ChartAreas[0].AxisX.Minimum = 0;  // X축 최소값
                chart1.ChartAreas[0].AxisX.Maximum = 100;  // X축 최대값
                chart1.ChartAreas[0].AxisY.Minimum = 0;  // X축 최소값
                chart1.ChartAreas[0].AxisY.Maximum = 100;  // X축 최대값

                Plan.Text = plan.ToString("F0");
                Act.Text = act.ToString("F0");
                //chart1.Series[0].Points[1].YValues[0] = act;
                //chart1.Series[0].Points[0].YValues[0] = plan;

                _unit1.Text = unit;
                _unit2.Text = unit;

                double 달성률 = ((act / plan) * 100);

                if(act == 0 || plan == 0)
                {
                    chart1.Titles[1].Text = 0.ToString("F0") + "%";
                    chart1.Series[0].Points[0].YValues[0] = 100;
                    chart1.Series[0].Points[1].YValues[0] = 0;
                    return;
                }
                if (달성률 > 100)
                {
                    chart1.Titles[1].Text = 100.ToString("F0") + "%";
                    chart1.Series[0].Points[0].YValues[0] = 0;
                    chart1.Series[0].Points[1].YValues[0] = 100;

                }
                else
                {
                    chart1.Titles[1].Text = 달성률.ToString("F0") + "%";
                    chart1.Series[0].Points[0].YValues[0] = 100 - 달성률;
                    chart1.Series[0].Points[1].YValues[0] = 달성률;
                }


                if (act >= plan)
                {
                    panel9.BackColor = Color.FromArgb(102, 255, 51);

                }
                else
                {
                    double oee1 = ((plan)/100)*90 ;
                    if (act > oee1)
                    {
                        panel9.BackColor = Color.Yellow;
                    }
                    else
                    {
                        panel9.BackColor = Color.Red;
                    }
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
