
using System;
using System.Data;
using System.Data.SqlClient;

namespace CoFAS.NEW.MES.Monitoring
{
    class DBClass
    {
        string ConnectionString2 = "Server = wms.tapex.co.kr;Database = HC_WMS;UID = sa;PWD = coever1191!";
      

        public virtual DataSet GET_Tapex_Monitoring_OUTList_R01()
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString2);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = "GET_Tapex_Monitoring_OUTList_R01";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add(new SqlParameter("@E_WHSeq".Trim(), SqlDbType.VarChar, 50));

                //cmd.Parameters["@E_WHSeq".Trim()].Value = ETRI_FaaS_2017.Properties.Settings.Default.WHSeq;

                // DB처리
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new System.Data.DataSet();
                con.Open();
                da.Fill(ds);
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

     

        public virtual DataTable GET_Tapex_Monitoring_ITEMList_R01(
              string _E_WHSeq
            , string _BD_CD
            , string _AREA_CD
            , string _item_code)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString2);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = "GET_Tapex_Monitoring_ITEMList_R01";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add(new SqlParameter("@E_WHSeq   ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@BD_CD     ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@AREA_CD   ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@ITEMCODE  ".Trim(), SqlDbType.VarChar, 50));

                cmd.Parameters["@E_WHSeq  ".Trim()].Value = _E_WHSeq;
                cmd.Parameters["@BD_CD    ".Trim()].Value = _BD_CD;
                cmd.Parameters["@AREA_CD  ".Trim()].Value = _AREA_CD;
                cmd.Parameters["@ITEMCODE ".Trim()].Value = _item_code;


                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
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

        public virtual DataTable USP_xComboBox_R10(string _ServieceName)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString2);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = "USP_xComboBox_R10";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add(new SqlParameter("@ServieceName ".Trim(), SqlDbType.VarChar, 50));



                cmd.Parameters["@ServieceName  ".Trim()].Value = _ServieceName;




                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
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

        public virtual DataTable USP_xPOPUP_R20(
               string _ServiceName
             , string _Parameter1
             , string _Parameter2
             , string _Parameter3
             , string _USE_YN)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString2);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = "USP_xPOPUP_R20";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add(new SqlParameter("@ServiceName".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@Parameter1 ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@Parameter2 ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@Parameter3 ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@USE_YN     ".Trim(), SqlDbType.VarChar, 50));



                cmd.Parameters["@ServiceName  ".Trim()].Value = _ServiceName;
                cmd.Parameters["@Parameter1   ".Trim()].Value = _Parameter1;
                cmd.Parameters["@Parameter2   ".Trim()].Value = _Parameter2;
                cmd.Parameters["@Parameter3   ".Trim()].Value = _Parameter3;
                cmd.Parameters["@USE_YN       ".Trim()].Value = _USE_YN;



                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
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
        public virtual DataSet USP_xPOPUP_R20_DataSet(
              string _ServiceName
            , string _Parameter1
            , string _Parameter2
            , string _Parameter3
            , string _USE_YN)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString2);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = "USP_xPOPUP_R20";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add(new SqlParameter("@ServiceName".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@Parameter1 ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@Parameter2 ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@Parameter3 ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@USE_YN     ".Trim(), SqlDbType.VarChar, 50));



                cmd.Parameters["@ServiceName  ".Trim()].Value = _ServiceName;
                cmd.Parameters["@Parameter1   ".Trim()].Value = _Parameter1;
                cmd.Parameters["@Parameter2   ".Trim()].Value = _Parameter2;
                cmd.Parameters["@Parameter3   ".Trim()].Value = _Parameter3;
                cmd.Parameters["@USE_YN       ".Trim()].Value = _USE_YN;



                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet dt = new System.Data.DataSet();
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
        public virtual DataTable GET_LOCATION(
             string _E_WHSeq
           , string _BD_CD
           , string _AREA_CD
           , string _CELL_CD
           , string _ItemSeq
            )
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString2);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = "GET_LOCATION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add(new SqlParameter("@E_WHSeq".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@BD_CD  ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@AREA_CD".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@CELL_CD".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@ItemSeq".Trim(), SqlDbType.VarChar, 50));



                cmd.Parameters["@E_WHSeq".Trim()].Value = _E_WHSeq;
                cmd.Parameters["@BD_CD  ".Trim()].Value = _BD_CD;
                cmd.Parameters["@AREA_CD".Trim()].Value = _AREA_CD;
                cmd.Parameters["@CELL_CD".Trim()].Value = _CELL_CD;
                cmd.Parameters["@ItemSeq".Trim()].Value = _ItemSeq;



                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
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
        
        public virtual DataTable USP_GET_Damayopaeg()
        {
            try
            {

                string strcon = "Server = 183.111.74.179,1433;Database = DAMAYO_MES;UID = Coever;PWD = dmy1234!@";
                SqlConnection con;
                con = new SqlConnection(strcon);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = @"select                              

           
								CASE  
                                 WHEN A.EQUIP_ID ='FG01' THEN '접착1' 
                                 WHEN A.EQUIP_ID ='FG02' THEN '접착2' 
								 WHEN A.EQUIP_ID ='FG03' THEN '접착3' 
								 WHEN A.EQUIP_ID ='FG04' THEN '접착4' 
								 WHEN A.EQUIP_ID ='FG05' THEN '접착6' 
							     WHEN A.EQUIP_ID ='FG06' THEN '8면기'  
								 WHEN A.EQUIP_ID ='FG07' THEN '3면기'
								 WHEN A.EQUIP_ID ='FG08' THEN '트레이' 
							     WHEN A.EQUIP_ID ='FG09' THEN '접착6'
								 WHEN A.EQUIP_ID ='FG10' THEN '접착7'
								 WHEN A.EQUIP_ID ='DC01' THEN '톱슨1'
								 WHEN A.EQUIP_ID ='DC02' THEN '톱슨2'
								 WHEN A.EQUIP_ID ='DC03' THEN '톱슨3'
								 WHEN A.EQUIP_ID ='DC04' THEN '톱슨4'     
								 WHEN A.EQUIP_ID ='DC05' THEN '톱슨5'      
                                 END AS 호기
                                ,B.PART_NAME                AS 제품명
                                ,ISNULL(B.WORK_ORDER_QTY,0) AS 계획수량
                                ,CASE A.EQUIP_ID  
                                 WHEN 'FG01' THEN 450000 
                                 WHEN 'FG02' THEN 450000 
								 WHEN 'FG03' THEN 300000 
								 WHEN 'FG04' THEN 300000 
								 WHEN 'FG05' THEN 300000 
							     WHEN 'FG06' THEN 450000  
								 WHEN 'FG07' THEN 450000  
								 WHEN 'FG08' THEN 400000 
							     WHEN 'FG09' THEN 300000
								 WHEN 'FG10' THEN 105000
                                 ELSE 400000             
                                 END AS 일일목표수량
                                ,ISNULL(WORK_QTY,0)         AS 금일생산실적
								,ISNULL(DAY_QTY,0)         AS 금일생산누계
                                ,ISNULL(C.M_QTY,0)          AS 월누계수량
                                ,B.WORK_END_TIME            AS 생산시간
                                from[dbo].[TB_EQUIP]
                                        A
                                left join
                                (
                                 select A.*
                                from VW_DAILY_WORK_LIST A
                                INNER JOIN
                                (
                                SELECT EQUIP_ID, MAX(WORK_END_TIME) AS WORK_END_TIME
                                FROM VW_DAILY_WORK_LIST
                                where WORK_DATE = FORMAT(GETDATE(), 'yyyy-MM-dd')
                                GROUP BY EQUIP_ID
                                )B ON A.EQUIP_ID = B.EQUIP_ID AND A.WORK_END_TIME =B.WORK_END_TIME
                                where WORK_DATE = FORMAT(GETDATE(), 'yyyy-MM-dd')
                                ) B ON A.EQUIP_ID  = B.EQUIP_ID
                                left join
                                (
                                select EQUIP_ID, SUM(WORK_QTY) AS M_QTY
                                from VW_DAILY_WORK_LIST
                                where WORK_DATE BETWEEN  FORMAT(GETDATE(), 'yyyy-MM') AND FORMAT(DATEADD(MONTH,1, GETDATE()), 'yyyy-MM')
                                GROUP BY EQUIP_ID
                                ) C ON B.EQUIP_ID  = C.EQUIP_ID 
								 left join
                                (
                                select EQUIP_ID, SUM(WORK_QTY) AS DAY_QTY
                                from VW_DAILY_WORK_LIST
                                where WORK_DATE = FORMAT(GETDATE(), 'yyyy-MM-dd')
                                GROUP BY EQUIP_ID
                                ) D ON B.EQUIP_ID  = D.EQUIP_ID 
                                WHERE 1=1
                              　AND A.PROC_ID IN(4,5)
                                AND A.EQUIP_SORT IS NOT NULL
                                AND A.IS_MAIN_EQUIP = 1
                                AND A.PROC_GROUP_1 IS NOT NULL
                                ORDER BY A.EQUIP_SORT					";
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
            catch (Exception ex)
            {

                return null;
            }
        }
        public virtual DataTable USP_GET_Damayopaeg2()
        {
            try
            {

                string strcon = "Server = 183.111.74.179,1433;Database = DAMAYO_MES;UID = Coever;PWD = dmy1234!@";
                SqlConnection con;
                con = new SqlConnection(strcon);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = @"select                              

           
								CASE  
                                 WHEN A.EQUIP_ID ='FG01' THEN '접착1' 
                                 WHEN A.EQUIP_ID ='FG02' THEN '접착2' 
								 WHEN A.EQUIP_ID ='FG03' THEN '접착3' 
								 WHEN A.EQUIP_ID ='FG04' THEN '접착4' 
								 WHEN A.EQUIP_ID ='FG05' THEN '접착6' 
							     WHEN A.EQUIP_ID ='FG06' THEN '8면기'  
								 WHEN A.EQUIP_ID ='FG07' THEN '3면기'
								 WHEN A.EQUIP_ID ='FG08' THEN '트레이' 
							     WHEN A.EQUIP_ID ='FG09' THEN '접착6'
								 WHEN A.EQUIP_ID ='FG10' THEN '접착7'
								 WHEN A.EQUIP_ID ='DC01' THEN '톱슨1'
								 WHEN A.EQUIP_ID ='DC02' THEN '톱슨2'
								 WHEN A.EQUIP_ID ='DC03' THEN '톱슨3'
								 WHEN A.EQUIP_ID ='DC04' THEN '톱슨4'     
								 WHEN A.EQUIP_ID ='DC05' THEN '톱슨5'      
                                 END AS 호기
                                ,B.PART_NAME                AS 제품명
                                ,ISNULL(B.WORK_ORDER_QTY,0) AS 계획수량
                                ,CASE A.EQUIP_ID  
                                 WHEN 'FG01' THEN 450000 
                                 WHEN 'FG02' THEN 450000 
								 WHEN 'FG03' THEN 300000 
								 WHEN 'FG04' THEN 300000 
								 WHEN 'FG05' THEN 300000 
							     WHEN 'FG06' THEN 450000  
								 WHEN 'FG07' THEN 450000  
								 WHEN 'FG08' THEN 400000 
							     WHEN 'FG09' THEN 300000
								 WHEN 'FG10' THEN 105000
                                 ELSE 400000             
                                 END AS 일일목표수량
                                ,ISNULL(WORK_QTY,0)         AS 금일생산실적
								,ISNULL(DAY_QTY,0)         AS 금일생산누계
                                ,ISNULL(C.M_QTY,0)          AS 월누계수량
                                ,B.WORK_END_TIME            AS 생산시간
                                from[dbo].[TB_EQUIP]
                                        A
                                left join
                                (
                                 select A.*
                                from VW_DAILY_WORK_LIST A
                                INNER JOIN
                                (
                                SELECT EQUIP_ID, MAX(WORK_END_TIME) AS WORK_END_TIME
                                FROM VW_DAILY_WORK_LIST
                                where WORK_DATE = FORMAT(GETDATE(), 'yyyy-MM-dd')
                                GROUP BY EQUIP_ID
                                )B ON A.EQUIP_ID = B.EQUIP_ID AND A.WORK_END_TIME =B.WORK_END_TIME
                                where WORK_DATE = FORMAT(GETDATE(), 'yyyy-MM-dd')
                                ) B ON A.EQUIP_ID  = B.EQUIP_ID
                                left join
                                (
                                select EQUIP_ID, SUM(WORK_QTY) AS M_QTY
                                from VW_DAILY_WORK_LIST
                                where WORK_DATE BETWEEN  FORMAT(GETDATE(), 'yyyy-MM') AND FORMAT(DATEADD(MONTH,1, GETDATE()), 'yyyy-MM')
                                GROUP BY EQUIP_ID
                                ) C ON B.EQUIP_ID  = C.EQUIP_ID 
								 left join
                                (
                                select EQUIP_ID, SUM(WORK_QTY) AS DAY_QTY
                                from VW_DAILY_WORK_LIST
                                where WORK_DATE = FORMAT(GETDATE(), 'yyyy-MM-dd')
                                GROUP BY EQUIP_ID
                                ) D ON B.EQUIP_ID  = D.EQUIP_ID 
                                WHERE 1=1
                              　AND A.PROC_ID IN(4)
                                AND A.EQUIP_SORT IS NOT NULL
                                AND A.IS_MAIN_EQUIP = 1
                                AND A.PROC_GROUP_1 IS NOT NULL
                                ORDER BY A.EQUIP_SORT					";
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
            catch (Exception ex)
            {

                return null;
            }
        }
        public virtual DataTable USP_GET_Damayopaeg3()
        {
            try
            {

                string strcon = "Server = 183.111.74.179,1433;Database = DAMAYO_MES;UID = Coever;PWD = dmy1234!@";
                SqlConnection con;
                con = new SqlConnection(strcon);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = @"select                              

           
								CASE  
                                 WHEN A.EQUIP_ID ='FG01' THEN '접착1' 
                                 WHEN A.EQUIP_ID ='FG02' THEN '접착2' 
								 WHEN A.EQUIP_ID ='FG03' THEN '접착3' 
								 WHEN A.EQUIP_ID ='FG04' THEN '접착4' 
								 WHEN A.EQUIP_ID ='FG05' THEN '접착6' 
							     WHEN A.EQUIP_ID ='FG06' THEN '8면기'  
								 WHEN A.EQUIP_ID ='FG07' THEN '3면기'
								 WHEN A.EQUIP_ID ='FG08' THEN '트레이' 
							     WHEN A.EQUIP_ID ='FG09' THEN '접착6'
								 WHEN A.EQUIP_ID ='FG10' THEN '접착7'
								 WHEN A.EQUIP_ID ='DC01' THEN '톱슨1'
								 WHEN A.EQUIP_ID ='DC02' THEN '톱슨2'
								 WHEN A.EQUIP_ID ='DC03' THEN '톱슨3'
								 WHEN A.EQUIP_ID ='DC04' THEN '톱슨4'     
								 WHEN A.EQUIP_ID ='DC05' THEN '톱슨5'      
                                 END AS 호기
                                ,B.PART_NAME                AS 제품명
                                ,ISNULL(B.WORK_ORDER_QTY,0) AS 계획수량
                                ,CASE A.EQUIP_ID  
                                 WHEN 'FG01' THEN 450000 
                                 WHEN 'FG02' THEN 450000 
								 WHEN 'FG03' THEN 300000 
								 WHEN 'FG04' THEN 300000 
								 WHEN 'FG05' THEN 300000 
							     WHEN 'FG06' THEN 450000  
								 WHEN 'FG07' THEN 450000  
								 WHEN 'FG08' THEN 400000 
							     WHEN 'FG09' THEN 300000
								 WHEN 'FG10' THEN 105000
                                 ELSE 400000             
                                 END AS 일일목표수량
                                ,ISNULL(WORK_QTY,0)         AS 금일생산실적
								,ISNULL(DAY_QTY,0)         AS 금일생산누계
                                ,ISNULL(C.M_QTY,0)          AS 월누계수량
                                ,B.WORK_END_TIME            AS 생산시간
                                from[dbo].[TB_EQUIP]
                                        A
                                left join
                                (
                                 select A.*
                                from VW_DAILY_WORK_LIST A
                                INNER JOIN
                                (
                                SELECT EQUIP_ID, MAX(WORK_END_TIME) AS WORK_END_TIME
                                FROM VW_DAILY_WORK_LIST
                                where WORK_DATE = FORMAT(GETDATE(), 'yyyy-MM-dd')
                                GROUP BY EQUIP_ID
                                )B ON A.EQUIP_ID = B.EQUIP_ID AND A.WORK_END_TIME =B.WORK_END_TIME
                                where WORK_DATE = FORMAT(GETDATE(), 'yyyy-MM-dd')
                                ) B ON A.EQUIP_ID  = B.EQUIP_ID
                                left join
                                (
                                select EQUIP_ID, SUM(WORK_QTY) AS M_QTY
                                from VW_DAILY_WORK_LIST
                                where WORK_DATE BETWEEN  FORMAT(GETDATE(), 'yyyy-MM') AND FORMAT(DATEADD(MONTH,1, GETDATE()), 'yyyy-MM')
                                GROUP BY EQUIP_ID
                                ) C ON B.EQUIP_ID  = C.EQUIP_ID 
								 left join
                                (
                                select EQUIP_ID, SUM(WORK_QTY) AS DAY_QTY
                                from VW_DAILY_WORK_LIST
                                where WORK_DATE = FORMAT(GETDATE(), 'yyyy-MM-dd')
                                GROUP BY EQUIP_ID
                                ) D ON B.EQUIP_ID  = D.EQUIP_ID 
                                WHERE 1=1
                              　AND A.PROC_ID IN(5)
                                AND A.EQUIP_SORT IS NOT NULL
                                AND A.IS_MAIN_EQUIP = 1
                                AND A.PROC_GROUP_1 IS NOT NULL
                                ORDER BY A.EQUIP_SORT					";
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
            catch (Exception ex)
            {

                return null;
            }
        }
        public virtual DataTable USP_GET_Damayopaeg4()
        {
            try
            {

                string strcon = "Server = 183.111.74.179,1433;Database = DAMAYO_MES;UID = Coever;PWD = dmy1234!@";
                SqlConnection con;
                con = new SqlConnection(strcon);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = @"select                              
								CASE  
                                 WHEN A.EQUIP_ID ='FG01' THEN '접착1' 
                                 WHEN A.EQUIP_ID ='FG02' THEN '접착2' 
								 WHEN A.EQUIP_ID ='FG03' THEN '접착3' 
								 WHEN A.EQUIP_ID ='FG04' THEN '접착4' 	
								 WHEN A.EQUIP_ID ='FG09' THEN '접착6'
								 WHEN A.EQUIP_ID ='FG10' THEN '접착7'
							     WHEN A.EQUIP_ID ='FG06' THEN '8면기'  
								 WHEN A.EQUIP_ID ='FG07' THEN '3면기'   
                                 END AS 호기
                                ,CASE A.EQUIP_ID  
                                 WHEN 'FG01' THEN 450000 
                                 WHEN 'FG02' THEN 450000 
								 WHEN 'FG03' THEN 400000 
								 WHEN 'FG04' THEN 400000 
	                             WHEN 'FG09' THEN 450000
								 WHEN 'FG10' THEN 450000
							     WHEN 'FG06' THEN 180000 							 
								 WHEN 'FG07' THEN 110000             
                                 END AS 일일목표수량
								,ISNULL(DAY_QTY,0)          AS 금일생산누계
                                ,ISNULL(C.M_QTY,0)          AS 월누계수량
                                ,B.WORK_END_TIME            AS 생산시간
                                from[dbo].[TB_EQUIP]
                                        A
                                left join
                                (
                                 select A.*
                                from VW_DAILY_WORK_LIST A
                                INNER JOIN
                                (
                                SELECT EQUIP_ID, MAX(WORK_END_TIME) AS WORK_END_TIME
                                FROM VW_DAILY_WORK_LIST
                                where WORK_DATE = FORMAT(GETDATE(), 'yyyy-MM-dd')
                                GROUP BY EQUIP_ID
                                )B ON A.EQUIP_ID = B.EQUIP_ID AND A.WORK_END_TIME =B.WORK_END_TIME
                                where WORK_DATE = FORMAT(GETDATE(), 'yyyy-MM-dd')
                                ) B ON A.EQUIP_ID  = B.EQUIP_ID
                                left join
                                (
                                select EQUIP_ID, SUM(WORK_QTY) AS M_QTY
                                from VW_DAILY_WORK_LIST
                                where WORK_DATE BETWEEN  FORMAT(GETDATE(), 'yyyy-MM') AND FORMAT(DATEADD(MONTH,1, GETDATE()), 'yyyy-MM')
                                GROUP BY EQUIP_ID
                                ) C ON B.EQUIP_ID  = C.EQUIP_ID 
								 left join
                                (
                                select EQUIP_ID, SUM(WORK_QTY) AS DAY_QTY
                                from VW_DAILY_WORK_LIST
                                where WORK_DATE = FORMAT(GETDATE(), 'yyyy-MM-dd')
                                GROUP BY EQUIP_ID
                                ) D ON B.EQUIP_ID  = D.EQUIP_ID 
                                WHERE 1=1
                              　AND A.PROC_ID IN(5)
                                AND A.EQUIP_SORT IS NOT NULL
                                AND A.IS_MAIN_EQUIP = 1
                                AND A.PROC_GROUP_1 IS NOT NULL
								AND A.EQUIP_ID IN(
							     'FG01'
								,'FG02'
								,'FG03'
								,'FG04'
								,'FG09'
								,'FG10'
								,'FG06'
								,'FG07'
								)
                                ORDER BY A.EQUIP_SORT";
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
            catch (Exception ex)
            {

                return null;
            }
        }
    }


}

