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
    /// <summary>박스 포장 등록 화면 입니다.</summary>
    public partial class DATA_MON : BaseForm1
    {
        private DataTable dtBtnInfo = null;
        private DataTable mInputData = new DataTable();

        private System.Windows.Forms.Timer mTimer = null;
        private int mTimerCount = 0;
        private string mButtonLocation = "B";

        private string mWrkRstFormType  = string.Empty;
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
        Action<ModuleBaseManager> AddSub;
        Action CloseSub;

        #region [ Constructor & Form Events ]

        /// <summary>POP_MAIN_BOX 새 인스턴스를 초기화합니다.</summary>
        public DATA_MON(string lineId, string lineNm, string wrkOrdNo, string wrkRstFormType, Action closeMain, Action<ModuleBaseManager> addSub, Action closeSub)
        {
            InitializeComponent();

            this.mLineId = lineId;
            this.mLineNm = lineNm;
            this.mWrkOrdNo = wrkOrdNo;
            this.mWrkRstFormType = wrkRstFormType;
            this.CloseMain = closeMain;
            this.AddSub = addSub;
            this.CloseSub = closeSub;
        }

        /// <summary>Form Load 이후 발생하는 이벤트 입니다. 화면을 초기화 합니다.</summary>
        public override void Form_Show()
        {
            try
            {
                base.Form_Show();

                gcItemList.Paint -= gcItemList.IdatGridControl_Paint;
                gvItemList.InitView();

                // 버튼 정보 조회
                ReturnDataStructure result = base.mDataProcess.ExcuteProcedure( "PPADM.GET_WRK_RST_BTN"
                                                                              , 1
                                                                              , new string[] { "A_LANG"
                                                                                             , "A_CLIENT_CD"
                                                                                             , "A_WRK_RST_FORM_TYPE" }
                                                                              , new object[] { Global.UserInfo.Language
                                                                                             , Global.CompanyInfo.Client
                                                                                             , this.mWrkRstFormType }
                                                                              );

                if (result.MessageBoxResult() == false)
                    return;

                this.dtBtnInfo = result.ReturnDataSet.Tables[0];
                this.flowLayoutPanel.Controls.Clear();
                foreach (DataRow dr in result.ReturnDataSet.Tables[0].Rows)     // 버튼 추가
                {
                    this.AddWrkBtn(this.CreateWrkBtn(dr["WRK_RST_FORM_BTN"].NullString(), dr["BTN_NM"].NullString()));
                }
                this.flowLayoutPanel.Controls.Add(this.btnLabelPrint);  // 공통적인 버튼이 아니라 클리어 후 다시 추가.
                // 버튼 유형이 없어서 btn_click에서 오류 발생하니, 유형 추가
                DataRow drNew = this.dtBtnInfo.NewRow();
                drNew["WRK_RST_FORM_BTN"] = "LABEL";
                this.dtBtnInfo.Rows.Add(drNew);
                this.dtBtnInfo.AcceptChanges();

                this.BcdControlsClear();
                this.ControlsClear();

                this.mPlantId = Global.LastWorkInfo.PlantId;
                this.mWrkCtrId = Global.LastWorkInfo.WrkCtrId;

                this.GetWorkInfo(this.mLineId, this.mWrkOrdNo);

                this.Disposed += (s, e) =>
                {
                    if (mTimer != null)
                        mTimer.Enabled = false;
                };

                //timer
                mTimer = new System.Windows.Forms.Timer();
                mTimer.Tick += mTimer_Tick;
                mTimer.Interval = 1000;    // 30초
                mTimer.Enabled = true;
                mTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBoxPanel.Show(ex.ToString(), MessageType.Warning);
            }
        }

        private void mTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //mTimer.Stop();
                //mTimer.Enabled = false;

                //this.mTimerCount++;

                //if (this.InvokeRequired)
                //{
                //    this.Invoke(new MethodInvoker(delegate
                //    {
                //        if (this.mTimerCount == 30)
                //        {

                //        }
                //    }));
                //}
                //else
                //{
                //    if (this.mTimerCount == 30)
                //    {

                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBoxPanel.Show(ex.ToString(), MessageType.Warning);
            }
            finally
            {
                //mTimer.Enabled = true;
                //mTimer.Start();
            }
        }

        #endregion



        #region [ Button Events ]

        /// <summary>서브 - 하단 작업 버튼</summary>
        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                string wrkType = btn.Tag.NullString();
                DataRow dr = this.dtBtnInfo.Select($"WRK_RST_FORM_BTN = '{wrkType}'")[0];

                Action<string> act0 = (wrkord) =>
                {
                    this.mWrkOrdNo = wrkord;
                    this.GetWorkInfo(this.mLineId, this.mWrkOrdNo);

                    this.CloseSub?.Invoke();

                    if (this.mInputData.Rows.Count > 0)
                    {
                        base.mDataProcess.SetGrid(gcItemList
                                         , this.mInputData
                                         , false
                                         , "ITEM_ID, PART_NO, MODEL, OPER"
                                         );

                        gvItemList.Columns["BCD"].Width = 140;
                        gvItemList.Columns["PART_NM"].Width = 220;
                        gvItemList.Columns["SPEC"].Width = 160;
                        gvItemList.Columns["LOT"].Width = 140;
                        gvItemList.Columns["STK_QTY"].Width = 100;

                        gvItemList.Columns["BCD"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvItemList.Columns["LOT"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvItemList.Columns["STK_QTY"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    }

                    this.txtSubBcdBarcode.Focus();
                    this.txtSubBcdBarcode.SelectAll();

                    mTimer.Enabled = true;
                    mTimer.Start();
                };

                object[] args;

                args = new object[] { this.mPlantId
                                    , this.mWrkCtrId
                                    , this.mLineId
                                    , this.mWrkOrdNo
                                    , this.mRouteItemId
                                    , this.mItemId
                                    , this.mOperId
                                    , this.mWrkDt
                                    , this.mShift
                                    , this.mWrker
                                    , this.mMoldId
                                    , this.mCavity
                                    , this.mProdType
                                    , act0 };

                this.mInputData = (gcItemList.DataSource as DataTable).Copy();

                switch (btn.Tag.NullString())
                {
                    case "WRKORD":      // 작업지시 선택
                        if (string.IsNullOrWhiteSpace(this.mWrker.NullString()) && string.IsNullOrWhiteSpace(this.mEhr.NullString()))
                        {
                            // 인원이 등록되지 않았습니다. 작업인원을 먼저 등록하세요.
                            MessageBoxPanel.Show("MSG_ERR_603".GetMessageStr(), MessageType.Warning);
                            return;
                        }

                        this.OpenPopUp(dr["FORM_NS"].NullString(), dr["FORM_CD"].NullString(), args);
                        break;

                    case "WRKER":       // 작업자 선택
                    case "DOWNTIME":    // 비가동 실적
                    case "RSTLIST":     // 실적 조회
                        this.OpenPopUp(dr["FORM_NS"].NullString(), dr["FORM_CD"].NullString(), args);
                        break;
                    case "LABEL":       // 라벨발행
                        this.PrintBoxBarcode();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBoxPanel.Show(ex.ToString(), MessageType.Warning);
            }
        }

        #endregion



        #region [ Controls Events ]

        /// <summary>타이틀 클릭 - 이전화면 이동</summary>
        private void lblSubLine_Click(object sender, EventArgs e)
        {
            try
            {
                this.CloseMain?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBoxPanel.Show(ex.ToString(), MessageType.Warning);
            }
        }

        /// <summary>바코드 엔터</summary>
        private void txtSubBcdBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.InputBarcode(this.txtSubBcdBarcode.EditValue.NullString(), this.tgsSubBcdCancel.IsOn ? "C" : "I");
        }

        private void lblSubTime_Click(object sender, EventArgs e)
        {
            Modules.PP.POP_POPUP_BTN_LOC pop = new Modules.PP.POP_POPUP_BTN_LOC();

            pop.StartPosition = FormStartPosition.CenterParent;
            pop.ButtonLocation = this.mButtonLocation;
            pop.ShowDialog();

            switch (pop.ButtonLocation)
            {
                case "T":
                    flowLayoutPanel.Size = new Size(this.Width, 90);
                    flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
                    flowLayoutPanel.BringToFront();
                    layoutControl3.BringToFront();
                    break;

                case "B":
                    flowLayoutPanel.Size = new Size(this.Width, 90);
                    flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
                    flowLayoutPanel.BringToFront();
                    layoutControl3.BringToFront();
                    break;

                case "L":
                    flowLayoutPanel.Size = new Size(166, 919);
                    flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Left;
                    flowLayoutPanel.BringToFront();
                    layoutControl3.BringToFront();
                    break;

                case "R":
                    flowLayoutPanel.Size = new Size(166, 919);
                    flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Right;
                    flowLayoutPanel.BringToFront();
                    layoutControl3.BringToFront();
                    break;

                default:
                    flowLayoutPanel.Size = new Size(this.Width, 90);
                    flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
                    flowLayoutPanel.BringToFront();
                    layoutControl3.BringToFront();
                    break;
            }

            this.mButtonLocation = pop.ButtonLocation;
        }

        #endregion



        #region [ Private Function / Procedure ]

        /// <summary>바코드 관련 컨트롤 초기화</summary>
        private void BcdControlsClear()
        {
            this.txtSubBcdBarcode.EditValue = null;
            this.lblSubBcdPartNo.Text = string.Empty;
            this.lblSubBcdPartNm.Text = string.Empty;
            this.lblSubBcdModel.Text = string.Empty;
            this.lblSubBcdSpec.Text = string.Empty;
            this.lblSubBcdLot.Text = string.Empty;
            this.lblSubBcdQty.Text = string.Empty;
        }

        /// <summary>컨트롤 초기화</summary>
        private void ControlsClear()
        {
            try
            {
                this.lblSubLine.Text = string.Empty;
                this.lblSubOper.Text = string.Empty;
                this.lblSubSpec.Text = string.Empty;
                this.lblSubInput.Tag = null;
                this.lblSubInput.Text = string.Empty;
                this.lblSubTime.Text = string.Empty;

                this.lblSubWrkOrdNo.Text = string.Empty;
                this.lblSubPartNo.Tag = null;
                this.lblSubPartNo.Text = string.Empty;
                this.lblSubPartNm.Text = string.Empty;
                this.lblSubStartTime.Text = string.Empty;
                this.lblSubModel.Text = string.Empty;
                this.lblSubLineStatus.Text = string.Empty;

                this.chtSubDownTimeState.Series.Clear();

                this.gageRangeWrkSingl.Value = 0;
                this.gageTextWrkSingl.Text = string.Empty;

                this.lblSubOrdQty.Text = string.Empty;
                this.lblSubInputQty.Text = string.Empty;
                this.lblSubPackQty.Text = string.Empty;
                this.lblSubWrkTime.Text = string.Empty;
                this.lblSubOperatingTime.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBoxPanel.Show(ex.ToString(), MessageType.Warning);
            }
        }

        /// <summary>작업정보 조회</summary>
        /// <param name="lineId">작업 라인 ID</param>
        /// <param name="wrkOrdNo">작업지시 번호</param>
        private void GetWorkInfo(string lineId, string wrkOrdNo)
        {
            try
            {
                // PP11050.GET_WORK_INFO
                Dictionary<string, object> dic = new Dictionary<string, object>()
                {
                    ["A_LANG"] = Global.UserInfo.Language,
                    ["A_CLIENT_CD"] = Global.CompanyInfo.Client,
                    ["A_PLANT_ID"] = this.mPlantId,
                    ["A_WRK_CTR_ID"] = this.mWrkCtrId,
                    ["A_LINE_ID"] = lineId,
                    ["A_WRK_ORD_NO"] = wrkOrdNo,
                    ["A_INCLUDE_STD_DT_FLAG"] = "N"
                };

                ReturnDataStructure result = base.mDataProcess.ExcuteProcedure("POP_MAIN_BOX.GET_WORK_INFO"
                                                                              , 1
                                                                              , dic
                                                                              );

                this.ControlsClear();
                if (result.MessageBoxResult(ProjectType.InputPanel) == false)
                    return;

                this.BcdControlsClear();
                if (result.ReturnDataSet.Tables[0].Rows.Count > 0)
                {
                    double planValue = 0;       // 계획 HPU
                    double runTime = 0;         // 가동시간
                    double resultValue = 0;     // 실적수량

                    DataTable dt = result.ReturnDataSet.Tables[0];

                    planValue = dt.Rows[0]["HPU"].ToDouble();
                    resultValue = dt.Rows[0]["PRD_QTY"].ToDouble();
                    runTime = dt.Rows[0]["RUN_TIME"].ToDouble();

                    this.lblSubLine.Tag = dt.Rows[0]["LINE_ID"].NullString();
                    this.lblSubLine.Text = dt.Rows[0]["LINE_NM"].NullString();

                    this.lblSubWrkOrdNo.Text = dt.Rows[0]["WRK_ORD_NO"].NullString();
                    this.lblSubPartNo.Text = dt.Rows[0]["PART_NO"].NullString();
                    this.lblSubPartNm.Text = dt.Rows[0]["PART_NM"].NullString();
                    this.lblSubStartTime.Text = dt.Rows[0]["START_TIME"].NullString();
                    this.lblSubSpec.Text = dt.Rows[0]["SPEC"].NullString();
                    this.lblSubModel.Text = dt.Rows[0]["MODEL"].NullString();
                    this.lblSubPackQty.Text = dt.Rows[0]["PRD_QTY"].ToDouble().ToString("#,##0");

                    this.lblSubLineStatus.Text = dt.Rows[0]["LINE_STATE"].NullString();

                    if (dt.Rows[0]["LINE_STATE_CODE"].NullString() == "LS000") //작업대기
                        this.lblSubLineStatus.ForeColor = Color.Gray;
                    else if (dt.Rows[0]["LINE_STATE_CODE"].NullString() == "LS001") //가동중
                        this.lblSubLineStatus.ForeColor = Color.Lime;
                    else if (dt.Rows[0]["LINE_STATE_CODE"].NullString() == "LS002") //비가동
                        this.lblSubLineStatus.ForeColor = Color.Orange;

                    this.lblSubOrdQty.Text = dt.Rows[0]["WRK_ORD_QTY"].ToDouble().ToString("#,##0");
                    this.lblSubPackQty.Text = dt.Rows[0]["GOOD_QTY"].ToDouble().ToString("#,##0");
                    this.lblSubWrkTime.Text = dt.Rows[0]["WRK_TIME"].ToDouble().ToString("#,##0");
                    this.lblSubOperatingTime.Text = dt.Rows[0]["RUN_TIME"].ToDouble().ToString("#,##0");

                    this.gageRangeWrkSingl.Value = dt.Rows[0]["WRK_RATIO"].ToInt();
                    this.gageTextWrkSingl.Text = dt.Rows[0]["WRK_RATIO"].NullString() + "%";

                    this.mOperId = dt.Rows[0]["OPER_ID"].NullString();
                    this.mItemId = dt.Rows[0]["ITEM_ID"].NullString();
                    this.mWrkDt = dt.Rows[0]["WRK_DT"].NullString();
                    this.mShift = dt.Rows[0]["SHIFT"].NullString();
                    this.mMoldId = dt.Rows[0]["MOLD_ID"].NullString();
                    this.mCavity = dt.Rows[0]["CAVITY"].NullString();
                    this.mProdType = dt.Rows[0]["PROD_TYPE"].NullString();
                }
                else
                {
                    this.lblSubLine.Tag = this.mLineId;
                    this.lblSubLine.Text = this.mLineNm;

                    this.mItemId = string.Empty;
                    this.mWrkDt = string.Empty;
                    this.mShift = string.Empty;
                    this.mMoldId = string.Empty;
                    this.mCavity = string.Empty;
                    this.mProdType = string.Empty;
                }

                base.mDataProcess.SetGrid(gcItemList
                                         , result.ReturnDataSet.Tables[1]
                                         , false
                                         , "ITEM_ID, PART_NO, MODEL, OPER"
                                         );

                gvItemList.Columns["BCD"].Width = 140;
                gvItemList.Columns["PART_NM"].Width = 220;
                gvItemList.Columns["SPEC"].Width = 160;
                gvItemList.Columns["LOT"].Width = 140;
                gvItemList.Columns["STK_QTY"].Width = 100;

                gvItemList.Columns["BCD"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gvItemList.Columns["LOT"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gvItemList.Columns["STK_QTY"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                DataTable dtSum = gcItemList.DataSource as DataTable;
                if (dtSum.Rows.Count > 0)
                    this.lblSubInputQty.Text = dtSum.Compute("SUM(STK_QTY)", "").ToDouble().ToString("#,##0");
                else
                    this.lblSubInputQty.Text = "0";

                if (result.ReturnDataSet.Tables[2].Rows.Count > 0)
                {
                    DataTable dt = result.ReturnDataSet.Tables[2];

                    if (this.mWrkDt == "")
                        this.mWrkDt = dt.Rows[0]["WRK_DT"].NullString();

                    if (this.mShift == "")
                        this.mShift = dt.Rows[0]["SHIFT"].NullString();

                    this.mWrker = dt.Rows[0]["WORKER_ID"].NullString();
                    this.lblSubTime.Text = dt.Rows[0]["CURR_TIME"].NullString();
                }
                else
                {
                    this.mWrkDt = string.Empty;
                    this.mShift = string.Empty;
                    this.mWrker = string.Empty;
                }

                if (result.ReturnDataSet.Tables[3].Rows.Count > 0)
                {
                    this.SetChart(this.chtSubDownTimeState, "DownTimeLine", result.ReturnDataSet.Tables[3]);
                }

                if (result.ReturnDataSet.Tables[4].Rows.Count > 0)
                {
                    DataTable dt = result.ReturnDataSet.Tables[4];

                    this.mEhr = dt.Rows[0]["INPUT_ID"].NullString();
                    this.lblSubInput.Tag = dt.Rows[0]["INPUT_ID"].NullString();
                    this.lblSubInput.Text = dt.Rows[0]["INPUT_NM"].NullString();
                }
            }
            catch (Exception ex)
            {
                MessageBoxPanel.Show(ex.ToString(), MessageType.Warning);
            }
            finally
            {
                this.lblSubTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                this.txtSubBcdBarcode.Focus();
                this.txtSubBcdBarcode.SelectAll();
            }
        }

        /// <summary>차트 정보 출력</summary>
        /// <param name="chart">챠트 컨트롤</param>
        /// <param name="type">챠트 유형</param>
        /// <param name="data">데이터</param>
        private void SetChart(ChartControl chart, string type, DataTable data)
        {
            try
            {
                switch (type)
                {
                    case "DownTimeLine":
                        chart.Series.Clear();

                        Series series = new Series();
                        OverlappedGanttSeriesView overlappedGanttSeriesView = new OverlappedGanttSeriesView();

                        overlappedGanttSeriesView.BarWidth = 1D;
                        overlappedGanttSeriesView.ColorEach = true;

                        series.View = overlappedGanttSeriesView;
                        series.ValueScaleType = ScaleType.DateTime;
                        series.CrosshairLabelPattern = "{V1} ~ {V2}";

                        for (int i = 0; i < data.Rows.Count; i++)
                        {
                            string title = data.Rows[i]["TITLE"].NullString();

                            int startYY = data.Rows[i]["START_YY"].ToInt();
                            int startMM = data.Rows[i]["START_MM"].ToInt();
                            int startDD = data.Rows[i]["START_DD"].ToInt();
                            int startHH = data.Rows[i]["START_HH"].ToInt();
                            int startMI = data.Rows[i]["START_MI"].ToInt();
                            int startSS = data.Rows[i]["START_SS"].ToInt();
                            int startFF = data.Rows[i]["START_FF"].ToInt();

                            int endYY = data.Rows[i]["END_YY"].ToInt();
                            int endMM = data.Rows[i]["END_MM"].ToInt();
                            int endDD = data.Rows[i]["END_DD"].ToInt();
                            int endHH = data.Rows[i]["END_HH"].ToInt();
                            int endMI = data.Rows[i]["END_MI"].ToInt();
                            int endSS = data.Rows[i]["END_SS"].ToInt();
                            int endFF = data.Rows[i]["END_FF"].ToInt();

                            string color = string.Empty;

                            if (data.Rows[i]["DT_GRP_ID"].NullString() == "RUN")         // 가동
                            {
                                color = "#00FF00";
                            }
                            else if (data.Rows[i]["DT_GRP_ID"].NullString() == "STD_DT") // 정규 비가동
                            {
                                color = "#FFFFFF";
                            }
                            else
                            {
                                int temp = data.Rows[i]["DT_COLOR"].ToInt();
                                if (temp == 0)
                                    color = "#FFFFFF";
                                else
                                    color = "#" + temp.ToString("X").ToUpper().Substring(2, 6);
                            }

                            SeriesPoint seriesPoint = new SeriesPoint(title, new object[] { ((object)(new DateTime(startYY, startMM, startDD, startHH, startMI, startSS, startFF)))
                                                                                          , ((object)(new DateTime(endYY, endMM, endDD, endHH, endMI, endSS, endFF))) }, 4);
                            seriesPoint.ColorSerializable = color;

                            series.Points.Add(seriesPoint);
                        }

                        chart.SeriesSerializable = new Series[] { series };

                        //// 다이어그램의 Y축 범위 설정 (화면상 X축)
                        ((GanttDiagram)chart.Diagram).AxisX.Visibility = DefaultBoolean.False;
                        ((GanttDiagram)chart.Diagram).AxisX.VisibleInPanesSerializable = "-1";

                        ((GanttDiagram)chart.Diagram).AxisY.Visibility = DefaultBoolean.False;
                        ((GanttDiagram)chart.Diagram).AxisY.VisibleInPanesSerializable = "-1";
                        ((GanttDiagram)chart.Diagram).AxisY.GridLines.Visible = false;
                        ((GanttDiagram)chart.Diagram).AxisY.Label.Visible = false;
                        ((GanttDiagram)chart.Diagram).AxisY.Tickmarks.MinorVisible = false;
                        ((GanttDiagram)chart.Diagram).AxisY.Tickmarks.Visible = false;
                        ((GanttDiagram)chart.Diagram).AxisY.WholeRange.AutoSideMargins = false;
                        ((GanttDiagram)chart.Diagram).AxisY.WholeRange.SideMarginsValue = 0D;

                        chart.Legend.Visibility = DefaultBoolean.False;

                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBoxPanel.Show(ex.ToString(), MessageType.Warning);
            }
        }

        /// <summary>바코드 투입</summary>
        /// <param name="barcode"></param>
        private void InputBarcode(string barcode, string inputType)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtSubBcdBarcode.EditValue.NullString()))
                {
                    // 바코드를 입력하세요.
                    MessageBoxPanel.Show("INPUT_BCD".GetMessageStr(), MessageType.Warning);
                    this.txtSubBcdBarcode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(this.mWrkOrdNo))
                {
                    // 작업이 시작되지 않았습니다.
                    MessageBoxPanel.Show("MSG_ERR_306".GetMessageStr(), MessageType.Warning);
                    this.txtSubBcdBarcode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(this.mWrker))
                {
                    // 작업자를 선택하세요.
                    MessageBoxPanel.Show("SELECT_WRKER".GetMessageStr(), MessageType.Warning);
                    this.txtSubBcdBarcode.Focus();
                    return;
                }

                switch (inputType)
                {
                    case "I": // 투입

                        Dictionary<string, object> dicI = new Dictionary<string, object>()
                        {
                            ["A_LANG"] = Global.UserInfo.Language,
                            ["A_CLIENT_CD"] = Global.CompanyInfo.Client,
                            ["A_PLANT_ID"] = this.mPlantId,
                            ["A_LINE_ID"] = this.mLineId,
                            ["A_OPER_ID"] = this.mOperId,
                            ["A_WRK_ORD_NO"] = this.mWrkOrdNo,
                            ["A_WRK_ORD_ITEM_ID"] = this.mItemId,
                            ["A_SHIFT"] = this.mShift,
                            ["A_BCD"] = this.txtSubBcdBarcode.EditValue,
                            ["A_WRK_DTM"] = DateTime.Now.DateTimeString(),
                            ["A_INPUT_TYPE"] = "B",
                            ["A_OPERATOR"] = string.IsNullOrWhiteSpace(this.mWrker) ? Global.UserInfo.Ehr : this.mWrker.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0],
                            ["A_IP_ADDR"] = Global.LocalPCInfo.IpAddress,
                            ["A_FORM_CD"] = this.Name
                        };

                        ReturnDataStructure resultI = base.mDataProcess.ExcuteProcedure("PP11160.PUT_INPUT"
                                                                                       , 1
                                                                                       , dicI
                                                                                       );

                        if (resultI.MessageBoxResult(ProjectType.InputPanel) == false)
                        {
                            this.txtSubBcdBarcode.Focus();
                            this.txtSubBcdBarcode.SelectAll();
                            return;
                        }

                        if (resultI.ReturnDataSet.Tables[0].Rows.Count > 0)
                        {
                            this.lblSubBcdPartNo.Text = resultI.ReturnDataSet.Tables[0].Rows[0]["PART_NO"].NullString();
                            this.lblSubBcdPartNm.Text = resultI.ReturnDataSet.Tables[0].Rows[0]["PART_NM"].NullString();
                            this.lblSubBcdModel.Text = resultI.ReturnDataSet.Tables[0].Rows[0]["MODEL"].NullString();
                            this.lblSubBcdSpec.Text = resultI.ReturnDataSet.Tables[0].Rows[0]["SPEC"].NullString();
                            this.lblSubBcdLot.Text = resultI.ReturnDataSet.Tables[0].Rows[0]["LOT"].NullString();
                            this.lblSubBcdQty.Text = resultI.ReturnDataSet.Tables[0].Rows[0]["STK_QTY"].NullString();
                        }

                        base.mDataProcess.SetGrid(gcItemList
                                                     , resultI.ReturnDataSet.Tables[1]
                                                     , true
                                                     );

                        DataTable dtSum1 = gcItemList.DataSource as DataTable;
                        if (dtSum1.Rows.Count > 0)
                            this.lblSubInputQty.Text = dtSum1.Compute("SUM(STK_QTY)", "").ToDouble().ToString("#,##0");
                        else
                            this.lblSubInputQty.Text = "0";

                        break;

                    case "C": // 취소

                        Dictionary<string, object> dicC = new Dictionary<string, object>()
                        {
                            ["A_LANG"] = Global.UserInfo.Language,
                            ["A_CLIENT_CD"] = Global.CompanyInfo.Client,
                            ["A_PLANT_ID"] = this.mPlantId,
                            ["A_LINE_ID"] = this.mLineId,
                            ["A_WRK_ORD_NO"] = this.mWrkOrdNo,
                            ["A_BCD"] = this.txtSubBcdBarcode.EditValue,
                            ["A_OPERATOR"] = string.IsNullOrWhiteSpace(this.mWrker) ? Global.UserInfo.Ehr : this.mWrker.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0]
                        };

                        ReturnDataStructure resultC = base.mDataProcess.ExcuteProcedure("PP11160.CANC_INPUT"
                                                                                       , 1
                                                                                       , dicC
                                                                                       );

                        if (resultC.MessageBoxResult(ProjectType.InputPanel) == false)
                        {
                            this.txtSubBcdBarcode.Focus();
                            this.txtSubBcdBarcode.SelectAll();
                            return;
                        }

                        if (resultC.ReturnDataSet.Tables[0].Rows.Count > 0)
                        {
                            this.lblSubBcdPartNo.Text = resultC.ReturnDataSet.Tables[0].Rows[0]["PART_NO"].NullString();
                            this.lblSubBcdPartNm.Text = resultC.ReturnDataSet.Tables[0].Rows[0]["PART_NM"].NullString();
                            this.lblSubBcdModel.Text = resultC.ReturnDataSet.Tables[0].Rows[0]["OPER"].NullString();
                            this.lblSubBcdLot.Text = resultC.ReturnDataSet.Tables[0].Rows[0]["LOT"].NullString();
                            this.lblSubBcdQty.Text = resultC.ReturnDataSet.Tables[0].Rows[0]["STK_QTY"].NullString();
                        }

                        base.mDataProcess.SetGrid(gcItemList
                                                 , resultC.ReturnDataSet.Tables[1]
                                                 , true
                                                 );

                        DataTable dtSum2 = gcItemList.DataSource as DataTable;
                        if (dtSum2.Rows.Count > 0)
                            this.lblSubInputQty.Text = dtSum2.Compute("SUM(STK_QTY)", "").ToDouble().ToString("#,##0");
                        else
                            this.lblSubInputQty.Text = "0";

                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBoxPanel.Show(ex.ToString(), MessageType.Warning);
            }
            finally
            {
                this.txtSubBcdBarcode.Focus();
                this.txtSubBcdBarcode.SelectAll();
            }
        }

        private void PrintBoxBarcode()
        {
            try
            {
                if (mWrkOrdNo.Equals(string.Empty) == true)
                {
                    // 작업이 시작되지 않았습니다
                    MessageBoxPanel.Show("MSG_ERR_306".GetMessageStr(), MessageType.Warning);
                    return;
                }

                if (gvItemList.RowCount == 0)
                {
                    // 투입된 자재가 없습니다.
                    MessageBoxPanel.Show("MSG_ERR_420".GetMessageStr(), MessageType.Warning);
                    this.txtSubBcdBarcode.Focus();
                    return;
                }

                // 2021.12.15 1개의 제품만 포장할 수 있다.
                //if (gvItemList.RowCount <= 1)
                //{
                //    // 최소 2개 이상의 바코드가 투입되어야합니다.
                //    MessageBoxPanel.Show("MSG_ERR_617".GetMessageStr(), MessageType.Warning);
                //    this.txtSubBcdBarcode.Focus();
                //    return;
                //}


                PP11160P01 pop = new PP11160P01();

                pop.PlantId = this.mPlantId;
                pop.LineId = this.mLineId;
                pop.WrkOrdNo = this.mWrkOrdNo;
                pop.Wrker = string.IsNullOrWhiteSpace(this.mWrker) ? Global.UserInfo.Ehr : this.mWrker.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0];

                if (pop.ShowDialog() != DialogResult.OK)
                    return;

                DataTable dtData = (gcItemList.DataSource as DataTable).Copy();
                dtData.Columns.Remove("ITEM_ID");
                dtData.Columns.Remove("PART_NO");
                dtData.Columns.Remove("PART_NM");
                dtData.Columns.Remove("OPER");
                dtData.Columns.Remove("MODEL");
                dtData.Columns.Remove("SPEC");
                dtData.AcceptChanges();

                Dictionary<string, object> dic = new Dictionary<string, object>()
                {
                    ["A_LANG"] = Global.UserInfo.Language,
                    ["A_CLIENT_CD"] = Global.CompanyInfo.Client,
                    ["A_COMPANY_ID"] = Global.CompanyInfo.CompanyId,
                    ["A_PLANT_ID"] = this.mPlantId,
                    ["A_LINE_ID"] = this.mLineId,
                    ["A_OPER_ID"] = this.mOperId,
                    ["A_WRK_ORD_NO"] = this.mWrkOrdNo,
                    ["A_WRK_ORD_ITEM_ID"] = this.mItemId,
                    ["A_CUST_LOT"] = pop.LotNo,
                    ["A_BCD_XML"] = base.mDataProcess.GetDataTableToXml(dtData),
                    ["A_BOX_QTY"] = this.lblSubInputQty.Text.ToDecimal(),
                    ["A_LOT_NO"] = pop.LotNo,
                    ["A_LOT_SEQ"] = pop.LotSeq,
                    ["A_LOT_DT"] = pop.LotDt,
                    ["A_BOX_NO"] = pop.BoxNo,
                    ["A_BOX_CNT"] = pop.BoxCnt,
                    ["A_COPY_QTY"] = pop.CopyQty,
                    ["A_WRK_DTM"] = DateTime.Now.DateTimeString(),
                    ["A_OPERATOR"] = string.IsNullOrWhiteSpace(this.mWrker) ? Global.UserInfo.Ehr : this.mWrker.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0],
                    ["A_IP_ADDR"] = Global.LocalPCInfo.IpAddress,
                    ["A_FORM_CD"] = this.Name
                };

                ReturnDataStructure result = base.mDataProcess.ExcuteProcedure("PP11160.PUT_PRT_RST"
                                                                              , 1
                                                                              , dic
                                                                              );

                if (result.MessageBoxResult(ProjectType.InputPanel) == false)
                    return;

                // 라벨발행
                DataSet ds = new DataSet();
                DataTable dt = result.ReturnDataSet.Tables[0].Copy();

                ds.Tables.Add(dt);

                // 라벨발행
                ProjectHelper.PrintReport("RPT_PP11160P01"   //"RPT_MM12030P01"
                                         , ds
                                         , base.mDataProcess
                                         , Global.CompanyInfo.CompanyId
                                         , this.Name
                                         , 1
                                         , (short)pop.CopyQty.ToInt()
                                         );

                this.BcdControlsClear();
                this.GetWorkInfo(this.mLineId, this.mWrkOrdNo);
            }
            catch (Exception ex)
            {
                MessageBoxPanel.Show(ex.ToString(), MessageType.Warning);
            }
            finally
            {
                this.txtSubBcdBarcode.Focus();
                this.txtSubBcdBarcode.SelectAll();
            }
        }

        /// <summary>팝업창 호출</summary>
        /// <param name="ModuleCode"></param>
        private void OpenPopUp(string moduleNameSpace, string ModuleCode, object[] args = null)
        {
            if (string.IsNullOrEmpty(ModuleCode))
                return;

            try
            {
                string moduleMenuId         = string.Empty;
                string moduleCode           = ModuleCode;
                string moduleName           = string.Empty;
                string moduleNS             = base.ActivityNameSpace;
                string moduleClass          = string.Empty;
                string moduleParam          = string.Empty;
                string moduleProgramPath    = string.Empty;

                Assembly assembly   = Assembly.GetExecutingAssembly();
                string nameSpace    = string.Format(assembly.EntryPoint.DeclaringType.Namespace + ".{0}.PP.", moduleNameSpace);

                ModuleBaseManager control;
                control = assembly.CreateInstance(nameSpace + moduleCode, true, BindingFlags.Default, null, args, null, null) as ModuleBaseManager;

                control.ActivityMenuID  = moduleMenuId;
                control.ActivityCode    = moduleCode;
                control.ActivityName    = moduleName;
                control.Text            = moduleName;
                control.ActivityClass   = moduleClass;
                control.ActivityParam   = moduleParam;
                control.ActivityPath    = moduleProgramPath;
                control.Dock            = DockStyle.Fill;

                this.mTimer.Enabled = false;
                this.mTimer.Stop();

                this.AddSub(control);

                control.Form_Show();
                control.Focus();
            }
            catch (Exception ex)
            {
                MessageBoxPanel.Show(ex.ToString(), MessageType.Warning);
            }
        }

        /// <summary>작업 버튼을 생성합니다.</summary>
        /// <param name="buttnType">생성할 버튼 유형입니다.</param>
        /// <returns>생성된 작업 버튼을 반환합니다.</returns>
        private Button CreateWrkBtn(string btnType, string btnName)
        {
            Button btnWrk = new Button();

            btnWrk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            btnWrk.FlatAppearance.BorderSize = 0;
            btnWrk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnWrk.Font = new System.Drawing.Font("나눔고딕", 13F, System.Drawing.FontStyle.Bold);
            btnWrk.ForeColor = System.Drawing.Color.White;
            btnWrk.Location = new System.Drawing.Point(12, 12);
            btnWrk.Margin = new System.Windows.Forms.Padding(6, 6, 6, 3);
            btnWrk.Name = "btnWorkerSingl";
            btnWrk.Size = new System.Drawing.Size(140, 66);
            btnWrk.TabIndex = 0;
            btnWrk.Tag = btnType;
            btnWrk.Text = btnName;
            btnWrk.UseVisualStyleBackColor = false;
            btnWrk.Click += new System.EventHandler(this.btn_Click);

            return btnWrk;
        }

        /// <summary>작업 버튼을 하단 패널에 추가합니다.</summary>
        /// <param name="btnWrk">추가하려는 작업 버튼 입니다.</param>
        private void AddWrkBtn(Button btnWrk) => this.flowLayoutPanel.Controls.Add(btnWrk);

        #endregion



        #region [ Scanner Data Receive ]

        /// <summary> 스캐너 이벤트 </summary>
        /// <param name="_ScanData"></param>
        public override void ReciveScanData(string _ScanData)
        {
            try
            {
                base.ReciveScanData(_ScanData);

                this.txtSubBcdBarcode.EditValue = _ScanData.Trim();

                this.InputBarcode(this.txtSubBcdBarcode.EditValue.NullString(), this.tgsSubBcdCancel.IsOn ? "C" : "I");

                this.txtSubBcdBarcode.Focus();
                this.txtSubBcdBarcode.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBoxPanel.Show(ex.ToString(), MessageType.Warning);
            }
        }

        #endregion


        private void layoutControlGroup2_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.NullString() == "UP")
            {
                gvItemList.MovePrevPage();
                gvItemList.MovePrevPage();
            }
            else
            {
                gvItemList.MoveNextPage();
                gvItemList.MoveNextPage();
            }
        }

        
    }
}
