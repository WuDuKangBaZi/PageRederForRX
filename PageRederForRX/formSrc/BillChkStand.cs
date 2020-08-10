using PageRederForRX.src.Function;
using PageRederTestConsole;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PageRederForRX.formSrc
{
    public partial class BillChkStand : Form
    {
        private int addBtn = 0;
        public string IBillId { get; set; }
        public string VOrgID { get; set; }
        public string VID { get; set; }
        public string VCheckType { get; set; }
        public string VHint { get; set; }
        public string VExPValue { get; set; }
        public string VDicID { get; set; }
        public string IOrderID { get; set; }
        public string Vtestvalue { get; set; }
        public string VRemarks { get; set; }
        Form1 f1;
        public BillChkStand(Form1 form1)
        {
            InitializeComponent();
            f1 = form1;
        }
        #region 单据属性数据初始化 数据来源为 双击表格的时候传递过来的 
        private void onShow(object sender, EventArgs e)
        {
            dataInit();
            vId.Text = VID;
            vId.ReadOnly = true;
            vCheckType.SelectedValue = VCheckType;
            vHint.Text = VHint;
            vtestvalue.Text = Vtestvalue;
            iorderid.Text = IOrderID;
            vExPValue.Text = VExPValue;
            vRemarks.Text = VRemarks;


        }
        #endregion
        #region 检测机制 下拉框值初始化
        private void dataInit()
        {

            DataLoad dl = new DataLoad();
            vCheckType.DataSource = dl.getparamcode("1005");
            vCheckType.ValueMember = "Key";
            vCheckType.DisplayMember = "Value";

        }
        #endregion
        #region 数据保存操作
        private void button1_Click(object sender, EventArgs e)
        {
            //保存数据到
            SqlConnection cnn = new DBUtil().GetConnection();
            SqlCommand cmd = new SqlCommand();
            SqlTransaction transaction = null;
            string Ex_SQL = "";
            if (addBtn == 0)
            {
                //
                Ex_SQL = $"update TBUDT_BillChkStand set vCheckType = '{vCheckType.SelectedValue.ToString()}'," +
                    $"vHint = '{vHint.Text}',vExPValue='{vExPValue.Text}',vDicID = 'hdata'," +
                    $"IOrderID = '{iorderid.Text}',vtestvalue = '{vtestvalue.Text}',vRemarks='{vRemarks.Text}' where IBillID = '{IBillId}' and vID = '{vId}'";

            }
            else
            {
                //新增
                Ex_SQL = $"insert into TBUDT_BillChkStand(IBillID,vOrgID,vID,vCheckType,vHint,vExPValue,vDicID,IOrderID,vtestvalue,vRemarks)VALUES('{IBillId}'," +
                    $"'0','{vId.Text}','{vCheckType.SelectedValue}','{vHint.Text}','{vExPValue.Text}','hdata','{iorderid.Text}','{vtestvalue.Text}','{vRemarks.Text}')";
            }
            try
            {
                transaction = cnn.BeginTransaction();
                cmd.Transaction = transaction;
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = Ex_SQL;
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();

            }
        }
        #endregion
        #region 新增菜单的点击事件
        private void 新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //新增按钮点击时 重置窗口数据
            vId.Text = "";
            vCheckType.Text = "";
            vHint.Text = "";
            vtestvalue.Text = "";
            iorderid.Text = "";
            vExPValue.Text = "";
            addBtn = 1;
            vId.ReadOnly = false;
        }
        #endregion
    }
}
