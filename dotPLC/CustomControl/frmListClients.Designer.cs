namespace dotPLC.CustomControl
{
    partial class frmListClients
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
            this.txtListClients = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtListClients
            // 
            this.txtListClients.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtListClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtListClients.Location = new System.Drawing.Point(0, 0);
            this.txtListClients.Multiline = true;
            this.txtListClients.Name = "txtListClients";
            this.txtListClients.ReadOnly = true;
            this.txtListClients.Size = new System.Drawing.Size(198, 230);
            this.txtListClients.TabIndex = 0;
            // 
            // frmListClients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(198, 230);
            this.Controls.Add(this.txtListClients);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmListClients";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "List of clients";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtListClients;
    }
}