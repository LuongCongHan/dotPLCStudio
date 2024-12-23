namespace dotPLC.CustomControl
{
    partial class frmDeviceMemory
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
            this.lswAddress = new System.Windows.Forms.ListView();
            this.panelNote = new System.Windows.Forms.Panel();
            this.lblNote = new System.Windows.Forms.Label();
            this.panelNote.SuspendLayout();
            this.SuspendLayout();
            // 
            // lswAddress
            // 
            this.lswAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lswAddress.HideSelection = false;
            this.lswAddress.Location = new System.Drawing.Point(0, 23);
            this.lswAddress.Name = "lswAddress";
            this.lswAddress.Size = new System.Drawing.Size(431, 527);
            this.lswAddress.TabIndex = 3;
            this.lswAddress.UseCompatibleStateImageBehavior = false;
            // 
            // panelNote
            // 
            this.panelNote.Controls.Add(this.lblNote);
            this.panelNote.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNote.Location = new System.Drawing.Point(0, 0);
            this.panelNote.Name = "panelNote";
            this.panelNote.Size = new System.Drawing.Size(431, 23);
            this.panelNote.TabIndex = 4;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.Location = new System.Drawing.Point(105, 7);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(185, 13);
            this.lblNote.TabIndex = 0;
            this.lblNote.Text = "D: Decimal, H: Hexadecimal, O: Octal";
            // 
            // frmDeviceMemory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 550);
            this.Controls.Add(this.lswAddress);
            this.Controls.Add(this.panelNote);
            this.Location = new System.Drawing.Point(459, 75);
            this.MinimizeBox = false;
            this.Name = "frmDeviceMemory";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Device/Buffer Memory";
            this.panelNote.ResumeLayout(false);
            this.panelNote.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lswAddress;
        private System.Windows.Forms.Panel panelNote;
        private System.Windows.Forms.Label lblNote;
    }
}