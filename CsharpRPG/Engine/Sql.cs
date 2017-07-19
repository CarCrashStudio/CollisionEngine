using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CsharpRPG.Engine
{
    public class Sql
    {
        string SELECTstring = "SELECT {0} FROM {1};";
        string SELECTWHEREstring = "SELECT {0} FROM {1} WHERE {2};";
        string SELECTWHEREANDstring = "SELECT {0} FROM {1} WHERE {2} AND {3};";
        string UPDATEstring = "UPDATE {0} SET {1} WHERE {2};";
        string UPDATEANDstring = "UPDATE {0} SET {1} WHERE {2} AND {3};";
        string INSERT10string = "INSERT INTO {0} VALUES ('{1}', '{2}', '{3}', '{4}','{5}','{6}','{7}','{8}','{9}','{10}');";
        string INSERT5string = "INSERT INTO {0} VALUES ('{1}', '{2}', '{3}', '{4}','{5}');";
        string INSERT4string = "INSERT INTO {0} VALUES ('{1}', '{2}', '{3}', '{4}');";
        string INSERT3string = "INSERT INTO {0} VALUES ('{1}', '{2}', '{3}');";

        SqlConnection Connection;
        SqlCommand Command;
        SqlDataReader Reader;
        object[,] Results;
        public Sql(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            //TestSQL();
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

        object[,] ExecuteReader(string query)
        {
            Results = new object[1, 1];
            try
            {
                int i = 0;
                Command = new SqlCommand(query, Connection);
                Reader = Command.ExecuteReader();
                Results = new object[10, Reader.FieldCount];
                while (Reader.Read())
                {                    
                    for (int j = 0; j < Reader.FieldCount; j++)
                    {
                        Results[i, j] = Reader.GetValue(j);
                    }
                    i++;
                }
                Reader.Close();
                return Results;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
        }

        public object[,] ExecuteSELECT(string arg, string table)
        {
            string query = String.Format(SELECTstring, arg, table);
            return ExecuteReader(query);
 
        }
        public object[,] ExecuteSELECTWHERE(string arg1, string arg2, string table)
        {
            string query = String.Format(SELECTWHEREstring, arg1, table, arg2);
            return ExecuteReader(query);
        }
        public object[,] ExecuteSELECTWHEREAND(string arg1, string arg2, string arg3, string table)
        {
            string query = String.Format(SELECTWHEREANDstring, arg1, table, arg2, arg3);
            return ExecuteReader(query);
        }
        public void ExecuteUPDATEAND(string table, string condition, string condition2, string arg1)
        {
            string query = String.Format(UPDATEANDstring, table, arg1, condition, condition2);
            Command = new SqlCommand(query, Connection);
            Command.ExecuteNonQuery();
        }
        public void ExecuteUPDATE(string table, string condition, string arg1)
        {
            string query = String.Format(UPDATEstring, table, arg1, condition);
            Command = new SqlCommand(query, Connection);
            Command.ExecuteNonQuery();
        }
        public void ExecuteINSERT10(string table, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10)
        {
            string query = String.Format(INSERT10string, table, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            Command = new SqlCommand(query, Connection);
            Command.ExecuteNonQuery();
        }
        public void ExecuteINSERT5(string table, object arg1, object arg2, object arg3, object arg4, object arg5)
        {
            string query = String.Format(INSERT5string, table, arg1, arg2, arg3, arg4, arg5);
            Command = new SqlCommand(query, Connection);
            Command.ExecuteNonQuery();
        }
        public void ExecuteINSERT4(string table, object arg1, object arg2, object arg3, object arg4)
        {
            string query = String.Format(INSERT4string, table, arg1, arg2, arg3, arg4);
            Command = new SqlCommand(query, Connection);
            Command.ExecuteNonQuery();
        }
        public void ExecuteINSERT3(string table, object arg1, object arg2, object arg3)
        {
            string query = String.Format(INSERT3string, table, arg1, arg2, arg3);
            Command = new SqlCommand(query, Connection);
            Command.ExecuteNonQuery();
        }
    }
}
