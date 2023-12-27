namespace PluginPopupTest
{
    partial class PopupForm
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
            this.lblParam1 = new System.Windows.Forms.Label();
            this.lblParam2 = new System.Windows.Forms.Label();
            this.lblParam3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblParam1
            // 
            this.lblParam1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblParam1.Location = new System.Drawing.Point(12, 9);
            this.lblParam1.Name = "lblParam1";
            this.lblParam1.Size = new System.Drawing.Size(776, 34);
            this.lblParam1.TabIndex = 0;
            // 
            // lblParam2
            // 
            this.lblParam2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblParam2.Location = new System.Drawing.Point(12, 54);
            this.lblParam2.Name = "lblParam2";
            this.lblParam2.Size = new System.Drawing.Size(776, 34);
            this.lblParam2.TabIndex = 1;
            // 
            // lblParam3
            // 
            this.lblParam3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblParam3.Location = new System.Drawing.Point(12, 105);
            this.lblParam3.Name = "lblParam3";
            this.lblParam3.Size = new System.Drawing.Size(776, 34);
            this.lblParam3.TabIndex = 2;
            // 
            // PopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblParam3);
            this.Controls.Add(this.lblParam2);
            this.Controls.Add(this.lblParam1);
            this.Name = "PopupForm";
            this.Text = "PopupForm";
            this.Shown += new System.EventHandler(this.PopupForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblParam1;
        public System.Windows.Forms.Label lblParam2;
        public System.Windows.Forms.Label lblParam3;
    }
}