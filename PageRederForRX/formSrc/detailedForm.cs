using PageRederTestConsole;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace PageRederForRX
{
    public partial class detailedForm : Form
    {
        int addBtn = 0;
        int fieldType = 0;
        public detailedForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //查询
            queryComInterface();
        }
        public void setParam(string ibillid, string vFieldCode, int fieldtype)
        {

            this.ibillid.Text = ibillid;
            this.vFieldCode.Text = vFieldCode;
            this.fieldType = fieldtype;

        }
        public void queryComInterface()
        {
            //com接口查询
            string querySql = $"select vApiID,vSQL,tba.vRemarks,vExpParam,IOrderId from TBUDT_BasicApiSQL tba left join TBUDT_BasicAllCtr tbac on tba.vApiID = tbac.vKeyValue where tbac.vkeyname = 'hzrxvapiid' and tbac.vkeyid = '{vFieldCode.Text}' and tbac.ibillid = '{ibillid.Text}'";
            DataSet ds = new DBUtil().Query(new DBUtil().GetConnection(), querySql);
            textBox1.Enabled = false;
            if (ds.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("当前字段并无Com接口！");
            }
            else
            {
                vApiId.Text = ds.Tables[0].Rows[0][0].ToString();
                vApiId.ReadOnly = true;
                vRemarks.Text = ds.Tables[0].Rows[0][2].ToString();
                vExpParam.Text = ds.Tables[0].Rows[0][3].ToString();
                vSql.Text = ds.Tables[0].Rows[0][1].ToString();
                IOrderId.Text = ds.Tables[0].Rows[0][4].ToString();
                if (fieldType == 1)
                {  // 表格处理 下拉表格有title
                    textBox1.Enabled = true;
                    string q_str = $"select vkeyValue from TBUDT_BasicListCap where ibillid = '{ibillid.Text}' and vkeyid = '{vFieldCode.Text}'";
                    ds = new DBUtil().Query(new DBUtil().GetConnection(), q_str);
                    textBox1.Text = ds.Tables[0].Rows[0][0].ToString();

                }
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*JObject jObject;
            //Json判断是否合法
            try
            {
                jObject = (JObject)JsonConvert.DeserializeObject(textBox1.Text);
            }
            catch (Exception ) {
                MessageBox.Show("表格格式不合法，请使用Json格式！");
                return;
            }*/

            //保存关联数据进去 
            DBUtil db = new DBUtil();
            if (addBtn == 0)
            {
                //update
                //更新钱数据处理 针对vSQL.Text内容处理
                string vsql = vSql.Text.Replace("'", "''");


                SqlConnection cnn = new DBUtil().GetConnection();
                SqlCommand cmd = new SqlCommand();
                SqlTransaction transaction = null;
                try {
                    transaction = cnn.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.Connection = cnn;

                    string up_SQL = $"update tbudt_basicApiSQL set vSQL=N'{vsql}',vExpParam = N'{vExpParam.Text}',vRemarks = N'{vRemarks.Text}' where vApiID = '{vApiId.Text}' ";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = up_SQL;
                    cmd.ExecuteNonQuery();
                    string up_cap = $"update TBUDT_BasicListCap set vKeyValue = '{textBox1.Text}' where ibillid = '{ibillid.Text}' and vKeyid='{vFieldCode.Text}'";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = up_cap;
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    queryComInterface();
                    MessageBox.Show("更新成功！");
                    cnn.Close();
                }
                catch(Exception ex){
                    MessageBox.Show("保存失败！", "错误");
                    transaction.Rollback();
                    cnn.Close();

                }

            }
            else
            {
                // insert 
                string vsql = vSql.Text.Replace("'", "''");

                SqlConnection cnn = new DBUtil().GetConnection();
                SqlCommand cmd = new SqlCommand();
                SqlTransaction transaction = null;
                try
                {
                    transaction = cnn.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.Connection = cnn;

                    string up_SQL = $"insert into tbudt_basicApiSql (vApiID,vSQL,vRemarks,vSQLType,vExpParam,vOrgID)values ('{vApiId.Text}','{vSql.Text}','{vRemarks.Text}',0,'{vExpParam.Text}',0)";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = up_SQL;
                    cmd.ExecuteNonQuery();
                    string up_cap = $"insert into tbudt_basicAllCtr (ibillid,vtype,vkeyid,vkeyname,vKeyValue,IOrderID,vOrgid) values ('{ibillid.Text}',1,'{vFieldCode.Text}','hzrxvapiid','{vApiId.Text}','{IOrderId.Text}',0)";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = up_cap;
                    cmd.ExecuteNonQuery();

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"insert into TBUDT_BasicListCap (IBILLID,vtype,vkeyid,vkeyname,vKeyValue,iorderid,VORGID,vDicID) VALUES('{ibillid.Text}',3,'{vFieldCode.Text}','listcaptionarr','{textBox1.Text}','{IOrderId.Text}',0,'hdata')"; ;
                    cmd.ExecuteNonQuery();


                    transaction.Commit();
                    queryComInterface();
                    MessageBox.Show("更新成功！");
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("保存失败！", "错误");
                    cnn.Close();

                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //新增标记
            vApiId.ReadOnly = false;
            addBtn = 1;
            textBox1.Enabled = true;
            string querySql = $"select max(IOrderID)+1 from TBUDT_BasicAllCtr where ibillid = '{ibillid.Text}'";
            DBUtil db = new DBUtil();
            IOrderId.Text = db.Query(new DBUtil().GetConnection(), querySql).Tables[0].Rows[0][0].ToString();
        }
    }
}
