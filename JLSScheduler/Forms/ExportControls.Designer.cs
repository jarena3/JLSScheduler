namespace JLSScheduler.Forms
{
    partial class ExportControls
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
            this.button5 = new System.Windows.Forms.Button();
            this.SaveFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ODTBTN = new System.Windows.Forms.Button();
            this.XPSBTN = new System.Windows.Forms.Button();
            this.PDFBTN = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.HTMLBTN = new System.Windows.Forms.Button();
            this.DOCBTN = new System.Windows.Forms.Button();
            this.CloseBTN = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.Location = new System.Drawing.Point(8, 22);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(194, 68);
            this.button5.TabIndex = 8;
            this.button5.Text = "Export as .DOCX";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // SaveFolderDialog
            // 
            this.SaveFolderDialog.Description = "Select folder where documents will be saved";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ODTBTN);
            this.groupBox2.Controls.Add(this.XPSBTN);
            this.groupBox2.Controls.Add(this.PDFBTN);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.HTMLBTN);
            this.groupBox2.Location = new System.Drawing.Point(231, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 114);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Experimental";
            // 
            // ODTBTN
            // 
            this.ODTBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ODTBTN.Location = new System.Drawing.Point(158, 85);
            this.ODTBTN.Name = "ODTBTN";
            this.ODTBTN.Size = new System.Drawing.Size(120, 23);
            this.ODTBTN.TabIndex = 15;
            this.ODTBTN.Text = "Export as .ODT";
            this.ODTBTN.UseVisualStyleBackColor = true;
            this.ODTBTN.Click += new System.EventHandler(this.ODTBTN_Click);
            // 
            // XPSBTN
            // 
            this.XPSBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.XPSBTN.Location = new System.Drawing.Point(158, 57);
            this.XPSBTN.Name = "XPSBTN";
            this.XPSBTN.Size = new System.Drawing.Size(120, 23);
            this.XPSBTN.TabIndex = 14;
            this.XPSBTN.Text = "Export as .XPS";
            this.XPSBTN.UseVisualStyleBackColor = true;
            this.XPSBTN.Click += new System.EventHandler(this.XPSBTN_Click);
            // 
            // PDFBTN
            // 
            this.PDFBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PDFBTN.Location = new System.Drawing.Point(7, 57);
            this.PDFBTN.Name = "PDFBTN";
            this.PDFBTN.Size = new System.Drawing.Size(120, 23);
            this.PDFBTN.TabIndex = 13;
            this.PDFBTN.Text = "Export as .PDF";
            this.PDFBTN.UseVisualStyleBackColor = true;
            this.PDFBTN.Click += new System.EventHandler(this.PDFBTN_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(7, 19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(271, 32);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Warning: MS Word must be installed on this system. These functions will fail on s" +
    "ome older versions of Word.";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // HTMLBTN
            // 
            this.HTMLBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HTMLBTN.Location = new System.Drawing.Point(7, 84);
            this.HTMLBTN.Name = "HTMLBTN";
            this.HTMLBTN.Size = new System.Drawing.Size(120, 23);
            this.HTMLBTN.TabIndex = 12;
            this.HTMLBTN.Text = "Export as .HTML";
            this.HTMLBTN.UseVisualStyleBackColor = true;
            this.HTMLBTN.Click += new System.EventHandler(this.HTMLBTN_Click);
            // 
            // DOCBTN
            // 
            this.DOCBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DOCBTN.Location = new System.Drawing.Point(8, 96);
            this.DOCBTN.Name = "DOCBTN";
            this.DOCBTN.Size = new System.Drawing.Size(194, 68);
            this.DOCBTN.TabIndex = 11;
            this.DOCBTN.Text = "Export as .DOC";
            this.DOCBTN.UseVisualStyleBackColor = true;
            this.DOCBTN.Click += new System.EventHandler(this.DOCBTN_Click);
            // 
            // CloseBTN
            // 
            this.CloseBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CloseBTN.Location = new System.Drawing.Point(370, 137);
            this.CloseBTN.Name = "CloseBTN";
            this.CloseBTN.Size = new System.Drawing.Size(143, 38);
            this.CloseBTN.TabIndex = 13;
            this.CloseBTN.Text = "Close";
            this.CloseBTN.UseVisualStyleBackColor = true;
            this.CloseBTN.Click += new System.EventHandler(this.CloseBTN_Click);
            // 
            // ExportControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 187);
            this.Controls.Add(this.CloseBTN);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.DOCBTN);
            this.Controls.Add(this.button5);
            this.MaximizeBox = false;
            this.Name = "ExportControls";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Export FIles";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.FolderBrowserDialog SaveFolderDialog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button HTMLBTN;
        private System.Windows.Forms.Button DOCBTN;
        private System.Windows.Forms.Button CloseBTN;
        private System.Windows.Forms.Button PDFBTN;
        private System.Windows.Forms.Button XPSBTN;
        private System.Windows.Forms.Button ODTBTN;
    }
}