using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraTab;
using DevExpress.LookAndFeel;
using System.ComponentModel;
using DevExpress.XtraBars.Navigation;

namespace CoFAS.NEW.MES.Core.Function
{
    public class DevExpressManager
    {
        #region Delegate

        /// <summary>
        /// 컨트롤 포커스 설정하기 델리게이트
        /// </summary>
        /// <param name="pControl">컨트롤</param>
        private delegate void FocusControlDelegate(System.Windows.Forms.Control pControl);

        /// <summary>
        /// 컨트롤 활성화 하기 델리게이트
        /// </summary>
        /// <param name="pForm">폼</param>
        /// <param name="bEnabled">활성화 여부</param>
        /// <param name="pControls">컨트롤 목록</param>
        private delegate void EnableControlDelegate(Form pForm, bool bEnabled, params System.Windows.Forms.Control[] pControls);

        /// <summary>
        /// 컨트롤 추가하기
        /// </summary>
        /// <param name="pParentControl">부모 컨트롤</param>
        /// <param name="pChildControl">자식 컨트롤</param>
        private delegate void AddControlDelegate(System.Windows.Forms.Control pParentControl, System.Windows.Forms.Control pChildControl);

        /// <summary>
        /// 커서 설정하기 델리게이트
        /// </summary>
        /// <param name="pControl">컨트롤</param>
        /// <param name="pCursor">커서</param>
        protected delegate void SetCursorDelegate(System.Windows.Forms.Control pControl, Cursor pCursor);

        /// <summary>
        /// 뷰 영역으로 컨트롤 스크롤 하기
        /// </summary>
        /// <param name="pFlowLayoutPanel">FlowLayoutPanel</param>
        /// <param name="pControl">Control</param>
        protected delegate void ScrollControlIntoViewDeletage(FlowLayoutPanel pFlowLayoutPanel, System.Windows.Forms.Control pControl);

        /// <summary>
        /// 탭 제목 설정하기 델리게이트
        /// </summary>
        /// <param name="pXtraTabControl">엑스트라 탭 컨트롤</param>
        /// <param name="nTabPageIndex">탭 페이지 인덱스</param>
        /// <param name="strCaption">제목</param>
        private delegate void SetTabCaptionDelegate(XtraTabControl pXtraTabControl, int nTabPageIndex, string strCaption);

        /// <summary>
        /// 탭 제목 설정하기 델리게이트 2
        /// </summary>
        /// <param name="pXtraTabControl">엑스트라 탭 컨트롤</param>
        /// <param name="nTabPageIndex">탭 페이지 인덱스</param>
        /// <param name="strDefaultTitle">디폴트 제목</param>
        /// <param name="nCount">건수</param>
        private delegate void SetTabCaptionDelegate2(XtraTabControl pXtraTabControl, int nTabPageIndex, string strDefaultTitle, int nCount);

        /// <summary>
        /// 텍스트 설정하기 델리게이트 1
        /// </summary>
        /// <param name="pControl">컨트롤</param>
        /// <param name="strText">텍스트</param>
        private delegate void SetTextDelegate1(System.Windows.Forms.Control pControl, string strText);

        /// <summary>
        /// 텍스트 설정하기 델리게이트 2
        /// </summary>
        /// <param name="pControl">컨트롤</param>
        /// <param name="strDefaultText">디폴트 텍스트</param>
        /// <param name="nCount">건수</param>
        private delegate void SetTextDelegate2(System.Windows.Forms.Control pControl, string strDefaultText, int nCount);

        #endregion

        private static string _pCORP_CODE;
        public static string pCORP_CODE
        {
            set
            {
                _pCORP_CODE = value;
            }
        }

        #region 생성자 - CoFAS_DevExpressManager()

        /// <summary>
        /// 생성자
        /// </summary>
        private DevExpressManager()
        {
        }

        #endregion

        #region 컨트롤 포커스 설정하기 - FocusControl(pControl)

        /// <summary>
        /// 컨트롤 포커스 설정하기
        /// </summary>
        /// <param name="pControl">컨트롤</param>
        public static void FocusControl(System.Windows.Forms.Control pControl)
        {
            try
            {
                if (pControl.InvokeRequired)
                {
                    FocusControlDelegate pFocusControlDelegate = new FocusControlDelegate(FocusControl);

                    pControl.Invoke(pFocusControlDelegate, pControl);
                }
                else
                {
                    pControl.Focus();
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    typeof(DevExpressManager),
                    "FocusControl(pControl)",
                    pException
                );
            }
        }

        #endregion

        #region 컨트롤 활성화 하기 - EnableControl(pForm, bEnabled, pControls)

        /// <summary>
        /// 컨트롤 활성화 하기
        /// </summary>
        /// <param name="pForm">폼</param>
        /// <param name="bEnabled">활성화 여부</param>
        /// <param name="pControls">컨트롤 목록</param>
        public static void EnableControl(Form pForm, bool bEnabled, params System.Windows.Forms.Control[] pControls)
        {
            if (pForm.InvokeRequired)
            {
                EnableControlDelegate pEnableControlDelegate = new EnableControlDelegate(EnableControl);

                pForm.Invoke(pEnableControlDelegate, pForm, bEnabled, pControls);
            }
            else
            {
                foreach (System.Windows.Forms.Control pControl in pControls)
                {
                    pControl.Enabled = bEnabled;
                }
            }
        }

        #endregion

        #region 컨트롤 추가하기 - AddControl(pParentControl, pChildControl)

        /// <summary>
        /// 컨트롤 추가하기
        /// </summary>
        /// <param name="pParentControl">부모 컨트롤</param>
        /// <param name="pChildControl">자식 컨트롤</param>
        public static void AddControl(System.Windows.Forms.Control pParentControl, System.Windows.Forms.Control pChildControl)
        {
            try
            {
                if (pParentControl.InvokeRequired)
                {
                    AddControlDelegate pAddControlDelegate = new AddControlDelegate(AddControl);

                    pParentControl.Invoke(pAddControlDelegate, pParentControl, pChildControl);
                }
                else
                {
                    pParentControl.Controls.Add(pChildControl);
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    typeof(DevExpressManager),
                    "FocusControl(pControl)",
                    pException
                );
            }
        }

        #endregion

        #region 커서 설정하기 - SetCursor(pControl, pCursor)

        /// <summary>
        /// 커서 설정하기
        /// </summary>
        public static void SetCursor(System.Windows.Forms.Control pControl, Cursor pCursor)
        {
            try
            {
                if (pControl.InvokeRequired)
                {

                    SetCursorDelegate pSetCursorDelegate = new SetCursorDelegate(SetCursor);

                    pControl.Invoke(pSetCursorDelegate, pControl, pCursor);
                    Application.DoEvents();
                }
                else
                {
                    pControl.Cursor = pCursor;
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    typeof(DevExpressManager),
                    "SetCursor(pControl, pCursor)",
                    pException
                );
            }
        }

        #endregion

        #region 디폴트 룩앤필 구하기 - GetDefaultLookAndFeel(pContainer, strSkinName)

        /// <summary>
        /// 디폴트 룩앤필 구하기
        /// </summary>
        /// <param name="pContainer">컨테이너</param>
        /// <param name="strSkinName">스킨명</param>
        /// <returns>디폴트 룩앤필</returns>
        public static DefaultLookAndFeel GetDefaultLookAndFeel(IContainer pContainer, string strSkinName)
        {
            DefaultLookAndFeel pDefaultLookAndFeel = null;

            try
            {
                pDefaultLookAndFeel = new DefaultLookAndFeel(pContainer);

                pDefaultLookAndFeel.LookAndFeel.SkinName = strSkinName;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    typeof(DevExpressManager),
                    "GetDefaultLookAndFeel(pContainer, strSkinName)",
                    pException
                );
            }

            return pDefaultLookAndFeel;
        }

        #endregion

        #region 도구 스트립 메뉴 항목 구하기 - GetToolStripMenuItem(strName, strText)

        /// <summary>
        /// 도구 스트립 메뉴 항목 구하기
        /// </summary>
        /// <param name="strName">명칭</param>
        /// <param name="strText">텍스트</param>
        /// <returns>도구 스트립 메뉴 항목</returns>
        public static ToolStripMenuItem GetToolStripMenuItem(string strName, string strText)
        {
            ToolStripMenuItem pToolStripMenuItem = null;

            try
            {
                pToolStripMenuItem = new ToolStripMenuItem();

                pToolStripMenuItem.Name = strName;
                pToolStripMenuItem.Text = strText;
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    typeof(DevExpressManager),
                    "GetToolStripMenuItem(strName, strText)",
                    pException
                );
            }

            return pToolStripMenuItem;
        }

        #endregion

        //////////////////////////////////////////////////////////// XtraTabControl

        #region 탭 제목 설정하기 - SetTabCaption(pXtraTabControl, nTabPageIndex, strCaption)

        /// <summary>
        /// 탭 제목 설정하기
        /// </summary>
        /// <param name="pXtraTabControl">엑스트라 탭 컨트롤</param>
        /// <param name="nTabPageIndex">탭 페이지 인덱스</param>
        /// <param name="strCaption">제목</param>
        public static void SetTabCaption(XtraTabControl pXtraTabControl, int nTabPageIndex, string strCaption)
        {
            try
            {
                if (pXtraTabControl.InvokeRequired)
                {
                    SetTabCaptionDelegate pSetTabCaptionDelegate = new SetTabCaptionDelegate(SetTabCaption);

                    pXtraTabControl.Invoke(pSetTabCaptionDelegate, pXtraTabControl, nTabPageIndex, strCaption);
                }
                else
                {
                    pXtraTabControl.TabPages[nTabPageIndex].Text = strCaption;
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    typeof(DevExpressManager),
                    "SetTabCaption(pXtraTabControl, nTabPageIndex, strCaption)",
                    pException
                );
            }
        }

        #endregion

        #region 탭 제목 설정하기 - SetTabCaption(pXtraTabControl, nTabPageIndex, strDefaultTitle, nCount)

        /// <summary>
        /// 탭 제목 설정하기
        /// </summary>
        /// <param name="pXtraTabControl">엑스트라 탭 컨트롤</param>
        /// <param name="nTabPageIndex">탭 페이지 인덱스</param>
        /// <param name="strDefaultTitle">디폴트 제목</param>
        /// <param name="nCount">건수</param>
        public static void SetTabCaption(XtraTabControl pXtraTabControl, int nTabPageIndex, string strDefaultTitle, int nCount)
        {
            try
            {
                if (pXtraTabControl.InvokeRequired)
                {
                    SetTabCaptionDelegate2 pShowListCountDelegate = new SetTabCaptionDelegate2(SetTabCaption);

                    pXtraTabControl.Invoke(pShowListCountDelegate, pXtraTabControl, nTabPageIndex, strDefaultTitle, nCount);
                }
                else
                {
                    if (nCount > 0)
                    {
                        pXtraTabControl.TabPages[nTabPageIndex].Text = string.Format
                        (
                            "{0} ({1}건)",
                            strDefaultTitle,
                            nCount.ToString("#,##0")
                        );
                    }
                    else
                    {
                        pXtraTabControl.TabPages[nTabPageIndex].Text = strDefaultTitle;
                    }
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    typeof(DevExpressManager),
                    "SetTabCaption(pXtraTabControl, nTabPageIndex, strDefaultTitle, nCount)",
                    pException
                );
            }
        }

        #endregion

        //////////////////////////////////////////////////////////// Navigation Control

        #region 초기화 시작하기 - BeginInitialize(AccordionControl pAccordionControl)

        /// <summary>
        /// 초기화 시작하기
        /// </summary>
        /// <param name="pAccordionControl">네비게이션 아카디언 컨트롤</param>
        public static void BeginInitialize(AccordionControl pAccordionControl)
        {
            try
            {
                ((ISupportInitialize)pAccordionControl).BeginInit();
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    typeof(DevExpressManager),
                    "BeginInitialize(AccordionControl pAccordionControl)",
                    pException
                );
            }
        }

        #endregion

        #region 초기화 종료하기 - EndInitialize(AccordionControl pAccordionControl)

        /// <summary>
        /// 초기화 종료하기
        /// </summary>
        /// <param name="pAccordionControl">네비게이션 아카디언 컨트롤</param>
        public static void EndInitialize(AccordionControl pAccordionControl)
        {
            try
            {
                ((ISupportInitialize)pAccordionControl).EndInit();
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    typeof(DevExpressManager),
                    "EndInitialize(AccordionControl pAccordionControl)",
                    pException
                );
            }
        }

        #endregion

        #region 데이타 지우기 - ClearData(AccordionControl pAccordionControl)

        /// <summary>
        /// 데이타 지우기
        /// </summary>
        /// <param name="pAccordionControl">네비게이션 아카디언 컨트롤</param>
        public static void ClearData(AccordionControl pAccordionControl)
        {
            try
            {
                AccordionControlElementCollection pAccordionControlElementCollection = pAccordionControl.Elements;


                for (int i = 0; i < pAccordionControlElementCollection.Count; i++)
                {
                    pAccordionControlElementCollection[i].Elements.Clear();
                }

                pAccordionControlElementCollection.Clear();

                pAccordionControl.Elements.Clear();
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    typeof(DevExpressManager),
                    "ClearData(AccordionControl pAccordionControl)",
                    pException
                );
            }
        }

        #endregion

        #region 네비게이션 아카디언 그룹 추가하기 - AddAccordionGroup(AccordionControl pAccordionControl, string strName, string strCaption)
        public static AccordionControlElement AddAccordionGroup(AccordionControl pAccordionControl, string strName, string strCaption)
        {
            try
            {
                AccordionControlElement pAccordionControlElementGroup = new AccordionControlElement();

                pAccordionControlElementGroup.Name = strName;
                pAccordionControlElementGroup.Text = strCaption;
                pAccordionControlElementGroup.Style = ElementStyle.Group;

                pAccordionControl.Elements.Add(pAccordionControlElementGroup);

                return pAccordionControlElementGroup;

            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    typeof(DevExpressManager),
                    "AccordionControlElement AddAccordionItem(AccordionControl pAccordionControl, string strName, string strCaption)",
                    pException
                );
            }
        }
        #endregion

        public static AccordionControlElement AddAccordionChildGroup(AccordionControlElement pAccordionControlElement, string strName, string strCaption)
        {
            try
            {
                AccordionControlElement pAccordionControlElementChildGroup = new AccordionControlElement();

                pAccordionControlElementChildGroup.Name = strName;
                pAccordionControlElementChildGroup.Text = strCaption;
                pAccordionControlElementChildGroup.Style = ElementStyle.Group;

                pAccordionControlElement.Elements.Add(pAccordionControlElementChildGroup);

                return pAccordionControlElementChildGroup;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    typeof(DevExpressManager),
                    "AccordionControlElement AddAccordionChildGroup(AccordionControlElement pAccordionControlElement, string strName, string strCaption)",
                    pException
                );
            }
        }

        #region 네비게이션 아카디언 아이템 추가하기 - AddAccordionItem(AccordionControlElement pAccordionControlElementGroup, string strName, string strCaption)
        public static AccordionControlElement AddAccordionItem(AccordionControlElement pAccordionControlElementGroup, string strName, string strCaption)
        {
            try
            {
                AccordionControlElement pAccordionControlElementItem = new AccordionControlElement();

                pAccordionControlElementItem.Name = strName;
                pAccordionControlElementItem.Text = strCaption;
                pAccordionControlElementItem.Style = ElementStyle.Item;

                pAccordionControlElementGroup.Elements.Add(pAccordionControlElementItem);

                return pAccordionControlElementItem;

            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    typeof(DevExpressManager),
                    "AccordionControlElement AddAccordionItem(AccordionControl pAccordionControl, string strName, string strCaption)",
                    pException
                );
            }
        }
        #endregion


        #region 텍스트 설정하기 - SetText(pControl, strText)

        /// <summary>
        /// 텍스트 설정하기
        /// </summary>
        /// <param name="pControl">컨트롤</param>
        /// <param name="strText">텍스트</param>
        public static void SetText(System.Windows.Forms.Control pControl, string strText)
        {
            if (pControl.InvokeRequired)
            {
                SetTextDelegate1 pSetTextDelegate1 = new SetTextDelegate1(SetText);

                pControl.Invoke(pSetTextDelegate1, pControl, strText);
            }
            else
            {
                pControl.Text = strText;
            }
        }

        #endregion

        #region 텍스트 설정하기 - SetText(pControl, strDefaultText, nCount)

        /// <summary>
        /// 텍스트 설정하기
        /// </summary>
        /// <param name="pControl">컨트롤</param>
        /// <param name="strDefaultText">디폴트 텍스트</param>
        /// <param name="nCount">건수</param>
        public static void SetText(System.Windows.Forms.Control pControl, string strDefaultText, int nCount)
        {
            if (pControl.InvokeRequired)
            {
                SetTextDelegate2 pSetTextDelegate2 = new SetTextDelegate2(SetText);

                pControl.Invoke(pSetTextDelegate2, pControl, strDefaultText, nCount);
            }
            else
            {
                if (nCount > 0)
                {
                    pControl.Text = string.Format("{0} : {1}건", strDefaultText, nCount);
                }
                else
                {
                    pControl.Text = strDefaultText;
                }
            }
        }

        #endregion

        #region 뷰 영역으로 컨트롤 스크롤 하기 - ScrollControlIntoView(pFlowLayoutPanel, pControl)

        /// <summary>
        /// 뷰 영역으로 컨트롤 스크롤 하기
        /// </summary>
        /// <param name="pFlowLayoutPanel">FlowLayoutPanel</param>
        /// <param name="pControl">Control</param>
        public static void ScrollControlIntoView(FlowLayoutPanel pFlowLayoutPanel, System.Windows.Forms.Control pControl)
        {
            if (pFlowLayoutPanel.InvokeRequired)
            {
                ScrollControlIntoViewDeletage pScrollControlIntoViewDeletage = new ScrollControlIntoViewDeletage(ScrollControlIntoView);

                pFlowLayoutPanel.Invoke(pScrollControlIntoViewDeletage, pFlowLayoutPanel, pControl);
            }
            else
            {
                pFlowLayoutPanel.ScrollControlIntoView(pControl);
            }
        }

        #endregion

    }
}
