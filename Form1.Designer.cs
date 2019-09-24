namespace FingerPrintDemo
{
    partial class Form1
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
            this.btn_Generate_Image = new System.Windows.Forms.Button();
            this.imageHolder = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContaminationBar = new System.Windows.Forms.ToolStripProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFingerPrintFormat = new System.Windows.Forms.TextBox();
            this.txtImageData = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageHolder)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Generate_Image
            // 
            this.btn_Generate_Image.Location = new System.Drawing.Point(471, 12);
            this.btn_Generate_Image.Name = "btn_Generate_Image";
            this.btn_Generate_Image.Size = new System.Drawing.Size(221, 54);
            this.btn_Generate_Image.TabIndex = 0;
            this.btn_Generate_Image.Text = "Generate Image";
            this.btn_Generate_Image.UseVisualStyleBackColor = true;
            this.btn_Generate_Image.Click += new System.EventHandler(this.Btn_Generate_Image_Click);
            // 
            // imageHolder
            // 
            this.imageHolder.Location = new System.Drawing.Point(15, 12);
            this.imageHolder.Name = "imageHolder";
            this.imageHolder.Size = new System.Drawing.Size(367, 381);
            this.imageHolder.TabIndex = 1;
            this.imageHolder.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripContaminationBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 421);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(794, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(114, 17);
            this.toolStripStatusLabel1.Text = "ContaminationLevel";
            // 
            // toolStripContaminationBar
            // 
            this.toolStripContaminationBar.Name = "toolStripContaminationBar";
            this.toolStripContaminationBar.Size = new System.Drawing.Size(100, 16);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(489, 430);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // txtFingerPrintFormat
            // 
            this.txtFingerPrintFormat.Location = new System.Drawing.Point(466, 95);
            this.txtFingerPrintFormat.Name = "txtFingerPrintFormat";
            this.txtFingerPrintFormat.Size = new System.Drawing.Size(226, 20);
            this.txtFingerPrintFormat.TabIndex = 4;
            // 
            // txtImageData
            // 
            this.txtImageData.Location = new System.Drawing.Point(466, 131);
            this.txtImageData.Multiline = true;
            this.txtImageData.Name = "txtImageData";
            this.txtImageData.Size = new System.Drawing.Size(226, 262);
            this.txtImageData.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 443);
            this.Controls.Add(this.txtImageData);
            this.Controls.Add(this.txtFingerPrintFormat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.imageHolder);
            this.Controls.Add(this.btn_Generate_Image);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageHolder)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Generate_Image;
        private System.Windows.Forms.PictureBox imageHolder;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripContaminationBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFingerPrintFormat;
        private System.Windows.Forms.TextBox txtImageData;
    }
}

