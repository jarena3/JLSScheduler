namespace JLSScheduler.Forms
{
    partial class CustomHolidayForm
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
            this.Calendar = new System.Windows.Forms.MonthCalendar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.CustomHolidaysTabPage = new System.Windows.Forms.TabPage();
            this.CustomHolidaysListBox = new System.Windows.Forms.ListBox();
            this.AllHolidaysTabPage = new System.Windows.Forms.TabPage();
            this.AllHolidaysListBox = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.CustomHolidaysTabPage.SuspendLayout();
            this.AllHolidaysTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // Calendar
            // 
            this.Calendar.BackColor = System.Drawing.SystemColors.Window;
            this.Calendar.CalendarDimensions = new System.Drawing.Size(3, 3);
            this.Calendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Calendar.Location = new System.Drawing.Point(247, 16);
            this.Calendar.MaxSelectionCount = 1;
            this.Calendar.Name = "Calendar";
            this.Calendar.ShowToday = false;
            this.Calendar.ShowTodayCircle = false;
            this.Calendar.TabIndex = 0;
            this.Calendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.Calendar_DateSelected);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 81);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(6, 19);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(207, 56);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "Add additonal holidays and break days to the semester by selecting them (click) i" +
    "n the calendar.  Remove added holidays by clicking again.";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 426);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(223, 46);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.CustomHolidaysTabPage);
            this.TabControl.Controls.Add(this.AllHolidaysTabPage);
            this.TabControl.Location = new System.Drawing.Point(12, 103);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(223, 317);
            this.TabControl.TabIndex = 3;
            // 
            // CustomHolidaysTabPage
            // 
            this.CustomHolidaysTabPage.Controls.Add(this.CustomHolidaysListBox);
            this.CustomHolidaysTabPage.Location = new System.Drawing.Point(4, 22);
            this.CustomHolidaysTabPage.Name = "CustomHolidaysTabPage";
            this.CustomHolidaysTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.CustomHolidaysTabPage.Size = new System.Drawing.Size(215, 291);
            this.CustomHolidaysTabPage.TabIndex = 0;
            this.CustomHolidaysTabPage.Text = "Custom Holidays";
            this.CustomHolidaysTabPage.UseVisualStyleBackColor = true;
            // 
            // CustomHolidaysListBox
            // 
            this.CustomHolidaysListBox.FormattingEnabled = true;
            this.CustomHolidaysListBox.Location = new System.Drawing.Point(4, 4);
            this.CustomHolidaysListBox.Name = "CustomHolidaysListBox";
            this.CustomHolidaysListBox.Size = new System.Drawing.Size(205, 277);
            this.CustomHolidaysListBox.TabIndex = 0;
            // 
            // AllHolidaysTabPage
            // 
            this.AllHolidaysTabPage.Controls.Add(this.AllHolidaysListBox);
            this.AllHolidaysTabPage.Location = new System.Drawing.Point(4, 22);
            this.AllHolidaysTabPage.Name = "AllHolidaysTabPage";
            this.AllHolidaysTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.AllHolidaysTabPage.Size = new System.Drawing.Size(215, 291);
            this.AllHolidaysTabPage.TabIndex = 1;
            this.AllHolidaysTabPage.Text = "All Holidays";
            this.AllHolidaysTabPage.UseVisualStyleBackColor = true;
            // 
            // AllHolidaysListBox
            // 
            this.AllHolidaysListBox.FormattingEnabled = true;
            this.AllHolidaysListBox.Location = new System.Drawing.Point(6, 6);
            this.AllHolidaysListBox.Name = "AllHolidaysListBox";
            this.AllHolidaysListBox.Size = new System.Drawing.Size(203, 277);
            this.AllHolidaysListBox.TabIndex = 0;
            // 
            // CustomHolidayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 490);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Calendar);
            this.MaximizeBox = false;
            this.Name = "CustomHolidayForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Edit Custom Holidays";
            this.groupBox1.ResumeLayout(false);
            this.TabControl.ResumeLayout(false);
            this.CustomHolidaysTabPage.ResumeLayout(false);
            this.AllHolidaysTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar Calendar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage CustomHolidaysTabPage;
        private System.Windows.Forms.TabPage AllHolidaysTabPage;
        private System.Windows.Forms.ListBox CustomHolidaysListBox;
        private System.Windows.Forms.ListBox AllHolidaysListBox;

    }
}