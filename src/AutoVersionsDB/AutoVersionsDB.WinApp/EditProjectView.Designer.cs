namespace AutoVersionsDB.WinApp
{
    public partial class EditProjectView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditProjectView));
            this.label2 = new System.Windows.Forms.Label();
            this.tbId = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbDBBackupFolder = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbDevScriptsFolderPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboConncectionType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errPrvProjectDetails = new System.Windows.Forms.ErrorProvider(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.lblIncrementalScriptsFolderPath = new System.Windows.Forms.Label();
            this.lblRepeatableScriptsFolderPath = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lblDevDummyDataScriptsFolderPath = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblDBProcess = new System.Windows.Forms.Label();
            this.imgValid = new System.Windows.Forms.Label();
            this.imgError = new System.Windows.Forms.Label();
            this.btnNavToProcess = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancelEditId = new System.Windows.Forms.Button();
            this.btnSaveId = new System.Windows.Forms.Button();
            this.btnEditId = new System.Windows.Forms.Button();
            this.tbProjectDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbConncetionTimeout = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbDBName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.rbDelEnv = new System.Windows.Forms.RadioButton();
            this.rbDevEnv = new System.Windows.Forms.RadioButton();
            this.label18 = new System.Windows.Forms.Label();
            this.pnlDevEnvFoldersFields = new System.Windows.Forms.Panel();
            this.label30 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDeployArtifactFolderPath = new System.Windows.Forms.TextBox();
            this.pnlDelEnvFields = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.tbDeliveryArtifactFolderPath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.notificationsControl1 = new AutoVersionsDB.WinApp.NotificationsView();
            this.pnlDevEnvDeplyFolder = new System.Windows.Forms.Panel();
            this.label26 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errPrvProjectDetails)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlDevEnvFoldersFields.SuspendLayout();
            this.pnlDelEnvFields.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pnlDevEnvDeplyFolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(25, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 36);
            this.label2.TabIndex = 1;
            this.label2.Text = "Id:";
            // 
            // tbId
            // 
            this.tbId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbId.Location = new System.Drawing.Point(206, 32);
            this.tbId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbId.Name = "tbId";
            this.tbId.Size = new System.Drawing.Size(284, 30);
            this.tbId.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(24, 14);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(265, 36);
            this.label9.TabIndex = 19;
            this.label9.Text = "DB Backup Folder:";
            // 
            // tbDBBackupFolder
            // 
            this.tbDBBackupFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDBBackupFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbDBBackupFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.tbDBBackupFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbDBBackupFolder.Location = new System.Drawing.Point(29, 62);
            this.tbDBBackupFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDBBackupFolder.Name = "tbDBBackupFolder";
            this.tbDBBackupFolder.Size = new System.Drawing.Size(943, 34);
            this.tbDBBackupFolder.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.Location = new System.Drawing.Point(13, 21);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(277, 36);
            this.label11.TabIndex = 22;
            this.label11.Text = "Scripts Folder Path:";
            // 
            // tbDevScriptsFolderPath
            // 
            this.tbDevScriptsFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDevScriptsFolderPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbDevScriptsFolderPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.tbDevScriptsFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbDevScriptsFolderPath.Location = new System.Drawing.Point(19, 71);
            this.tbDevScriptsFolderPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDevScriptsFolderPath.Name = "tbDevScriptsFolderPath";
            this.tbDevScriptsFolderPath.Size = new System.Drawing.Size(953, 34);
            this.tbDevScriptsFolderPath.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(24, 15);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 36);
            this.label6.TabIndex = 31;
            this.label6.Text = "DB Type:";
            // 
            // cboConncectionType
            // 
            this.cboConncectionType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboConncectionType.FormattingEnabled = true;
            this.cboConncectionType.Location = new System.Drawing.Point(206, 18);
            this.cboConncectionType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboConncectionType.Name = "cboConncectionType";
            this.cboConncectionType.Size = new System.Drawing.Size(333, 37);
            this.cboConncectionType.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(25, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project Details";
            // 
            // errPrvProjectDetails
            // 
            this.errPrvProjectDetails.ContainerControl = this;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label12.ForeColor = System.Drawing.Color.DimGray;
            this.errPrvProjectDetails.SetIconAlignment(this.label12, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.label12.Location = new System.Drawing.Point(19, 122);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(323, 31);
            this.label12.TabIndex = 23;
            this.label12.Text = "Incremental Scripts Folder Path:";
            // 
            // lblIncrementalScriptsFolderPath
            // 
            this.lblIncrementalScriptsFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIncrementalScriptsFolderPath.AutoEllipsis = true;
            this.lblIncrementalScriptsFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblIncrementalScriptsFolderPath.ForeColor = System.Drawing.Color.DimGray;
            this.errPrvProjectDetails.SetIconAlignment(this.lblIncrementalScriptsFolderPath, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.lblIncrementalScriptsFolderPath.Location = new System.Drawing.Point(19, 160);
            this.lblIncrementalScriptsFolderPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIncrementalScriptsFolderPath.Name = "lblIncrementalScriptsFolderPath";
            this.lblIncrementalScriptsFolderPath.Size = new System.Drawing.Size(915, 38);
            this.lblIncrementalScriptsFolderPath.TabIndex = 24;
            this.lblIncrementalScriptsFolderPath.Text = "C:\\Projects\\DBAutoVersions\\Code\\DBAutoVersions\\DBAutoVersions.BL.IntegrationTests" +
    "\\ScriptsFilesForTests\\StartState\\db_initState.sql";
            // 
            // lblRepeatableScriptsFolderPath
            // 
            this.lblRepeatableScriptsFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRepeatableScriptsFolderPath.AutoEllipsis = true;
            this.lblRepeatableScriptsFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblRepeatableScriptsFolderPath.ForeColor = System.Drawing.Color.DimGray;
            this.errPrvProjectDetails.SetIconAlignment(this.lblRepeatableScriptsFolderPath, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.lblRepeatableScriptsFolderPath.Location = new System.Drawing.Point(15, 234);
            this.lblRepeatableScriptsFolderPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRepeatableScriptsFolderPath.Name = "lblRepeatableScriptsFolderPath";
            this.lblRepeatableScriptsFolderPath.Size = new System.Drawing.Size(915, 38);
            this.lblRepeatableScriptsFolderPath.TabIndex = 66;
            this.lblRepeatableScriptsFolderPath.Text = "C:\\Projects\\DBAutoVersions\\Code\\DBAutoVersions\\DBAutoVersions.BL.IntegrationTests" +
    "\\ScriptsFilesForTests\\StartState\\db_initState.sql";
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label27.ForeColor = System.Drawing.Color.DimGray;
            this.errPrvProjectDetails.SetIconAlignment(this.label27, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.label27.Location = new System.Drawing.Point(15, 198);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(323, 31);
            this.label27.TabIndex = 65;
            this.label27.Text = "Repeatable Scripts Folder Path:";
            // 
            // lblDevDummyDataScriptsFolderPath
            // 
            this.lblDevDummyDataScriptsFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDevDummyDataScriptsFolderPath.AutoEllipsis = true;
            this.lblDevDummyDataScriptsFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDevDummyDataScriptsFolderPath.ForeColor = System.Drawing.Color.DimGray;
            this.errPrvProjectDetails.SetIconAlignment(this.lblDevDummyDataScriptsFolderPath, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.lblDevDummyDataScriptsFolderPath.Location = new System.Drawing.Point(13, 308);
            this.lblDevDummyDataScriptsFolderPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDevDummyDataScriptsFolderPath.Name = "lblDevDummyDataScriptsFolderPath";
            this.lblDevDummyDataScriptsFolderPath.Size = new System.Drawing.Size(915, 38);
            this.lblDevDummyDataScriptsFolderPath.TabIndex = 68;
            this.lblDevDummyDataScriptsFolderPath.Text = "C:\\Projects\\DBAutoVersions\\Code\\DBAutoVersions\\DBAutoVersions.BL.IntegrationTests" +
    "\\ScriptsFilesForTests\\StartState\\db_initState.sql";
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label29.ForeColor = System.Drawing.Color.DimGray;
            this.errPrvProjectDetails.SetIconAlignment(this.label29, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.label29.Location = new System.Drawing.Point(13, 271);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(404, 31);
            this.label29.TabIndex = 67;
            this.label29.Text = "Dev Dummy Data Scripts Folder Path:";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label28);
            this.panel5.Controls.Add(this.btnSave);
            this.panel5.Controls.Add(this.lblDBProcess);
            this.panel5.Controls.Add(this.imgValid);
            this.panel5.Controls.Add(this.imgError);
            this.panel5.Controls.Add(this.btnNavToProcess);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel5.Location = new System.Drawing.Point(27, 19);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(995, 102);
            this.panel5.TabIndex = 64;
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label28.Location = new System.Drawing.Point(833, 78);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(50, 20);
            this.label28.TabIndex = 89;
            this.label28.Text = "Save";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSave.ForeColor = System.Drawing.Color.DarkRed;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(835, 19);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.btnSave.Size = new System.Drawing.Size(51, 61);
            this.btnSave.TabIndex = 88;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // lblDBProcess
            // 
            this.lblDBProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDBProcess.AutoSize = true;
            this.lblDBProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDBProcess.Location = new System.Drawing.Point(907, 81);
            this.lblDBProcess.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDBProcess.Name = "lblDBProcess";
            this.lblDBProcess.Size = new System.Drawing.Size(71, 15);
            this.lblDBProcess.TabIndex = 87;
            this.lblDBProcess.Text = "DB Process";
            // 
            // imgValid
            // 
            this.imgValid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgValid.Image = ((System.Drawing.Image)(resources.GetObject("imgValid.Image")));
            this.imgValid.Location = new System.Drawing.Point(775, 20);
            this.imgValid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.imgValid.Name = "imgValid";
            this.imgValid.Size = new System.Drawing.Size(51, 61);
            this.imgValid.TabIndex = 72;
            // 
            // imgError
            // 
            this.imgError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgError.Location = new System.Drawing.Point(716, 20);
            this.imgError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.imgError.Name = "imgError";
            this.imgError.Size = new System.Drawing.Size(51, 61);
            this.imgError.TabIndex = 71;
            // 
            // btnNavToProcess
            // 
            this.btnNavToProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNavToProcess.BackColor = System.Drawing.Color.Transparent;
            this.btnNavToProcess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavToProcess.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnNavToProcess.FlatAppearance.BorderSize = 0;
            this.btnNavToProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavToProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNavToProcess.ForeColor = System.Drawing.Color.DarkRed;
            this.btnNavToProcess.Image = ((System.Drawing.Image)(resources.GetObject("btnNavToProcess.Image")));
            this.btnNavToProcess.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNavToProcess.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNavToProcess.Location = new System.Drawing.Point(921, 20);
            this.btnNavToProcess.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNavToProcess.Name = "btnNavToProcess";
            this.btnNavToProcess.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.btnNavToProcess.Size = new System.Drawing.Size(51, 61);
            this.btnNavToProcess.TabIndex = 68;
            this.btnNavToProcess.UseVisualStyleBackColor = false;
            this.btnNavToProcess.Click += new System.EventHandler(this.BtnNavToProcess_Click);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label15.Location = new System.Drawing.Point(0, -125);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(5, 201);
            this.label15.TabIndex = 64;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnCancelEditId);
            this.panel1.Controls.Add(this.btnSaveId);
            this.panel1.Controls.Add(this.btnEditId);
            this.panel1.Controls.Add(this.tbProjectDescription);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.tbId);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(27, 251);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(995, 151);
            this.panel1.TabIndex = 70;
            // 
            // btnCancelEditId
            // 
            this.btnCancelEditId.BackColor = System.Drawing.Color.Transparent;
            this.btnCancelEditId.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelEditId.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancelEditId.FlatAppearance.BorderSize = 0;
            this.btnCancelEditId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelEditId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCancelEditId.ForeColor = System.Drawing.Color.Gray;
            this.btnCancelEditId.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancelEditId.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancelEditId.Location = new System.Drawing.Point(162, 29);
            this.btnCancelEditId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancelEditId.Name = "btnCancelEditId";
            this.btnCancelEditId.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.btnCancelEditId.Size = new System.Drawing.Size(36, 49);
            this.btnCancelEditId.TabIndex = 91;
            this.btnCancelEditId.Text = "X";
            this.btnCancelEditId.UseVisualStyleBackColor = false;
            this.btnCancelEditId.Click += new System.EventHandler(this.BtnCancelEditId_Click);
            // 
            // btnSaveId
            // 
            this.btnSaveId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveId.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveId.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveId.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSaveId.FlatAppearance.BorderSize = 0;
            this.btnSaveId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSaveId.ForeColor = System.Drawing.Color.DarkRed;
            this.btnSaveId.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSaveId.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSaveId.Location = new System.Drawing.Point(557, 21);
            this.btnSaveId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSaveId.Name = "btnSaveId";
            this.btnSaveId.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.btnSaveId.Size = new System.Drawing.Size(51, 61);
            this.btnSaveId.TabIndex = 90;
            this.btnSaveId.UseVisualStyleBackColor = false;
            this.btnSaveId.Click += new System.EventHandler(this.BtnSaveId_Click);
            // 
            // btnEditId
            // 
            this.btnEditId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditId.BackColor = System.Drawing.Color.Transparent;
            this.btnEditId.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditId.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnEditId.FlatAppearance.BorderSize = 0;
            this.btnEditId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnEditId.ForeColor = System.Drawing.Color.DarkRed;
            this.btnEditId.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEditId.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEditId.Location = new System.Drawing.Point(498, 21);
            this.btnEditId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEditId.Name = "btnEditId";
            this.btnEditId.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.btnEditId.Size = new System.Drawing.Size(51, 61);
            this.btnEditId.TabIndex = 89;
            this.btnEditId.UseVisualStyleBackColor = false;
            this.btnEditId.Click += new System.EventHandler(this.BtnEditId_Click);
            // 
            // tbProjectDescription
            // 
            this.tbProjectDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProjectDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbProjectDescription.Location = new System.Drawing.Point(206, 89);
            this.tbProjectDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbProjectDescription.Name = "tbProjectDescription";
            this.tbProjectDescription.Size = new System.Drawing.Size(728, 30);
            this.tbProjectDescription.TabIndex = 66;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(25, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 36);
            this.label3.TabIndex = 65;
            this.label3.Text = "Description:";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.BackColor = System.Drawing.Color.Aqua;
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(5, 151);
            this.label16.TabIndex = 64;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tbConncetionTimeout);
            this.panel2.Controls.Add(this.label31);
            this.panel2.Controls.Add(this.tbPassword);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.tbUsername);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.tbDBName);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.tbServer);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.cboConncectionType);
            this.panel2.Location = new System.Drawing.Point(27, 412);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(995, 338);
            this.panel2.TabIndex = 71;
            // 
            // tbConncetionTimeout
            // 
            this.tbConncetionTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbConncetionTimeout.Location = new System.Drawing.Point(862, 100);
            this.tbConncetionTimeout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbConncetionTimeout.Name = "tbConncetionTimeout";
            this.tbConncetionTimeout.Size = new System.Drawing.Size(89, 30);
            this.tbConncetionTimeout.TabIndex = 76;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label31.Location = new System.Drawing.Point(716, 92);
            this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(129, 36);
            this.label31.TabIndex = 75;
            this.label31.Text = "Timeout:";
            // 
            // tbPassword
            // 
            this.tbPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbPassword.Location = new System.Drawing.Point(205, 250);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(464, 30);
            this.tbPassword.TabIndex = 74;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label14.Location = new System.Drawing.Point(24, 244);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(155, 36);
            this.label14.TabIndex = 73;
            this.label14.Text = "Password:";
            // 
            // tbUsername
            // 
            this.tbUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbUsername.Location = new System.Drawing.Point(205, 196);
            this.tbUsername.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(464, 30);
            this.tbUsername.TabIndex = 72;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label13.Location = new System.Drawing.Point(24, 190);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(158, 36);
            this.label13.TabIndex = 71;
            this.label13.Text = "Username:";
            // 
            // tbDBName
            // 
            this.tbDBName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbDBName.Location = new System.Drawing.Point(205, 144);
            this.tbDBName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDBName.Name = "tbDBName";
            this.tbDBName.Size = new System.Drawing.Size(464, 30);
            this.tbDBName.TabIndex = 70;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(24, 138);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(141, 36);
            this.label10.TabIndex = 69;
            this.label10.Text = "DB Name";
            // 
            // tbServer
            // 
            this.tbServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbServer.Location = new System.Drawing.Point(204, 92);
            this.tbServer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(464, 30);
            this.tbServer.TabIndex = 68;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(23, 86);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 36);
            this.label5.TabIndex = 67;
            this.label5.Text = "Server:";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.BackColor = System.Drawing.Color.Aqua;
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(5, 339);
            this.label17.TabIndex = 64;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.label23);
            this.panel3.Controls.Add(this.label24);
            this.panel3.Controls.Add(this.label22);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.rbDelEnv);
            this.panel3.Controls.Add(this.rbDevEnv);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Location = new System.Drawing.Point(27, 911);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(995, 177);
            this.panel3.TabIndex = 71;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label20.Location = new System.Drawing.Point(499, 129);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(244, 18);
            this.label20.TabIndex = 73;
            this.label20.Text = "* Not allow drop and recreate the db";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label23.Location = new System.Drawing.Point(499, 101);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(186, 18);
            this.label23.TabIndex = 72;
            this.label23.Text = "* Not allow add scripts files";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label24.Location = new System.Drawing.Point(499, 74);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(218, 18);
            this.label24.TabIndex = 71;
            this.label24.Text = "* Work with artifact deployed file";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label22.Location = new System.Drawing.Point(52, 129);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(217, 18);
            this.label22.TabIndex = 70;
            this.label22.Text = "* Allow drop and recreate the db";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label21.Location = new System.Drawing.Point(52, 101);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(159, 18);
            this.label21.TabIndex = 69;
            this.label21.Text = "* Allow add scripts files";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label19.Location = new System.Drawing.Point(52, 74);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(163, 18);
            this.label19.TabIndex = 67;
            this.label19.Text = "* Work with scripts files";
            // 
            // rbDelEnv
            // 
            this.rbDelEnv.AutoSize = true;
            this.rbDelEnv.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbDelEnv.Location = new System.Drawing.Point(473, 19);
            this.rbDelEnv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbDelEnv.Name = "rbDelEnv";
            this.rbDelEnv.Size = new System.Drawing.Size(301, 40);
            this.rbDelEnv.TabIndex = 66;
            this.rbDelEnv.TabStop = true;
            this.rbDelEnv.Text = "Delivery Enviroment";
            this.rbDelEnv.UseVisualStyleBackColor = true;
            // 
            // rbDevEnv
            // 
            this.rbDevEnv.AutoSize = true;
            this.rbDevEnv.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbDevEnv.Location = new System.Drawing.Point(27, 19);
            this.rbDevEnv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbDevEnv.Name = "rbDevEnv";
            this.rbDevEnv.Size = new System.Drawing.Size(367, 40);
            this.rbDevEnv.TabIndex = 65;
            this.rbDevEnv.TabStop = true;
            this.rbDevEnv.Text = "Development Enviroment";
            this.rbDevEnv.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label18.BackColor = System.Drawing.Color.Aqua;
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(5, 178);
            this.label18.TabIndex = 64;
            // 
            // pnlDevEnvFoldersFields
            // 
            this.pnlDevEnvFoldersFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDevEnvFoldersFields.BackColor = System.Drawing.Color.White;
            this.pnlDevEnvFoldersFields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDevEnvFoldersFields.Controls.Add(this.lblDevDummyDataScriptsFolderPath);
            this.pnlDevEnvFoldersFields.Controls.Add(this.label29);
            this.pnlDevEnvFoldersFields.Controls.Add(this.lblRepeatableScriptsFolderPath);
            this.pnlDevEnvFoldersFields.Controls.Add(this.label27);
            this.pnlDevEnvFoldersFields.Controls.Add(this.label30);
            this.pnlDevEnvFoldersFields.Controls.Add(this.label11);
            this.pnlDevEnvFoldersFields.Controls.Add(this.tbDevScriptsFolderPath);
            this.pnlDevEnvFoldersFields.Controls.Add(this.lblIncrementalScriptsFolderPath);
            this.pnlDevEnvFoldersFields.Controls.Add(this.label12);
            this.pnlDevEnvFoldersFields.Location = new System.Drawing.Point(27, 1099);
            this.pnlDevEnvFoldersFields.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlDevEnvFoldersFields.Name = "pnlDevEnvFoldersFields";
            this.pnlDevEnvFoldersFields.Size = new System.Drawing.Size(995, 370);
            this.pnlDevEnvFoldersFields.TabIndex = 74;
            // 
            // label30
            // 
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label30.BackColor = System.Drawing.Color.Aqua;
            this.label30.Location = new System.Drawing.Point(0, 0);
            this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(5, 369);
            this.label30.TabIndex = 64;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(13, 32);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(379, 36);
            this.label4.TabIndex = 66;
            this.label4.Text = "Deploy Artifact Folder Path:";
            // 
            // tbDeployArtifactFolderPath
            // 
            this.tbDeployArtifactFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDeployArtifactFolderPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbDeployArtifactFolderPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.tbDeployArtifactFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbDeployArtifactFolderPath.Location = new System.Drawing.Point(19, 81);
            this.tbDeployArtifactFolderPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDeployArtifactFolderPath.Name = "tbDeployArtifactFolderPath";
            this.tbDeployArtifactFolderPath.Size = new System.Drawing.Size(953, 34);
            this.tbDeployArtifactFolderPath.TabIndex = 65;
            // 
            // pnlDelEnvFields
            // 
            this.pnlDelEnvFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDelEnvFields.BackColor = System.Drawing.Color.White;
            this.pnlDelEnvFields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDelEnvFields.Controls.Add(this.label7);
            this.pnlDelEnvFields.Controls.Add(this.tbDeliveryArtifactFolderPath);
            this.pnlDelEnvFields.Controls.Add(this.label8);
            this.pnlDelEnvFields.Location = new System.Drawing.Point(27, 1642);
            this.pnlDelEnvFields.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlDelEnvFields.Name = "pnlDelEnvFields";
            this.pnlDelEnvFields.Size = new System.Drawing.Size(995, 154);
            this.pnlDelEnvFields.TabIndex = 75;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(13, 26);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(393, 36);
            this.label7.TabIndex = 66;
            this.label7.Text = "Delivery Artifact Folder Path:";
            // 
            // tbDeliveryArtifactFolderPath
            // 
            this.tbDeliveryArtifactFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDeliveryArtifactFolderPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbDeliveryArtifactFolderPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.tbDeliveryArtifactFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbDeliveryArtifactFolderPath.Location = new System.Drawing.Point(19, 75);
            this.tbDeliveryArtifactFolderPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDeliveryArtifactFolderPath.Name = "tbDeliveryArtifactFolderPath";
            this.tbDeliveryArtifactFolderPath.Size = new System.Drawing.Size(953, 34);
            this.tbDeliveryArtifactFolderPath.TabIndex = 65;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.BackColor = System.Drawing.Color.Aqua;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(5, 154);
            this.label8.TabIndex = 64;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.label25);
            this.panel7.Controls.Add(this.label9);
            this.panel7.Controls.Add(this.tbDBBackupFolder);
            this.panel7.Location = new System.Drawing.Point(27, 761);
            this.panel7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(995, 140);
            this.panel7.TabIndex = 76;
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label25.BackColor = System.Drawing.Color.Aqua;
            this.label25.Location = new System.Drawing.Point(0, 0);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(5, 139);
            this.label25.TabIndex = 64;
            // 
            // notificationsControl1
            // 
            this.notificationsControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notificationsControl1.Location = new System.Drawing.Point(27, 131);
            this.notificationsControl1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.notificationsControl1.Name = "notificationsControl1";
            this.notificationsControl1.Size = new System.Drawing.Size(996, 81);
            this.notificationsControl1.TabIndex = 77;
            this.notificationsControl1.ViewModel = null;
            // 
            // pnlDevEnvDeplyFolder
            // 
            this.pnlDevEnvDeplyFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDevEnvDeplyFolder.BackColor = System.Drawing.Color.White;
            this.pnlDevEnvDeplyFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDevEnvDeplyFolder.Controls.Add(this.label4);
            this.pnlDevEnvDeplyFolder.Controls.Add(this.tbDeployArtifactFolderPath);
            this.pnlDevEnvDeplyFolder.Controls.Add(this.label26);
            this.pnlDevEnvDeplyFolder.Location = new System.Drawing.Point(27, 1479);
            this.pnlDevEnvDeplyFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlDevEnvDeplyFolder.Name = "pnlDevEnvDeplyFolder";
            this.pnlDevEnvDeplyFolder.Size = new System.Drawing.Size(995, 154);
            this.pnlDevEnvDeplyFolder.TabIndex = 76;
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label26.BackColor = System.Drawing.Color.Aqua;
            this.label26.Location = new System.Drawing.Point(0, 0);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(5, 154);
            this.label26.TabIndex = 64;
            // 
            // EditProjectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.pnlDevEnvDeplyFolder);
            this.Controls.Add(this.notificationsControl1);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.pnlDelEnvFields);
            this.Controls.Add(this.pnlDevEnvFoldersFields);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "EditProjectView";
            this.Size = new System.Drawing.Size(1037, 1992);
            ((System.ComponentModel.ISupportInitialize)(this.errPrvProjectDetails)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlDevEnvFoldersFields.ResumeLayout(false);
            this.pnlDevEnvFoldersFields.PerformLayout();
            this.pnlDelEnvFields.ResumeLayout(false);
            this.pnlDelEnvFields.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.pnlDevEnvDeplyFolder.ResumeLayout(false);
            this.pnlDevEnvDeplyFolder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ErrorProvider errPrvProjectDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnNavToProcess;
        private System.Windows.Forms.ComboBox cboConncectionType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbDevScriptsFolderPath;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbDBBackupFolder;
        private System.Windows.Forms.TextBox tbId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbDevEnv;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.RadioButton rbDelEnv;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel pnlDevEnvFoldersFields;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDeployArtifactFolderPath;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label lblIncrementalScriptsFolderPath;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel pnlDelEnvFields;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbDeliveryArtifactFolderPath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label imgValid;
        private System.Windows.Forms.Label imgError;
        private NotificationsView notificationsControl1;
        private System.Windows.Forms.Panel pnlDevEnvDeplyFolder;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lblDevDummyDataScriptsFolderPath;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label lblRepeatableScriptsFolderPath;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label lblDBProcess;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbProjectDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSaveId;
        private System.Windows.Forms.Button btnEditId;
        private System.Windows.Forms.Button btnCancelEditId;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbDBName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbConncetionTimeout;
        private System.Windows.Forms.Label label31;
    }
}
