using CoFAS.NEW.MES.Core.Function;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class _LookupEdit : System.Windows.Forms.UserControl
    {
        #region ○ 이벤트 
        public event EventHandler ValueChanged;
        public event EventHandler ValueChanging;

        public event OnKeyDownEventHandler _OnKeyDown;
        public delegate void OnKeyDownEventHandler(object sender, KeyEventArgs e);

        #endregion

        public bool ReadOnly
        {
            get
            {
                return _pLookUpEdit.Properties.ReadOnly;
            }
            set
            {
                _pLookUpEdit.Properties.ReadOnly = value;
            }
        }

        public int ItemIndex
        {
            get
            {
                return _pLookUpEdit.ItemIndex;
            }
            set
            {
                _pLookUpEdit.ItemIndex = value;
            }
        }

        public DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit Properties
        {
            get
            {
                return _pLookUpEdit.Properties;
            }
        }

        public new Font Font
        {
            get
            {
                return _pLookUpEdit.Properties.Appearance.Font;
            }
            set
            {
                _pLookUpEdit.Properties.Appearance.Font = value;
                _pLookUpEdit.Properties.AppearanceDropDown.Font = value;

            }
        }

        public new Color ForeColor
        {
            get
            {
                return _pLookUpEdit.Properties.Appearance.ForeColor;
            }
            set
            {
                _pLookUpEdit.Properties.Appearance.ForeColor = value;

            }
        }

        public new string Text
        {
            get
            {
                return _pLookUpEdit.Text.ToString();
            }
            set
            {
                _pLookUpEdit.Text = value;
            }
        }

        #region ○ 생성자

        public _LookupEdit()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        protected virtual void Initialize()
        {
            try
            {
                _pLookUpEdit.Properties.AutoHeight = false;

                _pLookUpEdit.Properties.BestFitMode = BestFitMode.BestFitResizePopup; //드롭다운리스트 넓이 설정
                _pLookUpEdit.Properties.ShowHeader = false; // 헤더 표시 유무
                _pLookUpEdit.Properties.ShowFooter = false; // 푸터 표시 유무
                _pLookUpEdit.Properties.NullText = ""; // 널 값 캡션 표시

                _pLookUpEdit.EditValueChanged += new EventHandler(_pLookUpEdit_EditValueChanged);
                _pLookUpEdit.EditValueChanging += new ChangingEventHandler(_pLookUpEdit_EditValueChanging);
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

        #region LookUpEdit 선택 Value 변경시 처리하기 - _pLookUpEdit_EditValueChanged(object pSender, EventArgs pEventArgs)

        /// <summary>
        /// LookUpEdit 선택 Value 변경시 처리하기
        /// </summary>
        /// <param name="pSender">이벤트 발생자</param>
        /// <param name="pEventArgs">이벤트 인자</param>
        private void _pLookUpEdit_EditValueChanged(object pSender, EventArgs pEventArgs)
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
                    "_pLookUpEdit_EditValueChanged(object pSender, EventArgs pEventArgs)",
                    pException
                );
            }
        }

        #endregion

        #region LookUpEdit 선택 Value 변경시 처리하깅ing -
        /// <summary>
        /// LookUpEdit 선택 Value 변경시 처리하깅ing
        /// </summary>
        /// <param name="pSender"></param>
        /// <param name="pEventArgs"></param>
        private void _pLookUpEdit_EditValueChanging(object pSender, EventArgs pEventArgs)
        {
            try
            {
                if (ValueChanging != null)
                {
                    ValueChanging(this, EventArgs.Empty);
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
                    "_pLookUpEdit_EditValueChanging(object pSender, EventArgs pEventArgs)",
                    pException
                );
            }
        }
        #endregion
        public void DeleteItemByIndex(int index)
        {
            try
            {
                DataTable dt = _pLookUpEdit.Properties.DataSource as DataTable;
                dt.Rows.RemoveAt(index);
                _pLookUpEdit.Properties.DataSource = dt;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
             (
                 this,
                 "DeleteItemByIndex(int index)",
                 pException
             );

            }
        }

        #region 값 추가하기 - AddValue(DataTable _DataTable, int iRowCount, int iItemIndex, string strInitCaption)

        /// <summary>
        /// 값 추가하기
        /// </summary>
        /// <param name="dtList">데이터 리스트</param>
        /// <param name="iRowCount">표시 줄수</param>
        public void AddValue(DataTable _DataTable, int iRowCount, int iItemIndex, string strInitCaption)
        {
            try
            {
                if (_DataTable != null)
                {

                    _pLookUpEdit.Properties.DataSource = _DataTable;
                    _pLookUpEdit.Properties.ValueMember = "CD";
                    _pLookUpEdit.Properties.DisplayMember = "CD_NM";
                    _pLookUpEdit.Properties.PopulateColumns();
                    _pLookUpEdit.Properties.ForceInitialize();
                    if (_pLookUpEdit.Properties.Columns.Count != 0)
                        _pLookUpEdit.Properties.Columns[0].Visible = false;
                    _pLookUpEdit.Properties.DropDownRows = iRowCount == 0 ? _DataTable.Rows.Count : iRowCount;

                    if (_pLookUpEdit.Properties.ReadOnly)
                    {
                        _pLookUpEdit.Properties.ReadOnly = false;
                        _pLookUpEdit.ItemIndex = iItemIndex;
                        _pLookUpEdit.Properties.ReadOnly = true;
                    }
                    else
                    {
                        _pLookUpEdit.ItemIndex = iItemIndex;
                    }
                }
                else
                {
                    _pLookUpEdit.Properties.DataSource = null;
                }

                _pLookUpEdit.Properties.NullText = strInitCaption;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "AddValue(DataTable _DataTable, int iRowCount, int iItemIndex, string strInitCaption)",
                    pException
                );
            }
        }

        public void AddValue(DataTable _DataTable, int iRowCount, int iItemIndex, string strInitCaption, bool isALL)
        {
            try
            {
                if (_DataTable != null)
                {
                    _pLookUpEdit.Properties.ForceInitialize();

                    if (isALL)
                    {
                        DataRow row;

                        row = _DataTable.NewRow();
                        row["CD"] = "";
                        row["CD_NM"] = "ALL";
                        _DataTable.Rows.InsertAt(row, 0);
                    }
                    _pLookUpEdit.Properties.DataSource = _DataTable;
                    _pLookUpEdit.Properties.ValueMember = "CD";
                    _pLookUpEdit.Properties.DisplayMember = "CD_NM";

                    _pLookUpEdit.Properties.ForceInitialize();

                    _pLookUpEdit.Properties.PopulateColumns();

                    for (int i = 0; i < _pLookUpEdit.Properties.Columns.Count; i++)
                    {
                        _pLookUpEdit.Properties.Columns[i].Visible = false;
                    }

                    _pLookUpEdit.Properties.Columns["CD_NM"].Visible = true;
                    _pLookUpEdit.Properties.DropDownRows = iRowCount == 0 ? _DataTable.Rows.Count : iRowCount;

                    if (_pLookUpEdit.Properties.ReadOnly)
                    {
                        _pLookUpEdit.Properties.ReadOnly = false;
                        _pLookUpEdit.ItemIndex = iItemIndex;
                        _pLookUpEdit.Properties.ReadOnly = true;
                    }
                    else
                    {
                        _pLookUpEdit.ItemIndex = iItemIndex;
                    }
                }

                _pLookUpEdit.Properties.NullText = strInitCaption;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "AddValue(DataTable _DataTable, int iRowCount, int iItemIndex, string strInitCaption)",
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
                if (_pLookUpEdit.EditValue == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _pLookUpEdit.EditValue.ToString();
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

        public string GetDisplayName()
        {
            try
            {
                if (_pLookUpEdit.EditValue == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _pLookUpEdit.Properties.GetDisplayText(_pLookUpEdit.EditValue);
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "GetDisplayName()",
                    pException
                );
            }

        }

        //displayName으로 Value 구하기
        public void SetValueByDisplayName(string strValue)
        {
            try
            {
                if (strValue == "")
                {
                    _pLookUpEdit.EditValue = null;

                }
                else
                {
                    object key = _pLookUpEdit.Properties.GetKeyValueByDisplayText(strValue);
                    _pLookUpEdit.EditValue = key;
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "SetValue()",
                    pException
                );
            }
        }
        #endregion    

        public void SetValue(string strValue)
        {
            try
            {
                if (strValue == "")
                {
                    _pLookUpEdit.EditValue = null;

                }
                else
                {
                    _pLookUpEdit.EditValue = strValue;
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "SetValue()",
                    pException
                );
            }
        }
     
        
    }

}
