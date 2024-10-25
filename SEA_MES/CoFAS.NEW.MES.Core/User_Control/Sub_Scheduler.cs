using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class Sub_Scheduler : System.Windows.Forms.UserControl
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
        DataRow[] _data = null;
        public UserEntity _pUserEntity = null;

        public bool _Click = true;
        public string Day_Property
        {
            get { return this.lblday.Text; }
            set { this.lblday.Text = value; }
        }
        public Sub_Scheduler(DataRow[] data, UserEntity pUserEntity)
        {
            _data = data;
            _pUserEntity = pUserEntity;
            InitializeComponent();

        }
        public Sub_Scheduler(DataRow[] data, UserEntity pUserEntity, bool pClick)
        {
            _data = data;
            _pUserEntity = pUserEntity;
            InitializeComponent();
            _Click = pClick;
        }

        private void Sub_Scheduler_Load(object sender, EventArgs e)
        {

            this.MouseHover += Day_MouseHover;
            this.MouseLeave += DayMouseLeave;
            this.panel1.MouseHover += Day_MouseHover;
            this.panel1.MouseLeave += DayMouseLeave;
            if (_Click == true)
            {
                this.tableLayoutPanel1.Click += lbl_day_Click;
                this.lblday.Click += lbl_day_Click;
                this.panel1.Click += lbl_day_Click;
            }
            //label_Set();
        }

        private void DayMouseLeave(object sender, EventArgs e)
        {
            Sub_Scheduler lbl = sender as Sub_Scheduler;

            if (this.Name == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                this.BackColor = Color.LightYellow;
            }
            else
            {
                this.BackColor = Color.Transparent;
            }
        }

        private void Day_MouseHover(object sender, EventArgs e)
        {
            this.BackColor = Color.AliceBlue;
        }

        private void lbl_day_Click(object sender, EventArgs e)
        {
            try
            {
                frmScheduler frmScheduler = new frmScheduler(this.Name,_pUserEntity);
                frmScheduler.ShowDialog();
                Day_Set();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        public void Day_Set()
        {
            try

            {
                Control control =this.Parent.Parent.Parent;

                if (control.GetType() == typeof(Scheduler))
                {
                    (control as Scheduler).Day_Set();
                }
                else if (control.GetType() == typeof(Order_Scheduler))
                {
                    (control as Order_Scheduler).Day_Set();
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
        private void tableLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (_data != null)
                {
                    panel1.Controls.Clear();
                    if (_data.Length == 0)
                    {
                        return;
                    }
                    for (int i = 0; i < _data.Length; i++)
                    {
                        Label_Scheduler label = new Label_Scheduler(_data[i],_pUserEntity);                      
                        //label.Padding = new Padding(3, 3, 3, 3);
                        //label.Margin = new Padding(10, 10, 10, 10);
                        label.Name = _data[i][0].ToString();
                        label.label1.Text = _data[i][1].ToString();
                        label.Size = new Size(panel1.Size.Width - 30, 24);
                     
                        label.label1.Click += Label_Scheduler_Click;
                        label.label2.Click += Label_Scheduler_Click;
                        if (i == 0)
                        {
                            label.Location = new Point(5, 0);
                        }
                        else
                        {
                            label.Location = new Point(5, (label.Size.Height + 1) * i);
                        }
                        //label.Location = new Point(2, (i * 24));
                        panel1.Controls.Add(label);
                    }
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void Label_Scheduler_Click(object sender, EventArgs e)
        {
            try
            {
                Label_Scheduler label_  = (sender as Control).Parent.Parent as Label_Scheduler;
                CalendarEntity calendarEntity = new CalendarEntity();
                calendarEntity.id = int.Parse(label_.Name);
                DataTable  dataTable = new CalendarBusiness().Calendar_Info_R2(calendarEntity);
                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    return;
                }
                else
                {
                    calendarEntity.id = Convert.ToInt32(dataTable.Rows[0][0]);
                    calendarEntity.title = dataTable.Rows[0][1].ToString();
                    calendarEntity.content = dataTable.Rows[0][2].ToString();
                    calendarEntity.start_date = Convert.ToDateTime(dataTable.Rows[0][3]);
                    calendarEntity.end_date = Convert.ToDateTime(dataTable.Rows[0][4]);
                    calendarEntity.reg_date = Convert.ToDateTime(dataTable.Rows[0][5]);
                    calendarEntity.reg_user = dataTable.Rows[0][6].ToString();
                    calendarEntity.up_date = Convert.ToDateTime(dataTable.Rows[0][7]);
                    calendarEntity.up_user = dataTable.Rows[0][8].ToString();
                    calendarEntity.color_R = Convert.ToInt32(dataTable.Rows[0][9]);
                    calendarEntity.color_G = Convert.ToInt32(dataTable.Rows[0][10]);
                    calendarEntity.color_B = Convert.ToInt32(dataTable.Rows[0][11]);

                    frmScheduler frm = new frmScheduler(calendarEntity,_pUserEntity);
                    frm.ShowDialog();

                    Day_Set();
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        public void label_Set()
        {
            //for (int i = 0; i < _data.Length; i++)
            //{
            //    DataRow item = _data[i];
            //    Label_Scheduler label_Scheduler = new Label_Scheduler(item,_pUserEntity);
            //    label_Scheduler.Size = new Size(panel1.Size.Width, 30);
            //    label_Scheduler.Location = new Point(2, (i * 30));
            //    //label_Scheduler.Dock = DockStyle.Top;
            //    panel1.Controls.Add(label_Scheduler);
            //}
          
        }
    }
}
