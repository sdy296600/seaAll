using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class Scheduler : System.Windows.Forms.UserControl
    {
        #region .. Double Buffered function ..
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;
            System.Reflection.PropertyInfo aProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        #endregion

        DateTime _Main_time = DateTime.Now.AddDays(1 - DateTime.Now.Day);
        public UserEntity _pUserEntity = null;
        public Scheduler(UserEntity pUserEntity)
        {
            _pUserEntity = pUserEntity;
            InitializeComponent();      
        }

        private void Scheduler_Load(object sender, EventArgs e)
        {
            Day_Set();
        }

        public void Day_Set()
        {
            try
            {
                tableLayoutPanel2.Controls.Clear();

                CalendarEntity calendarEntity = new CalendarEntity();
                calendarEntity.CRUD = "R";

                DataTable _DataTable = new CalendarBusiness().Calendar_Info(calendarEntity);

                DayOfWeek_Set();

                int Month = _Main_time.Month;
                int col = 0;
                int row = 1;

                lblMonth.Text = _Main_time.Year.ToString() + "년" + (new System.Globalization.CultureInfo("ko-KR")).DateTimeFormat.MonthNames[Month - 1];
                lblMonth.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
                DateTime time = _Main_time;

                while (time.Month == Month)
                {
                    col = ((int)time.DayOfWeek);

                    DataRow[] datas = _DataTable.Select($"start_date <='{time.ToString("yyyy-MM-dd")}' AND end_date >= '{time.ToString("yyyy-MM-dd")}'");

                   

                    Sub_Scheduler lbl = new Sub_Scheduler(datas,_pUserEntity);

                    lbl.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
                    lbl.Name = time.ToString("yyyy-MM-dd");
                    lbl.Day_Property = time.Day.ToString();
                    lbl.Dock = DockStyle.Fill;
                    lbl.Margin = new Padding(0, 0, 0, 0);
                 
                    if (col == 6)
                    {
                        lbl.ForeColor = Color.DodgerBlue;
                    }
                    if (col == 0)
                    {
                        lbl.ForeColor = Color.OrangeRed;
                    }

                    if (time.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                    {
                        lbl.BackColor = Color.LightYellow;
                    }

                    tableLayoutPanel2.Controls.Add(lbl, col, row);
                    lbl.label_Set();

                    if (col == 6)
                    {
                        row++;
                    }

               
                    time = time.AddDays(1);
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }

        private void DayOfWeek_Set()
        {
            try
            {
                string[] item = Enum.GetNames(typeof(DayOfWeek));

                for (int i = 0; i < item.Length; i++)
                {
                    Label lbl = new Label();

                    lbl.Name = item[i];
                    lbl.Text = item[i];
                    lbl.TextAlign = ContentAlignment.MiddleCenter;
                    lbl.ForeColor = Color.Black;
                    lbl.BackColor = Color.Transparent;
                    lbl.Margin = new Padding(0, 0, 0, 0);
                    lbl.Dock = DockStyle.Fill;
                    lbl.Font = new Font("맑은 고딕", 12, FontStyle.Bold);

                    if (i == 0)
                    {
                        lbl.ForeColor = Color.OrangeRed;
                    }
                    if (i == 6)
                    {
                        lbl.ForeColor = Color.DodgerBlue;
                    }

                    tableLayoutPanel2.Controls.Add(lbl, i, 0);
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
      
        private void DateTime_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;

                switch (btn.Name)
                {
                    case "btndown":
                        _Main_time = _Main_time.AddMonths(-1);
                        break;
                    case "btnup":
                        _Main_time = _Main_time.AddMonths(1);
                        break;
                    case "btntoday":
                        _Main_time = DateTime.Now.AddDays(1 - DateTime.Now.Day);
                        break;
                }

                Day_Set();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }

    
    }
}
