using CoFAS.NEW.MES.Core.Entity;


namespace CoFAS.NEW.MES.Core
{
    public interface IForm
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Property

        #region 윈도우명 - WindowName

        /// <summary>
        /// 윈도우명
        /// </summary>
        string WindowName { get; set; }

        #endregion

        #region 메뉴 설정 개체 - MenuSettingEntity

        /// <summary>
        /// 메뉴 설정 개체
        /// </summary>
        MenuSettingEntity MenuSettingEntity { get; set; }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Method

        #region 조회 버튼 클릭시 이벤트 발생시키기 - RaiseSearchButtonClickedEvent()

        /// <summary>
        /// 조회 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        /// <param name="pSender">이벤트 발생자</param>
        /// <param name="pEventArgs">이벤트 인자</param>
        void RaiseSearchButtonClickedEvent();
      

        #endregion
    
        #region 삭제 버튼 클릭시 이벤트 발생시키기 - RaiseDeleteButtonClickedEvent()

        /// <summary>
        /// 삭제 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        /// <param name="pSender">이벤트 발생자</param>
        /// <param name="pEventArgs">이벤트 인자</param>
        void RaiseDeleteButtonClickedEvent();

        #endregion

        #region 저장 버튼 클릭시 이벤트 발생시키기 - RaiseSaveButtonClickedEvent()

        /// <summary>
        /// 저장 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        /// <param name="pSender">이벤트 발생자</param>
        /// <param name="pEventArgs">이벤트 인자</param>
        void RaiseSaveButtonClickedEvent();

        #endregion

        #region 인쇄 버튼 클릭시 이벤트 발생시키기 - RaisePrintButtonClickedEvent()

        /// <summary>
        /// 인쇄 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        /// <param name="pSender">이벤트 발생자</param>
        /// <param name="pEventArgs">이벤트 인자</param>
        void RaisePrintButtonClickedEvent();

        #endregion

        #region 내보내기 버튼 클릭시 이벤트 발생시키기 - RaiseExportButtonClickedEvent()

        /// <summary>
        /// 내보내기 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        /// <param name="pSender">이벤트 발생자</param>
        /// <param name="pEventArgs">이벤트 인자</param>
        void RaiseExportButtonClickedEvent();

        #endregion

        #region 가져오기 버튼 클릭시 이벤트 발생시키기 - RaiseImportButtonClickedEvent()

        /// <summary>
        /// 가져오기 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        /// <param name="pSender">이벤트 발생자</param>
        /// <param name="pEventArgs">이벤트 인자</param>
        void RaiseImportButtonClickedEvent();

        #endregion

        #region 초기화 버튼 클릭시 이벤트 발생시키기 - RaiseInitialButtonClickedEvent()

        /// <summary>
        /// 조회 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        /// <param name="pSender">이벤트 발생자</param>
        /// <param name="pEventArgs">이벤트 인자</param>
        void RaiseInitialButtonClickedEvent();

        #endregion

        #region 설정 버튼 클릭시 이벤트 발생시키기 - RaiseSettingButtonClickedEvent()

        /// <summary>
        /// 설정 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        /// <param name="pSender">이벤트 발생자</param>
        /// <param name="pEventArgs">이벤트 인자</param>
        void RaiseSettingButtonClickedEvent();

        #endregion

        #region 신규추가 버튼 클릭시 이벤트 발생시키기 - RaiseAddItemButtonClickedEvent()

        /// <summary>
        /// 신규추가 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        /// <param name="pSender">이벤트 발생자</param>
        /// <param name="pEventArgs">이벤트 인자</param>
        void RaiseAddItemButtonClickedEvent();

        #endregion

        #region 닫기 버튼 클릭시 이벤트 발생시키기 - RaiseFormCloseButtonClickedEvent()

        /// <summary>
        /// 닫기 버튼 클릭시 이벤트 발생시키기
        /// </summary>
        /// <param name="pSender">이벤트 발생자</param>
        /// <param name="pEventArgs">이벤트 인자</param>
        void RaiseFormCloseButtonClickedEvent();

        #endregion

    }
}
