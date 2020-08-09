using PageRederForRX.src.Function;
using PageRederTestConsole;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;

namespace PageRederForRX.formSrc
{
    public partial class BasicAllCtr : Form
    {
        System.Resources.ResourceManager rm = null;
        int addBtn = 0;
        public BasicAllCtr()
        {
            InitializeComponent();
        }
        private string ibillid, vType, vKeyid, vKeyName, vName, vkeyValue, vHzrxField1, vHzrxField2, IOrderId, vRemarks,TableName = "";
        public void setbasicAllCtr(string ibillid, string vType, string vKeyid, string vKeyName, string vName, string vkeyValue, string vHzrxField1, string vHzrxField2, string iOrderId, string vRemarks,string TableName)
        {
            this.ibillid = ibillid;
            this.vType = vType;
            this.vKeyid = vKeyid;
            this.vKeyName = vKeyName;
            this.vName = vName;
            this.vkeyValue = vkeyValue;
            this.vHzrxField1 = vHzrxField1;
            this.vHzrxField2 = vHzrxField2;
            IOrderId = iOrderId;
            this.vRemarks = vRemarks;
            this.TableName = TableName;
        }

        private void loadDefalutData()
        {
            DataLoad dl = new DataLoad();
            Basic_vKeyName.DataSource = dl.getparamcode("1243");
            Basic_vKeyName.ValueMember = "Key";
            Basic_vKeyName.DisplayMember = "Value";

            string Sql = rm.GetString("Basic_vtype_paramcode");
            Basic_vType.DataSource = dl.getParamCodeFor(Sql);
            Basic_vType.ValueMember = "key";
            Basic_vType.DisplayMember = "Value";

            //传递过来的数据 设置
            Basic_ibillid.Text = ibillid;
            Basic_ibillid.ReadOnly = true;
            Basic_vType.SelectedValue = vType;
            Basic_vKeyid.Text = vKeyid;
            Basic_vKeyName.SelectedValue = vKeyName;
            Basic_vkeyValue.Text = vkeyValue;
            Basic_vHzrxField1.Text = vHzrxField1;
            Basic_vHzrxField2.Text = vHzrxField2;
            Basic_IOrderId.Text = IOrderId;
            Basic_vRemarks.Text = vRemarks;

            addBtn = 0;


        }

       

        private void 新增ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            //

            MessageBox.Show("当前数据存储表格为多个表，暂时无法区分，新增暂停使用","锅又不是我的~");
            return;
            /*Basic_ibillid.ReadOnly = true;
            Basic_vType.SelectedItem = "";
            Basic_vKeyid.Text = "";
            Basic_vKeyName.SelectedValue = "";
            Basic_vkeyValue.Text = "";
            Basic_vHzrxField1.Text = "";
            Basic_vHzrxField2.Text = "";
            Basic_IOrderId.Text = "";
            Basic_vRemarks.Text = "";
            addBtn = 1;*/
        }

        private void 保存ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            // 保存操作
            DBUtil dB = new DBUtil();
            string TableName = "";
            if (addBtn == 0) {
                string up_sql = $"update {TableName} set vKeyName = '{Basic_vKeyName.SelectedValue}',vkeyvalue = '{Basic_vkeyValue}',vHzrxField1='{Basic_vHzrxField1.Text}',vHzrxField2 = '{Basic_vHzrxField2.Text}',IOrderId = '{Basic_IOrderId.Text}',vRemarks = '{Basic_vRemarks.Text}' where ibillid ='{Basic_ibillid}' and vkeyid = '{Basic_vKeyid.Text}' and vtype = '{Basic_vType.SelectedValue}'";
                SqlConnection cnn = new DBUtil().GetConnection();
                SqlCommand cmd = new SqlCommand();
                SqlTransaction transaction = null;
                try
                {
                    transaction = cnn.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = up_sql;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ev) {
                    
                    transaction.Rollback();
                    MessageBox.Show(ev.Message);
                }


            }

            
        }

        private void loadBasic(object sender, System.EventArgs e)
        {
            //加载窗体的时候操作
            rm = new System.Resources.ResourceManager("PageRederForRX.SqlProperty", Assembly.GetExecutingAssembly());
            loadDefalutData();
        }

        private void 退出ToolStripMenuItem_Click_1(object sender, System.EventArgs e)
        {
            Close();
        }

       
        

    }
}
