using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using DevExpress.Utils;
using CoFAS.NEW.MES.Core.Function;
using DevExpress.XtraEditors;

namespace CoFAS.NEW.MES.Core
{
    public partial class _xComboBox : System.Windows.Forms.UserControl
    {
        public event EventHandler ValueChanged;
        private bool _bInitialized_XComboBox = false;

        public _xComboBox()
        {
            InitializeComponent();
            Initialize();
            _pComboBoxEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        }

        #region Field

        /// <summary>
        /// 자동 갱신 여부
        /// </summary>
        protected bool _bAutoRefresh = false;

        /// <summary>
        /// 값 목록
        /// </summary>
        protected Dictionary<string, string> _dictValue = new Dictionary<string, string>();

        /// <summary>
        /// 명칭 목록
        /// </summary>
        protected Dictionary<string, string> _dictName = new Dictionary<string, string>();

        #endregion

        #region 내부 컨트롤 - InnerControl

        /// <summary>
        /// 내부 컨트롤
        /// </summary>
        public ComboBoxEdit InnerControl
        {
            get
            {
                return _pComboBoxEdit;
            }
            set
            {
                _pComboBoxEdit = value;
            }
        }

        #endregion

        #region 읽기 전용 여부 - UseReadOnly

        /// <summary>
        /// 읽기 전용 여부
        /// </summary>
        public bool UseReadOnly
        {
            get
            {
                return _pComboBoxEdit.Properties.ReadOnly;
            }
            set
            {
                _pComboBoxEdit.Properties.ReadOnly = value;
            }
        }

        #endregion

        #region 자동 갱신 여부 - AutoRefresh

        /// <summary>
        /// 자동 갱신 여부
        /// </summary>
        public bool AutoRefresh
        {
            get
            {
                return _bAutoRefresh;
            }
            set
            {
                if (_bAutoRefresh == value)
                {
                    return;
                }
                _bAutoRefresh = value;
                if (_bAutoRefresh)
                {
                    FillList();
                }
            }
        }

        #endregion


        #region 값 목록 - Values

        /// <summary>
        /// 값 목록
        /// </summary>
        public Dictionary<string, string> Values
        {
            get
            {
                return _dictValue;
            }
        }

        #endregion

        #region 명칭 목록 - Names

        /// <summary>
        /// 명칭 목록
        /// </summary>
        public Dictionary<string, string> Names
        {
            get
            {
                return _dictName;
            }
        }

        #endregion


        #region 값 - Value

        /// <summary>
        /// 값
        /// </summary>
        public string Value
        {
            get
            {
                return GetValue();
            }
            set
            {
                SetValue(value);
            }
        }

        #endregion


        #region 값 - SelectedIndex

        /// <summary>
        /// 선택값 index
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                return _pComboBoxEdit.SelectedIndex;
            }
            set
            {
                _pComboBoxEdit.SelectedIndex = value;
            }
        }

        #endregion

        #region 데이타 지우기 - ClearData()

        /// <summary>
        /// 데이타 지우기
        /// </summary>
        public void ClearData()
        {
            try
            {
                _dictValue.Clear();
                _dictName.Clear();
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "ClearData()",
                    pException
                );
            }
        }

        #endregion

        #region 컨트롤 지우기 - ClearControl()

        /// <summary>
        /// 컨트롤 지우기
        /// </summary>
        public void ClearControl()
        {
            try
            {
                _dictValue.Clear();
                _dictName.Clear();
                _pComboBoxEdit.Properties.Items.Clear();
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "ClearControl()",
                    pException
                );
            }
        }

        #endregion


        #region 값 추가하기 - AddValue(strValue, strName)

        /// <summary>
        /// 값 추가하기
        /// </summary>
        /// <param name="strValue">값</param>
        /// <param name="strName">명칭</param>
        public void AddValue(string strValue, string strName)
        {
            try
            {
                _dictValue.Add(strValue, strName);
                _dictName.Add(strName, strValue);
                _pComboBoxEdit.Properties.Items.Add(strName);
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "AddValue(strValue, strName)",
                    pException
                );
            }
        }

        /// <summary>
        /// 값 추가하기
        /// </summary>
        /// <param name="dtData"></param>
        public void AddValue(DataTable dtData)
        {
            try
            {
                if (dtData == null) return;
                string strValue;
                string strName;
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    strValue = dtData.Rows[i][0].ToString().Trim();
                    strName = dtData.Rows[i][1].ToString().Trim();
                    _dictValue.Add(strValue, strName);
                    _dictName.Add(strName, strValue);
                    _pComboBoxEdit.Properties.Items.Add(strName);
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "AddValue(strValue, strName)",
                    pException
                );
            }
        }

        #endregion

        #region 목록 채우기 - FillList()

        /// <summary>
        /// 목록 채우기
        /// </summary>
        public virtual void FillList()
        {
            try
            {
                ClearData();
                ClearControl();
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "FillList()",
                    pException
                );
            }
        }

        #endregion


        #region 값 설정하기 - SetValue(strValue)

        /// <summary>
        /// 값 설정하기
        /// </summary>
        /// <param name="strValue">값</param>
        public void SetValue(string strValue)
        {
            try
            {
                if (_dictValue.ContainsKey(strValue))
                {
                    _pComboBoxEdit.SelectedItem = _dictValue[strValue];
                }
                else
                {
                    _pComboBoxEdit.SelectedItem = null;
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "SetValue(strValue)",
                    pException
                );
            }
        }

        #endregion

        #region 값 설정하기 - SetValue(strName)

        /// <summary>
        /// 값 설정하기
        /// </summary>
        /// <param name="strName">명칭</param>
        public void SetName(string strName)
        {
            try
            {
                if (_dictName.ContainsKey(strName))
                {
                    _pComboBoxEdit.SelectedItem = strName;
                }
                else
                {
                    _pComboBoxEdit.SelectedItem = null;
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "SetValue(strName)",
                    pException
                );
            }
        }

        #endregion

        #region 값 구하기 - GetValue()

        /// <summary>
        /// 값 구하기
        /// </summary>
        /// <returns>값</returns>
        public string GetValue()
        {
            try
            {
                if (_pComboBoxEdit.SelectedItem == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _dictName[_pComboBoxEdit.SelectedItem as string];
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "GetValue()",
                    pException
                );
            }
        }

        #endregion

        #region 명칭 구하기 - GetName()

        /// <summary>
        /// 명칭 구하기
        /// </summary>
        /// <returns>명칭</returns>
        public string GetName()
        {
            try
            {
                if (_pComboBoxEdit.SelectedItem == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _pComboBoxEdit.SelectedItem as string;
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "GetName()",
                    pException
                );
            }
        }

        #endregion

        #region 초기화 하기 - Initialize()

        /// <summary>
        /// 초기화 하기
        /// </summary>
        protected virtual void Initialize()
        {
            try
            {
                if (!_bInitialized_XComboBox)
                {
                    // 폰트
                    _pComboBoxEdit.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
                    _pComboBoxEdit.Properties.AppearanceDisabled.Font = new System.Drawing.Font("맑은 고딕", 9F);
                    _pComboBoxEdit.Properties.AppearanceFocused.Font = new System.Drawing.Font("맑은 고딕", 9F);
                    _pComboBoxEdit.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("맑은 고딕", 9F);

                    _pComboBoxEdit.Properties.Appearance.Options.UseTextOptions = true;
                    _pComboBoxEdit.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Default;
                    _pComboBoxEdit.Properties.Appearance.TextOptions.VAlignment = VertAlignment.Center;

                    _pComboBoxEdit.Properties.AppearanceDisabled.BackColor = Color.FromArgb(224, 224, 224);
                    _pComboBoxEdit.Properties.AppearanceDisabled.Options.UseBackColor = true;
                    _pComboBoxEdit.Properties.AppearanceFocused.BackColor = Color.FromArgb(255, 255, 128);
                    _pComboBoxEdit.Properties.AppearanceFocused.Options.UseBackColor = true;
                    _pComboBoxEdit.Properties.AppearanceReadOnly.BackColor = Color.FromArgb(224, 224, 224);
                    _pComboBoxEdit.Properties.AppearanceReadOnly.Options.UseBackColor = true;

                    _pComboBoxEdit.SelectedIndexChanged += new EventHandler(_pComboBoxEdit_SelectedIndexChanged);
                    _bInitialized_XComboBox = true;
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "Initialize()",
                    pException
                );
            }
        }

        #endregion

        #region ComboBoxEdit 선택 인덱스 변경시 처리하기 - _pComboBoxEdit_SelectedIndexChanged(pSender, pEventArgs)

        /// <summary>
        /// ComboBoxEdit 선택 인덱스 변경시 처리하기
        /// </summary>
        /// <param name="pSender">이벤트 발생자</param>
        /// <param name="pEventArgs">이벤트 인자</param>
        private void _pComboBoxEdit_SelectedIndexChanged(object pSender, EventArgs pEventArgs)
        {
            try
            {
                if (ValueChanged != null)
                {
                    ValueChanged(this, EventArgs.Empty);
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "_pComboBoxEdit_SelectedIndexChanged(pSender, pEventArgs)",
                    pException
                );
            }
        }

        #endregion
    }
}
