﻿namespace PageRederForRX.formSrc
{
    partial class BillChkStand
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.vRemarks = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.vtestvalue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.iorderid = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.vCheckType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.vExPValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.vHint = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.vId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.菜单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.vRemarks);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.vtestvalue);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.iorderid);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.vCheckType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.vExPValue);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.vHint);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.vId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 286);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "单据数据检测";
            // 
            // vRemarks
            // 
            this.vRemarks.Location = new System.Drawing.Point(81, 116);
            this.vRemarks.Name = "vRemarks";
            this.vRemarks.Size = new System.Drawing.Size(366, 21);
            this.vRemarks.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "备注信息";
            // 
            // vtestvalue
            // 
            this.vtestvalue.Location = new System.Drawing.Point(81, 89);
            this.vtestvalue.Name = "vtestvalue";
            this.vtestvalue.Size = new System.Drawing.Size(175, 21);
            this.vtestvalue.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(262, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "排序优先";
            // 
            // iorderid
            // 
            this.iorderid.Location = new System.Drawing.Point(321, 89);
            this.iorderid.Name = "iorderid";
            this.iorderid.Size = new System.Drawing.Size(126, 21);
            this.iorderid.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "参数测试";
            // 
            // vCheckType
            // 
            this.vCheckType.FormattingEnabled = true;
            this.vCheckType.Location = new System.Drawing.Point(321, 29);
            this.vCheckType.Name = "vCheckType";
            this.vCheckType.Size = new System.Drawing.Size(126, 20);
            this.vCheckType.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(262, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "检测类型";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(372, 243);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "保存变更";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // vExPValue
            // 
            this.vExPValue.Location = new System.Drawing.Point(81, 146);
            this.vExPValue.Multiline = true;
            this.vExPValue.Name = "vExPValue";
            this.vExPValue.Size = new System.Drawing.Size(366, 91);
            this.vExPValue.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "表达式";
            // 
            // vHint
            // 
            this.vHint.Location = new System.Drawing.Point(81, 62);
            this.vHint.Name = "vHint";
            this.vHint.Size = new System.Drawing.Size(366, 21);
            this.vHint.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "检测提示";
            // 
            // vId
            // 
            this.vId.Location = new System.Drawing.Point(81, 29);
            this.vId.Name = "vId";
            this.vId.Size = new System.Drawing.Size(175, 21);
            this.vId.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "识别标识";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.菜单ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(478, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 菜单ToolStripMenuItem
            // 
            this.菜单ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新增ToolStripMenuItem});
            this.菜单ToolStripMenuItem.Name = "菜单ToolStripMenuItem";
            this.菜单ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.菜单ToolStripMenuItem.Text = "菜单";
            // 
            // 新增ToolStripMenuItem
            // 
            this.新增ToolStripMenuItem.Name = "新增ToolStripMenuItem";
            this.新增ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.新增ToolStripMenuItem.Text = "新增";
            this.新增ToolStripMenuItem.Click += new System.EventHandler(this.新增ToolStripMenuItem_Click);
            // 
            // BillChkStand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 311);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BillChkStand";
            this.Text = "单据数据监测配置窗口";
            this.Load += new System.EventHandler(this.onShow);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox vtestvalue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox iorderid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox vCheckType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox vExPValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox vHint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox vId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 菜单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新增ToolStripMenuItem;
        private System.Windows.Forms.TextBox vRemarks;
        private System.Windows.Forms.Label label7;
    }
}