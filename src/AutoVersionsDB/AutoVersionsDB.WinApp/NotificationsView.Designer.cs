namespace AutoVersionsDB.WinApp
{
    partial class NotificationsView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        ///// <summary> 
        ///// Clean up any resources being used.
        ///// </summary>
        ///// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationsView));
            this.panel7 = new System.Windows.Forms.Panel();
            this.lblProcessLog = new System.Windows.Forms.Label();
            this.pbStatus = new System.Windows.Forms.PictureBox();
            this.btnProcessLog = new System.Windows.Forms.Button();
            this.lblProcessStatusMessage = new System.Windows.Forms.Label();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.lblProcessLog);
            this.panel7.Controls.Add(this.pbStatus);
            this.panel7.Controls.Add(this.btnProcessLog);
            this.panel7.Controls.Add(this.lblProcessStatusMessage);
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            // 
            // lblProcessLog
            // 
            resources.ApplyResources(this.lblProcessLog, "lblProcessLog");
            this.lblProcessLog.Name = "lblProcessLog";
            this.lblProcessLog.Click += new System.EventHandler(this.LblProcessLog_Click);
            // 
            // pbStatus
            // 
            resources.ApplyResources(this.pbStatus, "pbStatus");
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.TabStop = false;
            this.pbStatus.Click += new System.EventHandler(this.PbStatus_Click);
            // 
            // btnProcessLog
            // 
            resources.ApplyResources(this.btnProcessLog, "btnProcessLog");
            this.btnProcessLog.BackColor = System.Drawing.Color.White;
            this.btnProcessLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProcessLog.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnProcessLog.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnProcessLog.Name = "btnProcessLog";
            this.btnProcessLog.UseVisualStyleBackColor = false;
            this.btnProcessLog.Click += new System.EventHandler(this.BtnProcessLog_Click);
            // 
            // lblProcessStatusMessage
            // 
            resources.ApplyResources(this.lblProcessStatusMessage, "lblProcessStatusMessage");
            this.lblProcessStatusMessage.AutoEllipsis = true;
            this.lblProcessStatusMessage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProcessStatusMessage.ForeColor = System.Drawing.Color.DimGray;
            this.lblProcessStatusMessage.Name = "lblProcessStatusMessage";
            this.lblProcessStatusMessage.Click += new System.EventHandler(this.LblProcessStatusMessage_Click);
            // 
            // NotificationsView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel7);
            this.Name = "NotificationsView";
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
            this.ResumeLayout(false);

        }



        #endregion

        private System.Windows.Forms.Panel panel7;
  //      private System.Windows.Forms.Button imgBtnStatus;
        private System.Windows.Forms.Label lblProcessStatusMessage;
        private System.Windows.Forms.PictureBox pbStatus;
        private System.Windows.Forms.Label lblProcessLog;
        private System.Windows.Forms.Button btnProcessLog;
    }
}
