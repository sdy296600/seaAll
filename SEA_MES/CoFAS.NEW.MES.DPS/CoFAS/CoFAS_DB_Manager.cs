using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace CoFAS.NEW.MES.DPS
{
    class CoFAS_DB_Manager
    {

        public void Set_Andon(string station_no,string button_no)
        {

            try
            {
                string constr = "Server = 127.0.0.1;Database = coplatform; UID = root; PWD = developPassw@rd";

                using (MySqlConnection conn = new MySqlConnection(constr))
                {
                    MySqlDataAdapter DBAdapter = new MySqlDataAdapter();

                    MySqlCommand cmd = new MySqlCommand();

                    DBAdapter.SelectCommand = cmd;

                    cmd.Connection = conn;

                    cmd.CommandText = $@"INSERT INTO andon_mest 
                                       (
                                       station_no
                                       , button_no
                                       , reg_date
                                       )
                                       VALUES
                                       (
                                        '{station_no}'
                                       ,'{button_no}'
                                       , now()
                                       );";

                    cmd.CommandType = CommandType.Text;

   
                    // DB처리
                    DataTable dt = new DataTable();

                    conn.Open();

                    DBAdapter.Fill(dt);

                    conn.Close();

             
                }

            }
            catch (Exception err)
            {

            }
        }

        public void reset_Andon(string station_no)
        {

            try
            {
                string constr = "Server = 127.0.0.1;Database = coplatform; UID = root; PWD = developPassw@rd";

                using (MySqlConnection conn = new MySqlConnection(constr))
                {
                    MySqlDataAdapter DBAdapter = new MySqlDataAdapter();

                    MySqlCommand cmd = new MySqlCommand();

                    DBAdapter.SelectCommand = cmd;

                    cmd.Connection = conn;

                    cmd.CommandText = $@"UPDATE andon_mest
                                            SET use_yn = 'Y'
                                            WHERE station_no = '{station_no}'";

                    cmd.CommandType = CommandType.Text;
                   

                    // DB처리
                    DataTable dt = new DataTable();

                    conn.Open();

                    DBAdapter.Fill(dt);

                    conn.Close();


                }

            }
            catch (Exception err)
            {

            }
        }
    }
}
