namespace AutoVersionsDB.WinApp
{
    public partial class EditProjectConfigDetails
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
            this.label2 = new System.Windows.Forms.Label();
            this.tbProjectCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbConnStr = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbDBBackupFolder = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbDevScriptsFolderPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboConncectionType = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbConnStrToMasterDB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errPrvProjectDetails = new System.Windows.Forms.ErrorProvider(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.lbllncrementalScriptsFolderPath = new System.Windows.Forms.Label();
            this.lblRepeatableScriptsFolderPath = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lblDevDummyDataScriptsFolderPath = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbConnectionTimeout = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblDbProcess = new System.Windows.Forms.Label();
            this.imgValid = new System.Windows.Forms.Label();
            this.imgError = new System.Windows.Forms.Label();
            this.btnNavToProcess = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSaveProjectCode = new System.Windows.Forms.Button();
            this.btnEditProjectCode = new System.Windows.Forms.Button();
            this.tbProjectDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
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
            this.notificationsControl1 = new AutoVersionsDB.WinApp.NotificationsControl();
            this.pnlDevEnvDeplyFolder = new System.Windows.Forms.Panel();
            this.label26 = new System.Windows.Forms.Label();
            this.btnCancelEditProjectCode = new System.Windows.Forms.Button();
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
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(25, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 36);
            this.label2.TabIndex = 1;
            this.label2.Text = "Code:";
            // 
            // tbProjectCode
            // 
            this.tbProjectCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProjectCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbProjectCode.Location = new System.Drawing.Point(206, 26);
            this.tbProjectCode.Margin = new System.Windows.Forms.Padding(4);
            this.tbProjectCode.Name = "tbProjectCode";
            this.tbProjectCode.Size = new System.Drawing.Size(284, 30);
            this.tbProjectCode.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label5.Location = new System.Drawing.Point(20, 73);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(262, 36);
            this.label5.TabIndex = 12;
            this.label5.Text = "Connection String:";
            // 
            // tbConnStr
            // 
            this.tbConnStr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbConnStr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbConnStr.Location = new System.Drawing.Point(27, 118);
            this.tbConnStr.Margin = new System.Windows.Forms.Padding(4);
            this.tbConnStr.Multiline = true;
            this.tbConnStr.Name = "tbConnStr";
            this.tbConnStr.Size = new System.Drawing.Size(945, 66);
            this.tbConnStr.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label9.Location = new System.Drawing.Point(24, 11);
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
            this.tbDBBackupFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.tbDBBackupFolder.Location = new System.Drawing.Point(29, 50);
            this.tbDBBackupFolder.Margin = new System.Windows.Forms.Padding(4);
            this.tbDBBackupFolder.Name = "tbDBBackupFolder";
            this.tbDBBackupFolder.Size = new System.Drawing.Size(943, 34);
            this.tbDBBackupFolder.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label11.Location = new System.Drawing.Point(13, 17);
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
            this.tbDevScriptsFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.tbDevScriptsFolderPath.Location = new System.Drawing.Point(19, 57);
            this.tbDevScriptsFolderPath.Margin = new System.Windows.Forms.Padding(4);
            this.tbDevScriptsFolderPath.Name = "tbDevScriptsFolderPath";
            this.tbDevScriptsFolderPath.Size = new System.Drawing.Size(953, 34);
            this.tbDevScriptsFolderPath.TabIndex = 21;
            this.tbDevScriptsFolderPath.TextChanged += new System.EventHandler(this.TbScriptsRootFolderPath_TextChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label6.Location = new System.Drawing.Point(24, 12);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 36);
            this.label6.TabIndex = 31;
            this.label6.Text = "DB Type:";
            // 
            // cboConncectionType
            // 
            this.cboConncectionType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.cboConncectionType.FormattingEnabled = true;
            this.cboConncectionType.Location = new System.Drawing.Point(206, 14);
            this.cboConncectionType.Margin = new System.Windows.Forms.Padding(4);
            this.cboConncectionType.Name = "cboConncectionType";
            this.cboConncectionType.Size = new System.Drawing.Size(333, 37);
            this.cboConncectionType.TabIndex = 32;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label14.Location = new System.Drawing.Point(23, 203);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(452, 36);
            this.label14.TabIndex = 38;
            this.label14.Text = "Connection String To Master DB:";
            // 
            // tbConnStrToMasterDB
            // 
            this.tbConnStrToMasterDB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbConnStrToMasterDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbConnStrToMasterDB.Location = new System.Drawing.Point(29, 249);
            this.tbConnStrToMasterDB.Margin = new System.Windows.Forms.Padding(4);
            this.tbConnStrToMasterDB.Multiline = true;
            this.tbConnStrToMasterDB.Name = "tbConnStrToMasterDB";
            this.tbConnStrToMasterDB.Size = new System.Drawing.Size(945, 66);
            this.tbConnStrToMasterDB.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(25, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 52);
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
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label12.ForeColor = System.Drawing.Color.DimGray;
            this.errPrvProjectDetails.SetIconAlignment(this.label12, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.label12.Location = new System.Drawing.Point(19, 98);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(323, 25);
            this.label12.TabIndex = 23;
            this.label12.Text = "Incremental Scripts Folder Path:";
            // 
            // lbllncrementalScriptsFolderPath
            // 
            this.lbllncrementalScriptsFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbllncrementalScriptsFolderPath.AutoEllipsis = true;
            this.lbllncrementalScriptsFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbllncrementalScriptsFolderPath.ForeColor = System.Drawing.Color.DimGray;
            this.errPrvProjectDetails.SetIconAlignment(this.lbllncrementalScriptsFolderPath, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.lbllncrementalScriptsFolderPath.Location = new System.Drawing.Point(19, 128);
            this.lbllncrementalScriptsFolderPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbllncrementalScriptsFolderPath.Name = "lbllncrementalScriptsFolderPath";
            this.lbllncrementalScriptsFolderPath.Size = new System.Drawing.Size(915, 30);
            this.lbllncrementalScriptsFolderPath.TabIndex = 24;
            this.lbllncrementalScriptsFolderPath.Text = "C:\\Projects\\DBAutoVersions\\Code\\DBAutoVersions\\DBAutoVersions.BL.IntegrationTests" +
    "\\ScriptsFilesForTests\\StartState\\db_initState.sql";
            // 
            // lblRepeatableScriptsFolderPath
            // 
            this.lblRepeatableScriptsFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRepeatableScriptsFolderPath.AutoEllipsis = true;
            this.lblRepeatableScriptsFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblRepeatableScriptsFolderPath.ForeColor = System.Drawing.Color.DimGray;
            this.errPrvProjectDetails.SetIconAlignment(this.lblRepeatableScriptsFolderPath, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.lblRepeatableScriptsFolderPath.Location = new System.Drawing.Point(15, 187);
            this.lblRepeatableScriptsFolderPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRepeatableScriptsFolderPath.Name = "lblRepeatableScriptsFolderPath";
            this.lblRepeatableScriptsFolderPath.Size = new System.Drawing.Size(915, 30);
            this.lblRepeatableScriptsFolderPath.TabIndex = 66;
            this.lblRepeatableScriptsFolderPath.Text = "C:\\Projects\\DBAutoVersions\\Code\\DBAutoVersions\\DBAutoVersions.BL.IntegrationTests" +
    "\\ScriptsFilesForTests\\StartState\\db_initState.sql";
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label27.ForeColor = System.Drawing.Color.DimGray;
            this.errPrvProjectDetails.SetIconAlignment(this.label27, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.label27.Location = new System.Drawing.Point(15, 158);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(323, 25);
            this.label27.TabIndex = 65;
            this.label27.Text = "Repeatable Scripts Folder Path:";
            // 
            // lblDevDummyDataScriptsFolderPath
            // 
            this.lblDevDummyDataScriptsFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDevDummyDataScriptsFolderPath.AutoEllipsis = true;
            this.lblDevDummyDataScriptsFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDevDummyDataScriptsFolderPath.ForeColor = System.Drawing.Color.DimGray;
            this.errPrvProjectDetails.SetIconAlignment(this.lblDevDummyDataScriptsFolderPath, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.lblDevDummyDataScriptsFolderPath.Location = new System.Drawing.Point(13, 246);
            this.lblDevDummyDataScriptsFolderPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDevDummyDataScriptsFolderPath.Name = "lblDevDummyDataScriptsFolderPath";
            this.lblDevDummyDataScriptsFolderPath.Size = new System.Drawing.Size(915, 30);
            this.lblDevDummyDataScriptsFolderPath.TabIndex = 68;
            this.lblDevDummyDataScriptsFolderPath.Text = "C:\\Projects\\DBAutoVersions\\Code\\DBAutoVersions\\DBAutoVersions.BL.IntegrationTests" +
    "\\ScriptsFilesForTests\\StartState\\db_initState.sql";
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label29.ForeColor = System.Drawing.Color.DimGray;
            this.errPrvProjectDetails.SetIconAlignment(this.label29, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.label29.Location = new System.Drawing.Point(13, 217);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(404, 25);
            this.label29.TabIndex = 67;
            this.label29.Text = "Dev Dummy Data Scripts Folder Path:";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label13.Location = new System.Drawing.Point(580, 15);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(289, 36);
            this.label13.TabIndex = 34;
            this.label13.Text = "Connection Timeout:";
            // 
            // tbConnectionTimeout
            // 
            this.tbConnectionTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbConnectionTimeout.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbConnectionTimeout.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.tbConnectionTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.tbConnectionTimeout.Location = new System.Drawing.Point(877, 17);
            this.tbConnectionTimeout.Margin = new System.Windows.Forms.Padding(4);
            this.tbConnectionTimeout.Name = "tbConnectionTimeout";
            this.tbConnectionTimeout.Size = new System.Drawing.Size(93, 34);
            this.tbConnectionTimeout.TabIndex = 35;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label28);
            this.panel5.Controls.Add(this.btnSave);
            this.panel5.Controls.Add(this.lblDbProcess);
            this.panel5.Controls.Add(this.imgValid);
            this.panel5.Controls.Add(this.imgError);
            this.panel5.Controls.Add(this.btnNavToProcess);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel5.Location = new System.Drawing.Point(27, 15);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(995, 82);
            this.panel5.TabIndex = 64;
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(833, 62);
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
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.DarkRed;
            this.btnSave.Image = global::AutoVersionsDB.WinApp.Properties.Resources.SaveIcon;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(835, 15);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.btnSave.Size = new System.Drawing.Size(51, 49);
            this.btnSave.TabIndex = 88;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // lblDbProcess
            // 
            this.lblDbProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDbProcess.AutoSize = true;
            this.lblDbProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblDbProcess.Location = new System.Drawing.Point(907, 65);
            this.lblDbProcess.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDbProcess.Name = "lblDbProcess";
            this.lblDbProcess.Size = new System.Drawing.Size(71, 15);
            this.lblDbProcess.TabIndex = 87;
            this.lblDbProcess.Text = "DB Process";
            // 
            // imgValid
            // 
            this.imgValid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgValid.Image = global::AutoVersionsDB.WinApp.Properties.Resources.CheckedGreen32;
            this.imgValid.Location = new System.Drawing.Point(775, 16);
            this.imgValid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.imgValid.Name = "imgValid";
            this.imgValid.Size = new System.Drawing.Size(51, 49);
            this.imgValid.TabIndex = 72;
            // 
            // imgError
            // 
            this.imgError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgError.Image = global::AutoVersionsDB.WinApp.Properties.Resources.error2_32_32;
            this.imgError.Location = new System.Drawing.Point(716, 16);
            this.imgError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.imgError.Name = "imgError";
            this.imgError.Size = new System.Drawing.Size(51, 49);
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
            this.btnNavToProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnNavToProcess.ForeColor = System.Drawing.Color.DarkRed;
            this.btnNavToProcess.Image = global::AutoVersionsDB.WinApp.Properties.Resources.Play32;
            this.btnNavToProcess.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNavToProcess.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNavToProcess.Location = new System.Drawing.Point(921, 16);
            this.btnNavToProcess.Margin = new System.Windows.Forms.Padding(4);
            this.btnNavToProcess.Name = "btnNavToProcess";
            this.btnNavToProcess.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.btnNavToProcess.Size = new System.Drawing.Size(51, 49);
            this.btnNavToProcess.TabIndex = 68;
            this.btnNavToProcess.UseVisualStyleBackColor = false;
            this.btnNavToProcess.Click += new System.EventHandler(this.BtnNavToProcess_Click);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(5, 81);
            this.label15.TabIndex = 64;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnCancelEditProjectCode);
            this.panel1.Controls.Add(this.btnSaveProjectCode);
            this.panel1.Controls.Add(this.btnEditProjectCode);
            this.panel1.Controls.Add(this.tbProjectDescription);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.tbProjectCode);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(27, 201);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(995, 121);
            this.panel1.TabIndex = 70;
            // 
            // btnSaveProjectCode
            // 
            this.btnSaveProjectCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveProjectCode.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveProjectCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveProjectCode.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSaveProjectCode.FlatAppearance.BorderSize = 0;
            this.btnSaveProjectCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveProjectCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnSaveProjectCode.ForeColor = System.Drawing.Color.DarkRed;
            this.btnSaveProjectCode.Image = global::AutoVersionsDB.WinApp.Properties.Resources.SaveIcon;
            this.btnSaveProjectCode.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSaveProjectCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSaveProjectCode.Location = new System.Drawing.Point(557, 17);
            this.btnSaveProjectCode.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveProjectCode.Name = "btnSaveProjectCode";
            this.btnSaveProjectCode.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.btnSaveProjectCode.Size = new System.Drawing.Size(51, 49);
            this.btnSaveProjectCode.TabIndex = 90;
            this.btnSaveProjectCode.UseVisualStyleBackColor = false;
            this.btnSaveProjectCode.Click += new System.EventHandler(this.btnSaveProjectCode_Click);
            // 
            // btnEditProjectCode
            // 
            this.btnEditProjectCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditProjectCode.BackColor = System.Drawing.Color.Transparent;
            this.btnEditProjectCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditProjectCode.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnEditProjectCode.FlatAppearance.BorderSize = 0;
            this.btnEditProjectCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditProjectCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnEditProjectCode.ForeColor = System.Drawing.Color.DarkRed;
            this.btnEditProjectCode.Image = global::AutoVersionsDB.WinApp.Properties.Resources.EditIcon32;
            this.btnEditProjectCode.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEditProjectCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEditProjectCode.Location = new System.Drawing.Point(498, 17);
            this.btnEditProjectCode.Margin = new System.Windows.Forms.Padding(4);
            this.btnEditProjectCode.Name = "btnEditProjectCode";
            this.btnEditProjectCode.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.btnEditProjectCode.Size = new System.Drawing.Size(51, 49);
            this.btnEditProjectCode.TabIndex = 89;
            this.btnEditProjectCode.UseVisualStyleBackColor = false;
            this.btnEditProjectCode.Click += new System.EventHandler(this.btnEditProjectCode_Click);
            // 
            // tbProjectDescription
            // 
            this.tbProjectDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProjectDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbProjectDescription.Location = new System.Drawing.Point(206, 71);
            this.tbProjectDescription.Margin = new System.Windows.Forms.Padding(4);
            this.tbProjectDescription.Name = "tbProjectDescription";
            this.tbProjectDescription.Size = new System.Drawing.Size(728, 30);
            this.tbProjectDescription.TabIndex = 66;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(25, 66);
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
            this.label16.Size = new System.Drawing.Size(5, 121);
            this.label16.TabIndex = 64;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.tbConnStr);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.tbConnectionTimeout);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.tbConnStrToMasterDB);
            this.panel2.Controls.Add(this.cboConncectionType);
            this.panel2.Location = new System.Drawing.Point(27, 330);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(995, 356);
            this.panel2.TabIndex = 71;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.BackColor = System.Drawing.Color.Aqua;
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(5, 356);
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
            this.panel3.Location = new System.Drawing.Point(27, 814);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(995, 142);
            this.panel3.TabIndex = 71;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label20.Location = new System.Drawing.Point(499, 103);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(244, 18);
            this.label20.TabIndex = 73;
            this.label20.Text = "* Not allow drop and recreate the db";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label23.Location = new System.Drawing.Point(499, 81);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(186, 18);
            this.label23.TabIndex = 72;
            this.label23.Text = "* Not allow add scripts files";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label24.Location = new System.Drawing.Point(499, 59);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(218, 18);
            this.label24.TabIndex = 71;
            this.label24.Text = "* Work with artifact deployed file";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label22.Location = new System.Drawing.Point(52, 103);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(217, 18);
            this.label22.TabIndex = 70;
            this.label22.Text = "* Allow drop and recreate the db";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label21.Location = new System.Drawing.Point(52, 81);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(159, 18);
            this.label21.TabIndex = 69;
            this.label21.Text = "* Allow add scripts files";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label19.Location = new System.Drawing.Point(52, 59);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(163, 18);
            this.label19.TabIndex = 67;
            this.label19.Text = "* Work with scripts files";
            // 
            // rbDelEnv
            // 
            this.rbDelEnv.AutoSize = true;
            this.rbDelEnv.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.rbDelEnv.Location = new System.Drawing.Point(473, 15);
            this.rbDelEnv.Margin = new System.Windows.Forms.Padding(4);
            this.rbDelEnv.Name = "rbDelEnv";
            this.rbDelEnv.Size = new System.Drawing.Size(301, 40);
            this.rbDelEnv.TabIndex = 66;
            this.rbDelEnv.TabStop = true;
            this.rbDelEnv.Text = "Delivery Enviroment";
            this.rbDelEnv.UseVisualStyleBackColor = true;
            this.rbDelEnv.CheckedChanged += new System.EventHandler(this.RbDelEnv_CheckedChanged);
            // 
            // rbDevEnv
            // 
            this.rbDevEnv.AutoSize = true;
            this.rbDevEnv.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.rbDevEnv.Location = new System.Drawing.Point(27, 15);
            this.rbDevEnv.Margin = new System.Windows.Forms.Padding(4);
            this.rbDevEnv.Name = "rbDevEnv";
            this.rbDevEnv.Size = new System.Drawing.Size(367, 40);
            this.rbDevEnv.TabIndex = 65;
            this.rbDevEnv.TabStop = true;
            this.rbDevEnv.Text = "Development Enviroment";
            this.rbDevEnv.UseVisualStyleBackColor = true;
            this.rbDevEnv.CheckedChanged += new System.EventHandler(this.RbDevEnv_CheckedChanged);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label18.BackColor = System.Drawing.Color.Aqua;
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(5, 142);
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
            this.pnlDevEnvFoldersFields.Controls.Add(this.lbllncrementalScriptsFolderPath);
            this.pnlDevEnvFoldersFields.Controls.Add(this.label12);
            this.pnlDevEnvFoldersFields.Location = new System.Drawing.Point(27, 964);
            this.pnlDevEnvFoldersFields.Margin = new System.Windows.Forms.Padding(4);
            this.pnlDevEnvFoldersFields.Name = "pnlDevEnvFoldersFields";
            this.pnlDevEnvFoldersFields.Size = new System.Drawing.Size(995, 296);
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
            this.label30.Size = new System.Drawing.Size(5, 295);
            this.label30.TabIndex = 64;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label4.Location = new System.Drawing.Point(13, 26);
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
            this.tbDeployArtifactFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.tbDeployArtifactFolderPath.Location = new System.Drawing.Point(19, 65);
            this.tbDeployArtifactFolderPath.Margin = new System.Windows.Forms.Padding(4);
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
            this.pnlDelEnvFields.Location = new System.Drawing.Point(27, 1399);
            this.pnlDelEnvFields.Margin = new System.Windows.Forms.Padding(4);
            this.pnlDelEnvFields.Name = "pnlDelEnvFields";
            this.pnlDelEnvFields.Size = new System.Drawing.Size(995, 124);
            this.pnlDelEnvFields.TabIndex = 75;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label7.Location = new System.Drawing.Point(13, 21);
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
            this.tbDeliveryArtifactFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.tbDeliveryArtifactFolderPath.Location = new System.Drawing.Point(19, 60);
            this.tbDeliveryArtifactFolderPath.Margin = new System.Windows.Forms.Padding(4);
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
            this.label8.Size = new System.Drawing.Size(5, 123);
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
            this.panel7.Location = new System.Drawing.Point(27, 694);
            this.panel7.Margin = new System.Windows.Forms.Padding(4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(995, 112);
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
            this.label25.Size = new System.Drawing.Size(5, 111);
            this.label25.TabIndex = 64;
            // 
            // notificationsControl1
            // 
            this.notificationsControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notificationsControl1.Location = new System.Drawing.Point(27, 105);
            this.notificationsControl1.Margin = new System.Windows.Forms.Padding(5);
            this.notificationsControl1.Name = "notificationsControl1";
            this.notificationsControl1.Size = new System.Drawing.Size(996, 65);
            this.notificationsControl1.TabIndex = 77;
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
            this.pnlDevEnvDeplyFolder.Location = new System.Drawing.Point(27, 1268);
            this.pnlDevEnvDeplyFolder.Margin = new System.Windows.Forms.Padding(4);
            this.pnlDevEnvDeplyFolder.Name = "pnlDevEnvDeplyFolder";
            this.pnlDevEnvDeplyFolder.Size = new System.Drawing.Size(995, 124);
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
            this.label26.Size = new System.Drawing.Size(5, 123);
            this.label26.TabIndex = 64;
            // 
            // btnCancelEditProjectCode
            // 
            this.btnCancelEditProjectCode.BackColor = System.Drawing.Color.Transparent;
            this.btnCancelEditProjectCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelEditProjectCode.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancelEditProjectCode.FlatAppearance.BorderSize = 0;
            this.btnCancelEditProjectCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelEditProjectCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancelEditProjectCode.ForeColor = System.Drawing.Color.Gray;
            this.btnCancelEditProjectCode.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancelEditProjectCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancelEditProjectCode.Location = new System.Drawing.Point(162, 23);
            this.btnCancelEditProjectCode.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelEditProjectCode.Name = "btnCancelEditProjectCode";
            this.btnCancelEditProjectCode.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.btnCancelEditProjectCode.Size = new System.Drawing.Size(36, 39);
            this.btnCancelEditProjectCode.TabIndex = 91;
            this.btnCancelEditProjectCode.Text = "X";
            this.btnCancelEditProjectCode.UseVisualStyleBackColor = false;
            this.btnCancelEditProjectCode.Click += new System.EventHandler(this.btnCancelEditProjectCode_Click);
            // 
            // EditProjectConfigDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EditProjectConfigDetails";
            this.Size = new System.Drawing.Size(1037, 1569);
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
        private System.Windows.Forms.TextBox tbConnectionTimeout;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnNavToProcess;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbConnStrToMasterDB;
        private System.Windows.Forms.ComboBox cboConncectionType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbDevScriptsFolderPath;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbDBBackupFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbConnStr;
        private System.Windows.Forms.TextBox tbProjectCode;
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
        private System.Windows.Forms.Label lbllncrementalScriptsFolderPath;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel pnlDelEnvFields;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbDeliveryArtifactFolderPath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label imgValid;
        private System.Windows.Forms.Label imgError;
        private NotificationsControl notificationsControl1;
        private System.Windows.Forms.Panel pnlDevEnvDeplyFolder;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lblDevDummyDataScriptsFolderPath;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label lblRepeatableScriptsFolderPath;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label lblDbProcess;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbProjectDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSaveProjectCode;
        private System.Windows.Forms.Button btnEditProjectCode;
        private System.Windows.Forms.Button btnCancelEditProjectCode;
    }
}
