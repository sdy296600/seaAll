using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;

namespace CoFAS.NEW.MES.Core
{
    public partial class ucTextEdit : DevExpress.XtraEditors.XtraUserControl // UserControl
    {
        public event OnKeyDownEventHandler _OnKeyDown;
        public delegate void OnKeyDownEventHandler(object sender, KeyEventArgs e);

        public event OnKeyPressEventHandler _OnKeyPress;
        public delegate void OnKeyPressEventHandler(object sender, KeyPressEventArgs e);

        public event EventHandler _OnTextChanged;
        public delegate void TextChangedEventHandler(object pSender, EventArgs pEventArgs);

        public event EventHandler _OnMouseHover;
        public delegate void MouseHoverEventHandler(object pSender, EventArgs pEventArgs);

        public event EventHandler _OnMouseUp;
        public delegate void MouseUpEventHandler(object pSender, MouseEventArgs pEventArgs);

        /// <summary>
        /// 명령 버튼
        /// </summary>
        private SimpleButton _sbCommandButton = null;

        /// <summary>
        /// 취소 버튼
        /// </summary>
        private SimpleButton _sbCancelButton = null;

        #region 명령 버튼 - CommandButton

        /// <summary>
        /// 명령 버튼
        /// </summary>
        public SimpleButton CommandButton
        {
            get
            {
                return _sbCommandButton;
            }
            set
            {
                _sbCommandButton = value;
            }
        }

        #endregion

        #region 취소 버튼 - CancelButton

        /// <summary>
        /// 취소 버튼
        /// </summary>
        public SimpleButton CancelButton
        {
            get
            {
                return _sbCancelButton;
            }
            set
            {
                _sbCancelButton = value;
            }
        }

        #endregion

        public bool ReadOnly
        {
            get
            {
                return _pTextEdit.Properties.ReadOnly;
            }
            set
            {
                _pTextEdit.Properties.ReadOnly = value;
            }
        }

        public int MaxLength
        {
            get
            {
                return _pTextEdit.Properties.MaxLength;
            }
            set
            {
                _pTextEdit.Properties.MaxLength = value;
            }
        }
        public bool Visible
        {
            get
            {
                return _pTextEdit.Visible;
            }
            set
            {
                _pTextEdit.Visible = value;
            }
        }     
        public MaskType MaskType
        {
            get
            {
               return _pTextEdit.Properties.Mask.MaskType;
            }
            set
            {
                _pTextEdit.Properties.Mask.MaskType = value;
                //if(_pTextEdit.Properties.Mask.MaskType == MaskType.Numeric)
                //{
                //    if(_pTextEdit.Name.ToLower() == "_lucost")
                //    {
                //        _pTextEdit.Properties.MaxLength = 18;
                //    }
                //    else if(_pTextEdit.Name.ToLower().IndexOf("qty") > -1 || _pTextEdit.Name.ToLower().IndexOf("cost") > -1)
                //    {
                //        _pTextEdit.Properties.MaxLength = 9;
                //    }
                //}
            }
        }

        public string EditMask
        {
            get
            {
                return _pTextEdit.Properties.Mask.EditMask;
            }
            set
            {
                _pTextEdit.Properties.Mask.EditMask = value;
            }
        }
        
        public bool UseMaskAsDisplayFormat
        {
            get
            {
                return _pTextEdit.Properties.Mask.UseMaskAsDisplayFormat;
            }
            set
            {
                _pTextEdit.Properties.Mask.UseMaskAsDisplayFormat = value;
            }
        }

        public  char PasswordChar
        {
            get
            {
                return _pTextEdit.Properties.PasswordChar;
            }
            set
            {
                _pTextEdit.Properties.PasswordChar = value;
            }
        } 

        public new string Text
        {
            get
            {
                return _pTextEdit.Text.ToString();
            }
            set
            {
                _pTextEdit.Text = value;
            }
        }

        public string NumText
        {
            get
            {
                return _pTextEdit.Properties.OwnerEdit.EditValue.ToString();
            }
            set
            {
                _pTextEdit.Properties.OwnerEdit.EditValue = value;
            }
        }

        public DevExpress.Utils.HorzAlignment TextAlignment
        {
            get
            {
                return _pTextEdit.Properties.Appearance.TextOptions.HAlignment;
            }
            set
            {
                
                    _pTextEdit.Properties.AppearanceFocused.TextOptions.HAlignment = value;
                    _pTextEdit.Properties.Appearance.TextOptions.HAlignment = value;
               
                    _pTextEdit.Properties.AppearanceFocused.TextOptions.HAlignment = value;
                    _pTextEdit.Properties.Appearance.TextOptions.HAlignment = value;
            }
        }

        public DevExpress.XtraEditors.Repository.RepositoryItemTextEdit Properties
        {
            get
            {
                return _pTextEdit.Properties;
            }
        }

        public new Font Font
        {
            get
            {
                return _pTextEdit.Properties.Appearance.Font;
            }
            set
            {
                _pTextEdit.Properties.Appearance.Font = value;

            }
        }

        public new Color ForeColor
        {
            get
            {
                return _pTextEdit.Properties.Appearance.ForeColor;
            }
            set
            {
                _pTextEdit.Properties.Appearance.ForeColor = value;

            }
        }

        public new Color BackColor
        {
            get
            {
                return _pTextEdit.Properties.Appearance.BackColor;
            }
            set
            {
                _pTextEdit.Properties.Appearance.BackColor = value;

            }
        }
        public string ToolTipt
        {
            get
            {
                return _pTextEdit.ToolTip;
            }
            set
            {
               _pTextEdit.ToolTip = value;

            }
        }


        private DevExpress.XtraEditors.StyleController StyleController1 = new DevExpress.XtraEditors.StyleController();
        public ucTextEdit()
        {
            InitializeComponent();

            Initialize();
            _pTextEdit.KeyDown += new KeyEventHandler(_pTextEdit_KeyDown);
            _pTextEdit.KeyPress += new KeyPressEventHandler(_pTextEdit_KeyPress);
            _pTextEdit.TextChanged += new EventHandler(_pTextEdit_TextChanged);
            _pTextEdit.MouseHover += new EventHandler(_pTextEdit_MouseHover);
            _pTextEdit.DoubleClick += new EventHandler(_pTextEdit_DoubleClick);
            _pTextEdit.MouseUp += new MouseEventHandler(_pTextEdit_MouseUp);
            _pTextEdit.MouseDown += new MouseEventHandler(_pTextEdit_MouseDown);

            //_pTextEdit.Select(_pTextEdit.Text.Length, 2);
            this.LookAndFeel.StyleChanged += new EventHandler(LookAndFeel_StyleChanged);
        }

        private void LookAndFeel_StyleChanged(object sender, EventArgs e)
        {
            StyleController1.LookAndFeel.Assign(this.LookAndFeel);
            this.LookAndFeel.Assign(this.LookAndFeel);
            _pTextEdit.LookAndFeel.SkinName = LookAndFeel.ActiveSkinName;
            _pTextEdit.LookAndFeel.UseDefaultLookAndFeel = false;

        }




        private void _pTextEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (_OnKeyDown != null)
            {
               if(e.KeyCode == Keys.Delete)
                {
                    _pTextEdit.Text = "";
                }
                    
                _OnKeyDown(this, e);
            }
        }

        private void _pTextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_OnKeyPress != null)
                _OnKeyPress(this, e);
        }

        private void _pTextEdit_TextChanged(object pSender, EventArgs pEventArgs)
        {
            if (_OnTextChanged != null)
                _OnTextChanged(this, pEventArgs);
        }

        private void _pTextEdit_MouseHover(object sender, EventArgs pEventArgs)
        {
            if (_OnMouseHover != null)
                _OnMouseHover(this, pEventArgs);
        }

        private void _pTextEdit_DoubleClick(object sender, EventArgs e)
        {
            ((TextEdit)sender).Focus();
            ((TextEdit)sender).SelectAll();
        }

        private void _pTextEdit_MouseUp(object sender, EventArgs pEventArgs)
        {
            if (_OnMouseUp != null)
                _OnMouseUp(this, pEventArgs);
        }

        private void _pTextEdit_MouseDown(object sender, EventArgs pEventArgs)
        {
            //((TextEdit)sender).Focus();
            //((TextEdit)sender).SelectAll();
            //if(_pTextEdit.EditorTypeName == "")
            //{
           // _pTextEdit.SelectAll();
                //_pTextEdit.Select(_pTextEdit.Text.Length, 2);
            //}
        }

        #region 초기화 하기 - Initialize()

        /// <summary>
        /// 초기화 하기
        /// </summary>
        protected virtual void Initialize()
        {
            try
            {
                _pTextEdit.Properties.AutoHeight = false;
                NumText = "";
                
            }
            catch (Exception pException)
            {

            }
        }
        #endregion
    }
}
