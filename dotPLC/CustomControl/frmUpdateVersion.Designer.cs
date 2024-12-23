namespace dotPLC.CustomControl
{
    partial class frmUpdateVersion
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbLinkUpdate = new System.Windows.Forms.LinkLabel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbSize = new System.Windows.Forms.Label();
            this.picLoad = new System.Windows.Forms.PictureBox();
            this.picSizeLoad = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSizeLoad)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::dotPLC.Properties.Resources.dotPLC48x48;
            this.pictureBox1.Location = new System.Drawing.Point(26, 23);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lbLinkUpdate
            // 
            this.lbLinkUpdate.AutoSize = true;
            this.lbLinkUpdate.Location = new System.Drawing.Point(95, 48);
            this.lbLinkUpdate.Name = "lbLinkUpdate";
            this.lbLinkUpdate.Size = new System.Drawing.Size(64, 15);
            this.lbLinkUpdate.TabIndex = 2;
            this.lbLinkUpdate.TabStop = true;
            this.lbLinkUpdate.Text = "linkLabel1";
            this.lbLinkUpdate.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(293, 150);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Font = new System.Drawing.Font("Microsoft New Tai Lue", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.Location = new System.Drawing.Point(95, 23);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(248, 17);
            this.lblCopyright.TabIndex = 3;
            this.lblCopyright.Text = "A new version of dotPLC Studio is available.";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(208, 150);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(70, 23);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Update version:";
            this.label1.Visible = false;
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.Location = new System.Drawing.Point(217, 95);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(44, 15);
            this.lbVersion.TabIndex = 5;
            this.lbVersion.Text = "2.0.0.0";
            this.lbVersion.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Download size:";
            this.label3.Visible = false;
            // 
            // lbSize
            // 
            this.lbSize.AutoSize = true;
            this.lbSize.Location = new System.Drawing.Point(217, 120);
            this.lbSize.Name = "lbSize";
            this.lbSize.Size = new System.Drawing.Size(51, 15);
            this.lbSize.TabIndex = 7;
            this.lbSize.Text = "1.01 MB";
            this.lbSize.Visible = false;
            // 
            // picLoad
            // 
            this.picLoad.Image = global::dotPLC.Properties.Resources.loading;
            this.picLoad.Location = new System.Drawing.Point(175, 28);
            this.picLoad.Name = "picLoad";
            this.picLoad.Size = new System.Drawing.Size(64, 64);
            this.picLoad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLoad.TabIndex = 8;
            this.picLoad.TabStop = false;
            this.picLoad.Visible = false;
            // 
            // picSizeLoad
            // 
            this.picSizeLoad.Image = global::dotPLC.Properties.Resources.loading;
            this.picSizeLoad.Location = new System.Drawing.Point(232, 118);
            this.picSizeLoad.Name = "picSizeLoad";
            this.picSizeLoad.Size = new System.Drawing.Size(16, 16);
            this.picSizeLoad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSizeLoad.TabIndex = 9;
            this.picSizeLoad.TabStop = false;
            // 
            // frmUpdateVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 179);
            this.Controls.Add(this.picSizeLoad);
            this.Controls.Add(this.lbSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lbLinkUpdate);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.picLoad);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmUpdateVersion";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Sortware Update";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSizeLoad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel lbLinkUpdate;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbSize;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.PictureBox picLoad;
        private System.Windows.Forms.PictureBox picSizeLoad;
    }
}