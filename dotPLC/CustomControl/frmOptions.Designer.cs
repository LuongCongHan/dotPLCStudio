namespace dotPLC.CustomControl
{
    partial class frmOptions
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.togglebtnSaveWatch = new dotPLC.CustomControl.ToggleButton();
            this.togglebtnSystemTray = new dotPLC.CustomControl.ToggleButton();
            this.togglebtnNotify = new dotPLC.CustomControl.ToggleButton();
            this.togglebtnTopMost = new dotPLC.CustomControl.ToggleButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.togglebtnAutoUpdate = new dotPLC.CustomControl.ToggleButton();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBoxOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(59, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Always on top";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(59, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Notice client status";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(59, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Minimize to tray";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(59, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Save data in Watch";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(29, 146);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Controls.Add(this.label5);
            this.groupBoxOptions.Controls.Add(this.togglebtnAutoUpdate);
            this.groupBoxOptions.Controls.Add(this.label4);
            this.groupBoxOptions.Controls.Add(this.label1);
            this.groupBoxOptions.Controls.Add(this.togglebtnSaveWatch);
            this.groupBoxOptions.Controls.Add(this.label2);
            this.groupBoxOptions.Controls.Add(this.togglebtnSystemTray);
            this.groupBoxOptions.Controls.Add(this.label3);
            this.groupBoxOptions.Controls.Add(this.togglebtnNotify);
            this.groupBoxOptions.Controls.Add(this.togglebtnTopMost);
            this.groupBoxOptions.Location = new System.Drawing.Point(12, 4);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(192, 136);
            this.groupBoxOptions.TabIndex = 5;
            this.groupBoxOptions.TabStop = false;
            // 
            // togglebtnSaveWatch
            // 
            this.togglebtnSaveWatch.AutoSize = true;
            this.togglebtnSaveWatch.Location = new System.Drawing.Point(6, 87);
            this.togglebtnSaveWatch.MinimumSize = new System.Drawing.Size(30, 14);
            this.togglebtnSaveWatch.Name = "togglebtnSaveWatch";
            this.togglebtnSaveWatch.OffBackColor = System.Drawing.Color.Gray;
            this.togglebtnSaveWatch.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.togglebtnSaveWatch.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(92)))), ((int)(((byte)(127)))));
            this.togglebtnSaveWatch.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.togglebtnSaveWatch.Size = new System.Drawing.Size(30, 14);
            this.togglebtnSaveWatch.TabIndex = 4;
            this.togglebtnSaveWatch.UseVisualStyleBackColor = true;
            // 
            // togglebtnSystemTray
            // 
            this.togglebtnSystemTray.AutoSize = true;
            this.togglebtnSystemTray.Location = new System.Drawing.Point(6, 62);
            this.togglebtnSystemTray.MinimumSize = new System.Drawing.Size(30, 14);
            this.togglebtnSystemTray.Name = "togglebtnSystemTray";
            this.togglebtnSystemTray.OffBackColor = System.Drawing.Color.Gray;
            this.togglebtnSystemTray.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.togglebtnSystemTray.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(92)))), ((int)(((byte)(127)))));
            this.togglebtnSystemTray.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.togglebtnSystemTray.Size = new System.Drawing.Size(30, 14);
            this.togglebtnSystemTray.TabIndex = 3;
            this.togglebtnSystemTray.UseVisualStyleBackColor = true;
            // 
            // togglebtnNotify
            // 
            this.togglebtnNotify.AutoSize = true;
            this.togglebtnNotify.Location = new System.Drawing.Point(6, 37);
            this.togglebtnNotify.MinimumSize = new System.Drawing.Size(30, 14);
            this.togglebtnNotify.Name = "togglebtnNotify";
            this.togglebtnNotify.OffBackColor = System.Drawing.Color.Gray;
            this.togglebtnNotify.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.togglebtnNotify.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(92)))), ((int)(((byte)(127)))));
            this.togglebtnNotify.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.togglebtnNotify.Size = new System.Drawing.Size(30, 14);
            this.togglebtnNotify.TabIndex = 2;
            this.togglebtnNotify.UseVisualStyleBackColor = true;
            // 
            // togglebtnTopMost
            // 
            this.togglebtnTopMost.AutoSize = true;
            this.togglebtnTopMost.Location = new System.Drawing.Point(6, 12);
            this.togglebtnTopMost.MinimumSize = new System.Drawing.Size(30, 14);
            this.togglebtnTopMost.Name = "togglebtnTopMost";
            this.togglebtnTopMost.OffBackColor = System.Drawing.Color.Gray;
            this.togglebtnTopMost.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.togglebtnTopMost.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(92)))), ((int)(((byte)(127)))));
            this.togglebtnTopMost.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.togglebtnTopMost.Size = new System.Drawing.Size(30, 14);
            this.togglebtnTopMost.TabIndex = 1;
            this.togglebtnTopMost.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(111, 146);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // togglebtnAutoUpdate
            // 
            this.togglebtnAutoUpdate.AutoSize = true;
            this.togglebtnAutoUpdate.Location = new System.Drawing.Point(6, 111);
            this.togglebtnAutoUpdate.MinimumSize = new System.Drawing.Size(30, 14);
            this.togglebtnAutoUpdate.Name = "togglebtnAutoUpdate";
            this.togglebtnAutoUpdate.OffBackColor = System.Drawing.Color.Gray;
            this.togglebtnAutoUpdate.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.togglebtnAutoUpdate.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(92)))), ((int)(((byte)(127)))));
            this.togglebtnAutoUpdate.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.togglebtnAutoUpdate.Size = new System.Drawing.Size(30, 14);
            this.togglebtnAutoUpdate.TabIndex = 5;
            this.togglebtnAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Auto check update";
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 176);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private ToggleButton togglebtnTopMost;
        private ToggleButton togglebtnNotify;
        private ToggleButton togglebtnSaveWatch;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.Button btnCancel;
        public ToggleButton togglebtnSystemTray;
        private System.Windows.Forms.Label label5;
        private ToggleButton togglebtnAutoUpdate;
    }
}