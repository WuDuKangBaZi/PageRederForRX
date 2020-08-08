using PageRederTestConsole;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PageRederForRX.src.Function
{
    class DataLoad
    {
        #region 主要布局信息加载 实际上查询的是 TBUDT_EditLayout
        public List<string> loadMainLayout(string ibillid) {
            DBUtil dbUtil = new DBUtil();
            GetLayoutConfig getLayoutConfig = new GetLayoutConfig();
            DataSet dataSet = getLayoutConfig.getModeLoayoutList(ibillid);
            List<string> mainLayoutList = new List<string>();
            foreach (DataRow row in dataSet.Tables[0].Rows) {
                foreach (DataColumn dataColumn in dataSet.Tables[0].Columns) {
                    mainLayoutList.Add(row[dataColumn].ToString());                
                }
            
            }


            return mainLayoutList;
        
        }
        #endregion


        #region 查询系统字典表 下拉框的参数使用
        public BindingSource getparamcode(string vtypeid) {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            string querySql = $"select code,name from tpzjk_getparamcode where typeid = '{vtypeid}'";
            DBUtil db = new DBUtil();
            SqlConnection sqlConnection = db.GetConnection();
            DataSet ds = db.Query(sqlConnection,querySql);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                //预加载返回字典吧还是
               
                keyValuePairs.Add(row[0].ToString(), row[1].ToString());

            }
            BindingSource bs = new BindingSource();
            bs.DataSource = keyValuePairs;
            return bs;
        }
        #endregion

    }
}
