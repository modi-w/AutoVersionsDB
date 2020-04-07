namespace AutoVersionsDB.WinApp
{
    partial class MessageWindow
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
            this.rtbMessages = new System.Windows.Forms.RichTextBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.chkShowOnlyErrors = new System.Windows.Forms.CheckBox();
            this.imgMsgType = new System.Windows.Forms.Button();
            this.lblMessageType = new System.Windows.Forms.Label();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbMessages
            // 
            this.rtbMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbMessages.BackColor = System.Drawing.Color.White;
            this.rtbMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.rtbMessages.Location = new System.Drawing.Point(1, 60);
            this.rtbMessages.Name = "rtbMessages";
            this.rtbMessages.ReadOnly = true;
            this.rtbMessages.Size = new System.Drawing.Size(858, 295);
            this.rtbMessages.TabIndex = 1;
            this.rtbMessages.Text = "";
            this.rtbMessages.WordWrap = false;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Controls.Add(this.chkShowOnlyErrors);
            this.panel7.Controls.Add(this.imgMsgType);
            this.panel7.Controls.Add(this.lblMessageType);
            this.panel7.Location = new System.Drawing.Point(1, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(858, 53);
            this.panel7.TabIndex = 67;
            // 
            // chkShowOnlyErrors
            // 
            this.chkShowOnlyErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowOnlyErrors.AutoSize = true;
            this.chkShowOnlyErrors.Location = new System.Drawing.Point(740, 22);
            this.chkShowOnlyErrors.Name = "chkShowOnlyErrors";
            this.chkShowOnlyErrors.Size = new System.Drawing.Size(107, 17);
            this.chkShowOnlyErrors.TabIndex = 66;
            this.chkShowOnlyErrors.Text = "Show Only Errors";
            this.chkShowOnlyErrors.UseVisualStyleBackColor = true;
            this.chkShowOnlyErrors.CheckedChanged += new System.EventHandler(this.chkShowOnlyErrors_CheckedChanged);
            // 
            // imgMsgType
            // 
            this.imgMsgType.BackColor = System.Drawing.Color.Transparent;
            this.imgMsgType.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.imgMsgType.FlatAppearance.BorderSize = 0;
            this.imgMsgType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.imgMsgType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imgMsgType.ForeColor = System.Drawing.Color.DarkRed;
            this.imgMsgType.Image = global::AutoVersionsDB.WinApp.Properties.Resources.info2_32_32;
            this.imgMsgType.Location = new System.Drawing.Point(9, 10);
            this.imgMsgType.Name = "imgMsgType";
            this.imgMsgType.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.imgMsgType.Size = new System.Drawing.Size(38, 37);
            this.imgMsgType.TabIndex = 65;
            this.imgMsgType.UseVisualStyleBackColor = false;
            // 
            // lblMessageType
            // 
            this.lblMessageType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessageType.AutoEllipsis = true;
            this.lblMessageType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessageType.ForeColor = System.Drawing.Color.DimGray;
            this.lblMessageType.Location = new System.Drawing.Point(53, 17);
            this.lblMessageType.Name = "lblMessageType";
            this.lblMessageType.Size = new System.Drawing.Size(681, 23);
            this.lblMessageType.TabIndex = 58;
            this.lblMessageType.Text = "Message Type";
            this.lblMessageType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MessageWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(860, 358);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.rtbMessages);
            this.Name = "MessageWindow";
            this.ShowIcon = false;
            this.Text = "Attention";
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbMessages;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button imgMsgType;
        private System.Windows.Forms.Label lblMessageType;
        private System.Windows.Forms.CheckBox chkShowOnlyErrors;
    }
}