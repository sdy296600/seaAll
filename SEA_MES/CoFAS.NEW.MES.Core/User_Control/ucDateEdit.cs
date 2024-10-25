using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CoFAS.NEW.MES.Core
{
    public partial class ucDateEdit : DevExpress.XtraEditors.XtraUserControl // UserControl
    {

        public bool ReadOnly
        {
            get
            {
                return _pDateEdit.Properties.ReadOnly;
            }
            set
            {
                _pDateEdit.Properties.ReadOnly = value;
            }
        }
        public DateTime DateTime
        {
            get
            {
                return _pDateEdit.DateTime;
            }
            set
            {
                _pDateEdit.DateTime = value;
            }
        }

        public Font DateEdit_Font
        {
            get
            {
                return _pDateEdit.Font;
            }
            set
            {
                _pDateEdit.Font = value;
            }
        }
        public void yearView()
        {
            _pDateEdit.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
            //_pDateEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Custom;
            _pDateEdit.Properties.Mask.EditMask = "yyyy";
        }
        public DevExpress.XtraEditors.Repository.RepositoryItemDateEdit Properties
        {
            get
            {
                return _pDateEdit.Properties;
            }
        }


        private DevExpress.XtraEditors.StyleController StyleController1 = new DevExpress.XtraEditors.StyleController();

       // public event ChangeEventHandler _ChangeEventHandler;
        public ucDateEdit()
        {
            InitializeComponent();
            Initialize();
            this._pDateEdit.EditValueChanged += _pDateEdit_EditValueChanged;
            this.LookAndFeel.StyleChanged += new EventHandler(LookAndFeel_StyleChanged);
          

        }

        private void LookAndFeel_StyleChanged(object sender, EventArgs e)
        {
            StyleController1.LookAndFeel.Assign(this.LookAndFeel);
            this.LookAndFeel.Assign(this.LookAndFeel);
            this.Properties.LookAndFeel.SkinName = LookAndFeel.ActiveSkinName;
            this.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
        }

        public EventHandler _ChangeEventHandler;

        #region 초기화 하기 - Initialize()
        /// <summary>
        /// 초기화 하기
        /// </summary>
        protected virtual void Initialize()
        {
            try
            {
                _pDateEdit.Properties.AutoHeight = false;

                _pDateEdit.DateTime = DateTime.Now;
            }
            catch (Exception pException)
            {
              
            }
        }


        #endregion

        private void _pDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (_ChangeEventHandler != null)
            {
                _ChangeEventHandler(sender, e);
            }
        }
    }
}
