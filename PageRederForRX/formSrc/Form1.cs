using PageRederForRX.formSrc;
using PageRederForRX.src.Function;
using PageRederForRX.src.LayoutEdit;
using PageRederTestConsole;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace PageRederForRX
{
    public partial class Form1 : Form
    {
        DataSet OSDataSet = new DataSet();
        private int com_add_btn = 0;
        System.Resources.ResourceManager rm = null;
        public Form1()
        {
            InitializeComponent();
            //此处为窗口初始化操作 可以用来检查一些配置之类的.
            rm = new System.Resources.ResourceManager("PageRederForRX.SqlProperty", Assembly.GetExecutingAssembly());

        }


        #region 第一次显示窗口的时候 可用作数据检查等
        private void firstLoad(object sender, EventArgs e)
        {
            messageList.Text = rm.GetString("ErrorNote");
            
        }
        #endregion
        #region 加载主要数据 即 读取布局信息 主表:TBUDT_ModeLayout 从表   TBUDT_ChartLayout

        private void button1_Click_1(object sender, EventArgs e)
        {
            // 查询主表数据
            MainListBox.Items.Clear();
            loadDefalutComboxSource();
            MainListBox.Items.AddRange(new DataLoad().loadMainLayout(textBox11.Text).ToArray());
            // 获取从表数据
            ChartLayoutListBox.Items.Clear();
            ChartLayoutListBox.Items.AddRange(new GetLayoutConfig().getChartLayoutList(textBox11.Text).ToArray());
            /* vs = dl.*/

        }
        #endregion

        #region 加载主表的数据
        private void manListSelect(object sender, EventArgs e)
        {
            //选择后调用查询界面
            //MessageBox.Show(MainListBox.SelectedItem.ToString());
            //dataGridView1.DataSource = ds.Tables[0]; // 此处调用完毕后 需要绘制界面了
            string sql = rm.GetString("query_TBUDT_EditLayout").Replace("{MainListBox.SelectedItem}", MainListBox.SelectedItem.ToString());
            //string queryString = $"select vID,vFieldCode,vMainLable,vLable,vTop,vLeft,vWidth,vHeight,vColor,vGroundColor,IOrderID,vfrmtype,ISNULL(vIsShow, '1') vIsShow ,vgroup,vChange,vtexttype,vDefault  from TBUDT_EditLayout where vpid ='{MainListBox.SelectedItem}'";
            OSDataSet = new DBUtil().Query(new DBUtil().GetConnection(), sql);
            ChartLayout_dataGridView.Visible = false;
            dataGridView1.Visible = true;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.DataSource = OSDataSet.Tables[0];
            ChartLayout_panel.Visible = false;
            mainPanel.Visible = true;
            mainPanel.Dock = DockStyle.Top;
            //mainPanel.AutoSize = true;
            mainPanel.Height = 500;
            mainPanel.AutoScroll = true;
            drawMainView(OSDataSet);
            getBasiList("");

        }
        #region 加载从表的数据
        private void chartLayourChange(object sender, EventArgs e)
        {
            //选择后调用查询界面
            string sql = rm.GetString("query_TBUDT_ChartLayout").Replace("{ChartLayoutListBox.SelectedItem}", ChartLayoutListBox.SelectedItem.ToString());
            //string queryString = $"select vID,vFieldCode,vMainLable,vLable,vTop,vLeft,vWidth,vHeight,vColor,vGroundColor,IOrderID,vfrmtype,vIsShow,vgroup,vChange,vtexttype,vDefault  from TBUDT_ChartLayout where vpid ='{ChartLayoutListBox.SelectedItem}'";
            OSDataSet = new DBUtil().Query(new DBUtil().GetConnection(), sql);
            ChartLayout_dataGridView.Visible = true;
            mainPanel.Visible = false;
            dataGridView1.Visible = false;
            /*ChartLayoutListBox.Dock = DockStyle.Fill;*/
            ChartLayout_dataGridView.DataSource = OSDataSet.Tables[0];
            ChartLayout_dataGridView.Dock = DockStyle.Fill;
            ChartLayout_panel.Visible = true;
            ChartLayout_panel.Dock = DockStyle.Top;
            //ChartLayout_panel.AutoSize = true;
            ChartLayout_panel.Height = 500;
            ChartLayout_panel.AutoScroll = true;

            /*drawChartLayout(OSDataSet);*/
            getBasiList("");
            MessageBox.Show("数据原因，从表暂不支持预览！");
            ChartLayout_panel.Height = 500;
        }
        #endregion
        #endregion
        #region 单据属性布局字段修改权限
        private void lockData(bool status)
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
        #endregion
        #region 双击单据字段列表时 将字段填充到右侧编辑表格内
        #region 主表点击
        private void doubleCilck(object sender, DataGridViewCellEventArgs e)
        {
            //双击事件 双击加载属性到右侧表格内去
            try
            {
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
            catch (ArgumentOutOfRangeException ooe)
            {
                MessageBox.Show("点击表头就会触发这个问题！");

            }

        }
        #endregion
        #region 从表点击事件
        private void chartLayout_click(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                textBox2.Text = ChartLayout_dataGridView.Rows[index].Cells["ChartLayout_vFieldCode"].Value.ToString();
                textBox13.Text = ChartLayout_dataGridView.Rows[index].Cells["ChartLayout_vFieldName"].Value.ToString();
                //textBox10.Text = ChartLayout_dataGridView.Rows[index].Cells["vLable"].Value.ToString();
                //textBox4.Text = ChartLayout_dataGridView.Rows[index].Cells["vTop"].Value.ToString();
                //textBox3.Text = ChartLayout_dataGridView.Rows[index].Cells["vLeft"].Value.ToString();
                textBox5.Text = ChartLayout_dataGridView.Rows[index].Cells["ChartLayout_vWidth"].Value.ToString();
                textBox6.Text = ChartLayout_dataGridView.Rows[index].Cells["ChartLayout_vHeight"].Value.ToString();
                //textBox7.Text = ChartLayout_dataGridView.Rows[index].Cells["vColor"].Value.ToString();
                //textBox8.Text = ChartLayout_dataGridView.Rows[index].Cells["vGroundColor"].Value.ToString();
                textBox9.Text = ChartLayout_dataGridView.Rows[index].Cells["ChartLayout_IOrderID"].Value.ToString();
                textBox1.Text = ChartLayout_dataGridView.Rows[index].Cells["ChartLayout_vgroup"].Value.ToString();
                //textBox15.Text = ChartLayout_dataGridView.Rows[index].Cells["vDefault"].Value.ToString();
                //combobox 属性赋值
                //弹窗类型

                comboBox2.SelectedValue = ChartLayout_dataGridView.Rows[index].Cells["ChartLayout_vfrmtype"].Value.ToString();

                //编码转换

                comboBox3.SelectedValue = ChartLayout_dataGridView.Rows[index].Cells["ChartLayout_vChange"].Value.ToString();
                //文本类型

                comboBox4.SelectedValue = ChartLayout_dataGridView.Rows[index].Cells["ChartLayout_vtexttype"].Value.ToString();
                //是否限时

                comboBox1.SelectedValue = ChartLayout_dataGridView.Rows[index].Cells["ChartLayout_vIsShow"].Value.ToString();

                lockData(false);

            }
            catch (ArgumentOutOfRangeException ooe)
            {
                MessageBox.Show("点击表头就会触发这个问题！");

            }
        }

        #endregion

        #endregion
        #region 界面绘制
        #region 主表绘制
        private void drawMainView(DataSet gridViewSet)
        {
            mainPanel.Controls.Clear();
            mainPanel.BackColor = stringToColor("#ffffff");
            //主绘制窗口 从数据中加载 
            //获取到数据 开始解析绘制
            ChartLayoutListBox.SelectedValue = "";
            foreach (DataRow row in gridViewSet.Tables[0].Rows)
            {
                int vfrmtype = int.Parse(row[11].ToString());
                drawEditLayout draw = new drawEditLayout();

                myLable lable = draw.drawlable(row);
                lable.ibillid = MainListBox.SelectedItem.ToString();
                lable.DoubleClick += new System.EventHandler(textboxDoubleClike);
                mainPanel.Controls.Add(lable);

                if (vfrmtype == 0)
                {

                    TextBox tb = draw.drawTextBox(row);
                    mainPanel.Controls.Add(tb);
                }
                if (vfrmtype == 1 || vfrmtype == 2)
                {
                    ComboBox combo = draw.drawComBox(row);
                    mainPanel.Controls.Add(combo);


                }
                if (vfrmtype == 3)
                {
                    DateTimePicker dateTimePicker = draw.drawDateTimePicker(row);
                    mainPanel.Controls.Add(dateTimePicker);
                }
            }
            mainPanel.Refresh();

        }
        #endregion
        #region 从表绘制
        private void drawChartLayout(DataSet gridVewSet)
        {
            MainListBox.SelectedValue = "";
            ChartLayout_panel.Controls.Clear();
            ChartLayout_panel.BackColor = stringToColor("#ffffff");
            // 针对从表 采用流布局 需要指定宽度  高度
            string querySql = rm.GetString("query_ChartLayout_For_TBUDT_ModeLayout").Replace("{ChartLayoutListBox.SelectedItem}", ChartLayoutListBox.SelectedItem.ToString());
            DataSet ds = new DBUtil().Query(new DBUtil().GetConnection(), querySql);
            ChartLayout_panel.Width = int.Parse(ds.Tables[0].Rows[0]["vwidth"].ToString());
            ChartLayout_panel.Height = int.Parse(ds.Tables[0].Rows[0]["vHeight"].ToString());

            //遍历数据 开始绘制
            foreach (DataRow row in gridVewSet.Tables[0].Rows)
            {
                Console.WriteLine(row["vfrmtype"]);
                int vfrmtype = int.Parse(row["vfrmtype"].ToString());
                drawChartLayout draw = new drawChartLayout();
                // 特殊性 需要用一个panel来装
                myLable lable = draw.drawlable(row);
                lable.ibillid = ChartLayoutListBox.SelectedItem.ToString();
                lable.DoubleClick += new System.EventHandler(textboxDoubleClike);
                Panel p = new Panel();
                p.Controls.Add(lable);
                if (vfrmtype == 0)
                {

                    TextBox tb = draw.drawTextBox(row);
                    p.Controls.Add(tb);
                }
                if (vfrmtype == 1 || vfrmtype == 2)
                {
                    ComboBox combo = draw.drawComBox(row);
                    p.Controls.Add(combo);


                }
                if (vfrmtype == 3)
                {
                    DateTimePicker dateTimePicker = draw.drawDateTimePicker(row);
                    p.Controls.Add(dateTimePicker);
                }
                ChartLayout_panel.Controls.Add(p);


            }






        }


        #endregion
        #endregion

        #region 字符串转颜色 暂时没用了
        private Color stringToColor(string colorString)
        {
            try
            {
                return ColorTranslator.FromHtml(colorString);
            }
            catch (Exception e)
            {
                //MessageBox.Show("这个单据的颜色配置异常，去数据库修改试试！");
                return ColorTranslator.FromHtml("#fffff");
            }
        }
        #endregion

        #region 右侧布局属性下拉框的默认值加载
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
        #endregion
        #region 锁定单据布局属性字段修改 
        private void lockbtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Visible)
            {
                if (textBox2.Text.Length < 1)
                {
                    MessageBox.Show("未选中任何字段！", "错误");
                    return;
                }
                lockData(true);

            }
            if (ChartLayout_dataGridView.Visible)
            {
                //从表的锁定
                //5 6 13 cb1
                if (textBox2.Text.Length < 1)
                {
                    MessageBox.Show("未选中任何字段！", "错误");
                    return;
                }
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                textBox13.Enabled = true;
                comboBox1.Enabled = true;

            }
        }
        #endregion
        #region 解锁单据布局属性字段修改
        private void recbtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Visible)
            {
                if (textBox2.Text.Length < 1)
                {
                    MessageBox.Show("未选中任何字段！", "错误");
                    return;
                }
                lockData(false);

            }
            if (ChartLayout_dataGridView.Visible)
            {
                //从表的锁定
                //5 6 13 cb1
                if (textBox2.Text.Length < 1)
                {
                    MessageBox.Show("未选中任何字段！", "错误");
                    return;
                }
                textBox5.Enabled = false;
                textBox6.Enabled = false;
                textBox13.Enabled = false;
                comboBox1.Enabled = false;

            }
        }
        #endregion
        #region com接口查询按钮 废弃 后续删除
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
        #endregion
        #region 布局更改界面的保存按钮 保存布局 其中提示的关键数据为几个下拉框 
        private void savethis_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Visible)
            {
                if (comboBox1.SelectedValue == null || comboBox2.SelectedValue == null || comboBox3.SelectedValue == null || comboBox4.SelectedValue == null)
                {
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
                    $",vtexttype = '{comboBox4.SelectedValue.ToString()}',vDefault = '{textBox15.Text}' where vFieldCode = '{textBox2.Text}' and vpid = '{MainListBox.SelectedValue.ToString()}' ";

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
            }
            else if (ChartLayout_dataGridView.Visible)
            {

                if (comboBox1.SelectedValue == null)
                {
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
                    string sql = $"	update TBUDT_ChartLayout set vFieldName = '{textBox13.Text}',vWidth = '{textBox5.Text}',vHeight = '{textBox6.Text}' vIsShow = '{comboBox1.SelectedValue.ToString()}' where vpid = '{ChartLayoutListBox.SelectedValue.ToString()}' and vfiledcode = '{textBox2.Text}'";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    cnn.Close();
                    MessageBox.Show("更新成功！");
                    
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    cnn.Close();
                    MessageBox.Show("保存失败！", "错误");
                    

                }
            }
            else
            {
                MessageBox.Show("发生无法预料的问题！", "错误");

            }

        }
        #endregion
        #region 单据属性 table页 查询按钮事件 
        private void button2_Click(object sender, EventArgs e)
        {
            //加载数据
            if (MainListBox.SelectedItem == null)
            {
                //未选中
                MessageBox.Show("未选中任何单据！");

            }
            else
            {
                queryTBUDT_BillChkStand();
            }
        }
        #endregion
        #region 单据属性 table页 查询方法
        void queryTBUDT_BillChkStand()
        {
            string querySql = $"select IBillId,vOrgID,vID,vCheckType,vHint,vExPValue,vDicID,IOrderID,vtestvalue,vRemarks from TBUDT_BillChkStand where IBillID = (select IBillID from  TBUDT_ModeLayout where vid = '{MainListBox.SelectedItem}')";
            DataSet ds = new DBUtil().Query(new DBUtil().GetConnection(), querySql);
            shuxingDataGrid.DataSource = ds.Tables[0];

        }
        #endregion
        #region 单据属性table页 表格双击事件
        private void shuxing_doubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //双击
            int rowIndex = e.RowIndex;
            BillChkStand stand = new BillChkStand();
            stand.IBillId = shuxingDataGrid.Rows[rowIndex].Cells["IBillId"].Value.ToString();
            stand.VOrgID = shuxingDataGrid.Rows[rowIndex].Cells["vOrgID"].Value.ToString();
            stand.VID = shuxingDataGrid.Rows[rowIndex].Cells["cvID"].Value.ToString();
            stand.VCheckType = shuxingDataGrid.Rows[rowIndex].Cells["vCheckType"].Value.ToString();
            stand.VHint = shuxingDataGrid.Rows[rowIndex].Cells["vHint"].Value.ToString();
            stand.VExPValue = shuxingDataGrid.Rows[rowIndex].Cells["vExPValue"].Value.ToString();
            stand.VDicID = shuxingDataGrid.Rows[rowIndex].Cells["vDicID"].Value.ToString();
            stand.IOrderID = shuxingDataGrid.Rows[rowIndex].Cells["cIOrderID"].Value.ToString();
            stand.Vtestvalue = shuxingDataGrid.Rows[rowIndex].Cells["vtestvalue"].Value.ToString();
            stand.VRemarks = shuxingDataGrid.Rows[rowIndex].Cells["vRemarks"].Value.ToString();
            stand.Show();

        }
        #endregion

        #region 这是给布局预览界面的 标签 添加的自定义双击按钮功能
        private void textboxDoubleClike(object sender, EventArgs e)
        {
            // 调用查询com接口
            myLable lable = (myLable)sender;
            loadComEdit(lable.inputName, lable.fieldType);
            //调用查询前段依赖
            getBasiList($" and vKeyid = '{lable.inputName}'");
            return;
        }
        #endregion
        #region 加载Com接口布局 Table页数据
        private void loadComEdit(string vFiledName, int fieldType)
        {
            com_vApiId.ReadOnly = true;
            com_IOrderId.ReadOnly = true;
            com_vFieldValue.Text = vFiledName;
            string querySql = $"select vApiID,vSQL,tba.vRemarks,vExpParam,IOrderId from TBUDT_BasicApiSQL tba" +
                $" left join TBUDT_BasicAllCtr tbac on tba.vApiID = tbac.vKeyValue " +
                $"where tbac.vkeyname = 'hzrxvapiid' and tbac.vkeyid = '{vFiledName}' and tbac.ibillid = (select  ibillid from TBUDT_ModeLayout where vid ='{MainListBox.SelectedItem}')";
            DBUtil dBUtil = new DBUtil();
            try
            {
                DataSet ds = dBUtil.Query(dBUtil.GetConnection(), querySql);
                com_vApiId.Text = ds.Tables[0].Rows[0][0].ToString();
                com_vSQL.Text = ds.Tables[0].Rows[0][1].ToString();
                com_vRemarks.Text = ds.Tables[0].Rows[0][2].ToString();
                com_vExpParam.Text = ds.Tables[0].Rows[0][3].ToString();
                com_IOrderId.Text = ds.Tables[0].Rows[0][4].ToString();
                com_add_btn = 0;
                if (fieldType == 1)
                {  // 表格处理 下拉表格有title
                    textBox1.Enabled = true;
                    string q_str = $"select vkeyValue from TBUDT_BasicListCap where ibillid = (select  ibillid from TBUDT_ModeLayout where vid ='{MainListBox.SelectedItem}') and vkeyid = '{vFiledName}'";
                    ds = new DBUtil().Query(new DBUtil().GetConnection(), q_str);
                    com_title_vkeyValue.Text = ds.Tables[0].Rows[0][0].ToString();

                }
                else
                {
                    com_title_vkeyValue.Text = "";


                }
            }
            catch (IndexOutOfRangeException)
            {
                //此处捕获的是 查询com接口不出数据 意思就是这一段没有com接口 所以 执行点击新增的功能 
                addBtn();
            }

        }
        #endregion
        #region com接口Table页上新增按钮被点击后执行的操作
        private void button3_Click(object sender, EventArgs e)
        {
            //表格新增.
            //新增标记
            addBtn();
        }
        #endregion
        #region com接口的新增按钮调用的方法 
        private void addBtn()
        {
            com_add_btn = 1;
            com_vApiId.ReadOnly = false;
            // com_IOrderId.ReadOnly = false;
            string querySql = $"select max(IOrderID)+1 from TBUDT_BasicAllCtr where ibillid = (select  ibillid from TBUDT_ModeLayout where vid ='{MainListBox.SelectedItem}')";
            DBUtil db = new DBUtil();
            com_IOrderId.Text = db.Query(new DBUtil().GetConnection(), querySql).Tables[0].Rows[0][0].ToString();
            com_vApiId.Text = "";
            com_vSQL.Text = "";
            com_vRemarks.Text = "";
            com_vExpParam.Text = "";
            com_title_vkeyValue.Text = "";
            com_title_vkeyValue.ReadOnly = false;
        }
        #endregion
        #region 全局获取Ibilldid 有的地方不一定有IbillId这个参数 可以用这个方法来根据选择的布局单号获取
        private string getIbilldId()
        {

            try
            {
                DBUtil db = new DBUtil();
                DataSet ds = db.Query(db.GetConnection(), $"select  ibillid from TBUDT_ModeLayout where vid ='{MainListBox.SelectedItem}'");
                return ds.Tables[0].Rows[0][0].ToString();
            }
            catch (IndexOutOfRangeException)
            {
                return "";
            }
        }
        #endregion
        #region Com接口的数据保存操作

        private void button4_Click(object sender, EventArgs e)
        {
            //com接口保存数据操作 暂时只能 操作主表 后续增加从表
            DBUtil db = new DBUtil();
            if (com_add_btn == 0)
            {
                //update
                //更新前数据处理 针对vSQL.Text内容处理
                string vsql = com_vSQL.Text.Replace("'", "''");


                SqlConnection cnn = new DBUtil().GetConnection();
                SqlCommand cmd = new SqlCommand();
                SqlTransaction transaction = null;
                try
                {
                    /*
                     * 方法解释：
                     * string up_SQL=$""; 添加$符号，可以更为灵活的替换参数 拼接sql 目前没有使用LinQ方式 替换参数方式为 {参数名} TextBox的值为： textBox.Text 下拉框(comBoBox)的值为 comBoBox.selectValue.toString() 更多可以在其他地方找到
                     * cmd.CommandType 这句话是固定的 执行事务的语句为Text 即文本
                     * cmd.CommAndText  指定要执行的语句
                     * cmd.ExecuteNonQuery() 这句语句的意思是执行前面设定的SQL 有一个返回值 int类型 整数 告诉你影响了几行数据
                     */
                    transaction = cnn.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.Connection = cnn;

                    string up_SQL = $"update tbudt_basicApiSQL set vSQL=N'{com_vSQL.Text}',vExpParam = N'{com_vExpParam.Text}',vRemarks = N'{com_vRemarks.Text}' where vApiID = '{com_vApiId.Text}' ";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = up_SQL;
                    cmd.ExecuteNonQuery();
                    string up_cap = $"update TBUDT_BasicListCap set vKeyValue = '{com_title_vkeyValue.Text}' where ibillid = '{getIbilldId()}' and vKeyid='{com_vFieldValue.Text}'";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = up_cap;
                    cmd.ExecuteNonQuery();
                    // 在所有事务语句写完后 transaction.Commit必须写 不然会出现报错或者其他 意思是提交更改 
                    transaction.Commit();
                    //queryComInterface();
                    MessageBox.Show("更新成功！");
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    // transaction.Rollback(); 回滚操作 在进入到cath 后回滚update或者insert 避免因为出现错误而导致数据异常

                    transaction.Rollback();
                    MessageBox.Show("保存失败！", "错误");
                    cnn.Close();

                }

            }
            else if (com_add_btn == 1)
            {
                // insert 
                string vsql = com_vSQL.Text.Replace("'", "''");

                SqlConnection cnn = new DBUtil().GetConnection();
                SqlCommand cmd = new SqlCommand();
                SqlTransaction transaction = null;
                try
                {
                    transaction = cnn.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.Connection = cnn;



                    string up_SQL = $"insert into tbudt_basicApiSql (vApiID,vSQL,vRemarks,vSQLType,vExpParam,vOrgID)values ('{com_vApiId.Text}','{com_vSQL.Text}','{com_vRemarks.Text}',0,'{com_vExpParam.Text}',0)";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = up_SQL;
                    cmd.ExecuteNonQuery();

                    string up_cap = $"insert into tbudt_basicAllCtr (ibillid,vtype,vkeyid,vkeyname,vKeyValue,IOrderID,vOrgid) values ('{getIbilldId()}',1,'{com_vFieldValue.Text}','hzrxvapiid','{com_vApiId.Text}','{com_IOrderId.Text}',0)";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = up_cap;
                    cmd.ExecuteNonQuery();
                    // 这里这一句 关联 有主从表字段 hdata 暂时没有做从表的操作 后续执行。
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"insert into TBUDT_BasicListCap (IBILLID,vtype,vkeyid,vkeyname,vKeyValue,iorderid,VORGID,vDicID) VALUES('{getIbilldId()}',3,'{com_vFieldValue.Text}','listcaptionarr','{textBox1.Text}','{com_IOrderId.Text}',0,'hdata')"; ;
                    cmd.ExecuteNonQuery();


                    transaction.Commit();
                    //queryComInterface();
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
        #endregion

        #region 单据属性
        #region 单据属性列表获取
        private void getBasiList(string whereIs)
        {
            // sql太长了 直接写到SqlProperty.resx里面去了 然后通过Replace替换掉
            string Sql = rm.GetString("getBasicSQL");
            Sql = Sql.Replace("{getIbilldId()}", getIbilldId()).Replace("{whereIs}", whereIs);
            DataSet ds = new DBUtil().Query(new DBUtil().GetConnection(), Sql);
            Basic_dataGridView.DataSource = ds.Tables[0];

        }
        #endregion

        #region 单据属性列表双击
        private void Basic_Double_Click(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            BasicAllCtr bac = new BasicAllCtr();
            DataGridViewRow dataGridView = Basic_dataGridView.Rows[index];
            bac.setbasicAllCtr(dataGridView.Cells["Basic_ibillid"].Value.ToString(), dataGridView.Cells["Basic_vType"].Value.ToString(),
                dataGridView.Cells["Basic_vKeyid"].Value.ToString(), dataGridView.Cells["Basic_vKeyName"].Value.ToString()
                , dataGridView.Cells["Basic_vKeyName"].Value.ToString(), dataGridView.Cells["Basic_vkeyValue"].Value.ToString()
                , dataGridView.Cells["Basic_vHzrxField1"].Value.ToString(), dataGridView.Cells["Basic_vHzrxField2"].Value.ToString(),
                dataGridView.Cells["Basic_IOrderId"].Value.ToString(), dataGridView.Cells["Basic_vRemarks"].Value.ToString(),
                dataGridView.Cells["tableName"].Value.ToString());
            bac.Show();
        }
        #endregion
        #endregion

        #region 4.赋值及权限

        #region 4.1 赋值及权限 table页 查询按钮点击事件
        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("未完成功能！参阅单据属性！","提示");
            //加载数据
            if (MainListBox.SelectedItem == null)
            {
                //未选中
                MessageBox.Show("未选中任何单据！");

            }
            else
            {
                queryTBUDT_RoleBillLay();
            }
        }

        #endregion
        #region 4.2 赋值及权限 table页 查询按钮调用方法
        void queryTBUDT_RoleBillLay()
        {
            string querySql = $"select IBillID,vID,vFieldMapName,vExPValue,vFieldValue,vFieldValueType,IOrderID,vOrgID,vIsuse from TBUDT_RoleBillLay where IBillID = (select IBillID from  TBUDT_ModeLayout where vid = '{MainListBox.SelectedItem}')";
            /*标准的数据库连接写法
             * DBUtil db = new DBUtil();
            SqlConnection sqlConnection = db.GetConnection();
            _ = db.Query(sqlConnection, querySql);*/
            DataSet ds = new DBUtil().Query(new DBUtil().GetConnection(), querySql);
            RoleBIllLay_fzqxdataGrid.DataSource = ds.Tables[0];
        }

        #endregion
        #region 赋值及权限table页 表格双击事件
        private void shuxing_doubleClick_2(object sender, DataGridViewCellEventArgs e)
        {
            //双击
            int rowIndex = e.RowIndex;
            BillChkStand stand = new BillChkStand();
            stand.IBillId = shuxingDataGrid.Rows[rowIndex].Cells["IBILLID"].Value.ToString();
            stand.VOrgID = shuxingDataGrid.Rows[rowIndex].Cells["vOrgID"].Value.ToString();
            stand.VID = shuxingDataGrid.Rows[rowIndex].Cells["cvID"].Value.ToString();
            stand.VCheckType = shuxingDataGrid.Rows[rowIndex].Cells["vCheckType"].Value.ToString();
            stand.VHint = shuxingDataGrid.Rows[rowIndex].Cells["vHint"].Value.ToString();
            stand.VExPValue = shuxingDataGrid.Rows[rowIndex].Cells["vExPValue"].Value.ToString();
            stand.VDicID = shuxingDataGrid.Rows[rowIndex].Cells["vDicID"].Value.ToString();
            stand.IOrderID = shuxingDataGrid.Rows[rowIndex].Cells["cIOrderID"].Value.ToString();
            stand.Vtestvalue = shuxingDataGrid.Rows[rowIndex].Cells["vtestvalue"].Value.ToString();
            stand.VRemarks = shuxingDataGrid.Rows[rowIndex].Cells["vRemarks"].Value.ToString();
            stand.Show();

        }
        #endregion

        #endregion


    }
}
