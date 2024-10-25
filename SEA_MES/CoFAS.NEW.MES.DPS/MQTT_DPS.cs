using System;
using System.IO;
using System.Windows.Forms;

using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Drawing;


using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Net.Http;
using Newtonsoft.Json;
using static CoFAS.NEW.MES.DPS.SensorInterface;
using MySql.Data.MySqlClient;

namespace CoFAS.NEW.MES.DPS
{
    public partial class MQTT_DPS : Form
    {
        //public CoFAS_WebUtilManager coFAS_WebUtilManager = new CoFAS_WebUtilManager();
        public static CoFAS_Log _pCoFAS_Log = new CoFAS_Log(Directory.GetCurrentDirectory() + "//LOG", "", 30, true);
        string strcon = $"Server=172.22.4.11,60901;Database=HS_MES;UID=MesConnection;PWD=8$dJ@-!W3b-35;";
       // string strcon = "Server = 127.0.0.1; Database = HS_MES;UID = sa;PWD = coever1191!";
        public System.Threading.Timer _timer;
       
        // 또는 다른 MQTT 브로커 주소
        MqttClient client = new MqttClient( "127.0.0.1");
     
        public MQTT_DPS()
        {
            InitializeComponent();
          

        }



        private void Form1_Load(object sender, EventArgs e)
        {

            Tray_IconSetting();
            //CallBack(null);
            client.Connect(Guid.NewGuid().ToString());
            _timer = new System.Threading.Timer(CallBack);

            _timer.Change(0, 30 * 1000);
        }


        public void CallBack(Object state)
        {
            try
            {
                BeginInvoke(new MethodInvoker(delegate ()
                {
                    DataTable dt = Get_작업조건현황();

                    List<MQTT_SET_DATE> list = new List<MQTT_SET_DATE>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MQTT_SET_DATE _DATE = new MQTT_SET_DATE();
                        _DATE.공정     = dt.Rows[i]["공정"].ToString();
                         _DATE.대분류  = dt.Rows[i]["대분류"].ToString();
                        _DATE.소분류   = dt.Rows[i]["소분류"].ToString();
                        _DATE.값       = dt.Rows[i]["값"].ToString();
                        _DATE.수집시간 = dt.Rows[i]["수집시간"].ToString();
                        list.Add(_DATE);
                    }
                    //string brokerAddress = "127.0.0.1"; // 또는 다른 MQTT 브로커 주소
                    //MqttClient client = new MqttClient(brokerAddress);
                    //client.Connect(Guid.NewGuid().ToString());
                    // 발행할 토픽 설정
                    string topic = "/event/c/DataGrid/DATAGRID";
                    // 발행할 메시지
                    string message = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                    // 발행
                    client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);

                    List<resource_sensor> list12 = new List<resource_sensor>();
                    DataTable dt2 = SELECT_DataTable();
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        resource_sensor _DATE = new resource_sensor();
                        _DATE.resource_code     = dt2.Rows[i]["resource_code     ".Trim()].ToString();
                        _DATE.sensor_unit       = dt2.Rows[i]["sensor_unit       ".Trim()].ToString();
                        _DATE.sensor_type       = dt2.Rows[i]["sensor_type       ".Trim()].ToString();
                        _DATE.icon_code         = dt2.Rows[i]["icon_code         ".Trim()].ToString();
                        _DATE.iot_code          = dt2.Rows[i]["iot_code          ".Trim()].ToString();
                        _DATE.sensor_float_digit= dt2.Rows[i]["sensor_float_digit".Trim()].ToString();
                        _DATE.m_limit_low       = dt2.Rows[i]["m_limit_low       ".Trim()].ToString();
                        _DATE.m_limit_high      = dt2.Rows[i]["m_limit_high      ".Trim()].ToString();
                        _DATE.limit_low         = dt2.Rows[i]["limit_low         ".Trim()].ToString();
                        _DATE.limit_high        = dt2.Rows[i]["limit_high        ".Trim()].ToString();

                        list12.Add(_DATE);
                    }
                    topic = "/event/c/DataGrid/DATAGRID1";

                
                    message = Newtonsoft.Json.JsonConvert.SerializeObject(list12);
                    client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);

                    DataSet ds = Get_생산진형현황();

                    if (ds != null && ds.Tables.Count != 0)
                    {
                        DataTable dt1 = ds.Tables[0];
                        DataRow dr = dt1.Rows[0];

                        topic = "/event/c/data_collection_digit/production_item";
                        message = $"{dr[1].ToString()}";
                        client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);

                        topic = "/event/c/data_collection_digit/Today";
                        message = $"Date : ({DateTime.Now.ToString("yyyy-MM-dd")})";
                        client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
               
                        dt = ds.Tables[1];
                        dr = dt.Rows[0];

                        topic = "/event/c/data_collection_digit/OEE";
                        message = Convert.ToDouble(dr["OEE"]).ToString("F2") + "%";
                        client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);


                        topic = "/event/c/data_collection_digit/p_plan";
                        message = Convert.ToDouble(dr["효율생산수량"]).ToString("F0"); 
                        client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);


                        topic = "/event/c/data_collection_digit/p_act";
                        message = Convert.ToDouble(dr["TOTAL_QTY"]).ToString("F0"); 
                        client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);


                        topic = "/event/c/data_collection_digit/t_plan";
                        message = Convert.ToDouble(dr["PL"]).ToString("F2") + "%";
                        client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);

                        double TOTAL_QTY = Convert.ToDouble(dr["TOTAL_QTY"]);
                        double OK_QTY = Convert.ToDouble(dr["OK_QTY"]);

                        topic = "/event/c/data_collection_digit/f_act";
                        message = ((OK_QTY / TOTAL_QTY) * 100).ToString("F2") + "%";
                        client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);



                        DataSet ds1 = Get_설비별불량현황();

                        if (ds1 != null && ds1.Tables.Count != 0)
                        {

                            DataTable dt3 = ds1.Tables[0];
                            
                            topic = "/event/c/data_collection_digit/불량율1";
                            message = Convert.ToDecimal(dt3.Rows[0]["불량율"]).ToString("F0") + "%"; //0.1% 0.11일때 노란색 0.11보다 크면 빨간색                   
                            client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                            
                            topic = "/event/c/data_collection_digit/불량율2";
                            message = Convert.ToDecimal(dt3.Rows[1]["불량율"]).ToString("F0") + "%"; //0.1%
                            client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                            
                            topic = "/event/c/data_collection_digit/불량율3";
                            message = Convert.ToDecimal(dt3.Rows[2]["불량율"]).ToString("F0") + "%"; //0.1%
                            client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                            
                            topic = "/event/c/data_collection_digit/불량율4";
                            message = Convert.ToDecimal(dt3.Rows[3]["불량율"]).ToString("F0") + "%"; //0.1%
                            client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                            
                            topic = "/event/c/data_collection_digit/불량율5";
                            message = Convert.ToDecimal(dt3.Rows[4]["불량율"]).ToString("F0") + "%"; //0.2% 0.22일때 노란색 0.22보다 크면 빨간색
                            client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                            
                            topic = "/event/c/data_collection_digit/불량율6";
                            message = Convert.ToDecimal(dt3.Rows[5]["불량율"]).ToString("F0") + "%"; //1.73% 1.903일때 노란색 1.903보다 크면 빨간색
                            client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                        }
                    }


                }));
            }
            catch (Exception pExcption)
            {
                MessageBox.Show(pExcption.ToString());
            }

        }
        public void CallBack1(Object state)
        {
            try
            {
                BeginInvoke(new MethodInvoker(delegate ()
                {
                    DataTable dt = Get_작업조건현황();

                    DateTime dateTime = DateTime.Now;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int x = 0; x < dt.Columns.Count; x++)
                        {
                            SensorInterfaceEntity  sensor = new SensorInterfaceEntity();

                            sensor.id = dt.Columns[x].ColumnName + i;
                            sensor.dt = dateTime.Ticks;
                            sensor.gr = dt.Columns[x].ColumnName + i;
                            sensor.vl = dt.Rows[i][x].ToString();
                            SensorInterfaceDevice_POST(sensor);
                        }
                        
                    }
                }));
            }
            catch (Exception pExcption)
            {

            }

        }
        public DataTable Get_작업조건현황()
        {

            try
            {

                SqlConnection con = new SqlConnection(strcon);
                string strSql_Insert = $@"select B.code_name as 공정
                                      ,A.code_name as 대분류
                                      ,A.code_etc1 as 소분류
                                      ,C.VALUE     as 값
                                      ,C.READ_DATE as 수집시간
                                  from [dbo].[Code_Mst] A
                                  INNER JOIN [dbo].[Code_Mst] B ON A.code_type = B.code
                                   LEFT JOIN 
                                   (
                                    SELECT A.*
                                    　FROM [dbo].[OPC_MST] A
                                    　INNER JOIN 
                                    　 (
                                    　 SELECT NAME ,MAX(READ_DATE) AS READ_DATE
                                    　 FROM [dbo].[OPC_MST]
                                    　GROUP BY NAME 
                                    　 ) B ON A.NAME = B.NAME AND  A.READ_DATE  = B.READ_DATE
                                   ) C ON  A.code_etc1 = C.NAME
                                  where 1=1
                                  and A.code_type like '%PR%'
                                  and A.code_type != 'PR00'
                                  ORDER BY A.code";



                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, con);


                // DB처리
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_Insert;
                DataTable ds = new System.Data.DataTable();
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

        public DataTable SELECT_DataTable()
        {
            try
            {
                string sql = @"SELECT 
                               resource_code,
                               sensor_unit,
                               sensor_type,
                               icon_code,
                               iot_code,
                               sensor_float_digit,
                               m_limit_low,
                               m_limit_high,
                               limit_low,
                               limit_high               
                             FROM resource_sensor WHERE remark = 'Y'";
                using (MySqlConnection conn = new MySqlConnection("Server=127.0.0.1;Database=coplatform;UID=root;PWD=developPassw@rd"))
                                            //_ConnectionString = "Server=10.10.10.216;Database=hansoldms;UID=coever;PWD=coever119!";
                {

                    MySqlDataAdapter DBAdapter = new MySqlDataAdapter();

                    MySqlCommand cmd = new MySqlCommand();

                    DBAdapter.SelectCommand = cmd;

                    cmd.Connection = conn;

                    cmd.CommandText = sql;

                    cmd.CommandType = CommandType.Text;

                    //파라메터 선언
                    //cmd.Parameters.Add(new MySqlParameter("@p_ProductionInstructp_Id", MySqlDbType.Int32));


                    //값할당
                    //cmd.Parameters["@p_ProductionInstructp_Id"].Value = productionInstructpId;


                    // DB처리
                    DataTable dt = new DataTable();

                    conn.Open();

                    DBAdapter.Fill(dt);

                    conn.Close();

                    return dt;
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public DataSet Get_생산진형현황()
        {

            try
            {

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
        public DataSet Get_설비별불량현황()
        {

            try
            {
               
                SqlConnection con = new SqlConnection(strcon);
                string strSql_Insert = $@"
                                SET ANSI_WARNINGS OFF
　　　　　　　　　　　　　　　　SET ARITHIGNORE ON
　　　　　　　　　　　　　　　　SET ARITHABORT OFF
　　　　　　　　　　　　　　　　SELECT A.COLUMN1          AS 공정
 　　　　　　　　　           　 ,A.NAME                  AS 공정명
              
　　　　　　　　　　　　　　　　,ISNULL((COUNT(C.ID)/(COUNT(B.ID) + COUNT(C.ID)))*100,0) AS 불량율
　　　　　　　　　　　　　　　　FROM [dbo].[EQUIPMENT] A
　　　　　　　　　　　　　　　　LEFT JOIN [dbo].[OPC_MST] B ON A.OUT_CODE+'_Pass' = B.NAME AND B.READ_DATE BETWEEN 
                                (SELECT  CASE 
                                 WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                 THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
                                 ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END + ' 08:30')
                                AND 
                                (SELECT  CASE 
                                 WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                 THEN FORMAT(DATEADD(DAY ,1, GETDATE()), 'yyyy-MM-dd')
                                 ELSE FORMAT(GETDATE(), 'yyyy-MM-dd')　END + ' 08:30')
　　　　　　　　　　　　　　　　LEFT JOIN [dbo].[OPC_MST] C ON A.OUT_CODE+'_Fail' = C.NAME AND C.READ_DATE BETWEEN 
                                (SELECT  CASE 
                                 WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                 THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
                                 ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END + ' 08:30')
                                AND 
                                (SELECT  CASE 
                                 WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                 THEN FORMAT(DATEADD(DAY ,1, GETDATE()), 'yyyy-MM-dd')
                                 ELSE FORMAT(GETDATE(), 'yyyy-MM-dd')　END + ' 08:30')
                                 WHERE A.ID IN(4,7,12,14,15,17)
                                 GROUP BY A.COLUMN1,A.NAME,A.ID
                                 ORDER BY A.ID";



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

                if (MessageBox.Show("MQTT_DPS 설비 데이터 연동을 종료 하시겠습니까?", "데이터 연동 종료확인", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    this.Dispose();
                }
            });

            TrayIcon.ContextMenuStrip = contextMenuStrip1;
        }



        #endregion

 
    }
    public class SensorInterface
    {
        public static HttpResponseMessage response = null;
        public static string result = string.Empty;

        public class SensorInterfaceEntity
        {

            public String id { get; set; }
            public String vl { get; set; }
            public long dt { get; set; }
            public String gr { get; set; }


        }
        public class SensorInterfaceEntity2
        {

            public string sensorId { get; set; }
            public string occurDate { get; set; }
            public string category { get; set; }
            public string value { get; set; }


        }
        /// <summary>
        /// ResourceSensor  조회
        /// </summary>
        /// <param name="entity"> Entity </param>
        /// <returns>string<result></returns>

        public static string EdgePCInterface_POST(SensorInterfaceEntity2 entity)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");

            sb.Append(JsonConvert.SerializeObject(entity));

            sb.Append("]");

            response = CoFAS_WebUtilManager.POSTAsync("EdgePCInterface", sb.ToString().Trim());

            return result = response.Content.ReadAsStringAsync().Result;
        }
        public static string SensorInterfaceDevice_POST(SensorInterfaceEntity entity)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");

            sb.Append(JsonConvert.SerializeObject(entity));

            sb.Append("]");

            response = CoFAS_WebUtilManager.POSTAsync("SensorInterfaceAutoCreate", sb.ToString().Trim());

            return result = response.Content.ReadAsStringAsync().Result;
        }
    }

    public class MQTT_SET_DATE
    {
        public string 공정 { get; set; }
        public string 대분류 { get; set; }
        public string 소분류 { get; set; }
        public string 값 { get; set; }
        public string 수집시간 { get; set; }
    }

    public class resource_sensor
    {

        public string resource_code { get; set; }
        public string sensor_unit { get; set; }
        public string sensor_type { get; set; }
        public string icon_code { get; set; }
        public string iot_code { get; set; }
        public string sensor_float_digit { get; set; }
        public string m_limit_low { get; set; }
        public string m_limit_high{ get; set; }
        public string limit_low { get; set; }
        public string limit_high { get; set; }
    }



}

