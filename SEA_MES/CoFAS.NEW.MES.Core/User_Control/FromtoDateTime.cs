using System;

namespace CoFAS.NEW.MES.Core
{
    public partial class FromtoDateTime : System.Windows.Forms.UserControl
    {
        public FromtoDateTime()
        {
            InitializeComponent();


            dtp_Start.Value = DateTime.Now.AddDays(-3);
            dtp_End.Value = DateTime.Now;
        }

        public DateTime StartValue
        {
            get { return this.dtp_Start.Value; }
            set { this.dtp_End.Value = value; }
        }
        public DateTime EndValue
        {
            get { return this.dtp_End.Value; }
            set { this.dtp_End.Value = value; }
        }
        private void FromtoDateTime_Load(object sender, EventArgs e)
        {
            DateReset();
        }

        public void DateReset()
        {
            dtp_Start.Value = DateTime.Parse(DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd"));
            dtp_End.Value = dtp_Start.Value.AddMonths(1).AddMinutes(-1);
            this.dtp_Start.ValueChanged += new System.EventHandler(this.dtp_Start_ValueChanged);
            this.dtp_End.ValueChanged += new System.EventHandler(this.dtp_End_ValueChanged);
        }

        private void dtp_Start_ValueChanged(object sender, EventArgs e)
        {
            //if (dtp_Start.Value > dtp_End.Value)
            //{
            //    dtp_Start.Value = DateTime.Parse(DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd"));
            //    dtp_End.Value = dtp_Start.Value.AddMonths(1).AddMinutes(-1);
            //    CustomMsg.ShowMessage("시작일은 종료일 보다 늦을수 없습니다.");
            //}
        }

        private void dtp_End_ValueChanged(object sender, EventArgs e)
        {
            //if (dtp_Start.Value > dtp_End.Value)
            //{
            //    dtp_Start.Value = DateTime.Parse(DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd"));
            //    dtp_End.Value = dtp_Start.Value.AddMonths(1).AddMinutes(-1);
            //    CustomMsg.ShowMessage("종료일은 시작일 보다 빠를수 없습니다.");
            //}

        }
    }
}
