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
                querySql = $"select vid from TBUDT_ModeLayout where IBillID like '%{ibillid}%' and vDicId = 'hdata' group by vid ";
            }
            else
            {
                querySql = "select vid from TBUDT_ModeLayout where  vDicId = 'hdata' group by vid";
            }
            DBUtil dBUtil = new DBUtil();
            SqlConnection connection = dBUtil.GetConnection();
            DataSet dSet = dBUtil.Query(connection, querySql);
            dBUtil.close(connection);
            return dSet;
        }
        #endregion
        #region 查询从表
        public List<string> getChartLayoutList(string ibillid)
        {
            string querySql = "";
            if (ibillid != "")
            {

                querySql = $"select vpid from TBUDT_ChartLayout te left join TBUDT_ModeLayout tm on te.vPID = tm.vid  where tm.IBillID like '%{ibillid}%' GROUP BY VPID";
            }
            else
            {
                querySql = "select vpid from TBUDT_ChartLayout GROUP BY VPID";
            }
            DBUtil dBUtil = new DBUtil();
            SqlConnection connection = dBUtil.GetConnection();
            DataSet dSet = dBUtil.Query(connection, querySql);
            List<string> mainLayoutList = new List<string>();
            foreach (DataRow row in dSet.Tables[0].Rows)
            {
                foreach (DataColumn dataColumn in dSet.Tables[0].Columns)
                {
                    mainLayoutList.Add(row[dataColumn].ToString());
                }

            }

            return mainLayoutList;
        }
        #endregion

    }
}
