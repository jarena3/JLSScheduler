namespace JLSScheduler.Forms
{
    partial class CustomHomeworkForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomHomeworkForm));
            this.TasksList = new System.Windows.Forms.ListBox();
            this.AddNewBTN = new System.Windows.Forms.Button();
            this.DeleteBTN = new System.Windows.Forms.Button();
            this.OkBTN = new System.Windows.Forms.Button();
            this.TitleTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DescriptionTB = new System.Windows.Forms.TextBox();
            this.RepeatsCB = new System.Windows.Forms.CheckBox();
            this.RepeatsCT = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.WeekNumberCT = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.AddEmptyBTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RepeatsCT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WeekNumberCT)).BeginInit();
            this.SuspendLayout();
            // 
            // TasksList
            // 
            this.TasksList.FormattingEnabled = true;
            this.TasksList.Location = new System.Drawing.Point(530, 29);
            this.TasksList.Name = "TasksList";
            this.TasksList.Size = new System.Drawing.Size(251, 225);
            this.TasksList.TabIndex = 0;
            this.TasksList.SelectedIndexChanged += new System.EventHandler(this.TasksList_SelectedIndexChanged);
            // 
            // AddNewBTN
            // 
            this.AddNewBTN.Location = new System.Drawing.Point(449, 25);
            this.AddNewBTN.Name = "AddNewBTN";
            this.AddNewBTN.Size = new System.Drawing.Size(75, 23);
            this.AddNewBTN.TabIndex = 1;
            this.AddNewBTN.Text = "Add New";
            this.AddNewBTN.UseVisualStyleBackColor = true;
            this.AddNewBTN.Click += new System.EventHandler(this.AddNewBTN_Click);
            // 
            // DeleteBTN
            // 
            this.DeleteBTN.Location = new System.Drawing.Point(449, 83);
            this.DeleteBTN.Name = "DeleteBTN";
            this.DeleteBTN.Size = new System.Drawing.Size(75, 23);
            this.DeleteBTN.TabIndex = 2;
            this.DeleteBTN.Text = "Delete";
            this.DeleteBTN.UseVisualStyleBackColor = true;
            this.DeleteBTN.Click += new System.EventHandler(this.DeleteBTN_Click);
            // 
            // OkBTN
            // 
            this.OkBTN.Location = new System.Drawing.Point(668, 260);
            this.OkBTN.Name = "OkBTN";
            this.OkBTN.Size = new System.Drawing.Size(112, 38);
            this.OkBTN.TabIndex = 3;
            this.OkBTN.Text = "OK";
            this.OkBTN.UseVisualStyleBackColor = true;
            this.OkBTN.Click += new System.EventHandler(this.OkBTN_Click);
            // 
            // TitleTB
            // 
            this.TitleTB.Location = new System.Drawing.Point(12, 29);
            this.TitleTB.Name = "TitleTB";
            this.TitleTB.Size = new System.Drawing.Size(394, 20);
            this.TitleTB.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Homework Title:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Homework Description:";
            // 
            // DescriptionTB
            // 
            this.DescriptionTB.Location = new System.Drawing.Point(12, 80);
            this.DescriptionTB.Multiline = true;
            this.DescriptionTB.Name = "DescriptionTB";
            this.DescriptionTB.Size = new System.Drawing.Size(394, 172);
            this.DescriptionTB.TabIndex = 6;
            // 
            // RepeatsCB
            // 
            this.RepeatsCB.AutoSize = true;
            this.RepeatsCB.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.RepeatsCB.Location = new System.Drawing.Point(12, 286);
            this.RepeatsCB.Name = "RepeatsCB";
            this.RepeatsCB.Size = new System.Drawing.Size(195, 17);
            this.RepeatsCB.TabIndex = 8;
            this.RepeatsCB.Text = "Repeats every                       weeks";
            this.RepeatsCB.UseVisualStyleBackColor = true;
            this.RepeatsCB.CheckedChanged += new System.EventHandler(this.RepeatsCB_CheckedChanged);
            // 
            // RepeatsCT
            // 
            this.RepeatsCT.Location = new System.Drawing.Point(104, 283);
            this.RepeatsCT.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.RepeatsCT.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RepeatsCT.Name = "RepeatsCT";
            this.RepeatsCT.Size = new System.Drawing.Size(56, 20);
            this.RepeatsCT.TabIndex = 9;
            this.RepeatsCT.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Due on week #:";
            // 
            // WeekNumberCT
            // 
            this.WeekNumberCT.Location = new System.Drawing.Point(104, 258);
            this.WeekNumberCT.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.WeekNumberCT.Name = "WeekNumberCT";
            this.WeekNumberCT.Size = new System.Drawing.Size(56, 20);
            this.WeekNumberCT.TabIndex = 13;
            this.WeekNumberCT.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(527, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Custom Homework Tasks:";
            // 
            // SaveBtn
            // 
            this.SaveBtn.Enabled = false;
            this.SaveBtn.Location = new System.Drawing.Point(274, 258);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(132, 38);
            this.SaveBtn.TabIndex = 15;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // AddEmptyBTN
            // 
            this.AddEmptyBTN.Location = new System.Drawing.Point(449, 54);
            this.AddEmptyBTN.Name = "AddEmptyBTN";
            this.AddEmptyBTN.Size = new System.Drawing.Size(75, 23);
            this.AddEmptyBTN.TabIndex = 16;
            this.AddEmptyBTN.Text = "Add Empty";
            this.AddEmptyBTN.UseVisualStyleBackColor = true;
            this.AddEmptyBTN.Click += new System.EventHandler(this.AddEmptyBTN_Click);
            // 
            // CustomHomeworkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 310);
            this.Controls.Add(this.AddEmptyBTN);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.WeekNumberCT);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.RepeatsCT);
            this.Controls.Add(this.RepeatsCB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DescriptionTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TitleTB);
            this.Controls.Add(this.OkBTN);
            this.Controls.Add(this.DeleteBTN);
            this.Controls.Add(this.AddNewBTN);
            this.Controls.Add(this.TasksList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CustomHomeworkForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Add Custom Homework Task";
            ((System.ComponentModel.ISupportInitialize)(this.RepeatsCT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WeekNumberCT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox TasksList;
        private System.Windows.Forms.Button AddNewBTN;
        private System.Windows.Forms.Button DeleteBTN;
        private System.Windows.Forms.Button OkBTN;
        private System.Windows.Forms.TextBox TitleTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DescriptionTB;
        private System.Windows.Forms.CheckBox RepeatsCB;
        private System.Windows.Forms.NumericUpDown RepeatsCT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown WeekNumberCT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button AddEmptyBTN;
    }
}