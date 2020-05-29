namespace AutoVersionsDB.WinApp
{
    partial class NotificationsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationsControl));
            this.panel7 = new System.Windows.Forms.Panel();
            this.imgBtnStatus = new System.Windows.Forms.Button();
            this.lblProcessStatusMessage = new System.Windows.Forms.Label();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.imgBtnStatus);
            this.panel7.Controls.Add(this.lblProcessStatusMessage);
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            // 
            // imgBtnStatus
            // 
            this.imgBtnStatus.BackColor = System.Drawing.Color.Transparent;
            this.imgBtnStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgBtnStatus.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.imgBtnStatus.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.imgBtnStatus, "imgBtnStatus");
            this.imgBtnStatus.ForeColor = System.Drawing.Color.DarkRed;
            this.imgBtnStatus.Image = global::AutoVersionsDB.WinApp.Properties.Resources.StopIcon_32;
            this.imgBtnStatus.Name = "imgBtnStatus";
            this.imgBtnStatus.UseVisualStyleBackColor = false;
            this.imgBtnStatus.Click += new System.EventHandler(this.imgBtnStatus_Click);
            // 
            // lblProcessStatusMessage
            // 
            resources.ApplyResources(this.lblProcessStatusMessage, "lblProcessStatusMessage");
            this.lblProcessStatusMessage.AutoEllipsis = true;
            this.lblProcessStatusMessage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProcessStatusMessage.ForeColor = System.Drawing.Color.DimGray;
            this.lblProcessStatusMessage.Name = "lblProcessStatusMessage";
            this.lblProcessStatusMessage.Click += new System.EventHandler(this.lblProcessStatusMessage_Click);
            // 
            // NotificationsControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel7);
            this.Name = "NotificationsControl";
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button imgBtnStatus;
        private System.Windows.Forms.Label lblProcessStatusMessage;
    }
}
