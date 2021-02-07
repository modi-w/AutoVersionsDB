using Ninject;

namespace AutoVersionsDB.WinApp
{
    partial class MainView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.tabMainLayout = new System.Windows.Forms.TabControl();
            this.tbChooseProject = new System.Windows.Forms.TabPage();
            this.chooseProject1 = new AutoVersionsDB.WinApp.ChooseProjectView();
            this.tbEditProjectConfig = new System.Windows.Forms.TabPage();
            this.editProjectConfigDetails1 = new AutoVersionsDB.WinApp.EditProjectView();
            this.tbDBVersionsMangement = new System.Windows.Forms.TabPage();
            this.dbVersionsMangement1 = new AutoVersionsDB.WinApp.DBVersionsView();
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
            this.tabMainLayout.Location = new System.Drawing.Point(-7, 58);
            this.tabMainLayout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabMainLayout.Name = "tabMainLayout";
            this.tabMainLayout.SelectedIndex = 0;
            this.tabMainLayout.Size = new System.Drawing.Size(1047, 615);
            this.tabMainLayout.TabIndex = 1;
            // 
            // tbChooseProject
            // 
            this.tbChooseProject.AutoScroll = true;
            this.tbChooseProject.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbChooseProject.Controls.Add(this.chooseProject1);
            this.tbChooseProject.Location = new System.Drawing.Point(4, 29);
            this.tbChooseProject.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbChooseProject.Name = "tbChooseProject";
            this.tbChooseProject.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbChooseProject.Size = new System.Drawing.Size(1039, 582);
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
            this.chooseProject1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chooseProject1.Location = new System.Drawing.Point(4, 5);
            this.chooseProject1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.chooseProject1.Name = "chooseProject1";
            this.chooseProject1.Size = new System.Drawing.Size(1039, 564);
            this.chooseProject1.TabIndex = 0;
            this.chooseProject1.ViewModel = null;
            // 
            // tbEditProjectConfig
            // 
            this.tbEditProjectConfig.AutoScroll = true;
            this.tbEditProjectConfig.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbEditProjectConfig.Controls.Add(this.editProjectConfigDetails1);
            this.tbEditProjectConfig.Location = new System.Drawing.Point(4, 29);
            this.tbEditProjectConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbEditProjectConfig.Name = "tbEditProjectConfig";
            this.tbEditProjectConfig.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbEditProjectConfig.Size = new System.Drawing.Size(1037, 558);
            this.tbEditProjectConfig.TabIndex = 1;
            this.tbEditProjectConfig.Text = "Edit Project Config";
            // 
            // editProjectConfigDetails1
            // 
            this.editProjectConfigDetails1.AutoScroll = true;
            this.editProjectConfigDetails1.AutoSize = true;
            this.editProjectConfigDetails1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.editProjectConfigDetails1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editProjectConfigDetails1.Location = new System.Drawing.Point(4, 5);
            this.editProjectConfigDetails1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.editProjectConfigDetails1.Name = "editProjectConfigDetails1";
            this.editProjectConfigDetails1.Size = new System.Drawing.Size(1029, 548);
            this.editProjectConfigDetails1.TabIndex = 0;
            this.editProjectConfigDetails1.ViewModel = null;
            // 
            // tbDBVersionsMangement
            // 
            this.tbDBVersionsMangement.AutoScroll = true;
            this.tbDBVersionsMangement.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbDBVersionsMangement.Controls.Add(this.dbVersionsMangement1);
            this.tbDBVersionsMangement.Location = new System.Drawing.Point(4, 29);
            this.tbDBVersionsMangement.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDBVersionsMangement.Name = "tbDBVersionsMangement";
            this.tbDBVersionsMangement.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDBVersionsMangement.Size = new System.Drawing.Size(1037, 558);
            this.tbDBVersionsMangement.TabIndex = 2;
            this.tbDBVersionsMangement.Text = "DB Versions Mangement";
            // 
            // dbVersionsMangement1
            // 
            this.dbVersionsMangement1.AutoScroll = true;
            this.dbVersionsMangement1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dbVersionsMangement1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbVersionsMangement1.Location = new System.Drawing.Point(4, 5);
            this.dbVersionsMangement1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.dbVersionsMangement1.Name = "dbVersionsMangement1";
            this.dbVersionsMangement1.Size = new System.Drawing.Size(1029, 548);
            this.dbVersionsMangement1.TabIndex = 0;
            this.dbVersionsMangement1.ViewModel = null;
            // 
            // lnkBtnChooseProject
            // 
            this.lnkBtnChooseProject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkBtnChooseProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lnkBtnChooseProject.Image = ((System.Drawing.Image)(resources.GetObject("lnkBtnChooseProject.Image")));
            this.lnkBtnChooseProject.Location = new System.Drawing.Point(13, 6);
            this.lnkBtnChooseProject.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkBtnChooseProject.Name = "lnkBtnChooseProject";
            this.lnkBtnChooseProject.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lnkBtnChooseProject.Size = new System.Drawing.Size(49, 41);
            this.lnkBtnChooseProject.TabIndex = 2;
            this.lnkBtnChooseProject.Click += new System.EventHandler(this.LnkBtnChooseProject_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Matura MT Script Capitals", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(73, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 40);
            this.label1.TabIndex = 3;
            this.label1.Text = "Auto Versions DB";
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1039, 667);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnkBtnChooseProject);
            this.Controls.Add(this.tabMainLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainView";
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
        private EditProjectView editProjectConfigDetails1;
        private ChooseProjectView chooseProject1;
        private System.Windows.Forms.LinkLabel lnkBtnChooseProject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tbDBVersionsMangement;
        private DBVersionsView dbVersionsMangement1;
    }
}

