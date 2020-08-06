using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PageRederTestConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // 测试模块，各种测试 
            // 1. 登录数据库，读取布局数据，然后拼接到一起
            DBUtil util = new DBUtil();
           SqlConnection sqlConnection =  util.GetConnection();
            if (sqlConnection.State != ConnectionState.Open) {
                sqlConnection.Open();
            }
            GetLayoutConfig getLayoutConfig = new GetLayoutConfig();
            getLayoutConfig.getModelLayout();

            Console.ReadLine();
        }
       




    }
}
