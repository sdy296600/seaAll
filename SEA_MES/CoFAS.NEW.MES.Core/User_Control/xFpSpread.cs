using FarPoint.Win.Spread;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Collections.Generic;
using CoFAS.NEW.MES.Core.Entity;

namespace CoFAS.NEW.MES.Core
{
    public partial class xFpSpread : FpSpread
    {
        public string _menu_name { get; set; }
        public string _user_account { get; set; }

        public List<CheckBoxCell_Yn> checkBoxCell_YNs = new List<CheckBoxCell_Yn>();

        public List<xFpSpread_Entity> _list = new List<xFpSpread_Entity>();
        public xFpSpread()
        {
            InitializeComponent();
            this.Change += _ChangeEvent;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.Font = new Font("맑은 고딕", 9);
            this.ButtonClicked += _EditorNotifyEvent;

        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public event ChangeEventHandler _ChangeEventHandler;
        public event SelectionChangedEventHandler _SelectionChangedEventHandler;
        public event EditorNotifyEventHandler _EditorNotifyEventHandler;

        public void _ChangeEvent(object obj, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            if (_ChangeEventHandler != null)
            {
                _ChangeEventHandler(obj, e);

              
            }
        }
        public void _EditorNotifyEvent(object obj, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (_EditorNotifyEventHandler != null)
            {
                _EditorNotifyEventHandler(obj, e);


            }
        }
        public void _SelectionChangedEvent(object obj, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (_SelectionChangedEventHandler != null)
            {
                _SelectionChangedEventHandler(obj, e);


            }
        }
        private void xFpSpread_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        }

      
    
     
    }

    public class CheckBoxCell_Yn
    {
        public string Cell_Name { get; set; }

        public bool CheckBox_Yn { get; set; }
    }

}
