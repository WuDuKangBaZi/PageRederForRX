using PageRederForRX.src.Function;
using PageRederTestConsole;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace PageRederForRX
{
    public partial class Form1 : Form
    {
        DataSet OSDataSet = new DataSet();
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //调用主要数据加载方法
            loadDefalutComboxSource();
            DataLoad dl = new DataLoad();
            List<string> vs = dl.loadMainLayout();
            foreach (string ll in vs)
            {
                MainListBox.Items.Add(ll);
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void manListSelect(object sender, EventArgs e)
        {
            //选择后调用查询界面
            //MessageBox.Show(MainListBox.SelectedItem.ToString());
            //dataGridView1.DataSource = ds.Tables[0]; // 此处调用完毕后 需要绘制界面了
            string queryString = $"select vID,vFieldCode,vMainLable,vLable,vTop,vLeft,vWidth,vHeight,vColor,vGroundColor,IOrderID,vfrmtype,vIsShow,vgroup,vChange,vtexttype,vDefault  from TBUDT_EditLayout where vpid ='{MainListBox.SelectedItem}'";
            OSDataSet = new DBUtil().Query(new DBUtil().GetConnection(), queryString);
            dataGridView1.DataSource = OSDataSet.Tables[0];
            drawMainView(OSDataSet);

        }
        private void lockData(Boolean status)
        {
            textBox1.Enabled = status;
            textBox2.Enabled = status;
            textBox3.Enabled = status;
            textBox4.Enabled = status;
            textBox5.Enabled = status;
            textBox6.Enabled = status;
            textBox7.Enabled = status;
            textBox8.Enabled = status;
            textBox9.Enabled = status;
            textBox10.Enabled = status;
            textBox13.Enabled = status;
            textBox15.Enabled = status;

            dataGridView1.Enabled = !status;

            lockbtn.Enabled = !status;
            recbtn.Enabled = status;
            savethis.Enabled = status;
            MainListBox.Enabled = !status;

            comboBox1.Enabled = status;
            comboBox2.Enabled = status;
            comboBox3.Enabled = status;
            comboBox4.Enabled = status;
        }

        private void doubleCilck(object sender, DataGridViewCellEventArgs e)
        {
            //双击事件 双击加载属性到右侧表格内去

            int index = e.RowIndex;
            //dataGridView1.Rows[index].Cells[1].Value.ToString()
            textBox2.Text = dataGridView1.Rows[index].Cells["vFieldCode"].Value.ToString();
            textBox13.Text = dataGridView1.Rows[index].Cells["vMainLable"].Value.ToString();
            textBox10.Text = dataGridView1.Rows[index].Cells["vLable"].Value.ToString();
            textBox4.Text = dataGridView1.Rows[index].Cells["vTop"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[index].Cells["vLeft"].Value.ToString();
            textBox5.Text = dataGridView1.Rows[index].Cells["vWidth"].Value.ToString();
            textBox6.Text = dataGridView1.Rows[index].Cells["vHeight"].Value.ToString();
            textBox7.Text = dataGridView1.Rows[index].Cells["vColor"].Value.ToString();
            textBox8.Text = dataGridView1.Rows[index].Cells["vGroundColor"].Value.ToString();
            textBox9.Text = dataGridView1.Rows[index].Cells["IOrderID"].Value.ToString();
            textBox1.Text = dataGridView1.Rows[index].Cells["vgroup"].Value.ToString();
            textBox15.Text = dataGridView1.Rows[index].Cells["vDefault"].Value.ToString();



            //combobox 属性赋值
            //弹窗类型

            comboBox2.SelectedValue = dataGridView1.Rows[index].Cells["vfrmtype"].Value.ToString();

            //编码转换

            comboBox3.SelectedValue = dataGridView1.Rows[index].Cells["vChange"].Value.ToString();
            //文本类型

            comboBox4.SelectedValue = dataGridView1.Rows[index].Cells["vtexttype"].Value.ToString();
            //是否限时

            comboBox1.SelectedValue = dataGridView1.Rows[index].Cells["vIsShow"].Value.ToString();

            lockData(false);

        }
        private void drawMainView(DataSet gridViewSet)
        {
            mainPanel.Controls.Clear();
            mainPanel.BackColor = stringToColor("#ffffff");
            //主绘制窗口 从数据中加载 
            //获取到数据 开始解析绘制
            foreach (DataRow row in gridViewSet.Tables[0].Rows)
            {
                int vfrmtype = int.Parse(row[11].ToString());
                if (vfrmtype == 0)
                {
                    drawTextBox(row);
                }
                if (vfrmtype == 1 || vfrmtype == 2)
                {
                    drawComBox(row);
                }
                if (vfrmtype == 3)
                {
                    drawDateTimePicker(row);
                }
            }
            mainPanel.Refresh();

        }
        private void drawDateTimePicker(DataRow gridRow)
        {
            //数据解析
            int top_int = int.Parse(gridRow[4].ToString());
            int left_int = int.Parse(gridRow[5].ToString());
            int grid_width = int.Parse(gridRow[6].ToString());
            int grid_height = int.Parse(gridRow[7].ToString());
            string vColor = gridRow[8].ToString();
            string vGroundColor = gridRow[9].ToString();
            //int vfrmtype = int.Parse(gridRow[9].ToString());
            int vIsShow = 0;// int.Parse(gridRow[10].ToString() == null ? gridRow[10].ToString() : "1");
            if (gridRow[12].ToString() == null || gridRow[12].ToString() == "")
            {
                vIsShow = 1;
            }
            else
            {
                vIsShow = int.Parse(gridRow[12].ToString());
            }
            //绘制 DateTimePicker
            Label label = drawLabl(gridRow);
            DateTimePicker tb = new DateTimePicker();
            tb.Name = gridRow[1].ToString();
            tb.Width = grid_width - 80;
            tb.Height = grid_height;
            tb.Top = top_int;
            tb.Left = left_int + 80;
            tb.ForeColor = stringToColor(vColor);
            tb.BackColor = stringToColor(vGroundColor);
            if (vIsShow == 1)
            {
                label.Visible = true;
                tb.Visible = true;
            }
            else
            {
                label.Visible = false;
                tb.Visible = false;
            }
            mainPanel.Controls.Add(label);
            mainPanel.Controls.Add(tb);
        }
        private void drawComBox(DataRow gridRow)
        {
            //数据解析
            int top_int = int.Parse(gridRow[4].ToString());
            int left_int = int.Parse(gridRow[5].ToString());
            int grid_width = int.Parse(gridRow[6].ToString());
            int grid_height = int.Parse(gridRow[7].ToString());
            string vColor = gridRow[8].ToString();
            string vGroundColor = gridRow[9].ToString();
            //int vfrmtype = int.Parse(gridRow[9].ToString());
            int vIsShow = 0;// int.Parse(gridRow[10].ToString() == null ? gridRow[10].ToString() : "1");
            if (gridRow[12].ToString() == null || gridRow[12].ToString() == "")
            {
                vIsShow = 1;
            }
            else
            {
                vIsShow = int.Parse(gridRow[12].ToString());
            }

            //绘制Label
            Label label = drawLabl(gridRow);
            ComboBox tb = new ComboBox();
            tb.Name = gridRow[1].ToString();
            tb.Width = grid_width - 80;
            tb.Height = grid_height;
            tb.Top = top_int;
            tb.Left = left_int + 80;
            tb.ForeColor = stringToColor(vColor);
            tb.BackColor = stringToColor(vGroundColor);
            if (vIsShow == 1)
            {
                label.Visible = true;
                tb.Visible = true;
            }
            else
            {
                label.Visible = false;
                tb.Visible = false;
            }
            mainPanel.Controls.Add(label);
            mainPanel.Controls.Add(tb);

        }
        private void drawTextBox(DataRow gridRow)
        {
            //获取基础类型
            int top_int = int.Parse(gridRow[4].ToString());
            int left_int = int.Parse(gridRow[5].ToString());
            int grid_width = int.Parse(gridRow[6].ToString());
            int grid_height = int.Parse(gridRow[7].ToString());
            string vColor = gridRow[8].ToString();
            string vGroundColor = gridRow[9].ToString();
            //int vfrmtype = int.Parse(gridRow[9].ToString());
            int vIsShow = 0;// int.Parse(gridRow[10].ToString() == null ? gridRow[10].ToString() : "1");
            if (gridRow[12].ToString() == null || gridRow[12].ToString() == "")
            {
                vIsShow = 1;
            }
            else
            {
                vIsShow = int.Parse(gridRow[12].ToString());
            }

            //绘制Label
            Label label = drawLabl(gridRow);
            /* label.Visible = vIsShow == 0 ? true : false;*/
            //绘制TextBox
            TextBox tb = new TextBox();
            tb.Name = gridRow[1].ToString();
            tb.Width = grid_width - 80;
            tb.Height = grid_height;
            tb.Top = top_int;
            tb.Left = left_int + 80;
            tb.ForeColor = stringToColor(vColor);
            tb.BackColor = stringToColor(vGroundColor);
            if (vIsShow == 1)
            {
                label.Visible = true;
                tb.Visible = true;
            }
            else
            {
                label.Visible = false;
                tb.Visible = false;
            }
            mainPanel.Controls.Add(label);
            mainPanel.Controls.Add(tb);


        }

        private Label drawLabl(DataRow gridRow)
        {
            //获取基础类型
            int top_int = int.Parse(gridRow[4].ToString());
            int left_int = int.Parse(gridRow[5].ToString());
            int grid_width = int.Parse(gridRow[6].ToString());
            int grid_height = int.Parse(gridRow[7].ToString());
            string vColor = gridRow[8].ToString();
            string vGroundColor = gridRow[9].ToString();
            //int vfrmtype = int.Parse(gridRow[9].ToString());
            int vIsShow = 0;// int.Parse(gridRow[10].ToString() == null ? gridRow[10].ToString() : "1");
            if (gridRow[10].ToString() == null || gridRow[10].ToString() == "")
            {
                vIsShow = 1;
            }
            else
            {
                vIsShow = int.Parse(gridRow[10].ToString());
            }

            //绘制Label
            Label label = new Label();
            label.Text = gridRow[2].ToString();
            label.Width = 80;
            label.Height = 25;
            label.AutoSize = false;
            label.Top = top_int;
            label.Left = left_int;
            label.ForeColor = stringToColor(vColor);
            label.BackColor = stringToColor(vGroundColor);
            return label;
        }
        private Color stringToColor(string colorString)
        {
            try
            {
                return ColorTranslator.FromHtml(colorString);
            }
            catch (Exception e)
            {
                MessageBox.Show("这个单据的颜色配置异常，去数据库修改试试！");
                return ColorTranslator.FromHtml("#fffff");
            }
        }

        private void onload(object sender, EventArgs e)
        {
            DataLoad dl = new DataLoad();
            BindingSource bs = new BindingSource();
            bs.DataSource = dl.getDictonaryByvfrmtype();
            comboBox2.DataSource = dl.getparamcode("9871");
            comboBox2.ValueMember = "code";
            comboBox2.DisplayMember = "name";
        }

        private void loadDefalutComboxSource()
        {
            //默认数据加载部分 combox2
            DataLoad dl = new DataLoad();
            BindingSource bs = new BindingSource();
            //是否显示
            comboBox1.DataSource = dl.getparamcode("5");
            comboBox1.ValueMember = "Key";
            comboBox1.DisplayMember = "Value";
            //弹窗类型
            comboBox2.DataSource = dl.getparamcode("9871");
            comboBox2.ValueMember = "Key";
            comboBox2.DisplayMember = "Value";
            //编码转换
            comboBox3.DataSource = dl.getparamcode("9872");
            comboBox3.ValueMember = "Key";
            comboBox3.DisplayMember = "Value";
            //文本类型 9873
            comboBox4.DataSource = dl.getparamcode("9873");
            comboBox4.ValueMember = "Key";
            comboBox4.DisplayMember = "Value";
        }

        private void lockbtn_Click(object sender, EventArgs e)
        {
            lockData(true);
        }

        private void recbtn_Click(object sender, EventArgs e)
        {
            lockData(false);
        }

        private void combtn_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length < 1)
            {
                MessageBox.Show("未选中字段");
            }
            else
            {
                detailedForm df = new detailedForm();

                df.setParam(new DBUtil().Query(new DBUtil().GetConnection(),
                    $"select IBillID from TBUDT_ModeLayout where vid ='{MainListBox.SelectedItem}'").Tables[0].Rows[0][0].ToString(),
                    textBox2.Text, int.Parse(comboBox2.SelectedValue.ToString()));
                df.Show();
            }

        }

        private void savethis_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null || comboBox2.SelectedValue == null || comboBox3.SelectedValue == null || comboBox4.SelectedValue == null ) {
                MessageBox.Show("关键数据存在空值！不能保存！");
                return;
            }

            SqlConnection cnn = new DBUtil().GetConnection();
            SqlCommand cmd = new SqlCommand();
            SqlTransaction transaction = null;
            try
            {
                transaction = cnn.BeginTransaction();
                cmd.Transaction = transaction;
                cmd.Connection = cnn;

                string save_sql = $"update TBUDT_EditLayout set vMainLable = '{textBox13.Text}',vLable='{textBox10.Text}',vTop = '{textBox4.Text}', " +
                $"vLeft = '{textBox3.Text}', vWidth = '{textBox5.Text}',vHeight = '{textBox6.Text}',vColor = '{textBox7.Text}',vGroundColor = '{textBox8.Text}'" +
                $",IOrderID = '{textBox9.Text}',vfrmtype ='{comboBox2.SelectedValue.ToString()}',vIsShow = '{comboBox1.SelectedValue.ToString()}',vChange = '{comboBox3.SelectedValue.ToString()}'" +
                $",vtexttype = '{comboBox4.SelectedValue.ToString()}',vDefault = '{textBox15.Text}' where vFieldCode = '{textBox2.Text}'";

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = save_sql;
                cmd.ExecuteNonQuery();
                transaction.Commit();

                //重新绘制
                string queryString = $"select vID,vFieldCode,vMainLable,vLable,vTop,vLeft,vWidth,vHeight,vColor,vGroundColor,IOrderID,vfrmtype,vIsShow,vgroup,vChange,vtexttype,vDefault  from TBUDT_EditLayout where vpid ='{MainListBox.SelectedItem}'";
                OSDataSet.Clear();
                OSDataSet = new DBUtil().Query(new DBUtil().GetConnection(), queryString);
                dataGridView1.DataSource = OSDataSet.Tables[0];
                drawMainView(OSDataSet);

                MessageBox.Show("更新成功！");
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败！", "错误");
                transaction.Rollback();
                cnn.Close();

            }

            // 保存布局信息
           /* string save_sql = $"update TBUDT_EditLayout set vMainLable = '{textBox13.Text}',vLable='{textBox10.Text}',vTop = '{textBox4.Text}', " +
                $"vLeft = '{textBox3.Text}', vWidth = '{textBox5.Text}',vHeight = '{textBox6.Text}',vColor = '{textBox7.Text}',vGroundColor = '{textBox8.Text}'" +
                $",IOrderID = '{textBox9.Text}',vfrmtype ='{comboBox2.SelectedValue.ToString()}',vIsShow = '{comboBox1.SelectedValue.ToString()}',vChange = '{comboBox3.SelectedValue.ToString()}'" +
                $",vtexttype = '{comboBox4.SelectedValue.ToString()}',vDefault = '{textBox15.Text}' where vFieldCode = '{textBox2.Text}'";

            DBUtil dB = new DBUtil();

            int re_count = dB.doExecute(save_sql);
            if (re_count != 1)
            {
                MessageBox.Show("更新失败！");
            }
            else
            {
                MessageBox.Show("更新成功");
                string queryString = $"select vID,vFieldCode,vMainLable,vLable,vTop,vLeft,vWidth,vHeight,vColor,vGroundColor,IOrderID,vfrmtype,vIsShow,vgroup,vChange,vtexttype,vDefault  from TBUDT_EditLayout where vpid ='{MainListBox.SelectedItem}'";
                OSDataSet.Clear();
                OSDataSet = new DBUtil().Query(new DBUtil().GetConnection(), queryString);
                dataGridView1.DataSource = OSDataSet.Tables[0];
                drawMainView(OSDataSet);
            }*/
        }
    }
}
