using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CsharpRPG.Engine
{
    public class Sql
    {
        string SELECTstring = "SELECT {0} FROM {1};";
        string SELECTWHEREstring = "SELECT {0} FROM {1} WHERE {2};";
        string UPDATEstring = "UPDATE {0} SET {1} WHERE {2};";
        string INSERTstring = "INSERT INTO {0} VALUES ('{1}', '{2}', '{3}', '{4}','{5}','{6}','{7}','{8}','{9}','{10}');";

        SqlConnection Connection;
        SqlCommand Command;
        SqlDataReader Reader;
        List<object> Results;

        public Sql(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            TestSQL();
        }

        void TestSQL()
        {
            try
            {
                Open();
                MessageBox.Show("Connected");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Open()
        {
            try
            {
                Connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Close()
        {
            try
            {
               Connection.Close();
            }
            catch { }
        }

        object[] ExecuteReader(string query)
        {
            try
            {
                Command = new SqlCommand(query, Connection);
                Reader = Command.ExecuteReader();
                Results = new List<object>();
                while (Reader.Read())
                {
                    Results.Add(Reader.GetValue(0));
                }
                Reader.Close();
                return Results.ToArray();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
        }

        public object[] ExecuteSELECT(string arg, string table)
        {
            string query = String.Format(SELECTstring, arg, table);
            return ExecuteReader(query);
 
        }
        public object[] ExecuteSELECTWHERE(string arg1, string arg2, string table)
        {
            string query = String.Format(SELECTWHEREstring, arg1, table, arg2);
            return ExecuteReader(query);
        }
        public void ExecuteUPDATE(string table, string condition, string arg1)
        {
            string query = String.Format(UPDATEstring, table, arg1, condition);
            Command = new SqlCommand(query, Connection);
            Command.ExecuteNonQuery();
        }
        public void ExecuteINSERT(string table, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10)
        {
            string query = String.Format(INSERTstring, table, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            Command = new SqlCommand(query, Connection);
            Command.ExecuteNonQuery();
        }
    }
}
