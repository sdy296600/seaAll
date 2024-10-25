using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Popup;
using CoFAS.NEW.MES.Core.Properties;
using FarPoint.Win;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace CoFAS.NEW.MES.Core.Function
{
    public enum GridHorizontalAlignment
    {
        Left = 0,
        Center = 1,
        Right = 2,
        General = 3
    }

    public enum GridVerticalAlignment
    {
        Middle = 0,
        Top = 1,
        Bottom = 2,
        General = 3
    }

    public class FpSpreadManager
    {
        #region ○ Field

        /// <summary>
        /// 날짜 포맷
        /// </summary>
        public static readonly string DEFAULT_DATE_FORMAT = "yyyy-MM-dd";
        public static readonly string DEFAULT_TIME_FORMAT = "HH:mm:ss";
        public static readonly string DEFAULT_DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        public static readonly Color COLOR_Basi = Color.FromArgb(255, 255, 255);        // 흰  색
        public static readonly Color COLOR_Oddd = Color.FromArgb(255, 255, 255);        // 흰  색
        public static readonly Color COLOR_Even = Color.FromArgb(233, 255, 191);        // 연두색
        public static readonly Color COLOR_Text = Color.FromArgb(0, 0, 0);              // 검정색
        public static readonly Color COLOR_Lock = Color.FromArgb(222, 238, 254);        // 지정색:LOCK Color.FromArgb(192, 224, 255)
        public static readonly Color COLOR_UnLk = Color.FromArgb(210, 221, 120);        // 지정색:LOCK중에서 일부만 입력가능할때
        public static readonly Color COLOR_Sele = Color.FromArgb(255, 255, 128);        // 지정색:로우를 선택한 색상((CRSSSEL_BACK))
        public static readonly Color COLOR_Erro = Color.FromArgb(255, 255, 0);          // 지정색:자료저장중 오류발생 배경색
        public static readonly Color COLOR_Grid = Color.FromArgb(128, 128, 128);        // 지정색:그리드색상
        public static readonly Color COLOR_Area = Color.FromArgb(229, 247, 255);        // 지정색:스프레드 바탕색 -> 그리드 색 흰색으로 변경
        public static readonly Color COLOR_Butt = Color.FromArgb(192, 229, 140);        // 지정색:스프레드 바탕색

        // 집계색상
        public static readonly Color COLOR_TP11 = Color.FromArgb(101, 203, 255);        // 하늘색 진함
        public static readonly Color COLOR_TP12 = Color.FromArgb(144, 217, 255);        //
        public static readonly Color COLOR_TP13 = Color.FromArgb(179, 229, 255);        //
        public static readonly Color COLOR_TP14 = Color.FromArgb(206, 238, 255);        //
        public static readonly Color COLOR_TP21 = Color.FromArgb(158, 204, 199);        // 하늘색 연함
        public static readonly Color COLOR_TP22 = Color.FromArgb(189, 220, 217);        //
        public static readonly Color COLOR_TP23 = Color.FromArgb(214, 233, 231);        //
        public static readonly Color COLOR_TP24 = Color.FromArgb(231, 242, 241);        //
        public static readonly Color COLOR_TP31 = Color.FromArgb(255, 242, 22);         // 노란계열
        public static readonly Color COLOR_TP32 = Color.FromArgb(255, 244, 56);         //
        public static readonly Color COLOR_TP33 = Color.FromArgb(255, 248, 112);        //
        public static readonly Color COLOR_TP34 = Color.FromArgb(255, 251, 168);        //

        // 처리변수
        private static Image _pBlank_01 = null;
        private static Image _pCheck_01 = null;
        private static Image _pError_01 = null;
        private static Image _pChoice_01 = null;
        private static Image _pSelect_01 = null;
        private static Image _pUnSelect_01 = null;
        private static Image _pFind_01 = null;
        private static Image _pFind_02 = null;
        private static Image _pFind_03 = null;
        private static Image _pFind_04 = null;
        private static Image _pFind_05 = null;
        private static Image _pFind_06 = null;
        private static Image _pFind_07 = null;
        private static Image _pOk_01 = null;
        private static Image _pNg_01 = null;

        // DB접속
        private static string _pCORP_CD;
        private static string _pConnectionString;
        private static OperationMode _pOperationType = OperationMode.Normal;

        #endregion

        #region ○ Property

        public static string pCORP_CD
        {
            set
            {
                _pCORP_CD = value;
            }
        }

        public static string pConnectionString
        {
            set
            {
                _pConnectionString = value;
            }
        }

        public static OperationMode pOperationType
        {
            set
            {
                _pOperationType = value;
            }
        }

        #endregion

        #region ○ 생성자

        public FpSpreadManager()
        {
        }

        #endregion

        # region ○ 스프레드 기본 설정

        /// <summary>
        /// 스프레드 전체 설정
        /// </summary>
        public static void SpreadSetStyle(FpSpread pfpSpread)
        {
            //pfpSpread.ActiveSheet.DefaultStyle.VisualStyles = VisualStyles.Off;
            //pfpSpread.InterfaceRenderer = null;

            FarPoint.Win.Spread.SheetView sheetView = new FarPoint.Win.Spread.SheetView();
            pfpSpread.InterfaceRenderer = null;
            //sheetView.GrayAreaBackColor = COLOR_Area;
            sheetView.GrayAreaBackColor = COLOR_Basi;
            pfpSpread.Sheets[0] = sheetView;

            // 스크롤바 관련 설정
            pfpSpread.ColumnSplitBoxPolicy = SplitBoxPolicy.Never;
            pfpSpread.RowSplitBoxPolicy = SplitBoxPolicy.Never;
            pfpSpread.HorizontalScrollBarPolicy = ScrollBarPolicy.AsNeeded;         // 스크롤바
            pfpSpread.VerticalScrollBarPolicy = ScrollBarPolicy.AsNeeded;           // 스코롤바
            pfpSpread.ScrollTipPolicy = ScrollTipPolicy.Vertical;                   // 스코롤시 row위치 표시
            pfpSpread.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both; //스크롤바 이동시 로우이동
            // Column, Row 관련 설정
            //pfpSpread.AllowColumnMove = false;                                      // 컬럼이동금지
            pfpSpread.AllowColumnMove = true;                                      // 컬럼이동금지
            pfpSpread.AllowRowMove = false;                                         // 로우이동금지
            pfpSpread.RetainSelectionBlock = false;                                 //
            pfpSpread.EditModeReplace = true;                                       // 셀엔터시 입력모드 금지
            pfpSpread.EditModePermanent = false;                                    // 셀수정시 에디트 처리

            // Tab
            pfpSpread.TabStripPolicy = TabStripPolicy.Never;                        // 표시안함

            // Color 설정
            pfpSpread.BackColor = COLOR_Area;

            // 이벤트 설정
            //pfpSpread.Enter += new EventHandler(fpSpread_Enter);
            //pfpSpread.PreviewKeyDown += OnSpreadPreviewKeyDown;
            //pfpSpread.KeyDown += OnSpreadKeyDown;                     // 엔터 아래로 이동(이벤트방식)

            //스프레드에서 엔터이동
            FarPoint.Win.Spread.InputMap im;
            // 아래 Row로
            im = pfpSpread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow);
            // Right로
            im = pfpSpread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap);
            // 위와 같이 사용하면 editmode에서도 무조건 이동
            //im = pfpSpread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused);
            //im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap); 


            try
            {
                _pBlank_01 = Resources.btn_spread_blank_01;
                _pCheck_01 = Resources.btn_spread_check_01;
                _pError_01 = Resources.btn_spread_error_01;
                _pChoice_01 = Resources.btn_spread_choice_01;
                _pSelect_01 = Resources.btn_spread_select_01;
                _pUnSelect_01 = Resources.btn_spread_unselect_01;
                _pFind_01 = Resources.btn_spread_find_01;
                _pFind_02 = Resources.btn_Spread_Find_02;
                _pFind_03 = Resources.btn_Spread_Find_03;
                _pFind_04 = Resources.btn_Spread_Find_04;
                _pFind_05 = Resources.btn_Spread_Find_05;
                _pFind_06 = Resources.saveto_16x16;
                _pFind_07 = Resources.save_16x16;

                _pOk_01 = Resources.btn_spread_ok_01;
                _pNg_01 = Resources.btn_spread_ng_01;


            }
            catch (Exception pException)
            {
                throw new ExceptionManager(null, "SpreadSetStyle", pException);
            }
        }
        /// <summary>
        /// 스프레드 전체 설정
        /// </summary>
        public static void SpreadSetStyle(FpSpread pfpSpread, int iSheets)
        {
            //pfpSpread.ActiveSheet.DefaultStyle.VisualStyles = VisualStyles.Off;
            //pfpSpread.InterfaceRenderer = null;

            FarPoint.Win.Spread.SheetView sheetView = new FarPoint.Win.Spread.SheetView();
            pfpSpread.InterfaceRenderer = null;
            sheetView.GrayAreaBackColor = COLOR_Area;
            pfpSpread.Sheets[iSheets] = sheetView;

            // 스크롤바 관련 설정
            pfpSpread.ColumnSplitBoxPolicy = SplitBoxPolicy.Never;
            pfpSpread.RowSplitBoxPolicy = SplitBoxPolicy.Never;
            pfpSpread.HorizontalScrollBarPolicy = ScrollBarPolicy.AsNeeded;         // 스크롤바
            pfpSpread.VerticalScrollBarPolicy = ScrollBarPolicy.AsNeeded;           // 스코롤바
            pfpSpread.ScrollTipPolicy = ScrollTipPolicy.Vertical;                   // 스코롤시 row위치 표시
            pfpSpread.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both; //스크롤바 이동시 로우이동

            // Column, Row 관련 설정
            pfpSpread.AllowColumnMove = false;                                      // 컬럼이동금지
            pfpSpread.AllowRowMove = false;                                         // 로우이동금지
            pfpSpread.RetainSelectionBlock = false;                                 //
            pfpSpread.EditModeReplace = true;                                       // 셀엔터시 입력모드 금지
            pfpSpread.EditModePermanent = false;                                    // 셀수정시 에디트 처리

            // Tab
            pfpSpread.TabStripPolicy = TabStripPolicy.Never;                        // 표시안함

            // Color 설정
            pfpSpread.BackColor = COLOR_Area;

            // 이벤트 설정
            //pfpSpread.Enter += new EventHandler(fpSpread_Enter);
            //pfpSpread.PreviewKeyDown += OnSpreadPreviewKeyDown;
            //pfpSpread.KeyDown += OnSpreadKeyDown;                     // 엔터 아래로 이동(이벤트방식)

            //스프레드에서 엔터이동
            FarPoint.Win.Spread.InputMap im;
            // 아래 Row로
            im = pfpSpread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow);
            // Right로
            im = pfpSpread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap);
            // 위와 같이 사용하면 editmode에서도 무조건 이동
            //im = pfpSpread.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused);
            //im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap); 

            try
            {
                _pBlank_01 = Resources.btn_spread_blank_01;
                _pCheck_01 = Resources.btn_spread_check_01;
                _pError_01 = Resources.btn_spread_error_01;
                _pChoice_01 = Resources.btn_spread_choice_01;
                _pSelect_01 = Resources.btn_spread_select_01;
                _pUnSelect_01 = Resources.btn_spread_unselect_01;
                _pFind_01 = Resources.btn_spread_find_01;
                _pFind_02 = Resources.btn_Spread_Find_02;
                _pFind_03 = Resources.btn_Spread_Find_03;
                _pFind_04 = Resources.btn_Spread_Find_04;
                _pFind_05 = Resources.btn_Spread_Find_05;
                _pFind_06 = Resources.saveto_16x16;
                _pFind_07 = Resources.save_16x16;
                _pOk_01 = Resources.btn_spread_ok_01;
                _pNg_01 = Resources.btn_spread_ng_01;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(null, "SpreadSetStyle", pException);
            }
        }

        /// <summary>
        /// 스크롤바 관련 기본 설정
        /// </summary>
        /// <param name="pfpSpread"></param>
        public static void SpreadSetScrollBarStyle(FpSpread pfpSpread)
        {
            // 스크롤바 관련 기본 설정
            pfpSpread.ColumnSplitBoxPolicy = SplitBoxPolicy.Never;
            pfpSpread.RowSplitBoxPolicy = SplitBoxPolicy.Never;
            pfpSpread.HorizontalScrollBarPolicy = ScrollBarPolicy.AsNeeded;         // 스크롤바
            pfpSpread.VerticalScrollBarPolicy = ScrollBarPolicy.AsNeeded;           // 스코롤바
            pfpSpread.ScrollTipPolicy = ScrollTipPolicy.Vertical;                   // 스코롤시 row위치 표시
            pfpSpread.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both; //스크롤바 이동시 로우이동
        }

        /// <summary>
        /// 스프레드 쉬트 전체 설정
        /// </summary>
        public static void SpreadSetSheetStyle(FpSpread pfpSpread, int pSheet)
        {
            pfpSpread.Sheets[pSheet].RowCount = 0;
            // ActiveShee 설정 기본
            pfpSpread.Sheets[pSheet].OperationMode = _pOperationType;               // 일기전용등 설정
            pfpSpread.Sheets[pSheet].ColumnHeader.Rows[0].Height = 30;              // 헤더 높이 (-1)안됨
            pfpSpread.Sheets[pSheet].Rows.Default.Height = 21;                      // 로우 높이
            //pfpSpread.Sheets[pSheet].AlternatingRows[1].BackColor = COLOR_Basi;     // 격줄로 색상변경
            // ActiveShee 설정 데이터바인딩시
            pfpSpread.Sheets[pSheet].DataAutoCellTypes = false;
            pfpSpread.Sheets[pSheet].DataAutoHeadings = false;
            pfpSpread.Sheets[pSheet].DataAutoSizeColumns = false;


            // 그리드색생
            pfpSpread.Sheets[pSheet].HorizontalGridLine = new GridLine(GridLineType.Flat, Color.FromArgb(180, 180, 180));
            pfpSpread.Sheets[pSheet].VerticalGridLine = new GridLine(GridLineType.Flat, Color.FromArgb(180, 180, 180));
            pfpSpread.Sheets[pSheet].ColumnHeaderHorizontalGridLine = new GridLine(GridLineType.Flat, Color.FromArgb(180, 180, 180));
            pfpSpread.Sheets[pSheet].ColumnHeaderVerticalGridLine = new GridLine(GridLineType.Flat, Color.FromArgb(180, 180, 180));
            pfpSpread.Sheets[pSheet].ColumnFooterHorizontalGridLine = new GridLine(GridLineType.Flat, Color.FromArgb(180, 180, 180));
            pfpSpread.Sheets[pSheet].ColumnFooterVerticalGridLine = new GridLine(GridLineType.Flat, Color.FromArgb(180, 180, 180));

        }

        // 스프레드 헤더 설정
        public static void SpreadSetHeader(xFpSpread pfpSpread, string pFileName, int pSheet, string pSetData, string pMenu_name, string pUser_account)
        {
            pOperationType = OperationMode.Normal;

            pfpSpread._menu_name = pMenu_name;
            pfpSpread._user_account = pUser_account;

            string[] pSetRow = pSetData.Split(',');
            string[] pSetColumn = null;
            string pHeaderName = string.Empty;  // 헤더명
            string pWidth = "8";                // 헤더넓이
            string pVisible = "0";              // 0:보임 1:숨김
            string pLocked = "1";               // 0:읽기전용 1:수정가능
            string pAlign = "1";                // 0:왼쪽 1:중앙 2:오른쪽
            string pCellType = "";              // 0:None 1:Text 2:Number 3:ComboBox 4:Button 5:...................
            string pLength = "";                // 전체자리수
            string pPoint = "";                 // 소숫점자리수
            string pCodeType = "";              // 조회유형 0:dbo.USP_CM000010RP_R10w 1:USP_CM000011RP_R10
            string pCodeName = "";              // 조회항목
            string pColumnKey = "";             // 컬럼Tag(Key)
            string pSeq = "";                   // 순번(사용은 안하고 참고용으로만)
            try
            {

                pfpSpread.Font = new Font("맑은 고딕", 9);

                pfpSpread.Sheets[pSheet].ColumnCount = 0;
                pfpSpread.Sheets[pSheet].ColumnCount = pSetRow.Length;

                pfpSpread.SubEditorOpening -= new SubEditorOpeningEventHandler(pfpSpread_SubEditorOpening);
                pfpSpread.SubEditorOpening += new SubEditorOpeningEventHandler(pfpSpread_SubEditorOpening);

                pfpSpread.ClipboardPasting -= new ClipboardPastingEventHandler(pfpSpread_ClipboardPasting);
                pfpSpread.ClipboardPasting += new ClipboardPastingEventHandler(pfpSpread_ClipboardPasting);

                //pfpSpread.CellClick -= new CellClickEventHandler(pfpSpread_CellClick);
                //pfpSpread.CellClick += new CellClickEventHandler(pfpSpread_CellClick);

                pfpSpread.CellDoubleClick -= new CellClickEventHandler(pfpSpread_CellClick);
                pfpSpread.CellDoubleClick += new CellClickEventHandler(pfpSpread_CellClick);
                pfpSpread.SelectionChanged -= new SelectionChangedEventHandler(pfpSpread_SelectionChanged);
                pfpSpread.SelectionChanged += new SelectionChangedEventHandler(pfpSpread_SelectionChanged);


                pfpSpread.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(pfpSpread_ColumnWidthChanged);
                pfpSpread.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(pfpSpread_ColumnWidthChanged);

                pfpSpread.ColumnDragMoveCompleted -= new FarPoint.Win.Spread.DragMoveCompletedEventHandler(pfpSpread_ColumnDragMoveCompleted);
                pfpSpread.ColumnDragMoveCompleted += new FarPoint.Win.Spread.DragMoveCompletedEventHandler(pfpSpread_ColumnDragMoveCompleted);

                //pfpSpread._list = new List<xFpSpread_Entity>();

                //for (int i = 0; i < pSetRow.Length; i++)
                //{
                //    pSetColumn = pSetRow[i].Split(':');

                //    xFpSpread_Entity entity = new xFpSpread_Entity();

                //    entity.HeaderName = pSetColumn[0].ToString().Trim();
                //    entity.Width      = pSetColumn[1].ToString().Trim();
                //    entity.Visible    = pSetColumn[2].ToString().Trim();
                //    entity.Locked     = pSetColumn[3].ToString().Trim();
                //    entity.Align      = pSetColumn[4].ToString().Trim();
                //    entity.CellType   = pSetColumn[5].ToString().Trim();
                //    entity.Length     = pSetColumn[6].ToString().Trim();
                //    entity.Point      = pSetColumn[7].ToString().Trim();
                //    entity.CodeType   = pSetColumn[8].ToString().Trim();
                //    entity.CodeName   = pSetColumn[9].ToString().Trim();
                //    entity.ColumnKey  = pSetColumn[10].ToString().Trim();
                //    if (pSetColumn.Length > 11)
                //    {
                //        entity.Seq = pSetColumn[11].ToString().Trim();
                //    }


                //    pfpSpread._list.Add(entity);
                //}

                //MenuSave_Entity _Entity = new MenuSave_Entity();

                //_Entity.user_account = pfpSpread._user_account;
                //_Entity.menu_name = pfpSpread._menu_name;
                //_Entity.spread_name = pfpSpread.Name;
                //DataTable dt = new MenuSave_Business().MenuSave_R10(_Entity);
                if (pSetRow.Length != 1)
                {


                    for (int i = 0; i < pSetRow.Length; i++)
                    {
                        pSetColumn = pSetRow[i].Split(':');
                        pHeaderName = pSetColumn[0].ToString().Trim();
                        pWidth = pSetColumn[1].ToString().Trim();
                        pVisible = pSetColumn[2].ToString().Trim();
                        pLocked = pSetColumn[3].ToString().Trim();
                        pAlign = pSetColumn[4].ToString().Trim();
                        pCellType = pSetColumn[5].ToString().Trim();
                        pLength = pSetColumn[6].ToString().Trim();
                        pPoint = pSetColumn[7].ToString().Trim();
                        pCodeType = pSetColumn[8].ToString().Trim();
                        pCodeName = pSetColumn[9].ToString().Trim();
                        pColumnKey = pSetColumn[10].ToString().Trim();
                        if (pSetColumn.Length > 11)
                            pSeq = pSetColumn[11].ToString().Trim();

                        pfpSpread.Sheets[pSheet].Columns[i].VerticalAlignment = CellVerticalAlignment.Center;   // 세로정렬 : 가운데
                        pfpSpread.Sheets[pSheet].Columns[i].Label = pHeaderName;
                        pfpSpread.Sheets[pSheet].Columns[i].Width = int.Parse(pWidth);
                        pfpSpread.Sheets[pSheet].Columns[i].Tag = pColumnKey;
                        pfpSpread.Sheets[pSheet].ColumnHeader.Columns[i].BackColor = Color.FromArgb(191, 207, 221);
                        pfpSpread.Sheets[pSheet].Columns[i].AllowAutoSort = true;

                        pfpSpread.Sheets[0].GrayAreaBackColor = Color.FromArgb(191, 207, 221);

                        // 보임,숨김설정
                        if (pVisible == "0")
                        {
                            pfpSpread.Sheets[pSheet].Columns[i].Visible = true;
                        }
                        else
                        {
                            pfpSpread.Sheets[pSheet].Columns[i].Visible = false;
                        }

                        // 일기전용,수정가능설정
                        switch (pLocked)
                        {
                            case "0":  // 일기전용
                                pfpSpread.Sheets[pSheet].Columns[i].Locked = true;
                                pfpSpread.Sheets[pSheet].Columns[i].BackColor = Color.FromArgb(240, 240, 236);
                                break;
                            case "1":  // 수정가능
                                pfpSpread.Sheets[pSheet].Columns[i].Locked = false;
                                pfpSpread.Sheets[pSheet].Columns[i].BackColor = Color.FromArgb(253, 253, 150);

                                if (pfpSpread.Sheets[pSheet].Columns[i].Label.Contains("*"))
                                {
                                    pfpSpread.Sheets[pSheet].ColumnHeader.Columns[i].ForeColor = Color.Blue;
                                }
                                break;
                            case "2":  // 읽기전용이지만 색상은 락색상 적용안함
                                pfpSpread.Sheets[pSheet].Columns[i].Locked = true;
                                pfpSpread.Sheets[pSheet].Columns[i].BackColor = Color.FromArgb(240, 240, 236);
                                break;
                            default:
                                break;
                        }
                        // 정렬방법
                        switch (pAlign)
                        {
                            case "0": // 왼쪽
                                pfpSpread.Sheets[pSheet].Columns[i].HorizontalAlignment = CellHorizontalAlignment.Left;
                                pfpSpread.Sheets[pSheet].Columns[i].CellPadding.Left = 3;
                                break;
                            case "1": // 중앙
                                pfpSpread.Sheets[pSheet].Columns[i].HorizontalAlignment = CellHorizontalAlignment.Center;
                                break;
                            case "2": // 오른쪽
                                pfpSpread.Sheets[pSheet].Columns[i].HorizontalAlignment = CellHorizontalAlignment.Right;
                                pfpSpread.Sheets[pSheet].Columns[i].CellPadding.Right = 3;
                                break;
                            default:
                                pfpSpread.Sheets[pSheet].Columns[i].HorizontalAlignment = CellHorizontalAlignment.General;
                                break;
                        }
                        // 셀타입설정 (다른것보다 우선해야함)
                        switch (pCellType)
                        {
                            case "11":  // 버튼
                                SpreadColumnAddButton(pfpSpread, pSheet, i, pHeaderName, pCodeType, pCodeName);
                                break;
                            case "12":  // 체크박스
                                SpreadColumnAddCheckBox(pfpSpread, pSheet, i, pCodeName);
                                break;
                            case "14":  // 콤보박스
                                SpreadColumnAddComboBox(pfpSpread, pSheet, i, pLength, pCodeType, pCodeName, "");
                                break;
                            case "17":  // 이미지
                                SpreadColumnAddImage(pfpSpread, pSheet, i);
                                break;
                            case "25":  // 날짜시간
                                SpreadColumnAddDateTime(pfpSpread, pSheet, i, pCodeName);
                                break;
                            case "28":  // 마스크
                                SpreadColumnAddMask(pfpSpread, pSheet, i, pCodeName);
                                break;
                            case "29":  // 숫자
                                SpreadColumnAddNumber(pfpSpread, pSheet, i, pPoint, pCodeType);
                                break;
                            case "32":  // 텍스트
                                SpreadColumnAddText(pfpSpread, pSheet, i, pLength);
                                break;
                            default:
                                break;
                        }
                        // 0~3까지는 기본설정으로 처리
                        if (i < 4)
                        {
                            pfpSpread.Sheets[pSheet].Columns[i].BackColor = COLOR_Basi;
                            if (i == 0 || i == 2 || i == 3)
                                pfpSpread.Sheets[pSheet].Columns[i].Width = 30;
                        }
                    }
                }
                new FpSpreadManager().ContextMenuStrip_Setting(pfpSpread);
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(null, "SpreadSetHeader", pException);
            }
            pfpSpread.Update();
        }

     
        private static void pfpSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            try
            {
                //xFpSpread pfpSpread = sender as xFpSpread;
                //new MenuSave_Business().MenuSave_A10(pfpSpread);
            }
            catch (Exception err)
            {

            }

        }
        private static void pfpSpread_ColumnDragMoveCompleted(object sender, DragMoveCompletedEventArgs e)
        {
            try
            {
                //xFpSpread pfpSpread = sender as xFpSpread;
                //new MenuSave_Business().MenuSave_A10(pfpSpread);
            }
            catch (Exception err)
            {

            }

        }
        private static void pfpSpread_SubEditorOpening(object sender, SubEditorOpeningEventArgs e)
        {
            xFpSpread pfpSpread = sender as xFpSpread;

            //if (pfpSpread.ActiveSheet.Cells[e.Row, e.Column].CellType.ToString() == "DateTimeCellType")
            //{
            //    // 서브에디터가 열리지 않도록 합니다.
            //    return;
            //}

            if (pfpSpread.Sheets[0].Columns[e.Column].CellType == null)
            {
                //e.Cancel = true;
                return;
            }
            if (pfpSpread.Sheets[0].Columns[e.Column].CellType.ToString() == "DateTimeCellType")
            {
                // 서브에디터가 열리지 않도록 합니다.
                return;
            }
            // 서브에디터가 열리는 이벤트가 발생한 셀이 숫자 셀이라면
            if (pfpSpread.Sheets[0].Columns[e.Column].CellType.ToString() == "NumberCellType")
            {
                // 서브에디터가 열리지 않도록 합니다.
                e.Cancel = true;
            }
        }

        private static void pfpSpread_ClipboardChanging(object sender, EventArgs e)
        {
            try
            {
                FpSpread pfpSpread = sender as FpSpread;

                if (!pfpSpread.Focus())
                {
                    return;
                }

                FarPoint.Win.Spread.Model.CellRange cr = default(FarPoint.Win.Spread.Model.CellRange);

                cr = pfpSpread.Sheets[0].GetSelection(0);

                string copy = string.Empty;

                int row = cr.Row;
                int RowCount = cr.RowCount;
                int Column = cr.Column;
                int ColumnCount = cr.ColumnCount;

                if (row == -1)
                {
                    row = 0;
                }
                if (Column == -1)
                {
                    Column = 0;
                }
                if (RowCount == -1)
                {
                    RowCount = pfpSpread.Sheets[0].RowCount;
                }

                if (ColumnCount == -1)
                {
                    ColumnCount = pfpSpread.Sheets[0].ColumnCount;
                }

                for (int i = cr.Row; i <= row + RowCount - 1; i++)
                {
                    for (int j = Column; j <= Column + ColumnCount - 1; j++)
                    {
                        if (pfpSpread.Sheets[0].Columns[j].Visible)
                        {
                            if (i == -1)
                            {
                                copy += pfpSpread.Sheets[0].Columns[j].Label + ((char)9).ToString();
                            }
                            else
                            {
                                copy += pfpSpread.Sheets[0].GetText(i, j).ToString() + ((char)9).ToString();
                            }
                        }

                    }

                    copy = copy.Substring(0, copy.Length - 1);

                    copy += ((char)13).ToString();
                }

                //copy = copy.Substring(0, copy.Length - 1);

                System.Windows.Forms.Clipboard.SetText(copy);
                //pfpSpread.ActiveSheet.ClipboardCopy();

                string textdata = (string)System.Windows.Forms.Clipboard.GetDataObject().GetData(System.Windows.Forms.DataFormats.Text);

            }
            catch (Exception _Exception)
            {

            }

        }

        private static void pfpSpread_ClipboardPasting(object sender, ClipboardPastingEventArgs e)
        {
            try
            {
                xFpSpread pfpSpread = sender as xFpSpread;

                if (!pfpSpread.Sheets[0].Columns[pfpSpread.ActiveSheet.ActiveColumnIndex].Locked)
                {
                    FarPoint.Win.Spread.Model.CellRange cr = default(FarPoint.Win.Spread.Model.CellRange);

                    cr = pfpSpread.Sheets[0].GetSelection(0);

                    if (cr == null)
                    {
                        return;
                    }
                    e.Handled = true;

                    string textdata = (string)System.Windows.Forms.Clipboard.GetDataObject().GetData(System.Windows.Forms.DataFormats.Text);

                    //textdata = textdata.Replace("\r\n", "");
                    string[] a = textdata.Split(new char[] { (char)13 });

                    int rowcount = a.Length - 1;

                    string[] b = a[0].Split(new char[] { (char)9 });

                    int colcount = b.Length;


                    for (int i = cr.Row; i <= cr.Row + cr.RowCount - 1; i++)
                    {
                        for (int x = 0; x <= rowcount - 1; x++)
                        {
                            b = a[x].Split(new char[] { (char)9 });

                            for (int j = cr.Column; j <= cr.Column + cr.ColumnCount - 1; j += colcount)
                            {
                                for (int y = 0; y <= colcount - 1; y++)
                                {
                                    int col = j + y;
                                    int row = i + x;

                                    if (pfpSpread.Sheets[0].RowHeader.Cells[row, 0].Text != "합계")
                                    {
                                        while (!pfpSpread.Sheets[0].Columns[col].Visible)
                                        {
                                            col++;
                                        }
                                        if (!pfpSpread.Sheets[0].Columns[col].Locked)
                                        {
                                            string myStr = b[y];

                                            myStr = myStr.Trim((char)10, (char)30);

                                            if (pfpSpread.Sheets[0].Columns[col].CellType.ToString() == "NumberCellType")
                                            {
                                                decimal val;
                                                if (Decimal.TryParse(myStr, out val))
                                                {
                                                    pfpSpread.Sheets[0].SetValue(row, col, val);
                                                }
                                                else
                                                {
                                                    pfpSpread.Sheets[0].SetValue(row, col, 0);
                                                }
                                            }
                                            if (pfpSpread.Sheets[0].Columns[col].CellType.ToString() == "TextCellType")
                                            {
                                                pfpSpread.Sheets[0].SetValue(row, col, myStr);
                                            }
                                            if (pfpSpread.Sheets[0].Columns[col].CellType.ToString() == "ComboBoxCellType")
                                            {
                                                pfpSpread.Sheets[0].SetText(row, col, myStr);
                                            }
                                            if (pfpSpread.Sheets[0].Columns[col].CellType.ToString() == "DateTimeCellType")
                                            {
                                                DateTime val;
                                                if (DateTime.TryParse(myStr, out val))
                                                {
                                                    pfpSpread.Sheets[0].SetValue(row, col, val);
                                                }
                                                // pfpSpread.Sheets[0].SetValue(row, col, myStr);
                                            }

                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, row, col));
                                        }
                                    }
                                }
                            }
                        }

                    }
                    // System.Windows.Forms.Clipboard.Clear();

                }

            }
            catch (Exception _Exception)
            {

            }

        }


        public void ContextMenuStrip_Setting(xFpSpread pfpSpread)
        {
            try
            {
                ContextMenuStrip m = new ContextMenuStrip();

                ToolStripMenuItem mVisible = new ToolStripMenuItem("숨김");
                ToolStripMenuItem mfilter = new ToolStripMenuItem("필터");
                ToolStripMenuItem mfix = new ToolStripMenuItem("고정");

                for (int i = 0; i < pfpSpread.Sheets[0].ColumnCount; i++)
                {
                    if (pfpSpread.Sheets[0].Columns[i].Visible)
                    {
                        string name = pfpSpread.Sheets[0].Columns[i].Label;
                        if (pfpSpread.Sheets[0].Columns[i].CellType != null)
                        {
                            if (!name.Contains("*") &&
                                pfpSpread.Sheets[0].Columns[i].CellType.ToString() != "CheckBoxCellType"
                                )
                            {
                                ToolStripMenuItem visible = new ToolStripMenuItem(name);

                                visible.Name = pfpSpread.Sheets[0].Columns[i].Tag.ToString();
                                visible.Click += new EventHandler(delegate
                                {
                                    try
                                    {
                                        if (pfpSpread.Sheets[0].Columns[visible.Name].Visible)
                                        {
                                            pfpSpread.Sheets[0].Columns[visible.Name].Visible = false;
                                            visible.Image = Resources.btn_spread_check_01;
                                        }
                                        else
                                        {
                                            pfpSpread.Sheets[0].Columns[visible.Name].Visible = true;
                                            visible.Image = null;
                                        }
                                        //new MenuSave_Business().MenuSave_A10(pfpSpread);
                                    }
                                    catch (Exception err)
                                    {

                                    }
                                });

                                mVisible.DropDownItems.Add(visible);
                                mVisible.Image = Resources.hide;
                            }
                            ToolStripMenuItem filter = new ToolStripMenuItem(name);
                            filter.Name = pfpSpread.Sheets[0].Columns[i].Tag.ToString();
                            filter.Click += new EventHandler(delegate
                            {
                                try
                                {
                                    if (pfpSpread.Sheets[0].Columns[filter.Name].AllowAutoFilter)
                                    {
                                        pfpSpread.Sheets[0].Columns[filter.Name].AllowAutoFilter = false;
                                        filter.Image = null;
                                    }
                                    else
                                    {
                                        pfpSpread.Sheets[0].Columns[filter.Name].AllowAutoFilter = true;
                                        filter.Image = Resources.btn_spread_check_01;
                                    }
                                    //new MenuSave_Business().MenuSave_A10(pfpSpread);

                                }
                                catch (Exception err)
                                {

                                }
                            });
                            mfilter.DropDownItems.Add(filter);
                            mfilter.Image = Resources.filter_16;

                            ToolStripMenuItem fix = new ToolStripMenuItem(name);


                            fix.Name = pfpSpread.Sheets[0].Columns[i].Tag.ToString();
                            fix.Click += new EventHandler(delegate

                            {
                                try
                                {
                                    for (int k = 0; k < mfix.DropDownItems.Count; k++)
                                    {
                                        if (mfix.DropDownItems[k].Image != null)
                                        {
                                            mfix.DropDownItems[k].Image = null;
                                        }
                                    }

                                    if (pfpSpread.ActiveSheet.FrozenColumnCount != pfpSpread.ActiveSheet.Columns[fix.Name].Index + 1)
                                    {
                                        fix.Image = Resources.btn_spread_check_01;
                                        pfpSpread.ActiveSheet.FrozenColumnCount = pfpSpread.ActiveSheet.Columns[fix.Name].Index + 1;
                                    }
                                    else
                                    {
                                        pfpSpread.ActiveSheet.FrozenColumnCount = 0;
                                    }

                                }
                                catch (Exception err)
                                {

                                }
                            });
                            mfix.DropDownItems.Add(fix);
                            mfix.Image = Resources.Pin2__16;
                        }
                    }
                }

                m.Items.Add(mVisible);
                m.Items.Add(mfilter);
                m.Items.Add(mfix);

                //FarPoint.Win.Spread.ConditionalFormattingIconSetStyle
                m.Items.Add("Excel Save", Resources.Excel_16, new EventHandler((delegate
                {
                    try
                    {
                        SaveFileDialog saveFile = new SaveFileDialog();
                        saveFile.InitialDirectory = @"c:";
                        saveFile.Title = "데이터 Excel 저장 위치 지정";
                        saveFile.DefaultExt = "xlsx";
                        saveFile.Filter = "Xlsx Files(*.xlsx)|*.xlsx";
                        if (saveFile.ShowDialog() == DialogResult.OK)
                        {
                            pfpSpread.Sheets[0].Protect = false;
                            pfpSpread.SaveExcel(saveFile.FileName.ToString(), FarPoint.Excel.ExcelSaveFlags.UseOOXMLFormat | FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
                            CustomMsg.ShowMessageLink("Excel 파일로 저장 하였습니다.\r\n" + saveFile.FileName.ToString(), "Excel 저장 알림");
                        }


                    }
                    catch (Exception err)
                    {

                    }
                })));
                //m.Items.Add("test", null, new EventHandler(colorMenuItem_Click));

                bool ck = true;
                for (int i = 0; i < pfpSpread.Sheets[0].ColumnCount; i++)
                {
                    if (pfpSpread.Sheets[0].Columns[i].Visible == true)
                    {
                        if (pfpSpread.Sheets[0].Columns[i].Locked != true)
                        {
                            ck = false;
                        }
                    }
                }
                if (ck == false)
                {
                    m.Items.Add("삭제", Resources.close_16x16, new EventHandler((delegate
                    {
                        try
                        {
                            DialogResult _DialogResult1 = CustomMsg.ShowMessage("삭제하시겠습니까?", "Question", MessageBoxButtons.YesNo);
                            if (_DialogResult1 == DialogResult.Yes)
                            {

                                FarPoint.Win.Spread.Model.CellRange[] selectedRanges =  pfpSpread.ActiveSheet.GetSelections();
                                foreach (FarPoint.Win.Spread.Model.CellRange range in selectedRanges)
                                {
                                    for (int i = range.Row; i < range.Row + range.RowCount; i++)
                                    {
                                        if (pfpSpread.Sheets[0].RowHeader.Cells[i, 0].Text != "입력")
                                        {
                                            pfpSpread.Sheets[0].RowHeader.Cells[i, 0].Text = "삭제";
                                            pfpSpread.Sheets[0].SetValue(i, "USE_YN", "N");
                                        }
                                    }
                                }
                                foreach (FarPoint.Win.Spread.Model.CellRange range in selectedRanges)
                                {
                                    int del = 0;

                                    for (int i = range.Row; i < range.Row + range.RowCount; i++)
                                    {
                                        //MessageBox.Show((i + del).ToString());

                                        if (pfpSpread.Sheets[0].RowHeader.Cells[(i + del), 0].Text == "입력")
                                        {
                                            FpSpreadManager.SpreadRowRemove(pfpSpread, 0, (i + del));
                                            del--;
                                        }
                                    }
                                }

                            }

                        }
                        catch (Exception err)
                        {
                            MessageBox.Show(err.Message);
                        }
                    })));

                }
                ///20230316 방찬석 추가 -- 검색어 찾기 기능
                m.Items.Add("검색어로 찾기", Resources.find, new EventHandler((delegate
                {
                    try
                    {
                        foreach (Form openForm in Application.OpenForms)
                        {
                            if (openForm.Name == "FindSpread") // 열린 폼의 이름 검사
                            {
                                if (openForm.WindowState == FormWindowState.Minimized)
                                {  // 폼을 최소화시켜 하단에 내려놓았는지 검사
                                    openForm.WindowState = FormWindowState.Normal;
                                    //openForm.Location = new Point(this.Location.X + this.Width, this.Location.Y);
                                }
                                //((frmPopupSum)openForm).SetInit(pfpSpread);
                                openForm.Activate();
                                return;
                            }

                        }

                        FindSpread popup = new FindSpread(pfpSpread);
                        popup.Show(); //showDialog 시 Main 시트 클릭이 안되어 Show로 진행. 차후 변경 가능.
                    }
                    catch (Exception err)
                    {

                    }
                })));
                m.Items.Add("합계", Resources.calculator16, new EventHandler((delegate
                {
                    try
                    {
                        //합계...
                        FarPoint.Win.Spread.Model.CellRange cr;
                        cr = pfpSpread.ActiveSheet.GetSelection(0);

                        //Cell acell, mycell;
                        //acell = pfpSpread.ActiveSheet.Cells[cr.Row, cr.Column];
                        //mycell = pfpSpread.ActiveSheet.Cells[cr.Row + cr.RowCount, cr.Column];

                        if (pfpSpread.ActiveSheet.Columns[cr.Column].CellType == null)
                        {
                            return;
                        }

                        if (pfpSpread.ActiveSheet.Columns[cr.Column].CellType.ToString() == "NumberCellType")
                        {

                            //pfpSpread.ActiveSheet.ColumnFooter.Cells[0, cr.Column].CellType 

                            DataTable clacDT = new DataTable();
                            clacDT.Columns.Add("calc", typeof(decimal));

                            for (int i = 0; i < cr.RowCount; i++)
                            {
                                DataRow dr = clacDT.NewRow();
                                dr["calc"] = pfpSpread.ActiveSheet.Cells[cr.Row + i, cr.Column].Value;
                                clacDT.Rows.Add(dr);
                            }

                            //FindSpread popup = new FindSpread(pfpSpread);
                            //popup.Show();

                            foreach (Form openForm in Application.OpenForms)
                            {
                                if (openForm.Name == "frmPopupSum") // 열린 폼의 이름 검사
                                {
                                    if (openForm.WindowState == FormWindowState.Minimized)
                                    {  // 폼을 최소화시켜 하단에 내려놓았는지 검사
                                        openForm.WindowState = FormWindowState.Normal;
                                        //openForm.Location = new Point(this.Location.X + this.Width, this.Location.Y);
                                    }
                                    ((frmPopupSum)openForm).SetInit(clacDT);
                                    openForm.Activate();
                                    return;
                                }
                            }
                            frmPopupSum popup = new frmPopupSum(clacDT);
                            popup.Show();

                        }
                        else
                        {
                            CustomMsg.ShowMessage("숫자 형식 Cell만 사용 하실 수 있습니다.", "합계 불가 알림");
                        }


                    }
                    catch (Exception err)
                    {
                        CustomMsg.ShowMessage("숫자 형식 Cell만 사용 하실 수 있습니다.", "합계 불가 알림");
                    }
                })));
                m.Items.Add("옵션", Resources.Pin2__16, new EventHandler((delegate
                {
                    try
                    {
                        BaseFormSetting baseFormSetting = new BaseFormSetting( pfpSpread._menu_name,pfpSpread,pfpSpread._user_account);

                        baseFormSetting.Show();
                    }
                    catch (Exception err)
                    {
                        
                    }
                })));
                pfpSpread.ContextMenuStrip = m;
            }
            catch (Exception _Exception)
            {

            }

        }

        public static void SpreadColumnComboBoxChanging(object cellTyp, string pCodeType, string pFirst, string pSecond)
        {
            ComboBoxCellType pCellType = cellTyp as ComboBoxCellType;
            if (pCodeType != "")
            {
                // DB자료 갖고오기
                DataTable pDataTable = new CoreBusiness().Spread_ComboBox(pCodeType, pFirst, pSecond);
                if (pDataTable != null)
                {

                    string[] itemData = new string[pDataTable.Rows.Count];
                    string[] items = new string[pDataTable.Rows.Count];
                    for (int i = 0; i < pDataTable.Rows.Count; i++)
                    {
                        itemData[i] = pDataTable.Rows[i][0].ToString().Trim();
                        items[i] = pDataTable.Rows[i][1].ToString().Trim();
                    }
                    //해당셀의 Value값들..
                    pCellType.ItemData = itemData;
                    //해당셀의 Text값들..
                    pCellType.Items = items;
                    pCellType.EditorValue = EditorValue.ItemData;
                }
                else
                {
                    pCellType.Items = null;
                }
                //pCellType.EditorValue = FarPoint.Win.Spread.CellType.EditorValue.ItemData;
                //pCellType.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.AllArrows;
                //pCellType.AutoSearch = FarPoint.Win.AutoSearch.SingleCharacter;
                //pCellType.Editable = false;
                //pCellType.ListAlignment = FarPoint.Win.ListAlignment.Left;
                //pCellType.ListOffset = 0;
                //pCellType.ListWidth = pCellType.ListWidth;
                //pCellType.MaxDrop = 10;
            }

        }


        public static void SpreadAutoWidth(FpSpread pfpSpread, int pSheet)
        {
            try
            {
                foreach (Column column in pfpSpread.Sheets[pSheet].ColumnHeader.Columns)
                {
                    if (column.GetPreferredWidth() < 30)
                    {
                        column.Width = 30;
                    }
                    else
                    {
                        column.Width = column.GetPreferredWidth();
                    }

                }

                if (pfpSpread.Sheets[pSheet].ColumnHeader.Rows[0].GetPreferredHeight() < 30)
                {
                    pfpSpread.Sheets[pSheet].ColumnHeader.Rows[0].Height = 30;
                }
                else
                {
                    pfpSpread.Sheets[pSheet].ColumnHeader.Rows[0].Height = pfpSpread.Sheets[pSheet].ColumnHeader.Rows[0].GetPreferredHeight();
                }

            }
            catch (Exception pException)
            {
                throw new ExceptionManager(null, "SpreadSetHeader", pException);
            }
            pfpSpread.Update();
        }
        #endregion

        #region ○ 셀타입 추가영역

        // 11 버튼 처리
        public static void SpreadColumnAddButton(FpSpread pfpSpread, int pSheet, int pCol, string pHeaderName, String pCodeType, String pCodeName)
        {
            try
            {
                MYComboBoxCellType pCellType = new MYComboBoxCellType();

                pCellType.pCodeType = pCodeType;

                pCellType.pFirst = pCodeName;

                if (pCodeType != "")
                {
                    // DB자료 갖고오기
                    DataTable pDataTable = new CoreBusiness().Spread_ComboBox(pCodeType, pCodeName,"");

                    if (pDataTable != null & pDataTable.Rows.Count != 0)
                    {
                        DataRow row;

                        row = pDataTable.NewRow();
                        row["CD"] = DBNull.Value;
                        row["CD_NM"] = DBNull.Value;
                        pDataTable.Rows.InsertAt(row, 0);
                        string[] itemData = new string[pDataTable.Rows.Count];
                        string[] items = new string[pDataTable.Rows.Count];
                        for (int i = 0; i < pDataTable.Rows.Count; i++)
                        {
                            itemData[i] = pDataTable.Rows[i][0].ToString().Trim();
                            items[i] = pDataTable.Rows[i][1].ToString().Trim();
                            //items[i] = pDataTable.Rows[i][1].ToString().Trim().Replace("\r"," ").Replace("\n", " ").Replace("\t", " ").Replace('"', ' ');
                        }
                        //해당셀의 Value값들..
                        pCellType.ItemData = itemData;
                        //해당셀의 Text값들..
                        pCellType.Items = items;
                        pCellType.EditorValue = EditorValue.ItemData;
                        //pCellType.EditorValue = FarPoint.Win.Spread.CellType.EditorValue.ItemData;
                    }
                    else
                    {
                        pCellType.Items = null;
                    }
                }

                pCellType.EditorValue = FarPoint.Win.Spread.CellType.EditorValue.ItemData;
                pCellType.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.AllArrows;
                pCellType.AutoSearch = FarPoint.Win.AutoSearch.SingleCharacter;
                pCellType.Editable = false;
                pCellType.ListAlignment = FarPoint.Win.ListAlignment.Left;
                pCellType.ListOffset = 0;
                // pCellType.ListWidth = int.Parse(pLength);
                pCellType.MaxDrop = 10;
                pfpSpread.Sheets[pSheet].Columns[pCol].CellType = pCellType;

            }
            catch (Exception pException)
            {
                throw new ExceptionManager(null, "SpreadColumnAddButton", pException);
            }
        }
        public static void SpreadColumnAddButton2(FpSpread pfpSpread, int pSheet, int pCol, string pHeaderName, String pCodeType)
        {
            try
            {
                ButtonCellType pCellType = new ButtonCellType();
                pCellType.TwoState = false;
                pCellType.ButtonColor = COLOR_Butt;
                switch (pCodeType)
                {
                    case "":  //
                        pCellType.Picture = _pFind_01;
                        pCellType.PictureDown = _pFind_01;
                        break;
                    case "0":  //
                        pCellType.Picture = _pFind_01;
                        pCellType.PictureDown = _pFind_01;
                        break;
                    case "1":  //
                        pCellType.Text = pHeaderName;
                        break;
                    case "2":  //
                        pCellType.Picture = _pFind_02;
                        pCellType.PictureDown = _pFind_02;
                        break;
                    case "3":  //
                        pCellType.Picture = _pFind_03;
                        pCellType.PictureDown = _pFind_03;
                        break;
                    case "4":  //
                        pCellType.Picture = _pFind_04;
                        pCellType.PictureDown = _pFind_04;
                        break;
                    case "5":
                        pCellType.Picture = _pFind_05;
                        pCellType.PictureDown = _pFind_05;

                        break;
                    case "6":
                        pCellType.Picture = _pFind_06;
                        pCellType.PictureDown = _pFind_06;
                        break;
                    case "7":
                        pCellType.Picture = _pFind_07;
                        pCellType.PictureDown = _pFind_07;
                        break;
                    default:
                        break;

                }
                pCellType.TextAlign = ButtonTextAlign.TextTopPictBottom;
                pfpSpread.Sheets[pSheet].Columns[pCol].CellType = pCellType;
                pfpSpread.Sheets[pSheet].Columns[pCol].Resizable = false;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(null, "SpreadColumnAddButton", pException);
            }
        }
        // 12 체크박스 처리
        public static void SpreadColumnAddCheckBox(FpSpread pfpSpread, int pSheet, int pCol, String pCodeName)
        {
            CheckBoxCellType pCellType = new CheckBoxCellType();
            pCellType.ThreeState = false;
            pCodeName = "5";
            switch (pCodeName)
            {
                case "":    // 기본
                    pCellType.Picture.False = _pBlank_01;
                    pCellType.Picture.FalseDisabled = _pBlank_01;
                    pCellType.Picture.FalsePressed = _pBlank_01;
                    pCellType.Picture.True = _pCheck_01;
                    pCellType.Picture.TrueDisabled = _pCheck_01;
                    pCellType.Picture.TruePressed = _pCheck_01;
                    break;
                case "0":   // 기본
                    pCellType.Picture.False = _pBlank_01;
                    pCellType.Picture.FalseDisabled = _pBlank_01;
                    pCellType.Picture.FalsePressed = _pBlank_01;
                    pCellType.Picture.True = _pCheck_01;
                    pCellType.Picture.TrueDisabled = _pCheck_01;
                    pCellType.Picture.TruePressed = _pCheck_01;
                    break;
                case "1":   // 에러
                    pCellType.Picture.False = _pBlank_01;
                    pCellType.Picture.FalseDisabled = _pBlank_01;
                    pCellType.Picture.FalsePressed = _pBlank_01;
                    pCellType.Picture.True = _pError_01;
                    pCellType.Picture.TrueDisabled = _pError_01;
                    pCellType.Picture.TruePressed = _pError_01;
                    break;
                case "2":   // 선택
                    pCellType.Picture.False = _pBlank_01;
                    pCellType.Picture.FalseDisabled = _pBlank_01;
                    pCellType.Picture.FalsePressed = _pBlank_01;
                    pCellType.Picture.True = _pChoice_01;
                    pCellType.Picture.TrueDisabled = _pChoice_01;
                    pCellType.Picture.TruePressed = _pChoice_01;
                    break;
                case "3":   // 선택해제
                    pCellType.Picture.False = _pUnSelect_01;
                    pCellType.Picture.FalseDisabled = _pUnSelect_01;
                    pCellType.Picture.FalsePressed = _pUnSelect_01;
                    pCellType.Picture.True = _pSelect_01;
                    pCellType.Picture.TrueDisabled = _pSelect_01;
                    pCellType.Picture.TruePressed = _pSelect_01;
                    break;
                case "4":   // 선택해제
                    pCellType.Picture.False = _pNg_01;
                    pCellType.Picture.FalseDisabled = _pNg_01;
                    pCellType.Picture.FalsePressed = _pNg_01;
                    pCellType.Picture.True = _pOk_01;
                    pCellType.Picture.TrueDisabled = _pOk_01;
                    pCellType.Picture.TruePressed = _pOk_01;
                    break;
                case "8":   // 선택해제 텍스트
                    pCellType.TextTrue = "선택";
                    pCellType.TextFalse = "해제";
                    break;
                case "9":   // 선택해제 텍스트
                    pCellType.TextTrue = "";
                    pCellType.TextFalse = "";
                    break;
                case "10":   // 선택해제 텍스트
                    pCellType.TextTrue = "마감";
                    pCellType.TextFalse = "진행";
                    break;
            }
            pfpSpread.Sheets[pSheet].Columns[pCol].CellType = pCellType;
        }
        public static void SpreadColumnAddDisplayCheckBox(FpSpread pfpSpread, int pSheet, int pCol, String pCodeName)
        {
            CheckBoxCellType pCellType = new CheckBoxCellType();
            pCellType.ThreeState = false;
            pCodeName = "5";
            switch (pCodeName)
            {
                case "":    // 기본
                    pCellType.Picture.False = _pBlank_01;
                    pCellType.Picture.FalseDisabled = _pBlank_01;
                    pCellType.Picture.FalsePressed = _pBlank_01;
                    pCellType.Picture.True = _pCheck_01;
                    pCellType.Picture.TrueDisabled = _pCheck_01;
                    pCellType.Picture.TruePressed = _pCheck_01;
                    break;
                case "0":   // 기본
                    pCellType.Picture.False = _pBlank_01;
                    pCellType.Picture.FalseDisabled = _pBlank_01;
                    pCellType.Picture.FalsePressed = _pBlank_01;
                    pCellType.Picture.True = _pCheck_01;
                    pCellType.Picture.TrueDisabled = _pCheck_01;
                    pCellType.Picture.TruePressed = _pCheck_01;
                    break;
                case "1":   // 에러
                    pCellType.Picture.False = _pBlank_01;
                    pCellType.Picture.FalseDisabled = _pBlank_01;
                    pCellType.Picture.FalsePressed = _pBlank_01;
                    pCellType.Picture.True = _pError_01;
                    pCellType.Picture.TrueDisabled = _pError_01;
                    pCellType.Picture.TruePressed = _pError_01;
                    break;
                case "2":   // 선택
                    pCellType.Picture.False = _pBlank_01;
                    pCellType.Picture.FalseDisabled = _pBlank_01;
                    pCellType.Picture.FalsePressed = _pBlank_01;
                    pCellType.Picture.True = _pChoice_01;
                    pCellType.Picture.TrueDisabled = _pChoice_01;
                    pCellType.Picture.TruePressed = _pChoice_01;
                    break;
                case "3":   // 선택해제
                    pCellType.Picture.False = _pUnSelect_01;
                    pCellType.Picture.FalseDisabled = _pUnSelect_01;
                    pCellType.Picture.FalsePressed = _pUnSelect_01;
                    pCellType.Picture.True = _pSelect_01;
                    pCellType.Picture.TrueDisabled = _pSelect_01;
                    pCellType.Picture.TruePressed = _pSelect_01;
                    break;
                case "4":   // 선택해제
                    pCellType.Picture.False = _pNg_01;
                    pCellType.Picture.FalseDisabled = _pNg_01;
                    pCellType.Picture.FalsePressed = _pNg_01;
                    pCellType.Picture.True = _pOk_01;
                    pCellType.Picture.TrueDisabled = _pOk_01;
                    pCellType.Picture.TruePressed = _pOk_01;
                    break;
                case "8":   // 선택해제 텍스트
                    pCellType.TextTrue = "선택";
                    pCellType.TextFalse = "해제";
                    break;
                case "9":   // 선택해제 텍스트
                    pCellType.TextTrue = "";
                    pCellType.TextFalse = "";
                    break;
                case "10":   // 선택해제 텍스트
                    pCellType.TextTrue = "마감";
                    pCellType.TextFalse = "진행";
                    break;
            }
            pfpSpread.Sheets[pSheet].Columns[pCol].CellType = pCellType;
        }
        // 14 콤보박스 처리
        public static void SpreadColumnAddComboBox(FpSpread pfpSpread, int pSheet, int pCol, string pLength, string pCodeType, string pFirst, string pSecond, string isALL)
        {
            ComboBoxCellType pCellType = new ComboBoxCellType();
            if (pCodeType != "")
            {
                // DB자료 갖고오기
                DataTable pDataTable = new CoreBusiness().Spread_ComboBox(pCodeType, pFirst, pSecond);

                if (pDataTable != null && pDataTable.Rows.Count != 0)
                {
                    if (isALL == "N")
                    {
                        DataRow row;

                        row = pDataTable.NewRow();
                        row["CD"] = DBNull.Value;
                        row["CD_NM"] = DBNull.Value;
                        pDataTable.Rows.InsertAt(row, 0);
                    }
                    string[] itemData = new string[pDataTable.Rows.Count];
                    string[] items = new string[pDataTable.Rows.Count];
                    for (int i = 0; i < pDataTable.Rows.Count; i++)
                    {
                        itemData[i] = pDataTable.Rows[i][0].ToString().Trim();
                        items[i] = pDataTable.Rows[i][1].ToString().Trim();
                    }
                    //해당셀의 Value값들..
                    pCellType.ItemData = itemData;
                    //해당셀의 Text값들..
                    pCellType.Items = items;
                    pCellType.EditorValue = EditorValue.ItemData;
                    //pCellType.EditorValue = FarPoint.Win.Spread.CellType.EditorValue.ItemData;
                }
                else
                {
                    pCellType.Items = null;
                }
            }
            pCellType.EditorValue = FarPoint.Win.Spread.CellType.EditorValue.ItemData;
            pCellType.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.AllArrows;
            pCellType.AutoSearch = FarPoint.Win.AutoSearch.SingleCharacter;
            pCellType.Editable = false;
            pCellType.ListAlignment = FarPoint.Win.ListAlignment.Left;
            pCellType.ListOffset = 0;
            // pCellType.ListWidth = int.Parse(pLength);
            pCellType.MaxDrop = 10;
            pfpSpread.Sheets[pSheet].Columns[pCol].CellType = pCellType;
        }
        public static void SpreadColumnAddFIND(FpSpread pfpSpread, int pSheet, int pCol, string pPoint, string pCodeType, string pFirst, string pSecond)
        {
            MYNumberCellType pCellType = new MYNumberCellType();

            pCellType.pDataTable = new CoreBusiness().Spread_ComboBox(pCodeType, pFirst, pSecond);

            if (pPoint != "0")
            {
                int Place = int.Parse(pPoint);
                pCellType.DecimalPlaces = Place;    // 소수점 위치
                pCellType.FixedPoint = true;
                //pCellType.Format("N2");// 소수점을 표시여부
            }
            else
            {
                pCellType.DecimalPlaces = 0;
                pCellType.FixedPoint = false;
            }
            //pCellType.Format("N2");// 소수점을 표시여부
            pCellType.Separator = ",";              //천단위 구분자
            pCellType.ShowSeparator = true;         //천단위 구분자 표시 여부
            pCellType.MaximumValue = 999999999999;  //최대값
            pCellType.MinimumValue = -999999999999; //최소값
            pCellType.NegativeRed = true;
            pCellType.ReadOnly = false;

            if (pCodeType == "+")
            {
                pCellType.MinimumValue = 0; //최소값
            }
            else if (pCodeType == "-")
            {
                pCellType.MaximumValue = 0; //최대값
            }

            pfpSpread.Sheets[pSheet].Columns[pCol].CellType = pCellType;
            pfpSpread.Sheets[pSheet].Columns[pCol].CellPadding.Right = 3;

        }
        public static void SpreadColumnAddComboBox(FpSpread pfpSpread, int pSheet, int pCol, string pLength, string pCodeType, string pFirst, string pSecond)
        {
            ComboBoxCellType pCellType = new ComboBoxCellType();
            if (pCodeType != "")
            {
                // DB자료 갖고오기
                DataTable pDataTable = new CoreBusiness().Spread_ComboBox(pCodeType, pFirst, pSecond);

                if (pDataTable != null)
                {
                    string[] itemData = new string[pDataTable.Rows.Count];
                    string[] items = new string[pDataTable.Rows.Count];
                    for (int i = 0; i < pDataTable.Rows.Count; i++)
                    {
                        itemData[i] = pDataTable.Rows[i][0].ToString().Trim();
                        items[i] = pDataTable.Rows[i][1].ToString().Trim();
                    }
                    //해당셀의 Value값들..
                    pCellType.ItemData = itemData;
                    //해당셀의 Text값들..
                    pCellType.Items = items;
                    pCellType.EditorValue = EditorValue.ItemData;
                    //pCellType.EditorValue = FarPoint.Win.Spread.CellType.EditorValue.ItemData;
                }
                else
                {
                    pCellType.Items = null;
                }
            }
            pCellType.EditorValue = FarPoint.Win.Spread.CellType.EditorValue.ItemData;
            pCellType.AcceptsArrowKeys = FarPoint.Win.SuperEdit.AcceptsArrowKeys.AllArrows;
            pCellType.AutoSearch = FarPoint.Win.AutoSearch.SingleCharacter;
            pCellType.Editable = false;
            pCellType.ListAlignment = FarPoint.Win.ListAlignment.Left;
            pCellType.ListOffset = 0;
            // pCellType.ListWidth = int.Parse(pLength);
            pCellType.MaxDrop = 10;
            pfpSpread.Sheets[pSheet].Columns[pCol].CellType = pCellType;
        }
        // 17 이미지
        public static void SpreadColumnAddImage(FpSpread pfpSpread, int pSheet, int pCol)
        {
            ImageCellType pCellType = new ImageCellType();
            pCellType.Style = RenderStyle.Stretch;
            //pCellType.TransparencyColor = Color.Black;
            //pCellType.TransparencyTolerance = 20;
            pfpSpread.Sheets[pSheet].Columns[pCol].CellType = pCellType;
        }


        // 25 날짜시간 처리
        public static void SpreadColumnAddDateTime(FpSpread pfpSpread, int pSheet, int pCol, string pCodeName)
        {
            try
            {
                DateTimeCellType pCellType = new DateTimeCellType();
                pCellType.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
                switch (pCodeName)
                {
                    case "0":
                        pCellType.UserDefinedFormat = DEFAULT_DATE_FORMAT;
                        break;
                    case "1":
                        pCellType.UserDefinedFormat = DEFAULT_TIME_FORMAT;
                        break;
                    case "2":
                        pCellType.UserDefinedFormat = DEFAULT_DATETIME_FORMAT;
                        break;
                    case "3":
                        pCellType.UserDefinedFormat = "yyyy-MM-dd HH:mm";
                        break;
                    case "4":
                        pCellType.UserDefinedFormat = "yyyy-MM-dd HH";
                        break;
                    case "5":
                        pCellType.UserDefinedFormat = "yyyy-MM";
                        break;
                    case "6":
                        pCellType.UserDefinedFormat = "HH:mm";
                        break;
                    default:
                        //pCellType.UserDefinedFormat = DEFAULT_DATE_FORMAT;
                        pCellType.UserDefinedFormat = DEFAULT_DATE_FORMAT;

                        //pCellType.SimpleEdit = true;

                        pCellType.SubEditor = new FarPoint.Win.Spread.CellType.SpreadDropDownMonthCalendar();
                        pCellType.DropDownButton = true;
                        pCellType.ButtonAlign = FarPoint.Win.ButtonAlign.Right;




                        break;
                }
                pfpSpread.Sheets[pSheet].Columns[pCol].CellType = pCellType;
            }
            catch(Exception)
            {

            }
        }
        public static void SpreadColumnAddDate(FpSpread pfpSpread, int pSheet, int pCol, string pCodeName)
        {
            //MYDateTimeCellType pCellType = new MYDateTimeCellType();
            //pCellType.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            //pCellType.UserDefinedFormat = DEFAULT_DATE_FORMAT;
            //pfpSpread.Sheets[pSheet].Columns[pCol].CellType = pCellType;
        }
        // 28 마스크
        public static void SpreadColumnAddMask(FpSpread pfpSpread, int pSheet, int pCol, string pCodeName)
        {
            try
            {
                MaskCellType pCellType = new MaskCellType();
                pCellType.Mask = pCodeName;
                pCellType.MaskChar = '*';
                pfpSpread.Sheets[pSheet].Columns[pCol].CellType = pCellType;
            }
            catch (Exception)
            {

            }
        }


        // 29 숫자 처리
        public static void SpreadColumnAddNumber(FpSpread pfpSpread, int pSheet, int pCol, string pPoint, string pCodeType)
        {
            NumberCellType pCellType = new NumberCellType();
            if (pPoint != "0")
            {
                int Place = int.Parse(pPoint);
                pCellType.DecimalPlaces = Place;    // 소수점 위치
                pCellType.FixedPoint = true;
                //pCellType.Format("N2");// 소수점을 표시여부
            }
            else
            {
                pCellType.DecimalPlaces = 0;
                pCellType.FixedPoint = false;
            }
            //pCellType.Format("N2");// 소수점을 표시여부
            pCellType.Separator = ",";              //천단위 구분자
            pCellType.ShowSeparator = true;         //천단위 구분자 표시 여부
            pCellType.MaximumValue = 999999999999;  //최대값
            pCellType.MinimumValue = -999999999999; //최소값
            pCellType.NegativeRed = true;
            pCellType.ReadOnly = false;

            if (pCodeType == "+")
            {
                pCellType.MinimumValue = 0; //최소값
            }
            else if (pCodeType == "-")
            {
                pCellType.MaximumValue = 0; //최대값
            }

            pfpSpread.Sheets[pSheet].Columns[pCol].CellType = pCellType;
            pfpSpread.Sheets[pSheet].Columns[pCol].CellPadding.Right = 3;
        }

        public static void SpreadColumnAddDisplayNumber(FpSpread pfpSpread, int pSheet, int pCol, string pPoint, string pCodeType)
        {
            DisplayNumberCellType pCellType = new DisplayNumberCellType();
            if (pPoint != "0")
            {
                int Place = int.Parse(pPoint);
                pCellType.DecimalPlaces = Place;    // 소수점 위치
                pCellType.FixedPoint = true;
                //pCellType.Format("N2");// 소수점을 표시여부
            }
            else
            {
                pCellType.DecimalPlaces = 0;
                pCellType.FixedPoint = false;
            }
            //pCellType.Format("N2");// 소수점을 표시여부
            pCellType.Separator = ",";              //천단위 구분자
            pCellType.ShowSeparator = true;         //천단위 구분자 표시 여부
            pCellType.MaximumValue = 999999999999;  //최대값
            pCellType.MinimumValue = -999999999999; //최소값
            pCellType.NegativeRed = true;
            pCellType.ReadOnly = false;

            if (pCodeType == "+")
            {
                pCellType.MinimumValue = 0; //최소값
            }
            else if (pCodeType == "-")
            {
                pCellType.MaximumValue = 0; //최대값
            }

            pfpSpread.Sheets[pSheet].Columns[pCol].CellType = pCellType;
            pfpSpread.Sheets[pSheet].Columns[pCol].CellPadding.Right = 3;
        }
        // 32 텍스트 처리
        public static void SpreadColumnAddText(FpSpread pfpSpread, int pSheet, int pCol, string pLength)
        {
            TextCellType pCellType = new TextCellType();
            int MaxLength = int.Parse(pLength);
            pCellType.MaxLength = MaxLength;
            pCellType.Multiline = false;
            pfpSpread.Sheets[pSheet].Columns[pCol].CellType = pCellType;
            pfpSpread.Sheets[pSheet].Columns[pCol].CellPadding.Left = 3;

            // 항후 추가 구현
            //if (1 == 2)
            //{
            //    pCellType.CharacterCasing = CharacterCasing.Upper;
            //    pCellType.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.Ascii;
            //    pCellType.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
            //    pCellType.MaxLength = int.Parse(pLength);
            //    pCellType.Multiline = true;
            //    pCellType.PasswordChar = Convert.ToChar("*");
            //    pCellType.ScrollBars = ScrollBars.Vertical;
            //    pCellType.Static = true;
            //}
        }
        public static void SpreadColumnAddDisplayText(FpSpread pfpSpread, int pSheet, int pCol, string pLength)
        {
            DisplayTextCellType pCellType = new DisplayTextCellType();
            int MaxLength = int.Parse(pLength);
            pCellType.MaxLength = MaxLength;
            pCellType.Multiline = false;
            pfpSpread.Sheets[pSheet].Columns[pCol].CellType = pCellType;
            pfpSpread.Sheets[pSheet].Columns[pCol].CellPadding.Left = 3;

            // 항후 추가 구현
            //if (1 == 2)
            //{
            //    pCellType.CharacterCasing = CharacterCasing.Upper;
            //    pCellType.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.Ascii;
            //    pCellType.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
            //    pCellType.MaxLength = int.Parse(pLength);
            //    pCellType.Multiline = true;
            //    pCellType.PasswordChar = Convert.ToChar("*");
            //    pCellType.ScrollBars = ScrollBars.Vertical;
            //    pCellType.Static = true;
            //}
        }

        // 33 패스워드 처리
        public static void SpreadColumnAddPassword(FpSpread pfpSpread, int pSheet, int pCol, string pLength)
        {
            TextCellType pCellType = new TextCellType();
            int MaxLength = int.Parse(pLength);
            pCellType.MaxLength = MaxLength;
            pCellType.Multiline = false;
            pCellType.PasswordChar = '*';
            pfpSpread.Sheets[pSheet].Columns[pCol].CellType = pCellType;
            pfpSpread.Sheets[pSheet].Columns[pCol].CellPadding.Left = 3;

            // 항후 추가 구현
            //if (1 == 2)
            //{
            //    pCellType.CharacterCasing = CharacterCasing.Upper;
            //    pCellType.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.Ascii;
            //    pCellType.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
            //    pCellType.MaxLength = int.Parse(pLength);
            //    pCellType.Multiline = true;
            //    pCellType.PasswordChar = Convert.ToChar("*");
            //    pCellType.ScrollBars = ScrollBars.Vertical;
            //    pCellType.Static = true;
            //}
        }

        #endregion ○ 셀타입 추가영역 끝

        #region ○ 스프레드 이벤트 처리

        // 키엔터시 다음 로우로 이동
        private static void OnSpreadKeyDown(object sender, KeyEventArgs e)
        {
            FpSpread fpSpread = (FpSpread)sender;
            if (e.KeyCode == Keys.Enter)
            {
                if (fpSpread.ActiveSheet.ActiveRowIndex < fpSpread.ActiveSheet.Rows.Count - 1)
                {
                    fpSpread.ActiveSheet.ActiveRowIndex++;
                    e.Handled = true;
                }
            }
        }

        #endregion

        #region ○ 로우 관련 메소드

        // 로우추가 (기본으로 마지막행에)
        public static void SpreadRowAdd(xFpSpread pfpSpread, int pSheet)
        {
            pfpSpread.Focus();
            int pRow = pfpSpread.Sheets[pSheet].Rows.Count;
            if (pRow < 0)
            {
                throw new ArgumentException("Row Index Out Of Range");
            }
            pfpSpread.Sheets[pSheet].RowCount = pRow + 1;
            pfpSpread.Sheets[pSheet].SetActiveCell(pfpSpread.Sheets[pSheet].Rows.Count - 1, 6, false);
            pfpSpread.Sheets[pSheet].Rows[pfpSpread.Sheets[pSheet].Rows.Count - 1].Label = "입력";
            // 해당위치로 view이동
            pfpSpread.ShowCell(pfpSpread.GetActiveRowViewportIndex()
                          , pfpSpread.GetActiveColumnViewportIndex()
                          , pRow-1
                          , 0
                          , FarPoint.Win.Spread.VerticalPosition.Center
                          , FarPoint.Win.Spread.HorizontalPosition.Left);

            
        }
        public static void SpreadRowAdd(FpSpread pfpSpread, int pSheet)
        {
            pfpSpread.Focus();
            int pRow = pfpSpread.Sheets[pSheet].Rows.Count;
            if (pRow < 0)
            {
                throw new ArgumentException("Row Index Out Of Range");
            }
            pfpSpread.Sheets[pSheet].RowCount = pRow + 1;
            pfpSpread.Sheets[pSheet].SetActiveCell(pfpSpread.Sheets[pSheet].Rows.Count - 1, 6, false);
            pfpSpread.Sheets[pSheet].Rows[pfpSpread.Sheets[pSheet].Rows.Count - 1].Label = "입력";
            // 해당위치로 view이동
            pfpSpread.ShowCell(pfpSpread.GetActiveRowViewportIndex()
                          , pfpSpread.GetActiveColumnViewportIndex()
                          , pRow - 1
                          , 0
                          , FarPoint.Win.Spread.VerticalPosition.Center
                          , FarPoint.Win.Spread.HorizontalPosition.Left);

          
        }
        // 로우추가 지정위치
        public static void SpreadRowAdd(FpSpread pfpSpread, int pSheet, int pCount)
        {
            // pRow는 메소드 오러로드 때문에 사용한 것임
            pfpSpread.Focus();
            int pRow = 0;
            if (pfpSpread.Sheets[pSheet].ActiveCell == null)
                pRow = 0;
            else
                pRow = pfpSpread.Sheets[pSheet].ActiveCell.Row.Index;
            pfpSpread.Sheets[pSheet].AddRows(pRow + 1, pCount);
            pfpSpread.Sheets[pSheet].Rows[pRow + 1].Label = "입력";
        }

        // 로우 삭제
        public static void SpreadRowRemove(FpSpread pfpSpread, int pSheet, int pRow)
        {
            CheckRangeRow(pfpSpread, pSheet, pRow); // 로우범위체크
            SpreadRowRemove(pfpSpread, pSheet, pRow, 1);
        }

        // 로우 삭제
        public static void SpreadRowRemove(FpSpread pfpSpread, int pSheet, int pRow, int pCount)
        {
            pfpSpread.Focus();
            if (pCount < 1)
            {
                throw new ArgumentException(string.Format("Row Index[{0}] Out Of Range", pCount));
            }
            CheckRangeRow(pfpSpread, pSheet, pRow + pCount); // 로우범위체크
            pfpSpread.Sheets[pSheet].Rows.Remove(pRow, pCount);
        }

        // 취소처리
        public static void SpreadRowCancel(FpSpread pfpSpread, int pSheet, int pRow)
        {
            pfpSpread.Focus();
            CheckRangeRow(pfpSpread, pSheet, pRow); // 로우범위체크
            string pHeaderLabel = pfpSpread.Sheets[pSheet].Rows[pRow].Label;
            switch (pHeaderLabel)
            {
                case "입력":
                    SpreadRowRemove(pfpSpread, pSheet, pRow);
                    if (pRow == pfpSpread.Sheets[pSheet].RowCount)
                        pfpSpread.Sheets[pSheet].SetActiveCell(pRow - 1, pfpSpread.Sheets[pSheet].ActiveColumnIndex);
                    break;
                case "수정":
                    pfpSpread.Sheets[pSheet].Rows[pRow].Label = "";
                    if (pRow < pfpSpread.Sheets[pSheet].RowCount)
                        pfpSpread.Sheets[pSheet].SetActiveCell(pRow + 1, pfpSpread.Sheets[pSheet].ActiveColumnIndex);
                    break;
                case "삭제":
                    pfpSpread.Sheets[pSheet].Rows[pRow].Label = "";
                    if (pRow < pfpSpread.Sheets[pSheet].RowCount)
                        pfpSpread.Sheets[pSheet].SetActiveCell(pRow + 1, pfpSpread.Sheets[pSheet].ActiveColumnIndex);
                    break;
            }
        }

        // 변경여부 체크
        public static bool SpreadChangeYN(FpSpread pfpSpread, int pSheet)
        {
            bool pChangeYN = false;
            for (int i = 0; i < pfpSpread.Sheets[pSheet].Rows.Count; i++)
            {
                if (pfpSpread.Sheets[pSheet].RowHeader.Cells[i, 0].Text != "")
                {
                    pChangeYN = true;
                    return pChangeYN;
                }
            }
            return pChangeYN;
        }

        #endregion

        #region ○ 스프레드에 필요한 사항 체크

        // 로우 범위 체크
        private static void CheckRangeRow(FpSpread pfpSpread, int pSheet, int pRow)
        {
            if (pRow < 0 || pRow > pfpSpread.Sheets[pSheet].RowCount)
            {
                throw new ArgumentException(string.Format("Row Index[{0}] Out Of Range", pRow));
            }
        }

        // 컬럼 범위 체크
        private static void CheckRangeColumn(FpSpread pfpSpread, int pSheet, int pCol)
        {
            if (pCol < 0 || pCol > pfpSpread.Sheets[pSheet].ColumnCount)
            {
                throw new ArgumentException(string.Format("Column Index[{0}] Out Of Range", pCol));
            }
        }

        #endregion

        #region ○ 셀관련 함수


        /// <summary>Set_OPC_OK
        /// 지정셀의 락을 설정 (pCnt는 컬럼의 갯수로 기본은 1
        /// </summary>
        public static void SpreadCellLock(FpSpread pfpSpread, int pSheet, int pRow, int pCol, int pCnt)
        {
            pfpSpread.Focus();
            pfpSpread.Sheets[pSheet].Cells[pRow, pCol, pRow, pCol + pCnt - 1].Locked = true;
            pfpSpread.Sheets[pSheet].Cells[pRow, pCol, pRow, pCol + pCnt - 1].BackColor = COLOR_Lock;
        }

        /// <summary>
        /// 지정셀의 락을 해제
        /// </summary>
        public static void SpreadCellUnLock(FpSpread pfpSpread, int pSheet, int pRow, int pCol, int pCnt)
        {
            pfpSpread.Focus();
            int pMod = pRow;
            if ((pMod %= 2) == 0)
            {
                pfpSpread.Sheets[pSheet].Cells[pRow, pCol, pRow, pCol + pCnt - 1].Locked = false;
                pfpSpread.Sheets[pSheet].Cells[pRow, pCol, pRow, pCol + pCnt - 1].BackColor = COLOR_Basi;
            }
            else
            {
                pfpSpread.Sheets[pSheet].Cells[pRow, pCol, pRow, pCol + pCnt - 1].Locked = false;
                pfpSpread.Sheets[pSheet].Cells[pRow, pCol, pRow, pCol + pCnt - 1].BackColor = COLOR_Even;
            }
        }

        /// <summary>
        /// 색상원 원위치
        /// </summary>
        public static void SpreadCellColorReset(FpSpread pfpSpread, int pSheet, int pRow, int pCol, int pCnt)
        {
            pfpSpread.Focus();
            int pMod = pRow;
            if ((pMod %= 2) == 0)
            {
                pfpSpread.Sheets[pSheet].Cells[pRow, pCol, pRow, pCol + pCnt - 1].BackColor = COLOR_Basi;
                for (int i = pCol; i < pCol + pCnt; i++)
                {
                    if (pfpSpread.Sheets[pSheet].Cells[pRow, i, pRow, i].Locked == true)
                    {
                        pfpSpread.Sheets[pSheet].Cells[pRow, i, pRow, i].BackColor = COLOR_Lock;
                    }
                }
            }
            else
            {
                pfpSpread.Sheets[pSheet].Cells[pRow, pCol, pRow, pCol + pCnt - 1].BackColor = COLOR_Even;
                for (int i = pCol; i < pCol + pCnt; i++)
                {
                    if (pfpSpread.Sheets[pSheet].Cells[pRow, i, pRow, i].Locked == true)
                    {
                        pfpSpread.Sheets[pSheet].Cells[pRow, i, pRow, i].BackColor = COLOR_Lock;
                    }
                }
            }
        }


        #endregion

        #region ○ 스프레드 인쇄

        /// <summary>
        /// 스프레드 미리보기
        /// </summary>
        public static void SpreadPrintPreview(FpSpread pfpSpread, int pSheet, PrintOrientation pOperation, string pTitle, string pName)
        {
            try
            {
                PrintInfo info = new PrintInfo();

                //가로세로페이지 설정(자동,가로,세로)
                //info.Orientation = PrintOrientation.Landscape;
                info.Orientation = pOperation;

                string Header = "/c/fz\"16\"/fb1 [" + pTitle + "] /fb0/fz\"9\"/n";
                string Footer = "/l/fz\"9\"출력자 : " + pName + "/fz\"9\"/c페이지 : /p / /pc " + "/r출력일자 : " + DateTime.Now.ToString("yyyy-MM-dd");
                //헤더 넣고(용지 위쪽에 넣을 제목)
                info.Header = Header;
                //아랫말 넣고(용지 아랫쪽에 넣을 페이지 번호 같은것)
                info.Footer = Footer;

                //여백 설정하고
                PrintMargin mg = new PrintMargin();
                mg.Header = 30;
                mg.Footer = 30;
                mg.Top = 20;
                mg.Bottom = 10;
                mg.Left = 30;
                mg.Right = 30;
                //여백 적용하고(위에서 정한 프린터 여백을 적용시키기)
                info.Margin = mg;

                //옵션 설정
                //전체인쇄(현재페이지,전체,선택페이지,지정페이지)
                info.PrintType = PrintType.All;
                //(전체인쇄)에서 지정된 페이지일 경우 페이지 설정
                info.PageStart = 0;
                info.PageEnd = 0;
                //컬럼 헤드를 보일 경우(세로)
                info.ShowRowHeaders = true;
                //컬럼 로우를 보일 경우(가로,제목 이다)
                info.ShowColumnHeaders = true;
                //그리드 라인을 표기할 것인가 말것인가(선 말이다)
                info.ShowGrid = true;
                //페이지 보드 설정(외곽 선, 바깥 네모를 보일것인가)
                info.ShowBorder = true;
                //스트레드 그림자 설정
                info.ShowShadows = false;
                //컬러 설정
                info.ShowColor = true;
                //페이지 자동 설정 부분(페이지 크기를 자동으로 맞출것인가)
                info.UseSmartPrint = false;

                //출력전 미리보기 하겠다고 하고
                info.Preview = true;
                pfpSpread.Sheets[pSheet].PrintInfo = info;

                //출력하기
                pfpSpread.PrintSheet(0);
            }
            catch (Exception pException)
            {
                throw new ArgumentException("스프레드 미리보기 에러 : " + pException.Message);
            }
        }
        /// <summary>
        /// 스프레드 미리보기, 컬럼/로우 컬럼헤드 유무
        /// </summary>
        public static void SpreadPrintPreview(FpSpread pfpSpread, int pSheet, PrintOrientation pOperation, string pTitle, string pName, bool pFit, bool pPreView, bool pShowRowHeaders, bool ShowColumnHeaders)
        {
            try
            {
                PrintInfo info = new PrintInfo();

                //가로세로페이지 설정(자동,가로,세로)
                //info.Orientation = PrintOrientation.Landscape;
                info.Orientation = pOperation;

                string Header = "/c/fz\"16\"/fb1 [" + pTitle + "] /fb0/fz\"9\"/n";
                string Footer = "/l/fz\"9\"출력자 : " + pName + "/fz\"9\"/c페이지 : /p / /pc " + "/r출력일자 : " + DateTime.Now.ToString("yyyy-MM-dd");
                //헤더 넣고(용지 위쪽에 넣을 제목)
                info.Header = Header;
                //아랫말 넣고(용지 아랫쪽에 넣을 페이지 번호 같은것)
                info.Footer = Footer;

                //여백 설정하고
                PrintMargin mg = new PrintMargin();
                mg.Header = 30;
                mg.Footer = 30;
                mg.Top = 20;
                mg.Bottom = 10;
                mg.Left = 30;
                mg.Right = 30;
                //여백 적용하고(위에서 정한 프린터 여백을 적용시키기)
                info.Margin = mg;

                //옵션 설정
                //세로로  출력을 할 경우, fit(자동너비)은 false로 해야함(컬럼너비는, 수동으로  세로로 나오게 조정할 것)
                info.BestFitCols = pFit;
                //전체인쇄(현재페이지,전체,선택페이지,지정페이지)
                info.PrintType = PrintType.All;
                //(전체인쇄)에서 지정된 페이지일 경우 페이지 설정
                info.PageStart = 0;
                info.PageEnd = 0;
                //컬럼 헤드를 보일 경우(세로)
                info.ShowRowHeaders = pShowRowHeaders;
                //컬럼 로우를 보일 경우(가로,제목 이다)
                info.ShowColumnHeaders = ShowColumnHeaders;
                //그리드 라인을 표기할 것인가 말것인가(선 말이다)
                info.ShowGrid = true;
                //페이지 보드 설정(외곽 선, 바깥 네모를 보일것인가)
                info.ShowBorder = true;
                //스트레드 그림자 설정
                info.ShowShadows = false;
                //컬러 설정
                info.ShowColor = true;
                //페이지 자동 설정 부분(페이지 크기를 자동으로 맞출것인가)
                info.UseSmartPrint = true;

                info.Centering = Centering.Horizontal;

                //출력전 미리보기 하겠다고 하고
                info.Preview = pPreView;
                pfpSpread.Sheets[pSheet].PrintInfo = info;

                //출력하기
                pfpSpread.PrintSheet(0);
            }
            catch (Exception pException)
            {
                throw new ArgumentException("스프레드 미리보기 에러 : " + pException.Message);
            }
        }

        #endregion

        #region ○ 스프레드 맞춤

        /// <summary>
        /// 스프레드 맞춤(세로)
        /// </summary>
        private static CellVerticalAlignment ToCellVerticalAlignment(GridVerticalAlignment cellAlignment)
        {
            switch (cellAlignment)
            {
                case GridVerticalAlignment.Top:
                    return CellVerticalAlignment.Top;
                case GridVerticalAlignment.Middle:
                    return CellVerticalAlignment.Center;
                case GridVerticalAlignment.Bottom:
                    return CellVerticalAlignment.Bottom;
                default:
                    return CellVerticalAlignment.General;
            }
        }

        /// <summary>
        /// 스프레드 맞춤(가로)
        /// </summary>
        private static CellHorizontalAlignment ToCellHorizontalAlignment(GridHorizontalAlignment cellAlignment)
        {
            switch (cellAlignment)
            {
                case GridHorizontalAlignment.Left:
                    return CellHorizontalAlignment.Left;
                case GridHorizontalAlignment.Center:
                    return CellHorizontalAlignment.Center;
                case GridHorizontalAlignment.Right:
                    return CellHorizontalAlignment.Right;
                default:
                    return CellHorizontalAlignment.General;
            }
        }

        #endregion

        /// <summary>
        /// DATATABLE 스프레드 바인딩
        /// </summary>
        /// <param name="fp">목적스프레드</param>
        /// <param name="dt">바인딩할테이블</param>
        public static void SpreadDataBind(FpSpread fp, DataTable dt)
        {


            fp.Sheets[0].Rows.Count = 0;
            fp.Sheets[0].Visible = false;
            fp.Sheets[0].Rows.Count = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (Column col in fp.Sheets[0].Columns)
                {
                    if (col.Tag != null && !string.IsNullOrEmpty(col.Tag.ToString()) && dt.Columns.Contains(col.Tag.ToString()))
                    {
                        if (fp.Sheets[0].Columns[col.Tag.ToString()].CellType.GetType() == typeof(TextCellType))
                        {

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                fp.Sheets[0].SetText(i, col.Tag.ToString(), dt.Rows[i][col.Tag.ToString()].ToString());
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                fp.Sheets[0].SetValue(i, col.Tag.ToString(), dt.Rows[i][col.Tag.ToString()].ToString());
                            }
                        }

                    }
                }
            }

            fp.Sheets[0].Visible = true;
        }

        public static void SpreadDataBind(FpSpread fp, DataTable dt, bool isAppend)
        {
            fp.Sheets[0].Visible = false;
            if (isAppend)
            {
                int count = fp.Sheets[0].Rows.Count;
                fp.Sheets[0].Rows.Count = dt.Rows.Count + count;
                if (dt.Rows.Count > 0)
                {
                    foreach (Column col in fp.Sheets[0].Columns)
                    {
                        if (col.Tag != null && !string.IsNullOrEmpty(col.Tag.ToString()) && dt.Columns.Contains(col.Tag.ToString()))
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                fp.Sheets[0].SetValue(i + count, col.Tag.ToString(), dt.Rows[i][col.Tag.ToString()].ToString());
                            }
                        }
                    }
                }
            }
            else
            {
                fp.Sheets[0].Rows.Count = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    foreach (Column col in fp.Sheets[0].Columns)
                    {
                        if (col.Tag != null && !string.IsNullOrEmpty(col.Tag.ToString()) && dt.Columns.Contains(col.Tag.ToString()))
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                fp.Sheets[0].SetValue(i, col.Tag.ToString(), dt.Rows[i][col.Tag.ToString()].ToString());
                            }
                        }
                    }
                }
            }

            fp.Sheets[0].Visible = true;
        }
        /// <summary>
        /// 각 시트별로 데이터 바인드
        /// </summary>
        /// <param name="fp">목적스프레드</param>
        /// <param name="iSheetnumber">시트번호</param>
        /// <param name="dt">바인딩테이블</param>
        public static void SpreadDataBind(FpSpread fp, int iSheetnumber, DataTable dt)
        {


            fp.Sheets[iSheetnumber].Rows.Count = 0;
            fp.Sheets[iSheetnumber].Visible = false;
            fp.Sheets[iSheetnumber].Rows.Count = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                foreach (Column col in fp.Sheets[iSheetnumber].Columns)
                {
                    if (col.Tag != null && !string.IsNullOrEmpty(col.Tag.ToString()) && dt.Columns.Contains(col.Tag.ToString()))
                    {
                        if (fp.Sheets[iSheetnumber].Columns[col.Tag.ToString()].CellType.GetType() == typeof(TextCellType))
                        {

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                fp.Sheets[iSheetnumber].SetText(i, col.Tag.ToString(), dt.Rows[i][col.Tag.ToString()].ToString());
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                fp.Sheets[iSheetnumber].SetValue(i, col.Tag.ToString(), dt.Rows[i][col.Tag.ToString()].ToString());
                            }
                        }

                    }
                }
            }

            fp.Sheets[iSheetnumber].Visible = true;
        }

        private static void pfpSpread_CellClick(object obj, CellClickEventArgs e)
        {
            try
            {
                if (e.ColumnHeader)
                {
                    xFpSpread pfpSpread = obj as xFpSpread;

                    if (pfpSpread != null && pfpSpread.Sheets[0].Columns[e.Column].CellType != null)
                    {
                        if (pfpSpread.Sheets[0].Columns[e.Column].CellType.ToString() == "CheckBoxCellType")
                        {
                            CheckBoxCell_Yn yn = pfpSpread.checkBoxCell_YNs.Find(x=>x.Cell_Name == pfpSpread.Sheets[0].ColumnHeader.Columns[e.Column].Label);

                            if (yn == null)
                            {
                                for (int i = 0; i < pfpSpread.Sheets[0].Rows.Count; i++)
                                {
                                    pfpSpread.Sheets[0].SetValue(i, e.Column, true);
                                    pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, i, e.Column));
                                }

                                yn = new CheckBoxCell_Yn();
                                yn.Cell_Name = pfpSpread.Sheets[0].ColumnHeader.Columns[e.Column].Label;
                                yn.CheckBox_Yn = true;
                                pfpSpread.checkBoxCell_YNs.Add(yn);
                            }
                            else
                            {
                                if (yn.CheckBox_Yn == true)
                                {
                                    for (int i = 0; i < pfpSpread.Sheets[0].Rows.Count; i++)
                                    {

                                        if (!pfpSpread.Sheets[0].Cells[i, e.Column].Locked)
                                        {
                                            pfpSpread.Sheets[0].SetValue(i, e.Column, false);
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, i, e.Column));
                                        }
                                    }

                                    yn.CheckBox_Yn = false;
                                }
                                else
                                {
                                    for (int i = 0; i < pfpSpread.Sheets[0].Rows.Count; i++)
                                    {
                                        if (!pfpSpread.Sheets[0].Cells[i, e.Column].Locked)
                                        {
                                            pfpSpread.Sheets[0].SetValue(i, e.Column, true);
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, i, e.Column));
                                        }
                                    }
                                    yn.CheckBox_Yn = true;
                                }
                            }

                        }


                    }

                }
                else
                {
                    xFpSpread pfpSpread = obj as xFpSpread;

                    for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                    {
                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString() == "COMPLETE_YN")
                        {
                            if (pfpSpread.Sheets[0].GetValue(e.Row, i) == null)
                            {
                                return;

                            }
                            if (pfpSpread.Sheets[0].GetValue(e.Row, i).ToString() != "N")
                            {
                                return;
                            }
                        }
                    }

                    if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(MYComboBoxCellType))
                    {
                        MYComboBoxCellType mYButtonCellType = pfpSpread.Sheets[0].Columns[e.Column].CellType as MYComboBoxCellType;
              
                        if (mYButtonCellType.pCodeType == "거래처")
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox(mYButtonCellType.pCodeType, "", "");

                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox._pDataTable = pDataTable;
                            basePopupBox.Name = "BaseCompanyPopupBox";
                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                pfpSpread.Sheets[0].SetValue(e.Row, e.Column, basePopupBox._CD);
                                pfpSpread.Sheets[0].SetText(e.Row, e.Column, basePopupBox._CD_NAME);

                            }
                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                        }
                        else if (mYButtonCellType.pCodeType == "BOM")
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox(mYButtonCellType.pCodeType, "", "");

                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox._pDataTable = pDataTable;
                            basePopupBox.Name = "BaseBOMPopupBox";
                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                pfpSpread.Sheets[0].SetValue(e.Row, e.Column, basePopupBox._CD);
                                pfpSpread.Sheets[0].SetText(e.Row, e.Column, basePopupBox._CD_NAME);

                            }
                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                        }
                        else if (mYButtonCellType.pCodeType.Contains("제품_"))
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox("제품_", "", "");
                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox.Name = "BaseProductPopupBox";
                            basePopupBox._pDataTable = pDataTable;

                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                foreach (DataColumn item in basePopupBox._pdataRow.Table.Columns)
                                {

                                    for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                                    {
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString() == item.ColumnName)
                                        {
                                            if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(MYComboBoxCellType))
                                            {
                                                MYComboBoxCellType pCellType = pfpSpread.Sheets[0].Columns[item.ColumnName].CellType as MYComboBoxCellType;


                                                for (int x = 0; x < pCellType.ItemData.Length; x++)
                                                {
                                                    if (pCellType.ItemData[x] == basePopupBox._pdataRow["CD"].ToString())
                                                    {
                                                        pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, pCellType.Items[x]);
                                                        pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, pCellType.ItemData[x]);

                                                    }
                                                }

                                            }
                                            else if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(NumberCellType))
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                            }
                                            else
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                            }
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }


                                    }
                                    //}
                                }


                            }
                        }
                        else if (mYButtonCellType.pCodeType.Contains("자재_"))
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox("자재_", "", "");
                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox.Name = "BaseMaterialPopupBox";
                            basePopupBox._pDataTable = pDataTable;
                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                foreach (DataColumn item in basePopupBox._pdataRow.Table.Columns)
                                {

                                    for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                                    {
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString() == item.ColumnName)
                                        {
                                            if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(MYComboBoxCellType))
                                            {
                                                MYComboBoxCellType pCellType = pfpSpread.Sheets[0].Columns[item.ColumnName].CellType as MYComboBoxCellType;


                                                for (int x = 0; x < pCellType.ItemData.Length; x++)
                                                {
                                                    if (pCellType.ItemData[x] == basePopupBox._pdataRow["CD"].ToString())
                                                    {
                                                        pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, pCellType.Items[x]);
                                                        pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, pCellType.ItemData[x]);
                                                    }
                                                }

                                            }
                                            else if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(NumberCellType))
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                            }
                                            else
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                            }
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }


                                    }
                                    //}
                                }


                            }
                        }
                        else if (mYButtonCellType.pCodeType.Contains("설비"))
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox("설비", "", "");

                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox.Name = "BaseEquipmentPopupBox";
                            basePopupBox._pDataTable = pDataTable;

                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                foreach (DataColumn item in basePopupBox._pdataRow.Table.Columns)
                                {

                                    for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                                    {
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString() == item.ColumnName)
                                        {
                                            if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(MYComboBoxCellType))
                                            {
                                                MYComboBoxCellType pCellType = pfpSpread.Sheets[0].Columns[item.ColumnName].CellType as MYComboBoxCellType;


                                                for (int x = 0; x < pCellType.ItemData.Length; x++)
                                                {
                                                    if (pCellType.ItemData[x] == basePopupBox._pdataRow["CD"].ToString())
                                                    {
                                                        pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, pCellType.Items[x]);
                                                        pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, pCellType.ItemData[x]);
                                                    }
                                                }

                                            }
                                            else if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(NumberCellType))
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                            }
                                            else
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());

                                            }
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }


                                    }
                                    //}
                                }


                            }
                        }
                        else if (mYButtonCellType.pCodeType.Contains("capacity"))
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox("capacity", "", "");

                            string str = pfpSpread.Sheets[0].GetText(e.Row, "STOCK_MST_OUT_CODE");
                            BasePopupBox basePopupBox = new BasePopupBox(str);
                            basePopupBox.Name = "BaseCapacityPopupBox";
                            basePopupBox._pDataTable = pDataTable;
                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                foreach (DataColumn item in basePopupBox._pdataRow.Table.Columns)
                                {

                                    for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                                    {
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString() == item.ColumnName)
                                        {
                                            if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(MYComboBoxCellType))
                                            {
                                                MYComboBoxCellType pCellType = pfpSpread.Sheets[0].Columns[item.ColumnName].CellType as MYComboBoxCellType;

                                                for (int x = 0; x < pCellType.ItemData.Length; x++)
                                                {
                                                    string cd = "CD";
                                                    if (item.ColumnName.Contains("STOCK_MST"))
                                                    {
                                                        cd = "CD_NM";
                                                    }

                                                    if (pCellType.ItemData[x] == basePopupBox._pdataRow[cd].ToString())
                                                    {
                                                        string str1 = pCellType.Items[x];
                                                        string str2 = pCellType.ItemData[x];
                                                        pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, pCellType.Items[x]);
                                                        pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, pCellType.ItemData[x]);

                                                    }
                                                }



                                            }
                                            else if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(NumberCellType))
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                            }
                                            else
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());

                                            }
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }


                                    }
                                    //}
                                }


                            }
                        }
                        else if (mYButtonCellType.pCodeType.Contains("EQCategory"))
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox("EQCategory", "", "");
                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox.Name = "BaseEQCategoryPopupBox";
                            basePopupBox._pDataTable = pDataTable;
                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                foreach (DataColumn item in basePopupBox._pdataRow.Table.Columns)
                                {

                                    for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                                    {
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString() == item.ColumnName)
                                        {
                                            if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(MYComboBoxCellType))
                                            {
                                                MYComboBoxCellType pCellType = pfpSpread.Sheets[0].Columns[item.ColumnName].CellType as MYComboBoxCellType;

                                                for (int x = 0; x < pCellType.ItemData.Length; x++)
                                                {


                                                    if (pCellType.ItemData[x] == basePopupBox._pdataRow[item.ColumnName].ToString())
                                                    {
                                                        pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, pCellType.Items[x]);
                                                        pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, pCellType.ItemData[x]);

                                                    }
                                                }



                                            }
                                            else if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(NumberCellType))
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                            }
                                            else
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());

                                            }
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }


                                    }
                                    //}
                                }


                            }
                        }
                        else if (mYButtonCellType.pCodeType.Contains("SACategory"))
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox("SACategory", "", "");
                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox.Name = "BaseSACategoryPopupBox";
                            basePopupBox._pDataTable = pDataTable;
                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                foreach (DataColumn item in basePopupBox._pdataRow.Table.Columns)
                                {

                                    for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                                    {
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString() == item.ColumnName)
                                        {
                                            if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(MYComboBoxCellType))
                                            {
                                                MYComboBoxCellType pCellType = pfpSpread.Sheets[0].Columns[item.ColumnName].CellType as MYComboBoxCellType;

                                                for (int x = 0; x < pCellType.ItemData.Length; x++)
                                                {


                                                    if (pCellType.ItemData[x] == basePopupBox._pdataRow[item.ColumnName].ToString())
                                                    {
                                                        pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, pCellType.Items[x]);
                                                        pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, pCellType.ItemData[x]);

                                                    }
                                                }



                                            }
                                            else if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(NumberCellType))
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                            }
                                            else
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());

                                            }
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }


                                    }
                                    //}
                                }


                            }
                        }
                        else if (mYButtonCellType.pCodeType.Contains("PRCategory"))
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox("PRCategory", "", "");
                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox.Name = "BasePRCategoryPopupBox";
                            basePopupBox._pDataTable = pDataTable;
                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                foreach (DataColumn item in basePopupBox._pdataRow.Table.Columns)
                                {

                                    for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                                    {
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString() == item.ColumnName)
                                        {
                                            if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(MYComboBoxCellType))
                                            {
                                                MYComboBoxCellType pCellType = pfpSpread.Sheets[0].Columns[item.ColumnName].CellType as MYComboBoxCellType;

                                                for (int x = 0; x < pCellType.ItemData.Length; x++)
                                                {


                                                    if (pCellType.ItemData[x] == basePopupBox._pdataRow[item.ColumnName].ToString())
                                                    {
                                                        pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, pCellType.Items[x]);
                                                        pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, pCellType.ItemData[x]);

                                                    }
                                                }



                                            }
                                            else if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(NumberCellType))
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                            }
                                            else
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());

                                            }
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }


                                    }
                                    //}
                                }


                            }
                        }
                        else if (mYButtonCellType.pCodeType.Contains("LOTCategory"))
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox("LOTCategory", "", "");
                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox.Name = "BaseLOTCategoryPopupBox";
                            basePopupBox._pDataTable = pDataTable;
                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                foreach (DataColumn item in basePopupBox._pdataRow.Table.Columns)
                                {

                                    for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                                    {
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString() == item.ColumnName)
                                        {
                                            if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(MYComboBoxCellType))
                                            {
                                                MYComboBoxCellType pCellType = pfpSpread.Sheets[0].Columns[item.ColumnName].CellType as MYComboBoxCellType;

                                                for (int x = 0; x < pCellType.ItemData.Length; x++)
                                                {


                                                    if (pCellType.ItemData[x] == basePopupBox._pdataRow[item.ColumnName].ToString())
                                                    {
                                                        pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, pCellType.Items[x]);
                                                        pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, pCellType.ItemData[x]);

                                                    }
                                                }



                                            }
                                            else if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(NumberCellType))
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                            }
                                            else
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());

                                            }
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }


                                    }
                                    //}
                                }


                            }
                        }
                        else if (mYButtonCellType.pCodeType.Contains("RESOURCE"))
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox("RESOURCE", "", "");
                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox.Name = "BaseRESOURCEPopupBox";
                            basePopupBox._pDataTable = pDataTable;
                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                foreach (DataColumn item in basePopupBox._pdataRow.Table.Columns)
                                {

                                    for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                                    {
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString() == item.ColumnName)
                                        {
                                            if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(MYComboBoxCellType))
                                            {
                                                MYComboBoxCellType pCellType = pfpSpread.Sheets[0].Columns[item.ColumnName].CellType as MYComboBoxCellType;

                              
                                                for (int x = 0; x < pCellType.ItemData.Length; x++)
                                                {
                                                    if (pCellType.ItemData[x] == basePopupBox._pdataRow["CD"].ToString())
                                                    {
                                                        pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, pCellType.Items[x]);
                                                        pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, pCellType.ItemData[x]);

                                                    }
                                                }

                                            }
                                            else if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(NumberCellType))
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                            }
                                            else
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                            }
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }


                                    }
                                }


                            }
                        }
                        else if (mYButtonCellType.pCodeType.Contains("소모품"))
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox("소모품", "", "");
                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox.Name = "Base소모품PopupBox";
                            basePopupBox._pDataTable = pDataTable;
                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                foreach (DataColumn item in basePopupBox._pdataRow.Table.Columns)
                                {

                                    for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                                    {
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString() == item.ColumnName)
                                        {
                                            if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(MYComboBoxCellType))
                                            {
                                                MYComboBoxCellType pCellType = pfpSpread.Sheets[0].Columns[item.ColumnName].CellType as MYComboBoxCellType;


                                                for (int x = 0; x < pCellType.ItemData.Length; x++)
                                                {
                                                    if (pCellType.ItemData[x].Trim() == basePopupBox._pdataRow["RESOURCE_NO"].ToString().Trim())
                                                    {
                                                        pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, pCellType.Items[x]);
                                                        pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, pCellType.ItemData[x]);

                                                    }
                                                }

                                            }
                                            else if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(NumberCellType))
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                            }
                                            else
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row,  item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                            }
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }


                                    }
                                }


                            }
                        }
                        else if (mYButtonCellType.pCodeType.Contains("세아_공정"))
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox("세아_공정", "", "");
                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox.Name = "Base세아_공정PopupBox";
                            basePopupBox._pDataTable = pDataTable;
                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                foreach (DataColumn item in basePopupBox._pdataRow.Table.Columns)
                                {

                                    for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                                    {
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString() == item.ColumnName)
                                        {
                                            if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(MYComboBoxCellType))
                                            {
                                                MYComboBoxCellType pCellType = pfpSpread.Sheets[0].Columns[item.ColumnName].CellType as MYComboBoxCellType;


                                                for (int x = 0; x < pCellType.ItemData.Length; x++)
                                                {
                                                    if (pCellType.ItemData[x].Trim() == basePopupBox._pdataRow["CD"].ToString().Trim())
                                                    {
                                                        pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, pCellType.Items[x]);
                                                        pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, pCellType.ItemData[x]);

                                                    }
                                                }

                                            }
                                            else if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(NumberCellType))
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                            }
                                            else
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                            }
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }


                                    }
                                }


                            }
                        }
                        else if (mYButtonCellType.pCodeType.Contains("세아_작업장"))
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox("세아_작업장", "", "");
                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox.Name = "Base세아_작업장PopupBox";
                            basePopupBox._pDataTable = pDataTable;
                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                foreach (DataColumn item in basePopupBox._pdataRow.Table.Columns)
                                {

                                    for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                                    {
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString() == item.ColumnName)
                                        {
                                            if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(MYComboBoxCellType))
                                            {
                                                MYComboBoxCellType pCellType = pfpSpread.Sheets[0].Columns[item.ColumnName].CellType as MYComboBoxCellType;


                                                for (int x = 0; x < pCellType.ItemData.Length; x++)
                                                {
                                                    if (pCellType.ItemData[x].Trim() == basePopupBox._pdataRow["CD"].ToString().Trim())
                                                    {
                                                        pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, pCellType.Items[x]);
                                                        pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, pCellType.ItemData[x]);

                                                    }
                                                }

                                            }
                                            else if (pfpSpread.Sheets[0].Columns[item.ColumnName].CellType.GetType() == typeof(NumberCellType))
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                            }
                                            else
                                            {
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                                pfpSpread.Sheets[0].SetValue(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
                                            }
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }


                                    }
                                }


                            }
                        }
                        else
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox(mYButtonCellType.pCodeType, "", "");

                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox._pDataTable = pDataTable;
                            basePopupBox.Name = "basePopupBox";
                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                pfpSpread.Sheets[0].SetValue(e.Row, e.Column, basePopupBox._CD);
                                pfpSpread.Sheets[0].SetText(e.Row, e.Column, basePopupBox._CD_NAME);

                            }
                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                        }
                    }
                    else if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(ImageCellType))
                    {
                        object str = pfpSpread.Sheets[0].GetValue(e.Row,e.Column);
                        BaseImagePopupBox baseImagePopupBox = new BaseImagePopupBox(pfpSpread.Sheets[0].GetValue(e.Row,e.Column));
                        if (baseImagePopupBox.ShowDialog() == DialogResult.OK)
                        {
                            pfpSpread.Sheets[0].SetValue(e.Row, e.Column, baseImagePopupBox._Image);
                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                        }

                    }

                }

            }
            catch (Exception err)
            {

            }

        }
       
        private static void pfpSpread_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            xFpSpread pfpSpread = sender as xFpSpread;

            //if (pfpSpread.Sheets[0].RowHeader.Cells[pfpSpread.Sheets[0].ActiveRowIndex, 0].Text != "합계")
            //{
            int idx = pfpSpread.Sheets[0].ActiveRowIndex;

            for (int i = 0; i < pfpSpread.ActiveSheet.RowCount; i++)
            {
                //fp.ActiveSheet.Rows[i].BackColor = Color.Transparent;
                //fp.ActiveSheet.Rows[i].ForeColor = Color.Black;
                //fp.ActiveSheet.RowHeader.Cells[i, 0].BackColor = Color.Transparent;
                pfpSpread.ActiveSheet.Rows[i].ForeColor = Color.Black;
                pfpSpread.ActiveSheet.Rows[i].Font = new Font("맑은 고딕", 9, FontStyle.Regular);
            }
            //fp.ActiveSheet.Rows[rowNo].BackColor = Color.Blue;
            //fp.ActiveSheet.Rows[rowNo].ForeColor = Color.White;
            //fp.ActiveSheet.RowHeader.Cells[rowNo, 0].BackColor = Color.DarkBlue;
            pfpSpread.ActiveSheet.Rows[idx].ForeColor = Color.DarkBlue;
            pfpSpread.ActiveSheet.Rows[idx].Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            // pfpSpread.ActiveSheet.SetActiveCell(idx, , false);
        }

    }

    #region ○ 확장 메소드

    /// <summary>
    /// spread 확장 메소드
    /// </summary>
    public static class SpreadExManager
    {
        /// <summary>
        /// Sets the value for the specified cell on this sheet.
        /// </summary>
        public static void SetValue(this SheetView pfpSheet, int pRow, string pColumnTag, object pValue)
        {
            try
            {
                if (pfpSheet.Columns[pColumnTag] != null)
                {
                    if (pfpSheet.Columns[pColumnTag].CellType != null)
                        {

                        if (pfpSheet.Columns[pColumnTag].CellType.GetType() == typeof(ImageCellType))
                        {
                            if (pValue.GetType() == typeof(byte[]))
                            {
                                byte[] bys = pValue as byte[];
                                using (MemoryStream ms = new MemoryStream(bys))
                                {
                                    pfpSheet.SetValue(pRow, pfpSheet.Columns[pColumnTag].Index, Image.FromStream(ms));
                                }
                            }
                        }
                        else if (pfpSheet.Columns[pColumnTag].CellType.GetType() == typeof(CheckBoxCellType))
                        {

                            if (pValue.ToString() == "Y" || pValue.ToString() == "True")
                            {
                                pfpSheet.SetValue(pRow, pfpSheet.Columns[pColumnTag].Index, true);
                            }
                            else
                            {
                                pfpSheet.SetValue(pRow, pfpSheet.Columns[pColumnTag].Index, false);
                            }
                        }
                        else if (pfpSheet.Columns[pColumnTag].CellType.GetType() == typeof(MYNumberCellType))
                        {
                            MYNumberCellType mYNumberCellType = pfpSheet.Columns[pColumnTag].CellType as MYNumberCellType;

                            // mYNumberCellType.pID = pValue.ToString();

                            DataRow[] rows =  mYNumberCellType.pDataTable.Select($"CD = '{pValue}'");
                            if (rows.Length != 0)
                            {
                                pfpSheet.SetValue(pRow, pfpSheet.Columns[pColumnTag].Index, rows[0]["CD_NM"].ToString());
                            }
                            else
                            {
                                pfpSheet.SetValue(pRow, pfpSheet.Columns[pColumnTag].Index, pValue);
                            }
                        }
                        else
                        {
                            pfpSheet.SetValue(pRow, pfpSheet.Columns[pColumnTag].Index, pValue);
                        }
                    }
                    else
                    {
                        string trimmed = pValue.ToString().Trim();

            
                        string result = trimmed.Replace(" ", "");

                        pfpSheet.SetValue(pRow, pfpSheet.Columns[pColumnTag].Index, pValue);

     
                    }

                }
            }
            catch (Exception er)
            {
                //MessageBox.Show(pColumnTag);
            }
        }

        /// <summary>
        /// Sets the Index for the specified cell on this sheet.
        /// </summary>
        public static void SetIndex(this SheetView pfpSheet, int pRow, string pColumnTag, int pIndex)
        {
            ComboBoxCellType cell = null;

            if (pfpSheet.Columns[pColumnTag] != null)
            {
                cell = pfpSheet.Columns[pColumnTag].Editor as FarPoint.Win.Spread.CellType.ComboBoxCellType;
            }
            if (cell != null && cell.ItemData[0] != null && cell.ItemData[0].Length > 0)
            {
                try
                {
                    pfpSheet.SetValue(pRow, pfpSheet.Columns[pColumnTag].Index, cell.ItemData[pIndex] as object);
                }
                catch (IndexOutOfRangeException) { }
            }
        }

        /// <summary>
        /// Sets the Index for the specified cell on this sheet.
        /// </summary>
        public static void SetIndex(this SheetView pfpSheet, int pRow, int pColumn, int pIndex)
        {
            ComboBoxCellType cell = null;

            if (pfpSheet.Columns[pColumn] != null)
            {
                cell = pfpSheet.Columns[pColumn].Editor as FarPoint.Win.Spread.CellType.ComboBoxCellType;
            }
            if (cell != null && cell.ItemData[0] != null && cell.ItemData[0].Length > 0)
            {
                try
                {
                    pfpSheet.SetValue(pRow, pfpSheet.Columns[pColumn].Index, cell.ItemData[pIndex] as object);
                }
                catch (IndexOutOfRangeException) { }

            }
        }

        /// <summary>
        /// Sets the formatted text in the cell on this sheet using the FarPoint.Win.Spread.CellType.IFormatter object for the cell.
        /// </summary>
        public static void SetText(this SheetView pfpSheet, int pRow, string pColumnTag, string pValue)
        {
            if (pfpSheet.Columns[pColumnTag] != null)
            {
                pfpSheet.SetText(pRow, pfpSheet.Columns[pColumnTag].Index, pValue);
            }
        }

        /// <summary>
        /// Gets the formatted text in the cell on this sheet using the FarPoint.Win.Spread.CellType.IFormatter object for the cell.
        /// 2016/06/22  현재 수정중인 값을 못가져오므로 가져오게 루틴 변경 김정호 (문제시 원복)
        /// </summary>
        public static string GetText(this SheetView pfpSheet, int pRow, string pColumnTag)
        {
            string reTurnValue = "";
            if (pfpSheet.Columns[pColumnTag] != null)
            {
                //reTurnValue = pfpSheet.GetText(pRow, pfpSheet.Columns[pColumnTag].Index);
                reTurnValue = pfpSheet.Cells[pRow, pfpSheet.Columns[pColumnTag].Index].Text;
                string pCellType = pfpSheet.Columns[pColumnTag].CellType.ToString();
                switch (pCellType)
                {
                    case "NumberCellType":
                        reTurnValue = (reTurnValue == null) ? "0" : reTurnValue;
                        break;
                    case "DateTimeCellType":
                        reTurnValue = (reTurnValue == null) ? "" : reTurnValue.ToString().Replace("-", "");
                        break;
                    default:
                        reTurnValue = (reTurnValue == null) ? "" : reTurnValue;
                        break;
                }
            }
            return reTurnValue;
        }

        /// <summary>
        /// Gets unformatted data from the specified cell on this sheet.
        /// </summary>
        public static object GetValue(this SheetView pfpSheet, int pRow, string pColumnTag)
        {
            if (pfpSheet.Columns[pColumnTag] != null)
            {
                object reTurnValeu = null;
                //reTurnValeu = pfpSheet.GetValue(pRow, pfpSheet.Columns[pColumnTag].Index);
                reTurnValeu = pfpSheet.Cells[pRow, pfpSheet.Columns[pColumnTag].Index].Value;
                string pCellType = pfpSheet.Columns[pColumnTag].CellType.ToString();
                switch (pCellType)
                {
                    case "NumberCellType":
                        return reTurnValeu == null ? "0" : reTurnValeu;
                    default:
                        return reTurnValeu == null ? "" : reTurnValeu;
                }
            }
            else
            {
                return "";
            }
        }
        public static object GetValue_CK(this SheetView pfpSheet, int pRow, string pColumnTag)
        {
            if (pfpSheet.Columns[pColumnTag] != null)
            {
                object reTurnValeu = null;
                //reTurnValeu = pfpSheet.GetValue(pRow, pfpSheet.Columns[pColumnTag].Index);
                reTurnValeu = pfpSheet.Cells[pRow, pfpSheet.Columns[pColumnTag].Index].Value;
                string pCellType = pfpSheet.Columns[pColumnTag].CellType.ToString();
                switch (pCellType)
                {
                    case "NumberCellType":
                        return reTurnValeu == null ? "0" : reTurnValeu;
                    default:
                        return reTurnValeu == null ? "" : reTurnValeu;
                }
            }
            else
            {
                return "";
            }
        }
        public static void RowLock(this SheetView pfpSheet)
        {
            foreach (Column col in pfpSheet.Columns)
            {
                col.Locked = true;
            }
        }

        public static void SetLock(this SheetView pfpSheet, int pRow, string pColumnTag)
        {
            if (pfpSheet.Columns[pColumnTag] != null)
            {
                pfpSheet.Cells[pRow, pfpSheet.Columns[pColumnTag].Index].Locked = true;
            }
        }

        public static DataTable SpreadToDataTable(this SheetView pfpSheet)
        {
            DataTable dt = new DataTable();

            foreach (Column col in pfpSheet.Columns)
            {
                if (!string.IsNullOrEmpty(col.Tag.ToString()))
                {
                    dt.Columns.Add(col.Tag.ToString());
                }

            }

            foreach (Row row in pfpSheet.Rows)
            {
                DataRow newRow = dt.NewRow();

                foreach (Column col in pfpSheet.Columns)
                {
                    if (!string.IsNullOrEmpty(col.Tag.ToString()))
                    {
                        newRow[col.Tag.ToString()] = pfpSheet.GetValue(row.Index, col.Tag.ToString());
                    }
                }
                dt.Rows.Add(newRow);
            }

            return dt;
        }

        public static string GetText(this Row pRow, string pColumnTag)
        {
            return ((FarPoint.Win.Spread.SheetView)(pRow.Parent.Parent)).GetText(pRow.Index, pColumnTag);
        }

        public static object GetValue(this Row pRow, string pColumnTag)
        {
            return ((FarPoint.Win.Spread.SheetView)(pRow.Parent.Parent)).GetValue(pRow.Index, pColumnTag);
        }

        public static void SetValue(this Row pRow, string pColumnTag, object pObject)
        {
            ((FarPoint.Win.Spread.SheetView)(pRow.Parent.Parent)).SetValue(pRow.Index, pColumnTag, pObject);
        }
    }

    #endregion
}
