namespace dotPLC.CustomControl
{
    partial class frmDownloadFile
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDownloadFile));
            this.timerCheckInternet = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.textProgressBar1 = new dotPLC.CustomControl.TextProgressBar();
            this.SuspendLayout();
            // 
            // timerCheckInternet
            // 
            this.timerCheckInternet.Interval = 1000;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Downloading...";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(237, 62);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // textProgressBar1
            // 
            this.textProgressBar1.CustomText = "";
            this.textProgressBar1.GradientBotColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(174)))), ((int)(((byte)(233)))));
            this.textProgressBar1.GradientMode = true;
            this.textProgressBar1.GradientTopColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(195)))), ((int)(((byte)(253)))));
            this.textProgressBar1.Location = new System.Drawing.Point(6, 37);
            this.textProgressBar1.Name = "textProgressBar1";
            this.textProgressBar1.ProgressColor = System.Drawing.Color.LightGreen;
            this.textProgressBar1.Size = new System.Drawing.Size(301, 18);
            this.textProgressBar1.TabIndex = 1;
            this.textProgressBar1.TextColor = System.Drawing.Color.Black;
            this.textProgressBar1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.textProgressBar1.VisualMode = dotPLC.CustomControl.ProgressBarDisplayMode.Percentage;
            // 
            // frmDownloadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 92);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.textProgressBar1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmDownloadFile";
            this.Text = "dotPLC Update";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timerCheckInternet;
        private System.Windows.Forms.Label label1;
        public TextProgressBar textProgressBar1;
        private System.Windows.Forms.Button btnCancel;
    }
}