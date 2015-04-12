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
            this.CancelBTN = new System.Windows.Forms.Button();
            this.NTPreviewBTN = new System.Windows.Forms.Button();
            this.KTPreviewBTN = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ExportHTMLBTN = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SaveFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelBTN
            // 
            this.CancelBTN.Location = new System.Drawing.Point(239, 143);
            this.CancelBTN.Name = "CancelBTN";
            this.CancelBTN.Size = new System.Drawing.Size(121, 35);
            this.CancelBTN.TabIndex = 0;
            this.CancelBTN.Text = "Cancel";
            this.CancelBTN.UseVisualStyleBackColor = true;
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
            // ExportHTMLBTN
            // 
            this.ExportHTMLBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ExportHTMLBTN.Location = new System.Drawing.Point(185, 22);
            this.ExportHTMLBTN.Name = "ExportHTMLBTN";
            this.ExportHTMLBTN.Size = new System.Drawing.Size(175, 41);
            this.ExportHTMLBTN.TabIndex = 7;
            this.ExportHTMLBTN.Text = "Export to .HTML";
            this.ExportHTMLBTN.UseVisualStyleBackColor = true;
            this.ExportHTMLBTN.Click += new System.EventHandler(this.ExportHTMLBTN_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.Location = new System.Drawing.Point(185, 71);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(175, 41);
            this.button5.TabIndex = 8;
            this.button5.Text = "Export to .DOCX";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // SaveFolderDialog
            // 
            this.SaveFolderDialog.Description = "Select folder where documents will be saved";
            // 
            // ExportControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 194);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.ExportHTMLBTN);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CancelBTN);
            this.Name = "ExportControls";
            this.Text = "ExportControls";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CancelBTN;
        private System.Windows.Forms.Button NTPreviewBTN;
        private System.Windows.Forms.Button KTPreviewBTN;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ExportHTMLBTN;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.FolderBrowserDialog SaveFolderDialog;
    }
}