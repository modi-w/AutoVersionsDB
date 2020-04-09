namespace AutoVersionsDB.WinApp
{
    partial class Main
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
            this.tabMainLayout = new System.Windows.Forms.TabControl();
            this.tbChooseProject = new System.Windows.Forms.TabPage();
            this.chooseProject1 = new AutoVersionsDB.WinApp.ChooseProject();
            this.tbEditProjectConfig = new System.Windows.Forms.TabPage();
            this.editProjectConfigDetails1 = new AutoVersionsDB.WinApp.EditProjectConfigDetails();
            this.tbDBVersionsMangement = new System.Windows.Forms.TabPage();
            this.dbVersionsMangement1 = new AutoVersionsDB.WinApp.DBVersionsMangement();
            this.lnkBtnChooseProject = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.tabMainLayout.SuspendLayout();
            this.tbChooseProject.SuspendLayout();
            this.tbEditProjectConfig.SuspendLayout();
            this.tbDBVersionsMangement.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMainLayout
            // 
            this.tabMainLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMainLayout.Controls.Add(this.tbChooseProject);
            this.tabMainLayout.Controls.Add(this.tbEditProjectConfig);
            this.tabMainLayout.Controls.Add(this.tbDBVersionsMangement);
            this.tabMainLayout.Location = new System.Drawing.Point(-5, 37);
            this.tabMainLayout.Name = "tabMainLayout";
            this.tabMainLayout.SelectedIndex = 0;
            this.tabMainLayout.Size = new System.Drawing.Size(1004, 586);
            this.tabMainLayout.TabIndex = 1;
            // 
            // tbChooseProject
            // 
            this.tbChooseProject.AutoScroll = true;
            this.tbChooseProject.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbChooseProject.Controls.Add(this.chooseProject1);
            this.tbChooseProject.Location = new System.Drawing.Point(4, 22);
            this.tbChooseProject.Name = "tbChooseProject";
            this.tbChooseProject.Padding = new System.Windows.Forms.Padding(3);
            this.tbChooseProject.Size = new System.Drawing.Size(996, 560);
            this.tbChooseProject.TabIndex = 0;
            this.tbChooseProject.Text = "Choose Project";
            // 
            // chooseProject1
            // 
            this.chooseProject1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chooseProject1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chooseProject1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chooseProject1.Location = new System.Drawing.Point(3, 3);
            this.chooseProject1.Name = "chooseProject1";
            this.chooseProject1.Size = new System.Drawing.Size(993, 554);
            this.chooseProject1.TabIndex = 0;
            // 
            // tbEditProjectConfig
            // 
            this.tbEditProjectConfig.AutoScroll = true;
            this.tbEditProjectConfig.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbEditProjectConfig.Controls.Add(this.editProjectConfigDetails1);
            this.tbEditProjectConfig.Location = new System.Drawing.Point(4, 22);
            this.tbEditProjectConfig.Name = "tbEditProjectConfig";
            this.tbEditProjectConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tbEditProjectConfig.Size = new System.Drawing.Size(1187, 563);
            this.tbEditProjectConfig.TabIndex = 1;
            this.tbEditProjectConfig.Text = "Edit Project Config";
            // 
            // editProjectConfigDetails1
            // 
            this.editProjectConfigDetails1.AutoScroll = true;
            this.editProjectConfigDetails1.AutoSize = true;
            this.editProjectConfigDetails1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.editProjectConfigDetails1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editProjectConfigDetails1.Location = new System.Drawing.Point(3, 3);
            this.editProjectConfigDetails1.Name = "editProjectConfigDetails1";
            this.editProjectConfigDetails1.Size = new System.Drawing.Size(1181, 557);
            this.editProjectConfigDetails1.TabIndex = 0;
            // 
            // tbDBVersionsMangement
            // 
            this.tbDBVersionsMangement.AutoScroll = true;
            this.tbDBVersionsMangement.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbDBVersionsMangement.Controls.Add(this.dbVersionsMangement1);
            this.tbDBVersionsMangement.Location = new System.Drawing.Point(4, 22);
            this.tbDBVersionsMangement.Name = "tbDBVersionsMangement";
            this.tbDBVersionsMangement.Padding = new System.Windows.Forms.Padding(3);
            this.tbDBVersionsMangement.Size = new System.Drawing.Size(1187, 563);
            this.tbDBVersionsMangement.TabIndex = 2;
            this.tbDBVersionsMangement.Text = "DB Versions Mangement";
            // 
            // dbVersionsMangement1
            // 
            this.dbVersionsMangement1.AutoScroll = true;
            this.dbVersionsMangement1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dbVersionsMangement1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbVersionsMangement1.Location = new System.Drawing.Point(3, 3);
            this.dbVersionsMangement1.Name = "dbVersionsMangement1";
            this.dbVersionsMangement1.Size = new System.Drawing.Size(1181, 557);
            this.dbVersionsMangement1.TabIndex = 0;
            // 
            // lnkBtnChooseProject
            // 
            this.lnkBtnChooseProject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkBtnChooseProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lnkBtnChooseProject.Image = global::AutoVersionsDB.WinApp.Properties.Resources.exitIcon32_32;
            this.lnkBtnChooseProject.Location = new System.Drawing.Point(10, 4);
            this.lnkBtnChooseProject.Name = "lnkBtnChooseProject";
            this.lnkBtnChooseProject.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lnkBtnChooseProject.Size = new System.Drawing.Size(37, 27);
            this.lnkBtnChooseProject.TabIndex = 2;
            this.lnkBtnChooseProject.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkBtnChooseProject_LinkClicked);
            this.lnkBtnChooseProject.Click += new System.EventHandler(this.lnkBtnChooseProject_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Matura MT Script Capitals", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(55, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "Auto Versions DB";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(998, 620);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnkBtnChooseProject);
            this.Controls.Add(this.tabMainLayout);
            this.Name = "Main";
            this.ShowIcon = false;
            this.Text = "Auto Versions DB";
            this.tabMainLayout.ResumeLayout(false);
            this.tbChooseProject.ResumeLayout(false);
            this.tbEditProjectConfig.ResumeLayout(false);
            this.tbEditProjectConfig.PerformLayout();
            this.tbDBVersionsMangement.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabMainLayout;
        private System.Windows.Forms.TabPage tbChooseProject;
        private System.Windows.Forms.TabPage tbEditProjectConfig;
        private EditProjectConfigDetails editProjectConfigDetails1;
        private ChooseProject chooseProject1;
        private System.Windows.Forms.LinkLabel lnkBtnChooseProject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tbDBVersionsMangement;
        private DBVersionsMangement dbVersionsMangement1;
    }
}

