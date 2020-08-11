using PageRederTestConsole;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PageRederForRX.formSrc
{
    public partial class ExcelToProvinceExp : Form
    {
        string excelFile = "";
        List<string> sqlList = new List<string>();
        bool bt1 = false;
        bool bt2 = false;
        Form1 f1 = null;
        
        public ExcelToProvinceExp(Form1 ff)
        {
            InitializeComponent();
            f1 = ff;

        }

        private void selectFile_Click(object sender, EventArgs e)
        {
            //选择Excel文件
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "请选择客户提供的住宿标准信息";
            openFileDialog.Filter = "所有文件(*xls*)|*.xls*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                excelFile = openFileDialog.FileName;
                textBox1.Text = excelFile;
                loadDataForExcel();
            }
        }
        private void loadDataForExcel()
        {
            if (excelFile.Length < 1)
            {
                MessageBox.Show("无效的文件路径！", "错误");
                return;
            }
            //读取Excel
            OleDbConnection oleDb = null;
            string sConnstring = $"provider=Microsoft.Jet.OLEDB.4.0;data source={excelFile};Extended Properties=Excel 8.0;Persist Security Info=False";
            oleDb = new OleDbConnection(sConnstring);
            string strSql = "select * from [Sheet1$]";
            OleDbCommand cmd = new OleDbCommand(strSql, oleDb);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "zhusu");
            dataGridView1.DataSource = ds.Tables[0];

            //添加一列住宿级别 用作数据检查
            /*            DataGridTextBoxColumn dgvc = new DataGridTextBoxColumn();
                        dgvc.HeaderText = "住宿级别";
                        dgvc.MappingName = "zsjb";*/

            dataGridView1.Columns.Add("zsjb", "住宿级别");
            //dataGridView1.Columns.Add("sjkfy", "级别费用");

        }

        private void checkdata_Click(object sender, EventArgs e)
        {
            //检查数据
            //开始便利
            int rowindex = 0;
            string ErrorMsg = "";
            foreach (DataGridViewRow dgr in dataGridView1.Rows)
            {
                /*Console.WriteLine($"一级目的地:{dgr.Cells[0].Value},二级目的地{dgr.Cells[1].Value},三级目的地{dgr.Cells[2].Value},正高住宿");*/
                //第8个为住宿标准
                string querSql = $"select vPlaceType from TBUDT_ProvinceCLF where vPlaceName = '{dgr.Cells[1].Value.ToString()}' and vpid = (select vid from tbudt_provinceCLF where vplaceName = '{dgr.Cells[0].Value.ToString()}' and ilevel = 0)";
                DBUtil db = new DBUtil();
                DataSet ds = db.Query(db.GetConnection(), querSql);
                //Console.WriteLine(ds.Tables[0].Rows[0][0]);
                try
                {
                    dataGridView1.Rows[rowindex].Cells[8].Value = ds.Tables[0].Rows[0][0];
                    // string qs = $"";
                    rowindex += 1;
                }
                catch (IndexOutOfRangeException)
                {
                    //层级没查询到
                    ErrorMsg += $"第{rowindex + 1} 行数据，未能匹配到正觉的数据层级，请检查源文件后重新加载！\n";
                    ErrorMsg += $"一级目的地:{dgr.Cells[0].Value},二级目的地{dgr.Cells[1].Value}\n";
                    rowindex += 1;

                }

            }
            if (ErrorMsg != "")
            {
                MessageBox.Show(ErrorMsg, "你有错误哦~");
            }
            else
            {
                MessageBox.Show("全部匹配上了哦，不过你要检查一下数据是否正确", "其实对不上我也没办法");
                bt1 = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //生成数据库语句
            //dataGridView1
            if (!bt1) {
                MessageBox.Show("请先检查数据!");
                return;
            }
            sqlList.Clear();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //遍历每一行
                //1.取得开始月份和结束月份
                int s_month = int.Parse(row.Cells[6].Value.ToString());
                int e_month = int.Parse(row.Cells[7].Value.ToString());
                if (s_month == 1 && e_month == 12)
                {
                    //开始拼接第一句sql; 开始时间是一月 结束时间是2月
                    string sql = $"update TBUDT_ProvinceExp set vExPValue='{row.Cells[2].Value.ToString()}',DAMTN = '{row.Cells[3].Value.ToString()}.00000000' where vProDate >='2020-01-01' and vProDate <='2020-12-31' and vPlaceType = {row.Cells[8].Value}";
                    sqlList.Add(sql);
                    string sql_2 = $"update TBUDT_ProvinceExp set vExPValue='{row.Cells[4].Value.ToString()}',DAMTN = '{row.Cells[5].Value.ToString()}.00000000' where vProDate >='2020-01-01' and vProDate <='2020-12-31' and vPlaceType = {row.Cells[8].Value}";
                    sqlList.Add(sql_2);
                }
                //开始折腾
                if (s_month != 1 || e_month != 12)
                {
                    //非全年数据判断
                    if (s_month < e_month)
                    {
                        //正常一年内的数据 例如 7-9月
                        string s_date = "";
                        //开始月份小于10 需要前面拼接个0
                        if (s_month < 10) { s_date = "2020-0" + s_month + "-01"; } else { s_date = "2020-" + s_month + "-01"; }
                        string e_date = "";
                        if (e_month + 1 < 10 && e_month < 12)
                        {
                            e_date = "2020-0" + (s_month + 1) + "-01";
                        }
                        else if (e_month + 1 >= 10 && e_month < 12)
                        {
                            e_date = "2020-" + (s_month + 1) + "-01";
                        }
                        else if (e_month == 12)
                        {
                            e_date = "2020-" + (s_month) + "-01";
                        }
                        string sql = $"update TBUDT_ProvinceExp set vExPValue='{row.Cells[2].Value.ToString()}',DAMTN = '{row.Cells[3].Value.ToString()}.00000000' where vProDate >='{s_date}' and vProDate <'{e_date}' and vPlaceType = {row.Cells[8].Value}";
                        sqlList.Add(sql);
                        string sql_2 = $"update TBUDT_ProvinceExp set vExPValue='{row.Cells[4].Value.ToString()}',DAMTN = '{row.Cells[5].Value.ToString()}.00000000' where vProDate >='{s_date}' and vProDate <'{e_date}' and vPlaceType = {row.Cells[8].Value}";
                        sqlList.Add(sql_2);

                    }
                    else {
                        //非正常的数据 即 12 - 1 7-5之类的 不过 7-5这种 不太会出现 保险起见还是加上
                        string s_date = "";
                        if (s_month < 10) { s_date = "2020-0" + s_month + "-01"; } else { s_date = "2020-" + s_month + "-01"; }
                        //然后 开始执行 end_time = 2020-12-31 
                        string sql = $"update TBUDT_ProvinceExp set vExPValue='{row.Cells[2].Value.ToString()}',DAMTN = '{row.Cells[3].Value.ToString()}.00000000' where vProDate >='{s_date}' and vProDate <='2020-12-31' and vPlaceType = {row.Cells[8].Value}";
                        sqlList.Add(sql);
                        string sql_2 = $"update TBUDT_ProvinceExp set vExPValue='{row.Cells[4].Value.ToString()}',DAMTN = '{row.Cells[5].Value.ToString()}.00000000' where vProDate >='{s_date}' and vProDate <='2020-12-31' and vPlaceType = {row.Cells[8].Value}";
                        sqlList.Add(sql_2);
                        string e_date = "";
                        // 执行 2020-01-01 到 2020-x+1-01
                        if (e_month + 1 < 10) { e_date = "2020-0" + (e_month + 1) + "-01"; } else { e_date = "2020-" + (e_month + 1) + "-01"; }
                        string sql_3 = $"update TBUDT_ProvinceExp set vExPValue='{row.Cells[2].Value.ToString()}',DAMTN = '{row.Cells[3].Value.ToString()}.00000000' where vProDate >='2020-01-01' and vProDate <'{e_date}' and vPlaceType = {row.Cells[8].Value}";
                        sqlList.Add(sql_3);
                        string sql_4 = $"update TBUDT_ProvinceExp set vExPValue='{row.Cells[4].Value.ToString()}',DAMTN = '{row.Cells[5].Value.ToString()}.00000000' where vProDate >='2020-01-01' and vProDate <'{e_date}' and vPlaceType = {row.Cells[8].Value}";
                        sqlList.Add(sql_4);
                    }

                }

            }
            //拼接完成~
            MessageBox.Show("拼接完成~");
            bt2 = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!bt2) {
                MessageBox.Show("请先生成语句！");
                return;
            }
            //输出到txt
            string sql_date = $".\\Update_TBUDT_ProvinceExp_{DateTime.Now.ToString("yyyy_MM_dd")}.sql";
            if (File.Exists(sql_date)) { File.Delete(sql_date); }
            StreamWriter sw = new StreamWriter(sql_date,true);
            foreach (string str in sqlList) {
                //循环输出
                sw.WriteLine(str+";");
            }
            sw.Close();
            MessageBox.Show("SQL语句已输出到程序根目录，请自行核对~");
        }

        private void closeing(object sender, FormClosingEventArgs e)
        {
            f1.etpe = null;
        }
    }
}
