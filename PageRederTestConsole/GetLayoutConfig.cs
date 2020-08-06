using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PageRederTestConsole
{
    class GetLayoutConfig
    {
        //TBUDT_ModeLayout
        public DataSet getModelLayout() {
            string querySql = "select concat(tm.IBillID,'号单据-',tm.vID) from TBUDT_ModeLayout tm where tm.vid in (select DISTINCT vpid from TBUDT_EditLayout)";
            DBUtil dBUtil = new DBUtil();
            SqlConnection connection = dBUtil.GetConnection();
            DataSet dSet = dBUtil.Query(connection, querySql);
            //测试遍历 打印数据
            /*foreach(DataRow dataRow in dSet.Tables[0].Rows)
            {
                foreach (DataColumn column in dSet.Tables[0].Columns)
                {
                    Console.WriteLine(dataRow[column].ToString());
                }

            }*/
            return dSet;
        

        }
    }
}
