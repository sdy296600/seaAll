using MySql.Data.MySqlClient;
using System.Data;

namespace CoFAS.NEW.MES.POP.Function
{
    class MY_DBClass
    {
        public string _ConnectionString = string.Empty;
        public MY_DBClass()
        {
            _ConnectionString = "Server=10.10.10.216;Database=hansoldms;UID=coever;PWD=coever119!;";
        }

        public DataTable SELECT_DataTable(string sql)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_ConnectionString))
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

        public DataSet SELECT_DataSet(string sql)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_ConnectionString))
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
                    DataSet dt = new DataSet();

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
    }
}
