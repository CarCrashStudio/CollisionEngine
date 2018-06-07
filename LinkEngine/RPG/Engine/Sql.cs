using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RPG
{
    public class Sql
    {
        string SELECTstring = "SELECT {0} FROM {1};";
        string SELECTWHEREstring = "SELECT {0} FROM {1} WHERE {2};";
        string SELECTWHEREANDstring = "SELECT {0} FROM {1} WHERE {2} AND {3};";
        string UPDATEstring = "UPDATE {0} SET {1} WHERE {2};";
        string UPDATEANDstring = "UPDATE {0} SET {1} WHERE {2} AND {3};";
        string INSERTstring = "INSERT INTO {0} VALUES ({1});";
        string DELETEWHEREstring = "DELETE FROM {0} WHERE {1};";

        SqlConnection Connection;
        SqlCommand Command;
        SqlDataReader Reader;
        object[,] Results;
        List<object[,]> temp;
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
            temp = new List<object[,]>();   
            try
            {
                int i = 0;
                int count = 0;
                Command = new SqlCommand(query, Connection);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    count++;
                }
                if (count == 0)
                {
                    Results = new object[count + 1, 1];
                }
                else
                {
                    Results = new object[count, Reader.FieldCount];
                }
                Reader.Close();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        for (int j = 0; j < Reader.FieldCount; j++)
                        {
                            Results[i, j] = Reader.GetValue(j);
                        }
                        i++;
                    }
                }
                else
                {
                    for(int k = 0; k < 1; k++)
                        for (int l = 0; l < 1; l++)
                            Results[k, l] = 0;
                }

                Reader.Close();
                return Results;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); Reader.Close();  return null; }
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
        public void ExecuteINSERT(string table, object arg1)
        {
            string query = String.Format(INSERTstring, table, arg1);
            Command = new SqlCommand(query, Connection);
            Command.ExecuteNonQuery();
        }
        public void ExecuteDELETEWHERE(string table, string condition)
        {
            string query = String.Format(DELETEWHEREstring, table, condition);
            Command = new SqlCommand(query, Connection);
            Command.ExecuteNonQuery();
        }
    }
}
