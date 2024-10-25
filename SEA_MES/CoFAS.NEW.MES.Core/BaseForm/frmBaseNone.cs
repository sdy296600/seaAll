using CoFAS.NEW.MES.Core.Entity;
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
    public partial class frmBaseNone : Form, IForm
    {
        #region .. Double Buffered function ..
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;
            System.Reflection.PropertyInfo aProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }

        #endregion

        #region .. code for Flucuring ..

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
        public delegate void MessageEventHandler(string pMessage);
        public event MessageEventHandler MessageEvent;

        public MenuSettingEntity _pMenuSettingEntity = null;

        #region 메인 폼 - MainForm

        /// <summary>
        /// 메인 폼
        /// </summary>
        public IMainForm MainForm
        {
            get
            {
                return MdiParent as IMainForm;
            }
        }

        #endregion

        #region 윈도우명 - WindowName

        /// <summary>
        /// 윈도우명
        /// </summary>
        public string WindowName
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }

        #endregion

        #region Event

        /// <summary>
        /// 닫기 버튼 클릭시 
        /// </summary>
        public event EventHandler CloseButtonClicked;

        /// <summary>
        /// 내보내기 버튼 클릭시
        /// </summary>
        public event EventHandler ExportButtonClicked;

        /// <summary>
        /// 가져오기 버튼 클릭시
        /// </summary>
        public event EventHandler ImportButtonClicked;

        /// <summary>
        /// 인쇄 버튼 클릭시
        /// </summary>
        public event EventHandler PrintButtonClicked;

        /// <summary>
        /// 저장 버튼 클릭시
        /// </summary>
        public event EventHandler SaveButtonClicked;

        /// <summary>
        /// 삭제 버튼 클릭시
        /// </summary>
        public event EventHandler DeleteButtonClicked;

        /// <summary>
        /// 조회 버튼 클릭시
        /// </summary>
        public event EventHandler SearchButtonClicked;

        /// <summary>
        /// 초기화 버튼 클릭시
        /// </summary>
        public event EventHandler InitialButtonClicked;

        /// <summary>
        /// 설정 버튼 클릭시
        /// </summary>
        public event EventHandler SettingButtonClicked;

        /// <summary>
        /// 신규추가 버튼 클릭시
        /// </summary>
        public event EventHandler AddItemButtonClicked;
        #endregion

        #region 메뉴 설정 개체 - MenuSettingEntity

        /// <summary>
        /// 메뉴 설정 개체
        /// </summary>
        public MenuSettingEntity MenuSettingEntity
        {
            get
            {
                return _pMenuSettingEntity;
            }
            set
            {
                _pMenuSettingEntity = value;
            }
        }

        #endregion

        public frmBaseNone()
        {
            InitializeComponent();
           
        }

        #region 닫기 버튼 클릭시 이벤트 발생시키기 - RaiseExitButtonClickedEvent()

        /// <summary>
        /// 닫기 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        public void RaiseFormCloseButtonClickedEvent()
        {
            if (CloseButtonClicked != null)
            {
                CloseButtonClicked(this, EventArgs.Empty);
            }
        }

        #endregion

        #region 내보내기 버튼 클릭시 이벤트 발생시키기 - RaiseExportButtonClickedEvent()

        /// <summary>
        /// 내보내기 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        public void RaiseExportButtonClickedEvent()
        {
            if (ExportButtonClicked != null)
            {
                ExportButtonClicked(this, EventArgs.Empty);
            }
        }

        #endregion

        #region 가져오기 버튼 클릭시 이벤트 발생시키기 - RaiseImportButtonClickedEvent()

        /// <summary>
        /// 가져오기 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        public void RaiseImportButtonClickedEvent()
        {
            if (ImportButtonClicked != null)
            {
                ImportButtonClicked(this, EventArgs.Empty);
            }
        }

        #endregion

        #region 인쇄 버튼 클릭시 이벤트 발생시키기 - RaisePrintButtonClickedEvent()

        /// <summary>
        /// 인쇄 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        public void RaisePrintButtonClickedEvent()
        {
            if (PrintButtonClicked != null)
            {
                PrintButtonClicked(this, EventArgs.Empty);
            }
        }

        #endregion

        #region 삭제 버튼 클릭시 이벤트 발생시키기 - RaiseDeleteButtonClickedEvent()

        /// <summary>
        /// 삭제 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        public void RaiseDeleteButtonClickedEvent()
        {
            if (DeleteButtonClicked != null)
            {
                DeleteButtonClicked(this, EventArgs.Empty);
            }
        }

        #endregion

        #region 저장 버튼 클릭시 이벤트 발생시키기 - RaiseSaveButtonClickedEvent()

        /// <summary>
        /// 저장 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        public void RaiseSaveButtonClickedEvent()
        {
            if (SaveButtonClicked != null)
            {
                SaveButtonClicked(this, EventArgs.Empty);
            }
        }

        #endregion

        #region 조회 버튼 클릭시 이벤트 발생시키기 - RaiseFindButtonClickedEvent()

        /// <summary>
        /// 조회 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        public void RaiseSearchButtonClickedEvent()
        {
            if (SearchButtonClicked != null)
            {
                SearchButtonClicked(this, EventArgs.Empty);
            }
        }

        #endregion

        #region 초기화 버튼 클릭시 이벤트 발생시키기 - RaiseInitialButtonClickedEvent()

        /// <summary>
        /// 초기화 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        public void RaiseInitialButtonClickedEvent()
        {
            if (InitialButtonClicked != null)
            {
                InitialButtonClicked(this, EventArgs.Empty);
            }
        }

        #endregion

        #region 설정 버튼 클릭시 이벤트 발생시키기 - RaiseSettingButtonClickedEvent()

        /// <summary>
        /// 설정 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        public void RaiseSettingButtonClickedEvent()
        {
            if (SettingButtonClicked != null)
            {
                SettingButtonClicked(this, EventArgs.Empty);
            }
        }

        #endregion

        #region 신규추가 버튼 클릭시 이벤트 발생시키기 - RaiseAddItemButtonClickedEvent()

        /// <summary>
        /// 신규추가 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        public void RaiseAddItemButtonClickedEvent()
        {
            if (AddItemButtonClicked != null)
            {
                AddItemButtonClicked(this, EventArgs.Empty);
            }
        }

        #endregion

        #region 메시지 표시하기

        /// <summary>
        /// 메시지를 status에 표시한다
        /// </summary>
        /// <param name="pMessage"></param>
        protected void DisplayMessage(string pMessage)
        {
            DateTime dt = DateTime.Now;
            if (MessageEvent != null)
            {
                MessageEvent(string.Format("[{0}:{1}:{2}] {3}\r\n", dt.ToString("HH"), dt.ToString("mm"), dt.ToString("ss"), pMessage));
            }
        }


        #endregion

        private void _pnContent_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        }

        
    }
}
