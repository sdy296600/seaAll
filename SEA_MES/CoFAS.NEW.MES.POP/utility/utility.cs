using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Net.Sockets;
using System.IO.Ports;
using System.Threading;

namespace CoFAS.NEW.MES.POP
{
    public class utility
    {
        public utility() { }
      
        public static My_Settings My_Settings_Start()
        {
            My_Settings settings = new My_Settings();
            try
            {
                string path = Application.StartupPath + "\\MySettings.txt";

                FileInfo info = new FileInfo(path);

                if (info.Exists)
                {
                    settings = My_Settings_Get();
                }
                else
                {
                    settings = My_Settings_initialization();
                }
                return settings;
            }
            catch (Exception err)
            {
                return settings;
            }
        }

        public static My_Settings My_Settings_Get()
        {
            My_Settings settings = new My_Settings();
            try
            {
                string path = Application.StartupPath + "\\MySettings.txt";

                FileInfo info = new FileInfo(path);

                string str = File.ReadAllText(path);

                settings = JsonConvert.DeserializeObject<My_Settings>(str);

                return settings;
            }
            catch (Exception err)
            {
                return settings;
            }
        }

        public static My_Settings My_Settings_Set(My_Settings setting)
        {
            My_Settings settings = new My_Settings();

            try
            {
                string path = Application.StartupPath + "\\MySettings.txt";

                FileInfo info = new FileInfo(path);

                string str = JsonConvert.SerializeObject(setting);

                StringBuilder sb = new StringBuilder();

                foreach (string item in str.Split(','))
                {
                    sb.Append(item + "," + "\n\r");
                }

                str = sb.ToString();

                str = str.Substring(0, str.Length - 3);

                File.WriteAllText(path, str);

                settings = JsonConvert.DeserializeObject<My_Settings>(File.ReadAllText(path));

                return settings;
            }
            catch (Exception err)
            {
                return settings;
            }
        }

        public static My_Settings My_Settings_initialization()
        {
            My_Settings settings = new My_Settings();

            try
            {
                settings.USER_ID = "219220";
                settings.SERVER_IP = "127.0.0.1";
                settings.THEME = "Blue";
                settings.MQTT = "1883";
                settings.FONT_SIZE = 15;
                settings.FONT_TYPE = "맑은 고딕";
                settings.MULTI_LANGUAGE = "한국어";
                settings.USER_ID_SAVE = true;       
                settings.DB_PORT = "3306";
                settings.DB_IP = "127.0.0.1";
                settings.DB_NAME = "HS_MES";
                settings.DB_ID = "sa";
                settings.DB_TYPE = "MsSql";          
                settings.PortName = "COM6";
                settings.BaudRate = "960";
                settings.DataSize = "8";
                settings.Parity = "none";
                settings.Handshake = "none";

                settings = My_Settings_Set(settings);

                return settings;
            }

            catch (Exception err)
            {
                return settings;
            }
        }

       

        

    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

  
    public class SHA256Encryption
    {
        // SHA 암호화는 복호화가 불가능
        public static string SHA256_EncryptToString(string phrase)
        {
            string pReturn = string.Empty;

            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(phrase));

                // Convert byte array to a string   
                StringBuilder pStringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    pStringBuilder.Append(bytes[i].ToString("x2"));
                }

                pReturn = pStringBuilder.ToString();
            }

            return pReturn;
        }

        public static string SHA384_EncryptToString(string phrase)
        {
            string pReturn = string.Empty;

            using (SHA384 sha384Hash = SHA384.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha384Hash.ComputeHash(Encoding.UTF8.GetBytes(phrase));

                // Convert byte array to a string   
                StringBuilder pStringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    pStringBuilder.Append(bytes[i].ToString("x2"));
                }

                pReturn = pStringBuilder.ToString();
            }

            return pReturn;
        }

        public static string SHA512_EncryptToString(string phrase)
        {
            string pReturn = string.Empty;

            using (SHA512 sha512Hash = SHA512.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(phrase));

                // Convert byte array to a string   
                StringBuilder pStringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    pStringBuilder.Append(bytes[i].ToString("x2"));
                }

                pReturn = pStringBuilder.ToString();
            }

            return pReturn;
        }
        private string SHA512Hash(string data)
        {
            SHA512 sha = new SHA512Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        public static string Encrypt(string data)
        {
            var str = data + "TEST";
            var result = new SHA512Managed().ComputeHash(Encoding.UTF8.GetBytes(str));
            return Convert.ToBase64String(result);
        }
    }

    public class SystemLog
    {
        // ip Address 가져오기
        public static string Get_IpAddress()
        {
            string ip = "";
            IPHostEntry pIPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var item in pIPHostEntry.AddressList)
            {
                if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ip = item.ToString();
                }
            }

            return ip;

            // 권장하지 않는 방법이라함 .net 2.0버전
            //IPHostEntry host = Dns.GetHostByName(Dns.GetHostName());
            //string myip = host.AddressList[0].ToString();
            //return myip;
        }
        // Mac Address 가져오기
        public static string Get_MacAddress()
        {
            string MacAddress = "";

            MacAddress = NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString();
            MacAddress = MacAddress.Substring(0, 2) + ":" + MacAddress.Substring(2, 2) + ":" + MacAddress.Substring(4, 2) + ":" + MacAddress.Substring(6, 2) + ":" + MacAddress.Substring(8, 2) + ":" + MacAddress.Substring(10, 2);

            return MacAddress;
        }
        // PC Name 가져오기
        public static string Get_PcName()
        {
            string PCName = "";
            PCName = Environment.MachineName;

            return PCName;
        }

        public class SimpleLogger
        {
            private static readonly object locker = new object();

            public void WriteToLog(string fileName, string text)
            {
                lock (locker)
                {
                    StreamWriter SW;

                    //해당 파일이 있을 때
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + fileName))
                    {
                        FileInfo flinfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + fileName);

                        //string strFileSize = GetFileSize(flinfo.Length);

                        //500MB를 기준으로 로그 파일 초기화 시킨다. 500MB = 52,428,800 byte
                        if (flinfo.Length > 52428800)
                        {
                            //초기화. 해당 파일을 지우고 새로 만든다.
                            File.Delete(AppDomain.CurrentDomain.BaseDirectory + fileName);

                            SW = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + fileName);
                            SW.WriteLine(text);
                            SW.Close();

                            SW.Dispose();
                        }
                        else
                        {
                            //덮어 씌우기
                            SW = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + fileName);
                            SW.WriteLine(text);
                            SW.Close();

                            SW.Dispose();
                        }
                    }
                    //해당 파일일 없을때
                    else
                    {
                        SW = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + fileName);
                        SW.WriteLine(text);
                        SW.Close();

                        SW.Dispose();
                    }
                }
            }
        }

    }
    public class My_Settings
    {
        public string SERVER_IP { get; set; }       
        public string DB_PORT { get; set; }
        public string USER_ID { get; set; }
        public string THEME { get; set; }
        public string MQTT { get; set; }
        public int FONT_SIZE { get; set; }
        public string FONT_TYPE { get; set; }
        public string MULTI_LANGUAGE { get; set; }

        public bool USER_ID_SAVE { get; set; }

        public string DB_IP { get; set; }
        public string DB_NAME { get; set; }
        public string DB_ID { get; set; }
        public string DB_TYPE { get; set; }    
        public string PortName { get; set; }
        public string BaudRate { get; set; }
        public string DataSize { get; set; }
        public string Parity { get; set; }
        public string Handshake { get; set; }

    }

    public class DataGridViewUtil
    {
        /// <summary>
        /// 그리드뷰 컬럼 추가 메서드
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="headerText"></param>
        /// <param name="dataPropertyName"></param>
        /// <param name="visibility"></param>
        /// <param name="colWidth"></param>
        /// <param name="textAlign"></param>
        /// <param name="readOnly"></param>
        public static void AddNewColumnToDataGridView(DataGridView dgv, string headerText, string dataPropertyName, bool visibility, int colWidth = 100, DataGridViewContentAlignment textAlign = DataGridViewContentAlignment.MiddleCenter, bool readOnly = true,bool essential = false)
        {
            DataGridViewTextBoxColumn gridCol = new DataGridViewTextBoxColumn();
            gridCol.HeaderText = headerText;
            gridCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            gridCol.Resizable = DataGridViewTriState.False;
            gridCol.DataPropertyName = dataPropertyName; 
            gridCol.Name = dataPropertyName;
            gridCol.Width = colWidth;
            gridCol.Visible = visibility;
            gridCol.ValueType = typeof(string);
            gridCol.ReadOnly = readOnly;

         
            gridCol.DefaultCellStyle.Alignment = textAlign;
            dgv.Columns.Add(gridCol);

            if (essential)
            {
                //DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
                //headerStyle.ForeColor = Color.Red; // Set the desired font color
                //dgv.Columns[dataPropertyName].HeaderCell.Style.ApplyStyle(headerStyle);

                dgv.Columns[dataPropertyName].HeaderCell.Style.ForeColor = Color.Red;
            }

            //dgv.Refresh();
        }
        public static void AddNewColumnToDataGridViewDateTime(DataGridView dgv, string headerText, string dataPropertyName, bool visibility, int colWidth = 100, DataGridViewContentAlignment textAlign = DataGridViewContentAlignment.MiddleCenter, bool readOnly = true)
        {

            DataGridViewTextBoxColumn gridCol = new DataGridViewTextBoxColumn();
            gridCol.HeaderText = headerText;
            gridCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            gridCol.Resizable = DataGridViewTriState.False;
            gridCol.DataPropertyName = dataPropertyName;
            gridCol.Width = colWidth;
            gridCol.Visible = visibility;
            gridCol.ValueType = typeof(DateTime?);
            gridCol.ReadOnly = readOnly;
            gridCol.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            gridCol.DefaultCellStyle.Alignment = textAlign;
            dgv.Columns.Add(gridCol);
        }
        /// <summary>
        /// 그리드뷰 초기 설정 메서드
        /// </summary>
        /// <param name="dgv"></param>
        public static void InitSettingGridView(DataGridView dgv)
        {
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgv.RowTemplate.Height = 50;
            dgv.ColumnHeadersHeight = 50;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv.DefaultCellStyle.SelectionBackColor = Color.White;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.RowHeadersVisible = false;
            dgv.EditMode = DataGridViewEditMode.EditOnEnter;
        }

  






        /// <summary>
        /// 그리드뷰 버튼컬럼 추가 메서드
        /// </summary>
        /// <param name="headerText"></param>
        /// <param name="dgv"></param>
        /// <param name="topMargin"></param>
        /// <param name="bottomMargin"></param>
        /// <returns></returns>
        public static int DataGridViewBtnSet(string headerText, DataGridView dgv, int topMargin = 100, int bottomMargin = 100)
        {
            DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
            btn1.HeaderText = headerText;
            btn1.Text = headerText;
            btn1.Width = 200;
            btn1.DefaultCellStyle.Padding = new Padding(5, topMargin, 5, bottomMargin);
            btn1.UseColumnTextForButtonValue = true;
            return dgv.Columns.Add(btn1);
        }
        /// <summary>
        /// 그리드뷰 버튼컬럼 추가 메서드
        /// </summary>
        /// <param name="headerText"></param>
        /// <param name="dgv"></param>
        /// <param name="topMargin"></param>
        /// <param name="bottomMargin"></param>
        /// <returns></returns>
        public static int DataGridViewBtnSet(string headerText, string text, DataGridView dgv, int topMargin = 37, int bottomMargin = 37)
        {
            DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
            btn1.HeaderText = headerText;
            btn1.Text = text;
            btn1.DefaultCellStyle.ForeColor = Color.Black;
            //btn1.FlatStyle = FlatStyle.Flat;
            //btn1.DefaultCellStyle.BackColor = Color.LightGray;

            //btn1.Width = 80;
            //btn1.DefaultCellStyle.Padding = new Padding(0,0,0,0);
            btn1.UseColumnTextForButtonValue = true;
            return dgv.Columns.Add(btn1);
        }
        /// <summary>
        /// 그리드뷰 이미지컬럼 추가 메서드
        /// </summary>
        /// <param name="headerText"></param>
        /// <param name="propertyName"></param>
        /// <param name="width"></param>
        /// <param name="dgv"></param>
        public static void DataGridViewImageSet(string headerText, string propertyName, int width, DataGridView dgv)
        {
            DataGridViewImageColumn img = new DataGridViewImageColumn();
            img.HeaderText = headerText;
            img.DataPropertyName = propertyName;
            img.ImageLayout = DataGridViewImageCellLayout.Zoom;
            img.Width = width;
            dgv.Columns.Add(img);
        }
        /// <summary>
        /// 그리드뷰 체크박스 컬럼 추가 메서드
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int DataGridViewCheckBoxSet(DataGridView dgv, string name)
        {
            DataGridViewCheckBoxColumn chb1 = new DataGridViewCheckBoxColumn();
            chb1.HeaderText = "        ";
            chb1.Name = name;
            chb1.Width = 60;
            chb1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            chb1.DefaultCellStyle.Padding = new Padding(0, 0, 0, 0);
            chb1.FlatStyle = FlatStyle.Flat;
            return dgv.Columns.Add(chb1);
        }
        /// <summary>
        /// 그리드뷰 체크박스 컬럼 추가 메서드
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int DataGridViewCheckBoxSet(DataGridView dgv, string name, string HeaderText)
        {
            CustomDataGridViewCheckBoxColumn chb1 = new CustomDataGridViewCheckBoxColumn();
            chb1.HeaderText = HeaderText;
            chb1.Name = name;
            //chb1.DataPropertyName= name;
            chb1.Width = 60;
            chb1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            chb1.DefaultCellStyle.Font = dgv.Font;
            chb1.DefaultCellStyle.Padding = new Padding(0, 0, 0, 0);
            
            chb1.CheckBoxSize = new Size(32, 32);
            chb1.FlatStyle = FlatStyle.Flat;
            return dgv.Columns.Add(chb1);
        }
        /// <summary>
        /// 그리드뷰 체크박스 컬럼 추가 메서드(컬럼헤더추가) -OJH
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="name"></param>
        /// <param name="HeaderText"></param>
        /// <param name="Width"></param>
        /// <returns></returns>
        public static int DataGridViewCheckBoxSet(DataGridView dgv, string name, string HeaderText = "        ", int Width = 100)
        {
            DataGridViewCheckBoxColumn chb1 = new DataGridViewCheckBoxColumn();
            chb1.HeaderText = HeaderText;
            chb1.Name = name;
            chb1.Width = Width;
            chb1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            chb1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            chb1.DefaultCellStyle.Padding = new Padding(0, 0, 0, 0);
            chb1.FlatStyle = FlatStyle.Flat;
            
            return dgv.Columns.Add(chb1);
        }
        /// <summary>
        /// 그리드뷰 행번호 추가 메서드 -OJH
        /// </summary>
        /// <param name="dataGridView"></param>
        public static void DataGridViewRowNumSet(DataGridView dataGridView)
        {
            dataGridView.RowPostPaint += DataGridView_RowPostPaint;
        }
        /// <summary>
        /// 그리드뷰 행번호 추가 이벤트 -OJH
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();
            var centerFormat = new StringFormat()
            { // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, grid.Font, SystemBrushes.ControlText, headerBounds, centerFormat); ;

        }
    }
    public class CustomDataGridViewCheckBoxColumn : DataGridViewCheckBoxColumn
    {
        public CustomDataGridViewCheckBoxColumn() : base() =>
            CellTemplate = new CustomDataGridViewCheckBoxCell();

        public override DataGridViewCell CellTemplate
        {
            get => base.CellTemplate;
            set
            {
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(CustomDataGridViewCheckBoxCell)))
                    throw new InvalidCastException("CustomDataGridViewCheckBoxCell.");

                base.CellTemplate = value;
            }
        }

        [Category("Appearance")]
        [DefaultValue(typeof(Size), "17, 17")]
        [Description("The size of the check box.")]
        public Size CheckBoxSize { get; set; } = new Size(17, 17);

        // We should copy the new properties.
        public override object Clone()
        {
            var c = base.Clone() as CustomDataGridViewCheckBoxColumn;
            c.CheckBoxSize = CheckBoxSize;
            return c;
        }
    }
    public class CustomDataGridViewCheckBoxCell : DataGridViewCheckBoxCell
    {
        private Rectangle curCellBounds;
        private Rectangle checkBoxRect;

        public CustomDataGridViewCheckBoxCell() : base() { }

        protected override void Paint(
            Graphics g,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates elementState,
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            // Paint default except the check box parts.
            var parts = paintParts & ~(DataGridViewPaintParts.ContentForeground
            | DataGridViewPaintParts.ContentBackground);

            base.Paint(g,
                clipBounds,
                cellBounds,
                rowIndex,
                elementState,
                value,
                formattedValue,
                errorText,
                cellStyle,
                advancedBorderStyle,
                parts);

            if (curCellBounds != cellBounds)
            {
                // To get the box size...
                var col = OwningColumn as CustomDataGridViewCheckBoxColumn;

                curCellBounds = cellBounds;
                // ToDo: Use col.DefaultCellStyle.Alignment or
                // DataGridView.ColumnHeadersDefaultCellStyle.Alignment
                // to position the box. MiddleCenter here...
                checkBoxRect = new Rectangle(
                    (cellBounds.Width - col.CheckBoxSize.Width) / 2 + cellBounds.X,
                    (cellBounds.Height - col.CheckBoxSize.Height) / 2 + cellBounds.Y,
                    col.CheckBoxSize.Width,
                    col.CheckBoxSize.Height);
            }


            ControlPaint.DrawCheckBox(g, checkBoxRect, (bool)formattedValue
                ? ButtonState.Checked | ButtonState.Flat
                : ButtonState.Flat);
            //ControlPaint.DrawCheckBox(g, checkBoxRect, true
            //    ? ButtonState.Checked | ButtonState.Flat
            //    : ButtonState.Flat);
        }

        // In case you don't use the `Alignment` property to position the 
        // box. This is to disallow toggling the state if you click on the
        // original content area outside the drawn box.
        protected override void OnContentClick(DataGridViewCellEventArgs e)
        {
            if (!ReadOnly &&
                checkBoxRect.Contains(DataGridView.PointToClient(Cursor.Position)))
                base.OnContentClick(e);
        }

        protected override void OnContentDoubleClick(DataGridViewCellEventArgs e)
        {
            if (!ReadOnly &&
                checkBoxRect.Contains(DataGridView.PointToClient(Cursor.Position)))
                base.OnContentDoubleClick(e);
        }

        // Toggle the checked state by mouse clicks...
        protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (!ReadOnly && e.Button == MouseButtons.Left &&
                checkBoxRect.Contains(DataGridView.PointToClient(Cursor.Position)))
            {
                Value = Value == null || !Convert.ToBoolean(Value);
                DataGridView.RefreshEdit();
                DataGridView.NotifyCurrentCellDirty(true);
            }
        }

        // ... and Space key...
        protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
        {
            base.OnKeyDown(e, rowIndex);
            if (!ReadOnly && e.KeyCode == Keys.Space)
            {
                Value = Value == null || !Convert.ToBoolean(Value.ToString());
                DataGridView.RefreshEdit();
                DataGridView.NotifyCurrentCellDirty(true);
            }
        }
    }
   

    public class LoginEntity
    {
        public string user_account { get; set; }
        public string user_name { get; set; }
        public string user_password { get; set; }

        public string user_newpassword { get; set; }
    }

    public class SystemLogEntity
    {
        #region Property

        //사용자 설정 엔티티
        public string user_account { get; set; }
        public string user_ip { get; set; }
        public string user_mac { get; set; }
        public string user_pc { get; set; }
        public string event_type { get; set; }
        public string event_log { get; set; }
        public string menu_id { get; set; }
        #endregion

        #region 생성자 - SystemLogInfoEntity()

        public SystemLogEntity() { }

        #endregion

        #region 생성자 - SystemLogInfoEntity(pSystemLogInfoEntity)

        public SystemLogEntity(SystemLogEntity pSystemLogEntity)
        {
            user_account = pSystemLogEntity.user_account;
            user_ip = pSystemLogEntity.user_ip;
            user_mac = pSystemLogEntity.user_mac;
            user_pc = pSystemLogEntity.user_pc;
            event_type = pSystemLogEntity.event_type;
            event_log = pSystemLogEntity.event_log;
            menu_id = pSystemLogEntity.menu_id;
        }

        #endregion
    }

    public class RawPrinterHelper
    {
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            IntPtr hPrinter;
            DOCINFOA di = new DOCINFOA();
            Int32 dwError = 0, dwWritten = 0;
            bool success = false;

            di.pDocName = "ZPL Document";
            di.pDataType = "RAW";

            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    if (StartPagePrinter(hPrinter))
                    {
                        success = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            if (!success)
            {
                dwError = Marshal.GetLastWin32Error();
                throw new System.ComponentModel.Win32Exception(dwError);
            }
            return success;
        }
        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            byte[] rawData = Encoding.Default.GetBytes(szString);
            pBytes = Marshal.AllocCoTaskMem(rawData.Length);
            Marshal.Copy(rawData, 0, pBytes, rawData.Length);
            dwCount = rawData.Length;
            try
            {
                SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            }
            finally
            {
                Marshal.FreeCoTaskMem(pBytes);
            }
            return true;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)] public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)] public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)] public string pDataType;
        }
    }


    public class CSUtil
    {
        bool logresult = false;

        public CSUtil()
        {
            try
            {

            }
            catch (Exception ex)
            {
            }
        }


        private string GetIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddr = string.Empty;
            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddr = host.AddressList[i].ToString();
                }
            }
            return ipAddr;
        }


        // 컨테이너 하위의 컨트롤들을 모두 찾는다.. (현재 사용안함)
        /// <summary>
        /// 선택한 컨테이너의 하위에서 지정한 컨트롤 이름과 일치하는 컨트롤을 찾는다.
        /// 단 시작 하는 0번째부터 비교한다.
        /// </summary>
        /// <param name="containerControl">루트 컨테이너</param>
        /// <param name="subname">비교할 문자열</param>
        /// <returns>Control[]</returns>
        public Control[] GetAllControlsUsingRecursive(Control containerControl, string subname)
        {
            List<Control> allControls = new List<Control>();

            foreach (Control control in containerControl.Controls)
            {
                //자식 컨트롤을 컬렉션에 추가한다
                if (control.Name.Length >= subname.Length)
                {
                    if (control.Name.Substring(0, subname.Length) == subname)
                        allControls.Add(control);

                    //만일 자식 컨트롤이 또 다른 자식 컨트롤을 가지고 있다면…
                    if (control.Controls.Count > 0)
                    {
                        //자신을 재귀적으로 호출한다
                        allControls.AddRange(GetAllControlsUsingRecursive(control, subname));
                    }
                }
            }
            //모든 컨트롤을 반환한다
            return allControls.ToArray();
        }

        /// <summary>
        /// 컨테이너 하위의 컨트롤을 검색해서 컨트롤을 찾는다.
        /// </summary>
        /// <param name="rootControl">루트 컨테이너 ex) tlpMain</param>
        /// <param name="controlID">찾을 컨트롤 이름</param>
        /// <returns>Control</returns>
        public Control FindControlRecursive(Control rootControl, string controlID)
        {
            if (rootControl.Name == controlID) return rootControl;

            foreach (Control controlToSearch in rootControl.Controls)
            {
                Control controlToReturn =
                    FindControlRecursive(controlToSearch, controlID);
                if (controlToReturn != null) return controlToReturn;
            }
            return null;
        }
        /// <summary>
        /// RESIZE INVOKE처리
        /// </summary>
        /// <param name="ctl"></param>
        delegate void delInvoke_Control_RESIZE(Control ctl);
        public void Invoke_Control_RESIZE(Control ctl)
        {
            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new delInvoke_Control_RESIZE(Invoke_Control_RESIZE), new object[] { ctl });
            }
            else
            {
                Font fnt = ctl.Font;

                try
                {
                    if (ctl.Height > 4)
                    {
                        string[] str = ctl.Text.Split('\n');

                        string maxText = string.Empty;
                        int maxlength = 0;

                        if (str.Length > 0)
                        {
                            maxText = str[0].Replace("\n", string.Empty);
                            maxlength = str[0].Replace("\n", string.Empty).Length;
                            for (int i = 0; i < str.Length; i++)
                            {
                                if (str[i].Replace("\n", string.Empty).Length > maxlength)
                                {
                                    maxlength = str[i].Replace("\n", string.Empty).Length + 1;
                                    maxText = str[i].Replace("\n", string.Empty);
                                }
                            }
                        }

                        int textWidth = System.Windows.Forms.TextRenderer.MeasureText(maxText, new Font(fnt.FontFamily, fnt.Size, fnt.Style)).Width;

                        if ((textWidth > 0 && (fnt.Size * ctl.Width / 72 / 2) - 2 < ctl.Height) && ctl.Width > textWidth)
                        {
                            bool result = false;

                            //크게 해준다.
                            while (ctl.Width > System.Windows.Forms.TextRenderer.MeasureText(maxText, new Font(fnt.FontFamily, fnt.Size, fnt.Style)).Width && ctl.Height > (fnt.Height * str.Length) && ctl.Height > System.Windows.Forms.TextRenderer.MeasureText(maxText, new Font(fnt.FontFamily, fnt.Size, fnt.Style)).Height * str.Length)
                            {
                                result = true;
                                fnt = new Font(fnt.FontFamily, fnt.Size + 0.5f, fnt.Style);
                            }
                            if (result)
                            {
                                fnt = new Font(fnt.FontFamily, fnt.Size - 0.5f, fnt.Style);
                            }
                        }
                        else
                        {
                            //작게 해준다
                            while (ctl.Width < System.Windows.Forms.TextRenderer.MeasureText(maxText, new Font(fnt.FontFamily, fnt.Size, fnt.Style)).Width || ctl.Height < System.Windows.Forms.TextRenderer.MeasureText(maxText, new Font(fnt.FontFamily, fnt.Size, fnt.Style)).Height * str.Length)
                            {
                                if (fnt.Size >= 0.6)
                                {
                                    fnt = new Font(fnt.FontFamily, fnt.Size - 0.5f, fnt.Style);
                                }
                                else
                                    break;
                            }
                        }

                        ctl.Font = fnt;


                    }
                    else
                        return;
                }
                catch (Exception ex)
                {
                    SaveLog(ex.ToString());
                }
            }
        }
        public void FontResize(Label lbl)
        {

            Font fnt = lbl.Font;

            try
            {
                if (lbl.Height > 4)
                {
                    string[] str = lbl.Text.Split('\n');

                    string maxText = string.Empty;
                    int maxlength = 0;

                    if (str.Length > 0)
                    {
                        maxText = str[0].Replace("\n", string.Empty);
                        maxlength = str[0].Replace("\n", string.Empty).Length + 1;
                        for (int i = 0; i < str.Length; i++)
                        {
                            if (str[i].Replace("\n", string.Empty).Length > maxlength)
                            {
                                maxlength = str[i].Replace("\n", string.Empty).Length;
                                maxText = str[i].Replace("\n", string.Empty);
                            }
                        }

                        if (maxText == "") return;

                        int textWidth = System.Windows.Forms.TextRenderer.MeasureText(maxText, new Font(fnt.FontFamily, fnt.Size, fnt.Style)).Width;

                        //if ((textWidth > 0 && (fnt.Size * lbl.Width / 72 / 2) - 2 < lbl.Height) && lbl.Width > textWidth)

                        int textHeight = System.Windows.Forms.TextRenderer.MeasureText(maxText, new Font(fnt.FontFamily, fnt.Size, fnt.Style)).Height * str.Length;
                        if (textHeight < lbl.Height && lbl.Width > textWidth)
                        {
                            bool result = false;

                            //크게 해준다.
                            while (lbl.Width > System.Windows.Forms.TextRenderer.MeasureText(maxText, new Font(fnt.FontFamily, fnt.Size, fnt.Style)).Width && lbl.Height > (fnt.Height * str.Length) && lbl.Height > System.Windows.Forms.TextRenderer.MeasureText(maxText, new Font(fnt.FontFamily, fnt.Size, fnt.Style)).Height * str.Length)
                            {
                                result = true;
                                fnt = new Font(fnt.FontFamily, fnt.Size + 0.5f, fnt.Style);
                                //lbl.Font = new Font(lbl.Font.FontFamily, lbl.Font.Size + 0.5f, lbl.Font.Style);
                            }
                            if (result)
                            {
                                fnt = new Font(fnt.FontFamily, fnt.Size - 0.5f, fnt.Style);
                                //lbl.Font = new Font(lbl.Font.FontFamily, lbl.Font.Size - 0.5f, lbl.Font.Style);
                            }
                        }
                        else
                        {
                            //작게 해준다
                            while (lbl.Width < System.Windows.Forms.TextRenderer.MeasureText(maxText, new Font(fnt.FontFamily, fnt.Size, fnt.Style)).Width || lbl.Height < System.Windows.Forms.TextRenderer.MeasureText(maxText, new Font(fnt.FontFamily, fnt.Size, fnt.Style)).Height * str.Length)
                            {
                                if (fnt.Size >= 0.6)
                                {
                                    fnt = new Font(fnt.FontFamily, fnt.Size - 0.5f, fnt.Style);
                                    //lbl.Font = new Font(lbl.Font.FontFamily, lbl.Font.Size - 0.5f, lbl.Font.Style);
                                }
                                else
                                    break;
                            }
                        }


                    }
                }
                else
                    return;


                lbl.Font = fnt;
            }
            catch (Exception ex)
            {
                SaveLog(ex.ToString());
            }
            //Application.DoEvents();
        }

        /// <summary>
        /// MAC 주소 가져오기
        /// </summary>
        //public ArrayList GetMACAddress()
        //{
        //    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
        //    ManagementObjectCollection moc = mc.GetInstances();

        //    ArrayList localIp = new ArrayList();;


        //    foreach (ManagementObject mo in moc)
        //    {
        //        if (mo["MacAddress"] != null)
        //        {
        //            if ((bool)mo["IPEnabled"] == true)
        //            {
        //                localIp.Add(mo["MacAddress"].ToString());
        //                //MessageBox.Show(mo["MacAddress"].ToString());

        //                //localIp =  (string[])mo["IpAddress"];
        //            }
        //        }
        //    }

        //    moc.Dispose();
        //    mc.Dispose();


        //    return localIp;
        //}
        /// <summary>
        /// ip 가져오기
        /// </summary>
        /// <returns></returns>
        public string GetLocalIP()
        {
            string _IP = null;
            System.Net.IPHostEntry _IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (System.Net.IPAddress _IPAddress in _IPHostEntry.AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    _IP = _IPAddress.ToString();
                }
            }
            return _IP;
        }
        delegate void delInvoke_Control_ComboBox(ComboBox ctl, ArrayList arr);
        public void Invoke_Control_ComboBox(ComboBox ctl, ArrayList arr)
        {

            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new delInvoke_Control_ComboBox(Invoke_Control_ComboBox), new object[] { ctl, arr });
            }
            else
            {
                ctl.DataSource = null;
                ctl.DisplayMember = "text";
                ctl.ValueMember = "value";
                ctl.DataSource = arr;
            }
        }



        /// <summary>
        /// 리스트박스의 0번째에 Text를 삽입해준다. (100개 이후는 삭제함)
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="str"></param>
        delegate void delInvoke_Control_log(ListBox ctl, string str);
        public void Invoke_Control_log(ListBox ctl, string str)
        {
            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new delInvoke_Control_log(Invoke_Control_log), new object[] { ctl, str });
            }
            else
            {

                ctl.Items.Insert(0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + str);
                SaveInOutLog(ctl.Name, str);
                if (ctl.Items.Count > 100)
                {
                    for (int i = 10; i < ctl.Items.Count; i++)
                    {
                        ctl.Items.RemoveAt(ctl.Items.Count - 1);
                    }

                }

            }
        }

        /// <summary>
        /// 리스트박스의 0번째에 Text를 삽입해준다. (100개 이후는 삭제함) DB 저장은 하지 않음
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="str"></param>
        delegate void delInvoke_Control_log_no(ListBox ctl, string str);
        public void Invoke_Control_log_no(ListBox ctl, string str)
        {
            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new delInvoke_Control_log_no(Invoke_Control_log_no), new object[] { ctl, str });
            }
            else
            {

                ctl.Items.Insert(0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + str);
                if (ctl.Items.Count > 100)
                {
                    for (int i = 10; i < ctl.Items.Count; i++)
                    {
                        ctl.Items.RemoveAt(ctl.Items.Count - 1);
                    }

                }

            }
        }

        delegate void delInvoke_LabelColor(Label ctl, string str, Color cl);
        /// <summary>
        /// Label 에 값과 글씨색상을 넣어준다.
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="str"></param>
        public void Invoke_LabelColor(Label ctl, string str, Color cl)
        {
            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new delInvoke_LabelColor(Invoke_LabelColor), new object[] { ctl, str, cl });
            }
            else
            {
                ctl.Text = str;
                ctl.ForeColor = cl;
            }
        }


        delegate void delInvoke_LabelBackColor(Label ctl, string str, Color cl);
        /// <summary>
        /// Label 에 값과 배경색상을 넣어준다.
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="str"></param>
        public void Invoke_LabelBackColor(Label ctl, string str, Color cl)
        {
            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new delInvoke_LabelBackColor(Invoke_LabelBackColor), new object[] { ctl, str, cl });
            }
            else
            {
                ctl.Text = str;
                ctl.BackColor = cl;
            }
        }


        delegate void delInvoke_Label(Label ctl, string str);
        /// <summary>
        /// Label 에 값을 넣어준다.
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="str"></param>
        public void Invoke_Label(Label ctl, string str)
        {
            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new delInvoke_Label(Invoke_Label), new object[] { ctl, str });
            }
            else
            {
                ctl.Text = str;
            }
        }

        delegate void delInvoke_Textbox(TextBox ctl, string str);
        /// <summary>
        /// Textbox 에 값을 넣어준다.
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="str"></param>
        public void Invoke_Textbox(TextBox ctl, string str)
        {
            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new delInvoke_Textbox(Invoke_Textbox), new object[] { ctl, str });
            }
            else
            {
                ctl.Text = str;
            }
        }

        delegate void delInvoke_Trackbar(TrackBar ctl, int str);
        /// <summary>
        /// Trackbar 에 값을 넣어준다.
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="str"></param>
        public void Invoke_Trackbar(TrackBar ctl, int str)
        {
            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new delInvoke_Trackbar(Invoke_Trackbar), new object[] { ctl, str });
            }
            else
            {
                ctl.Value = str;
            }
        }

        delegate int delInvoke_TrackbarR(TrackBar ctl);
        /// <summary>
        /// Trackbar 에 값을 넣어준다.
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="str"></param>
        public int Invoke_TrackbarR(TrackBar ctl)
        {
            int st = 0;

            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new delInvoke_TrackbarR(Invoke_TrackbarR), new object[] { ctl });
            }
            else
            {
                st = ctl.Value;
            }

            return st;
        }



        string res1;

        delegate string delInvoke_LabelText(Label ctl);
        /// <summary>
        /// Label의 값을 불러온다.
        /// </summary>
        /// <param name="ctl"></param>
        /// <returns></returns>
        public string Invoke_LabelText(Label ctl)
        {

            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new delInvoke_LabelText(Invoke_LabelText), new object[] { ctl });
            }
            else
            {
                res1 = ctl.Text;
            }

            return res1;
        }


        delegate void delInvoke_CtlBackColor(Control ctl, Color col);
        /// <summary>
        /// 컨트롤의 배경색을 변경해준다.
        /// </summary>
        /// <param name="ctl"></param>
        /// <returns></returns>
        public void Invoke_CtlBackColor(Control ctl, Color col)
        {

            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new delInvoke_CtlBackColor(Invoke_CtlBackColor), new object[] { ctl, col });
            }
            else
            {
                ctl.BackColor = col;
            }
        }

        delegate string delInvoke_ComboText(ComboBox ctl);
        /// <summary>
        /// 콤보박스의 선택한 값(Value)를 반환한다.
        /// </summary>
        /// <param name="ctl"></param>
        /// <returns></returns>
        public string Invoke_ComboText(ComboBox ctl)
        {

            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new delInvoke_ComboText(Invoke_ComboText), new object[] { ctl });
            }
            else
            {
                if (ctl.Items.Count > 0)
                    res1 = ctl.SelectedValue.ToString();
            }

            return res1;
        }

        delegate string delInvoke_ComboItemText(ComboBox ctl);
        /// <summary>
        /// 콤보박스의 선택한 이름(Item)을 반환한다.
        /// </summary>
        /// <param name="ctl"></param>
        /// <returns></returns>
        public string Invoke_ComboItemText(ComboBox ctl)
        {

            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new delInvoke_ComboItemText(Invoke_ComboItemText), new object[] { ctl });
            }
            else
            {
                if (ctl.Items.Count > 0)
                    res1 = ctl.SelectedItem.ToString();
            }

            return res1;
        }


        delegate void delInvoke_Pic(PictureBox ctl, Image img);
        /// <summary>
        /// 픽처박스의 이미지를 교체한다.
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="img"></param>
        public void Invoke_Pic(PictureBox ctl, Image img)
        {
            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new delInvoke_Pic(Invoke_Pic), new object[] { ctl, img });
            }
            else
            {
                ctl.Image = img;
            }
        }

        delegate void delInvoke_BACKGROUND_IMAGE(Control ctl, Image img);
        /// <summary>
        /// 컨트롤의 배경 이미지를 교체한다.
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="img"></param>
        public void Invoke_BACKGROUND_IMAGE(Control ctl, Image img)
        {
            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new delInvoke_BACKGROUND_IMAGE(Invoke_BACKGROUND_IMAGE), new object[] { ctl, img });
            }
            else
            {
                ctl.BackgroundImage = img;
            }
        }

        /// <summary>
        /// 로그 파일에 저장한다. 내부적으로 시간은 자동으로 넣어준다. 에러 내용만 넣을것.
        /// </summary>
        /// <param name="str">ex.Tostring()</param>
        public void SaveLog(string str)
        {
            string fileName = DateTime.Now.ToShortDateString();
            FileStream fs = new FileStream(Application.StartupPath + "\\" + fileName + ".txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.WriteLine("[" + DateTime.Now.ToString() + "] - " + str);
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// 입출 내역 로그를 저장한다.
        /// </summary>
        /// <param name="ControlNmae">입고/출고/수동입력 등 구분</param>
        /// <param name="str">로그내용</param>
        /// <param name="result">로그 작성여부 true/false</param>
        public void SaveInOutLog(string ControlNmae, string str)
        {
            if (logresult)
            {
                string fileName = "InOut_" + DateTime.Now.ToShortDateString();
                FileStream fs = new FileStream(Application.StartupPath + "\\" + fileName + ".txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.WriteLine("[" + DateTime.Now.ToString() + "," + ControlNmae + "] " + str);
                sw.Close();
                fs.Close();
            }
        }


        delegate void delInvoke_LabelPic(Label ctl, Image img);
        /// <summary>
        /// Label의 이미지를 변경한다.(배경아님)
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="img"></param>
        public void Invoke_LabelPic(Label ctl, Image img)
        {
            if (ctl.InvokeRequired)
            {
                ctl.Invoke(new delInvoke_LabelPic(Invoke_LabelPic), new object[] { ctl, img });
            }
            else
            {
                ctl.Image = img;
            }
        }

        delegate void delInvoke_SetLabelSeq(Label lbl, string str);
        /// <summary>
        /// 라벨의 텍스트를 변경하고 라벨의 사이즈에 맞에 폰트크기를 조정한다.
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="str"></param>
        public void SetLabelSeq(Label lbl, string str)
        {
            if (lbl != null)
            {
                if (lbl.InvokeRequired)
                {
                    lbl.Invoke(new delInvoke_SetLabelSeq(SetLabelSeq), new object[] { lbl, str });
                }
                else
                {
                    lbl.Text = str;
                    Invoke_Control_RESIZE(lbl);
                }
            }

        }






        public static String Bytes2String(byte[] byt, int intFromIndex, int intLength)
        {
            if (byt == null)
                return "";

            if (intFromIndex == 0 && intLength == 0)
                intLength = byt.Length;

            return Encoding.Default.GetString(byt, intFromIndex, intLength);
        }

        public static String Bytes2String(byte[] byt)
        {
            if (byt == null)
                return "";


            return Encoding.Default.GetString(byt);
        }


        public static string ByteToHexString(Byte prmData)
        {
            string returnValue = null;
            try
            {
                returnValue = prmData.ToString("X"); //2자를 만들기 위해.  1 -> 01
                if (returnValue.Length == 1)
                {
                    returnValue = "0" + returnValue;
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        /// <summary>
        /// Byte배열을 16진수문자열로 변환.   {1}{255} => 01FF
        /// </summary>
        /// <param name="prmData"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString(Byte[] prmData)
        {
            string returnValue = null;

            try
            {
                foreach (Byte b in prmData)
                {
                    returnValue += ByteToHexString(b);
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        /// <summary>
        /// 16진수문자열을 2자씩 분리후 Byte배열로 변환. 01FF => {1}{255} 
        /// </summary>
        /// <param name="prmHexString">16진수문자열(09AAFF01...)</param>
        /// <returns>Byte배열</returns>
        public static byte[] HexStringToByteArray(string prmHexString)
        {
            byte[] returnValue = null;
            int arrayLength = 0;

            prmHexString = prmHexString.Replace(" ", "");
            try
            {
                arrayLength = prmHexString.Length / 2 + prmHexString.Length % 2;
                returnValue = new byte[arrayLength];

                for (int i = 0; i < arrayLength; i++)
                {
                    string temp = prmHexString.Substring(i * 2);
                    //문자열의 길이가 홀수인 경우, 마지막에 문자열 길이가 1이됨
                    if (temp.Length >= 2)
                    {
                        returnValue[i] = byte.Parse(prmHexString.Substring(i * 2, 2), NumberStyles.HexNumber);
                    }
                    else
                    {
                        returnValue[i] = byte.Parse(prmHexString.Substring(i * 2, 1), NumberStyles.HexNumber);
                    }
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }

        static public string ToHex(string arg)
        {
            byte[] str = ToBytes(arg);
            string result = "";
            foreach (byte ch in str)
            {
                result += string.Format("{0:x2} ", ch);
            }

            return result;
        }

        static public byte[] ToBytes(string arg)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(arg);
        }


    }

    public class CRC_Modbus
    {
        public byte[] crc_table = new byte[512];

        public CRC_Modbus()
        {
            crc_table[0] = 0x0;
            crc_table[1] = 0xC1;
            crc_table[2] = 0x81;
            crc_table[3] = 0x40;
            crc_table[4] = 0x1;
            crc_table[5] = 0xC0;
            crc_table[6] = 0x80;
            crc_table[7] = 0x41;
            crc_table[8] = 0x1;
            crc_table[9] = 0xC0;
            crc_table[10] = 0x80;
            crc_table[11] = 0x41;
            crc_table[12] = 0x0;
            crc_table[13] = 0xC1;
            crc_table[14] = 0x81;
            crc_table[15] = 0x40;
            crc_table[16] = 0x1;
            crc_table[17] = 0xC0;
            crc_table[18] = 0x80;
            crc_table[19] = 0x41;
            crc_table[20] = 0x0;
            crc_table[21] = 0xC1;
            crc_table[22] = 0x81;
            crc_table[23] = 0x40;
            crc_table[24] = 0x0;
            crc_table[25] = 0xC1;
            crc_table[26] = 0x81;
            crc_table[27] = 0x40;
            crc_table[28] = 0x1;
            crc_table[29] = 0xC0;
            crc_table[30] = 0x80;
            crc_table[31] = 0x41;
            crc_table[32] = 0x1;
            crc_table[33] = 0xC0;
            crc_table[34] = 0x80;
            crc_table[35] = 0x41;
            crc_table[36] = 0x0;
            crc_table[37] = 0xC1;
            crc_table[38] = 0x81;
            crc_table[39] = 0x40;
            crc_table[40] = 0x0;
            crc_table[41] = 0xC1;
            crc_table[42] = 0x81;
            crc_table[43] = 0x40;
            crc_table[44] = 0x1;
            crc_table[45] = 0xC0;
            crc_table[46] = 0x80;
            crc_table[47] = 0x41;
            crc_table[48] = 0x0;
            crc_table[49] = 0xC1;
            crc_table[50] = 0x81;
            crc_table[51] = 0x40;
            crc_table[52] = 0x1;
            crc_table[53] = 0xC0;
            crc_table[54] = 0x80;
            crc_table[55] = 0x41;
            crc_table[56] = 0x1;
            crc_table[57] = 0xC0;
            crc_table[58] = 0x80;
            crc_table[59] = 0x41;
            crc_table[60] = 0x0;
            crc_table[61] = 0xC1;
            crc_table[62] = 0x81;
            crc_table[63] = 0x40;
            crc_table[64] = 0x1;
            crc_table[65] = 0xC0;
            crc_table[66] = 0x80;
            crc_table[67] = 0x41;
            crc_table[68] = 0x0;
            crc_table[69] = 0xC1;
            crc_table[70] = 0x81;
            crc_table[71] = 0x40;
            crc_table[72] = 0x0;
            crc_table[73] = 0xC1;
            crc_table[74] = 0x81;
            crc_table[75] = 0x40;
            crc_table[76] = 0x1;
            crc_table[77] = 0xC0;
            crc_table[78] = 0x80;
            crc_table[79] = 0x41;
            crc_table[80] = 0x0;
            crc_table[81] = 0xC1;
            crc_table[82] = 0x81;
            crc_table[83] = 0x40;
            crc_table[84] = 0x1;
            crc_table[85] = 0xC0;
            crc_table[86] = 0x80;
            crc_table[87] = 0x41;
            crc_table[88] = 0x1;
            crc_table[89] = 0xC0;
            crc_table[90] = 0x80;
            crc_table[91] = 0x41;
            crc_table[92] = 0x0;
            crc_table[93] = 0xC1;
            crc_table[94] = 0x81;
            crc_table[95] = 0x40;
            crc_table[96] = 0x0;
            crc_table[97] = 0xC1;
            crc_table[98] = 0x81;
            crc_table[99] = 0x40;
            crc_table[100] = 0x1;
            crc_table[101] = 0xC0;
            crc_table[102] = 0x80;
            crc_table[103] = 0x41;
            crc_table[104] = 0x1;
            crc_table[105] = 0xC0;
            crc_table[106] = 0x80;
            crc_table[107] = 0x41;
            crc_table[108] = 0x0;
            crc_table[109] = 0xC1;
            crc_table[110] = 0x81;
            crc_table[111] = 0x40;
            crc_table[112] = 0x1;
            crc_table[113] = 0xC0;
            crc_table[114] = 0x80;
            crc_table[115] = 0x41;
            crc_table[116] = 0x0;
            crc_table[117] = 0xC1;
            crc_table[118] = 0x81;
            crc_table[119] = 0x40;
            crc_table[120] = 0x0;
            crc_table[121] = 0xC1;
            crc_table[122] = 0x81;
            crc_table[123] = 0x40;
            crc_table[124] = 0x1;
            crc_table[125] = 0xC0;
            crc_table[126] = 0x80;
            crc_table[127] = 0x41;
            crc_table[128] = 0x1;
            crc_table[129] = 0xC0;
            crc_table[130] = 0x80;
            crc_table[131] = 0x41;
            crc_table[132] = 0x0;
            crc_table[133] = 0xC1;
            crc_table[134] = 0x81;
            crc_table[135] = 0x40;
            crc_table[136] = 0x0;
            crc_table[137] = 0xC1;
            crc_table[138] = 0x81;
            crc_table[139] = 0x40;
            crc_table[140] = 0x1;
            crc_table[141] = 0xC0;
            crc_table[142] = 0x80;
            crc_table[143] = 0x41;
            crc_table[144] = 0x0;
            crc_table[145] = 0xC1;
            crc_table[146] = 0x81;
            crc_table[147] = 0x40;
            crc_table[148] = 0x1;
            crc_table[149] = 0xC0;
            crc_table[150] = 0x80;
            crc_table[151] = 0x41;
            crc_table[152] = 0x1;
            crc_table[153] = 0xC0;
            crc_table[154] = 0x80;
            crc_table[155] = 0x41;
            crc_table[156] = 0x0;
            crc_table[157] = 0xC1;
            crc_table[158] = 0x81;
            crc_table[159] = 0x40;
            crc_table[160] = 0x0;
            crc_table[161] = 0xC1;
            crc_table[162] = 0x81;
            crc_table[163] = 0x40;
            crc_table[164] = 0x1;
            crc_table[165] = 0xC0;
            crc_table[166] = 0x80;
            crc_table[167] = 0x41;
            crc_table[168] = 0x1;
            crc_table[169] = 0xC0;
            crc_table[170] = 0x80;
            crc_table[171] = 0x41;
            crc_table[172] = 0x0;
            crc_table[173] = 0xC1;
            crc_table[174] = 0x81;
            crc_table[175] = 0x40;
            crc_table[176] = 0x1;
            crc_table[177] = 0xC0;
            crc_table[178] = 0x80;
            crc_table[179] = 0x41;
            crc_table[180] = 0x0;
            crc_table[181] = 0xC1;
            crc_table[182] = 0x81;
            crc_table[183] = 0x40;
            crc_table[184] = 0x0;
            crc_table[185] = 0xC1;
            crc_table[186] = 0x81;
            crc_table[187] = 0x40;
            crc_table[188] = 0x1;
            crc_table[189] = 0xC0;
            crc_table[190] = 0x80;
            crc_table[191] = 0x41;
            crc_table[192] = 0x0;
            crc_table[193] = 0xC1;
            crc_table[194] = 0x81;
            crc_table[195] = 0x40;
            crc_table[196] = 0x1;
            crc_table[197] = 0xC0;
            crc_table[198] = 0x80;
            crc_table[199] = 0x41;
            crc_table[200] = 0x1;
            crc_table[201] = 0xC0;
            crc_table[202] = 0x80;
            crc_table[203] = 0x41;
            crc_table[204] = 0x0;
            crc_table[205] = 0xC1;
            crc_table[206] = 0x81;
            crc_table[207] = 0x40;
            crc_table[208] = 0x1;
            crc_table[209] = 0xC0;
            crc_table[210] = 0x80;
            crc_table[211] = 0x41;
            crc_table[212] = 0x0;
            crc_table[213] = 0xC1;
            crc_table[214] = 0x81;
            crc_table[215] = 0x40;
            crc_table[216] = 0x0;
            crc_table[217] = 0xC1;
            crc_table[218] = 0x81;
            crc_table[219] = 0x40;
            crc_table[220] = 0x1;
            crc_table[221] = 0xC0;
            crc_table[222] = 0x80;
            crc_table[223] = 0x41;
            crc_table[224] = 0x1;
            crc_table[225] = 0xC0;
            crc_table[226] = 0x80;
            crc_table[227] = 0x41;
            crc_table[228] = 0x0;
            crc_table[229] = 0xC1;
            crc_table[230] = 0x81;
            crc_table[231] = 0x40;
            crc_table[232] = 0x0;
            crc_table[233] = 0xC1;
            crc_table[234] = 0x81;
            crc_table[235] = 0x40;
            crc_table[236] = 0x1;
            crc_table[237] = 0xC0;
            crc_table[238] = 0x80;
            crc_table[239] = 0x41;
            crc_table[240] = 0x0;
            crc_table[241] = 0xC1;
            crc_table[242] = 0x81;
            crc_table[243] = 0x40;
            crc_table[244] = 0x1;
            crc_table[245] = 0xC0;
            crc_table[246] = 0x80;
            crc_table[247] = 0x41;
            crc_table[248] = 0x1;
            crc_table[249] = 0xC0;
            crc_table[250] = 0x80;
            crc_table[251] = 0x41;
            crc_table[252] = 0x0;
            crc_table[253] = 0xC1;
            crc_table[254] = 0x81;
            crc_table[255] = 0x40;
            crc_table[256] = 0x0;
            crc_table[257] = 0xC0;
            crc_table[258] = 0xC1;
            crc_table[259] = 0x1;
            crc_table[260] = 0xC3;
            crc_table[261] = 0x3;
            crc_table[262] = 0x2;
            crc_table[263] = 0xC2;
            crc_table[264] = 0xC6;
            crc_table[265] = 0x6;
            crc_table[266] = 0x7;
            crc_table[267] = 0xC7;
            crc_table[268] = 0x5;
            crc_table[269] = 0xC5;
            crc_table[270] = 0xC4;
            crc_table[271] = 0x4;
            crc_table[272] = 0xCC;
            crc_table[273] = 0xC;
            crc_table[274] = 0xD;
            crc_table[275] = 0xCD;
            crc_table[276] = 0xF;
            crc_table[277] = 0xCF;
            crc_table[278] = 0xCE;
            crc_table[279] = 0xE;
            crc_table[280] = 0xA;
            crc_table[281] = 0xCA;
            crc_table[282] = 0xCB;
            crc_table[283] = 0xB;
            crc_table[284] = 0xC9;
            crc_table[285] = 0x9;
            crc_table[286] = 0x8;
            crc_table[287] = 0xC8;
            crc_table[288] = 0xD8;
            crc_table[289] = 0x18;
            crc_table[290] = 0x19;
            crc_table[291] = 0xD9;
            crc_table[292] = 0x1B;
            crc_table[293] = 0xDB;
            crc_table[294] = 0xDA;
            crc_table[295] = 0x1A;
            crc_table[296] = 0x1E;
            crc_table[297] = 0xDE;
            crc_table[298] = 0xDF;
            crc_table[299] = 0x1F;
            crc_table[300] = 0xDD;
            crc_table[301] = 0x1D;
            crc_table[302] = 0x1C;
            crc_table[303] = 0xDC;
            crc_table[304] = 0x14;
            crc_table[305] = 0xD4;
            crc_table[306] = 0xD5;
            crc_table[307] = 0x15;
            crc_table[308] = 0xD7;
            crc_table[309] = 0x17;
            crc_table[310] = 0x16;
            crc_table[311] = 0xD6;
            crc_table[312] = 0xD2;
            crc_table[313] = 0x12;
            crc_table[314] = 0x13;
            crc_table[315] = 0xD3;
            crc_table[316] = 0x11;
            crc_table[317] = 0xD1;
            crc_table[318] = 0xD0;
            crc_table[319] = 0x10;
            crc_table[320] = 0xF0;
            crc_table[321] = 0x30;
            crc_table[322] = 0x31;
            crc_table[323] = 0xF1;
            crc_table[324] = 0x33;
            crc_table[325] = 0xF3;
            crc_table[326] = 0xF2;
            crc_table[327] = 0x32;
            crc_table[328] = 0x36;
            crc_table[329] = 0xF6;
            crc_table[330] = 0xF7;
            crc_table[331] = 0x37;
            crc_table[332] = 0xF5;
            crc_table[333] = 0x35;
            crc_table[334] = 0x34;
            crc_table[335] = 0xF4;
            crc_table[336] = 0x3C;
            crc_table[337] = 0xFC;
            crc_table[338] = 0xFD;
            crc_table[339] = 0x3D;
            crc_table[340] = 0xFF;
            crc_table[341] = 0x3F;
            crc_table[342] = 0x3E;
            crc_table[343] = 0xFE;
            crc_table[344] = 0xFA;
            crc_table[345] = 0x3A;
            crc_table[346] = 0x3B;
            crc_table[347] = 0xFB;
            crc_table[348] = 0x39;
            crc_table[349] = 0xF9;
            crc_table[350] = 0xF8;
            crc_table[351] = 0x38;
            crc_table[352] = 0x28;
            crc_table[353] = 0xE8;
            crc_table[354] = 0xE9;
            crc_table[355] = 0x29;
            crc_table[356] = 0xEB;
            crc_table[357] = 0x2B;
            crc_table[358] = 0x2A;
            crc_table[359] = 0xEA;
            crc_table[360] = 0xEE;
            crc_table[361] = 0x2E;
            crc_table[362] = 0x2F;
            crc_table[363] = 0xEF;
            crc_table[364] = 0x2D;
            crc_table[365] = 0xED;
            crc_table[366] = 0xEC;
            crc_table[367] = 0x2C;
            crc_table[368] = 0xE4;
            crc_table[369] = 0x24;
            crc_table[370] = 0x25;
            crc_table[371] = 0xE5;
            crc_table[372] = 0x27;
            crc_table[373] = 0xE7;
            crc_table[374] = 0xE6;
            crc_table[375] = 0x26;
            crc_table[376] = 0x22;
            crc_table[377] = 0xE2;
            crc_table[378] = 0xE3;
            crc_table[379] = 0x23;
            crc_table[380] = 0xE1;
            crc_table[381] = 0x21;
            crc_table[382] = 0x20;
            crc_table[383] = 0xE0;
            crc_table[384] = 0xA0;
            crc_table[385] = 0x60;
            crc_table[386] = 0x61;
            crc_table[387] = 0xA1;
            crc_table[388] = 0x63;
            crc_table[389] = 0xA3;
            crc_table[390] = 0xA2;
            crc_table[391] = 0x62;
            crc_table[392] = 0x66;
            crc_table[393] = 0xA6;
            crc_table[394] = 0xA7;
            crc_table[395] = 0x67;
            crc_table[396] = 0xA5;
            crc_table[397] = 0x65;
            crc_table[398] = 0x64;
            crc_table[399] = 0xA4;
            crc_table[400] = 0x6C;
            crc_table[401] = 0xAC;
            crc_table[402] = 0xAD;
            crc_table[403] = 0x6D;
            crc_table[404] = 0xAF;
            crc_table[405] = 0x6F;
            crc_table[406] = 0x6E;
            crc_table[407] = 0xAE;
            crc_table[408] = 0xAA;
            crc_table[409] = 0x6A;
            crc_table[410] = 0x6B;
            crc_table[411] = 0xAB;
            crc_table[412] = 0x69;
            crc_table[413] = 0xA9;
            crc_table[414] = 0xA8;
            crc_table[415] = 0x68;
            crc_table[416] = 0x78;
            crc_table[417] = 0xB8;
            crc_table[418] = 0xB9;
            crc_table[419] = 0x79;
            crc_table[420] = 0xBB;
            crc_table[421] = 0x7B;
            crc_table[422] = 0x7A;
            crc_table[423] = 0xBA;
            crc_table[424] = 0xBE;
            crc_table[425] = 0x7E;
            crc_table[426] = 0x7F;
            crc_table[427] = 0xBF;
            crc_table[428] = 0x7D;
            crc_table[429] = 0xBD;
            crc_table[430] = 0xBC;
            crc_table[431] = 0x7C;
            crc_table[432] = 0xB4;
            crc_table[433] = 0x74;
            crc_table[434] = 0x75;
            crc_table[435] = 0xB5;
            crc_table[436] = 0x77;
            crc_table[437] = 0xB7;
            crc_table[438] = 0xB6;
            crc_table[439] = 0x76;
            crc_table[440] = 0x72;
            crc_table[441] = 0xB2;
            crc_table[442] = 0xB3;
            crc_table[443] = 0x73;
            crc_table[444] = 0xB1;
            crc_table[445] = 0x71;
            crc_table[446] = 0x70;
            crc_table[447] = 0xB0;
            crc_table[448] = 0x50;
            crc_table[449] = 0x90;
            crc_table[450] = 0x91;
            crc_table[451] = 0x51;
            crc_table[452] = 0x93;
            crc_table[453] = 0x53;
            crc_table[454] = 0x52;
            crc_table[455] = 0x92;
            crc_table[456] = 0x96;
            crc_table[457] = 0x56;
            crc_table[458] = 0x57;
            crc_table[459] = 0x97;
            crc_table[460] = 0x55;
            crc_table[461] = 0x95;
            crc_table[462] = 0x94;
            crc_table[463] = 0x54;
            crc_table[464] = 0x9C;
            crc_table[465] = 0x5C;
            crc_table[466] = 0x5D;
            crc_table[467] = 0x9D;
            crc_table[468] = 0x5F;
            crc_table[469] = 0x9F;
            crc_table[470] = 0x9E;
            crc_table[471] = 0x5E;
            crc_table[472] = 0x5A;
            crc_table[473] = 0x9A;
            crc_table[474] = 0x9B;
            crc_table[475] = 0x5B;
            crc_table[476] = 0x99;
            crc_table[477] = 0x59;
            crc_table[478] = 0x58;
            crc_table[479] = 0x98;
            crc_table[480] = 0x88;
            crc_table[481] = 0x48;
            crc_table[482] = 0x49;
            crc_table[483] = 0x89;
            crc_table[484] = 0x4B;
            crc_table[485] = 0x8B;
            crc_table[486] = 0x8A;
            crc_table[487] = 0x4A;
            crc_table[488] = 0x4E;
            crc_table[489] = 0x8E;
            crc_table[490] = 0x8F;
            crc_table[491] = 0x4F;
            crc_table[492] = 0x8D;
            crc_table[493] = 0x4D;
            crc_table[494] = 0x4C;
            crc_table[495] = 0x8C;
            crc_table[496] = 0x44;
            crc_table[497] = 0x84;
            crc_table[498] = 0x85;
            crc_table[499] = 0x45;
            crc_table[500] = 0x87;
            crc_table[501] = 0x47;
            crc_table[502] = 0x46;
            crc_table[503] = 0x86;
            crc_table[504] = 0x82;
            crc_table[505] = 0x42;
            crc_table[506] = 0x43;
            crc_table[507] = 0x83;
            crc_table[508] = 0x41;
            crc_table[509] = 0x81;
            crc_table[510] = 0x80;
            crc_table[511] = 0x40;
        }


        public int crc16(byte[] modbusframe, int Length)
        {
            int i;
            int index;
            int crc_Low = 0xFF;
            int crc_High = 0xFF;

            for (i = 0; i < Length; i++)
            {
                index = crc_High ^ (char)modbusframe[i];
                crc_High = crc_Low ^ crc_table[index];
                crc_Low = (byte)crc_table[index + 256];
            }

            return crc_High * 256 + crc_Low;
        }

    }

    public class Barcode_Class
    {
        public delegate void BarCodeReadComplete(object sender, ReadEventArgs e);

        public event BarCodeReadComplete Readed;

        public Barcode_Class(string pPortName)
        {
            if (!Port.IsOpen) //연결
            {

                //My_Settings my = utility.My_Settings_Get();

                Port.PortName = pPortName;
                Port.BaudRate = 9600;
                Port.StopBits = StopBits.One;
                Port.DataBits = 8;
                Port.Parity = Parity.None;
                Port.Handshake = Handshake.RequestToSend;


                try
                {
                    Port.Open();
                }
                catch (Exception err)
                {
                    //MessageBox.Show($"바코드 연결이 되어있지 않습니다({err.Message})");
                }
            }
            else  //연결끊기
            {
                Port.Close();
            }


        }

        public class ReadEventArgs : EventArgs
        {
            private string msg;

            public string ReadMsg
            {
                get { return msg; }
                set { msg = value; }
            }

        }


        public SerialPort _port;

        private SerialPort Port
        {
            get
            {
                if (_port == null)
                {
                    _port = new SerialPort();
                    _port.DataReceived += Port_DataReceived;
                }

                return _port;
            }
        }

        private StringBuilder _strings;
        private String Strings
        {
            set
            {
                if (_strings == null)
                    _strings = new StringBuilder(1024);

                _strings.AppendLine(value);
                if (Readed != null)
                {
                    ReadEventArgs args = new ReadEventArgs();
                    args.ReadMsg = _strings.ToString();
                    Readed(this, args);
                    _strings.Clear();
                }
            }
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(500);

            string msg = Port.ReadExisting();

            Strings = msg;
        }

        public bool IsOPen
        {
            get { return Port.IsOpen; }
            set
            {
                if (value)
                {
                    //button1.Text = "연결 끊기";
                }
                else
                {
                    //button1.Text = "연결";
                }
            }
        }


        public void Port_Close()
        {
            Port.Close();
        }
    }
}
