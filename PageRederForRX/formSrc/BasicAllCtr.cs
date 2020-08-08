using PageRederForRX.src.Function;
using System.Windows.Forms;

namespace PageRederForRX.formSrc
{
    public partial class BasicAllCtr : Form
    {
        public BasicAllCtr()
        {
            InitializeComponent();
        }

        public void setbasicAllCtr(string ibillid, string vType, string vKeyid, string vKeyName, string vName, string vkeyValue, string vHzrxField1, string vHzrxField2, string iOrderId, string vRemarks)
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
        }

        private string ibillid, vType, vKeyid, vKeyName, vName, vkeyValue, vHzrxField1, vHzrxField2, IOrderId, vRemarks = "";

        private void loadBasic(object sender, System.EventArgs e)
        {
            //加载窗体的时候操作
            loadDefalutData();
        }

        private void 退出ToolStripMenuItem_Click_1(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void loadDefalutData() {
            DataLoad dl = new DataLoad();
            Basic_vKeyName.DataSource = dl.getparamcode("1243");
            Basic_vKeyName.ValueMember = "Key";
            Basic_vKeyName.DisplayMember = "Value";

            //传递过来的数据 设置
            Basic_ibillid.Text = ibillid;
            Basic_vType.SelectedValue = vType;
            Basic_vKeyid.Text = vKeyid;
            Basic_vKeyName.SelectedValue = vKeyName;
            Basic_vkeyValue.Text = vkeyValue;
            Basic_vHzrxField1.Text = vHzrxField1;
            Basic_vHzrxField2.Text = vHzrxField2;
            Basic_IOrderId.Text = IOrderId;
            Basic_vRemarks.Text = vRemarks;

        }
        

    }
}
