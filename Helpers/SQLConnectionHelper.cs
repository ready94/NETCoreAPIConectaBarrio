using MySql.Data.MySqlClient;
using System.Data;

namespace NETCoreAPIConectaBarrio.Helpers
{
    public class SQLConnectionHelper
    {

        public static MySqlConnection? ConnectBBDD()
        {
            try
            {
                MySqlConnection mysql = new("datasource=127.0.0.1;port=3306;username=sysadmin;password=D_PHSknw5Q_p*DdE;database=conectabarrio;");
                return mysql;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static string[] TEST()
        {
            MySqlConnection conn = ConnectBBDD();
            List<string> res = [];

            if (conn != null)
            {
                conn.Open();
                string sql = "SELECT * FROM sys_t_users";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                        res.Add(dr.GetName(i) + ": " + dr.GetValue(i));
                    
                    res.Add("----------------------");
                }
                dr.Close();

                sql = "INSERT INTO `test` (`IDTEST`, `NAME`, `CREATIONDATE`) VALUES (1, 'BRUNO', '2024-03-23');";
                cmd = new MySqlCommand(sql, conn);
                int insert = cmd.ExecuteNonQuery();

                if (insert > 0)
                {
                    sql = "SELECT * FROM test";
                    cmd = new MySqlCommand(sql, conn);
                    dr = cmd.ExecuteReader();
                  
                    while (dr.Read())
                    {
                        for (int i = 0; i < dr.FieldCount; i++)                        
                            res.Add(dr.GetName(i) + ": " + dr.GetValue(i));
                        
                        res.Add("----------------------");
                    }
                }

                conn.Close();
            }

            return res.ToArray();
        }

        //public static object GetSingleRow(string table, string[] fields, object[] values, string[] relations)
        //{
        //    if (ConnectBBDD())
        //    {
        //        string sql = "SELECT * FROM " + table;
        //        if (values.Length == fields.Length == relations.Length > 0)
        //        {
        //            sql += (string)Convert
        //        }

        //        MySqlCommand cmd = new MySqlCommand("SELECT * FROM " + table);
        //    }
        //}

        //public static List<object> GetListRow(string table, string[] fields, object[] values, string[] relations)
        //{

        //}

        //public static bool InsertBBDD(string table, string[] fields, object[] values, string[] relations)
        //{

        //}

        //public static bool DeleteBBDD(string table, string[] fields, object[] values, string[] relations)
        //{

        //}

        //public static bool UpdateBBDD(string table, string[] fields, object[] values, string[] relations)
        //{

        //}
    }
}
