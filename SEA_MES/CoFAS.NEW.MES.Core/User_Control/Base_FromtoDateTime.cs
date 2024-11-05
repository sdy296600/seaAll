using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class Base_FromtoDateTime : System.Windows.Forms.UserControl
    {
        public Base_FromtoDateTime ()
        {
            InitializeComponent();
          
        }
        public string SearchName
        {
            get { return Control_Name.Text; }
            set { Control_Name.Text = value; }
        }
        public DateTime StartValue
        {
            get { return dtp_Start.DateTime; }
            set { dtp_End.DateTime = value; }
        }
        public DateTime EndValue
        {
            get { return dtp_End.DateTime; }
            set {dtp_End.DateTime = value; }
        }
        private void Base_FromtoDateTime_Load(object sender, EventArgs e)
        {
            DateReset();
            dtp_Start.Font = new Font("맑은 고딕", 9);
            dtp_End.Font = new Font("맑은 고딕", 9);

            dtp_Start._pDateEdit.Properties.Mask.EditMask = "yyyy-MM-dd";
            dtp_Start._pDateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;

            dtp_End._pDateEdit.Properties.Mask.EditMask = "yyyy-MM-dd";
            dtp_End._pDateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
        }
        public void DateReset()
        {
            dtp_Start.DateTime = DateTime.Parse(DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd"));
            dtp_End.DateTime = dtp_Start.DateTime.AddMonths(1).AddMinutes(-1);
            this.dtp_Start._pDateEdit.EditValueChanged += new System.EventHandler(this.dtp_Start_ValueChanged);
            this.dtp_End._pDateEdit.EditValueChanged += new System.EventHandler(this.dtp_End_ValueChanged);
        }

        private void dtp_Start_ValueChanged(object sender, EventArgs e)
        {
            if (dtp_Start.DateTime > dtp_End.DateTime)
            {
                dtp_Start.DateTime = DateTime.Parse(DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd"));
                dtp_End.DateTime = dtp_Start.DateTime.AddMonths(1).AddMinutes(-1);
                CustomMsg.ShowMessage("시작일은 종료일 보다 늦을수 없습니다.");
            }
        }

        private void dtp_End_ValueChanged(object sender, EventArgs e)
        {
            if (dtp_Start.DateTime > dtp_End.DateTime)
            {
                dtp_Start.DateTime = DateTime.Parse(DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd"));
                dtp_End.DateTime = dtp_Start.DateTime.AddMonths(1).AddMinutes(-1);
                CustomMsg.ShowMessage("종료일은 시작일 보다 빠를수 없습니다.");
            }

        }
        public void OnlyUseOneBox()
        {
            // 마지막 두 개의 열 삭제
            int columnsToRemove = 2;
            int totalColumns = tableLayoutPanel1.ColumnCount;

            for (int i = 0; i < columnsToRemove; i++)
            {
                if (totalColumns > 0)
                {
                    // 마지막 열을 삭제
                    tableLayoutPanel1.ColumnStyles.RemoveAt(totalColumns - 1); // 열 스타일 삭제
                    tableLayoutPanel1.Controls.OfType<Control>()
                        .Where(c => tableLayoutPanel1.GetColumn(c) == totalColumns - 1)
                        .ToList()
                        .ForEach(c => tableLayoutPanel1.Controls.Remove(c)); // 컨트롤 삭제
                    tableLayoutPanel1.ColumnCount--; // 열 수 감소
                    totalColumns--; // 총 열 수 업데이트
                }
            }
        }
    }
}
