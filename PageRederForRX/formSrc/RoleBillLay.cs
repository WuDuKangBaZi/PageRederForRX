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
    public partial class RoleBillLay : Form
    {
        /*private int addBtn = 0;*/
        public string IBillId { get; set; }
        public string VOrgID { get; set; }
        public string VID { get; set; }
        public string VFieldValueType { get; set; }
        public string VFieldValue { get; set; }
        public string VExPValue { get; set; }
        public string VFieldMapName { get; set; }
        public string IOrderID { get; set; }
        public string VIsUse { get; set; }

        public RoleBillLay()
        {
            InitializeComponent();
        }

        #region 单据属性数据初始化 数据来源为 双击表格的时候传递过来的 
        private void onShow(object sender, EventArgs e)
        {
            dataInit();
            vId.Text = VID;
            vId.ReadOnly = true;
            vfieldvaluetype.SelectedValue = VFieldValueType;
            vfieldvalue.Text = VExPValue;
            vfieldmapname.Text = VFieldMapName;
            iorderid.Text = IOrderID;
            vexpvalue.Text = VExPValue;
            vorgid.SelectedValue = VOrgID;
            visuse.SelectedValue = VIsUse;

        }
        #endregion

        #region 检测机制 下拉框值初始化
        private void dataInit()
        {

            DataLoad dl = new DataLoad();
            vfieldvaluetype.DataSource = dl.getparamcode("4568");
            vfieldvaluetype.ValueMember = "Key";
            vfieldvaluetype.DisplayMember = "Value";
            visuse.DataSource = dl.getparamcode("55");
            visuse.ValueMember = "Key";
            visuse.DisplayMember = "Value";

        }
        #endregion
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void vfieldvaluetype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
