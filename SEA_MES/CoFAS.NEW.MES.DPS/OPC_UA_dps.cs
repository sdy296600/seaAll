using System;
using System.IO;
using System.Windows.Forms;
using Opc.UaFx.Client;
using Opc.UaFx;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Drawing;
using Licenser = Opc.UaFx.Client.Licenser;
using uPLibrary.Networking.M2Mqtt;
using System.Text;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace CoFAS.NEW.MES.DPS
{
    public partial class OPC_UA_dps : Form
    {

        public static CoFAS_Log _pCoFAS_Log = new CoFAS_Log(Directory.GetCurrentDirectory() + "//LOG", "", 30, true);

        public DateTime _Last_Time = DateTime.Now;



        public OpcClient client;

        public List<OPC_Entity> _OPC_LIST = new List<OPC_Entity>();

        public DateTime _TIME_STAMP = DateTime.Now;

        public System.Threading.Timer _timer;
        public System.Threading.Timer _timer1;
       

        public OPC_UA_dps()
        {
            InitializeComponent();
            Licenser.LicenseKey = "AALOERR5OO7EKFNQCABINGCH6TYOVHPLFC2QCUIARDIAL75HZNAN4RAJ5SHIOGGSDKZXBJWGHUWWXQ5HKWI7OFVYYMERDPQDC7ZW7ZTLL3MPGPAZURMLT3ER66XKUM62YBBPBRGK6SBRI4DBXF4NGVKZUATMW3VI7EALG5FQN2ETIIHIGHODOCL2EPO55TO5D4GPJROX5FHSUSALQX56E6NCBRCDX35VJBWLQDD4QANXIWUKO7D3Q7SWDDL55ZZCSN7NLHKB3W5O524VIXFPLVJIYKM5U56IBOZSJDIVDPCPUVB36OHE6JP3WLL4ISRFWTSMQ4TM5F3E7E7IMJVA7RVRGVKKAGD3AO72HU42AGTIWUGKVF5HXNJA6JSB6SUWCQNLSIC47IO6WDA4ZOXBDNTDYSJXL677DNNWTXMJOESNPP5AVOGM7CFTL23WS75NVY6ETHPF4LZJ5OOOXIAKQ2KZP4VAYE53MLSZB34GAAZPLBDITGHP4TBLOAYBX676JK4O2CIKDM2IOYBJ5WAVQJNTH6IQJCE6MLQU5DHZ4SNPO4RUUMTSTYMHMIH3PTMHQOEK4G7QQYPV66CWRW5D5AWW5PER7GD4OH2RV3AMFASDTIQPHS5K47AGP4AU4YU3J7BK3T7OJYJ7IBZ5F4YSINDMSQYPOCKYNJQRWJ3EYNMXZ4KNXKFK6EYS42VDDI3K6GIPM25NP6DHFHOQU4P2STPEKQ74UDR4Y3OZS5GTJZ5IA3WDUG73WFQ2PN7THPVMZFERWTRVR2ND3VNIKYKM5JPEWPRGYXUR2NHKT2IF3PCFIT52SAQ22WTWE5TRH2FBGQW5JO7NRKNVFDDLD5UYUXAGQJFGWWV6KS753X3P6P6RYOKG5E2DU7UNJ76URHFWXI4Q6LRVFZGKGLVQJPUSRI2FTSCDEOEMSAL3S3NIZYPMLPDA4R6YO3ZDX27A535RPHRHVMVZQGIHI3VF3RQZUUAWJLNCO52SM4AGKW63CTMP2NUQ6VLRP72PNSME5ZEYQ3KH22WVRFSSO6W25KLSULW65B6JA6KQN2C2G5TFANSA6HVT4G5UJWGEGTH7QQIWKW4JHND37U6NDHTQNMUWDI3AUHB433GH7RLZBVAGQPLV4XYU4R3UH4IIO3HF24I2QLG2KZYJPQKC64NS447HKCRZB6SYY5HGRXBCKYHWNCTJETN7L7EBFWYGMJML56JRNUTSFJZBZSRJLCNY7WUX7735KHENS7UDN5YTR5QYLW2665ZWUFMYUSOOCE";
            ILicenseInfo license = Licenser.LicenseInfo;
            if (license.IsExpired) MessageBox.Show("The OPA UA SDK license is expired!");

        }



        private void Form1_Load(object sender, EventArgs e)
        {

            Tray_IconSetting();


            OPC_UA_Protocol_SERVER_ON();
            _timer = new System.Threading.Timer(CallBack);
            //timer = new System.Threading.Timer(CallBack1);
            _timer.Change(0, 600 * 1000);

           // _timer1.Change(0, 60 * 1000);
        }


        public void CallBack(Object state)
        {
            try
            {
                BeginInvoke(new MethodInvoker(delegate ()
                {

                    DataTable dt = Get_Eaton_Station_Date();
                    if (dt == null)
                    {
                        lbl_ok.Text = "0";
                        lbl_ng.Text = "0";
                    }
                    else
                    {
                        lbl_ok.Text = dt.Rows[0][1].ToString();
                        lbl_ng.Text = dt.Rows[1][1].ToString();
                    }
                    TimeSpan 시간차이 =  DateTime.Now - _Last_Time;

                    label4.Text = 시간차이.TotalMinutes.ToString();
                    //MY_Core core = new MY_Core();

                    //core.GET_FTP_LIST();


                    //TimeSpan 시간차이 =  DateTime.Now - _Last_Time;

                    //label4.Text = 시간차이.TotalMinutes.ToString();
                    //if (시간차이.TotalMinutes >= 5)
                    //{
                    //    _stop = true;
                    //}
                }));
            }
            catch (Exception pExcption)
            {

            }

        }

      

        public DataTable Get_Eaton_Station_Date()
        {
            try
            {
                string strcon = $"Server=172.22.4.11,60901;Database=HS_MES;UID=MesConnection;PWD=8$dJ@-!W3b-35;";
                //string strcon = "Server = 127.0.0.1; Database = HS_MES;UID = sa;PWD = coever1191!";
                SqlConnection con = new SqlConnection(strcon);
                string strSql_Insert = $@"select 'OK', count(id) AS QTY
                                            from [dbo].[OPC_MST_OK]
                                            where READ_DATE >
                                            (SELECT  CASE 
                                            WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                            THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
                                            ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END + ' 08:30')
                                            UNION 
                                            select 'NG',count(id) as QTY
                                            from [dbo].[OPC_MST_NG]
                                            where READ_DATE >
                                            (SELECT  CASE 
                                            WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                            THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
                                            ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END + ' 08:30')";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, con);


                // DB처리
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_Insert;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        #region ○ OPC_UA_Protocol

        private void OPC_UA_Protocol_SERVER_ON()
        {
            try
            {
                _paTCP_Light.BackColor = Color.Green;

                client = new OpcClient("opc.tcp://DESKTOP-UVPSU54:4990/FactoryTalkLinxGateway1");

                client.Connect();

                //client.WriteNode($"ns=2;s=TagGroup01#::[MES OPC]Program:MES_Data.MES_FSt040_QR_Double_Error", "1");
                //return;
                DataTable dt =Get_OPC("OPC00");
                foreach (DataRow item in dt.Rows)
                {

                    OPC_Entity opc_main = new OPC_Entity();
                    opc_main.code = item["code"].ToString();
                    opc_main.name = item["code_name"].ToString();
                    opc_main.node = $"TagGroup01#::[MES OPC]Program:MES_Data.{opc_main.name}";
                    client.SubscribeDataChange($"ns=2;s=TagGroup01#::[MES OPC]Program:MES_Data.{opc_main.name}", HandleDataChanged);
                    log_set($"ns=2;s=TagGroup01#::[MES OPC]Program:MES_Data.{opc_main.name}");
                    DataTable dt2 =Get_OPC(opc_main.code);

                    foreach (DataRow item1 in dt2.Rows)
                    {
                        OPC_Entity opc_sub = new OPC_Entity();
                        opc_sub.type = item1["code_type"].ToString();
                        opc_sub.code = item1["code"].ToString();
                        opc_sub.name = item1["code_name"].ToString();

                        opc_main.opc_list.Add(opc_sub);
            
                    }

                    _OPC_LIST.Add(opc_main);
                }


            }
            catch (Exception err)
            {
                log_set(err.Message);

                
                return;
            }
        }

        private void Browse(OpcNodeInfo node, int level = 0)
        {
            Console.WriteLine("{0}{1}({2})",
                    new string('.', level * 4),
                    node.Attribute(OpcAttribute.DisplayName).Value,
                    node.NodeId);

            level++;

            foreach (var childNode in node.Children())
                Browse(childNode, level);
        }
        private void HandleDataChanged(object sender, OpcDataChangeReceivedEventArgs e)
        {
            try
            {
                OpcMonitoredItem Opcitem = (OpcMonitoredItem)sender;

                TimeSpan 시간차이 =  DateTime.Now - _Last_Time;

                label4.Text = 시간차이.TotalMinutes.ToString();

                if (시간차이.TotalMinutes >= 5)
                {
                    Set_STOP(_Last_Time, DateTime.Now);
                }

                _Last_Time = DateTime.Now;

                var type = e.MonitoredItem.NodeId.ToString();

                //노드아이디 값 가져오기 ex) TagGroup01#[TestPLC]Test_Input
                var name = e.MonitoredItem.NodeId.Value.ToString();


                OPC_Entity opc = _OPC_LIST.Find(x => x.node == name);
                log_set(name);

                if (opc != null)
                {
                    if (opc.name.Contains("Data_Save"))
                    {
                        if (e.Item.Value.ToString() == "True")
                        {
                            DateTime time = DateTime.Now;
                            List<OPC_Insert_Entity> insertlist = new List<OPC_Insert_Entity>();

                            OPC_Insert_Entity main = new OPC_Insert_Entity();

                            main.OPC_TYPE = opc.name;
                            main.NAME = opc.name;
                            main.VALUE = e.Item.Value.ToString();
                            main.READ_DATE = time;
                            insertlist.Add(main);

                            foreach (OPC_Entity item in opc.opc_list)
                            {
                                OpcValue value = client.ReadNode($"ns=2;s=TagGroup01#::[MES OPC]Program:MES_Data.{item.name}");

                                OPC_Insert_Entity _Entity = new OPC_Insert_Entity();

                                _Entity.OPC_TYPE = opc.name;
                                _Entity.NAME = item.name;
                                _Entity.VALUE = value.ToString();
                                _Entity.READ_DATE = time;
                                insertlist.Add(_Entity);
                            }

                            foreach (OPC_Insert_Entity item in insertlist)
                            {
                                if (item.NAME == "MES_FSt040_Pass" &&
                                    item.VALUE == "True")
                                {
                                    foreach (OPC_Insert_Entity item2 in insertlist)
                                    {
                                        //if (item2.NAME == "MES_FSt030_Part_No")
                                        //{
                                        //    Set_OPC_OK(item2);
                                        //}
                                        if (item.NAME == "MES_FSt040_QR")
                                        {
                                            if (item.VALUE != "")
                                            {
                                                DataTable dataTable  =  Set_OPC_QR(item.VALUE);

                                                if (dataTable != null)
                                                {
                                                    //정상 일땐 0 

                                                    //불량 일땐 1 
                                                    if (dataTable.Rows[0][0].ToString() == "1")
                                                    {
                                                        client.WriteNode($"ns=2;s=TagGroup01#::[MES OPC]Program:MES_Data.MES_FSt040_QR_Double_Error", "1");
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                                if (item.NAME.Contains("Fail") &&
                                    item.VALUE == "True")
                                {
                                    Set_OPC_NG(item);
                                }
                            }


                            Set_OPC(insertlist);
                        }
                    }
                    else if (opc.name == "MES_FSt040_Pass")
                    {       
                        if (e.Item.Value.ToString() == "True")
                        {
                            DateTime time = DateTime.Now;
                            List<OPC_Insert_Entity> insertlist = new List<OPC_Insert_Entity>();

                            foreach (OPC_Entity item in opc.opc_list)
                            {
                                OpcValue value = client.ReadNode($"ns=2;s=TagGroup01#::[MES OPC]Program:MES_Data.{item.name}");

                                OPC_Insert_Entity _Entity = new OPC_Insert_Entity();

                                _Entity.OPC_TYPE = opc.name;
                                _Entity.NAME = item.name;
                                _Entity.VALUE = value.ToString();
                                _Entity.READ_DATE = time;
                                insertlist.Add(_Entity);
                            }

                            foreach (OPC_Insert_Entity item in insertlist)
                            {
                                if (item.NAME == "MES_FSt030_Part_No")
                                {
                                    Set_OPC_OK(item);
                                }

                            }
                   
                        }
                    }
                    else
                    {
                        OPC_Insert_Entity main = new OPC_Insert_Entity();
                        main.OPC_TYPE = opc.name;
                        main.NAME = opc.name;
                        main.VALUE = e.Item.Value.ToString();
                        main.READ_DATE = DateTime.Now;

                        List<OPC_Insert_Entity> insertlist = new List<OPC_Insert_Entity>();
                        insertlist.Add(main);
                        Set_OPC(insertlist);
                    }

                }

            }
            catch (Exception err)
            {
                log_set(err.Message);
            }
        }

        string _sqlcon = "Server=172.22.4.11,60901;Database=HS_MES;UID=MesConnection;PWD=8$dJ@-!W3b-35;";

        //string _sqlcon = "Server=127.0.0.1;Database=HS_MES;UID=sa;PWD=coever1191!;";
        public DataTable Get_OPC(string code_type)
        {
            DataTable dt = new DataTable();
            try
            {


                SqlConnection conn = new SqlConnection(_sqlcon);

                string strSql_Insert = $@"SELECT *
                                           FROM [dbo].[Code_Mst]
                                          WHERE code_type like  '%{code_type}%'";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);



                conn.Open();

                System.Data.SqlClient.SqlDataReader dr;

                dt = new System.Data.DataTable();

                dr = cmd_Insert.ExecuteReader();

                dt.Load(dr);

                conn.Close();

                return dt;

            }
            catch (Exception err)
            {
                return dt;
            }
        }

        public void Set_OPC(List<OPC_Insert_Entity> list)
        {
            try
            {
                foreach (OPC_Insert_Entity item in list)
                {
                    log_set(item.OPC_TYPE + "-" + item.NAME + " VALUE : " + item.VALUE);
                }


                SqlConnection conn = new SqlConnection(_sqlcon);

                // DB 연결

                conn.Open();

                foreach (OPC_Insert_Entity item in list)
                {
                    string strSql_Insert = $@"INSERT INTO [dbo].[OPC_MST]
                                             ([OPC_TYPE]
                                             ,[NAME]
                                             ,[VALUE]
                                             ,[READ_DATE])
                                       VALUES
                                             ('{ item.OPC_TYPE}'
                                             ,'{ item.NAME}'
                                             ,'{ item.VALUE}'
                                             ,'{ item.READ_DATE.ToString("yyyy-MM-dd HH:mm:ss")}')";

                    SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

                    cmd_Insert.ExecuteNonQuery();

                }

                conn.Close();

                conn.Dispose();
            }
            catch (Exception err)
            {
                log_set(err.Message);
            }
        }

        public void Set_OPC_OK(OPC_Insert_Entity item)
        {
            try
            {
                SqlConnection conn = new SqlConnection(_sqlcon);

                // DB 연결

                conn.Open();

                string strSql_Insert = $@"INSERT INTO [dbo].[OPC_MST_OK]
                                             ([OPC_TYPE]
                                             ,[NAME]
                                             ,[VALUE]
                                             ,[READ_DATE])
                                       VALUES
                                             ('{ item.OPC_TYPE}'
                                             ,'{ item.NAME}'
                                             ,'{ item.VALUE}'
                                             ,'{ item.READ_DATE.ToString("yyyy-MM-dd HH:mm:ss")}')";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

                cmd_Insert.ExecuteNonQuery();

                conn.Close();

                conn.Dispose();
            }
            catch (Exception err)
            {
                log_set(err.Message);
            }
        }
        public void Set_OPC_NG(OPC_Insert_Entity item)
        {
            try
            {
                SqlConnection conn = new SqlConnection(_sqlcon);

                // DB 연결

                conn.Open();

                string strSql_Insert = $@"INSERT INTO [dbo].[OPC_MST_NG]
                                             ([OPC_TYPE]
                                             ,[NAME]
                                             ,[VALUE]
                                             ,[READ_DATE])
                                       VALUES
                                             ('{ item.OPC_TYPE}'
                                             ,'{ item.NAME}'
                                             ,'{ item.VALUE}'
                                             ,'{ item.READ_DATE.ToString("yyyy-MM-dd HH:mm:ss")}')";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

                cmd_Insert.ExecuteNonQuery();

                conn.Close();

                conn.Dispose();
            }
            catch (Exception err)
            {
                log_set(err.Message);
            }
        }
        public void Set_OPC_STOP(OPC_Insert_Entity item)
        {
            try
            {
                SqlConnection conn = new SqlConnection(_sqlcon);

                // DB 연결

                conn.Open();

                string strSql_Insert = $@"INSERT INTO [dbo].[OPC_MST_STOP]
                                             ([OPC_TYPE]
                                             ,[NAME]
                                             ,[VALUE]
                                             ,[READ_DATE])
                                       VALUES
                                             ('{ item.OPC_TYPE}'
                                             ,'{ item.NAME}'
                                             ,'{ item.VALUE}'
                                             ,'{ item.READ_DATE.ToString("yyyy-MM-dd HH:mm:ss")}')";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

                cmd_Insert.ExecuteNonQuery();

                conn.Close();

                conn.Dispose();
            }
            catch (Exception err)
            {
                log_set(err.Message);
            }
        }
        public void Set_STOP(DateTime START_TIME, DateTime END_TIME)
        {
            try
            {
                SqlConnection conn = new SqlConnection(_sqlcon);

                // DB 연결

                conn.Open();


                string strSql_Insert = $@"IF NOT EXISTS (select *
                                         from
                                         (
                                         select DATEADD(MINUTE,(Cast(code_etc1 as int)), 
                                         (SELECT  CASE 
                                          WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                          THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
                                          ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd') END)
                                          ) as  시작
                                         , DATEADD(MINUTE,(Cast(code_etc2 as int)),DATEADD(MINUTE,(Cast(code_etc1 as int)), ( SELECT  CASE 
                                          WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                          THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
                                          ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd') END))) as 종료
                                         from Code_Mst 
                                        where 1=1
                                        and code_type = 'CD12'
                                        and code_etc1 != ''
                                        and use_yn = 'Y'
                                        )a
                                        where
                                           시작 between '{START_TIME.ToString("yyyy-MM-dd HH:mm:ss")}' and '{END_TIME.ToString("yyyy-MM-dd HH:mm:ss")}'
                                        or 종료 between '{START_TIME.ToString("yyyy-MM-dd HH:mm:ss")}' and '{END_TIME.ToString("yyyy-MM-dd HH:mm:ss")}' )
                                        BEGIN    
                                         select top 1 
                                         'CD12001'
                                         ,id
                                         ,0
                                         ,0
                                         ,'{START_TIME.ToString("yyyy-MM-dd HH:mm:ss")}'
                                         ,'{END_TIME.ToString("yyyy-MM-dd HH:mm:ss")}'
                                         ,0
                                         ,'자동수집'
                                         ,'Y'
                                         ,'DPS'
                                         ,GETDATE()
                                         ,'DPS'
                                         ,GETDATE()
                                         from [dbo].[PRODUCTION_INSTRUCT]
                                         where 1=1
                                         and START_INSTRUCT_DATE　is not null
                                         and END_INSTRUCT_DATE　is null
                                         order by START_INSTRUCT_DATE desc";
                                         //and FORMAT(START_INSTRUCT_DATE, 'yyyy-MM-dd') = 
                                         //( SELECT  CASE 
                                         // WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                         // THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
                                         // ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd') END);
                                         //END";
                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

                if(cmd_Insert.ExecuteNonQuery() == 0)
                {

                }

                conn.Close();

                conn.Dispose();
            }
            catch (Exception err)
            {
                log_set(err.Message);
            }
        }
       
        public DataTable Set_OPC_QR(string  pBARCODE)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this._sqlcon);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = $@"IF (NOT EXISTS (SELECT 1 FROM [dbo].[BARCODE_CHECK] WHERE BARCODE = '{pBARCODE}' ))
                                     BEGIN
                                         INSERT INTO[dbo].[BARCODE_CHECK]
                                                ([BARCODE])
                                          VALUES
                                                ('{pBARCODE}');
                                     
                                                SELECT 0;
                                     END
                                     ELSE
                                     BEGIN
                                                SELECT 1;                                   
                                     END";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

              
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();

                return dt;
            }
            catch (Exception err)
            {
                return null;
            }
        }
        public void log_set(string str)
        {

            listBox1.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " : " + str);
            _lciTCP_DATA.Text = DateTime.Now.ToString("HH:mm:ss") + " : " + str;
            _pCoFAS_Log.WLog(DateTime.Now.ToString("HH:mm:ss") + " : " + str);

            if (listBox1.Items.Count == 1000)
            {
                listBox1.Items.Clear();
            }
            else
            {
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }

        }
        public DataSet Get_생산진형현황()
        {

            try
            {
                string strcon = $"Server=172.22.4.11,60901;Database=HS_MES;UID=MesConnection;PWD=8$dJ@-!W3b-35;";
                //string strcon = "Server = 127.0.0.1; Database = HS_MES;UID = sa;PWD = coever1191!";
                SqlConnection con = new SqlConnection(strcon);
                string strSql_Insert = $@"select 
                                         ISNULL(C.OUT_CODE,'') AS OUT_CODE
                                        ,ISNULL(C.NAME,'')     AS NAME                                       
                                          from 
                                          (
                                           SELECT  CASE 
                                             WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                             THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
                                             ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END AS 'NOWTIME'
                                          ) A
                                          LEFT JOIN [dbo].[PRODUCTION_INSTRUCT] B ON A.NOWTIME = FORMAT(B.INSTRUCT_DATE, 'yyyy-MM-dd') AND B.USE_YN = 'Y'  AND B.START_INSTRUCT_DATE IS NOT NULL and B.END_INSTRUCT_DATE IS NULL
                                          LEFT JOIN [dbo].[STOCK_MST] C ON B.STOCK_MST_ID = C.ID
                                          ORDER BY B.START_INSTRUCT_DATE DESC;

 SET ANSI_WARNINGS OFF
 SET ARITHIGNORE ON
 SET ARITHABORT OFF                                          
SELECT 
 ISNULL((((SUM(AA.가용가능시간) - SUM(AA.입력비가동시간))/Convert(Decimal,SUM(AA.가용가능시간)))),0)*100 AS A
,ISNULL(SUM(AA.TOTAL)/SUM(가용생산수량),0)*100 AS P
,ISNULL(SUM(AA.OK)/SUM(AA.TOTAL),0)*100 AS Q   
,ISNULL((((SUM(AA.가용가능시간) - SUM(AA.입력비가동시간))/Convert(Decimal,SUM(AA.가용가능시간)))),0)
*ISNULL(SUM(AA.TOTAL)/SUM(가용생산수량),0)
*ISNULL(SUM(AA.OK)/SUM(AA.TOTAL),0)*100 AS OEE   
,ISNULL(SUM(AA.가용가능시간),0)         AS 가용가능시간
,ISNULL(SUM(AA.입력비가동시간),0)       AS 입력비가동시간
,ISNULL(SUM(AA.TOTAL),0)                AS TOTAL_QTY
,ISNULL(SUM(AA.OK),0)                   AS OK_QTY
,ISNULL(SUM(AA.NG),0)                   AS NG_QTY
,ISNULL(SUM(AA.가용생산수량),0)         AS TIME_QTY
,ISNULL(SUM(AA.목표생산수량),0)         AS 목표생산수량
,ISNULL(SUM(AA.효율생산수량),0)         AS 효율생산수량
,ISNULL(ISNULL(SUM(AA.TOTAL),0)/ISNULL(SUM(AA.효율생산수량),0),0)*100 AS PL
FROM 
(
SELECT
(DATEDIFF(SS,B.START_INSTRUCT_DATE,isnull(B.END_INSTRUCT_DATE,GETDATE())))-ISNULL(고정비가동.STOP_TIME,0) AS 가용가능시간
,ISNULL(비가동.STOP_TIME,0)      AS 입력비가동시간
,D.CYCLE_TIME
,(CASE 
 WHEN D.PERFORMANCE = 0
 THEN 1
 ELSE D.PERFORMANCE END) as PERFORMANCE
,(CASE 
 WHEN B.END_INSTRUCT_DATE is not null  
 THEN ISNULL(실적수량.OK_QTY,0) 
 ELSE ISNULL(실적수량.OK_QTY,0)    + ISNULL(수집정상.OK,0) 
 END  ) as OK
 ,(CASE 
 WHEN B.END_INSTRUCT_DATE is not null  
 THEN ISNULL(실적수량.NG_QTY,0) 
 ELSE ISNULL(실적수량.NG_QTY,0)    + ISNULL(수집불량.NG,0)
 END  ) as NG
  ,(CASE 
 WHEN B.END_INSTRUCT_DATE is not null  
 THEN ISNULL(실적수량.TOTAL_QTY,0)
 ELSE ISNULL(실적수량.TOTAL_QTY,0) + ISNULL((수집정상.OK + 수집불량.NG),0)
 END  ) as TOTAL
,((DATEDIFF(SS,B.START_INSTRUCT_DATE,isnull(B.END_INSTRUCT_DATE,GETDATE()))) -ISNULL(고정비가동.STOP_TIME,0) - ISNULL(비가동.STOP_TIME,0))/D.CYCLE_TIME AS 가용생산수량
,((DATEDIFF(SS,B.START_INSTRUCT_DATE,isnull(B.END_INSTRUCT_DATE,GETDATE())))-ISNULL(고정비가동.STOP_TIME,0))/D.CYCLE_TIME AS 목표생산수량
,(((DATEDIFF(SS,B.START_INSTRUCT_DATE,isnull(B.END_INSTRUCT_DATE,GETDATE())))-ISNULL(고정비가동.STOP_TIME,0))/D.CYCLE_TIME) * 
(CASE 
 WHEN D.PERFORMANCE = 0
 THEN 1
 ELSE D.PERFORMANCE END) AS 효율생산수량
from 
(
 SELECT  CASE 
   WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
   THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
   ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END AS 'NOWTIME'
) A
INNER JOIN [dbo].[PRODUCTION_INSTRUCT] B ON A.NOWTIME = FORMAT(B.INSTRUCT_DATE, 'yyyy-MM-dd') AND B.USE_YN = 'Y'  AND B.START_INSTRUCT_DATE IS NOT NULL
INNER JOIN [dbo].[PRODUCTION_PLAN] C ON B.PRODUCTION_PLAN_ID = C.ID AND C.LINE ='CD14002'
INNER JOIN [dbo].[WORK_CAPA] D ON B.WORK_CAPA_STD_OPERATOR = D.ID
LEFT JOIN 
(
SELECT SUM(STOP_TIME) AS STOP_TIME 
,PRODUCTION_INSTRUCT_ID
FROM [dbo].[EQUIPMENT_STOP] a
inner join Code_Mst b on a.TYPE = b.code and b.code_description =''
WHERE a.USE_YN = 'Y'
GROUP BY PRODUCTION_INSTRUCT_ID
)비가동 ON  B.ID = 비가동.PRODUCTION_INSTRUCT_ID
LEFT JOIN 
(
SELECT SUM(STOP_TIME) AS STOP_TIME 
,PRODUCTION_INSTRUCT_ID
FROM [dbo].[EQUIPMENT_STOP] a
inner join Code_Mst b on a.TYPE = b.code and b.code_description ='고정비가동'
WHERE a.USE_YN = 'Y'
GROUP BY PRODUCTION_INSTRUCT_ID
)고정비가동 ON  B.ID = 고정비가동.PRODUCTION_INSTRUCT_ID
LEFT JOIN 
  (
  SELECT a.ID,COUNT(b.id) AS OK
  FROM [dbo].[PRODUCTION_INSTRUCT] a
   LEFT JOIN [dbo].[OPC_MST_OK] b ON b.READ_DATE BETWEEN a.START_INSTRUCT_DATE AND ISNULL(a.END_INSTRUCT_DATE,GETDATE())
   where 1 = 1
 AND a.USE_YN = 'Y'
 AND a.START_INSTRUCT_DATE IS NOT NULL
 AND FORMAT(a.INSTRUCT_DATE, 'yyyy-MM-dd')　=
 (
 SELECT  CASE 
 WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
 THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
 ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END 
 )
   group by a.ID
   )수집정상 ON B.ID = 수집정상.ID
   LEFT JOIN 
  (
  SELECT a.ID,COUNT(b.id) AS NG
  FROM [dbo].[PRODUCTION_INSTRUCT] a
   LEFT JOIN [dbo].[OPC_MST_NG] b ON b.READ_DATE BETWEEN a.START_INSTRUCT_DATE AND ISNULL(a.END_INSTRUCT_DATE,GETDATE()) AND b.name = 'MES_FSt030_Pass'
   where 1 = 1
 AND a.USE_YN = 'Y'
 AND a.START_INSTRUCT_DATE IS NOT NULL
 AND FORMAT(a.INSTRUCT_DATE, 'yyyy-MM-dd')　=
 (
  SELECT  CASE 
  WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
  THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
  ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END 
  )
   group by a.ID
   )수집불량 ON B.ID = 수집불량.ID
    LEFT JOIN 	
      (	 
       SELECT a.ID
       ,SUM(b.OK_QTY) AS OK_QTY
       ,SUM(b.NG_QTY) AS NG_QTY
       ,SUM(b.TOTAL_QTY) AS TOTAL_QTY
         FROM [dbo].[PRODUCTION_INSTRUCT] a
         LEFT JOIN [dbo].[PRODUCTION_RESULT] b on a.ID = b.PRODUCTION_INSTRUCT_ID
         where 1 = 1
    AND	a.USE_YN = 'Y'
    AND a.START_INSTRUCT_DATE IS NOT NULL
    AND FORMAT(a.INSTRUCT_DATE, 'yyyy-MM-dd')　=
    (
     SELECT  CASE 
     WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
     THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
     ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END 
     )
         GROUP BY a.id
      )실적수량 ON B.ID = 실적수량.ID
) AA;";



                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, con);


                // DB처리
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_Insert;
                DataSet ds = new System.Data.DataSet();
                con.Open();
                da.Fill(ds);
                con.Close();
                return ds;

            }
            catch (Exception err)
            {
                return null;
            }
        }

        #endregion

        #region ○ 공통

        private void Tray_IconSetting()
        {
            TrayIcon.MouseDoubleClick += new MouseEventHandler(delegate
            {
                this.ShowInTaskbar = true;
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
            });

            showToolStripMenuItem.Click += new EventHandler(delegate
            {
                this.ShowInTaskbar = true;
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
            });

            exitToolStripMenuItem.Click += new EventHandler(delegate
            {

                if (MessageBox.Show("OPC_UA_dps 설비 데이터 연동을 종료 하시겠습니까?", "데이터 연동 종료확인", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    this.Dispose();
                }
            });

            TrayIcon.ContextMenuStrip = contextMenuStrip1;
        }



        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frm_OPC_List frm_OPC_ = new frm_OPC_List();
            frm_OPC_.ShowDialog();
        }
    }
    public class OPC_Entity
    {
        public string type { get; set; }

        public string code { get; set; }

        public string name { get; set; }

        public string node { get; set; }

        public List<OPC_Entity> opc_list { get; set; } = new List<OPC_Entity>();
    }

    public class OPC_Insert_Entity
    {
        public string OPC_TYPE { get; set; }

        public string NAME { get; set; }

        public string VALUE { get; set; }

        public DateTime READ_DATE { get; set; }

    }



}

