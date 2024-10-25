using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace CoFAS.NEW.MES
{
    public partial class frmMain : Form, IMainForm
    {
        #region ○ 변수선언

        Loading waitform = new Loading();
        public UserEntity _pUserEntity = null;
        public SystemLogEntity _SystemLogEntity = null;
        private IForm _pActiveChildForm;

        private string _strWindowName = "";         //화면 아이디 처리
        private bool pWindowState = false;
        private DataTable _dtMenuSetting = null;    //메뉴 설정 리스트 
        private MouseEventArgs menuMouseClick;      // 메뉴 리스트 컨트롤러의 마우스 버튼 클릭 이벤트 처리
        private System.Threading.Timer _tmrNow;

        private string _Menu_No = string.Empty;
        private string _Menu_Name = string.Empty;

   

        #endregion

        #region 활성 자식 폼 - ActiveChildForm
        public IForm ActiveChildForm
        {
            get
            {
                _pActiveChildForm = ActiveMdiChild as IForm;
                return _pActiveChildForm;
            }
        }

        #endregion

        #region 사용자 개체 - UserEntity

        public UserEntity UserEntity
        {
            get
            {
                return _pUserEntity;
            }
            set
            {
                _pUserEntity = value;
            }
        }

        #endregion

        #region ○ 생성자

        public frmMain(UserEntity pUserEntity, SystemLogEntity pSystemLogEntity)
        {
            waitform.Show(this, "준비중입니다.");
            _pUserEntity = pUserEntity;
            _SystemLogEntity = pSystemLogEntity;

            InitializeComponent();

            Load += FrmMain_Load;
            FormClosing += FrmMain_FormClosing;

          
        }

        #endregion

        #region ○ 폼 이벤트

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(Application.StartupPath + "\\" + "logo.png"))
            {
                pictureEdit1.Image = Image.FromFile(Application.StartupPath + "\\" + "logo.png");
            }
            else
            {
                pictureEdit1.Image = Properties.Resources.None;
            }

            this.Text += " - " + _pUserEntity.V_NAME;

            this.WindowState = FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm_Main_KeyDown);
            _strWindowName = this.Name.ToString();              //SCREEN ID
            _UserName.Text = _pUserEntity.user_name;

            _SystemLogEntity = new SystemLogEntity();

            Message_Display(_pUserEntity.user_name + " 님 환영합니다.");
            _tmrNow = new System.Threading.Timer(new TimerCallback(Now_DateTime), null, 0, 1000);

            SetMenuSettingList(_pUserEntity.user_authority);      //메뉴 리스트 조회
            SetMenu();

            _acdMenuControl.MouseClick += new MouseEventHandler(_acdMenuControl_MouseClick);

            InitializeButtons();

            _SystemLogEntity.user_account = _pUserEntity.user_account;
            _SystemLogEntity.user_ip = SystemLog.Get_IpAddress();
            _SystemLogEntity.user_mac = SystemLog.Get_MacAddress();
            _SystemLogEntity.user_pc = SystemLog.Get_PcName();



            Scheduler scheduler = new Scheduler(_pUserEntity);
            scheduler.Dock = DockStyle.Fill;

            _btSet.Controls.Add(scheduler);


            waitform.Close();

        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DialogResult pDialogResult = CustomMsg.ShowMessage("프로그램을 종료 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                if (pDialogResult == DialogResult.Yes)
                {
                    _SystemLogEntity.user_account = _pUserEntity.user_account;
                    _SystemLogEntity.user_ip = SystemLog.Get_IpAddress();
                    _SystemLogEntity.user_mac = SystemLog.Get_MacAddress();
                    _SystemLogEntity.user_pc = SystemLog.Get_PcName();
                    _SystemLogEntity.event_type = "LogOut";
                    _SystemLogEntity.event_log = this.Name;

                    bool _ErrorYn = new SystemLogBusiness().SystemLog_Info(_SystemLogEntity);
                    // 종료이력 쌓기
                    Application.ExitThread();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception pException)
            {
                Console.WriteLine(pException.Message);
                Application.ExitThread();
            }
        }

        #endregion

        #region 버튼 설정 설정하기 - SetButtonSetting(pButtonSetting)

        /// <summary>
        /// 버튼 설정 설정하기
        /// </summary>
        public void SetButtonSetting(MainFormButtonSetting pButtonSetting)
        {
            try
            {
                btnHome.Visible = true;
                btnSearch.Visible = pButtonSetting.UseYNSearchButton;
                btnPrint.Visible = pButtonSetting.UseYNPrintButton;
                btnDelete.Visible = pButtonSetting.UseYNDeleteButton;
                btnSave.Visible = pButtonSetting.UseYNSaveButton;
                btnImport.Visible = pButtonSetting.UseYNImportButton;
                btnExport.Visible = pButtonSetting.UseYNExportButton;
                btnInitialize.Visible = pButtonSetting.UseYNInitialButton;
                btnAddItem.Visible = pButtonSetting.UseYNAddItemButton;
                btnClose.Visible = pButtonSetting.UseYNFormCloseButton;
                btnSetting.Visible = true;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(this, "SetButtonSetting(pButtonSetting)", pException);
            }
        }

        #region ○ 버튼 초기화 하기 - InitializeButtons()
        private void InitializeButtons()
        {
            try
            {
                MainFormButtonSetting pButtonSetting = new MainFormButtonSetting();

                pButtonSetting.UseYNSearchButton = false; //조회
                pButtonSetting.UseYNExportButton = false; //내보내기
                pButtonSetting.UseYNImportButton = false; //가져오기
                pButtonSetting.UseYNPrintButton = false;  //프린터
                pButtonSetting.UseYNSaveButton = false;   //저장
                pButtonSetting.UseYNDeleteButton = false; //삭제

                pButtonSetting.UseYNAddItemButton = false; //신규 추가

                pButtonSetting.UseYNInitialButton = false; //초기화
                //pButtonSetting.UseYNSettingButton = false; //설정
                pButtonSetting.UseYNSettingButton = false; //설정
                pButtonSetting.UseYNFormCloseButton = false;
                SetButtonSetting(pButtonSetting);

            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(this, "InitializeButtons()", pException);
            }
        }

        #endregion

        #endregion

        #region ○ 공용 버튼 이벤트

        private void SimpleButton_Click(object obj, EventArgs e)
        {
            SimpleButton pCmd = obj as SimpleButton;
            string pCls = pCmd.Name.Substring(3);


            _SystemLogEntity.event_type = "Button";
            _SystemLogEntity.event_log = _Menu_No + "-" + _Menu_Name + " : " + pCls;

            switch (pCls)
            {
                case "Home":
                    _pnMain.BringToFront();
    
                    btnPrint.Visible = false;
                    btnDelete.Visible = false;
                    btnSearch.Visible = false;
                    btnSave.Visible = false;
                    btnImport.Visible = false;
                    btnExport.Visible = false;
                    btnInitialize.Visible = false;
                    btnAddItem.Visible = false;
                    btnClose.Visible = false;
                    btnSetting.Visible = true;

                    _ScreenID.Text = _strWindowName;
                    _ScreenName.Text = btnHome.Text;
                 
                    break;
                case "Search":
                    if (ActiveChildForm == null)
                        return;


                    ActiveChildForm.RaiseSearchButtonClickedEvent();
                    break;
                case "Print":
                    if (ActiveChildForm == null)
                        return;

                    ActiveChildForm.RaisePrintButtonClickedEvent();
                    break;
                case "Delete":
                    if (ActiveChildForm == null)
                        return;

                    ActiveChildForm.RaiseDeleteButtonClickedEvent();
                    break;
                case "Save":
                    if (ActiveChildForm == null)
                        return;

                    ActiveChildForm.RaiseSaveButtonClickedEvent();
                    break;
                case "Import":
                    if (ActiveChildForm == null)
                        return;

                    ActiveChildForm.RaiseImportButtonClickedEvent();
                    break;
                case "Export":
                    if (ActiveChildForm == null)
                        return;
             
                    ActiveChildForm.RaiseExportButtonClickedEvent();
                    break;
                case "Initialize":
                    if (ActiveChildForm == null)
                        return;

                    ActiveChildForm.RaiseInitialButtonClickedEvent();
                    break;
                case "AddItem":
                    if (ActiveChildForm == null)
                        return;

                    ActiveChildForm.RaiseAddItemButtonClickedEvent();
                    break;
                case "Close":
                    if (ActiveChildForm == null)
                    {
    
                        _ScreenID.Text = "";
                        _ScreenName.Text = "";

                
                        btnPrint.Visible = false;
                        btnDelete.Visible = false;
                        btnSearch.Visible = false;
                        btnSave.Visible = false;
                        btnImport.Visible = false;
                        btnExport.Visible = false;
                        btnInitialize.Visible = false;
                        btnAddItem.Visible = false;
                        btnClose.Visible = false;
                        btnSetting.Visible = false;
                        return;
                        //}
                    }
                    else
                        ActiveChildForm.RaiseFormCloseButtonClickedEvent();
                    break;
            }

            // 이력 저장
            bool _ErrorYn = new SystemLogBusiness().SystemLog_Info(_SystemLogEntity);
            if (_ErrorYn)
                CustomMsg.ShowMessage("로그인이력이 잘못 저장되었습니다.", "Information", MessageBoxButtons.OK);
        }

      

        #endregion

        #region ○ 메뉴 설정 목록 설정하기 - SetMenuSettingList(string strUserID)

        private void SetMenuSettingList(string strUserID)
        {
            try
            {
                #region 메뉴 설정 목록이 있으면 메뉴 설정 목록을 지운다.

                if (_dtMenuSetting != null)
                {
                    _dtMenuSetting.Dispose();
                    _dtMenuSetting = null;
                }

                #endregion

                DataTable dtMenuSetting = new MenuSettingBusiness().MenuList_Search(strUserID);
                if (dtMenuSetting == null || dtMenuSetting.Rows.Count < 2)
                {
                    return;
                }

                _dtMenuSetting = dtMenuSetting.Clone();

                int nCurrentMenuID = 0;
                int nMenuID;
                DataRow drNewMenuSetting;

                for (int i = 0; i < dtMenuSetting.Rows.Count; i++)
                {
                    nMenuID = (int)(dtMenuSetting.Rows[i]["menu_id"]);

                    if (nMenuID == nCurrentMenuID)
                    {
                        continue;
                    }
                    else
                    {
                        nCurrentMenuID = nMenuID;
                        drNewMenuSetting = _dtMenuSetting.NewRow();
                        for (int j = 0; j < _dtMenuSetting.Columns.Count; j++)
                        {
                            drNewMenuSetting[j] = dtMenuSetting.Rows[i][j];
                        }
                        _dtMenuSetting.Rows.Add(drNewMenuSetting);
                    }
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(this, "SetMenuSettingList(string strUserID)", pException);
            }
        }

        #endregion

        #region ○ 메뉴 설정하기 - SetMenu()

        private void SetMenu()
        {
            try
            {
                _acdMenuControl.ElementClick += new DevExpress.XtraBars.Navigation.ElementClickEventHandler(acdMenuControl_ElementClick);

                if (_dtMenuSetting == null) return;

                DataRow[] drRoots = _dtMenuSetting.Select("p_menu_id = '-1'", "sort ASC");
                DataView dvChildren = _dtMenuSetting.DefaultView;

                //그룹 생성 즐겨찾기 , MES 메뉴

                DevExpressManager.BeginInitialize(_acdMenuControl);

                // Module List
                for (int i = 0; i < drRoots.Length; i++)
                {
                    int nMenuID = (int)(drRoots[i]["menu_id"]);
                    string strMenuName = drRoots[i]["menu_name"] as string;
                    string strIconName = drRoots[i]["icon"] as string;

                    AccordionControlElement pAccordionControlElementGroup = DevExpressManager.AddAccordionGroup(_acdMenuControl, new Guid().ToString(), strMenuName);

                    //색상 이미지 폰트 설정

                    pAccordionControlElementGroup.Appearance.Normal.BackColor = Color.White;
                    //pAccordionControlElementGroup.Appearance.Normal.BackColor = Color.FromArgb(46, 51, 73);

                    pAccordionControlElementGroup.Appearance.Normal.Font = new Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
                    //pAccordionControlElementGroup.Appearance.Normal.ForeColor = Color.White;
                    pAccordionControlElementGroup.Appearance.Normal.ForeColor = Color.Black;
                    pAccordionControlElementGroup.Appearance.Normal.ForeColor = Color.FromArgb(46, 51, 73) ;

                    //pAccordionControlElementGroup.Appearance.Hovered.BackColor = Color.FromArgb(78, 184, 206);
                    pAccordionControlElementGroup.Appearance.Hovered.BackColor = Color.FromArgb(61, 68, 97);
                    //pAccordionControlElementGroup.Appearance.Hovered.BackColor = Color.White;
                    pAccordionControlElementGroup.Appearance.Hovered.Font = new Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
                    pAccordionControlElementGroup.Appearance.Hovered.ForeColor = Color.White;

                    pAccordionControlElementGroup.Appearance.Pressed.BackColor = Color.FromArgb(46, 51, 73);
                    pAccordionControlElementGroup.Appearance.Pressed.Font = new Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
                    pAccordionControlElementGroup.Appearance.Pressed.ForeColor = Color.White;


                    pAccordionControlElementGroup.ImageOptions.Image = CoFAS.NEW.MES.Properties.Resources.ResourceManager.GetObject(strIconName, CultureInfo.CurrentCulture) as Image;

                    pAccordionControlElementGroup.Hint = strMenuName;

                    dvChildren.RowFilter = string.Format("p_menu_id = {0}", nMenuID);
                    dvChildren.Sort = "sort ASC";

                    // Menu List
                    for (int j = 0; j < dvChildren.Count; j++)
                    {
                        MenuSettingEntity pMenuSettingEntity = new MenuSettingEntity(dvChildren[j]);

                        AccordionControlElement pAccordionControlElementItem = DevExpressManager.AddAccordionItem(pAccordionControlElementGroup, new Guid().ToString(), pMenuSettingEntity.MENU_NAME);
                      
                        pAccordionControlElementItem.Appearance.Normal.BackColor = Color.White;
                        //pAccordionControlElementItem.Appearance.Normal.BackColor = Color.FromArgb(46, 51, 73);
                        pAccordionControlElementItem.Appearance.Normal.Font = new Font("맑은 고딕", 9F);
                        pAccordionControlElementItem.Appearance.Normal.ForeColor = Color.FromArgb(46, 51, 73);

                        //pAccordionControlElementItem.Appearance.Hovered.BackColor = Color.FromArgb(78, 184, 206);
                        pAccordionControlElementItem.Appearance.Hovered.BackColor = Color.FromArgb(61, 68, 97);
                        pAccordionControlElementItem.Appearance.Hovered.Font = new Font("맑은 고딕", 9F);
                        pAccordionControlElementItem.Appearance.Hovered.ForeColor = Color.White;

                        pAccordionControlElementItem.Appearance.Pressed.BackColor = Color.FromArgb(46, 51, 73);
                        pAccordionControlElementItem.Appearance.Pressed.Font = new Font("맑은 고딕", 9F);
                        pAccordionControlElementItem.Appearance.Pressed.ForeColor = Color.White;


                        if (!string.IsNullOrEmpty(pMenuSettingEntity.MENU_ICONCSS))
                        {
                            try
                            {
                                pAccordionControlElementItem.ImageOptions.Image = CoFAS.NEW.MES.Properties.Resources.ResourceManager.GetObject(pMenuSettingEntity.MENU_ICONCSS, CultureInfo.CurrentCulture) as Image;
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            pAccordionControlElementItem.ImageOptions.Image = CoFAS.NEW.MES.Properties.Resources.None;
                        }

                        pAccordionControlElementItem.Tag = pMenuSettingEntity;
                    }
                }

                DevExpressManager.EndInitialize(_acdMenuControl);
                dvChildren.RowFilter = string.Empty;
                dvChildren.Sort = string.Empty;
                dvChildren = null;
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(this, "SetMenu()", pException);
            }
        }

        #endregion

        #region ○ 차일드 메뉴 실행 이벤트 - acdMenuControl_ElementClick(object sender, DevExpress.XtraBars.Navigation.ElementClickEventArgs e)
        private void acdMenuControl_ElementClick(object sender, DevExpress.XtraBars.Navigation.ElementClickEventArgs e)
        {
            try
            {
                if (e.Element.Style == DevExpress.XtraBars.Navigation.ElementStyle.Group) return;

                if (menuMouseClick.Button == MouseButtons.Left) //메뉴 오픈
                {
                    if (e.Element.Tag != null)
                    {
                        //dashboard1.Visible = false;
                        MenuSettingEntity pMenuSettingEntity = e.Element.Tag as MenuSettingEntity;

                        if (!string.IsNullOrEmpty(pMenuSettingEntity.MENU_WINDOW_NAME))
                        {
                            _Menu_No = pMenuSettingEntity.MENU_NO;
                            _Menu_Name = pMenuSettingEntity.MENU_NAME;

                            _SystemLogEntity.user_account = _pUserEntity.user_account;
                            _SystemLogEntity.user_ip = SystemLog.Get_IpAddress();
                            _SystemLogEntity.user_mac = SystemLog.Get_MacAddress();
                            _SystemLogEntity.user_pc = SystemLog.Get_PcName();
                            _SystemLogEntity.event_type = "Menu";

                            _SystemLogEntity.event_log = _Menu_No + "-" + _Menu_Name + " : Menu Open";

                            bool _ErrorYn = new SystemLogBusiness().SystemLog_Info(_SystemLogEntity);
                            if (!_ErrorYn)
                                ShowWindow(pMenuSettingEntity);
                        }
                    }
                }
            }
            catch (Exception pException)
            {
                CustomMsg.ShowExceptionMessage(pException.ToString(), "오류", MessageBoxButtons.OK);

            }

        }

        /// <summary>
        /// 메뉴 마우스 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _acdMenuControl_MouseClick(object sender, MouseEventArgs e)
        {
            menuMouseClick = e;
        }

        private void ShowWindow(MenuSettingEntity pMenuSettingEntity)
        {
            try
            {
                #region 열고자 하는 메뉴 화면이 존재하는 경우 해당 화면을 보여준다.

                _pnButtonGroup.Visible = true;

                foreach (Form pChildForm in MdiChildren)
                {
                    IForm pForm = pChildForm as IForm;
                    if (pForm.MenuSettingEntity.MENU_WINDOW_NAME == pMenuSettingEntity.MENU_WINDOW_NAME)
                    {
                        _pnMain.SendToBack();
                        _pnButtonGroup.Visible = true;

                        _ScreenID.Text = pMenuSettingEntity.MENU_WINDOW_NAME;
                        _ScreenName.Text = pMenuSettingEntity.MENU_NAME;

                        MainFormButtonSetting _MainFormButtonSetting = new MainFormButtonSetting();

                        DataTable _DataTable = new MenuButton_Business().MenuButton_Select(pMenuSettingEntity.MENU_NO, UserEntity.user_authority);
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
                        SetButtonSetting(_MainFormButtonSetting);
                        pChildForm.Show();
                        pChildForm.BringToFront();
                        pChildForm.Activate();
                        return;
                    }
                }

                #endregion

                if (pMenuSettingEntity.BASE_YN == "Y")
                {
                    string pAssemblyDll = "CoFAS.NEW.MES.Core";
                    string pAssemblyClass = pAssemblyDll + "." + pMenuSettingEntity.BASE_FORM_NAME;
                    Assembly assembly = Assembly.LoadFile(Application.StartupPath + "\\" + pAssemblyDll + ".dll");

                    Type type = assembly.GetType(pAssemblyClass);
                    object obj = Activator.CreateInstance(type);

                    Type formType = obj.GetType();

                    if(formType.BaseType.FullName.Contains("BaseForm")|| 
                        formType.BaseType.FullName == "Monthly_Schedule"||
                        formType.BaseType.FullName == "CoFAS.NEW.MES.Core.frmBaseNone")
                    {
                        CoFAS.NEW.MES.Core.frmBaseNone xfrmBaseNone = obj as CoFAS.NEW.MES.Core.frmBaseNone;

                        if (xfrmBaseNone != null)
                        {
                            _ScreenID.Text = pMenuSettingEntity.MENU_WINDOW_NAME; //하단 프로그램 ID 표시
                            _ScreenName.Text = pMenuSettingEntity.MENU_NAME; // 하단 프로그램 명 표시

                            xfrmBaseNone.Text = pMenuSettingEntity.MENU_NAME; // 폼 프로그램 명 표시 (탭에 표시됨)
                            xfrmBaseNone.MdiParent = this;
                            xfrmBaseNone.MessageEvent += new frmBaseNone.MessageEventHandler(Message_Display);
                            xfrmBaseNone.MenuSettingEntity = pMenuSettingEntity;
                            xfrmBaseNone.Size = new Size(1600, 514);
                            xfrmBaseNone.Show();
                        }

                    }
                    //switch (formType.BaseType.FullName)
                    //{
                    //    case "System.Windows.Forms.Form":
                    //        Form form = obj as Form;
                    //        if (form != null)
                    //        {
                    //            _ScreenID.Text = pMenuSettingEntity.MENU_WINDOW_NAME; //하단 프로그램 ID 표시
                    //            _ScreenName.Text = pMenuSettingEntity.MENU_NAME; // 하단 프로그램 명 표시

                    //            form.Text = pMenuSettingEntity.MENU_NAME; // 폼 프로그램 명 표시 (탭에 표시됨)
                    //            form.MdiParent = this;
                    //            form.Show();
                    //        }
                    //        break;
                    //    case "CoFAS.NEW.MES.Core.frmBaseNone":

                          
                    //}

                    //switch (pMenuSettingEntity.BASE_FROM_NAME)
                    //{
                    //    case "BaseForm1":
                    //        BaseForm1 baseForm = new BaseForm1(pMenuSettingEntity);
                    //        _ScreenID.Text = pMenuSettingEntity.MENU_WINDOW_NAME; //하단 프로그램 ID 표시
                    //        _ScreenName.Text = pMenuSettingEntity.MENU_NAME;

                    //        baseForm.Text = pMenuSettingEntity.MENU_NAME; // 폼 프로그램 명 표시 (탭에 표시됨)
                    //        baseForm.MdiParent = this;
                    //        baseForm.MessageEvent += new frmBaseNone.MessageEventHandler(Message_Display);
                    //        baseForm.MenuSettingEntity = pMenuSettingEntity;
                    //        baseForm.Show();
                    //        break;
                    //    case "BaseForm2":
                    //        BaseForm2 baseForm2 = new BaseForm2(pMenuSettingEntity);
                    //        _ScreenID.Text = pMenuSettingEntity.MENU_WINDOW_NAME; //하단 프로그램 ID 표시
                    //        _ScreenName.Text = pMenuSettingEntity.MENU_NAME;

                    //        baseForm2.Text = pMenuSettingEntity.MENU_NAME; // 폼 프로그램 명 표시 (탭에 표시됨)
                    //        baseForm2.MdiParent = this;
                    //        baseForm2.MessageEvent += new frmBaseNone.MessageEventHandler(Message_Display);
                    //        baseForm2.MenuSettingEntity = pMenuSettingEntity;
                    //        baseForm2.Show();
                    //        break;
                    //    case "DoubleBaseForm1":
                    //        DoubleBaseForm1 DoubleBaseForm1 = new DoubleBaseForm1(); 
                    //        _ScreenID.Text = pMenuSettingEntity.MENU_WINDOW_NAME; //하단 프로그램 ID 표시
                    //        _ScreenName.Text = pMenuSettingEntity.MENU_NAME;
                    //        DoubleBaseForm1._MenuSettingEntity = pMenuSettingEntity;
                    //        DoubleBaseForm1.Text = pMenuSettingEntity.MENU_NAME; // 폼 프로그램 명 표시 (탭에 표시됨)
                    //        DoubleBaseForm1.MdiParent = this;
                    //        DoubleBaseForm1.MessageEvent += new frmBaseNone.MessageEventHandler(Message_Display);
                    //        DoubleBaseForm1.MenuSettingEntity = pMenuSettingEntity;
                    //        DoubleBaseForm1.Show();
                    //        break;
                    //    case "DoubleBaseForm2":
                    //        DoubleBaseForm2 DoubleBaseForm2 = new DoubleBaseForm2();
                    //        _ScreenID.Text = pMenuSettingEntity.MENU_WINDOW_NAME; //하단 프로그램 ID 표시
                    //        _ScreenName.Text = pMenuSettingEntity.MENU_NAME;
                    //        DoubleBaseForm2._MenuSettingEntity = pMenuSettingEntity;
                    //        DoubleBaseForm2.Text = pMenuSettingEntity.MENU_NAME; // 폼 프로그램 명 표시 (탭에 표시됨)
                    //        DoubleBaseForm2.MdiParent = this;
                    //        DoubleBaseForm2.MessageEvent += new frmBaseNone.MessageEventHandler(Message_Display);
                    //        DoubleBaseForm2.MenuSettingEntity = pMenuSettingEntity;
                    //        DoubleBaseForm2.Show();
                    //        break;
                    //    case "Monthly_Schedule":
                    //        Monthly_Schedule monthly_Schedule = new Monthly_Schedule(pMenuSettingEntity);
                    //        _ScreenID.Text = pMenuSettingEntity.MENU_WINDOW_NAME; //하단 프로그램 ID 표시
                    //        _ScreenName.Text = pMenuSettingEntity.MENU_NAME;

                    //        monthly_Schedule.Text = pMenuSettingEntity.MENU_NAME; // 폼 프로그램 명 표시 (탭에 표시됨)
                    //        monthly_Schedule.MdiParent = this;
                    //        monthly_Schedule.MessageEvent += new frmBaseNone.MessageEventHandler(Message_Display);
                    //        monthly_Schedule.MenuSettingEntity = pMenuSettingEntity;
                    //        monthly_Schedule.Show();
                    //        break;


                    //}
                    //string pAssemblyDll = "CoFAS.NEW.MES.Core";
                    //string pAssemblyClass = pAssemblyDll + "." + pMenuSettingEntity.BASE_FROM_NAME;

                    //Assembly assembly = Assembly.LoadFile(Application.StartupPath + "\\" + pAssemblyDll + ".dll");

                    //Type type = assembly.GetType(pAssemblyClass);
                    //object obj = Activator.CreateInstance(type);

                    //Type formType = obj.GetType();


                }
                else
                
                {
                    string pAssemblyDll = "CoFAS.NEW.MES." + pMenuSettingEntity.MENU_MODULE;
                    string pAssemblyClass = pAssemblyDll + "." + pMenuSettingEntity.MENU_WINDOW_NAME;

                    Assembly assembly = Assembly.LoadFile(Application.StartupPath + "\\" + pAssemblyDll + ".dll");

                    Type type = assembly.GetType(pAssemblyClass);
                    object obj = Activator.CreateInstance(type);

                    Type formType = obj.GetType();

                    switch (formType.BaseType.FullName)
                    {
                        case "System.Windows.Forms.Form":
                            Form form = obj as Form;
                            if (form != null)
                            {
                                _ScreenID.Text = pMenuSettingEntity.MENU_WINDOW_NAME; //하단 프로그램 ID 표시
                                _ScreenName.Text = pMenuSettingEntity.MENU_NAME; // 하단 프로그램 명 표시

                                form.Text = pMenuSettingEntity.MENU_NAME; // 폼 프로그램 명 표시 (탭에 표시됨)
                                form.MdiParent = this;
                                form.Size = new Size(1600, 514);
                                form.Show();
                            }
                            break;
                        case "CoFAS.NEW.MES.Core.frmBaseNone":

                             CoFAS.NEW.MES.Core.frmBaseNone xfrmBaseNone = obj as  CoFAS.NEW.MES.Core.frmBaseNone;

                            if (xfrmBaseNone != null)
                            {
                                _ScreenID.Text = pMenuSettingEntity.MENU_WINDOW_NAME; //하단 프로그램 ID 표시
                                _ScreenName.Text = pMenuSettingEntity.MENU_NAME; // 하단 프로그램 명 표시

                                xfrmBaseNone.Text = pMenuSettingEntity.MENU_NAME; // 폼 프로그램 명 표시 (탭에 표시됨)
                                xfrmBaseNone.MdiParent = this;
                                xfrmBaseNone.MessageEvent += new frmBaseNone.MessageEventHandler(Message_Display);
                                xfrmBaseNone.MenuSettingEntity = pMenuSettingEntity;
                                xfrmBaseNone.Size = new Size(1600, 514);
                                xfrmBaseNone.Show();
                            }

                            break;
                    }
                }

            }
            catch (Exception pException)
            {
                throw new ExceptionManager(this, "ShowWindow(pMenuSettingEntity)", pException);
            }
        }

        #endregion

        #region 탭 메뉴 이벤트 설정

        private void xtraTabMdiControl_PageRemoved(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            if (xtraTabMdiControl.Pages.Count == 0)
            {
                _pnMain.BringToFront();
         
                btnPrint.Visible = false;
                btnDelete.Visible = false;
                btnSearch.Visible = false;
                btnSave.Visible = false;
                btnImport.Visible = false;
                btnExport.Visible = false;
                btnInitialize.Visible = false;
                btnAddItem.Visible = false;
                btnClose.Visible = false;
                btnSetting.Visible = false;
            }
        }

        private void xtraTabMdiControl_PageAdded(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            if (xtraTabMdiControl.Pages.Count != 0)
            {
                _pnMain.SendToBack();
            }
        }

        #endregion

        #region 기타이벤트 설정

        #region ○ 메시지표시 - Message_Display(string pMessage)

        public void Message_Display(string pMessage)
        {
            _Message.Text = " " + pMessage;
        }

        #endregion

        private void Now_DateTime(object obj)
        {
            ControlManager.InvokeIfNeeded(_Time, () => _Time.Text = DateTime.Now.ToString());
        }

        private void lbl_Click(object obj, EventArgs e)
        {
            LabelControl pCmd = obj as LabelControl;
            string pCls = pCmd.Name.Substring(4);

            switch (pCls)
            {
                case "Min":
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "Max":
                    if (!pWindowState)
                    {
                        this.WindowState = FormWindowState.Maximized;
                        pWindowState = true;
                    }
                    else
                    {
                        this.WindowState = FormWindowState.Normal;
                        pWindowState = false;
                    }
                    break;
                case "Exit":
                    DialogResult pDialogResult = CustomMsg.ShowMessage("프로그램을 종료 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                    if (pDialogResult == DialogResult.Yes)
                    {
                        _SystemLogEntity.user_account = _pUserEntity.user_account;
                        _SystemLogEntity.user_ip = SystemLog.Get_IpAddress();
                        _SystemLogEntity.user_mac = SystemLog.Get_MacAddress();
                        _SystemLogEntity.user_pc = SystemLog.Get_PcName();
                        _SystemLogEntity.event_type = "Logout";
                        _SystemLogEntity.event_log = this.Name;

                        bool _ErrorYn = new SystemLogBusiness().SystemLog_Info(_SystemLogEntity);
                        // 종료이력 쌓기
                        Application.ExitThread();
                    }
                    break;
            }
        }
        
        private void frmMain_MdiChildActivate(object sender, EventArgs e)
        {
            if (ActiveChildForm != null)
            {
                _ScreenID.Text = ActiveChildForm.MenuSettingEntity.MENU_WINDOW_NAME;
                _ScreenName.Text = ActiveChildForm.MenuSettingEntity.MENU_NAME;
            }
            else
            {
                //차일드 폼이 없을 경우 메인 홈 명칭으로 변경
                _ScreenID.Text = _strWindowName;
                _ScreenName.Text = btnHome.Text;

                InitializeButtons();
            }
        }

        // 좌측메뉴 숨기기
        private void _ptCI_DoubleClick(object sender, EventArgs e)
        {
            if (_acdMenuControl.Visible)
            {

                _acdMenuControl.Visible = false;
            }
            else
            {
                _acdMenuControl.Visible = true;
            }
        }

        #endregion

        private void btnSetting_Click(object sender, EventArgs e)
        {
            //중복 실행 방지
            frmPwChange pwc = new frmPwChange(_pUserEntity);

            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == "frmPwChange")
                {
                    frm.Activate();
                    return;
                }
            }
            pwc.ShowDialog();
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        }
        private void Frm_Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.ActiveMdiChild == null)
            {
                return;
            }

            switch (e.KeyData)
            {
                case Keys.F1:
                    btnSearch.PerformClick();
                    break;

                case Keys.F2:
                    btnPrint.PerformClick();
                    break;

                case Keys.F3:
                    btnDelete.PerformClick();
                    break;

                case Keys.F4:
                    btnSave.PerformClick();
                    break;

                case Keys.F5:
                    btnImport.PerformClick();
                    break;

                case Keys.F6:
                    btnExport.PerformClick();
                    break;

                case Keys.F7:
                    btnInitialize.PerformClick();
                    break;

                case Keys.F8:
                    btnAddItem.PerformClick();
                    break;
                //case Keys.F9:
                //    _btLOTOUT.PerformClick();
                //    break;

                case Keys.F10:
                    btnClose.PerformClick();
                    break;


                default:
                    break;

            }


        }

        private void btnAddItem_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < 10; i++)
                {
                    ActiveChildForm.RaiseAddItemButtonClickedEvent();
                    //SimpleButton_Click(btnAddItem, null);
                }
            }
        }
    }
}
