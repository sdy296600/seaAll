using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class frmScheduler : Form
    {
        public UserEntity _pUserEntity = null;
        CalendarEntity _CalendarEntity = null;
        string _DateTime = string.Empty;
        public frmScheduler(string dateTime, UserEntity pUserEntity)
        {
            InitializeComponent();
            btn_delete.Enabled = false;
            _DateTime = dateTime;
            _pUserEntity = pUserEntity;
        }

        public frmScheduler(CalendarEntity CalendarEntity, UserEntity pUserEntity)
        { 

            _CalendarEntity = CalendarEntity;
            _pUserEntity = pUserEntity;
            InitializeComponent();
        }

        private void fromtoDateTime1_Load(object sender, EventArgs e)
        {
            if (_DateTime != string.Empty)
            {
                fromtoDateTime1.dtp_End.Value = DateTime.Parse(_DateTime);
                fromtoDateTime1.dtp_Start.Value = DateTime.Parse(_DateTime);
               
               
            
            }
            else
            {


                txt_title.Text                = _CalendarEntity.title       ;
                txt_content.Text              = _CalendarEntity.content     ;
                fromtoDateTime1.dtp_End.Value = _CalendarEntity.end_date;
                fromtoDateTime1.dtp_Start.Value = _CalendarEntity.start_date  ;
              
                pan_color.BackColor = Color.FromArgb(_CalendarEntity.color_R, _CalendarEntity.color_G, _CalendarEntity.color_B);

            }
            
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog color = new ColorDialog();

                if (color.ShowDialog() == DialogResult.OK)
                {
                    pan_color.BackColor = color.Color;
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private void tspTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();

                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
             
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                _CalendarEntity.CRUD = "D";
                DataTable _DataTable = new CalendarBusiness().Calendar_Info(_CalendarEntity);

                this.Close();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_title.Text.Length == 0 || txt_content.Text.Length == 0)
                {
                    CustomMsg.ShowMessage("제목과 내용을 입력해주세요.");
                    return;
                }
                CalendarEntity calendarEntity = new CalendarEntity();
                if (_DateTime != string.Empty)
                {
                    calendarEntity.CRUD = "C";
                    calendarEntity.id = 0;
                    calendarEntity.reg_date = DateTime.Now;
                    calendarEntity.reg_user = _pUserEntity.user_account;

                }
                else
                {
                    calendarEntity.CRUD = "U";
                    calendarEntity.id = _CalendarEntity.id;
                    calendarEntity.reg_date = _CalendarEntity.reg_date;
                    calendarEntity.reg_user = _CalendarEntity.reg_user;
                }

                calendarEntity.title = txt_title.Text;
                calendarEntity.content = txt_content.Text;
                calendarEntity.start_date = fromtoDateTime1.StartValue;
                calendarEntity.end_date = fromtoDateTime1.EndValue;
                calendarEntity.up_date = DateTime.Now;
                calendarEntity.up_user = _pUserEntity.user_account;
                calendarEntity.color_R = pan_color.BackColor.R;
                calendarEntity.color_G = pan_color.BackColor.G;
                calendarEntity.color_B = pan_color.BackColor.B;
                DataTable _DataTable = new CalendarBusiness().Calendar_Info(calendarEntity);

                this.Close();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
    }
}
