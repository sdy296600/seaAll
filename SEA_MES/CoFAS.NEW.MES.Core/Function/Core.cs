using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Popup;
using CoFAS.NEW.MES.Core.Properties;
using DevExpress.XtraEditors;
using FarPoint.Excel;
using FarPoint.Win;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


namespace CoFAS.NEW.MES.Core.Function
{
    public class Core
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
                    DataTable pDataTable = new CoreBusiness().Spread_ComboBox(pCodeType, pCodeName, "");

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
                        //pCellType.EditorValue = EditorValue.ItemData;
                        pCellType.EditorValue = FarPoint.Win.Spread.CellType.EditorValue.ItemData;
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
                //pCellType.MaxLength = int.Parse(pLength);
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
                pCellType.ButtonColor = Color.FromArgb(255, 255, 255); ;
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
            DisplayCheckBoxCellType pCellType = new DisplayCheckBoxCellType();
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
            NEW_ComboBoxCellType pCellType = new NEW_ComboBoxCellType();

            pCellType.pCodeType = pCodeType;

            pCellType.pFirst = pFirst;

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
        //파일
        public static void SpreadColumnAddFile(FpSpread pfpSpread, int pSheet, int pCol, string pHeaderName)
        {
            FileButtonCellType pCellType = new FileButtonCellType();
            pCellType.TwoState = false;
            pCellType.ButtonColor = Color.FromArgb(255, 255, 255);
            pCellType.Text = pHeaderName;
            pCellType.TextAlign = ButtonTextAlign.TextTopPictBottom;
            pfpSpread.Sheets[pSheet].Columns[pCol].CellType = pCellType;
            pfpSpread.Sheets[pSheet].Columns[pCol].Resizable = false;
        }
        //이미지SpreadColumnAddImage SpreadColumnAddFile
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
            catch (Exception)
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
            //int MaxLength = int.Parse(pLength);
            int MaxLength = int.Parse(pLength);
            pCellType.MaxLength = MaxLength;
            pCellType.Multiline = false;
            pCellType.pCodeType = pfpSpread.Sheets[pSheet].Columns[pCol].Label;
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
        public static void SpreadColumnAddTextButtonCellType(FpSpread pfpSpread, int pSheet, int pCol, string pLength, string pCodeType)
        {
            TextButtonCellType pCellType = new TextButtonCellType();
            int MaxLength = int.Parse(pLength);
            pCellType.MaxLength = MaxLength;
            pCellType.Multiline = false;
            pCellType.pCodeType = pCodeType;
            pfpSpread.Sheets[pSheet].Columns[pCol].CellType = pCellType;
            pfpSpread.Sheets[pSheet].Columns[pCol].CellPadding.Left = 3;


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

        #region ○ 스프레드 이벤트 영역
        private static void pfpSpread_SubEditorOpening(object sender, SubEditorOpeningEventArgs e)
        {
            xFpSpread pfpSpread = sender as xFpSpread;


            if (pfpSpread.Sheets[0].Columns[e.Column].CellType == null)
            {
                //e.Cancel = true;
                return;
            }
            if (pfpSpread.Sheets[0].Columns[e.Column].CellType.ToString() == "DateTimeCellType")
            {
                DateTimeCellType celltype = pfpSpread.Sheets[0].Columns[e.Column].CellType as DateTimeCellType;

                if (celltype.UserDefinedFormat != DEFAULT_DATE_FORMAT)
                {
                    e.Cancel = true;
                    return;
                }
                // 서브에디터가 열리지 않도록 합니다.
                //return; 
                //e.Cancel = true;
            }
            // 서브에디터가 열리는 이벤트가 발생한 셀이 숫자 셀이라면
            if (pfpSpread.Sheets[0].Columns[e.Column].CellType.ToString() == "NumberCellType")
            {
                // 서브에디터가 열리지 않도록 합니다.
                e.Cancel = true;
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
                                        if (!pfpSpread.Sheets[0].Columns[col].Locked ||
                                            pfpSpread.Sheets[0].ColumnHeader.Columns[col].ForeColor == Color.FromArgb(82, 60, 216))
                                        // if (fpMain.Sheets[0].ColumnHeader.Columns[c].ForeColor == Color.FromArgb(82, 60, 216))
                                        // pfpSpread.Sheets[0].ColumnHeader.Columns[i].ForeColor
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
                                            if (pfpSpread.Sheets[0].Columns[col].CellType.ToString().Contains("NEW_ComboBoxCellType"))
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

        private static void pfpSpread_CellClick(object obj, CellClickEventArgs e)
        {
            try
            {
                //pfpSpread.ColumnDragMove += new FarPoint.Win.Spread.DragMoveEventHandler(fpSpread1_ColumnDragMove);

                if (e.ColumnHeader)
                {
                    e.Cancel = true;
                    xFpSpread pfpSpread = obj as xFpSpread;
                    //pfpSpread.Sheets[0].AllowColumnMove = false;

                    if (pfpSpread != null && pfpSpread.Sheets[0].Columns[e.Column].CellType != null)
                    {
                        if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(CheckBoxCellType))
                        {
                            CheckBoxCell_Yn yn = pfpSpread.checkBoxCell_YNs.Find(x => x.Cell_Name == pfpSpread.Sheets[0].ColumnHeader.Columns[e.Column].Label);

                            if (yn == null)
                            {
                                for (int i = 0; i < pfpSpread.Sheets[0].Rows.Count; i++)
                                {
                                    if (pfpSpread.Sheets[0].RowHeader.Cells[i, 0].Text != "합계")
                                    {
                                        if (!pfpSpread.Sheets[0].Cells[i, e.Column].Locked)
                                        {
                                            pfpSpread.Sheets[0].SetValue(i, e.Column, true);
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, i, e.Column));
                                        }
                                    }
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
                                        if (pfpSpread.Sheets[0].RowHeader.Cells[i, 0].Text != "합계")
                                        {
                                            if (!pfpSpread.Sheets[0].Cells[i, e.Column].Locked)
                                            {
                                                pfpSpread.Sheets[0].SetValue(i, e.Column, false);

                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, i, e.Column));
                                            }
                                        }
                                    }

                                    yn.CheckBox_Yn = false;
                                }
                                else
                                {
                                    for (int i = 0; i < pfpSpread.Sheets[0].Rows.Count; i++)
                                    {
                                        if (pfpSpread.Sheets[0].RowHeader.Cells[i, 0].Text != "합계")
                                        {
                                            if (!pfpSpread.Sheets[0].Cells[i, e.Column].Locked)
                                            {
                                                pfpSpread.Sheets[0].SetValue(i, e.Column, true);
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, i, e.Column));
                                            }
                                        }
                                    }

                                    yn.CheckBox_Yn = true;
                                }

                            }
                            pfpSpread._EditorNotifyEvent(pfpSpread, new EditorNotifyEventArgs(null, null, 0, 0));
                        }
                        else if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(TextButtonCellType))
                        {
                            TextButtonCellType cellType = pfpSpread.Sheets[0].Columns[e.Column].CellType as TextButtonCellType;

                            if (cellType == null)
                            {
                                return;
                            }

                            if (cellType.pCodeType == "거래처")
                            {
                                DataTable pDataTable = new CoreBusiness().Spread_ComboBox(cellType.pCodeType, "", "");

                                BasePopupBox basePopupBox = new BasePopupBox();
                                basePopupBox._pDataTable = pDataTable;
                                basePopupBox.Name = "BaseCompanyPopupBox";
                                basePopupBox._UserAccount = pfpSpread._user_account;
                                if (basePopupBox.ShowDialog() == DialogResult.OK)
                                {
                                    foreach (DataColumn item in basePopupBox._pdataRow.Table.Columns)
                                    {
                                        for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                                        {
                                            if (pfpSpread.Sheets[0].Columns[i].Label.Contains("거래처") || pfpSpread.Sheets[0].Columns[i].Label.Contains("발주처"))
                                            {
                                                for (int x = 0; x < pfpSpread.Sheets[0].Rows.Count; x++)
                                                {
                                                    if (pfpSpread.Sheets[0].Rows[x].BackColor != Color.FromArgb(198, 239, 206))
                                                    {
                                                        if (pfpSpread.Sheets[0].RowHeader.Cells[x, 0].Text != "합계")
                                                        {
                                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("NAME"))
                                                            {
                                                                pfpSpread.Sheets[0].SetValue(x, i, basePopupBox._pdataRow["CD_NM"].ToString());
                                                                string pHeaderLabel = pfpSpread.Sheets[0].RowHeader.Cells[x, 0].Text;

                                                                if (pHeaderLabel == "")
                                                                {
                                                                    pfpSpread.Sheets[0].RowHeader.Cells[x, 0].Text = "수정";
                                                                }
                                                            }

                                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("ID"))
                                                            {
                                                                pfpSpread.Sheets[0].SetValue(x, i, basePopupBox._pdataRow["CD"].ToString());
                                                                string pHeaderLabel = pfpSpread.Sheets[0].RowHeader.Cells[x, 0].Text;

                                                                if (pHeaderLabel == "")
                                                                {
                                                                    pfpSpread.Sheets[0].RowHeader.Cells[x, 0].Text = "수정";
                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                        }

                                    }
                                    //pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                }
                            }
                            else
                            {
                                DataTable pDataTable = new CoreBusiness().Spread_ComboBox(cellType.pCodeType, cellType.pFirst, "");

                                BasePopupBox basePopupBox = new BasePopupBox();
                                basePopupBox._pDataTable = pDataTable;
                                basePopupBox.Name = "Base_Searchbox";
                                basePopupBox._UserAccount = pfpSpread._user_account;
                                if (basePopupBox.ShowDialog() == DialogResult.OK)
                                {
                                    for (int x = 0; x < pfpSpread.Sheets[0].Rows.Count; x++)
                                    {
                                        if (pfpSpread.Sheets[0].Rows[x].BackColor != Color.FromArgb(198, 239, 206))
                                        {
                                            if (pfpSpread.Sheets[0].RowHeader.Cells[x, 0].Text != "합계")
                                            {
                                                pfpSpread.Sheets[0].SetValue(x, e.Column, basePopupBox._pdataRow["CD"].ToString());
                                                string pHeaderLabel = pfpSpread.Sheets[0].RowHeader.Cells[e.Row, 0].Text;

                                                if (pHeaderLabel == "")
                                                {
                                                    pfpSpread.Sheets[0].RowHeader.Cells[e.Row, 0].Text = "수정";
                                                }

                                            }
                                        }
                                    }
                                    if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(FarPoint.Win.Spread.CellType.NumberCellType))
                                    {
                                        Function.Core._AddItemSUM(pfpSpread);
                                        pfpSpread.ActiveSheet.SetActiveCell(e.Row, e.Column);
                                        pfpSpread.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Center, FarPoint.Win.Spread.HorizontalPosition.Center);
                                    }
                                }
                            }
                        }
                        else if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(NEW_ComboBoxCellType))
                        {
                            NEW_ComboBoxCellType cellType = pfpSpread.Sheets[0].Columns[e.Column].CellType as NEW_ComboBoxCellType;

                            if (cellType == null)
                            {
                                return;
                            }

                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox(cellType.pCodeType, cellType.pFirst, "");

                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox._pDataTable = pDataTable;
                            basePopupBox.Name = "Base_Searchbox";
                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                for (int x = 0; x < pfpSpread.Sheets[0].Rows.Count; x++)
                                {
                                    if (pfpSpread.Sheets[0].Rows[x].BackColor != Color.FromArgb(198, 239, 206))
                                    {
                                        if (pfpSpread.Sheets[0].RowHeader.Cells[x, 0].Text != "합계")
                                        {
                                            pfpSpread.Sheets[0].SetValue(x, e.Column, basePopupBox._pdataRow["CD"].ToString());
                                            string pHeaderLabel = pfpSpread.Sheets[0].RowHeader.Cells[x, 0].Text;

                                            if (pHeaderLabel == "")
                                            {
                                                pfpSpread.Sheets[0].RowHeader.Cells[x, 0].Text = "수정";
                                            }

                                        }
                                    }
                                }
                                if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(FarPoint.Win.Spread.CellType.NumberCellType))
                                {
                                    Function.Core._AddItemSUM(pfpSpread);
                                    pfpSpread.ActiveSheet.SetActiveCell(e.Row, e.Column);
                                    pfpSpread.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Center, FarPoint.Win.Spread.HorizontalPosition.Center);
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


                    if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(ImageCellType))
                    {
                        //FILE_NAME
                        object str = pfpSpread.Sheets[0].GetValue(e.Row, e.Column);



                        BaseImagePopupBox baseImagePopupBox = new BaseImagePopupBox(str);
                        if (baseImagePopupBox.ShowDialog() == DialogResult.OK)
                        {
                            pfpSpread.Sheets[0].SetValue(e.Row, e.Column, baseImagePopupBox._Image);
                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
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
                                                pfpSpread.Sheets[0].SetText(e.Row, item.ColumnName, basePopupBox._pdataRow[item.ColumnName].ToString());
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

                    else if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(TextButtonCellType))
                    {
                        TextButtonCellType cellType = pfpSpread.Sheets[0].Columns[e.Column].CellType as TextButtonCellType;

                        if (cellType == null)
                        {
                            return;
                        }
                        if (pfpSpread.Sheets[0].Rows[e.Row].BackColor == Color.FromArgb(198, 239, 206))
                        {
                            return;
                        }
                        if (cellType.pCodeType.Contains("제품"))
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox("제품_", "", "");
                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox.Name = "BaseProductPopupBox";
                            basePopupBox.pfpSpread = pfpSpread;
                            basePopupBox._pDataTable = pDataTable;

                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                foreach (DataColumn item in basePopupBox._pdataRow.Table.Columns)
                                {
                                    for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                                    {
                                        if (pfpSpread.Sheets[0].Columns[i].Label.Contains("제품"))
                                        {
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("OUT_CODE"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_OUT_CODE"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("NAME"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["CD_NM"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("STANDARD"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_STANDARD"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("TYPE"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_TYPE"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("TYPE2"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_TYPE2"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("TYPE2"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_TYPE2"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("UNIT"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_UNIT"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("PRICE"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_PRICE"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("STOCK_MST_ID"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_ID"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }


                                        }

                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("PROCESS_ID"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["PROCESS_ID"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }
                                    }
                                }


                            }
                        }
                        else if (cellType.pCodeType.Contains("자재"))
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox("자재_", "", "");
                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox.Name = "BaseMaterialPopupBox";
                            basePopupBox._pDataTable = pDataTable;

                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                                {
                                    if (pfpSpread.Sheets[0].Columns[i].Label.Contains("자재"))
                                    {
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("OUT_CODE"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_OUT_CODE"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("NAME"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["CD_NM"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("STANDARD"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_STANDARD"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("TYPE"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_TYPE"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("TYPE2"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_TYPE2"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("TYPE2"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_TYPE2"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("UNIT"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_UNIT"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("PRICE"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_PRICE"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("STOCK_MST_ID"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_ID"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }
                                    }


                                }


                            }
                        }
                        else if (cellType.pCodeType.Contains("파트"))
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox("파트_", "", "");
                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox.Name = "BaseStockPopupBox";
                            basePopupBox._pDataTable = pDataTable;

                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                                {
                                    if (pfpSpread.Sheets[0].Columns[i].Label.Contains("파트"))
                                    {
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("OUT_CODE"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_OUT_CODE"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("NAME"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["CD_NM"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("STANDARD"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_STANDARD"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("TYPE"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_TYPE"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }

                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("TYPE2"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_TYPE2"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("UNIT"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_UNIT"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("PRICE"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_PRICE"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }
                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("STOCK_MST_ID"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_ID"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }
                                    }


                                }


                            }
                        }
                        else if (cellType.pCodeType == "거래처")
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox(cellType.pCodeType, "", "");

                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox._pDataTable = pDataTable;
                            basePopupBox.Name = "BaseCompanyPopupBox";
                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                foreach (DataColumn item in basePopupBox._pdataRow.Table.Columns)
                                {
                                    for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                                    {
                                        if (pfpSpread.Sheets[0].Columns[i].Label.Contains("거래처") || pfpSpread.Sheets[0].Columns[i].Label.Contains("발주처"))
                                        {
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("NAME"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["CD_NM"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }

                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("ID"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["CD"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }
                                        }
                                    }

                                }
                                // pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                            }
                        }
                        else if (cellType.pCodeType == "BOM")
                        {
                            DataTable pDataTable = new CoreBusiness().Spread_ComboBox(cellType.pCodeType, "", "");

                            BasePopupBox basePopupBox = new BasePopupBox();
                            basePopupBox._pDataTable = pDataTable;
                            basePopupBox.Name = "BaseBOMPopupBox";
                            basePopupBox._UserAccount = pfpSpread._user_account;
                            if (basePopupBox.ShowDialog() == DialogResult.OK)
                            {
                                foreach (DataColumn item in basePopupBox._pdataRow.Table.Columns)
                                {
                                    for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                                    {
                                        if (pfpSpread.Sheets[0].Columns[i].Label.Contains("하위"))
                                        {
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("OUT_CODE"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_OUT_CODE"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("NAME"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["CD_NM"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("STANDARD"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_STANDARD"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("TYPE"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_TYPE"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("TYPE2"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_TYPE2"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("TYPE2"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_TYPE2"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("UNIT"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_UNIT"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("PRICE"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_PRICE"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }
                                            if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("STOCK_MST_ID"))
                                            {
                                                pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["STOCK_MST_ID"].ToString());
                                                pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                            }


                                        }

                                        if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("PROCESS_ID"))
                                        {
                                            pfpSpread.Sheets[0].SetValue(e.Row, i, basePopupBox._pdataRow["PROCESS_ID"].ToString());
                                            pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                        }
                                    }
                                }

                            }
                            //pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                        }
                    }

                }

            }
            catch (Exception err)
            {

            }

        }
        private static void fpSpread1_AutoSortedColumn(object sender, AutoSortedColumnEventArgs e)
        {
            xFpSpread pfpSpread = sender as xFpSpread;
            _AddItemSUM(pfpSpread);
        }
        private static void pfpSpread_ButtonClicked(object obj, EditorNotifyEventArgs e)
        {
            try
            {
                xFpSpread pfpSpread = obj as xFpSpread;

                if (e.EditingControl == null)
                {
                    return;
                }
                if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(FileButtonCellType))
                {

                    byte[] str = pfpSpread.Sheets[0].GetValue(e.Row, e.Column) as byte[];
                    string file_name = pfpSpread.Sheets[0].GetValue(e.Row, "FILE_NAME").ToString();
                    BaseFilePopupBox baseFilePopupBox = new BaseFilePopupBox(str, file_name);
                    if (baseFilePopupBox.ShowDialog() == DialogResult.OK)
                    {
                        pfpSpread.Sheets[0].SetValue(e.Row, pfpSpread.Sheets[0].Columns[e.Column].Tag.ToString(), baseFilePopupBox._File);
                        pfpSpread.Sheets[0].SetValue(e.Row, "FILE_NAME", baseFilePopupBox._File_name);
                        pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                    }
                    else
                    {
                        pfpSpread.Sheets[0].SetValue(e.Row, pfpSpread.Sheets[0].Columns[e.Column].Tag.ToString(), str);
                        pfpSpread.Sheets[0].SetValue(e.Row, "FILE_NAME", baseFilePopupBox._File_name);
                    }

                }


                if (e.EditingControl.Text == "거래명세서(국내)출력")
                {
                    if (pfpSpread.Sheets[0].RowHeader.Cells[e.Row, 0].Text == "입력")
                    {
                        CustomMsg.ShowMessage("등록되지않은 출고지시입니다.");
                        return;
                    }
                    string order_mst_id = pfpSpread.Sheets[0].GetValue(e.Row, "ORDER_MST_ID").ToString();
                    string out_wait_mst_id = pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString();
                    DevExpress.XtraSpreadsheet.SpreadsheetControl sc = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                    sc.LoadDocument(Application.StartupPath + $"\\MES_양식_거래명세서(국내).xlsx", DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                    DevExpress.Spreadsheet.Worksheet sheet = sc.Document.Worksheets[0];

                    string str = $@"select 
                               C.ORDER_TYPE        AS 'C.ORDER_TYPE'
							　,C.OUT_CODE         AS 'C.ORDER_MST_CODE'
                              ,C.ORDER_DATE       AS 'C.ORDER_DATE'
                              ,C.NAME             AS 'C.NAME'
                              ,C.OUT_CODE         AS 'C.OUT_CODE'
                              ,C.OUT_CODE         AS 'C.ORDER_USER'
                              ,C.IN_ORDER_USER    AS 'C.IN_ORDER_USER'
							　,B.NAME             AS 'B.NAME'
                              ,A.DEMAND_DATE      AS 'A.DEMAND_DATE'
							  ,D.TYPE2            AS 'D.TYPE2'
							  ,D.OUT_CODE         AS 'D.OUT_CODE'
							  ,D.NAME             AS 'D.NAME'
							  ,D.STANDARD         AS 'D.STANDARD'
							  ,E.code_name        AS 'D.TYPE'
							  ,F.code_name        AS 'D.UNIT'
							  ,G.code_name		  AS 'A.CONVERSION_UNIT'
							  ,A.ORDER_QTY        AS 'A.ORDER_QTY'
							  ,A.ORDER_REMAIN_QTY AS 'A.ORDER_REMAIN_QTY'
							  ,A.STOCK_MST_PRICE  AS 'A.STOCK_MST_PRICE'
							  ,A.COST             AS 'A.COST'
							  ,A.COMMENT          AS 'A.COMMENT'
							  ,A.STOP_YN          AS 'A.STOP_YN'
							  ,H.code_name        AS 'A.OUT_TYPE'
							  ,A.SUPPLY_PRICE_KRW AS 'A.SUPPLY_PRICE_KRW'
							  ,A.FOREIGN_CURRENY  AS 'A.FOREIGN_CURRENY'
							  ,A.PUBLISH_DATE     AS 'A.PUBLISH_DATE'
							  ,A.DEADLINE         AS 'A.DEADLINE'
							  ,A.COMPLETE_YN      AS 'A.COMPLETE_YN'
							  ,A.USE_YN           AS 'A.USE_YN'
                              ,A.REG_DATE         AS 'A.REG_DATE'
							  ,A.REG_USER         AS 'A.REG_USER'
							  ,A.UP_USER          AS 'A.UP_USER'
							  ,A.UP_DATE          AS 'A.UP_DATE'
                              ,A.EXPORT_ID        AS 'A.EXPORT_ID'
                              ,J.OUT_QTY          AS 'J.OUT_QTY'
                              from [dbo].[ORDER_DETAIL] A
                              INNER JOIN [dbo].[COMPANY] B ON A.COMPANY_ID = B.ID
                              INNER JOIN [dbo].[ORDER_MST] C ON A.ORDER_MST_ID = C.ID
                              INNER JOIN [dbo].[STOCK_MST] D ON A.STOCK_MST_ID = D.ID
							  INNER JOIN [dbo].[Code_Mst] E ON D.TYPE = E.code
							  and E.code_type = 'SD04'
							  INNER JOIN [dbo].[Code_Mst] F ON D.UNIT = F.code
							  and F.code_type = 'CD04'
							  INNER JOIN [dbo].[Code_Mst] G ON A.CONVERSION_UNIT = G.code
							  and G.code_type = 'CD04'
							  INNER JOIN [dbo].[Code_Mst] H ON A.OUT_TYPE = H.code
							  and H.code_type = 'CD20'
							  INNER JOIN [dbo].[Code_Mst] I ON C.ORDER_TYPE = I.code
							  and I.code_type = 'SD09'
							  INNER JOIN [dbo].[OUT_STOCK_WAIT_DETAIL] J ON A.ID = J.ORDER_DETAIL_ID
							  and J.OUT_QTY != 0
                              WHERE 1=1
                              AND A.USE_YN = 'Y'
							  AND C.ID = {order_mst_id}
                              AND J.OUT_STOCK_WAIT_MST_ID = {out_wait_mst_id}";

                    string where = @"AND A.OUT_TYPE != 'CD20001'";
                    StringBuilder sb = new StringBuilder();
                    //Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                    where += sb.ToString();

                    string sql = str + where;
                    DataTable _DataTable = new CoreBusiness().SELECT(sql);
                    if (_DataTable.Rows.Count < 1)
                    {
                        CustomMsg.ShowMessage("출고예정인 제품이 없습니다.");
                        return;
                    }
                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        sheet.Cells[14, 10].SetValueFromText(_DataTable.Rows[i]["B.NAME"].ToString()); //고객사
                        sheet.Cells[14, 11].SetValueFromText(_DataTable.Rows[i]["C.ORDER_MST_CODE"].ToString()); //발주번호
                        sheet.Cells[14, 12].SetValueFromText(_DataTable.Rows[i]["C.NAME"].ToString()); //프로젝트명
                        sheet.Cells[14, 13].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "OUT_STOCK_DATE").ToString()); //완료일
                        sheet.Cells[14, 14].SetValueFromText(_DataTable.Rows[i]["C.ORDER_USER"].ToString()); //받는담당자
                        sheet.Cells[14, 15].SetValueFromText(_DataTable.Rows[i]["C.IN_ORDER_USER"].ToString()); //내부담당자

                        sheet.Cells[i + 17, 10].SetValueFromText(_DataTable.Rows[i]["D.OUT_CODE"].ToString()); //품목번호
                        sheet.Cells[i + 17, 11].SetValueFromText(_DataTable.Rows[i]["D.NAME"].ToString()); //품목
                        sheet.Cells[i + 17, 12].SetValueFromText(_DataTable.Rows[i]["D.STANDARD"].ToString()); //사이즈
                        sheet.Cells[i + 17, 13].SetValueFromText(_DataTable.Rows[i]["D.UNIT"].ToString()); //단위
                        sheet.Cells[i + 17, 14].SetValueFromText(_DataTable.Rows[i]["J.OUT_QTY"].ToString()); //수량
                        sheet.Cells[i + 17, 15].SetValueFromText(_DataTable.Rows[i]["A.STOCK_MST_PRICE"].ToString()); //단가
                    }

                    using (DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem())
                    {
                        using (DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(printingSystem))
                        {
                            DevExpress.Spreadsheet.IWorkbook wb = null;
                            wb = sc.Document;
                            link.Component = wb;
                            link.CreateDocument();
                            link.ShowPreviewDialog();
                        }
                    }

                }

                else if (e.EditingControl.Text == "거래명세서(해외)출력")
                {
                    if (pfpSpread.Sheets[0].RowHeader.Cells[e.Row, 0].Text == "입력")
                    {
                        CustomMsg.ShowMessage("등록되지않은 출고지시입니다.");
                        return;
                    }
                    string order_mst_id = pfpSpread.Sheets[0].GetValue(e.Row, "ORDER_MST_ID").ToString();
                    string out_wait_mst_id = pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString();
                    DevExpress.XtraSpreadsheet.SpreadsheetControl sc = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                    sc.LoadDocument(Application.StartupPath + $"\\MES_양식_Profoma_Invoice_해외.xlsx", DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                    DevExpress.Spreadsheet.Worksheet sheet = sc.Document.Worksheets[0];

                    string str = $@"select 
                               C.ORDER_TYPE        AS 'C.ORDER_TYPE'
							　,C.OUT_CODE         AS 'C.ORDER_MST_CODE'
                              ,C.ORDER_DATE       AS 'C.ORDER_DATE'
                              ,C.NAME             AS 'C.NAME'
                              ,C.OUT_CODE         AS 'C.OUT_CODE'
							　,B.NAME             AS 'B.NAME'
                              ,A.DEMAND_DATE      AS 'A.DEMAND_DATE'
							  ,D.TYPE2            AS 'D.TYPE2'
							  ,D.OUT_CODE         AS 'D.OUT_CODE'
							  ,D.NAME             AS 'D.NAME'
							  ,D.STANDARD         AS 'D.STANDARD'
							  ,E.code_name        AS 'D.TYPE'
							  ,F.code_name        AS 'D.UNIT'
							  ,G.code_name		  AS 'A.CONVERSION_UNIT'
							  ,A.ORDER_QTY        AS 'A.ORDER_QTY'
							  ,A.ORDER_REMAIN_QTY AS 'A.ORDER_REMAIN_QTY'
							  ,A.STOCK_MST_PRICE  AS 'A.STOCK_MST_PRICE'
							  ,A.COST             AS 'A.COST'
							  ,A.COMMENT          AS 'A.COMMENT'
							  ,A.STOP_YN          AS 'A.STOP_YN'
							  ,H.code_name        AS 'A.OUT_TYPE'
							  ,A.SUPPLY_PRICE_KRW AS 'A.SUPPLY_PRICE_KRW'
							  ,A.FOREIGN_CURRENY  AS 'A.FOREIGN_CURRENY'
							  ,A.PUBLISH_DATE     AS 'A.PUBLISH_DATE'
							  ,A.DEADLINE         AS 'A.DEADLINE'
							  ,A.COMPLETE_YN      AS 'A.COMPLETE_YN'
							  ,A.USE_YN           AS 'A.USE_YN'
                              ,A.REG_DATE         AS 'A.REG_DATE'
							  ,A.REG_USER         AS 'A.REG_USER'
							  ,A.UP_USER          AS 'A.UP_USER'
							  ,A.UP_DATE          AS 'A.UP_DATE'
                              ,A.EXPORT_ID        AS 'A.EXPORT_ID'
                              ,J.OUT_QTY          AS 'J.OUT_QTY'
                              from [dbo].[ORDER_DETAIL] A
                              INNER JOIN [dbo].[COMPANY] B ON A.COMPANY_ID = B.ID
                              INNER JOIN [dbo].[ORDER_MST] C ON A.ORDER_MST_ID = C.ID
                              INNER JOIN [dbo].[STOCK_MST] D ON A.STOCK_MST_ID = D.ID
							  INNER JOIN [dbo].[Code_Mst] E ON D.TYPE = E.code
							  and E.code_type = 'SD04'
							  INNER JOIN [dbo].[Code_Mst] F ON D.UNIT = F.code
							  and F.code_type = 'CD04'
							  INNER JOIN [dbo].[Code_Mst] G ON A.CONVERSION_UNIT = G.code
							  and G.code_type = 'CD04'
							  INNER JOIN [dbo].[Code_Mst] H ON A.OUT_TYPE = H.code
							  and H.code_type = 'CD20'
							  INNER JOIN [dbo].[Code_Mst] I ON C.ORDER_TYPE = I.code
							  and I.code_type = 'SD09'
							  INNER JOIN [dbo].[OUT_STOCK_WAIT_DETAIL] J ON A.ID = J.ORDER_DETAIL_ID
							  and J.OUT_QTY != 0
                              WHERE 1=1
                              AND A.USE_YN = 'Y'
							  AND C.ID = {order_mst_id}
                              AND J.OUT_STOCK_WAIT_MST_ID = {out_wait_mst_id}";

                    string where = @"AND A.OUT_TYPE != 'CD20001'";
                    StringBuilder sb = new StringBuilder();
                    //Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                    where += sb.ToString();

                    string sql = str + where;
                    DataTable _DataTable = new CoreBusiness().SELECT(sql);
                    if (_DataTable.Rows.Count < 1)
                    {
                        CustomMsg.ShowMessage("출고예정인 제품이 없습니다.");
                        return;
                    }
                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        sheet.Cells[11, 17].SetValueFromText(_DataTable.Rows[i]["C.ORDER_MST_CODE"].ToString()); //발주번호
                        sheet.Cells[11, 18].SetValueFromText(_DataTable.Rows[i]["C.NAME"].ToString()); //프로젝트명

                        sheet.Cells[i + 14, 16].SetValueFromText(_DataTable.Rows[i]["D.OUT_CODE"].ToString()); //품목번호
                        sheet.Cells[i + 14, 17].SetValueFromText(_DataTable.Rows[i]["D.NAME"].ToString()); //품목
                        sheet.Cells[i + 14, 18].SetValueFromText(_DataTable.Rows[i]["D.STANDARD"].ToString()); //사이즈
                        sheet.Cells[i + 14, 19].SetValueFromText(_DataTable.Rows[i]["D.UNIT"].ToString()); //단위
                        sheet.Cells[i + 14, 20].SetValueFromText(_DataTable.Rows[i]["J.OUT_QTY"].ToString()); //수량
                        sheet.Cells[i + 14, 21].SetValueFromText(_DataTable.Rows[i]["A.STOCK_MST_PRICE"].ToString()); //단가
                    }

                    using (DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem())
                    {
                        using (DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(printingSystem))
                        {
                            DevExpress.Spreadsheet.IWorkbook wb = null;
                            wb = sc.Document;
                            link.Component = wb;
                            link.CreateDocument();
                            link.ShowPreviewDialog();
                        }
                    }

                }

                else if (e.EditingControl.Text == "PACKING LIST출력")
                {
                    if (pfpSpread.Sheets[0].RowHeader.Cells[e.Row, 0].Text == "입력")
                    {
                        CustomMsg.ShowMessage("등록되지않은 출고지시입니다.");
                        return;
                    }
                    string order_mst_id = pfpSpread.Sheets[0].GetValue(e.Row, "ORDER_MST_ID").ToString();
                    string out_wait_mst_id = pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString();
                    DevExpress.XtraSpreadsheet.SpreadsheetControl sc = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                    sc.LoadDocument(Application.StartupPath + $"\\MES_양식_PACKING LIST.xlsx", DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                    DevExpress.Spreadsheet.Worksheet sheet = sc.Document.Worksheets[0];

                    if (pfpSpread._menu_name == "견본출고지시" || pfpSpread._menu_name == "견본출고등록")
                    {
                        string str = $@"select 
                               C.ORDER_TYPE        AS 'C.ORDER_TYPE'
							　,C.OUT_CODE         AS 'C.ORDER_MST_CODE'
                              ,C.ORDER_DATE       AS 'C.ORDER_DATE'
                              ,C.NAME             AS 'C.NAME'
                              ,C.OUT_CODE         AS 'C.OUT_CODE'
                              ,A.DEMAND_DATE      AS 'A.DEMAND_DATE'
							  ,D.TYPE2            AS 'D.TYPE2'
							  ,D.OUT_CODE         AS 'D.OUT_CODE'
							  ,D.NAME             AS 'D.NAME'
							  ,D.STANDARD         AS 'D.STANDARD'
							  ,E.code_name        AS 'D.TYPE'
							  ,F.code_name        AS 'D.UNIT'
							  ,G.code_name		  AS 'A.CONVERSION_UNIT'
							  ,A.ORDER_QTY        AS 'A.ORDER_QTY'
							  ,A.ORDER_REMAIN_QTY AS 'A.ORDER_REMAIN_QTY'
							  ,A.STOCK_MST_PRICE  AS 'A.STOCK_MST_PRICE'
							  ,A.COST             AS 'A.COST'
							  ,A.COMMENT          AS 'A.COMMENT'
							  ,A.STOP_YN          AS 'A.STOP_YN'
							  ,H.code_name        AS 'A.OUT_TYPE'
							  ,A.SUPPLY_PRICE_KRW AS 'A.SUPPLY_PRICE_KRW'
							  ,A.FOREIGN_CURRENY  AS 'A.FOREIGN_CURRENY'
							  ,A.PUBLISH_DATE     AS 'A.PUBLISH_DATE'
							  ,A.DEADLINE         AS 'A.DEADLINE'
							  ,A.COMPLETE_YN      AS 'A.COMPLETE_YN'
							  ,A.USE_YN           AS 'A.USE_YN'
                              ,A.REG_DATE         AS 'A.REG_DATE'
							  ,A.REG_USER         AS 'A.REG_USER'
							  ,A.UP_USER          AS 'A.UP_USER'
							  ,A.UP_DATE          AS 'A.UP_DATE'
                              ,A.EXPORT_ID        AS 'A.EXPORT_ID'
                              ,J.OUT_QTY          AS 'J.OUT_QTY'
                              from [dbo].[ORDER_DETAIL] A
                              INNER JOIN [dbo].[ORDER_MST] C ON A.ORDER_MST_ID = C.ID
                              INNER JOIN [dbo].[STOCK_MST] D ON A.STOCK_MST_ID = D.ID
							  INNER JOIN [dbo].[Code_Mst] E ON D.TYPE = E.code
							  and E.code_type = 'SD04'
							  INNER JOIN [dbo].[Code_Mst] F ON D.UNIT = F.code
							  and F.code_type = 'CD04'
							  INNER JOIN [dbo].[Code_Mst] G ON A.CONVERSION_UNIT = G.code
							  and G.code_type = 'CD04'
							  INNER JOIN [dbo].[Code_Mst] H ON A.OUT_TYPE = H.code
							  and H.code_type = 'CD20'
							  INNER JOIN [dbo].[Code_Mst] I ON C.ORDER_TYPE = I.code
							  and I.code_type = 'SD27'
							  INNER JOIN [dbo].[OUT_STOCK_WAIT_DETAIL] J ON A.ID = J.ORDER_DETAIL_ID
							  and J.OUT_QTY != 0
                              WHERE 1=1
                              AND A.USE_YN = 'Y'
							  AND C.ID = {order_mst_id}
                              AND J.OUT_STOCK_WAIT_MST_ID = {out_wait_mst_id}";

                        string where = @"AND A.OUT_TYPE != 'CD20001'";
                        StringBuilder sb = new StringBuilder();
                        //Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                        where += sb.ToString();

                        string sql = str + where;
                        DataTable _DataTable = new CoreBusiness().SELECT(sql);
                        if (_DataTable.Rows.Count < 1)
                        {
                            CustomMsg.ShowMessage("출고예정인 제품이 없습니다.");
                            return;
                        }
                        for (int i = 0; i < _DataTable.Rows.Count; i++)
                        {
                            //sheet.Cells[5, 7].SetValueFromText(_DataTable.Rows[i]["B.NAME"].ToString()); //고객사
                            sheet.Cells[5, 8].SetValueFromText(_DataTable.Rows[i]["C.ORDER_MST_CODE"].ToString()); //발주번호
                            sheet.Cells[5, 9].SetValueFromText(_DataTable.Rows[i]["C.NAME"].ToString()); //프로젝트명

                            sheet.Cells[i + 8, 7].SetValueFromText(_DataTable.Rows[i]["D.OUT_CODE"].ToString()); //품목번호
                            sheet.Cells[i + 8, 8].SetValueFromText(_DataTable.Rows[i]["D.NAME"].ToString()); //품목
                            sheet.Cells[i + 8, 9].SetValueFromText(_DataTable.Rows[i]["D.STANDARD"].ToString()); //사이즈
                            sheet.Cells[i + 8, 10].SetValueFromText(_DataTable.Rows[i]["D.UNIT"].ToString()); //단위
                            sheet.Cells[i + 8, 11].SetValueFromText(_DataTable.Rows[i]["J.OUT_QTY"].ToString()); //수량
                        }

                        using (DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem())
                        {
                            using (DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(printingSystem))
                            {
                                DevExpress.Spreadsheet.IWorkbook wb = null;
                                wb = sc.Document;
                                link.Component = wb;
                                link.CreateDocument();
                                link.ShowPreviewDialog();
                            }
                        }
                    }

                    else
                    {
                        string str = $@"select 
                               C.ORDER_TYPE        AS 'C.ORDER_TYPE'
							　,C.OUT_CODE         AS 'C.ORDER_MST_CODE'
                              ,C.ORDER_DATE       AS 'C.ORDER_DATE'
                              ,C.NAME             AS 'C.NAME'
                              ,C.OUT_CODE         AS 'C.OUT_CODE'
							　,B.NAME             AS 'B.NAME'
                              ,A.DEMAND_DATE      AS 'A.DEMAND_DATE'
							  ,D.TYPE2            AS 'D.TYPE2'
							  ,D.OUT_CODE         AS 'D.OUT_CODE'
							  ,D.NAME             AS 'D.NAME'
							  ,D.STANDARD         AS 'D.STANDARD'
							  ,E.code_name        AS 'D.TYPE'
							  ,F.code_name        AS 'D.UNIT'
							  ,G.code_name		  AS 'A.CONVERSION_UNIT'
							  ,A.ORDER_QTY        AS 'A.ORDER_QTY'
							  ,A.ORDER_REMAIN_QTY AS 'A.ORDER_REMAIN_QTY'
							  ,A.STOCK_MST_PRICE  AS 'A.STOCK_MST_PRICE'
							  ,A.COST             AS 'A.COST'
							  ,A.COMMENT          AS 'A.COMMENT'
							  ,A.STOP_YN          AS 'A.STOP_YN'
							  ,H.code_name        AS 'A.OUT_TYPE'
							  ,A.SUPPLY_PRICE_KRW AS 'A.SUPPLY_PRICE_KRW'
							  ,A.FOREIGN_CURRENY  AS 'A.FOREIGN_CURRENY'
							  ,A.PUBLISH_DATE     AS 'A.PUBLISH_DATE'
							  ,A.DEADLINE         AS 'A.DEADLINE'
							  ,A.COMPLETE_YN      AS 'A.COMPLETE_YN'
							  ,A.USE_YN           AS 'A.USE_YN'
                              ,A.REG_DATE         AS 'A.REG_DATE'
							  ,A.REG_USER         AS 'A.REG_USER'
							  ,A.UP_USER          AS 'A.UP_USER'
							  ,A.UP_DATE          AS 'A.UP_DATE'
                              ,A.EXPORT_ID        AS 'A.EXPORT_ID'
                              ,J.OUT_QTY          AS 'J.OUT_QTY'
                              from [dbo].[ORDER_DETAIL] A
                              INNER JOIN [dbo].[COMPANY] B ON A.COMPANY_ID = B.ID
                              INNER JOIN [dbo].[ORDER_MST] C ON A.ORDER_MST_ID = C.ID
                              INNER JOIN [dbo].[STOCK_MST] D ON A.STOCK_MST_ID = D.ID
							  INNER JOIN [dbo].[Code_Mst] E ON D.TYPE = E.code
							  and E.code_type = 'SD04'
							  INNER JOIN [dbo].[Code_Mst] F ON D.UNIT = F.code
							  and F.code_type = 'CD04'
							  INNER JOIN [dbo].[Code_Mst] G ON A.CONVERSION_UNIT = G.code
							  and G.code_type = 'CD04'
							  INNER JOIN [dbo].[Code_Mst] H ON A.OUT_TYPE = H.code
							  and H.code_type = 'CD20'
							  INNER JOIN [dbo].[Code_Mst] I ON C.ORDER_TYPE = I.code
							  and I.code_type = 'SD09'
							  INNER JOIN [dbo].[OUT_STOCK_WAIT_DETAIL] J ON A.ID = J.ORDER_DETAIL_ID
							  and J.OUT_QTY != 0
                              WHERE 1=1
                              AND A.USE_YN = 'Y'
							  AND C.ID = {order_mst_id}
                              AND J.OUT_STOCK_WAIT_MST_ID = {out_wait_mst_id}";

                        string where = @"AND A.OUT_TYPE != 'CD20001'";
                        StringBuilder sb = new StringBuilder();
                        //Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                        where += sb.ToString();

                        string sql = str + where;
                        DataTable _DataTable = new CoreBusiness().SELECT(sql);
                        if (_DataTable.Rows.Count < 1)
                        {
                            CustomMsg.ShowMessage("출고예정인 제품이 없습니다.");
                            return;
                        }
                        for (int i = 0; i < _DataTable.Rows.Count; i++)
                        {
                            sheet.Cells[5, 7].SetValueFromText(_DataTable.Rows[i]["B.NAME"].ToString()); //고객사
                            sheet.Cells[5, 8].SetValueFromText(_DataTable.Rows[i]["C.ORDER_MST_CODE"].ToString()); //발주번호
                            sheet.Cells[5, 9].SetValueFromText(_DataTable.Rows[i]["C.NAME"].ToString()); //프로젝트명

                            sheet.Cells[i + 8, 7].SetValueFromText(_DataTable.Rows[i]["D.OUT_CODE"].ToString()); //품목번호
                            sheet.Cells[i + 8, 8].SetValueFromText(_DataTable.Rows[i]["D.NAME"].ToString()); //품목
                            sheet.Cells[i + 8, 9].SetValueFromText(_DataTable.Rows[i]["D.STANDARD"].ToString()); //사이즈
                            sheet.Cells[i + 8, 10].SetValueFromText(_DataTable.Rows[i]["D.UNIT"].ToString()); //단위
                            sheet.Cells[i + 8, 11].SetValueFromText(_DataTable.Rows[i]["J.OUT_QTY"].ToString()); //수량
                        }

                        using (DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem())
                        {
                            using (DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(printingSystem))
                            {
                                DevExpress.Spreadsheet.IWorkbook wb = null;
                                wb = sc.Document;
                                link.Component = wb;
                                link.CreateDocument();
                                link.ShowPreviewDialog();
                            }
                        }
                    }


                }

                else if (e.EditingControl.Text == "포장요청서출력")
                {
                    if (pfpSpread.Sheets[0].RowHeader.Cells[e.Row, 0].Text == "입력")
                    {
                        CustomMsg.ShowMessage("등록되지않은 출고지시입니다.");
                        return;
                    }
                    string order_mst_id = pfpSpread.Sheets[0].GetValue(e.Row, "ORDER_MST_ID").ToString();
                    string out_wait_mst_id = pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString();
                    DevExpress.XtraSpreadsheet.SpreadsheetControl sc = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                    sc.LoadDocument(Application.StartupPath + $"\\MES_포장요청서_양식.xlsx", DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                    DevExpress.Spreadsheet.Worksheet sheet = sc.Document.Worksheets[0];

                    if (pfpSpread._menu_name == "견본출고지시" || pfpSpread._menu_name == "견본출고등록")
                    {
                        string str = $@"select 
                               C.ORDER_TYPE        AS 'C.ORDER_TYPE'
							　,C.OUT_CODE         AS 'C.ORDER_MST_CODE'
                              ,C.ORDER_DATE       AS 'C.ORDER_DATE'
                              ,C.NAME             AS 'C.NAME'
                              ,C.OUT_CODE         AS 'C.OUT_CODE'
                              ,A.DEMAND_DATE      AS 'A.DEMAND_DATE'
							  ,D.TYPE2            AS 'D.TYPE2'
							  ,D.OUT_CODE         AS 'D.OUT_CODE'
							  ,D.NAME             AS 'D.NAME'
							  ,D.STANDARD         AS 'D.STANDARD'
							  ,E.code_name        AS 'D.TYPE'
							  ,F.code_name        AS 'D.UNIT'
							  ,G.code_name		  AS 'A.CONVERSION_UNIT'
							  ,A.ORDER_QTY        AS 'A.ORDER_QTY'
							  ,A.ORDER_REMAIN_QTY AS 'A.ORDER_REMAIN_QTY'
							  ,A.STOCK_MST_PRICE  AS 'A.STOCK_MST_PRICE'
							  ,A.COST             AS 'A.COST'
							  ,A.COMMENT          AS 'A.COMMENT'
							  ,A.STOP_YN          AS 'A.STOP_YN'
							  ,H.code_name        AS 'A.OUT_TYPE'
							  ,A.SUPPLY_PRICE_KRW AS 'A.SUPPLY_PRICE_KRW'
							  ,A.FOREIGN_CURRENY  AS 'A.FOREIGN_CURRENY'
							  ,A.PUBLISH_DATE     AS 'A.PUBLISH_DATE'
							  ,A.DEADLINE         AS 'A.DEADLINE'
							  ,A.COMPLETE_YN      AS 'A.COMPLETE_YN'
							  ,A.USE_YN           AS 'A.USE_YN'
                              ,A.REG_DATE         AS 'A.REG_DATE'
							  ,A.REG_USER         AS 'A.REG_USER'
							  ,A.UP_USER          AS 'A.UP_USER'
							  ,A.UP_DATE          AS 'A.UP_DATE'
                              ,A.EXPORT_ID        AS 'A.EXPORT_ID'
                              ,J.OUT_QTY          AS 'J.OUT_QTY'
                              ,K.OUT_CODE         AS 'K.OUT_CODE'
                              from [dbo].[ORDER_DETAIL] A
                              INNER JOIN [dbo].[ORDER_MST] C ON A.ORDER_MST_ID = C.ID
                              INNER JOIN [dbo].[STOCK_MST] D ON A.STOCK_MST_ID = D.ID
							  INNER JOIN [dbo].[Code_Mst] E ON D.TYPE = E.code
							  and E.code_type = 'SD04'
							  INNER JOIN [dbo].[Code_Mst] F ON D.UNIT = F.code
							  and F.code_type = 'CD04'
							  INNER JOIN [dbo].[Code_Mst] G ON A.CONVERSION_UNIT = G.code
							  and G.code_type = 'CD04'
							  INNER JOIN [dbo].[Code_Mst] H ON A.OUT_TYPE = H.code
							  and H.code_type = 'CD20'
							  INNER JOIN [dbo].[Code_Mst] I ON C.ORDER_TYPE = I.code
							  and I.code_type = 'SD27'
                              INNER JOIN [dbo].[OUT_STOCK_WAIT_DETAIL] J ON A.ID = J.ORDER_DETAIL_ID
							  and J.OUT_QTY != 0
							  INNER JOIN [dbo].[IN_STOCK_DETAIL] K ON J.IN_STOCK_DETAIL_ID = K.ID
                              WHERE 1=1
                              AND A.USE_YN = 'Y'
							  AND C.ID = {order_mst_id}
                              AND J.OUT_STOCK_WAIT_MST_ID = {out_wait_mst_id}";

                        string where = @"AND A.OUT_TYPE != 'CD20001'";
                        StringBuilder sb = new StringBuilder();
                        //Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                        where += sb.ToString();

                        string sql = str + where;
                        DataTable _DataTable = new CoreBusiness().SELECT(sql);
                        if (_DataTable.Rows.Count < 1)
                        {
                            CustomMsg.ShowMessage("출고예정인 제품이 없습니다.");
                            return;
                        }
                        for (int i = 0; i < _DataTable.Rows.Count; i++)
                        {
                            //sheet.Cells[11, 11].SetValueFromText(_DataTable.Rows[i]["B.NAME"].ToString()); //고객사
                            sheet.Cells[11, 12].SetValueFromText(_DataTable.Rows[i]["C.ORDER_MST_CODE"].ToString()); //발주번호
                            sheet.Cells[11, 13].SetValueFromText(_DataTable.Rows[i]["C.NAME"].ToString()); //프로젝트명

                            sheet.Cells[i + 11, 14].SetValueFromText(_DataTable.Rows[i]["D.OUT_CODE"].ToString()); //품목번호
                            sheet.Cells[i + 11, 15].SetValueFromText(_DataTable.Rows[i]["D.NAME"].ToString()); //품목
                            sheet.Cells[i + 11, 16].SetValueFromText(_DataTable.Rows[i]["D.STANDARD"].ToString()); //사이즈
                            sheet.Cells[i + 11, 17].SetValueFromText(_DataTable.Rows[i]["D.UNIT"].ToString()); //단위
                            sheet.Cells[i + 11, 18].SetValueFromText(_DataTable.Rows[i]["J.OUT_QTY"].ToString()); //수량
                            sheet.Cells[i + 11, 19].SetValueFromText(_DataTable.Rows[i]["K.OUT_CODE"].ToString()); //LOT
                            sheet.Cells[i + 11, 10].SetValueFromText(_DataTable.Rows[i]["A.DEMAND_DATE"].ToString()); //납기일
                        }

                        using (DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem())
                        {
                            using (DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(printingSystem))
                            {
                                DevExpress.Spreadsheet.IWorkbook wb = null;
                                wb = sc.Document;
                                link.Component = wb;
                                link.CreateDocument();
                                link.ShowPreviewDialog();
                            }
                        }
                    }

                    else
                    {
                        string str = $@"select 
                               C.ORDER_TYPE        AS 'C.ORDER_TYPE'
							　,C.OUT_CODE         AS 'C.ORDER_MST_CODE'
                              ,C.ORDER_DATE       AS 'C.ORDER_DATE'
                              ,C.NAME             AS 'C.NAME'
                              ,C.OUT_CODE         AS 'C.OUT_CODE'
							　,B.NAME             AS 'B.NAME'
                              ,A.DEMAND_DATE      AS 'A.DEMAND_DATE'
							  ,D.TYPE2            AS 'D.TYPE2'
							  ,D.OUT_CODE         AS 'D.OUT_CODE'
							  ,D.NAME             AS 'D.NAME'
							  ,D.STANDARD         AS 'D.STANDARD'
							  ,E.code_name        AS 'D.TYPE'
							  ,F.code_name        AS 'D.UNIT'
							  ,G.code_name		  AS 'A.CONVERSION_UNIT'
							  ,A.ORDER_QTY        AS 'A.ORDER_QTY'
							  ,A.ORDER_REMAIN_QTY AS 'A.ORDER_REMAIN_QTY'
							  ,A.STOCK_MST_PRICE  AS 'A.STOCK_MST_PRICE'
							  ,A.COST             AS 'A.COST'
							  ,A.COMMENT          AS 'A.COMMENT'
							  ,A.STOP_YN          AS 'A.STOP_YN'
							  ,H.code_name        AS 'A.OUT_TYPE'
							  ,A.SUPPLY_PRICE_KRW AS 'A.SUPPLY_PRICE_KRW'
							  ,A.FOREIGN_CURRENY  AS 'A.FOREIGN_CURRENY'
							  ,A.PUBLISH_DATE     AS 'A.PUBLISH_DATE'
							  ,A.DEADLINE         AS 'A.DEADLINE'
							  ,A.COMPLETE_YN      AS 'A.COMPLETE_YN'
							  ,A.USE_YN           AS 'A.USE_YN'
                              ,A.REG_DATE         AS 'A.REG_DATE'
							  ,A.REG_USER         AS 'A.REG_USER'
							  ,A.UP_USER          AS 'A.UP_USER'
							  ,A.UP_DATE          AS 'A.UP_DATE'
                              ,A.EXPORT_ID        AS 'A.EXPORT_ID'
                              ,J.OUT_QTY          AS 'J.OUT_QTY'
                              ,K.OUT_CODE         AS 'K.OUT_CODE'
                              from [dbo].[ORDER_DETAIL] A
                              INNER JOIN [dbo].[COMPANY] B ON A.COMPANY_ID = B.ID
                              INNER JOIN [dbo].[ORDER_MST] C ON A.ORDER_MST_ID = C.ID
                              INNER JOIN [dbo].[STOCK_MST] D ON A.STOCK_MST_ID = D.ID
							  INNER JOIN [dbo].[Code_Mst] E ON D.TYPE = E.code
							  and E.code_type = 'SD04'
							  INNER JOIN [dbo].[Code_Mst] F ON D.UNIT = F.code
							  and F.code_type = 'CD04'
							  INNER JOIN [dbo].[Code_Mst] G ON A.CONVERSION_UNIT = G.code
							  and G.code_type = 'CD04'
							  INNER JOIN [dbo].[Code_Mst] H ON A.OUT_TYPE = H.code
							  and H.code_type = 'CD20'
							  INNER JOIN [dbo].[Code_Mst] I ON C.ORDER_TYPE = I.code
							  and I.code_type = 'SD09'
                              INNER JOIN [dbo].[OUT_STOCK_WAIT_DETAIL] J ON A.ID = J.ORDER_DETAIL_ID
							  and J.OUT_QTY != 0
							  INNER JOIN [dbo].[IN_STOCK_DETAIL] K ON J.IN_STOCK_DETAIL_ID = K.ID
                              WHERE 1=1
                              AND A.USE_YN = 'Y'
							  AND C.ID = {order_mst_id}
                              AND J.OUT_STOCK_WAIT_MST_ID = {out_wait_mst_id}";

                        string where = @"AND A.OUT_TYPE != 'CD20001'";
                        StringBuilder sb = new StringBuilder();
                        //Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                        where += sb.ToString();

                        string sql = str + where;
                        DataTable _DataTable = new CoreBusiness().SELECT(sql);
                        if (_DataTable.Rows.Count < 1)
                        {
                            CustomMsg.ShowMessage("출고예정인 제품이 없습니다.");
                            return;
                        }
                        for (int i = 0; i < _DataTable.Rows.Count; i++)
                        {
                            sheet.Cells[11, 11].SetValueFromText(_DataTable.Rows[i]["B.NAME"].ToString()); //고객사
                            sheet.Cells[11, 12].SetValueFromText(_DataTable.Rows[i]["C.ORDER_MST_CODE"].ToString()); //발주번호
                            sheet.Cells[11, 13].SetValueFromText(_DataTable.Rows[i]["C.NAME"].ToString()); //프로젝트명

                            sheet.Cells[i + 11, 14].SetValueFromText(_DataTable.Rows[i]["D.OUT_CODE"].ToString()); //품목번호
                            sheet.Cells[i + 11, 15].SetValueFromText(_DataTable.Rows[i]["D.NAME"].ToString()); //품목
                            sheet.Cells[i + 11, 16].SetValueFromText(_DataTable.Rows[i]["D.STANDARD"].ToString()); //사이즈
                            sheet.Cells[i + 11, 17].SetValueFromText(_DataTable.Rows[i]["D.UNIT"].ToString()); //단위
                            sheet.Cells[i + 11, 18].SetValueFromText(_DataTable.Rows[i]["J.OUT_QTY"].ToString()); //수량
                            sheet.Cells[i + 11, 19].SetValueFromText(_DataTable.Rows[i]["K.OUT_CODE"].ToString()); //LOT
                            sheet.Cells[i + 11, 10].SetValueFromText(_DataTable.Rows[i]["A.DEMAND_DATE"].ToString()); //납기일
                        }

                        using (DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem())
                        {
                            using (DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(printingSystem))
                            {
                                DevExpress.Spreadsheet.IWorkbook wb = null;
                                wb = sc.Document;
                                link.Component = wb;
                                link.CreateDocument();
                                link.ShowPreviewDialog();
                            }
                        }
                    }
                }

                else if (e.EditingControl.Text == "출하요청서출력")
                {
                    if (pfpSpread.Sheets[0].RowHeader.Cells[e.Row, 0].Text == "입력")
                    {
                        CustomMsg.ShowMessage("등록되지않은 출고지시입니다.");
                        return;
                    }
                    string order_mst_id = pfpSpread.Sheets[0].GetValue(e.Row, "ORDER_MST_ID").ToString();
                    string out_wait_mst_id = pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString();
                    DevExpress.XtraSpreadsheet.SpreadsheetControl sc = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                    sc.LoadDocument(Application.StartupPath + $"\\MES_출하요청서_양식.xlsx", DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                    DevExpress.Spreadsheet.Worksheet sheet = sc.Document.Worksheets[0];

                    if (pfpSpread._menu_name == "견본출고지시" || pfpSpread._menu_name == "견본출고등록")
                    {
                        string str = $@"select 
                               C.ORDER_TYPE        AS 'C.ORDER_TYPE'
							　,C.OUT_CODE         AS 'C.ORDER_MST_CODE'
                              ,C.ORDER_DATE       AS 'C.ORDER_DATE'
                              ,C.NAME             AS 'C.NAME'
                              ,C.OUT_CODE         AS 'C.OUT_CODE'
                              ,A.DEMAND_DATE      AS 'A.DEMAND_DATE'
							  ,D.TYPE2            AS 'D.TYPE2'
							  ,D.OUT_CODE         AS 'D.OUT_CODE'
							  ,D.NAME             AS 'D.NAME'
							  ,D.STANDARD         AS 'D.STANDARD'
							  ,E.code_name        AS 'D.TYPE'
							  ,F.code_name        AS 'D.UNIT'
							  ,G.code_name		  AS 'A.CONVERSION_UNIT'
							  ,A.ORDER_QTY        AS 'A.ORDER_QTY'
							  ,A.ORDER_REMAIN_QTY AS 'A.ORDER_REMAIN_QTY'
							  ,A.STOCK_MST_PRICE  AS 'A.STOCK_MST_PRICE'
							  ,A.COST             AS 'A.COST'
							  ,A.COMMENT          AS 'A.COMMENT'
							  ,A.STOP_YN          AS 'A.STOP_YN'
							  ,H.code_name        AS 'A.OUT_TYPE'
							  ,A.SUPPLY_PRICE_KRW AS 'A.SUPPLY_PRICE_KRW'
							  ,A.FOREIGN_CURRENY  AS 'A.FOREIGN_CURRENY'
							  ,A.PUBLISH_DATE     AS 'A.PUBLISH_DATE'
							  ,A.DEADLINE         AS 'A.DEADLINE'
							  ,A.COMPLETE_YN      AS 'A.COMPLETE_YN'
							  ,A.USE_YN           AS 'A.USE_YN'
                              ,A.REG_DATE         AS 'A.REG_DATE'
							  ,A.REG_USER         AS 'A.REG_USER'
							  ,A.UP_USER          AS 'A.UP_USER'
							  ,A.UP_DATE          AS 'A.UP_DATE'
                              ,A.EXPORT_ID        AS 'A.EXPORT_ID'
                              ,J.OUT_QTY          AS 'J.OUT_QTY'
                              ,K.OUT_CODE         AS 'K.OUT_CODE'
                              from [dbo].[ORDER_DETAIL] A
                              INNER JOIN [dbo].[ORDER_MST] C ON A.ORDER_MST_ID = C.ID
                              INNER JOIN [dbo].[STOCK_MST] D ON A.STOCK_MST_ID = D.ID
							  INNER JOIN [dbo].[Code_Mst] E ON D.TYPE = E.code
							  and E.code_type = 'SD04'
							  INNER JOIN [dbo].[Code_Mst] F ON D.UNIT = F.code
							  and F.code_type = 'CD04'
							  INNER JOIN [dbo].[Code_Mst] G ON A.CONVERSION_UNIT = G.code
							  and G.code_type = 'CD04'
							  INNER JOIN [dbo].[Code_Mst] H ON A.OUT_TYPE = H.code
							  and H.code_type = 'CD20'
							  INNER JOIN [dbo].[Code_Mst] I ON C.ORDER_TYPE = I.code
							  and I.code_type = 'SD27'
                              INNER JOIN [dbo].[OUT_STOCK_WAIT_DETAIL] J ON A.ID = J.ORDER_DETAIL_ID
							  and J.OUT_QTY != 0
							  INNER JOIN [dbo].[IN_STOCK_DETAIL] K ON J.IN_STOCK_DETAIL_ID = K.ID
                              WHERE 1=1
                              AND A.USE_YN = 'Y'
							  AND C.ID = {order_mst_id}";

                        string where = @"AND A.OUT_TYPE != 'CD20001'";
                        StringBuilder sb = new StringBuilder();
                        //Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                        where += sb.ToString();

                        string sql = str + where;
                        DataTable _DataTable = new CoreBusiness().SELECT(sql);
                        if (_DataTable.Rows.Count < 1)
                        {
                            CustomMsg.ShowMessage("출고예정인 제품이 없습니다.");
                            return;
                        }
                        for (int i = 0; i < _DataTable.Rows.Count; i++)
                        {
                            //sheet.Cells[i + 11, 14].SetValueFromText(_DataTable.Rows[i]["B.NAME"].ToString()); //고객사
                            sheet.Cells[i + 11, 12].SetValueFromText(_DataTable.Rows[i]["C.ORDER_MST_CODE"].ToString()); //발주번호
                            sheet.Cells[i + 11, 13].SetValueFromText(_DataTable.Rows[i]["C.NAME"].ToString()); //프로젝트명

                            sheet.Cells[i + 11, 14].SetValueFromText(_DataTable.Rows[i]["D.OUT_CODE"].ToString()); //품목번호
                            sheet.Cells[i + 11, 15].SetValueFromText(_DataTable.Rows[i]["D.NAME"].ToString()); //품목
                            sheet.Cells[i + 11, 16].SetValueFromText(_DataTable.Rows[i]["D.STANDARD"].ToString()); //사이즈
                            sheet.Cells[i + 11, 17].SetValueFromText(_DataTable.Rows[i]["D.UNIT"].ToString()); //단위
                            sheet.Cells[i + 11, 18].SetValueFromText(_DataTable.Rows[i]["J.OUT_QTY"].ToString()); //수량
                            sheet.Cells[i + 11, 19].SetValueFromText(_DataTable.Rows[i]["K.OUT_CODE"].ToString()); //LOT
                            sheet.Cells[i + 11, 10].SetValueFromText(_DataTable.Rows[i]["A.DEMAND_DATE"].ToString()); //납기일
                        }

                        using (DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem())
                        {
                            using (DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(printingSystem))
                            {
                                DevExpress.Spreadsheet.IWorkbook wb = null;
                                wb = sc.Document;
                                link.Component = wb;
                                link.CreateDocument();
                                link.ShowPreviewDialog();
                            }
                        }
                    }

                    else
                    {
                        string str = $@"select 
                               C.ORDER_TYPE             AS 'C.ORDER_TYPE'
							　,C.OUT_CODE               AS 'C.ORDER_MST_CODE'
                              ,C.ORDER_DATE             AS 'C.ORDER_DATE'
                              ,C.NAME                   AS 'C.NAME'
                              ,C.OUT_CODE               AS 'C.OUT_CODE'
							　,B.NAME                   AS 'B.NAME'
                              ,A.DEMAND_DATE            AS 'A.DEMAND_DATE'
							  ,D.TYPE2                  AS 'D.TYPE2'
							  ,D.OUT_CODE               AS 'D.OUT_CODE'
							  ,D.NAME                   AS 'D.NAME'
							  ,D.STANDARD               AS 'D.STANDARD'
							  ,E.code_name              AS 'D.TYPE'
							  ,F.code_name              AS 'D.UNIT'
							  ,G.code_name		        AS 'A.CONVERSION_UNIT'
							  ,A.ORDER_QTY              AS 'A.ORDER_QTY'
							  ,A.ORDER_REMAIN_QTY       AS 'A.ORDER_REMAIN_QTY'
							  ,A.STOCK_MST_PRICE        AS 'A.STOCK_MST_PRICE'
							  ,A.COST                   AS 'A.COST'
							  ,A.COMMENT                AS 'A.COMMENT'
							  ,A.STOP_YN                AS 'A.STOP_YN'
							  ,H.code_name              AS 'A.OUT_TYPE'
							  ,A.SUPPLY_PRICE_KRW       AS 'A.SUPPLY_PRICE_KRW'
							  ,A.FOREIGN_CURRENY        AS 'A.FOREIGN_CURRENY'
							  ,A.PUBLISH_DATE           AS 'A.PUBLISH_DATE'
							  ,A.DEADLINE               AS 'A.DEADLINE'
							  ,A.COMPLETE_YN            AS 'A.COMPLETE_YN'
							  ,A.USE_YN                 AS 'A.USE_YN'
                              ,A.REG_DATE               AS 'A.REG_DATE'
							  ,A.REG_USER               AS 'A.REG_USER'
							  ,A.UP_USER                AS 'A.UP_USER'
							  ,A.UP_DATE                AS 'A.UP_DATE'
                              ,A.EXPORT_ID              AS 'A.EXPORT_ID'
                              ,ISNULL(J.OUT_QTY,0)      AS 'J.OUT_QTY'
                              ,ISNULL(K.OUT_CODE,'')    AS 'K.OUT_CODE'
                              from [dbo].[ORDER_DETAIL] A
                              INNER JOIN [dbo].[COMPANY] B ON A.COMPANY_ID = B.ID
                              INNER JOIN [dbo].[ORDER_MST] C ON A.ORDER_MST_ID = C.ID
                              INNER JOIN [dbo].[STOCK_MST] D ON A.STOCK_MST_ID = D.ID
							  INNER JOIN [dbo].[Code_Mst] E ON D.TYPE = E.code
							  and E.code_type = 'SD04'
							  INNER JOIN [dbo].[Code_Mst] F ON D.UNIT = F.code
							  and F.code_type = 'CD04'
							  INNER JOIN [dbo].[Code_Mst] G ON A.CONVERSION_UNIT = G.code
							  and G.code_type = 'CD04'
							  INNER JOIN [dbo].[Code_Mst] H ON A.OUT_TYPE = H.code
							  and H.code_type = 'CD20'
							  INNER JOIN [dbo].[Code_Mst] I ON C.ORDER_TYPE = I.code
							  and I.code_type = 'SD09'
                              LEFT JOIN [dbo].[OUT_STOCK_WAIT_DETAIL] J ON A.ID = J.ORDER_DETAIL_ID
							  and J.OUT_QTY != 0
							  LEFT JOIN [dbo].[IN_STOCK_DETAIL] K ON J.IN_STOCK_DETAIL_ID = K.ID
                              WHERE 1=1
                              AND A.USE_YN = 'Y'
							  AND C.ID = {order_mst_id}";

                        //string where = @"AND A.OUT_TYPE != 'CD20001'";
                        StringBuilder sb = new StringBuilder();
                        //Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                        //where += sb.ToString();

                        string sql = str /*+ where*/;
                        DataTable _DataTable = new CoreBusiness().SELECT(sql);
                        if (_DataTable.Rows.Count < 1)
                        {
                            CustomMsg.ShowMessage("출고예정인 제품이 없습니다.");
                            return;
                        }
                        for (int i = 0; i < _DataTable.Rows.Count; i++)
                        {
                            sheet.Cells[11, 11].SetValueFromText(_DataTable.Rows[i]["B.NAME"].ToString()); //고객사
                            sheet.Cells[11, 12].SetValueFromText(_DataTable.Rows[i]["C.ORDER_MST_CODE"].ToString()); //발주번호
                            sheet.Cells[11, 13].SetValueFromText(_DataTable.Rows[i]["C.NAME"].ToString()); //프로젝트명

                            sheet.Cells[i + 11, 14].SetValueFromText(_DataTable.Rows[i]["D.OUT_CODE"].ToString()); //품목번호
                            sheet.Cells[i + 11, 15].SetValueFromText(_DataTable.Rows[i]["D.NAME"].ToString()); //품목
                            sheet.Cells[i + 11, 16].SetValueFromText(_DataTable.Rows[i]["D.STANDARD"].ToString()); //사이즈
                            sheet.Cells[i + 11, 17].SetValueFromText(_DataTable.Rows[i]["D.UNIT"].ToString()); //단위
                            //sheet.Cells[i + 11, 18].SetValueFromText(_DataTable.Rows[i]["J.OUT_QTY"].ToString()); //수량 
                            sheet.Cells[i + 11, 18].SetValueFromText(_DataTable.Rows[i]["A.ORDER_QTY"].ToString()); //수량
                            sheet.Cells[i + 11, 19].SetValueFromText(_DataTable.Rows[i]["K.OUT_CODE"].ToString()); //LOT
                            sheet.Cells[i + 11, 10].SetValueFromText(_DataTable.Rows[i]["A.DEMAND_DATE"].ToString()); //납기일
                        }

                        using (DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem())
                        {
                            using (DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(printingSystem))
                            {
                                DevExpress.Spreadsheet.IWorkbook wb = null;
                                wb = sc.Document;
                                link.Component = wb;
                                link.CreateDocument();
                                link.ShowPreviewDialog();
                            }
                        }
                    }
                }

                else if (e.EditingControl.Text == "라벨출력")
                {
                    LabelPrintImagePopup basePopupBox = new LabelPrintImagePopup();
                    if (basePopupBox.ShowDialog() == DialogResult.OK)
                    {
                        //pfpSpread.Sheets[0].SetValue(e.Row, e.Column, baseImagePopupBox._Image);
                        //pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                    }
                }

                else if (e.EditingControl.Text == "출고이력조회")
                {
                    //if (pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString() != "0" &&
                    //    pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString() != "")
                    //{
                    if (pfpSpread != null && pfpSpread.Sheets[0].Columns[e.Column].CellType != null)
                    {
                        if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(ButtonCellType))
                        {

                            if (pfpSpread.Sheets[0].Columns[e.Column].Tag.ToString() == "출고이력조회")
                            {
                                출고이력조회_PopupBox basePopupBox = new 출고이력조회_PopupBox(pfpSpread.Sheets[0].GetValue(e.Row, "A.STOCK_MST_ID").ToString(), pfpSpread._user_account, pfpSpread.Sheets[0].GetValue(e.Row, "A.ORDER_DETAIL_ID").ToString());
                                if (basePopupBox.ShowDialog() == DialogResult.OK)
                                {
                                    //pfpSpread.Sheets[0].SetValue(e.Row, e.Column, baseImagePopupBox._Image);
                                    //pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                }
                            }
                            if (pfpSpread.Sheets[0].Columns[e.Column].Tag.ToString() == "")
                            {

                            }
                        }
                    }
                    //}
                }

                else if (e.EditingControl.Text == "입고이력조회")
                {
                    if (pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString() != "0" &&
                        pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString() != "")
                    {
                        if (pfpSpread != null && pfpSpread.Sheets[0].Columns[e.Column].CellType != null)
                        {
                            if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(ButtonCellType))
                            {

                                if (pfpSpread.Sheets[0].Columns[e.Column].Tag.ToString() == "입고이력조회")
                                {
                                    입고이력조회_PopupBox basePopupBox = new 입고이력조회_PopupBox(pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString(), pfpSpread._user_account);
                                    if (basePopupBox.ShowDialog() == DialogResult.OK)
                                    {
                                        //pfpSpread.Sheets[0].SetValue(e.Row, e.Column, baseImagePopupBox._Image);
                                        //pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                    }
                                }
                                if (pfpSpread.Sheets[0].Columns[e.Column].Tag.ToString() == "")
                                {

                                }
                            }
                        }
                    }
                }

                else if (e.EditingControl.Text == "초중종기준")
                {
                    if (pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString() != "0" &&
                        pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString() != "")
                    {
                        if (pfpSpread != null && pfpSpread.Sheets[0].Columns[e.Column].CellType != null)
                        {
                            if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(ButtonCellType))
                            {

                                if (pfpSpread.Sheets[0].Columns[e.Column].Tag.ToString() == "초중종기준")
                                {
                                    제품초중종기준_PopupBox basePopupBox = new 제품초중종기준_PopupBox(pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString(), pfpSpread._user_account, pfpSpread.Sheets[0].GetValue(e.Row, "OUT_CODE").ToString(), pfpSpread.Sheets[0].GetValue(e.Row, "NAME").ToString(), pfpSpread.Sheets[0].GetValue(e.Row, "STANDARD").ToString(), pfpSpread.Sheets[0].GetValue(e.Row, "TYPE").ToString());
                                    if (basePopupBox.ShowDialog() == DialogResult.OK)
                                    {
                                        //pfpSpread.Sheets[0].SetValue(e.Row, e.Column, baseImagePopupBox._Image);
                                        //pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                    }
                                }
                                if (pfpSpread.Sheets[0].Columns[e.Column].Tag.ToString() == "")
                                {

                                }
                            }
                        }
                    }
                }

                else if (e.EditingControl.Text == "스펙")
                {
                    if (pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString() != "0" &&
                        pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString() != "")
                    {
                        if (pfpSpread != null && pfpSpread.Sheets[0].Columns[e.Column].CellType != null)
                        {
                            if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(ButtonCellType))
                            {

                                if (pfpSpread.Sheets[0].Columns[e.Column].Tag.ToString() == "스펙")
                                {
                                    제품스펙_PopupBox basePopupBox = new 제품스펙_PopupBox(pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString(), pfpSpread._user_account, pfpSpread.Sheets[0].GetValue(e.Row, "OUT_CODE").ToString(), pfpSpread.Sheets[0].GetValue(e.Row, "NAME").ToString(), pfpSpread.Sheets[0].GetValue(e.Row, "STANDARD").ToString(), pfpSpread.Sheets[0].GetValue(e.Row, "TYPE").ToString());
                                    if (basePopupBox.ShowDialog() == DialogResult.OK)
                                    {
                                        //pfpSpread.Sheets[0].SetValue(e.Row, e.Column, baseImagePopupBox._Image);
                                        //pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                    }
                                }
                                if (pfpSpread.Sheets[0].Columns[e.Column].Tag.ToString() == "")
                                {

                                }
                            }
                        }
                    }
                }

                else if (e.EditingControl.Text == "치수")
                {
                    if (pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString() != "0" &&
                        pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString() != "")
                    {
                        if (pfpSpread != null && pfpSpread.Sheets[0].Columns[e.Column].CellType != null)
                        {
                            if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(ButtonCellType))
                            {

                                if (pfpSpread.Sheets[0].Columns[e.Column].Tag.ToString() == "치수")
                                {
                                    치수등록_PopupBox basePopupBox = new 치수등록_PopupBox(
                                        pfpSpread.Sheets[0].GetValue(e.Row, "STOCK_MST_ID").ToString()
                                        , pfpSpread._user_account
                                        , pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString());
                                    if (basePopupBox.ShowDialog() == DialogResult.OK)
                                    {
                                        //pfpSpread.Sheets[0].SetValue(e.Row, e.Column, baseImagePopupBox._Image);
                                        //pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                    }
                                }
                                if (pfpSpread.Sheets[0].Columns[e.Column].Tag.ToString() == "")
                                {

                                }
                            }
                        }
                    }
                }

                else if (e.EditingControl.Text == "원가분석출력")
                {
                    string order_mst_id = pfpSpread.Sheets[0].GetValue(e.Row, "품번").ToString();
                    string order_mst_NAME = pfpSpread.Sheets[0].GetValue(e.Row, "품명").ToString();
                    DevExpress.XtraSpreadsheet.SpreadsheetControl sc = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                    sc.LoadDocument(Application.StartupPath + $"\\월별원가분석.xlsx", DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                    DevExpress.Spreadsheet.Worksheet sheet = sc.Document.Worksheets[0];

                    string str = $@"SELECT 
    RESOURCE_NO AS 품번,
	'' AS 품명 ,
	순서,
    구분,
	[YEAR],
    [1] AS  '1월' ,
    [2] AS  '2월',
    [3] AS  '3월',
    [4] AS  '4월',
    [5] AS  '5월',
    [6] AS  '6월',
    [7] AS  '7월',
    [8] AS  '8월',
    [9] AS  '9월',
    [10] AS '10월',
    [11] AS '11월',
    [12] AS '12월'
FROM 

(   
SELECT
RESOURCE_NO,
'10' AS 순서, 
'CT' AS 구분, 
MIN(CONVERT(DECIMAL(18,2),CYCLE_TIME)) AS D_VALUE,
 YEAR(REG_DATE) AS [Year],
        MONTH(REG_DATE) AS [Month]
  FROM [HS_MES].[dbo].[ELEC_SHOT]
    
  GROUP BY RESOURCE_NO,YEAR(REG_DATE), MONTH(REG_DATE)

  UNION ALL

    SELECT 
      RESOURCE_NO,
	  '11' AS 순서, 
      '작업시간' AS 작업시간,
        ISNULL((SUM(CONVERT(DECIMAL(18,2),[WORK_TIME])) / 60 / 60 ),'0') AS D_VALUE,  -- 시간
        YEAR(ORDER_DATE) AS [Year],
        MONTH(ORDER_DATE) AS [Month]
    FROM [HS_MES].[dbo].[WORK_PERFORMANCE]
     
    GROUP BY YEAR(ORDER_DATE), MONTH(ORDER_DATE),RESOURCE_NO



  



    UNION ALL

	 SELECT 
      A.RESOURCE_NO,
	  '12' AS 순서, 
      '성능가동율' AS 구분,
       ISNULL((CASE WHEN SUM(CONVERT(DECIMAL(18,2),WORK_TIME)) = '0' THEN '0' ELSE  SUM(CONVERT(DECIMAL(18,2),[QTY_COMPLETE])) / (SUM(CONVERT(DECIMAL(18,2),WORK_TIME))/60/60) / 22 * F.cavity END),'0') AS D_VALUE,  
        YEAR(ORDER_DATE) AS [Year],
        MONTH(ORDER_DATE) AS [Month]
	
		
    FROM [HS_MES].[dbo].[WORK_PERFORMANCE] AS A
	  INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E  WITH (NOLOCK)
  ON A.RESOURCE_NO = E.order_no
  AND A.LOT_NO = E.lot
  LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F  WITH (NOLOCK)
  ON E.code_md = F.code_md 
    
    GROUP BY YEAR(ORDER_DATE), MONTH(ORDER_DATE),A.RESOURCE_NO,F.cavity


	  UNION ALL
	  SELECT RESOURCE_NO ,순서,구분 , SUM(D_VALUE) , [Year] ,[Month] FROM 
	  (
	 SELECT 
      A.RESOURCE_NO,
	  '13' AS 순서, 
      '직접노무비' AS 구분,
      CASE WHEN  ISNULL((CASE WHEN SUM(CONVERT(DECIMAL(18,2),WORK_TIME)) = '0' THEN '0' ELSE  SUM(CONVERT(DECIMAL(18,2),[QTY_COMPLETE])) / (SUM(CONVERT(DECIMAL(18,2),WORK_TIME))/60/60) / 22 * F.cavity END),'0') ='0' THEN '0' 
	  ELSE ((16900 /ISNULL((CASE WHEN SUM(CONVERT(DECIMAL(18,2),WORK_TIME)) = '0' THEN '0' ELSE  SUM(CONVERT(DECIMAL(18,2),[QTY_COMPLETE])) / (SUM(CONVERT(DECIMAL(18,2),WORK_TIME))/60/60) / 22 * F.cavity END),'0')) * A.IN_PER)/ A.QTY_COMPLETE
	      END AS D_VALUE,  
        YEAR(ORDER_DATE) AS [Year],
        MONTH(ORDER_DATE) AS [Month]
		
	
		
    FROM [HS_MES].[dbo].[WORK_PERFORMANCE] AS A
	  INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E  WITH (NOLOCK)
  ON A.RESOURCE_NO = E.order_no
  AND A.LOT_NO = E.lot
  LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F  WITH (NOLOCK)
  ON E.code_md = F.code_md 
  LEFT OUTER JOIN [HS_MES].[dbo].[MATERIALCOST_PROCESS] AS G
  ON G.PROCESS_ID = '주조'
  LEFT OUTER JOIN [HS_MES].[dbo].[ELEC_SHOT] AS I
  ON A.RESOURCE_NO = I.RESOURCE_NO
  
    GROUP BY YEAR(ORDER_DATE), MONTH(ORDER_DATE),A.RESOURCE_NO,F.cavity,	A.WORK_TIME,  A.IN_PER,A.QTY_COMPLETE
	) AS A
	  
	GROUP BY
	[Year], [Month],A.RESOURCE_NO,순서,구분
	

	  UNION ALL
	  SELECT RESOURCE_NO ,순서,구분 , SUM(D_VALUE) , [Year] ,[Month] FROM 
	  (
	 SELECT 
      A.RESOURCE_NO,
	  '14' AS 순서, 
      '간접노무비' AS 구분,
      CASE WHEN  ISNULL((CASE WHEN SUM(CONVERT(DECIMAL(18,2),WORK_TIME)) = '0' THEN '0' ELSE  SUM(CONVERT(DECIMAL(18,2),[QTY_COMPLETE])) / (SUM(CONVERT(DECIMAL(18,2),WORK_TIME))/60/60) / 22 * F.cavity END),'0') ='0' THEN '0' 
	  ELSE ((16900 /ISNULL((CASE WHEN SUM(CONVERT(DECIMAL(18,2),WORK_TIME)) = '0' THEN '0' ELSE  SUM(CONVERT(DECIMAL(18,2),[QTY_COMPLETE])) / (SUM(CONVERT(DECIMAL(18,2),WORK_TIME))/60/60) / 22 * F.cavity END),'0')) * A.IN_PER * 81 / 100)/ A.QTY_COMPLETE
	      END AS D_VALUE,  
        YEAR(ORDER_DATE) AS [Year],
        MONTH(ORDER_DATE) AS [Month]
		
	
		
    FROM [HS_MES].[dbo].[WORK_PERFORMANCE] AS A
	  INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E  WITH (NOLOCK)
  ON A.RESOURCE_NO = E.order_no
  AND A.LOT_NO = E.lot
  LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F  WITH (NOLOCK)
  ON E.code_md = F.code_md 
  LEFT OUTER JOIN [HS_MES].[dbo].[MATERIALCOST_PROCESS] AS G
  ON G.PROCESS_ID = '주조'
  LEFT OUTER JOIN [HS_MES].[dbo].[ELEC_SHOT] AS I
  ON A.RESOURCE_NO = I.RESOURCE_NO
    
    GROUP BY YEAR(ORDER_DATE), MONTH(ORDER_DATE),A.RESOURCE_NO,F.cavity,	A.WORK_TIME,  A.IN_PER, A.QTY_COMPLETE
	) AS A
	  
	GROUP BY
	[Year], [Month],A.RESOURCE_NO,순서,구분
	
	UNION ALL

	 SELECT 
      A.RESOURCE_NO,
	  '15' AS 순서, 
      '종합가동율' AS 종합가동율,
       (ISNULL((CASE WHEN SUM(CONVERT(DECIMAL(18,2),WORK_TIME)) = '0' THEN '0' ELSE  SUM(CONVERT(DECIMAL(18,2),[QTY_COMPLETE])) / (SUM(CONVERT(DECIMAL(18,2),WORK_TIME))/60/60) / 22 * F.cavity END),'0')) * ROUND(ISNULL(SUM(CONVERT(DECIMAL(18,2),WORK_TIME)),0) / 60 / 60 / 24 ,2) /(1*260/12) AS D_VALUE,  
        YEAR(ORDER_DATE) AS [Year],
        MONTH(ORDER_DATE) AS [Month]
	
		
    FROM [HS_MES].[dbo].[WORK_PERFORMANCE] AS A
	  INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E  WITH (NOLOCK)
  ON A.RESOURCE_NO = E.order_no
  AND A.LOT_NO = E.lot
  LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F  WITH (NOLOCK)
  ON E.code_md = F.code_md 
    
    GROUP BY YEAR(ORDER_DATE), MONTH(ORDER_DATE),A.RESOURCE_NO,F.cavity

	UNION ALL

	 SELECT 
      A.RESOURCE_NO,
	  '16' AS 순서, 
      '설비감상비' AS 구분,
      CASE WHEN (NULLIF((CASE WHEN SUM(CONVERT(DECIMAL(18,2),WORK_TIME)) = '0' THEN '0' ELSE  SUM(CONVERT(DECIMAL(18,2),[QTY_COMPLETE])) / (SUM(CONVERT(DECIMAL(18,2),WORK_TIME))/60/60) / 22 * F.cavity END),'0')) * ROUND(NULLIF(SUM(CONVERT(DECIMAL(18,2),WORK_TIME)),0) / 60 / 60 / 24 ,2) /(1*260/12) = 0 THEN '0'
	  ELSE 
	  CONVERT(DECIMAL(18,2) ,G.EQUIPMENT_COST) / CONVERT(DECIMAL(18,2) ,G.EQUIPMENT_USE_YEAR) / CONVERT(DECIMAL(18,2) ,G.EQUIPMENT_OPERATION)/ CONVERT(DECIMAL(18,2) ,G.EQUIPMENT_OPERATION_DAY)/
	  (NULLIF((CASE WHEN SUM(CONVERT(DECIMAL(18,2),WORK_TIME)) = '0' THEN '0' ELSE  SUM(CONVERT(DECIMAL(18,2),[QTY_COMPLETE])) / (SUM(CONVERT(DECIMAL(18,2),WORK_TIME))/60/60) / 22 * F.cavity END),'0')) * ROUND(NULLIF(SUM(CONVERT(DECIMAL(18,2),WORK_TIME)),0) / 60 / 60 / 24 ,2) /(1*260/12) END  AS D_VALUE,  
        YEAR(ORDER_DATE) AS [Year],
        MONTH(ORDER_DATE) AS [Month]
	
		
    FROM [HS_MES].[dbo].[WORK_PERFORMANCE] AS A
	  INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E  WITH (NOLOCK)
  ON A.RESOURCE_NO = E.order_no
  AND A.LOT_NO = E.lot
  LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F  WITH (NOLOCK)
  ON E.code_md = F.code_md 
  INNER JOIN [HS_MES].[dbo].[MATERIALCOST_EQUIPMENT] AS G
  ON G.EQUIPMENT_ID ='설비850TON'
 --  WHERE A.QTY_COMPLETE >0 
    GROUP BY YEAR(ORDER_DATE), MONTH(ORDER_DATE),A.RESOURCE_NO,F.cavity,G.EQUIPMENT_COST,G.EQUIPMENT_USE_YEAR,G.EQUIPMENT_OPERATION,G.EQUIPMENT_OPERATION_DAY


		UNION ALL

	 SELECT 
      A.RESOURCE_NO,
	  '17' AS 순서, 
      '건물감상비' AS 구분,
     ISNULL( ((((((109*convert(int,G.UNIT_PRICE_PER_PYEONG))*5.5)/40)/260)/22)/((NULLIF((CASE WHEN SUM(CONVERT(DECIMAL(18,2),WORK_TIME)) = '0' THEN '0' ELSE  SUM(CONVERT(DECIMAL(18,2),[QTY_COMPLETE])) / (SUM(CONVERT(DECIMAL(18,2),WORK_TIME))/60/60) / 22 * F.cavity END),'0')) * ROUND(NULLIF(SUM(CONVERT(DECIMAL(18,2),WORK_TIME)),0) / 60 / 60 / 24 ,2) /(1*260/12)))/A.QTY_COMPLETE,0)  AS D_VALUE,  
        YEAR(ORDER_DATE) AS [Year],
        MONTH(ORDER_DATE) AS [Month]
	
		
    FROM [HS_MES].[dbo].[WORK_PERFORMANCE] AS A
	  INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E  WITH (NOLOCK)
  ON A.RESOURCE_NO = E.order_no
  AND A.LOT_NO = E.lot
  LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F  WITH (NOLOCK)
  ON E.code_md = F.code_md 
  INNER JOIN [HS_MES].[dbo].[MATERIALCOST_EQUIPMENT] AS G
  ON G.EQUIPMENT_ID ='설비850TON'
 --  WHERE A.QTY_COMPLETE >0 
    GROUP BY YEAR(ORDER_DATE), MONTH(ORDER_DATE),A.RESOURCE_NO,F.cavity,G.EQUIPMENT_COST,G.EQUIPMENT_USE_YEAR,G.EQUIPMENT_OPERATION,G.EQUIPMENT_OPERATION_DAY,A.QTY_COMPLETE,G.UNIT_PRICE_PER_PYEONG

	
		UNION ALL

	 SELECT 
      A.RESOURCE_NO,
	  '18' AS 순서, 
      '수선비' AS 구분,
     (( CONVERT(DECIMAL(18,2) ,G.EQUIPMENT_COST) / CONVERT(DECIMAL(18,2) ,G.EQUIPMENT_USE_YEAR) / CONVERT(DECIMAL(18,2) ,G.EQUIPMENT_OPERATION)/ CONVERT(DECIMAL(18,2) ,G.EQUIPMENT_OPERATION_DAY)/
	  (NULLIF((CASE WHEN SUM(CONVERT(DECIMAL(18,2),WORK_TIME)) = '0' THEN '0' ELSE  SUM(CONVERT(DECIMAL(18,2),[QTY_COMPLETE])) / (SUM(CONVERT(DECIMAL(18,2),WORK_TIME))/60/60) / 22 * F.cavity END),'0')) * ROUND(NULLIF(SUM(CONVERT(DECIMAL(18,2),WORK_TIME)),0) / 60 / 60 / 24 ,2) /(1*convert(int,G.EQUIPMENT_OPERATION)/convert(int,G.EQUIPMENT_USE_YEAR)))+(ISNULL( ((((((109*convert(int,G.UNIT_PRICE_PER_PYEONG))*5.5)/40)/convert(int,G.EQUIPMENT_OPERATION))/22)/((NULLIF((CASE WHEN SUM(CONVERT(DECIMAL(18,2),WORK_TIME)) = '0' THEN '0' ELSE  SUM(CONVERT(DECIMAL(18,2),[QTY_COMPLETE])) / (SUM(CONVERT(DECIMAL(18,2),WORK_TIME))/60/60) / 22 * F.cavity END),'0')) * ROUND(NULLIF(SUM(CONVERT(DECIMAL(18,2),WORK_TIME)),0) / 60 / 60 / 24 ,2) /(1*convert(int,G.EQUIPMENT_OPERATION)/convert(int,G.EQUIPMENT_USE_YEAR)))),0)))/A.QTY_COMPLETE AS D_VALUE,  
        YEAR(ORDER_DATE) AS [Year],
        MONTH(ORDER_DATE) AS [Month]
	
		
    FROM [HS_MES].[dbo].[WORK_PERFORMANCE] AS A
	  INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E  WITH (NOLOCK)
  ON A.RESOURCE_NO = E.order_no
  AND A.LOT_NO = E.lot
  LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F  WITH (NOLOCK)
  ON E.code_md = F.code_md 
  INNER JOIN [HS_MES].[dbo].[MATERIALCOST_EQUIPMENT] AS G
  ON G.EQUIPMENT_ID ='설비850TON'
 --  WHERE A.QTY_COMPLETE >0 
    GROUP BY YEAR(ORDER_DATE), MONTH(ORDER_DATE),A.RESOURCE_NO,F.cavity,G.EQUIPMENT_COST,G.EQUIPMENT_USE_YEAR,G.EQUIPMENT_OPERATION,G.EQUIPMENT_OPERATION_DAY,A.QTY_COMPLETE,G.UNIT_PRICE_PER_PYEONG,G.EQUIPMENT_OPERATION
		UNION ALL

		 SELECT RESOURCE_NO, 순서 , 구분 , AVG(D_VALUE) D_VALUE,[YEAR],[MONTH] FROM
(SELECT 
	 
    A.[RESOURCE_NO]  AS 'RESOURCE_NO',
    '19' AS 순서, 
    '전체전력비' AS 구분,
   
	YEAR(A.REG_DATE) AS [YEAR],
	MONTH(A.REG_DATE) AS [MONTH],
	
	(CONVERT(DECIMAL(18,2),sum(ISNULL((CONVERT(DECIMAL(18,2),ISNULL(ELECTRICAL_ENERGY,0)) * convert(int,(H.[EQUIPMENT_POWER_RATIO])) ),0))))  AS D_VALUE
	

  FROM [HS_MES].[dbo].[ELEC_SHOT] AS A

  INNER JOIN (SELECT RESOURCE_NO , LOT_NO, SUM(QTY_COMPLETE) QTY_COMPLETE  ,SUM(CONVERT(DECIMAL(18,2),IN_PER)) AS IN_PER  , SUM( CONVERT(DECIMAL(18,2),WORK_TIME)) AS WORK_TIME  FROM [HS_MES].[dbo].[WORK_PERFORMANCE] GROUP BY RESOURCE_NO, LOT_NO  ) AS J
  ON A.RESOURCE_NO = J.RESOURCE_NO
  AND A.LOT_NO = J.LOT_NO

   INNER JOIN [sea_mfg].[dbo].[resource] AS B WITH (NOLOCK)
  ON B.resource_no  = A.resource_no
  INNER JOIN [sea_mfg].[dbo].[schedrtg] AS C  WITH (NOLOCK)
  ON C.order_no = A.resource_no 
  AND C.lot = A.LOT_NO
  INNER JOIN [sea_mfg].[dbo].[resource] AS D  WITH (NOLOCK)
  ON C.workcenter =D.resource_no
  INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E  WITH (NOLOCK)
  ON A.RESOURCE_NO = E.order_no
  AND A.LOT_NO = E.lot
  LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F  WITH (NOLOCK)
  ON E.code_md = F.code_md 
  LEFT OUTER JOIN [HS_MES].[dbo].[MATERIALCOST_PROCESS] AS G
  ON G.PROCESS_ID = '주조'
  INNER JOIN HS_MES.dbo.MATERIALCOST_EQUIPMENT AS H
  ON H.EQUIPMENT_ID ='설비850TON'
  LEFT OUTER JOIN [HS_MES].[dbo].[ELECTRIC_USE] AS I
  ON A.RESOURCE_NO = I.RESOURCE_NO
   
  WHERE    CONVERT(DECIMAL(18,2),ELECTRICAL_ENERGY) <100

  group by A.REG_DATE
  ,A.[RESOURCE_NO]     
  
  ,F.cavity 
  ,J.QTY_COMPLETE
  ,J.WORK_TIME
  ) AS A

  GROUP BY  RESOURCE_NO, 순서, 구분,[YEAR], [MONTH]
		UNION ALL
	SELECT 
      A.RESOURCE_NO,
	  '20' AS 순서, 
      '간접경비' AS 구분,
     ((CONVERT(DECIMAL(18,2) ,G.EQUIPMENT_COST) / CONVERT(DECIMAL(18,2) ,G.EQUIPMENT_USE_YEAR) / CONVERT(DECIMAL(18,2) ,G.EQUIPMENT_OPERATION)/ CONVERT(DECIMAL(18,2) ,G.EQUIPMENT_OPERATION_DAY)/
	  (NULLIF((CASE WHEN SUM(CONVERT(DECIMAL(18,2),WORK_TIME)) = '0' THEN '0' ELSE  SUM(CONVERT(DECIMAL(18,2),[QTY_COMPLETE])) / (SUM(CONVERT(DECIMAL(18,2),WORK_TIME))/60/60) / 22 * F.cavity END),'0')) * ROUND(NULLIF(SUM(CONVERT(DECIMAL(18,2),WORK_TIME)),0) / 60 / 60 / 24 ,2) /(1*convert(int,G.EQUIPMENT_OPERATION)/convert(int,G.EQUIPMENT_USE_YEAR)))+(ISNULL( ((((((109*convert(int,G.UNIT_PRICE_PER_PYEONG))*5.5)/40)/convert(int,G.EQUIPMENT_OPERATION))/convert(int,G.EQUIPMENT_OPERATION_DAY))/((NULLIF((CASE WHEN SUM(CONVERT(DECIMAL(18,2),WORK_TIME)) = '0' THEN '0' ELSE  SUM(CONVERT(DECIMAL(18,2),[QTY_COMPLETE])) / (SUM(CONVERT(DECIMAL(18,2),WORK_TIME))/60/60) / 22 * F.cavity END),'0')) * ROUND(NULLIF(SUM(CONVERT(DECIMAL(18,2),WORK_TIME)),0) / 60 / 60 / 24 ,2) /(1*convert(int,G.EQUIPMENT_OPERATION)/convert(int,G.EQUIPMENT_USE_YEAR)))),0))+(( CONVERT(DECIMAL(18,2) ,G.EQUIPMENT_COST) / CONVERT(DECIMAL(18,2) ,G.EQUIPMENT_USE_YEAR) / CONVERT(DECIMAL(18,2) ,G.EQUIPMENT_OPERATION)/ CONVERT(DECIMAL(18,2) ,G.EQUIPMENT_OPERATION_DAY)/
	  (NULLIF((CASE WHEN SUM(CONVERT(DECIMAL(18,2),WORK_TIME)) = '0' THEN '0' ELSE  SUM(CONVERT(DECIMAL(18,2),[QTY_COMPLETE])) / (SUM(CONVERT(DECIMAL(18,2),WORK_TIME))/60/60) / 22 * F.cavity END),'0')) * ROUND(NULLIF(SUM(CONVERT(DECIMAL(18,2),WORK_TIME)),0) / 60 / 60 / 24 ,2) /(1*convert(int,G.EQUIPMENT_OPERATION)/convert(int,G.EQUIPMENT_USE_YEAR)))+(ISNULL( ((((((109*convert(int,G.UNIT_PRICE_PER_PYEONG))*5.5)/40)/convert(int,G.EQUIPMENT_OPERATION))/convert(int,G.EQUIPMENT_OPERATION_DAY))/((NULLIF((CASE WHEN SUM(CONVERT(DECIMAL(18,2),WORK_TIME)) = '0' THEN '0' ELSE  SUM(CONVERT(DECIMAL(18,2),[QTY_COMPLETE])) / (SUM(CONVERT(DECIMAL(18,2),WORK_TIME))/60/60) / 22 * F.cavity END),'0')) * ROUND(NULLIF(SUM(CONVERT(DECIMAL(18,2),WORK_TIME)),0) / 60 / 60 / 24 ,2) /(1*convert(int,G.EQUIPMENT_OPERATION)/convert(int,G.EQUIPMENT_USE_YEAR)))),0)))+(70))/A.QTY_COMPLETE  AS D_VALUE,  
        YEAR(ORDER_DATE) AS [Year],
        MONTH(ORDER_DATE) AS [Month]
	
		
    FROM [HS_MES].[dbo].[WORK_PERFORMANCE] AS A
	  INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E  WITH (NOLOCK)
  ON A.RESOURCE_NO = E.order_no
  AND A.LOT_NO = E.lot
  LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F  WITH (NOLOCK)
  ON E.code_md = F.code_md 
  INNER JOIN [HS_MES].[dbo].[MATERIALCOST_EQUIPMENT] AS G
  ON G.EQUIPMENT_ID ='설비850TON'
 --  WHERE A.QTY_COMPLETE >0 
    GROUP BY YEAR(ORDER_DATE), MONTH(ORDER_DATE),A.RESOURCE_NO,F.cavity,G.EQUIPMENT_COST,G.EQUIPMENT_USE_YEAR,G.EQUIPMENT_OPERATION,G.EQUIPMENT_OPERATION_DAY,A.QTY_COMPLETE,convert(int,G.UNIT_PRICE_PER_PYEONG),G.EQUIPMENT_USE_YEAR

UNION ALL

  SELECT 
      RESOURCE_NO,
	  '21' AS 순서, 
      '재료비' AS 구분,
      convert(decimal(18,2),(CONVERT(decimal(18,2),isnull((SELECT  top 1 [qty_per]  FROM [sea_mfg].[dbo].[cproduct_defn] where resource_no = A.RESOURCE_NO and ENG_CHG_CODE ='A' ),'0'))) * CONVERT(int,isnull((SELECT TOP 1 [price] FROM [sea_mfg].[dbo].[prices] where resource_no = (SELECT  top 1 [resource_used]  FROM [sea_mfg].[dbo].[cproduct_defn] where resource_no = A.RESOURCE_NO and ENG_CHG_CODE ='A') order by update_date desc),'0')) * (   (convert(int,1.05) /100.0 +1) )  ) AS D_VALUE,  -- 시간
        YEAR(ORDER_DATE) AS [Year],
        MONTH(ORDER_DATE) AS [Month]
    FROM [HS_MES].[dbo].[WORK_PERFORMANCE] AS A
     
    GROUP BY YEAR(ORDER_DATE), MONTH(ORDER_DATE),RESOURCE_NO

UNION ALL

  SELECT 
      A.RESOURCE_NO RESOURCE_NO,
	  '31' AS 순서, 
      (SELECT  top 1 [resource_used]  FROM [sea_mfg].[dbo].[cproduct_defn] where resource_no = A.RESOURCE_NO and ENG_CHG_CODE ='A') AS 구분,
      convert(decimal(18,2),(CONVERT(decimal(18,2),isnull((SELECT  top 1 [qty_per]  FROM [sea_mfg].[dbo].[cproduct_defn] where resource_no = A.RESOURCE_NO and ENG_CHG_CODE ='A' ),'0'))) * CONVERT(int,isnull((SELECT TOP 1 [price] FROM [sea_mfg].[dbo].[prices] where resource_no = (SELECT  top 1 [resource_used]  FROM [sea_mfg].[dbo].[cproduct_defn] where resource_no = A.RESOURCE_NO and ENG_CHG_CODE ='A') order by update_date desc),'0')) * (   (convert(int,G.MATERIAL_COST_PER) /100.0 +1) )  ) AS D_VALUE,  -- 시간
        YEAR(ORDER_DATE) AS [Year],
        MONTH(ORDER_DATE) AS [Month]
    FROM [HS_MES].[dbo].[WORK_PERFORMANCE] AS A

	  LEFT OUTER JOIN [HS_MES].[dbo].[MATERIALCOST_PROCESS] AS G
  ON G.PROCESS_ID = '주조'
     
    GROUP BY YEAR(ORDER_DATE), MONTH(ORDER_DATE),A.RESOURCE_NO,G.MATERIAL_COST_PER


		UNION ALL

	
SELECT RESOURCE_NO, 순서, 구분, AVG(D_VALUE) D_VALUE, [YEAR], [MONTH]
FROM
(
    SELECT 
        A.[RESOURCE_NO] AS 'RESOURCE_NO',
        '22' AS 순서, 
        '성능가동률_BEST' AS 구분,
        YEAR(A.REG_DATE) AS [YEAR],
        MONTH(A.REG_DATE) AS [MONTH],
        
        -- 0으로 나누는 오류 방지를 위해 NULLIF 사용
        (3600 / NULLIF(AVG(CONVERT(DECIMAL(18,2), A.CYCLE_TIME)), 0) * 0.85) * NULLIF(ISNULL(F.cavity, '0'), 0) AS D_VALUE

    FROM [HS_MES].[dbo].[ELEC_SHOT] AS A
    INNER JOIN 
    (
        SELECT RESOURCE_NO, LOT_NO, SUM(QTY_COMPLETE) QTY_COMPLETE, 
               SUM(CONVERT(DECIMAL(18,2), IN_PER)) AS IN_PER, 
               SUM(CONVERT(DECIMAL(18,2), WORK_TIME)) AS WORK_TIME 
        FROM [HS_MES].[dbo].[WORK_PERFORMANCE] 
        GROUP BY RESOURCE_NO, LOT_NO
    ) AS J ON A.RESOURCE_NO = J.RESOURCE_NO AND A.LOT_NO = J.LOT_NO
    INNER JOIN [sea_mfg].[dbo].[resource] AS B WITH (NOLOCK) ON B.resource_no = A.resource_no
    INNER JOIN [sea_mfg].[dbo].[schedrtg] AS C WITH (NOLOCK) ON C.order_no = A.resource_no AND C.lot = A.LOT_NO
    INNER JOIN [sea_mfg].[dbo].[resource] AS D WITH (NOLOCK) ON C.workcenter = D.resource_no
    INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E WITH (NOLOCK) ON A.RESOURCE_NO = E.order_no AND A.LOT_NO = E.lot
    LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F WITH (NOLOCK) ON E.code_md = F.code_md
    LEFT OUTER JOIN [HS_MES].[dbo].[MATERIALCOST_PROCESS] AS G ON G.PROCESS_ID = '주조'
    INNER JOIN HS_MES.dbo.MATERIALCOST_EQUIPMENT AS H ON H.EQUIPMENT_ID = '설비850TON'
    LEFT OUTER JOIN [HS_MES].[dbo].[ELECTRIC_USE] AS I ON A.RESOURCE_NO = I.RESOURCE_NO
    
    WHERE CONVERT(DECIMAL(18,2), ELECTRICAL_ENERGY) < 100

    GROUP BY A.REG_DATE, A.[RESOURCE_NO], F.cavity, J.QTY_COMPLETE, J.WORK_TIME
) AS A

GROUP BY RESOURCE_NO, 순서, 구분, [YEAR], [MONTH]


  UNION ALL 

SELECT RESOURCE_NO, 순서, 구분, AVG(D_VALUE) D_VALUE, [YEAR], [MONTH]
FROM
(
    SELECT 
        A.[RESOURCE_NO] AS 'RESOURCE_NO',
        '23' AS 순서, 
        '종합가동률_BEST' AS 구분,
        YEAR(A.REG_DATE) AS [YEAR],
        MONTH(A.REG_DATE) AS [MONTH],
        
        -- 0으로 나누는 오류 방지를 위해 NULLIF 사용
        (3600 / NULLIF(AVG(CONVERT(DECIMAL(18,2), A.CYCLE_TIME)), 0) * 0.85) * NULLIF(ISNULL(F.cavity, '0'), 0) * 1 AS D_VALUE

    FROM [HS_MES].[dbo].[ELEC_SHOT] AS A
    INNER JOIN 
    (
        SELECT RESOURCE_NO, LOT_NO, SUM(QTY_COMPLETE) QTY_COMPLETE, 
               SUM(CONVERT(DECIMAL(18,2), IN_PER)) AS IN_PER, 
               SUM(CONVERT(DECIMAL(18,2), WORK_TIME)) AS WORK_TIME 
        FROM [HS_MES].[dbo].[WORK_PERFORMANCE] 
        GROUP BY RESOURCE_NO, LOT_NO
    ) AS J ON A.RESOURCE_NO = J.RESOURCE_NO AND A.LOT_NO = J.LOT_NO
    INNER JOIN [sea_mfg].[dbo].[resource] AS B WITH (NOLOCK) ON B.resource_no = A.resource_no
    INNER JOIN [sea_mfg].[dbo].[schedrtg] AS C WITH (NOLOCK) ON C.order_no = A.resource_no AND C.lot = A.LOT_NO
    INNER JOIN [sea_mfg].[dbo].[resource] AS D WITH (NOLOCK) ON C.workcenter = D.resource_no
    INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E WITH (NOLOCK) ON A.RESOURCE_NO = E.order_no AND A.LOT_NO = E.lot
    LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F WITH (NOLOCK) ON E.code_md = F.code_md
    LEFT OUTER JOIN [HS_MES].[dbo].[MATERIALCOST_PROCESS] AS G ON G.PROCESS_ID = '주조'
    INNER JOIN HS_MES.dbo.MATERIALCOST_EQUIPMENT AS H ON H.EQUIPMENT_ID = '설비850TON'
    LEFT OUTER JOIN [HS_MES].[dbo].[ELECTRIC_USE] AS I ON A.RESOURCE_NO = I.RESOURCE_NO
    
    WHERE CONVERT(DECIMAL(18,2), ELECTRICAL_ENERGY) < 100

    GROUP BY A.REG_DATE, A.[RESOURCE_NO], F.cavity, J.QTY_COMPLETE, J.WORK_TIME
) AS A

GROUP BY RESOURCE_NO, 순서, 구분, [YEAR], [MONTH]

  UNION ALL
  SELECT RESOURCE_NO, 순서, 구분, AVG(D_VALUE) D_VALUE, [YEAR], [MONTH]
FROM
(
    SELECT 
        A.[RESOURCE_NO] AS 'RESOURCE_NO',
        '24' AS 순서, 
        '직접노무비_BEST' AS 구분,
        YEAR(A.REG_DATE) AS [YEAR],
        MONTH(A.REG_DATE) AS [MONTH],
        
        -- 0으로 나누는 오류 방지를 위해 NULLIF 사용
        (16900 / NULLIF(
            (3600 / NULLIF(AVG(CONVERT(DECIMAL(18,2), A.CYCLE_TIME)), 0)) * 0.85 * ISNULL(F.cavity, '0'), 
            0
        )) * 1 AS D_VALUE

    FROM [HS_MES].[dbo].[ELEC_SHOT] AS A
    INNER JOIN 
    (
        SELECT RESOURCE_NO, LOT_NO, SUM(QTY_COMPLETE) QTY_COMPLETE, 
               SUM(CONVERT(DECIMAL(18,2), IN_PER)) AS IN_PER, 
               SUM(CONVERT(DECIMAL(18,2), WORK_TIME)) AS WORK_TIME 
        FROM [HS_MES].[dbo].[WORK_PERFORMANCE] 
        GROUP BY RESOURCE_NO, LOT_NO
    ) AS J ON A.RESOURCE_NO = J.RESOURCE_NO AND A.LOT_NO = J.LOT_NO
    INNER JOIN [sea_mfg].[dbo].[resource] AS B WITH (NOLOCK) ON B.resource_no = A.resource_no
    INNER JOIN [sea_mfg].[dbo].[schedrtg] AS C WITH (NOLOCK) ON C.order_no = A.resource_no AND C.lot = A.LOT_NO
    INNER JOIN [sea_mfg].[dbo].[resource] AS D WITH (NOLOCK) ON C.workcenter = D.resource_no
    INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E WITH (NOLOCK) ON A.RESOURCE_NO = E.order_no AND A.LOT_NO = E.lot
    LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F WITH (NOLOCK) ON E.code_md = F.code_md
    LEFT OUTER JOIN [HS_MES].[dbo].[MATERIALCOST_PROCESS] AS G ON G.PROCESS_ID = '주조'
    INNER JOIN HS_MES.dbo.MATERIALCOST_EQUIPMENT AS H ON H.EQUIPMENT_ID = '설비850TON'
    LEFT OUTER JOIN [HS_MES].[dbo].[ELECTRIC_USE] AS I ON A.RESOURCE_NO = I.RESOURCE_NO
    
    WHERE CONVERT(DECIMAL(18,2), ELECTRICAL_ENERGY) < 100

    GROUP BY A.REG_DATE, A.[RESOURCE_NO], F.cavity, J.QTY_COMPLETE, J.WORK_TIME
) AS A

GROUP BY RESOURCE_NO, 순서, 구분, [YEAR], [MONTH]

  UNION ALL
SELECT RESOURCE_NO, 순서, 구분, AVG(D_VALUE) D_VALUE, [YEAR], [MONTH]
FROM
(
    SELECT 
        A.[RESOURCE_NO] AS 'RESOURCE_NO',
        '25' AS 순서, 
        '간접노무비_BEST' AS 구분,
        YEAR(A.REG_DATE) AS [YEAR],
        MONTH(A.REG_DATE) AS [MONTH],
        
        -- 0으로 나누는 오류 방지를 위해 NULLIF 사용
        (16900 / NULLIF(
            (3600 / NULLIF(AVG(CONVERT(DECIMAL(18,2), A.CYCLE_TIME)), 0)) * 0.85 * ISNULL(F.cavity, '0'), 
            0
        )) * 1 * 0.81 AS D_VALUE

    FROM [HS_MES].[dbo].[ELEC_SHOT] AS A
    INNER JOIN 
    (
        SELECT RESOURCE_NO, LOT_NO, SUM(QTY_COMPLETE) QTY_COMPLETE, 
               SUM(CONVERT(DECIMAL(18,2), IN_PER)) AS IN_PER, 
               SUM(CONVERT(DECIMAL(18,2), WORK_TIME)) AS WORK_TIME 
        FROM [HS_MES].[dbo].[WORK_PERFORMANCE] 
        GROUP BY RESOURCE_NO, LOT_NO
    ) AS J ON A.RESOURCE_NO = J.RESOURCE_NO AND A.LOT_NO = J.LOT_NO
    INNER JOIN [sea_mfg].[dbo].[resource] AS B WITH (NOLOCK) ON B.resource_no = A.resource_no
    INNER JOIN [sea_mfg].[dbo].[schedrtg] AS C WITH (NOLOCK) ON C.order_no = A.resource_no AND C.lot = A.LOT_NO
    INNER JOIN [sea_mfg].[dbo].[resource] AS D WITH (NOLOCK) ON C.workcenter = D.resource_no
    INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E WITH (NOLOCK) ON A.RESOURCE_NO = E.order_no AND A.LOT_NO = E.lot
    LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F WITH (NOLOCK) ON E.code_md = F.code_md
    LEFT OUTER JOIN [HS_MES].[dbo].[MATERIALCOST_PROCESS] AS G ON G.PROCESS_ID = '주조'
    INNER JOIN HS_MES.dbo.MATERIALCOST_EQUIPMENT AS H ON H.EQUIPMENT_ID = '설비850TON'
    LEFT OUTER JOIN [HS_MES].[dbo].[ELECTRIC_USE] AS I ON A.RESOURCE_NO = I.RESOURCE_NO
    
    WHERE CONVERT(DECIMAL(18,2), ELECTRICAL_ENERGY) < 100

    GROUP BY A.REG_DATE, A.[RESOURCE_NO], F.cavity, J.QTY_COMPLETE, J.WORK_TIME
) AS A

GROUP BY RESOURCE_NO, 순서, 구분, [YEAR], [MONTH]

  UNION ALL
SELECT RESOURCE_NO, 순서, 구분, AVG(D_VALUE) D_VALUE, [YEAR], [MONTH]
FROM
(
    SELECT 
        A.[RESOURCE_NO] AS 'RESOURCE_NO',
        '26' AS 순서, 
        '설비감상비_BEST' AS 구분,
        YEAR(A.REG_DATE) AS [YEAR],
        MONTH(A.REG_DATE) AS [MONTH],
        
        -- 0으로 나누는 오류 방지를 위해 NULLIF 사용
        -- EQUIPMENT_COST가 0인 경우 나누기 오류를 방지
        -- CYCLE_TIME이 0인 경우를 고려하여 NULLIF 사용
        convert(int, H.EQUIPMENT_COST) / NULLIF(12 * 22 * 260, 0) / NULLIF(
            (3600 / NULLIF(AVG(CONVERT(DECIMAL(18,2), A.CYCLE_TIME)), 0)) * 0.85 * ISNULL(F.cavity, '0') * 1, 
            0
        ) AS D_VALUE

    FROM [HS_MES].[dbo].[ELEC_SHOT] AS A
    INNER JOIN 
    (
        SELECT RESOURCE_NO, LOT_NO, SUM(QTY_COMPLETE) QTY_COMPLETE, 
               SUM(CONVERT(DECIMAL(18,2), IN_PER)) AS IN_PER, 
               SUM(CONVERT(DECIMAL(18,2), WORK_TIME)) AS WORK_TIME 
        FROM [HS_MES].[dbo].[WORK_PERFORMANCE] 
        GROUP BY RESOURCE_NO, LOT_NO
    ) AS J ON A.RESOURCE_NO = J.RESOURCE_NO AND A.LOT_NO = J.LOT_NO
    INNER JOIN [sea_mfg].[dbo].[resource] AS B WITH (NOLOCK) ON B.resource_no = A.resource_no
    INNER JOIN [sea_mfg].[dbo].[schedrtg] AS C WITH (NOLOCK) ON C.order_no = A.resource_no AND C.lot = A.LOT_NO
    INNER JOIN [sea_mfg].[dbo].[resource] AS D WITH (NOLOCK) ON C.workcenter = D.resource_no
    INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E WITH (NOLOCK) ON A.RESOURCE_NO = E.order_no AND A.LOT_NO = E.lot
    LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F WITH (NOLOCK) ON E.code_md = F.code_md
    LEFT OUTER JOIN [HS_MES].[dbo].[MATERIALCOST_PROCESS] AS G ON G.PROCESS_ID = '주조'
    INNER JOIN HS_MES.dbo.MATERIALCOST_EQUIPMENT AS H ON H.EQUIPMENT_ID = '설비850TON'
    LEFT OUTER JOIN [HS_MES].[dbo].[ELECTRIC_USE] AS I ON A.RESOURCE_NO = I.RESOURCE_NO
    
    WHERE CONVERT(DECIMAL(18,2), ELECTRICAL_ENERGY) < 100

    GROUP BY A.REG_DATE, A.[RESOURCE_NO], F.cavity, J.QTY_COMPLETE, J.WORK_TIME, H.EQUIPMENT_COST
) AS A

GROUP BY RESOURCE_NO, 순서, 구분, [YEAR], [MONTH]

  UNION ALL
  SELECT RESOURCE_NO, 순서, 구분, AVG(D_VALUE) D_VALUE, [YEAR], [MONTH]
FROM
(
    SELECT 
        A.[RESOURCE_NO] AS 'RESOURCE_NO',
        '27' AS 순서, 
        '건물감상비_BEST' AS 구분,
        YEAR(A.REG_DATE) AS [YEAR],
        MONTH(A.REG_DATE) AS [MONTH],
        
        -- 0으로 나누는 오류 방지를 위해 NULLIF 사용
        (((((109 * convert(int, H.UNIT_PRICE_PER_PYEONG)) * 5.5) / 40) / convert(int, H.EQUIPMENT_OPERATION_DAY)) / convert(int, H.EQUIPMENT_OPERATION))
        / NULLIF((3600 / NULLIF(AVG(CONVERT(DECIMAL(18,2), A.CYCLE_TIME)), 0) * 0.85) * NULLIF(ISNULL(F.cavity, '0'), 0) * 1, 0) AS D_VALUE

    FROM [HS_MES].[dbo].[ELEC_SHOT] AS A
    INNER JOIN 
    (
        SELECT RESOURCE_NO, LOT_NO, SUM(QTY_COMPLETE) QTY_COMPLETE, 
               SUM(CONVERT(DECIMAL(18,2), IN_PER)) AS IN_PER, 
               SUM(CONVERT(DECIMAL(18,2), WORK_TIME)) AS WORK_TIME 
        FROM [HS_MES].[dbo].[WORK_PERFORMANCE] 
        GROUP BY RESOURCE_NO, LOT_NO
    ) AS J ON A.RESOURCE_NO = J.RESOURCE_NO AND A.LOT_NO = J.LOT_NO
    INNER JOIN [sea_mfg].[dbo].[resource] AS B WITH (NOLOCK) ON B.resource_no = A.resource_no
    INNER JOIN [sea_mfg].[dbo].[schedrtg] AS C WITH (NOLOCK) ON C.order_no = A.resource_no AND C.lot = A.LOT_NO
    INNER JOIN [sea_mfg].[dbo].[resource] AS D WITH (NOLOCK) ON C.workcenter = D.resource_no
    INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E WITH (NOLOCK) ON A.RESOURCE_NO = E.order_no AND A.LOT_NO = E.lot
    LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F WITH (NOLOCK) ON E.code_md = F.code_md
    LEFT OUTER JOIN [HS_MES].[dbo].[MATERIALCOST_PROCESS] AS G ON G.PROCESS_ID = '주조'
    INNER JOIN HS_MES.dbo.MATERIALCOST_EQUIPMENT AS H ON H.EQUIPMENT_ID = '설비850TON'
    LEFT OUTER JOIN [HS_MES].[dbo].[ELECTRIC_USE] AS I ON A.RESOURCE_NO = I.RESOURCE_NO
    
    WHERE CONVERT(DECIMAL(18,2), ELECTRICAL_ENERGY) < 100

    GROUP BY A.REG_DATE, A.[RESOURCE_NO], H.UNIT_PRICE_PER_PYEONG, F.cavity, 
             J.QTY_COMPLETE, J.WORK_TIME, H.EQUIPMENT_USE_YEAR, 
             H.EQUIPMENT_OPERATION_DAY, H.EQUIPMENT_OPERATION
) AS A

GROUP BY RESOURCE_NO, 순서, 구분, [YEAR], [MONTH]
 

  UNION ALL
SELECT RESOURCE_NO, 순서, 구분, AVG(D_VALUE) D_VALUE, [YEAR], [MONTH]
FROM
(
    SELECT 
        A.[RESOURCE_NO] AS 'RESOURCE_NO',
        '28' AS 순서, 
        '수선비_BEST' AS 구분,
        YEAR(A.REG_DATE) AS [YEAR],
        MONTH(A.REG_DATE) AS [MONTH],
        
        -- 분모에 0을 방지하기 위해 NULLIF 사용
        (
            convert(int, H.EQUIPMENT_COST) / 
            NULLIF(convert(int, H.EQUIPMENT_USE_YEAR), 0) / 
            NULLIF(convert(int, H.EQUIPMENT_OPERATION_DAY), 0) / 
            NULLIF(convert(int, H.EQUIPMENT_OPERATION), 0) / 
            NULLIF(
                (3600 / NULLIF(AVG(CONVERT(DECIMAL(18,2), A.CYCLE_TIME)), 0)) * 0.85 * ISNULL(F.cavity, '1') * 1, 
                0
            )
        ) + 
        (
            (
                ((109 * convert(int, H.UNIT_PRICE_PER_PYEONG) * 5.5) / 40) / 
                NULLIF(convert(int, H.EQUIPMENT_OPERATION_DAY), 0)
            ) / 
            NULLIF(convert(int, H.EQUIPMENT_OPERATION), 0)
        ) / 
        NULLIF(
            (3600 / NULLIF(AVG(CONVERT(DECIMAL(18,2), A.CYCLE_TIME)), 0)) * 0.85 * ISNULL(F.cavity, '1') * 1, 
            0
        )
        * 0.049 AS D_VALUE

    FROM [HS_MES].[dbo].[ELEC_SHOT] AS A
    INNER JOIN 
    (
        SELECT RESOURCE_NO, LOT_NO, SUM(QTY_COMPLETE) QTY_COMPLETE, 
               SUM(CONVERT(DECIMAL(18,2), IN_PER)) AS IN_PER, 
               SUM(CONVERT(DECIMAL(18,2), WORK_TIME)) AS WORK_TIME 
        FROM [HS_MES].[dbo].[WORK_PERFORMANCE] 
        GROUP BY RESOURCE_NO, LOT_NO
    ) AS J ON A.RESOURCE_NO = J.RESOURCE_NO AND A.LOT_NO = J.LOT_NO
    INNER JOIN [sea_mfg].[dbo].[resource] AS B WITH (NOLOCK) ON B.resource_no = A.resource_no
    INNER JOIN [sea_mfg].[dbo].[schedrtg] AS C WITH (NOLOCK) ON C.order_no = A.resource_no AND C.lot = A.LOT_NO
    INNER JOIN [sea_mfg].[dbo].[resource] AS D WITH (NOLOCK) ON C.workcenter = D.resource_no
    INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E WITH (NOLOCK) ON A.RESOURCE_NO = E.order_no AND A.LOT_NO = E.lot
    LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F WITH (NOLOCK) ON E.code_md = F.code_md
    LEFT OUTER JOIN [HS_MES].[dbo].[MATERIALCOST_PROCESS] AS G ON G.PROCESS_ID = '주조'
    INNER JOIN HS_MES.dbo.MATERIALCOST_EQUIPMENT AS H ON H.EQUIPMENT_ID = '설비850TON'
    LEFT OUTER JOIN [HS_MES].[dbo].[ELECTRIC_USE] AS I ON A.RESOURCE_NO = I.RESOURCE_NO
    
    WHERE CONVERT(DECIMAL(18,2), ELECTRICAL_ENERGY) < 100

    GROUP BY A.REG_DATE, A.[RESOURCE_NO], H.UNIT_PRICE_PER_PYEONG, F.cavity, J.QTY_COMPLETE, 
             J.WORK_TIME, H.EQUIPMENT_COST, H.EQUIPMENT_USE_YEAR, H.EQUIPMENT_OPERATION_DAY, 
             H.EQUIPMENT_OPERATION
) AS A

GROUP BY RESOURCE_NO, 순서, 구분, [YEAR], [MONTH]


  UNION ALL
  SELECT RESOURCE_NO, 순서 , 구분 , AVG(D_VALUE) D_VALUE,[YEAR],[MONTH] FROM
(SELECT 
	 
    A.[RESOURCE_NO]  AS 'RESOURCE_NO',
    '29' AS 순서, 
    '전체전력비_BEST' AS 구분,
   
	YEAR(A.REG_DATE) AS [YEAR],
	MONTH(A.REG_DATE) AS [MONTH],
	
	(CONVERT(DECIMAL(18,2),sum(ISNULL((CONVERT(DECIMAL(18,2),ISNULL(ELECTRICAL_ENERGY,0)) * convert(int,(H.[EQUIPMENT_POWER_RATIO])) ),0))))/J.QTY_COMPLETE  AS D_VALUE
	

  FROM [HS_MES].[dbo].[ELEC_SHOT] AS A

  INNER JOIN (SELECT RESOURCE_NO , LOT_NO,     
  CASE 
        WHEN SUM(QTY_COMPLETE) = 0 THEN 1 
        ELSE SUM(QTY_COMPLETE) 
    END AS QTY_COMPLETE ,SUM(CONVERT(DECIMAL(18,2),IN_PER)) AS IN_PER  , SUM( CONVERT(DECIMAL(18,2),WORK_TIME)) AS WORK_TIME  FROM [HS_MES].[dbo].[WORK_PERFORMANCE] GROUP BY RESOURCE_NO, LOT_NO  ) AS J
  ON A.RESOURCE_NO = J.RESOURCE_NO
  AND A.LOT_NO = J.LOT_NO

   INNER JOIN [sea_mfg].[dbo].[resource] AS B WITH (NOLOCK)
  ON B.resource_no  = A.resource_no
  INNER JOIN [sea_mfg].[dbo].[schedrtg] AS C  WITH (NOLOCK)
  ON C.order_no = A.resource_no 
  AND C.lot = A.LOT_NO
  INNER JOIN [sea_mfg].[dbo].[resource] AS D  WITH (NOLOCK)
  ON C.workcenter =D.resource_no
  INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E  WITH (NOLOCK)
  ON A.RESOURCE_NO = E.order_no
  AND A.LOT_NO = E.lot
  LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F  WITH (NOLOCK)
  ON E.code_md = F.code_md 
  LEFT OUTER JOIN [HS_MES].[dbo].[MATERIALCOST_PROCESS] AS G
  ON G.PROCESS_ID = '주조'
  INNER JOIN HS_MES.dbo.MATERIALCOST_EQUIPMENT AS H
  ON H.EQUIPMENT_ID ='설비850TON'
  LEFT OUTER JOIN [HS_MES].[dbo].[ELECTRIC_USE] AS I
  ON A.RESOURCE_NO = I.RESOURCE_NO
   
  WHERE    CONVERT(DECIMAL(18,2),ELECTRICAL_ENERGY) <100

  group by A.REG_DATE
  ,A.[RESOURCE_NO]     
  
  ,F.cavity 
  ,J.QTY_COMPLETE
  ,J.WORK_TIME
  ) AS A

  GROUP BY  RESOURCE_NO, 순서, 구분,[YEAR], [MONTH]

  UNION ALL
SELECT 
    RESOURCE_NO, 
    순서,
    구분,
    AVG(D_VALUE) AS D_VALUE,
    [YEAR],
    [MONTH] 
FROM
(
    SELECT 
        A.[RESOURCE_NO] AS 'RESOURCE_NO',
        '30' AS 순서, 
        '간접경비_BEST' AS 구분,
        YEAR(A.REG_DATE) AS [YEAR],
        MONTH(A.REG_DATE) AS [MONTH],

        (
            -- 첫 번째 계산식: CYCLE_TIME이 0일 경우 처리
            (CONVERT(INT, H.EQUIPMENT_COST) / 
            CONVERT(INT, H.EQUIPMENT_USE_YEAR) / 
            CONVERT(INT, H.EQUIPMENT_OPERATION_DAY) / 
            CONVERT(INT, H.EQUIPMENT_OPERATION) / 
            (3600 / NULLIF(AVG(CONVERT(DECIMAL(18,2), A.CYCLE_TIME)), 0) * 0.85) *
            NULLIF(ISNULL(F.cavity, 0), 0) * 1)
        ) 
        + 
        (
            -- 두 번째 계산식: CYCLE_TIME이 0일 경우 처리
            (((109 * CONVERT(INT, H.UNIT_PRICE_PER_PYEONG) * 5.5) / 40) / 
            CONVERT(INT, H.EQUIPMENT_OPERATION_DAY) / 
            CONVERT(INT, H.EQUIPMENT_OPERATION)) / 
            (3600 / NULLIF(AVG(CONVERT(DECIMAL(18,2), A.CYCLE_TIME)), 0) * 0.85) * 
            NULLIF(ISNULL(F.cavity, 0), 0) * 1
        ) 
        + 
        (
            -- 세 번째 계산식: CYCLE_TIME이 0일 경우 처리
            (((CONVERT(INT, H.EQUIPMENT_COST) / CONVERT(INT, H.EQUIPMENT_USE_YEAR) / 
            CONVERT(INT, H.EQUIPMENT_OPERATION_DAY) / 
            CONVERT(INT, H.EQUIPMENT_OPERATION) / 
            (3600 / NULLIF(AVG(CONVERT(DECIMAL(18,2), A.CYCLE_TIME)), 0) * 0.85) * 
            NULLIF(ISNULL(F.cavity, 0), 0) * 1) + 
            (((109 * CONVERT(INT, H.UNIT_PRICE_PER_PYEONG) * 5.5) / 40) / 
            CONVERT(INT, H.EQUIPMENT_OPERATION_DAY) / 
            CONVERT(INT, H.EQUIPMENT_OPERATION)) / 
            (3600 / NULLIF(AVG(CONVERT(DECIMAL(18,2), A.CYCLE_TIME)), 0) * 0.85) * 
            NULLIF(ISNULL(F.cavity, 0), 0) * 1
            )
        ) 
        * 0.049
        +
        -- 전기 에너지 계산식
        (
            CONVERT(DECIMAL(18,2), SUM(ISNULL((CONVERT(DECIMAL(18,2), ISNULL(ELECTRICAL_ENERGY, 0)) * CONVERT(INT, (H.[EQUIPMENT_POWER_RATIO]))), 0)))) / 
            J.QTY_COMPLETE
        ) * 0.206 AS D_VALUE

    FROM [HS_MES].[dbo].[ELEC_SHOT] AS A
    INNER JOIN 
    (
        SELECT RESOURCE_NO, LOT_NO, 
               CASE WHEN SUM(QTY_COMPLETE) = 0 THEN 1 ELSE SUM(QTY_COMPLETE) END AS QTY_COMPLETE,
               SUM(CONVERT(DECIMAL(18,2), IN_PER)) AS IN_PER,
               SUM(CONVERT(DECIMAL(18,2), WORK_TIME)) AS WORK_TIME  
        FROM [HS_MES].[dbo].[WORK_PERFORMANCE] 
        GROUP BY RESOURCE_NO, LOT_NO
    ) AS J
    ON A.RESOURCE_NO = J.RESOURCE_NO AND A.LOT_NO = J.LOT_NO
    INNER JOIN [sea_mfg].[dbo].[resource] AS B WITH (NOLOCK)
    ON B.resource_no = A.resource_no
    INNER JOIN [sea_mfg].[dbo].[schedrtg] AS C WITH (NOLOCK)
    ON C.order_no = A.resource_no AND C.lot = A.LOT_NO
    INNER JOIN [sea_mfg].[dbo].[resource] AS D WITH (NOLOCK)
    ON C.workcenter = D.resource_no
    INNER JOIN [sea_mfg].[dbo].[demand_mstr_ext] AS E WITH (NOLOCK)
    ON A.RESOURCE_NO = E.order_no AND A.LOT_NO = E.lot
    LEFT OUTER JOIN [sea_mfg].[dbo].[md_mst] AS F WITH (NOLOCK)
    ON E.code_md = F.code_md 
    LEFT OUTER JOIN [HS_MES].[dbo].[MATERIALCOST_PROCESS] AS G
    ON G.PROCESS_ID = '주조'
    INNER JOIN HS_MES.dbo.MATERIALCOST_EQUIPMENT AS H
    ON H.EQUIPMENT_ID = '설비850TON'
    LEFT OUTER JOIN [HS_MES].[dbo].[ELECTRIC_USE] AS I
    ON A.RESOURCE_NO = I.RESOURCE_NO
    WHERE CONVERT(DECIMAL(18,2), ELECTRICAL_ENERGY) < 100

    GROUP BY A.REG_DATE, A.[RESOURCE_NO], F.cavity, J.QTY_COMPLETE, 
             J.WORK_TIME, H.UNIT_PRICE_PER_PYEONG, H.EQUIPMENT_COST, 
             H.EQUIPMENT_USE_YEAR, H.EQUIPMENT_OPERATION_DAY, 
             H.EQUIPMENT_OPERATION
) AS A
GROUP BY RESOURCE_NO, 순서, 구분, [YEAR], [MONTH]




)	
AS SourceTable
PIVOT
(
    SUM(D_VALUE) -- 필요에 따라 AVG, MAX, MIN 등 다른 집계 함수를 사용할 수 있습니다.
    FOR MONTH IN ([1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12])
) AS PivotTable


WHERE RESOURCE_NO = '{order_mst_id}'
ORDER BY 
    순서";
                    StringBuilder sb = new StringBuilder();
                    //Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                    string sql = str;
                    DataTable _DataTable = new CoreBusiness().SELECT(sql);

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        //sheet.Cells[2, 2].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "거래처").ToString()); //거래처
                        int rowIndex_성능가동률 = _DataTable.AsEnumerable().FirstOrDefault(row => row["순서"].ToString() == "12")?.Field<int>("IndexColumn") ?? -1;
                        int rowIndex_직접노무비 = _DataTable.AsEnumerable().FirstOrDefault(row => row["순서"].ToString() == "13")?.Field<int>("IndexColumn") ?? -1;
                        int rowIndex_종합가동률 = _DataTable.AsEnumerable().FirstOrDefault(row => row["순서"].ToString() == "15")?.Field<int>("IndexColumn") ?? -1;

                        int rowIndex_설비감상비 = _DataTable.AsEnumerable().FirstOrDefault(row => row["순서"].ToString() == "16")?.Field<int>("IndexColumn") ?? -1;
                        int rowIndex_건물감상비 = _DataTable.AsEnumerable().FirstOrDefault(row => row["순서"].ToString() == "17")?.Field<int>("IndexColumn") ?? -1;
                        int rowIndex_수선비 = _DataTable.AsEnumerable().FirstOrDefault(row => row["순서"].ToString() == "18")?.Field<int>("IndexColumn") ?? -1;
                        int rowIndex_전력비 = _DataTable.AsEnumerable().FirstOrDefault(row => row["순서"].ToString() == "19")?.Field<int>("IndexColumn") ?? -1;
                        int rowIndex_간접경비 = _DataTable.AsEnumerable().FirstOrDefault(row => row["순서"].ToString() == "20")?.Field<int>("IndexColumn") ?? -1;
                        int rowIndex_재료비 = _DataTable.AsEnumerable().FirstOrDefault(row => row["순서"].ToString() == "21")?.Field<int>("IndexColumn") ?? -1;


                        sheet.Cells[5, 3].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "품번").ToString()); //품번
                        sheet.Cells[6, 3].SetValueFromText(order_mst_NAME); //품명
                        sheet.Cells[7, 3].SetValueFromText(_DataTable.Rows[i]["순서"].ToString() == "31" ? _DataTable.Rows[i]["구분"].ToString() : ""); 
                        //원재료
                        for (int num = 1; num <= 12; num++)
                        {
                            if (rowIndex_성능가동률 != -1)  // 해당 row가 존재하면
                                sheet.Cells[11, num + 4].SetValueFromText(_DataTable.Rows[rowIndex_성능가동률][$"{num}월"].ToString());//성능가동률
                            if (rowIndex_직접노무비 != -1)  // 해당 row가 존재하면
                                //sheet.Cells[7, num + 5].SetValueFromText(_DataTable.Rows[i]["구분"].ToString() == "재료비" ? _DataTable.Rows[i]["1월"].ToString() : "-"); //1월
                                sheet.Cells[12, num + 4].SetValueFromText(_DataTable.Rows[rowIndex_직접노무비][$"{num}월"].ToString());  //직접노무비
                            if (rowIndex_종합가동률 != -1)  // 해당 row가 존재하면
                                sheet.Cells[13, num + 4].SetValueFromText(_DataTable.Rows[rowIndex_종합가동률][$"{num}월"].ToString()); //종합가동률
                            if (rowIndex_설비감상비 != -1)  // 해당 row가 존재하면

                                sheet.Cells[14, num + 4].SetValueFromText(_DataTable.Rows[rowIndex_설비감상비][$"{num}월"].ToString()); //설비감상비
                            if (rowIndex_건물감상비 != -1)  // 해당 row가 존재하면

                                sheet.Cells[15, num + 4].SetValueFromText(_DataTable.Rows[rowIndex_건물감상비][$"{num}월"].ToString()); //건물감상비
                            if (rowIndex_수선비 != -1)  // 해당 row가 존재하면

                                sheet.Cells[16, num + 4].SetValueFromText(_DataTable.Rows[rowIndex_수선비][$"{num}월"].ToString()); //수선비
                            if (rowIndex_전력비 != -1)  // 해당 row가 존재하면

                                sheet.Cells[17, num + 4].SetValueFromText(_DataTable.Rows[rowIndex_전력비][$"{num}월"].ToString());  //전력비

                            if (rowIndex_간접경비 != -1)  // 해당 row가 존재하면
                                sheet.Cells[18, num + 4].SetValueFromText(_DataTable.Rows[rowIndex_간접경비][$"{num}월"].ToString());//간접경비
                            if (rowIndex_재료비 != -1)  // 해당 row가 존재하면
                                sheet.Cells[19, num + 4].SetValueFromText(_DataTable.Rows[rowIndex_재료비][$"{num}월"].ToString()); //원재료);
                        }

                        //sheet.Cells[7, 18].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "평균").ToString()); //평균
                        //sheet.Cells[7, 19].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "비고").ToString()); //비고

                        //sheet.Cells[11, 20].SetValueFromText(_DataTable.Rows[12]["6월"].ToString()); //성능가동율
                        //sheet.Cells[12, 20].SetValueFromText(_DataTable.Rows[14]["6월"].ToString()); //비고
                        //sheet.Cells[13, 20].SetValueFromText(_DataTable.Rows[15]["6월"].ToString()); //비고
                        //sheet.Cells[14, 20].SetValueFromText(_DataTable.Rows[13]["6월"].ToString()); //종합가동률
                        //sheet.Cells[15, 20].SetValueFromText(_DataTable.Rows[16]["6월"].ToString()); //설비
                        //sheet.Cells[16, 20].SetValueFromText(_DataTable.Rows[17]["6월"].ToString()); //건물
                        //sheet.Cells[17, 20].SetValueFromText(_DataTable.Rows[18]["6월"].ToString()); //수선
                        //sheet.Cells[18, 20].SetValueFromText(_DataTable.Rows[19]["6월"].ToString()); //수선
                        //sheet.Cells[19, 20].SetValueFromText(_DataTable.Rows[20]["6월"].ToString()); //수선
                        //sheet.Cells[20, 20].SetValueFromText(_DataTable.Rows[21]["6월"].ToString()); //수선

                        //sheet.Cells[i + 14, 16].SetValueFromText(_DataTable.Rows[i]["D.OUT_CODE"].ToString()); //품목번호
                        //sheet.Cells[i + 14, 17].SetValueFromText(_DataTable.Rows[i]["D.NAME"].ToString()); //품목
                        //sheet.Cells[i + 14, 18].SetValueFromText(_DataTable.Rows[i]["D.STANDARD"].ToString()); //사이즈
                        //sheet.Cells[i + 14, 19].SetValueFromText(_DataTable.Rows[i]["D.UNIT"].ToString()); //단위
                        //sheet.Cells[i + 14, 20].SetValueFromText(_DataTable.Rows[i]["J.OUT_QTY"].ToString()); //수량
                        //sheet.Cells[i + 14, 21].SetValueFromText(_DataTable.Rows[i]["A.STOCK_MST_PRICE"].ToString()); //단가
                    }

                    using (DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem())
                    {
                        using (DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(printingSystem))
                        {
                            DevExpress.Spreadsheet.IWorkbook wb = null;
                            wb = sc.Document;
                            link.Component = wb;
                            link.CreateDocument();
                            link.ShowPreviewDialog();
                        }
                    }

                }

                else if (e.EditingControl.Text == "작업지시명세서출력")
                {
                    DevExpress.XtraSpreadsheet.SpreadsheetControl sc = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                    sc.LoadDocument(Application.StartupPath + $"\\작업지시명세서.xlsx", DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                    DevExpress.Spreadsheet.Worksheet sheet = sc.Document.Worksheets[0];

                    string str = $@"select 
* from COST_BASE_MST
where RESOURCE_NO = '{pfpSpread.Sheets[0].GetValue(e.Row, "품번").ToString()}'
";

                    StringBuilder sb = new StringBuilder();
                    //Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                    string sql = str;
                    DataTable _DataTable = new CoreBusiness().SELECT(sql);

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        //sheet.Cells[3, 5].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "품번").ToString() + "  " + pfpSpread.Sheets[0].GetValue(e.Row, "LOT_NO").ToString() + "의 작업지시명세서"); //품목코드,품목명 주조1
                        //sheet.Cells[4, 8].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "품번").ToString() + "," + (pfpSpread.Sheets[0].GetValue(e.Row, "품명").ToString())); //품목코드,품목명 사상1
                        //sheet.Cells[4, 11].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "품번").ToString() + "," + (pfpSpread.Sheets[0].GetValue(e.Row, "품명").ToString())); //품목코드,품목명 가공1
                        //sheet.Cells[4, 14].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "품번").ToString() + "," + (pfpSpread.Sheets[0].GetValue(e.Row, "품명").ToString())); //품목코드,품목명 바렐 세척 검사
                        //sheet.Cells[4, 17].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "품번").ToString() + "," + (pfpSpread.Sheets[0].GetValue(e.Row, "품명").ToString())); //품목코드,품목명 조립검사

                        sheet.Cells[6, 5].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "주조1(기본)").ToString()); //기본 주조1
                        sheet.Cells[6, 6].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "주조1(BEST)").ToString()); //BEST 주조1

                        //sheet.Cells[9, 5].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "재료비").ToString()); //재료비
                        //sheet.Cells[9, 6].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "재료비").ToString()); //best재료비

                        sheet.Cells[6, 8].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "사상1(기본)").ToString()); //기본 사상1
                        sheet.Cells[6, 9].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "사상1(BEST)").ToString()); //BEST 사상1

                        sheet.Cells[6, 11].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "가공1(기본)").ToString()); //기본 가공1
                        sheet.Cells[6, 12].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "가공1(BEST)").ToString()); //BEST 가공1

                        sheet.Cells[6, 14].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "바렐세척검사(기본)").ToString()); //기본 바렐세척검사
                        sheet.Cells[6, 15].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "바렐세척검사(BEST)").ToString()); //BEST 바렐세척검사

                        sheet.Cells[6, 17].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "조립검사(기본)").ToString()); //기본 조립검사
                        sheet.Cells[6, 18].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "조립검사(BEST)").ToString()); //BEST 조립검사

                        sheet.Cells[11, 5].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "성능가동률").ToString()); //성능가동율
                        sheet.Cells[12, 5].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "직접노무비").ToString()); //직접노무비
                        sheet.Cells[13, 5].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "간접노무비").ToString()); //간접노무비
                        sheet.Cells[14, 5].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "종합가동률").ToString()); //종합가동율
                        sheet.Cells[15, 5].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "설비감상비").ToString()); //설비감상비
                        sheet.Cells[16, 5].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "건물감상비").ToString()); //건물감상비
                        sheet.Cells[17, 5].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "수선비").ToString()); //수선비
                        sheet.Cells[18, 5].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "전체전력비").ToString()); //전력비
                        sheet.Cells[19, 5].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "간접경비").ToString()); //간접경비
                                                                                                                     // sheet.Cells[21, 5].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "공정비").ToString()); //공정비

                        sheet.Cells[11, 6].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "성능가동률_BEST").ToString()); //성능가동율
                        sheet.Cells[12, 6].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "직접노무비_BEST").ToString()); //직접노무비
                        sheet.Cells[13, 6].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "간접노무비_BEST").ToString()); //간접노무비
                        sheet.Cells[14, 6].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "종합가동률_BEST").ToString()); //종합가동율
                        sheet.Cells[15, 6].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "설비감상비_BEST").ToString()); //설비감상비
                        sheet.Cells[16, 6].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "건물감상비_BEST").ToString()); //건물감상비
                        sheet.Cells[17, 6].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "수선비_BEST").ToString()); //수선비
                        sheet.Cells[18, 6].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "전체전력비_BEST").ToString()); //전력비
                        sheet.Cells[19, 6].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "간접경비_BEST").ToString()); //간접경비
                                                                                                                          //  sheet.Cells[21, 6].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "공정비").ToString()); //공정비

                        //sheet.Cells[22, 3].SetValueFromText(_DataTable.Rows[0]["MATERIAL_M_COST"].ToString()); //재료관리비
                        //sheet.Cells[23, 3].SetValueFromText(_DataTable.Rows[0]["OUT_M_COST"].ToString()); //외주관리비
                        //sheet.Cells[24, 3].SetValueFromText(_DataTable.Rows[0]["GENERAL_M_COST"].ToString()); //일반관리비

                        sheet.Cells[23, 5].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "재료관리비").ToString()); //재료관리비
                        sheet.Cells[24, 5].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "외주관리비").ToString()); //외주관리비
                        sheet.Cells[25, 5].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "일반관리비").ToString()); //일반관리비

                        sheet.Cells[23, 6].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "재료관리비").ToString()); //재료관리비
                        sheet.Cells[24, 6].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "외주관리비").ToString()); //외주관리비
                        sheet.Cells[25, 6].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "일반관리비").ToString()); //일반관리비

                        sheet.Cells[7, 3].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "QTY_COMPLETE").ToString()); //사용량
                        sheet.Cells[6, 3].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "원재료").ToString()); //원재료

                        sheet.Cells[0, 2].SetValueFromText(pfpSpread.Sheets[0].GetValue(e.Row, "품번").ToString() + "  " + pfpSpread.Sheets[0].GetValue(e.Row, "LOT_NO").ToString() + "의 작업지시명세서"); //사용량

                        //sheet.Cells[i + 14, 16].SetValueFromText(_DataTable.Rows[i]["D.OUT_CODE"].ToString()); //품목번호
                        //sheet.Cells[i + 14, 17].SetValueFromText(_DataTable.Rows[i]["D.NAME"].ToString()); //품목
                        //sheet.Cells[i + 14, 18].SetValueFromText(_DataTable.Rows[i]["D.STANDARD"].ToString()); //사이즈
                        //sheet.Cells[i + 14, 19].SetValueFromText(_DataTable.Rows[i]["D.UNIT"].ToString()); //단위
                        //sheet.Cells[i + 14, 20].SetValueFromText(_DataTable.Rows[i]["J.OUT_QTY"].ToString()); //수량
                        //sheet.Cells[i + 14, 21].SetValueFromText(_DataTable.Rows[i]["A.STOCK_MST_PRICE"].ToString()); //단가
                    }

                    using (DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem())
                    {
                        using (DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(printingSystem))
                        {
                            DevExpress.Spreadsheet.IWorkbook wb = null;
                            wb = sc.Document;
                            link.Component = wb;
                            link.CreateDocument();
                            link.ShowPreviewDialog();
                        }
                    }

                }

                else if (e.EditingControl.Text == "BOM")
                {
                    if (pfpSpread != null && pfpSpread.Sheets[0].Columns[e.Column].CellType != null)
                    {
                        if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(ButtonCellType))
                        {
                            for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                            {
                                if (pfpSpread.Sheets[0].Columns[i].Tag.ToString().Contains("STOCK_MST_ID"))
                                {
                                    if (pfpSpread.Sheets[0].GetValue(e.Row, i) != null)
                                    {
                                        string STOCK_MST_ID = pfpSpread.Sheets[0].GetValue(e.Row, i).ToString();

                                        BaseBomPopupBox baseBomPopupBox = new BaseBomPopupBox(STOCK_MST_ID);
                                        baseBomPopupBox.Show();
                                        return;
                                    }

                                }
                            }

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

            int idx = pfpSpread.Sheets[0].ActiveRowIndex;

            for (int i = 0; i < pfpSpread.ActiveSheet.RowCount; i++)
            {

                pfpSpread.ActiveSheet.Rows[i].ForeColor = Color.Black;
                pfpSpread.ActiveSheet.Rows[i].Font = new Font("맑은 고딕", 9, FontStyle.Regular);
            }

            pfpSpread.ActiveSheet.Rows[idx].ForeColor = Color.DarkBlue;
            pfpSpread.ActiveSheet.Rows[idx].Font = new Font("맑은 고딕", 10, FontStyle.Bold);

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
                xFpSpread pfpSpread = sender as xFpSpread;

                //for (int i = 0; i < pfpSpread.Sheets[0].Columns.Count; i++)
                //{
                //    MessageBox.Show(pfpSpread.Sheets[0].Columns[i].Label);
                //}
                new MenuSave_Business().MenuSave_A10(pfpSpread);
            }
            catch (Exception err)
            {

            }

        }

        public static void initializeSpread(DataTable dt, xFpSpread pfpSpread, string name, string user_account)
        {
            try
            {  // fpMain ---------------------------------------------------------------------------------------------------------------------------------------------------
               // 콤보가 있을때 반드시 설정
                FpSpreadManager.pConnectionString = ConfigrationManager.ConnectionString;

                // 스프레드 기본설정
                FpSpreadManager.pOperationType = OperationMode.Normal; // 읽기전용설정
                FpSpreadManager.SpreadSetStyle(pfpSpread);                 // 스프레드 전체설정      
                FpSpreadManager.SpreadSetSheetStyle(pfpSpread, 0);         // 스프레드 쉬트설정

                // 스프레드 쉬트 설정


                pfpSpread._menu_name = name;
                pfpSpread._user_account = user_account;

                string pESSENTIAL_YN = string.Empty;  // 헤더명
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
                string pSAVE_YN = "Y";                   // 순번(사용은 안하고 참고용으로만)
                try
                {

                    pfpSpread.Font = new Font("맑은 고딕", 9);

                    pfpSpread.Sheets[0].ColumnCount = 0;
                    pfpSpread.Sheets[0].ColumnCount = dt.Rows.Count;

                    pfpSpread.Sheets[0].AutoCalculation = false;
                    //pfpSpread.Sheets[0].AutoUpdate = false;

                    pfpSpread.SubEditorOpening -= new SubEditorOpeningEventHandler(pfpSpread_SubEditorOpening);
                    pfpSpread.SubEditorOpening += new SubEditorOpeningEventHandler(pfpSpread_SubEditorOpening);

                    pfpSpread.ClipboardPasting -= new ClipboardPastingEventHandler(pfpSpread_ClipboardPasting);
                    pfpSpread.ClipboardPasting += new ClipboardPastingEventHandler(pfpSpread_ClipboardPasting);

                    //pfpSpread.CellClick -= new CellClickEventHandler(pfpSpread_CellClick);
                    //pfpSpread.CellClick += new CellClickEventHandler(pfpSpread_CellClick);


                    pfpSpread.CellDoubleClick -= new CellClickEventHandler(pfpSpread_CellClick);
                    pfpSpread.CellDoubleClick += new CellClickEventHandler(pfpSpread_CellClick);

                    pfpSpread._EditorNotifyEventHandler -= new EditorNotifyEventHandler(pfpSpread_ButtonClicked);
                    pfpSpread._EditorNotifyEventHandler += new EditorNotifyEventHandler(pfpSpread_ButtonClicked);

                    pfpSpread.AutoSortedColumn -= new AutoSortedColumnEventHandler(fpSpread1_AutoSortedColumn);
                    pfpSpread.AutoSortedColumn += new AutoSortedColumnEventHandler(fpSpread1_AutoSortedColumn);


                    pfpSpread._SelectionChangedEventHandler -= new SelectionChangedEventHandler(pfpSpread_SelectionChanged);
                    pfpSpread._SelectionChangedEventHandler += new SelectionChangedEventHandler(pfpSpread_SelectionChanged);

                    pfpSpread.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(pfpSpread_ColumnWidthChanged);
                    pfpSpread.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(pfpSpread_ColumnWidthChanged);

                    pfpSpread.ColumnDragMoveCompleted -= new FarPoint.Win.Spread.DragMoveCompletedEventHandler(pfpSpread_ColumnDragMoveCompleted);
                    pfpSpread.ColumnDragMoveCompleted += new FarPoint.Win.Spread.DragMoveCompletedEventHandler(pfpSpread_ColumnDragMoveCompleted);



                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        pESSENTIAL_YN = dt.Rows[i]["ESSENTIAL_YN"].ToString().Trim();
                        pColumnKey = dt.Rows[i]["COLUMNKEY"].ToString().Trim().ToUpper();
                        pHeaderName = dt.Rows[i]["HEADERNAME"].ToString().Trim();
                        pWidth = dt.Rows[i]["WIDTH"].ToString().Trim();
                        pVisible = dt.Rows[i]["VISIBLE"].ToString().Trim();
                        pLocked = dt.Rows[i]["LOCKED"].ToString().Trim();
                        pAlign = dt.Rows[i]["ALIGN"].ToString().Trim();
                        pCellType = dt.Rows[i]["CELLTYPE"].ToString().Trim();
                        pLength = dt.Rows[i]["LENGTH"].ToString().Trim();
                        pPoint = dt.Rows[i]["POINT"].ToString().Trim();
                        pCodeType = dt.Rows[i]["CODETYPE"].ToString().Trim();
                        pCodeName = dt.Rows[i]["CODENAME"].ToString().Trim();
                        pSeq = dt.Rows[i]["SEQ"].ToString().Trim();

                        for (int x = 0; x < dt.Columns.Count; x++)
                        {
                            if (dt.Columns[x].ColumnName == "SAVE_YN")
                            {
                                pSAVE_YN = dt.Rows[i]["SAVE_YN"].ToString().Trim();
                            }
                        }
                        pfpSpread.Sheets[0].Columns[i].VerticalAlignment = CellVerticalAlignment.Center;   // 세로정렬 : 가운데
                        pfpSpread.Sheets[0].Columns[i].Label = pHeaderName;
                        pfpSpread.Sheets[0].Columns[i].Width = int.Parse(pWidth);
                        pfpSpread.Sheets[0].Columns[i].Tag = pColumnKey;
                        pfpSpread.Sheets[0].ColumnHeader.Columns[i].BackColor = Color.FromArgb(191, 207, 221);
                        pfpSpread.Sheets[0].Columns[i].AllowAutoSort = true;
                        pfpSpread.Sheets[0].GrayAreaBackColor = Color.FromArgb(191, 207, 221);
                        pfpSpread.Sheets[0].Columns[i].ParentStyleName = pSAVE_YN;
                        // 보임,숨김설정
                        if (pVisible == "Y")
                        {
                            pfpSpread.Sheets[0].Columns[i].Visible = true;
                        }
                        else
                        {
                            pfpSpread.Sheets[0].Columns[i].Visible = false;
                        }

                        // 읽기전용,수정가능설정
                        switch (pLocked)
                        {
                            case "Y":  // 읽기전용
                                pfpSpread.Sheets[0].Columns[i].Locked = true;
                                pfpSpread.Sheets[0].Columns[i].BackColor = Color.FromArgb(240, 240, 236);
                                break;
                            case "N":  // 수정가능
                                pfpSpread.Sheets[0].Columns[i].Locked = false;
                                pfpSpread.Sheets[0].Columns[i].BackColor = Color.FromArgb(253, 253, 150);

                                break;
                            case "2":  // 읽기전용이지만 색상은 락색상 적용안함
                                pfpSpread.Sheets[0].Columns[i].Locked = true;
                                pfpSpread.Sheets[0].Columns[i].BackColor = Color.FromArgb(240, 240, 236);
                                break;
                            default:
                                break;
                        }
                        if (pESSENTIAL_YN == "Y")
                        {
                            pfpSpread.Sheets[0].ColumnHeader.Columns[i].Font = new Font("맑은 고딕", 9, FontStyle.Bold);
                            pfpSpread.Sheets[0].ColumnHeader.Columns[i].ForeColor = Color.FromArgb(82, 60, 216);
                        }

                        // 정렬방법
                        switch (pAlign)
                        {
                            case "왼쪽": // 왼쪽
                                pfpSpread.Sheets[0].Columns[i].HorizontalAlignment = CellHorizontalAlignment.Left;
                                pfpSpread.Sheets[0].Columns[i].CellPadding.Left = 3;
                                break;
                            case "중앙": // 중앙
                                pfpSpread.Sheets[0].Columns[i].HorizontalAlignment = CellHorizontalAlignment.Center;
                                break;
                            case "오른쪽": // 오른쪽
                                pfpSpread.Sheets[0].Columns[i].HorizontalAlignment = CellHorizontalAlignment.Right;
                                pfpSpread.Sheets[0].Columns[i].CellPadding.Right = 3;
                                break;
                            default:
                                pfpSpread.Sheets[0].Columns[i].HorizontalAlignment = CellHorizontalAlignment.General;
                                break;
                        }
                        // 셀타입설정 (다른것보다 우선해야함)
                        switch (pCellType)
                        {
                            case "버튼":  // 버튼
                                SpreadColumnAddButton(pfpSpread, 0, i, pHeaderName, pCodeType, pCodeName);
                                break;
                            case "체크박스":  // 체크박스
                                SpreadColumnAddCheckBox(pfpSpread, 0, i, pCodeName);
                                break;
                            case "Display체크박스":  // 체크박스
                                SpreadColumnAddDisplayCheckBox(pfpSpread, 0, i, pCodeName);
                                break;
                            case "콤보박스":  // 콤보박스
                                SpreadColumnAddComboBox(pfpSpread, 0, i, pLength, pCodeType, pCodeName, "", pESSENTIAL_YN);
                                break;
                            case "이미지":  // 이미지
                                SpreadColumnAddImage(pfpSpread, 0, i);
                                break;
                            case "파일":  // 이미지
                                SpreadColumnAddFile(pfpSpread, 0, i, pHeaderName);
                                break;
                            case "날짜시간":  // 날짜시간
                                SpreadColumnAddDateTime(pfpSpread, 0, i, pCodeType);
                                break;
                            case "날짜":  // 날짜시간
                                SpreadColumnAddDate(pfpSpread, 0, i, pCodeName);
                                break;
                            case "마스크":  // 마스크
                                SpreadColumnAddMask(pfpSpread, 0, i, pCodeName);
                                break;
                            case "숫자":  // 숫자
                                SpreadColumnAddNumber(pfpSpread, 0, i, pPoint, pCodeType);
                                break;
                            case "Display숫자":  // 숫자
                                SpreadColumnAddDisplayNumber(pfpSpread, 0, i, pPoint, pCodeType);
                                break;
                            case "텍스트":  // 텍스트
                                SpreadColumnAddText(pfpSpread, 0, i, pLength);
                                break;
                            case "FIND":  // 텍스트
                                SpreadColumnAddFIND(pfpSpread, 0, i, pPoint, pCodeType, "", pESSENTIAL_YN);
                                break;
                            case "버튼2":  // 텍스트
                                SpreadColumnAddButton2(pfpSpread, 0, i, pHeaderName, pCodeType);
                                break;
                            case "Display텍스트":  // 텍스트
                                SpreadColumnAddDisplayText(pfpSpread, 0, i, pLength);
                                break;
                            case "Display버튼텍스트":  // 텍스트
                                SpreadColumnAddTextButtonCellType(pfpSpread, 0, i, pLength, pCodeType);
                                break;

                            default:
                                break;
                        }

                    }

                }
                catch (Exception pException)
                {
                    throw new ExceptionManager(null, "SpreadSetHeader", pException);
                }

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
            finally
            {
                new utility().ContextMenuStrip_Setting(pfpSpread);
                //ContextMenuStrip_Setting(pfpSpread);
                pfpSpread.Update();
            }
        }



        #endregion ○스프레드 이벤트

        private static void CoreInitializeControl(DataTable dt, Panel panel, MenuSettingEntity _MenuSettingEntity)
        {
            try
            {
                panel.Controls.Clear();

                if (_MenuSettingEntity != null)
                {
                    panel.Name = _MenuSettingEntity.BASE_TABLE.Split('/')[0];
                    panel.Tag = _MenuSettingEntity.BASE_WHERE;
                }

                int x = 5;
                int y = 5;
                int w = 310;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["SEARCH_YN"].ToString() == "Y")
                    {
                        switch (dt.Rows[i]["CELLTYPE"].ToString())
                        {
                            case "텍스트":
                            case "Display텍스트":
                            case "Display버튼텍스트":
                                Base_textbox base_Textbox = new Base_textbox();
                                base_Textbox.Name = dt.Rows[i]["COLUMNKEY"].ToString();
                                base_Textbox.SearchName = dt.Rows[i]["HEADERNAME"].ToString();
                                if (x + w >= panel.Size.Width)
                                {
                                    x = 5;
                                    y += 30;
                                    base_Textbox.Location = new Point(x, y);
                                }
                                else
                                {
                                    base_Textbox.Location = new Point(x, y);
                                }
                                x += w;
                                panel.Controls.Add(base_Textbox);
                                break;
                            case "콤보박스":
                                bool all_yn = true;

                                if (dt.Rows[i]["COLUMNKEY"].ToString().Contains("USE_YN"))
                                {
                                    all_yn = false;
                                }

                                Base_ComboBox base_ComboBox = new Base_ComboBox(dt.Rows[i], all_yn);
                                base_ComboBox.Name = dt.Rows[i]["COLUMNKEY"].ToString();
                                base_ComboBox.SearchName = dt.Rows[i]["HEADERNAME"].ToString();
                                if (x + w >= panel.Size.Width)
                                {
                                    x = 5;
                                    y += 30;
                                    base_ComboBox.Location = new Point(x, y);
                                }
                                else
                                {
                                    base_ComboBox.Location = new Point(x, y);
                                }
                                x += w;
                                if (base_ComboBox.Name.Contains("USE_YN"))
                                {
                                    base_ComboBox.Visible = false;
                                }
                                panel.Controls.Add(base_ComboBox);
                                break;

                            case "날짜시간":
                                Base_FromtoDateTime base_datetime = new Base_FromtoDateTime();
                                base_datetime.Name = dt.Rows[i]["COLUMNKEY"].ToString();
                                base_datetime.SearchName = dt.Rows[i]["HEADERNAME"].ToString();
                                if (x + w >= panel.Size.Width)
                                {
                                    x = 5;
                                    y += 30;
                                    base_datetime.Location = new Point(x, y);
                                }
                                else
                                {
                                    base_datetime.Location = new Point(x, y);
                                }
                                x += w;
                                panel.Controls.Add(base_datetime);
                                break;

                            case "버튼":
                                DataTable dt1 = new CoreBusiness().Spread_ComboBox(dt.Rows[i]["CODETYPE"].ToString(), dt.Rows[i]["CODENAME"].ToString(), "");
                                Base_Searchbox base_Searchbox = new Base_Searchbox(dt1);
                                base_Searchbox.Name = dt.Rows[i]["COLUMNKEY"].ToString();
                                base_Searchbox.SearchName = dt.Rows[i]["HEADERNAME"].ToString();
                                if (x + w >= panel.Size.Width)
                                {
                                    x = 5;
                                    y += 30;
                                    base_Searchbox.Location = new Point(x, y);
                                }
                                else
                                {
                                    base_Searchbox.Location = new Point(x, y);
                                }
                                x += w;
                                panel.Controls.Add(base_Searchbox);
                                break;
                        }

                    }
                }
                PanelControl _pnHeader = panel.Parent.Parent as PanelControl;
                if (_pnHeader != null)
                {
                    if (panel.Controls.Count == 0)
                    {

                        _pnHeader.Size = new Size(1600, 0);
                    }
                    else
                    {
                        int cou = 0;

                        if (panel.Controls.Count <= 5)
                        {
                            cou = 60;
                        }
                        else if (panel.Controls.Count <= 10)
                        {
                            cou = 90;
                        }
                        else if (panel.Controls.Count <= 15)
                        {
                            cou = 120;
                        }
                        else if (panel.Controls.Count <= 20)
                        {
                            cou = 150;
                        }
                        _pnHeader.Size = new Size(1600, cou);
                    }
                }

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
            finally
            {

            }
        }
        public static void _AddItemButtonClicked(xFpSpread fpMain, string user_account)
        {
            try
            {


                FpSpreadManager.SpreadRowAdd(fpMain, 0);

                fpMain.Sheets[0].RowHeader.Cells[fpMain.Sheets[0].ActiveRowIndex, 0].Text = "입력";

                int su = fpMain.Sheets[0].ActiveRowIndex;

                while (fpMain.Sheets[0].RowHeader.Cells[su, 0].Text == "합계")
                {
                    su--;
                }
                for (int i = 0; i < fpMain.Sheets[0].ColumnCount; i++)
                {
                    if (fpMain.Sheets[0].Columns[i].CellType != null)
                    {
                        if (fpMain.Sheets[0].Columns[i].CellType.GetType() == typeof(NEW_ComboBoxCellType))
                        {
                            NEW_ComboBoxCellType pCellType = fpMain.Sheets[0].Columns[i].CellType as NEW_ComboBoxCellType;

                            if (pCellType.ItemData.Length != 0)
                            {
                                if (fpMain.Sheets[0].Columns[i].Tag.ToString() == "COMPLETE_YN")
                                {
                                    fpMain.Sheets[0].SetValue(su, fpMain.Sheets[0].Columns[i].Tag.ToString(), pCellType.ItemData[1]);
                                }
                                else
                                {
                                    fpMain.Sheets[0].SetValue(su, fpMain.Sheets[0].Columns[i].Tag.ToString(), pCellType.ItemData[0]);

                                }
                            }
                        }

                        if (fpMain.Sheets[0].Columns[i].CellType.GetType() == typeof(MYComboBoxCellType))
                        {
                            MYComboBoxCellType pCellType = fpMain.Sheets[0].Columns[i].CellType as MYComboBoxCellType;

                            if (pCellType.ItemData.Length != 0)
                            {
                                fpMain.Sheets[0].SetValue(su, fpMain.Sheets[0].Columns[i].Tag.ToString(), pCellType.ItemData[0]);
                            }
                        }
                        if (fpMain.Sheets[0].Columns[i].CellType.GetType() == typeof(NumberCellType))
                        {
                            fpMain.Sheets[0].SetValue(su, fpMain.Sheets[0].Columns[i].Tag.ToString(), 0);
                        }
                        if (fpMain.Sheets[0].Columns[i].CellType.GetType() == typeof(DateTimeCellType))
                        {
                            fpMain.Sheets[0].SetValue(su, fpMain.Sheets[0].Columns[i].Tag.ToString(), DateTime.Now);
                        }
                        if (fpMain.Sheets[0].Columns[i].CellType.GetType() == typeof(ButtonCellType))
                        {
                            fpMain.Sheets[0].SetValue(su, fpMain.Sheets[0].Columns[i].Tag.ToString(), null);
                        }
                    }
                    if (fpMain.Sheets[0].Columns[i].Tag.ToString().Contains("COMPLETE_YN"))
                    {
                        fpMain.Sheets[0].SetValue(su, i, "N");
                    }
                    if (fpMain.Sheets[0].Columns[i].Tag.ToString().Contains("USE_YN"))
                    {
                        fpMain.Sheets[0].SetValue(su, i, "Y");
                    }
                    if (fpMain.Sheets[0].Columns[i].Tag.ToString().Contains("UP_USER"))
                    {
                        fpMain.Sheets[0].SetValue(su, i, user_account);
                    }
                    if (fpMain.Sheets[0].Columns[i].Tag.ToString().Contains("UP_DATE"))
                    {
                        fpMain.Sheets[0].SetValue(su, i, DateTime.Now);
                    }
                    if (fpMain.Sheets[0].Columns[i].Tag.ToString().Contains("REG_USER"))
                    {
                        fpMain.Sheets[0].SetValue(su, i, user_account);
                    }
                    if (fpMain.Sheets[0].Columns[i].Tag.ToString().Contains("REG_DATE"))
                    {
                        fpMain.Sheets[0].SetValue(su, i, DateTime.Now);
                    }
                }
                _AddItemSUM(fpMain);



            }
            catch (Exception err)
            {

            }
        }
        public static void _AddItemSUM(xFpSpread fpMain)
        {
            try
            {

                for (int x = 0; x < fpMain.Sheets[0].RowCount; x++)
                {
                    if (fpMain.Sheets[0].RowHeader.Cells[x, 0].Text == "합계")
                    {
                        FpSpreadManager.SpreadRowRemove(fpMain, 0, x);
                    }
                }

                bool ck = false;
                for (int i = 0; i < fpMain.Sheets[0].ColumnCount; i++)
                {
                    if (fpMain.Sheets[0].Columns[i].CellType != null && fpMain.Sheets[0].Columns[i].Visible)
                    {
                        if (fpMain.Sheets[0].Columns[i].CellType.GetType().ToString().Contains("NumberCellType"))
                        {
                            ck = true;
                        }
                    }

                }

                if (ck)
                {
                    FpSpreadManager.SpreadRowAdd(fpMain, 0);

                    fpMain.Sheets[0].RowHeader.Cells[fpMain.Sheets[0].ActiveRowIndex, 0].Text = "합계";

                    for (int i = 0; i < fpMain.Sheets[0].ColumnCount; i++)
                    {
                        //MessageBox.Show(fpMain.Sheets[0].Columns[i].Tag.ToString());
                        if (fpMain.Sheets[0].Columns[i].CellType != null && fpMain.Sheets[0].Columns[i].Visible)
                        {
                            if (fpMain.Sheets[0].Columns[i].CellType.GetType().ToString().Contains("NumberCellType"))
                            {
                                decimal sum = 0;

                                for (int x = 0; x < fpMain.Sheets[0].RowCount; x++)
                                {
                                    if (fpMain.Sheets[0].GetValue(x, i) != DBNull.Value)
                                    {
                                        if (fpMain.Sheets[0].GetValue(x, i) != null)
                                        {
                                            decimal sum1 = 0;
                                            if (decimal.TryParse(fpMain.Sheets[0].GetValue(x, i).ToString(), out sum1))
                                            {
                                                sum += Convert.ToDecimal(fpMain.Sheets[0].GetValue(x, i));
                                            }
                                        }
                                    }
                                }

                                fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, fpMain.Sheets[0].Columns[i].Tag.ToString(), sum);
                            }
                            else
                            {
                                //fpMain.Sheets[0].Rows[fpMain.Sheets[0].ActiveRowIndex].CellType = new TextCellType();
                                fpMain.Sheets[0].Cells[fpMain.Sheets[0].ActiveRowIndex, i].CellType = new TextCellType();

                            }
                        }

                    }


                    fpMain.Sheets[0].Rows[fpMain.Sheets[0].ActiveRowIndex].BackColor = Color.FromArgb(123, 171, 229);
                    //Color.FromArgb(123, 171, 229); Color.FromArgb(255, 255, 255);
                    //fpMain.Sheets[0].Rows[fpMain.Sheets[0].ActiveRowIndex].ForeColor = Color.White;
                    fpMain.Sheets[0].Rows[fpMain.Sheets[0].ActiveRowIndex].Locked = true;


                    //Color startColor = Color.Blue;
                    //Color endColor = Color.White;

                    //// 그라데이션을 적용할 로우의 인덱스 설정
                    //int row = fpMain.Sheets[0].ActiveRowIndex;

                    //// 그라데이션을 적용할 셀의 개수 설정
                    //int totalCells = fpMain.Sheets[0].Columns.Count;

                    //// 그라데이션을 적용합니다.
                    //for (int col = 0; col < totalCells; col++)
                    //{
                    //    // 그라데이션을 계산합니다.
                    //    double ratio = Math.Abs(col - (totalCells / 2.0)) / (totalCells / 2.0);
                    //    int red = (int)(startColor.R + ((endColor.R - startColor.R) * ratio));
                    //    int green = (int)(startColor.G + ((endColor.G - startColor.G) * ratio));
                    //    int blue = (int)(startColor.B + ((endColor.B - startColor.B) * ratio));
                    //    Color cellColor = Color.FromArgb(red, green, blue);
                    //    fpMain.Sheets[0].Cells[row, col].BackColor = cellColor;

                    //    //255, 228, 228, 255
                    //}

                    fpMain.Sheets[0].SetActiveCell(fpMain.Sheets[0].ActiveRow.Index - 1, fpMain.Sheets[0].ActiveColumn.Index, false);
                }
            }
            catch (Exception err)
            {

            }
        }
        public static void _AddItemAGV(xFpSpread fpMain)
        {
            try
            {

                for (int x = 0; x < fpMain.Sheets[0].RowCount; x++)
                {
                    if (fpMain.Sheets[0].RowHeader.Cells[x, 0].Text == "평균")
                    {
                        FpSpreadManager.SpreadRowRemove(fpMain, 0, x);
                    }
                }

                bool ck = false;
                for (int i = 0; i < fpMain.Sheets[0].ColumnCount; i++)
                {
                    if (fpMain.Sheets[0].Columns[i].CellType != null && fpMain.Sheets[0].Columns[i].Visible)
                    {
                        if (fpMain.Sheets[0].Columns[i].CellType.GetType().ToString().Contains("NumberCellType"))
                        {
                            ck = true;
                        }
                    }

                }

                if (ck)
                {
                    FpSpreadManager.SpreadRowAdd(fpMain, 0);

                    fpMain.Sheets[0].RowHeader.Cells[fpMain.Sheets[0].ActiveRowIndex, 0].Text = "평균";

                    for (int i = 0; i < fpMain.Sheets[0].ColumnCount; i++)
                    {
                        if (fpMain.Sheets[0].Columns[i].CellType != null && fpMain.Sheets[0].Columns[i].Visible)
                        {
                            if (fpMain.Sheets[0].Columns[i].CellType.GetType().ToString().Contains("NumberCellType"))
                            {
                                decimal sum = 0;

                                for (int x = 0; x < fpMain.Sheets[0].RowCount; x++)
                                {
                                    if (fpMain.Sheets[0].RowHeader.Cells[x, 0].Text != "합계")
                                    {
                                        if (fpMain.Sheets[0].GetValue(x, i) != DBNull.Value)
                                        {
                                            sum += Convert.ToDecimal(fpMain.Sheets[0].GetValue(x, i));
                                        }
                                    }
                                }

                                fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, fpMain.Sheets[0].Columns[i].Tag.ToString(), sum / (fpMain.Sheets[0].RowCount - 2));
                            }
                            else
                            {
                                //fpMain.Sheets[0].Rows[fpMain.Sheets[0].ActiveRowIndex].CellType = new TextCellType();
                                fpMain.Sheets[0].Cells[fpMain.Sheets[0].ActiveRowIndex, i].CellType = new TextCellType();

                            }
                        }

                    }


                    fpMain.Sheets[0].Rows[fpMain.Sheets[0].ActiveRowIndex].BackColor = Color.FromArgb(123, 171, 229);
                    //fpMain.Sheets[0].Rows[fpMain.Sheets[0].ActiveRowIndex].ForeColor = Color.White;
                    fpMain.Sheets[0].Rows[fpMain.Sheets[0].ActiveRowIndex].Locked = true;
                    fpMain.Sheets[0].SetActiveCell(fpMain.Sheets[0].Rows.Count - 2, 6, false);
                }
            }
            catch (Exception err)
            {

            }
        }
        public static bool _SaveButtonClicked(xFpSpread fpMain)
        {
            try
            {

                for (int r = 0; r < fpMain.Sheets[0].Rows.Count; r++)
                {
                    if (fpMain.Sheets[0].RowHeader.Cells[r, 0].Text != "합계" &&
                        fpMain.Sheets[0].RowHeader.Cells[r, 0].Text != "")
                    {

                        int y = 1;

                        for (int c = 0; c < fpMain.Sheets[0].Columns.Count; c++)
                        {
                            if (fpMain.Sheets[0].Columns[c].Visible)
                            {
                                if (fpMain.Sheets[0].Columns[c].CellType.GetType() == typeof(DateTimeCellType))
                                {
                                    DateTime dt = DateTime.Now;
                                    if (fpMain.Sheets[0].GetValue(r, c) != null)
                                    {
                                        string str = fpMain.Sheets[0].GetValue(r, c).ToString();
                                        if (!DateTime.TryParse(fpMain.Sheets[0].GetValue(r, c).ToString(), out dt))
                                        {
                                            if (str != "")
                                            {
                                                CustomMsg.ShowMessage($"입력 값이 잘못입력 되었습니다. {fpMain.Sheets[0].Columns[c].Label} 행{r + 1} , 열{y}");

                                                fpMain.Sheets[0].SetActiveCell(r, c);

                                                fpMain.Sheets[0].Cells[r, c].CanFocus = true;

                                                return false;
                                            }
                                        }
                                    }
                                }

                                if (fpMain.Sheets[0].Columns[c].CellType.GetType() == typeof(NumberCellType) ||
                                    fpMain.Sheets[0].Columns[c].CellType.GetType() == typeof(MYNumberCellType))

                                {

                                    if (fpMain.Sheets[0].GetValue(r, c) != null)
                                    {
                                        decimal dec = 0;
                                        if (!decimal.TryParse(fpMain.Sheets[0].GetValue(r, c).ToString(), out dec))
                                        {
                                            if (dec > 0)
                                            {
                                                CustomMsg.ShowMessage($"입력 값이 잘못입력 되었습니다. {fpMain.Sheets[0].Columns[c].Label} 행{r + 1} , 열{y}");

                                                fpMain.Sheets[0].SetActiveCell(r, c);

                                                fpMain.Sheets[0].Cells[r, c].CanFocus = true;

                                                return false;
                                            }
                                        }
                                    }
                                }

                                if (fpMain.Sheets[0].ColumnHeader.Columns[c].ForeColor == Color.FromArgb(82, 60, 216))
                                {
                                    if (fpMain.Sheets[0].GetValue(r, c) == null ||
                                        fpMain.Sheets[0].GetValue(r, c).ToString() == "" ||
                                        fpMain.Sheets[0].GetValue(r, c).ToString() == "0")

                                    {

                                        CustomMsg.ShowMessage($"필수 입력 값이 누락 되었습니다. {fpMain.Sheets[0].Columns[c].Label} 행{r + 1} , 열{y}");

                                        fpMain.Sheets[0].SetActiveCell(r, c);

                                        fpMain.Sheets[0].Cells[r, c].CanFocus = true;

                                        return false;
                                    }
                                }
                                y++;
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }
        public static Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }
        public static byte[] ImageToByteArray(string imagePath)
        {
            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader br = new BinaryReader(fs);
                return br.ReadBytes((int)fs.Length);
            }
        }
        public static void GET_WHERE(Panel _Panel, StringBuilder sql)
        {

            for (int i = 0; i < _Panel.Controls.Count; i++)
            {
                string _par = _Panel.Controls[i].Name;


                switch (_Panel.Controls[i].GetType().Name)
                {
                    case "Base_textbox":
                        Base_textbox base_Textbox = _Panel.Controls[i] as Base_textbox;
                        if (base_Textbox.SearchText.Length > 0)
                        {
                            sql.Append($" and {_par} like '%{base_Textbox.SearchText}%' ");
                            //sql1.SqlValue = $"%{base_Textbox.SearchText}%";
                        }
                        break;
                    case "Base_ComboBox":

                        Base_ComboBox base_ComboBox = _Panel.Controls[i] as Base_ComboBox;
                        if (base_ComboBox.SearchText.Length > 0)
                        {
                            sql.Append($" and {_par} = '{base_ComboBox.SearchText}' ");
                        }
                        break;

                    case "Base_FromtoDateTime":

                        Base_FromtoDateTime _FromtoDateTime = _Panel.Controls[i] as Base_FromtoDateTime;

                        sql.Append($@"and {_par} BETWEEN  '{_FromtoDateTime.StartValue.ToString("yyyy-MM-dd HH:mm")}' 
                                                       and '{_FromtoDateTime.EndValue.ToString("yyyy-MM-dd HH:mm")}' ");


                        break;
                    case "Base_Searchbox":

                        Base_Searchbox _Base_Searchbox = _Panel.Controls[i] as Base_Searchbox;

                        sql.Append($" and {_par} = '{_Base_Searchbox.SearchText}' ");


                        break;

                }



            }
        }
        public static void GET_WHERE_UNION(Panel _Panel, StringBuilder sql)
        {

            for (int i = 0; i < _Panel.Controls.Count; i++)
            {
                string _par = _Panel.Controls[i].Name;


                switch (_Panel.Controls[i].GetType().Name)
                {
                    case "Base_textbox":
                        Base_textbox base_Textbox = _Panel.Controls[i] as Base_textbox;
                        if (base_Textbox.SearchText.Length > 0)
                        {
                            sql.Append($" and [{_par}] like '%{base_Textbox.SearchText}%' ");
                            //sql1.SqlValue = $"%{base_Textbox.SearchText}%";
                        }
                        break;
                    case "Base_ComboBox":

                        Base_ComboBox base_ComboBox = _Panel.Controls[i] as Base_ComboBox;
                        if (base_ComboBox.SearchText.Length > 0)
                        {
                            sql.Append($" and [{_par}] = '{base_ComboBox.SearchText}' ");
                        }
                        break;

                    case "Base_FromtoDateTime":

                        Base_FromtoDateTime _FromtoDateTime = _Panel.Controls[i] as Base_FromtoDateTime;

                        sql.Append($@"and [{_par}] BETWEEN  '{_FromtoDateTime.StartValue.ToString("yyyy-MM-dd HH:mm")}' 
                                                       and '{_FromtoDateTime.EndValue.ToString("yyyy-MM-dd HH:mm")}' ");


                        break;

                }



            }
        }
        public static void InitializeControl(DataTable dt, xFpSpread spread, Form form, Panel _PAN_WHERE, MenuSettingEntity _pMenuSettingEntity)
        {
            try
            {
                //form.Size = new Size(1500, 514);

                Core.CoreInitializeControl(dt, _PAN_WHERE, _pMenuSettingEntity);

                spread.Sheets[0].Rows.Count = 0;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        public static void InitializeButton(MenuSettingEntity _pMenuSettingEntity, IMainForm MainForm)
        {
            try
            {
                MainFormButtonSetting _MainFormButtonSetting = new MainFormButtonSetting();

                DataTable _DataTable = new MenuButton_Business().MenuButton_Select(_pMenuSettingEntity.MENU_NO, MainForm.UserEntity.user_authority);
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    _MainFormButtonSetting.UseYNSearchButton = _DataTable.Rows[0]["menu_search"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNPrintButton = _DataTable.Rows[0]["menu_print"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNDeleteButton = _DataTable.Rows[0]["menu_delete"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNSaveButton = _DataTable.Rows[0]["menu_save"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNImportButton = _DataTable.Rows[0]["menu_import"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNExportButton = _DataTable.Rows[0]["menu_export"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNInitialButton = _DataTable.Rows[0]["menu_initialize"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNAddItemButton = _DataTable.Rows[0]["menu_newadd"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNFormCloseButton = true;
                }
                else
                {
                    _MainFormButtonSetting.UseYNSearchButton = false;
                    _MainFormButtonSetting.UseYNPrintButton = false;
                    _MainFormButtonSetting.UseYNDeleteButton = false;
                    _MainFormButtonSetting.UseYNSaveButton = false;
                    _MainFormButtonSetting.UseYNImportButton = false;
                    _MainFormButtonSetting.UseYNExportButton = false;
                    _MainFormButtonSetting.UseYNInitialButton = false;
                    _MainFormButtonSetting.UseYNAddItemButton = false;
                    _MainFormButtonSetting.UseYNFormCloseButton = true;
                }

                MainForm.SetButtonSetting(_MainFormButtonSetting);
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        public static void DisplayData_Set(DataTable _DataTable, xFpSpread spread)
        {
            if (_DataTable != null && _DataTable.Rows.Count > 0)
            {
                spread.Sheets[0].Rows.Count = 0;
                spread.Sheets[0].Visible = false;
                spread.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                for (int i = 0; i < _DataTable.Rows.Count; i++)
                {
                    foreach (DataColumn item in _DataTable.Columns)
                    {

                        spread.Sheets[0].SetValue(i, item.ColumnName.ToUpper(), _DataTable.Rows[i][item.ColumnName]);

                        if (item.ColumnName.Contains("COMPLETE_YN"))
                        {
                            switch (_DataTable.Rows[i][item.ColumnName].ToString())
                            {
                                case "Y":

                                    spread.Sheets[0].Rows[i].BackColor = Color.FromArgb(198, 239, 206);
                                    for (int x = 0; x < spread.Sheets[0].ColumnCount; x++)
                                    {
                                        if (spread.Sheets[0].Columns[x].CellType.GetType() == null)
                                        {
                                            return;
                                        }

                                        if (spread.Sheets[0].Columns[x].CellType.GetType() != typeof(ButtonCellType) &&
                                            spread.Sheets[0].Columns[x].CellType.GetType() != typeof(FileButtonCellType))
                                        {
                                            spread.Sheets[0].Cells[i, x].Locked = true;
                                        }

                                    }

                                    break;
                                case "W":
                                    spread.Sheets[0].Rows[i].BackColor = Color.LightBlue;
                                    for (int x = 0; x < spread.Sheets[0].ColumnCount; x++)
                                    {
                                        if (spread.Sheets[0].Columns[x].CellType.GetType() == null)
                                        {
                                            return;
                                        }
                                        if (spread.Sheets[0].Columns[x].CellType.GetType() != typeof(ButtonCellType) &&
                                              spread.Sheets[0].Columns[x].CellType.GetType() != typeof(FileButtonCellType))
                                        {
                                            spread.Sheets[0].Cells[i, x].Locked = true;
                                        }

                                    }
                                    break;
                            }
                        }
                    }


                }

                _AddItemSUM(spread);



                spread.Sheets[0].Visible = true;


            }
            else
            {
                spread.Sheets[0].Rows.Count = 0;
                if (spread.Name.Contains("fpMain"))
                {
                    CustomMsg.ShowMessage("조회 내역이 없습니다.");
                }

            }
        }
        public static List<Code_mst> GET_PRODUCT_TYPE()
        {
            List<Code_mst> list = new List<Code_mst>();
            try
            {
                string sql = @"select *
                                 from Code_Mst
                                where 1=1
                                  and code_Type IN('SD03', 'SD04') 
                                  and use_yn = 'Y'";

                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        Code_mst code = new Code_mst();
                        code.CODE = _DataTable.Rows[i]["code"].ToString();
                        code.CODE_NAME = _DataTable.Rows[i]["code_name"].ToString();
                        list.Add(code);
                    }

                }


            }
            catch (Exception)
            {


            }
            finally
            {

            }

            return list;
        }
        public static List<Code_mst> GET_PROCESS()
        {
            List<Code_mst> list = new List<Code_mst>();
            try
            {
                string sql = @"select *
                                 from PROCESS
                                where USE_YN = 'Y'";

                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        Code_mst code = new Code_mst();
                        code.CODE = _DataTable.Rows[i]["code"].ToString();
                        code.CODE_NAME = _DataTable.Rows[i]["code_name"].ToString();
                        list.Add(code);
                    }

                }


            }
            catch (Exception)
            {


            }
            finally
            {

            }

            return list;
        }
        public static void Log_API(SystemLogEntity _SystemLogEvent)
        {
            try
            {
                HttpClient _client = new HttpClient()
                {
                    BaseAddress = new Uri("https://log.smart-factory.kr")

                };

                _client.Timeout = TimeSpan.FromSeconds(1);

                SMRTFTRLOG_V1_1Entity entity = new SMRTFTRLOG_V1_1Entity();

                entity.crtfcKey = CoFAS.NEW.MES.Core.Properties.Settings.Default.crtfcKey;
                entity.logDt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                entity.useSe = _SystemLogEvent.event_log;
                entity.sysUser = _SystemLogEvent.user_account;
                entity.conectIp = _SystemLogEvent.user_ip;
                entity.dataUsgqty = "0";
                //GetByteArrayAsync
                //GetAsync
                var aa = _client.GetAsync($"/apisvc/sendLogDataJSON.do?logData={JsonConvert.SerializeObject(entity)}").Result;
            }
            catch (Exception)
            {

            }
        }

        public static void DisplayBOM_Set(string pSTOCK_MST_ID, TreeView pTreeView)
        {
            pTreeView.Font = new Font("맑은 고딕", 16, FontStyle.Bold);

            string sql = $@"
                            WITH  bom_cte AS (
                                -- Anchor member: 시작점 (최상위 부모)
                                SELECT id, STOCK_MST_ID, SUB_STOCK_MST_ID, PRODUCTION_QTY , CONSUME_QTY ,SEQ AS level
                                FROM bom
                                WHERE STOCK_MST_ID = SUB_STOCK_MST_ID
                            	AND STOCK_MST_ID = {pSTOCK_MST_ID}
                            
                                UNION ALL
                            
                                -- Recursive member: 재귀적으로 하위 구성품을 조회
                                SELECT b.id, b.STOCK_MST_ID, b.SUB_STOCK_MST_ID, b.PRODUCTION_QTY,  b.CONSUME_QTY ,a.level + 1
                                FROM bom_cte a
                                INNER JOIN bom b ON a.SUB_STOCK_MST_ID = b.STOCK_MST_ID 
                            	where b.STOCK_MST_ID != b.SUB_STOCK_MST_ID	
                            )
                            
                            -- CTE 결과를 출력
                           SELECT a.*
                                 ,c.OUT_CODE        as '파트코드'
                                 ,c.name            as '파트명'
                                 ,c.STANDARD        as '규격'
                                 ,d.code_name       as '타입'
                                 ,isnull(e.NAME,'') as '공정'
                                 FROM bom_cte a
                                 inner join STOCK_MST b on a.STOCK_MST_ID = b.id
                                 inner join STOCK_MST c on a.SUB_STOCK_MST_ID = c.id
                                 inner join Code_Mst d on d.code = c.TYPE 
                                  left join PROCESS e on c.PROCESS_ID = e.ID
                                 order by a.level";


            DataTable _DataTable = new CoreBusiness().SELECT(sql);

            List<BOMNode> list = new List<BOMNode>();

            for (int i = 0; i < _DataTable.Rows.Count; i++)
            {
                BOMNode bOMNode = new BOMNode(
                                         _DataTable.Rows[i]["파트코드"].ToString()
                        , Convert.ToInt32(_DataTable.Rows[i]["level"])

                        );
                bOMNode.상위 = _DataTable.Rows[i]["STOCK_MST_ID"].ToString();
                bOMNode.하위 = _DataTable.Rows[i]["SUB_STOCK_MST_ID"].ToString();
                bOMNode.소요량 = Convert.ToInt32(_DataTable.Rows[i]["CONSUME_QTY"]);

                bOMNode.파트코드 = _DataTable.Rows[i]["파트코드"].ToString();
                bOMNode.파트명 = _DataTable.Rows[i]["파트명"].ToString();
                bOMNode.규격 = _DataTable.Rows[i]["규격"].ToString();
                bOMNode.타입 = _DataTable.Rows[i]["타입"].ToString();
                bOMNode.공정 = _DataTable.Rows[i]["공정"].ToString();
                list.Add(bOMNode);
            }

            BOMNode bom = list.Find(x => x.Quantity == 0);

            BOM_SET(list, bom);

            PopulateTreeView(pTreeView, bom);

        }
        public static void BOM_SET(List<BOMNode> list, BOMNode bom)
        {
            foreach (BOMNode item in list)
            {
                if (item.상위 != item.하위)
                {
                    if (bom.하위 == item.상위)
                    {
                        bom.Children.Add(item);
                        BOM_SET(list, item);
                    }
                }
            }
        }
        public static void PopulateTreeView(TreeView treeView, BOMNode bomNode)
        {
            try
            {
                if (bomNode == null)
                {
                    return;
                }

                treeView.Nodes.Clear();
                TreeNode node = new TreeNode(
                $"{bomNode.파트코드} - {bomNode.공정}" + "(소모량: " + bomNode.소요량 + ")");
                //  TreeNode node = new TreeNode(bomNode.파트코드 + " (소모량: " + bomNode.소요량 + ")");

                //node.ImageKey = "icon"; // 이미지 키 설정
                //node.SelectedImageKey = "icon"; // 선택된 이미지 키 설
                node.Expand();
                foreach (var child in bomNode.Children)
                {
                    PopulateTreeView(node, child);
                }

                treeView.Nodes.Add(node);

            }
            catch (Exception err)
            {

            }
        }
        public static void PopulateTreeView(TreeNode treeNode, BOMNode bomNode)
        {

            //TreeNode node = new TreeNode(
            //   $"코드:{bomNode.파트코드} 명칭:{bomNode.파트명} 규격:{bomNode.규격} 타입:{bomNode.타입} 공정:{bomNode.공정}" + "(소모량: " + bomNode.소요량 + ")");
            TreeNode node = new TreeNode(
              $"{bomNode.파트코드} - {bomNode.공정}" + "(소모량: " + bomNode.소요량 + ")");

            //node.ImageKey = "icon"; // 이미지 키 설정
            //node.SelectedImageKey = "icon"; // 선택된 이미지 키 설
            node.Expand();
            foreach (var child in bomNode.Children)
            {
                PopulateTreeView(node, child);
            }
            treeNode.Nodes.Add(node);
        }
    }


    public class MYComboBoxCellType : ComboBoxCellType
    {
        public string pCodeType { get; set; }

        public string pFirst { get; set; }

        public string pSecond { get; set; }
    }
    public class NEW_ComboBoxCellType : ComboBoxCellType
    {
        public string pCodeType { get; set; }

        public string pFirst { get; set; }
    }

    public class MYNumberCellType : NumberCellType
    {
        public DataTable pDataTable { get; set; }

        public string pID { get; set; }

    }

    public class DisplayTextCellType : TextCellType
    {
        public string pCodeType { get; set; }

        public string pFirst { get; set; }

        public string pSecond { get; set; }

    }

    public class TextButtonCellType : TextCellType
    {
        public string pCodeType { get; set; }

        public string pFirst { get; set; }

        public string pSecond { get; set; }

    }

    public class FileButtonCellType : ButtonCellType
    {
    }

    public class DisplayNumberCellType : NumberCellType
    {


    }

    public class DisplayCheckBoxCellType : CheckBoxCellType
    {


    }

    public class Code_mst
    {
        public string CODE { get; set; }
        public string CODE_NAME { get; set; }
    }

    public class SMRTFTRLOG_V1_1Entity
    {
        public string crtfcKey { get; set; }
        public string logDt { get; set; }
        public string useSe { get; set; }
        public string sysUser { get; set; }
        public string conectIp { get; set; }
        public string dataUsgqty { get; set; }

    }

    public class BOMNode
    {
        public string 상위 { get; set; }
        public string 하위 { get; set; }
        public int Quantity { get; set; }
        public int 소요량 { get; set; }
        public string 파트코드 { get; set; }
        public string 파트명 { get; set; }
        public string 규격 { get; set; }
        public string 타입 { get; set; }
        public string 공정 { get; set; }
        public BOMNode(string name, int quantity)
        {
            파트코드 = name;
            Quantity = quantity;
        }

        public List<BOMNode> Children { get; set; } = new List<BOMNode>();

    }
}
