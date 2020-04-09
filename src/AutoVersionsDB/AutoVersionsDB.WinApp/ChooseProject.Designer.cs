namespace AutoVersionsDB.WinApp
{
    partial class ChooseProject
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.tbSerchProject = new System.Windows.Forms.TextBox();
            this.btnNewProject = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 69);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(987, 482);
            this.flowLayoutPanel1.TabIndex = 67;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.tbSerchProject);
            this.panel2.Controls.Add(this.btnNewProject);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(967, 53);
            this.panel2.TabIndex = 33;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(4, 95);
            this.label8.TabIndex = 65;
            // 
            // tbSerchProject
            // 
            this.tbSerchProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.tbSerchProject.Location = new System.Drawing.Point(10, 7);
            this.tbSerchProject.Name = "tbSerchProject";
            this.tbSerchProject.Size = new System.Drawing.Size(399, 38);
            this.tbSerchProject.TabIndex = 31;
            this.tbSerchProject.TextChanged += new System.EventHandler(this.tbSerchProject_TextChanged);
            // 
            // btnNewProject
            // 
            this.btnNewProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewProject.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnNewProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.btnNewProject.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnNewProject.Image = global::AutoVersionsDB.WinApp.Properties.Resources.plus32_white;
            this.btnNewProject.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewProject.Location = new System.Drawing.Point(855, 3);
            this.btnNewProject.Name = "btnNewProject";
            this.btnNewProject.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnNewProject.Size = new System.Drawing.Size(106, 44);
            this.btnNewProject.TabIndex = 30;
            this.btnNewProject.Text = "New";
            this.btnNewProject.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnNewProject.UseVisualStyleBackColor = false;
            this.btnNewProject.Click += new System.EventHandler(this.btnNewProject_Click);
            // 
            // ChooseProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Name = "ChooseProject";
            this.Size = new System.Drawing.Size(993, 554);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbSerchProject;
        private System.Windows.Forms.Button btnNewProject;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
