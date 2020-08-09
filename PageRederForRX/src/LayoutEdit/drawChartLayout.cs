using PageRederForRX.src.Function;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PageRederForRX.src.LayoutEdit
{
    class drawChartLayout
    {
        // 绘制从表
        public myLable drawlable(DataRow gridRow)
        {  //绘制Label
            myLable label = new myLable();
            label.Text = gridRow["ChartLayout_vFieldName"].ToString();
            label.inputName = gridRow["ChartLayout_vFieldCode"].ToString();
            /*label.ibillid = MainListBox.SelectedItem.ToString();*/
            label.fieldType = int.Parse(gridRow["ChartLayout_vfrmtype"].ToString());
            label.Width = 80;
            label.Height = 25;
            label.AutoSize = false;
            //label.Top = top_int;
            //label.Left = left_int;
            if (gridRow["ChartLayout_vIsShow"].ToString() == null || gridRow["ChartLayout_vIsShow"].ToString() == "" || gridRow["ChartLayout_vIsShow"].ToString() == "1")
            {
                label.Visible = true;
            }
            else if (gridRow["ChartLayout_vIsShow"].ToString() == "3")
            {
                label.Enabled = false;
                label.Visible = true;
            }
            else
            {
                label.Visible = false;
            }
            return label;

        }



        public TextBox drawTextBox(DataRow gridRow)
        {
            /*  int top_int = int.Parse(gridRow[4].ToString());
              int left_int = int.Parse(gridRow[5].ToString());*/
            int grid_width = int.Parse(gridRow["ChartLayout_vWidth"].ToString());
            int grid_height = int.Parse(gridRow["ChartLayout_vHeight"].ToString());
            TextBox tb = new TextBox();
            tb.Name = gridRow["ChartLayout_vFieldCode"].ToString();
            tb.Width = grid_width - 80;
            tb.Height = grid_height;
            if (gridRow["ChartLayout_vIsShow"].ToString() == null || gridRow["ChartLayout_vIsShow"].ToString() == "" || gridRow["ChartLayout_vIsShow"].ToString() == "1")
            {
                tb.Visible = true;
            }
            else if (gridRow["ChartLayout_vIsShow"].ToString() == "3")
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



        public ComboBox drawComBox(DataRow gridRow)
        {
            //数据解析
            /*int top_int = int.Parse(gridRow[4].ToString());
            int left_int = int.Parse(gridRow[5].ToString());*/
            int grid_width = int.Parse(gridRow["ChartLayout_vWidth"].ToString());
            int grid_height = int.Parse(gridRow["ChartLayout_vHeight"].ToString());
            //
            ComboBox combo = new ComboBox();
            combo.Name = gridRow[1].ToString();
            combo.Width = grid_width - 80;
            combo.Height = grid_height;
/*            combo.Top = top_int;
            combo.Left = left_int + 80;*/
            if (gridRow["ChartLayout_vIsShow"].ToString() == null || gridRow["ChartLayout_vIsShow"].ToString() == "" || gridRow["ChartLayout_vIsShow"].ToString() == "1")
            {
                combo.Visible = true;
            }
            else if (gridRow["ChartLayout_vIsShow"].ToString() == "3")
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
/*            int top_int = int.Parse(gridRow[4].ToString());
            int left_int = int.Parse(gridRow[5].ToString());*/
            int grid_width = int.Parse(gridRow["ChartLayout_vWidth"].ToString());
            int grid_height = int.Parse(gridRow["ChartLayout_vHeight"].ToString());
            //int vfrmtype = int.Parse(gridRow[9].ToString());
            DateTimePicker dateTimePicker = new DateTimePicker();
            dateTimePicker.Name = gridRow["ChartLayout_vFieldCode"].ToString();
            dateTimePicker.Width = grid_width - 80;
            dateTimePicker.Height = grid_height;
/*            dateTimePicker.Top = top_int;
            dateTimePicker.Left = left_int + 80;*/
            //tb.ForeColor = stringToColor(vColor);
            //tb.BackColor = stringToColor(vGroundColor);

            if (gridRow["ChartLayout_vIsShow"].ToString() == null || gridRow["ChartLayout_vIsShow"].ToString() == "" || gridRow["ChartLayout_vIsShow"].ToString() == "1")
            {
                dateTimePicker.Visible = true;
            }
            else if (gridRow["ChartLayout_vIsShow"].ToString() == "3")
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
