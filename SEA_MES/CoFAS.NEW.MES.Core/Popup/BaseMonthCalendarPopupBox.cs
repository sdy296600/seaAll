using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class BaseMonthCalendarPopupBox : Form
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

        private void SetDoubleBuffered_Control(Control.ControlCollection controls)

        {
            foreach (Control item in controls)
            {
                if (item.Controls.Count != 0)
                {
                    SetDoubleBuffered_Control(item.Controls);
                }

                SetDoubleBuffered(item);
            }
        }
        #endregion
        #region ○ 폼 이동

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);


        private void tspMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        #endregion

        #region ○ 변수선언

        public int _ID = 0;
        public string _TYPE = string.Empty;
        public string _SelectionStart = string.Empty;
        #endregion

        #region ○ 생성자

         public BaseMonthCalendarPopupBox(int pID,string pTYPE)
        {

            _ID = pID;
            _TYPE = pTYPE;
            InitializeComponent();

            Load += new EventHandler(Form_Load);
        }

        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                myMonthCalendar1.MaxSelectionCount = 1;


                GET_DATE(DateTime.Now);
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }






        #endregion

   
        private void button1_Click(object sender, EventArgs e)
        {

            _SelectionStart = myMonthCalendar1.SelectionStart.ToString("yyyy-MM-dd");
            this.DialogResult = DialogResult.OK;
            this.Close();
          
        }

        private void myMonthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            GET_DATE(e.Start);
        }

        private void GET_DATE(DateTime date)
        {
            string sql =$@"SELECT CHECK_DATE
                                 from [dbo].[EQUIP_INSPECTION_LIST]
                                 where EQUIPMENT_ID = {_ID}
                                   AND FORMAT(CHECK_DATE, 'yyyy-MM') = '{date.ToString("yyyy-MM")}'
                                   AND TYPE ='{_TYPE}'
                                 GROUP BY CHECK_DATE";
            DataTable dataTable = new CoreBusiness().SELECT(sql);

            DateTime[] dates = new DateTime[dataTable.Rows.Count];


            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                dates[i] = Convert.ToDateTime(dataTable.Rows[i][0]);
            }

            myMonthCalendar1.BoldedDates = dates;

            myMonthCalendar1.UpdateBoldedDates();
        }
    }

    public class MyMonthCalendar : MonthCalendar
    {
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);
        protected override void OnHandleCreated(EventArgs e)
        {
            SetWindowTheme(Handle, string.Empty, string.Empty);
            base.OnHandleCreated(e);
        }

       
    }
}


