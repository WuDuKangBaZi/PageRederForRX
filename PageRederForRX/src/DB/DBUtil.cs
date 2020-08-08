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
        #region 获取数据库连接 后续优化做连接池
        public SqlConnection GetConnection()
        {
            SqlConnection sqlConnection = null;
            try
            {

                string str = PageRederForRX.Properties.Settings.Default.sqlstr;
                sqlConnection = new SqlConnection(str);
                sqlConnection.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("数据库读取错误！");
                return null;
            }
            return sqlConnection;
        }
        #endregion
        #region 数据库连接关闭方法 很少用 因为不一定用得到 关闭软件后连接就断了
        public void close(SqlConnection sqlConnection)
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        #endregion
        // Query
        #region 查询 传入参数为 数据库连接  查询数据语句
        public DataSet Query(SqlConnection connection, string sSql)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;
            sqlCommand.CommandText = sSql;
            var re = sqlCommand.ExecuteNonQuery();
            if (re != -1)
            {
                connection.Close();
                return null;
            }
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Table");
            connection.Close();
            return dataSet;
        }
        #endregion
        #region 标准的sql insert/update执行方法 废弃 全体走事务 方法仅供参考
        public int doExecute(string sSQL)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection();
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sSQL;
                sqlCommand.Connection = sqlConnection;
                return sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return 0;
            }


        }
        #endregion
    }
}
