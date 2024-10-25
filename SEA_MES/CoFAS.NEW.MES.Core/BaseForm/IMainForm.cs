using CoFAS.NEW.MES.Core.Entity;


namespace CoFAS.NEW.MES.Core
{
    public interface IMainForm
    {
        #region 사용자 개체 - UserEntity

        /// <summary>
        /// 사용자 개체
        /// </summary>
        UserEntity UserEntity { get; set; }

        #endregion

        #region 버튼 설정 설정하기 - SetButtonSetting(MainFormButtonSetting pButtonSetting)

        /// <summary>
        /// 버튼 설정 설정하기
        /// </summary>
        /// <param name="pButtonSetting">버튼 설정</param>
        void SetButtonSetting(MainFormButtonSetting pButtonSetting);

        #endregion
    }
}
