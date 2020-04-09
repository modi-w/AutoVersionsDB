namespace AutoVersionsDB.WinApp
{
    public partial class EditProjectConfigDetails
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
            this.components = new System.ComponentModel.Container();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbProjectName = new System.Windows.Forms.TextBox();
            this.lblProjectGuid = new System.Windows.Forms.Label();
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
            this.btnSave = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.tbConnectionTimeout = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.imgValid = new System.Windows.Forms.Label();
            this.imgError = new System.Windows.Forms.Label();
            this.btnNavToProcess = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
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
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.errPrvProjectDetails.SetIconAlignment(this.label3, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.label3.Location = new System.Drawing.Point(19, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Project Guid";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(19, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Project Name:";
            // 
            // tbProjectName
            // 
            this.tbProjectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbProjectName.Location = new System.Drawing.Point(191, 20);
            this.tbProjectName.Name = "tbProjectName";
            this.tbProjectName.Size = new System.Drawing.Size(539, 26);
            this.tbProjectName.TabIndex = 2;
            // 
            // lblProjectGuid
            // 
            this.lblProjectGuid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProjectGuid.AutoSize = true;
            this.lblProjectGuid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblProjectGuid.ForeColor = System.Drawing.Color.DimGray;
            this.lblProjectGuid.Location = new System.Drawing.Point(187, 59);
            this.lblProjectGuid.Name = "lblProjectGuid";
            this.lblProjectGuid.Size = new System.Drawing.Size(168, 20);
            this.lblProjectGuid.TabIndex = 4;
            this.lblProjectGuid.Text = "1234-1234-1234-1234";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label5.Location = new System.Drawing.Point(15, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(210, 29);
            this.label5.TabIndex = 12;
            this.label5.Text = "Connection String:";
            // 
            // tbConnStr
            // 
            this.tbConnStr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbConnStr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbConnStr.Location = new System.Drawing.Point(20, 96);
            this.tbConnStr.Multiline = true;
            this.tbConnStr.Name = "tbConnStr";
            this.tbConnStr.Size = new System.Drawing.Size(710, 54);
            this.tbConnStr.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label9.Location = new System.Drawing.Point(18, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(215, 29);
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
            this.tbDBBackupFolder.Location = new System.Drawing.Point(22, 41);
            this.tbDBBackupFolder.Name = "tbDBBackupFolder";
            this.tbDBBackupFolder.Size = new System.Drawing.Size(708, 29);
            this.tbDBBackupFolder.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label11.Location = new System.Drawing.Point(10, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(224, 29);
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
            this.tbDevScriptsFolderPath.Location = new System.Drawing.Point(14, 46);
            this.tbDevScriptsFolderPath.Name = "tbDevScriptsFolderPath";
            this.tbDevScriptsFolderPath.Size = new System.Drawing.Size(716, 29);
            this.tbDevScriptsFolderPath.TabIndex = 21;
            this.tbDevScriptsFolderPath.TextChanged += new System.EventHandler(this.tbScriptsRootFolderPath_TextChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label6.Location = new System.Drawing.Point(18, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 29);
            this.label6.TabIndex = 31;
            this.label6.Text = "DB Type:";
            // 
            // cboConncectionType
            // 
            this.cboConncectionType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.cboConncectionType.FormattingEnabled = true;
            this.cboConncectionType.Location = new System.Drawing.Point(137, 11);
            this.cboConncectionType.Name = "cboConncectionType";
            this.cboConncectionType.Size = new System.Drawing.Size(268, 32);
            this.cboConncectionType.TabIndex = 32;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label14.Location = new System.Drawing.Point(17, 165);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(364, 29);
            this.label14.TabIndex = 38;
            this.label14.Text = "Connection String To Master DB:";
            // 
            // tbConnStrToMasterDB
            // 
            this.tbConnStrToMasterDB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbConnStrToMasterDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbConnStrToMasterDB.Location = new System.Drawing.Point(22, 202);
            this.tbConnStrToMasterDB.Multiline = true;
            this.tbConnStrToMasterDB.Name = "tbConnStrToMasterDB";
            this.tbConnStrToMasterDB.Size = new System.Drawing.Size(710, 54);
            this.tbConnStrToMasterDB.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(19, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 39);
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
            this.label12.Location = new System.Drawing.Point(14, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(242, 20);
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
            this.lbllncrementalScriptsFolderPath.Location = new System.Drawing.Point(14, 104);
            this.lbllncrementalScriptsFolderPath.Name = "lbllncrementalScriptsFolderPath";
            this.lbllncrementalScriptsFolderPath.Size = new System.Drawing.Size(686, 24);
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
            this.lblRepeatableScriptsFolderPath.Location = new System.Drawing.Point(11, 152);
            this.lblRepeatableScriptsFolderPath.Name = "lblRepeatableScriptsFolderPath";
            this.lblRepeatableScriptsFolderPath.Size = new System.Drawing.Size(686, 24);
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
            this.label27.Location = new System.Drawing.Point(11, 128);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(242, 20);
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
            this.lblDevDummyDataScriptsFolderPath.Location = new System.Drawing.Point(10, 200);
            this.lblDevDummyDataScriptsFolderPath.Name = "lblDevDummyDataScriptsFolderPath";
            this.lblDevDummyDataScriptsFolderPath.Size = new System.Drawing.Size(686, 24);
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
            this.label29.Location = new System.Drawing.Point(10, 176);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(303, 20);
            this.label29.TabIndex = 67;
            this.label29.Text = "Dev Dummy Data Scripts Folder Path:";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.btnSave.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnSave.Location = new System.Drawing.Point(547, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnSave.Size = new System.Drawing.Size(138, 44);
            this.btnSave.TabIndex = 29;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label13.Location = new System.Drawing.Point(416, 14);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(236, 29);
            this.label13.TabIndex = 34;
            this.label13.Text = "Connection Timeout:";
            // 
            // tbConnectionTimeout
            // 
            this.tbConnectionTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbConnectionTimeout.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbConnectionTimeout.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.tbConnectionTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.tbConnectionTimeout.Location = new System.Drawing.Point(658, 14);
            this.tbConnectionTimeout.Name = "tbConnectionTimeout";
            this.tbConnectionTimeout.Size = new System.Drawing.Size(71, 29);
            this.tbConnectionTimeout.TabIndex = 35;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.imgValid);
            this.panel5.Controls.Add(this.imgError);
            this.panel5.Controls.Add(this.btnNavToProcess);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Controls.Add(this.btnSave);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel5.Location = new System.Drawing.Point(20, 12);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(747, 67);
            this.panel5.TabIndex = 64;
            // 
            // imgValid
            // 
            this.imgValid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgValid.Image = global::AutoVersionsDB.WinApp.Properties.Resources.CheckedGreen32;
            this.imgValid.Location = new System.Drawing.Point(503, 15);
            this.imgValid.Name = "imgValid";
            this.imgValid.Size = new System.Drawing.Size(38, 40);
            this.imgValid.TabIndex = 72;
            // 
            // imgError
            // 
            this.imgError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgError.Image = global::AutoVersionsDB.WinApp.Properties.Resources.error2_32_32;
            this.imgError.Location = new System.Drawing.Point(459, 15);
            this.imgError.Name = "imgError";
            this.imgError.Size = new System.Drawing.Size(38, 40);
            this.imgError.TabIndex = 71;
            // 
            // btnNavToProcess
            // 
            this.btnNavToProcess.BackColor = System.Drawing.Color.Transparent;
            this.btnNavToProcess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavToProcess.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnNavToProcess.FlatAppearance.BorderSize = 0;
            this.btnNavToProcess.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNavToProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnNavToProcess.ForeColor = System.Drawing.Color.DarkRed;
            this.btnNavToProcess.Image = global::AutoVersionsDB.WinApp.Properties.Resources.Play32;
            this.btnNavToProcess.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNavToProcess.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNavToProcess.Location = new System.Drawing.Point(691, 15);
            this.btnNavToProcess.Name = "btnNavToProcess";
            this.btnNavToProcess.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnNavToProcess.Size = new System.Drawing.Size(38, 40);
            this.btnNavToProcess.TabIndex = 68;
            this.btnNavToProcess.UseVisualStyleBackColor = false;
            this.btnNavToProcess.Click += new System.EventHandler(this.btnNavToProcess_Click);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(4, 66);
            this.label15.TabIndex = 64;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.tbProjectName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblProjectGuid);
            this.panel1.Location = new System.Drawing.Point(20, 163);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(747, 99);
            this.panel1.TabIndex = 70;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.BackColor = System.Drawing.Color.Aqua;
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(4, 98);
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
            this.panel2.Location = new System.Drawing.Point(20, 268);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(747, 290);
            this.panel2.TabIndex = 71;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.BackColor = System.Drawing.Color.Aqua;
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(4, 289);
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
            this.panel3.Location = new System.Drawing.Point(20, 661);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(747, 116);
            this.panel3.TabIndex = 71;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label20.Location = new System.Drawing.Point(374, 84);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(203, 15);
            this.label20.TabIndex = 73;
            this.label20.Text = "* Not allow drop and recreate the db";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label23.Location = new System.Drawing.Point(374, 66);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(153, 15);
            this.label23.TabIndex = 72;
            this.label23.Text = "* Not allow add scripts files";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label24.Location = new System.Drawing.Point(374, 48);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(179, 15);
            this.label24.TabIndex = 71;
            this.label24.Text = "* Work with artifact deployed file";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label22.Location = new System.Drawing.Point(39, 84);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(181, 15);
            this.label22.TabIndex = 70;
            this.label22.Text = "* Allow drop and recreate the db";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label21.Location = new System.Drawing.Point(39, 66);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(131, 15);
            this.label21.TabIndex = 69;
            this.label21.Text = "* Allow add scripts files";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label19.Location = new System.Drawing.Point(39, 48);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(131, 15);
            this.label19.TabIndex = 67;
            this.label19.Text = "* Work with scripts files";
            // 
            // rbDelEnv
            // 
            this.rbDelEnv.AutoSize = true;
            this.rbDelEnv.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.rbDelEnv.Location = new System.Drawing.Point(355, 12);
            this.rbDelEnv.Name = "rbDelEnv";
            this.rbDelEnv.Size = new System.Drawing.Size(245, 33);
            this.rbDelEnv.TabIndex = 66;
            this.rbDelEnv.TabStop = true;
            this.rbDelEnv.Text = "Delivery Enviroment";
            this.rbDelEnv.UseVisualStyleBackColor = true;
            this.rbDelEnv.CheckedChanged += new System.EventHandler(this.rbDelEnv_CheckedChanged);
            // 
            // rbDevEnv
            // 
            this.rbDevEnv.AutoSize = true;
            this.rbDevEnv.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.rbDevEnv.Location = new System.Drawing.Point(20, 12);
            this.rbDevEnv.Name = "rbDevEnv";
            this.rbDevEnv.Size = new System.Drawing.Size(301, 33);
            this.rbDevEnv.TabIndex = 65;
            this.rbDevEnv.TabStop = true;
            this.rbDevEnv.Text = "Development Enviroment";
            this.rbDevEnv.UseVisualStyleBackColor = true;
            this.rbDevEnv.CheckedChanged += new System.EventHandler(this.rbDevEnv_CheckedChanged);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label18.BackColor = System.Drawing.Color.Aqua;
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(4, 115);
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
            this.pnlDevEnvFoldersFields.Location = new System.Drawing.Point(20, 783);
            this.pnlDevEnvFoldersFields.Name = "pnlDevEnvFoldersFields";
            this.pnlDevEnvFoldersFields.Size = new System.Drawing.Size(747, 241);
            this.pnlDevEnvFoldersFields.TabIndex = 74;
            // 
            // label30
            // 
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label30.BackColor = System.Drawing.Color.Aqua;
            this.label30.Location = new System.Drawing.Point(0, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(4, 240);
            this.label30.TabIndex = 64;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label4.Location = new System.Drawing.Point(10, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(304, 29);
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
            this.tbDeployArtifactFolderPath.Location = new System.Drawing.Point(14, 53);
            this.tbDeployArtifactFolderPath.Name = "tbDeployArtifactFolderPath";
            this.tbDeployArtifactFolderPath.Size = new System.Drawing.Size(716, 29);
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
            this.pnlDelEnvFields.Location = new System.Drawing.Point(20, 1137);
            this.pnlDelEnvFields.Name = "pnlDelEnvFields";
            this.pnlDelEnvFields.Size = new System.Drawing.Size(747, 101);
            this.pnlDelEnvFields.TabIndex = 75;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label7.Location = new System.Drawing.Point(10, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(315, 29);
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
            this.tbDeliveryArtifactFolderPath.Location = new System.Drawing.Point(14, 49);
            this.tbDeliveryArtifactFolderPath.Name = "tbDeliveryArtifactFolderPath";
            this.tbDeliveryArtifactFolderPath.Size = new System.Drawing.Size(716, 29);
            this.tbDeliveryArtifactFolderPath.TabIndex = 65;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.BackColor = System.Drawing.Color.Aqua;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(4, 100);
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
            this.panel7.Location = new System.Drawing.Point(20, 564);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(747, 91);
            this.panel7.TabIndex = 76;
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label25.BackColor = System.Drawing.Color.Aqua;
            this.label25.Location = new System.Drawing.Point(0, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(4, 90);
            this.label25.TabIndex = 64;
            // 
            // notificationsControl1
            // 
            this.notificationsControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notificationsControl1.Location = new System.Drawing.Point(20, 85);
            this.notificationsControl1.Name = "notificationsControl1";
            this.notificationsControl1.Size = new System.Drawing.Size(747, 53);
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
            this.pnlDevEnvDeplyFolder.Location = new System.Drawing.Point(20, 1030);
            this.pnlDevEnvDeplyFolder.Name = "pnlDevEnvDeplyFolder";
            this.pnlDevEnvDeplyFolder.Size = new System.Drawing.Size(747, 101);
            this.pnlDevEnvDeplyFolder.TabIndex = 76;
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label26.BackColor = System.Drawing.Color.Aqua;
            this.label26.Location = new System.Drawing.Point(0, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(4, 100);
            this.label26.TabIndex = 64;
            // 
            // EditProjectConfigDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
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
            this.Name = "EditProjectConfigDetails";
            this.Size = new System.Drawing.Size(778, 1275);
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
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnNavToProcess;
        private System.Windows.Forms.Label label3;
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
        private System.Windows.Forms.Label lblProjectGuid;
        private System.Windows.Forms.TextBox tbProjectName;
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
    }
}
