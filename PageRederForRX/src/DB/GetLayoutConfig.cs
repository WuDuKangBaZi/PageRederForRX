﻿using System;
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
        public DataSet getModeLoayoutList() {
            string querySql = "select vpid from TBUDT_EditLayout GROUP BY VPID";
            DBUtil dBUtil = new DBUtil();
            SqlConnection connection = dBUtil.GetConnection();
            DataSet dSet = dBUtil.Query(connection, querySql);
            dBUtil.close(connection);
            return dSet;
        }
        public DataSet getEditLoayoutList(string vpid) {
            string querySql = "";

            return null;
        }
    }
}