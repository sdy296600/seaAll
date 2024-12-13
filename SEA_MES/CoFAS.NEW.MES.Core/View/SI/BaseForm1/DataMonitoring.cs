using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Layout;

namespace CoFAS.NEW.MES.Core
{
    /// <summary>수집 데이터 모니터링 화면 입니다.</summary>
    public partial class DataMonitoring : BaseForm1
    {
        private DataTable dtBtnInfo = null;
        private DataTable mInputData = new DataTable();

        private System.Windows.Forms.Timer mTimer = null;
        private int mTimerCount = 0;
        private string mButtonLocation = "B";

        private string mWrkRstFormType = string.Empty;
        private string mLineWrkType = string.Empty;
        private string mPlantId = string.Empty;
        private string mWrkCtrId = string.Empty;
        private string mLineId = string.Empty;
        private string mLineNm = string.Empty;
        private string mWrkOrdNo = string.Empty;
        private string mRouteItemId = string.Empty;
        private string mItemId = string.Empty;
        private string mOperId = string.Empty;
        private string mWrkDt = string.Empty;
        private string mShift = string.Empty;
        private string mWrker = string.Empty;
        private string mMoldId = string.Empty;
        private string mCavity = string.Empty;
        private string mProdType = string.Empty;
        private string mEhr = string.Empty;

        Action CloseMain;
        Action CloseSub;

        #region [ Constructor & Form Events ]

        /// <summary>DataMonitoring 새 인스턴스를 초기화합니다.</summary>
        public DataMonitoring(string lineId, string lineNm, string wrkOrdNo, string wrkRstFormType, Action closeMain/*, Action<ModuleBaseManager> addSub*/, Action closeSub)
        {
            InitializeComponent();

            this.mLineId = lineId;
            this.mLineNm = lineNm;
            this.mWrkOrdNo = wrkOrdNo;
            this.mWrkRstFormType = wrkRstFormType;
            this.CloseMain = closeMain;
            //this.AddSub = addSub;
            this.CloseSub = closeSub;
        }

        #endregion

        /// <summary>Form Load 이후 발생하는 이벤트 입니다. 화면을 초기화 합니다.</summary>
        public override void Form_Show()
        {
            
        }

    }
}
