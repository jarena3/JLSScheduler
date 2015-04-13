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
            this.NTPreviewBTN = new System.Windows.Forms.Button();
            this.KTPreviewBTN = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.SaveFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.DOCBTN = new System.Windows.Forms.Button();
            this.HTMLBTN = new System.Windows.Forms.Button();
            this.CloseBTN = new System.Windows.Forms.Button();
            this.PDFBTN = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // NTPreviewBTN
            // 
            this.NTPreviewBTN.Location = new System.Drawing.Point(6, 19);
            this.NTPreviewBTN.Name = "NTPreviewBTN";
            this.NTPreviewBTN.Size = new System.Drawing.Size(143, 23);
            this.NTPreviewBTN.TabIndex = 1;
            this.NTPreviewBTN.Text = "NT Page";
            this.NTPreviewBTN.UseVisualStyleBackColor = true;
            // 
            // KTPreviewBTN
            // 
            this.KTPreviewBTN.Location = new System.Drawing.Point(6, 48);
            this.KTPreviewBTN.Name = "KTPreviewBTN";
            this.KTPreviewBTN.Size = new System.Drawing.Size(143, 23);
            this.KTPreviewBTN.TabIndex = 2;
            this.KTPreviewBTN.Text = "KT Page";
            this.KTPreviewBTN.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 77);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Parent Info Page";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 106);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Student Syllabus";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 135);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(143, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Student Homework Sheet";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NTPreviewBTN);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.KTPreviewBTN);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(157, 166);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preview";
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.Location = new System.Drawing.Point(212, 22);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(284, 41);
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
            this.groupBox2.Controls.Add(this.PDFBTN);
            this.groupBox2.Controls.Add(this.HTMLBTN);
            this.groupBox2.Controls.Add(this.DOCBTN);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Location = new System.Drawing.Point(212, 79);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 153);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Experimental";
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
            this.textBox1.Text = "Warning: these functions will fail if MS Word 15 is not installed on this compute" +
    "r.";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DOCBTN
            // 
            this.DOCBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DOCBTN.Location = new System.Drawing.Point(6, 57);
            this.DOCBTN.Name = "DOCBTN";
            this.DOCBTN.Size = new System.Drawing.Size(113, 38);
            this.DOCBTN.TabIndex = 11;
            this.DOCBTN.Text = "Export as .DOC (for older systems)";
            this.DOCBTN.UseVisualStyleBackColor = true;
            this.DOCBTN.Click += new System.EventHandler(this.DOCBTN_Click);
            // 
            // HTMLBTN
            // 
            this.HTMLBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HTMLBTN.Location = new System.Drawing.Point(165, 57);
            this.HTMLBTN.Name = "HTMLBTN";
            this.HTMLBTN.Size = new System.Drawing.Size(113, 38);
            this.HTMLBTN.TabIndex = 12;
            this.HTMLBTN.Text = "Export as HTML";
            this.HTMLBTN.UseVisualStyleBackColor = true;
            // 
            // CloseBTN
            // 
            this.CloseBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CloseBTN.Location = new System.Drawing.Point(18, 194);
            this.CloseBTN.Name = "CloseBTN";
            this.CloseBTN.Size = new System.Drawing.Size(143, 38);
            this.CloseBTN.TabIndex = 13;
            this.CloseBTN.Text = "Close";
            this.CloseBTN.UseVisualStyleBackColor = true;
            this.CloseBTN.Click += new System.EventHandler(this.CloseBTN_Click);
            // 
            // PDFBTN
            // 
            this.PDFBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PDFBTN.Location = new System.Drawing.Point(84, 101);
            this.PDFBTN.Name = "PDFBTN";
            this.PDFBTN.Size = new System.Drawing.Size(113, 38);
            this.PDFBTN.TabIndex = 13;
            this.PDFBTN.Text = "Export as .PDF";
            this.PDFBTN.UseVisualStyleBackColor = true;
            // 
            // ExportControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 244);
            this.Controls.Add(this.CloseBTN);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "ExportControls";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Export FIles";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button NTPreviewBTN;
        private System.Windows.Forms.Button KTPreviewBTN;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.FolderBrowserDialog SaveFolderDialog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button HTMLBTN;
        private System.Windows.Forms.Button DOCBTN;
        private System.Windows.Forms.Button CloseBTN;
        private System.Windows.Forms.Button PDFBTN;
    }
}