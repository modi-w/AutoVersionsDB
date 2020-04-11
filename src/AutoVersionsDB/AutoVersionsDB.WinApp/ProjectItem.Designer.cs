namespace AutoVersionsDB.WinApp
{
    partial class ProjectItem
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
            this.lblProjectName = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblProcessLink = new System.Windows.Forms.Label();
            this.lblEditProject = new System.Windows.Forms.Label();
            this.lblDeleteProject = new System.Windows.Forms.Label();
            this.lblProjectIcon = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblProjectName
            // 
            this.lblProjectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProjectName.CausesValidation = false;
            this.lblProjectName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblProjectName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProjectName.Location = new System.Drawing.Point(63, 6);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(457, 56);
            this.lblProjectName.TabIndex = 3;
            this.lblProjectName.Text = "Project 1";
            this.lblProjectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProjectName.Click += new System.EventHandler(this.lblProjectName_Click);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label8.Location = new System.Drawing.Point(-1, -13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(4, 95);
            this.label8.TabIndex = 66;
            // 
            // lblProcessLink
            // 
            this.lblProcessLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProcessLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProcessLink.Image = global::AutoVersionsDB.WinApp.Properties.Resources.Play32;
            this.lblProcessLink.Location = new System.Drawing.Point(623, 8);
            this.lblProcessLink.Margin = new System.Windows.Forms.Padding(0);
            this.lblProcessLink.Name = "lblProcessLink";
            this.lblProcessLink.Size = new System.Drawing.Size(43, 56);
            this.lblProcessLink.TabIndex = 7;
            this.lblProcessLink.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblProcessLink.Click += new System.EventHandler(this.lblProcessLink_Click);
            // 
            // lblEditProject
            // 
            this.lblEditProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEditProject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblEditProject.Image = global::AutoVersionsDB.WinApp.Properties.Resources.EditIcon32;
            this.lblEditProject.Location = new System.Drawing.Point(573, 9);
            this.lblEditProject.Margin = new System.Windows.Forms.Padding(0);
            this.lblEditProject.Name = "lblEditProject";
            this.lblEditProject.Size = new System.Drawing.Size(50, 53);
            this.lblEditProject.TabIndex = 6;
            this.lblEditProject.Click += new System.EventHandler(this.lblEditProject_Click);
            // 
            // lblDeleteProject
            // 
            this.lblDeleteProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDeleteProject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDeleteProject.Image = global::AutoVersionsDB.WinApp.Properties.Resources.trashIcon32_red;
            this.lblDeleteProject.Location = new System.Drawing.Point(523, 9);
            this.lblDeleteProject.Margin = new System.Windows.Forms.Padding(0);
            this.lblDeleteProject.Name = "lblDeleteProject";
            this.lblDeleteProject.Size = new System.Drawing.Size(50, 53);
            this.lblDeleteProject.TabIndex = 5;
            this.lblDeleteProject.Click += new System.EventHandler(this.lblDeleteProject_Click);
            // 
            // lblProjectIcon
            // 
            this.lblProjectIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProjectIcon.Image = global::AutoVersionsDB.WinApp.Properties.Resources.dbIcon_32_34;
            this.lblProjectIcon.Location = new System.Drawing.Point(12, 6);
            this.lblProjectIcon.Name = "lblProjectIcon";
            this.lblProjectIcon.Size = new System.Drawing.Size(45, 56);
            this.lblProjectIcon.TabIndex = 4;
            this.lblProjectIcon.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblProjectIcon.Click += new System.EventHandler(this.lblProjectIcon_Click);
            // 
            // ProjectItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblProcessLink);
            this.Controls.Add(this.lblEditProject);
            this.Controls.Add(this.lblDeleteProject);
            this.Controls.Add(this.lblProjectIcon);
            this.Controls.Add(this.lblProjectName);
            this.Name = "ProjectItem";
            this.Size = new System.Drawing.Size(669, 69);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDeleteProject;
        private System.Windows.Forms.Label lblProjectIcon;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.Label lblEditProject;
        private System.Windows.Forms.Label lblProcessLink;
        private System.Windows.Forms.Label label8;
    }
}
