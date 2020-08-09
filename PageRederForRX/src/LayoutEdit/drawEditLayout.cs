using PageRederForRX.src.Function;
using System;
using System.Data;
using System.Windows.Forms;

namespace PageRederForRX.src.LayoutEdit
{
    class drawEditLayout
    {
        //绘制工具集合 
        public TextBox drawTextBox(DataRow gridRow)
        {
            int top_int = int.Parse(gridRow[4].ToString());
            int left_int = int.Parse(gridRow[5].ToString());
            int grid_width = int.Parse(gridRow[6].ToString());
            int grid_height = int.Parse(gridRow[7].ToString());
            TextBox tb = new TextBox();
            tb.Name = gridRow[1].ToString();
            tb.Width = grid_width - 80;
            tb.Height = grid_height;
            tb.Top = top_int;
            tb.Left = left_int + 80;
            if (gridRow[12].ToString() == null || gridRow[10].ToString() == "" || gridRow[12].ToString() == "1")
            {
                tb.Visible = true;
            }
            else if (gridRow[12].ToString() == "3")
            {
                tb.Enabled = false;
                tb.Visible = true;
            }
            else
            {
                tb.Visible = false;
            }
            return tb;

        }
        public myLable drawlable(DataRow gridRow)
        {

            //获取基础类型
            int top_int = int.Parse(gridRow[4].ToString());
            int left_int = int.Parse(gridRow[5].ToString());
            int grid_width = int.Parse(gridRow[6].ToString());
            int grid_height = int.Parse(gridRow[7].ToString());
            string vColor = gridRow[8].ToString();
            string vGroundColor = gridRow[9].ToString();
            //int vfrmtype = int.Parse(gridRow[9].ToString());
            bool vIsShow;
            string isSHow = gridRow[12].ToString().ToString();
            

            //绘制Label
            myLable label = new myLable();
            label.Text = gridRow[2].ToString();
            label.inputName = gridRow[1].ToString();
            /*label.ibillid = MainListBox.SelectedItem.ToString();*/
            label.fieldType = int.Parse(gridRow["vfrmtype"].ToString());
            label.Width = 80;
            label.Height = 25;
            label.AutoSize = false;
            label.Top = top_int;
            label.Left = left_int;
            if (gridRow[12].ToString() == null || gridRow[10].ToString() == "" || gridRow[12].ToString() == "1")
            {
                label.Visible = true;
            }
            else if (gridRow[12].ToString() == "3") {
                label.Enabled = false;
                label.Visible = true;
            }
            else
            {
                label.Visible = false;
            }
             
            /*label.DoubleClick += new System.EventHandler(textboxDoubleClike);*/
            //label.ForeColor = stringToColor(vColor);
            //label.BackColor = stringToColor(vGroundColor);
            return label;

        }
        public ComboBox drawComBox(DataRow gridRow)
        {
            //数据解析
            int top_int = int.Parse(gridRow[4].ToString());
            int left_int = int.Parse(gridRow[5].ToString());
            int grid_width = int.Parse(gridRow[6].ToString());
            int grid_height = int.Parse(gridRow[7].ToString());         
            //
            ComboBox combo = new ComboBox();
            combo.Name = gridRow[1].ToString();
            combo.Width = grid_width - 80;
            combo.Height = grid_height;
            combo.Top = top_int;
            combo.Left = left_int + 80;
            if (gridRow[12].ToString() == null || gridRow[10].ToString() == "" || gridRow[12].ToString() == "1")
            {
                combo.Visible = true;
            }
            else if (gridRow[12].ToString() == "3")
            {
                combo.Enabled = false;
                combo.Visible = true;
            }
            else
            {
                combo.Visible = false;
            }
            return combo;


        }
        public DateTimePicker drawDateTimePicker(DataRow gridRow)
        {
            //数据解析
            int top_int = int.Parse(gridRow[4].ToString());
            int left_int = int.Parse(gridRow[5].ToString());
            int grid_width = int.Parse(gridRow[6].ToString());
            int grid_height = int.Parse(gridRow[7].ToString());
            //int vfrmtype = int.Parse(gridRow[9].ToString());
            DateTimePicker dateTimePicker  = new DateTimePicker();
            dateTimePicker.Name = gridRow[1].ToString();
            dateTimePicker.Width = grid_width - 80;
            dateTimePicker.Height = grid_height;
            dateTimePicker.Top = top_int;
            dateTimePicker.Left = left_int + 80;
            //tb.ForeColor = stringToColor(vColor);
            //tb.BackColor = stringToColor(vGroundColor);

            if (gridRow[12].ToString() == null || gridRow[10].ToString() == "" || gridRow[12].ToString() == "1")
            {
                dateTimePicker.Visible = true;
            }
            else if (gridRow[12].ToString() == "3")
            {
                dateTimePicker.Enabled = false;
                dateTimePicker.Visible = true;
            }
            else
            {
                dateTimePicker.Visible = false;
            }
            return dateTimePicker;
        }
    }
}
