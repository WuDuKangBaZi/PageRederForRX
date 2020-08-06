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
        #region
        public List<string> loadMainLayout() {
            DBUtil dbUtil = new DBUtil();
            GetLayoutConfig getLayoutConfig = new GetLayoutConfig();
            DataSet dataSet = getLayoutConfig.getModeLoayoutList();
            List<string> mainLayoutList = new List<string>();
            foreach (DataRow row in dataSet.Tables[0].Rows) {
                foreach (DataColumn dataColumn in dataSet.Tables[0].Columns) {
                    mainLayoutList.Add(row[dataColumn].ToString());                
                }
            
            }


            return mainLayoutList;
        
        }
        #endregion
        public Dictionary<int, string> getDictonaryByvfrmtype() {
            Dictionary<int, string> defaulat = new Dictionary<int, string>();
            defaulat.Add(0, "输入框");
            defaulat.Add(1, "表格");
            defaulat.Add(2, "树");
            defaulat.Add(3, "时间");
            defaulat.Add(10, "跳转页面 多选");
            defaulat.Add(11, "跳转页面 单选");
            defaulat.Add(12, "右边显示是否");
            return defaulat;
        
        }
        public Dictionary<int, string> getDictonaryByTF() {
            Dictionary<int, string> defaulat = new Dictionary<int, string>();
            defaulat.Add(0, "输入框");
            defaulat.Add(1, "表格");
            defaulat.Add(2, "树");
            defaulat.Add(3, "时间");
            defaulat.Add(10, "跳转页面 多选");
            defaulat.Add(11, "跳转页面 单选");
            defaulat.Add(12, "右边显示是否");
            return defaulat;
        }
        public BindingSource getparamcode(string vtypeid) {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            string querySql = $"select code,name from tpzjk_getparamcode where typeid = '{vtypeid}'";
            Console.WriteLine(querySql);
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

    }
}
