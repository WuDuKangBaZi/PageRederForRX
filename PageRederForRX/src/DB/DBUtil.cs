using PageRederForRX;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PageRederTestConsole
{
    class DBUtil
    {
        public SqlConnection GetConnection()
        {
            SqlConnection sqlConnection = null;
            try
            {

                string str = PageRederForRX.Properties.Settings.Default.sqlstr;
                Console.WriteLine(str);
                sqlConnection = new SqlConnection(str);
                sqlConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("数据库读取错误！");
                return null;
            }
            return sqlConnection;
        }

        public void close(SqlConnection sqlConnection)
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        // Query
        public DataSet Query(SqlConnection connection,string sSql) {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;
            sqlCommand.CommandText = sSql;
            var re = sqlCommand.ExecuteNonQuery();
            if (re != -1) {
                connection.Close();
                return null;
            }
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Table");
            connection.Close();
            return dataSet; 
        }
        public int doExecute(string sSQL) {
            try
            {
                SqlConnection sqlConnection = new SqlConnection();
                if (sqlConnection.State == ConnectionState.Closed) {
                    sqlConnection.Open();
                }
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sSQL;
                sqlCommand.Connection = sqlConnection;
                return sqlCommand.ExecuteNonQuery();
            }catch(Exception e)
            {
                return 0;
            }


        }
    }
}
