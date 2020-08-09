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
        #region 查询 ModelLayout 表 这个是查询所有 暂时没有使用
        public DataSet getModelLayout()
        {
            string querySql = "select ibillid,vlayouttype,vdicid,vtop,vwidth,vheight,vid,vremarks from TBUDT_ModeLayout";
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
        #endregion
        #region 查询 ModeLoayout 即 主表信息
        public DataSet getModeLoayoutList(string ibillid)
        {
            string querySql = "";
            if (ibillid != "")
            {
                querySql = $"select vpid from TBUDT_EditLayout te left join TBUDT_ModeLayout tm on te.vPID = tm.vid  where tm.IBillID like '%{ibillid}%' GROUP BY VPID";
            }
            else
            {
                querySql = "select vpid from TBUDT_EditLayout GROUP BY VPID";
            }
            DBUtil dBUtil = new DBUtil();
            SqlConnection connection = dBUtil.GetConnection();
            DataSet dSet = dBUtil.Query(connection, querySql);
            dBUtil.close(connection);
            return dSet;
        }
        #endregion
        #region 查询从表 未完成功能
        public DataSet getChartLayoutList(string ibillid)
        {
            string querySql = "";
            if (ibillid != "")
            {

                querySql = $"select vpid from TBUDT_ChartLayout te left join TBUDT_ModeLayout tm on te.vPID = tm.vid  where tm.IBillID like '%{ibillid}%' GROUP BY VPID";
            }
            else
            {
                querySql = "select vpid from TBUDT_ModeLayout GROUP BY VPID";
            }
            DBUtil dBUtil = new DBUtil();
            SqlConnection connection = dBUtil.GetConnection();
            DataSet dSet = dBUtil.Query(connection, querySql);
            dBUtil.close(connection);
            return dSet;
        }
        #endregion

    }
}
