﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace PageRederTestConsole
{
    class DBUtil
    {
        public SqlConnection GetConnection()
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection("Server=192.168.0.201;Database=fundsMange_S2;uid=sa;pwd=Hzrx2019");
                sqlConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
                return null;
            }
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Table");
            return dataSet; 
        }
    }
}
