using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;

namespace NETCoreAPIConectaBarrio.Helpers
{
    public class SQLConnectionHelper
    {
        public const string EQUAL = "=";

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
            MySqlConnection? conn = ConnectBBDD();
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

        public static DataRow? GetResult(string table, string[]? fields = null, object[]? values = null, string[]? relations = null)
        {
            MySqlConnection? conn = ConnectBBDD();
            DataTable dt = new DataTable();
            DataRow res = null;

            if (conn != null)
            {
                conn.Open();
                string sql = "SELECT * FROM `" + table + "`";

                if (fields?.Length == values?.Length && fields?.Length == relations?.Length && fields?.Length > 0)
                {
                    sql += " WHERE ";
                    for (int i = 0; i < fields.Length; i++)
                    {
                        sql += "`" + fields?[i] + "`" + relations?[i] + "'" + values?[i] + "'";
                        if (i < fields?.Length - 1)
                            sql += " AND ";
                    }
                }

                sql += " LIMIT 1";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }

                if(dt.Rows.Count > 0) { res = dt.Rows[0]; }
                
                conn.Close();
            }

            return res;
        }


        public static DataTable GetResultTable(string table, string[]? fields = null, object[]? values = null, string[]? relations = null)
        {
            MySqlConnection? conn = ConnectBBDD();
            DataTable dt = new DataTable();

            if (conn != null)
            {
                conn.Open();
                string sql = "SELECT * FROM `" + table + "`";

                if (fields?.Length == values?.Length && fields?.Length == relations?.Length && fields?.Length > 0)
                {
                    sql += " WHERE ";
                    for (int i = 0; i < fields?.Length; i++)
                    {
                        sql += "`" + fields?[i] + "`" + relations?[i] + "'" + values?[i] + "'";
                        if (i < fields?.Length - 1)
                            sql += " AND ";
                    }
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }

                conn.Close();
            }

            return dt;
        }

        public static bool InsertBBDD(string table, string[] fields, object[] values)
        {
            MySqlConnection? conn = ConnectBBDD();
            bool res = false;

            conn?.Open();

            if (conn?.State != ConnectionState.Open)
            {
                string sql = "INSERT INTO `" + table + "` (";
                for (int i = 0; i < fields.Length; i++)
                {
                    sql += "`" + fields[i] + "`";
                    if (i < fields.Length - 1) sql += ", ";
                    else sql += ") ";
                }

                sql += "VALUES (";
                for (int i = 0; i < values.Length; i++)
                {
                    sql += SQLTypeConverter.ParseTypeToString(values[i].GetType(), values[i]);

                    if (i < values.Length - 1) sql += ", ";
                    else sql += "); ";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                int insert = cmd.ExecuteNonQuery();
                if (insert > 0) { res = true; }

                conn?.Close();
            }
            return res;
        }

        public static bool DeleteBBDD(string table, string[] fields, object[] values, string[] relations)
        {

            MySqlConnection? conn = ConnectBBDD();
            bool res = false;

            conn?.Open();

            if (conn?.State == ConnectionState.Open)
            {
                if (values.Length == fields.Length == relations.Length > 0)
                {
                    string sql = "DELETE FROM `" + table + "` WHERE ";
                    for (int i = 0; i < fields.Length; i++)
                    {
                        sql += "`" + fields[i] + "` " + relations[i] + " " + SQLTypeConverter.ParseTypeToString(values[i].GetType(), values[i]);
                        if (i < fields.Length - 1)
                            sql += " AND ";
                    }

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    int insert = cmd.ExecuteNonQuery();
                    if (insert > 0) { res = true; }
                }

                conn?.Close();
            }

            return res;
        }

        public static bool UpdateBBDD(string table, string[] fields, object[] values, string[] fieldsFilter, object[] valuesFilter)
        {

            MySqlConnection? conn = ConnectBBDD();
            bool res = false;
            conn?.Open();

            if (conn?.State == ConnectionState.Open)
            {
                string sql = "UPDATE `" + table + "` SET ";
                if (values.Length > 0 && fields.Length > 0 && values.Length == fields.Length)
                {
                    for (int i = 0; i < fields.Length; i++)
                    {
                        sql += "`" + fields[i] + "` = " + SQLTypeConverter.ParseTypeToString(values[i].GetType(), values[i]);
                        if (i < fields.Length - 1)
                            sql += ", ";
                    }
                }
                sql += " WHERE ";
                if (valuesFilter.Length > 0 && fieldsFilter.Length > 0 && valuesFilter.Length == fieldsFilter.Length)
                {
                    for (int i = 0; i < fieldsFilter.Length; i++)
                    {
                        sql += "`" + fieldsFilter[i] + "` = " + SQLTypeConverter.ParseTypeToString(valuesFilter[i].GetType(), valuesFilter[i]);
                        if (i < fieldsFilter.Length - 1)
                            sql += ", ";
                    }
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                int update = cmd.ExecuteNonQuery();

                if (update > 0) { res = true; }

                conn?.Close();
            }

            return res;
        }
}
}
