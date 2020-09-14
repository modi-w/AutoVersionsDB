using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.ScriptFiles.Incremental;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.WinApp.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp
{

    public partial class DBProcessControl : UserControl
    {
        private ProjectConfigItem _projectConfigItem;
        private ScriptFilesState _scriptFilesState;

        public enum DBVersionsMangementViewType
        {
            ReadyToRunSync,
            ReadyToSyncToSpecificState,
            MissingSystemTables,
            HistoryExecutedFilesChanged,
            SetDBStateManually,
            InProcess,
            RestoreDatabaseError
        }



        public event OnEditProjectHandler OnEditProject;

        public string TargetStateScriptFileName { get; private set; }


        public DBProcessControl()
        {
            InitializeComponent();

            dgIncrementalScriptsFiles.AutoGenerateColumns = false;
            dgIncrementalScriptsFiles.SelectionChanged += DgIncrementalScriptsFiles_SelectionChanged;

            dgRepeatableScriptsFiles.AutoGenerateColumns = false;
            dgRepeatableScriptsFiles.SelectionChanged += DgRepeatableScriptsFiles_SelectionChanged;

            dgDevDummyDataScriptsFiles.AutoGenerateColumns = false;
            dgDevDummyDataScriptsFiles.SelectionChanged += DgDevDummyDataScriptsFiles_SelectionChanged;


            ChangeButtonsPanelsLocation(pnlMainActions);
            ChangeButtonsPanelsLocation(pnlMissingSystemTables);
            ChangeButtonsPanelsLocation(pnlRestoreDbError);
            ChangeButtonsPanelsLocation(pnlSyncToSpecificState);
            ChangeButtonsPanelsLocation(pnlSetDBStateManually);

            this.Controls.Remove(pnlActionButtons);

            //pnlSyncToSpecificState.Location = new Point(873, 14);
            //pnlMissingSystemTables.Location = new Point(600, 10);
            //pnlSetDBStateManually.Location = new Point(880, 24);
            //btnShowHistoricalBackups.Location = new Point(880, 24);


            //#if !DEBUG
            btnSetDBToSpecificState.Visible = false;
            lblSetDBToSpecificState.Visible = false;
            //#endif

            SetToolTips();
        }

        private void ChangeButtonsPanelsLocation(Panel panelToMove)
        {
            pnlActionButtons.Controls.Remove(panelToMove);
            pnlHeader.Controls.Add(panelToMove);
            panelToMove.Location = new Point(pnlHeader.Width - panelToMove.Width, 0);
        }

        private void SetToolTips()
        {
            using (ToolTip tooltipControl = new ToolTip
            {
                // Set up the delays for the ToolTip.
                AutoPopDelay = 5000,
                InitialDelay = 500,
                ReshowDelay = 500,
                // Force the ToolTip text to be displayed whether or not the form is active.
                ShowAlways = true
            })
            {
                // Set up the ToolTip text with the controls.
                tooltipControl.SetToolTip(this.btnRefresh, "Refresh");
                tooltipControl.SetToolTip(this.btnRunSync, "Sync the db with the missing scripts");
                tooltipControl.SetToolTip(this.btnRecreateDbFromScratchMain, "Recreate DB From Scratch");
                tooltipControl.SetToolTip(this.btnRecreateDbFromScratch2, "Recreate DB From Scratch");
                tooltipControl.SetToolTip(this.btnDeploy, "Create Deploy Package");
                tooltipControl.SetToolTip(this.btnSetDBToSpecificState, "Set DB To Specific State");
                tooltipControl.SetToolTip(this.btnVirtualExecution, "Set DB to specific state virtually. Use it if your DB is not empty but you never use our migration tool on it yet.");
                tooltipControl.SetToolTip(this.btnShowHistoricalBackups, "Open the backup history folder.");
            }

        }




        #region Refresh

        public void SetProjectConfigItem(string projectCode)
        {
            _projectConfigItem = AutoVersionsDbAPI.GetProjectConfigByProjectCode(projectCode);

            RefreshAll();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void RefreshAll()
        {
            TargetStateScriptFileName = null;

            pnlDevDummyDataFiles.BeginInvoke((MethodInvoker)(() =>
            {
                pnlDevDummyDataFiles.Visible = _projectConfigItem.IsDevEnvironment;
            }));


            HideAllActionPanels();

            notificationsControl1.PreparingMesage();



            Task.Run(() =>
            {
                notificationsControl1.BeforeStart();


                //      bindToUIElements();

                try
                {
                    ProcessTrace processResults = AutoVersionsDbAPI.ValidateAll(_projectConfigItem.ProjectCode, notificationsControl1.OnNotificationStateChanged);

                    RefreshScriptFilesState();

                    if (processResults.HasError)
                    {
                        notificationsControl1.AfterComplete();

                        if (processResults.ErrorCode == "SystemTables")
                        {
                            SetViewState(DBVersionsMangementViewType.MissingSystemTables);
                        }
                        else if (processResults.ErrorCode == "IsHistoryExecutedFilesChanged")
                        {
                            SetViewState(DBVersionsMangementViewType.HistoryExecutedFilesChanged);
                        }
                        else
                        {
                            SetViewState(DBVersionsMangementViewType.ReadyToRunSync);
                        }
                    }
                    else
                    {

                        notificationsControl1.Clear();
                        SetViewState(DBVersionsMangementViewType.ReadyToRunSync);
                    }

                }
                catch (Exception ex)
                {
                    //LogManagerObj.AddExceptionMessageToLog(ex, "refreshAll()");
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            });
        }

        #endregion


        #region UI Commands

        private void DgIncrementalScriptsFiles_SelectionChanged(object sender, EventArgs e)
        {
            dgIncrementalScriptsFiles.ClearSelection();
        }
        private void DgRepeatableScriptsFiles_SelectionChanged(object sender, EventArgs e)
        {
            dgRepeatableScriptsFiles.ClearSelection();
        }
        private void DgDevDummyDataScriptsFiles_SelectionChanged(object sender, EventArgs e)
        {
            dgDevDummyDataScriptsFiles.ClearSelection();
        }


        private void BtnShowHistoricalBackups_Click(object sender, EventArgs e)
        {
            OsProcessUtils.StartOsProcess(_projectConfigItem.DBBackupBaseFolder);
        }

        private void BtnCancelSyncSpecificState_Click(object sender, EventArgs e)
        {
            notificationsControl1.Clear();
            SetViewState(DBVersionsMangementViewType.ReadyToRunSync);
        }

        private void BtnSetDBToSpecificState_Click(object sender, EventArgs e)
        {
            SetViewState(DBVersionsMangementViewType.ReadyToSyncToSpecificState);
        }



        private void BtnNavToEdit_Click(object sender, EventArgs e)
        {
            OnEditProject?.Invoke(_projectConfigItem.ProjectCode);
        }


        private void DgScriptFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                RuntimeScriptFileBase currScriptFileInfo = (dgIncrementalScriptsFiles.DataSource as List<RuntimeScriptFileBase>)[e.RowIndex];

                TargetStateScriptFileName = currScriptFileInfo.Filename;

                MarkUnMarkSelectedTargetInGrid();
            }

        }

        private void BtnCancelSetDBStateManually_Click(object sender, EventArgs e)
        {
            SetViewState(DBVersionsMangementViewType.MissingSystemTables);
        }


        private void BtnOpenIncrementalScriptsFolder_Click(object sender, EventArgs e)
        {
            OsProcessUtils.StartOsProcess(_projectConfigItem.IncrementalScriptsFolderPath);
        }
        private void BtnCreateNewIncrementalScriptFile_Click(object sender, EventArgs e)
        {
            using (TextInputWindow textInputWindow = new TextInputWindow("Create new script script file, insert the script name:"))
            {
                textInputWindow.ShowDialog();

                if (textInputWindow.IsApply)
                {
                    string newFileFullPath = AutoVersionsDbAPI.CreateNewIncrementalScriptFile(_projectConfigItem, textInputWindow.ResultText);

                    RefreshAll();

                    OsProcessUtils.StartOsProcess(newFileFullPath);
                }
            }

        }

        private void BtnOpenRepeatableScriptsFolder_Click(object sender, EventArgs e)
        {
            OsProcessUtils.StartOsProcess(_projectConfigItem.RepeatableScriptsFolderPath);
        }
        private void BtnCreateNewRepeatableScriptFile_Click(object sender, EventArgs e)
        {
            using (TextInputWindow textInputWindow = new TextInputWindow("Create new script script file, insert the script name:"))
            {
                textInputWindow.ShowDialog();

                if (textInputWindow.IsApply)
                {
                    string newFileFullPath = AutoVersionsDbAPI.CreateNewRepeatableScriptFile(_projectConfigItem, textInputWindow.ResultText);
                    RefreshAll();
                    OsProcessUtils.StartOsProcess(newFileFullPath);
                }
            }

        }


        private void BtnOpenDevDummyDataScriptsFolder_Click(object sender, EventArgs e)
        {
            OsProcessUtils.StartOsProcess(_projectConfigItem.DevDummyDataScriptsFolderPath);
        }
        private void BtnCreateNewDevDummyDataScriptFile_Click(object sender, EventArgs e)
        {
            using (TextInputWindow textInputWindow = new TextInputWindow("Create new script script file, insert the script name:"))
            {
                textInputWindow.ShowDialog();

                if (textInputWindow.IsApply)
                {
                    string newFileFullPath = AutoVersionsDbAPI.CreateNewDevDummyDataScriptFile(_projectConfigItem, textInputWindow.ResultText);
                    RefreshAll();
                    OsProcessUtils.StartOsProcess(newFileFullPath);
                }
            }

        }
        #endregion



        #region Run Process

        private void BtnRunSync_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    SetViewState(DBVersionsMangementViewType.InProcess);
                    notificationsControl1.BeforeStart();

                    ProcessTrace processResults = AutoVersionsDbAPI.SyncDB(_projectConfigItem.ProjectCode, notificationsControl1.OnNotificationStateChanged);

                    RefreshScriptFilesState();

                    notificationsControl1.AfterComplete();
                    SetViewState_AfterProcessComplete(processResults);
                }
                catch (Exception ex)
                {
                    //LogManagerObj.AddExceptionMessageToLog(ex, "btnRunSync_Click()");
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            });
        }

        private void RefreshScriptFilesState() => _scriptFilesState = AutoVersionsDbAPI.CreateScriptFilesState(_projectConfigItem);

        private bool CheckIsTargetStateHistory()
        {
            bool isAllowRun = true;

            if (!AutoVersionsDbAPI.ValdiateTargetStateAlreadyExecuted(_projectConfigItem.ProjectCode, TargetStateScriptFileName, notificationsControl1.OnNotificationStateChanged))
            {
                string warningMessage = $"This action will drop the Database and recreate it only by the scripts, you may lose Data. Are you sure?";
                isAllowRun = MessageBox.Show(this, warningMessage, "Pay Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes;
            }

            return isAllowRun;
        }

        private void BtnRecreateDbFromScratchMain_Click(object sender, EventArgs e)
        {
            RunRecreateDBFromScratch();
        }


        private void BtnApplySyncSpecificState_Click(object sender, EventArgs e)
        {
            if (CheckIsTargetStateHistory())
            {
                Task.Run(() =>
                {
                    try
                    {
                        SetViewState(DBVersionsMangementViewType.InProcess);
                        notificationsControl1.BeforeStart();

                        ProcessTrace processResults = AutoVersionsDbAPI.SetDBToSpecificState(_projectConfigItem.ProjectCode, TargetStateScriptFileName, true, notificationsControl1.OnNotificationStateChanged);
                        RefreshScriptFilesState();

                        notificationsControl1.AfterComplete();
                        SetViewState_AfterProcessComplete(processResults);

                    }
                    catch (Exception ex)
                    {
                        //LogManagerObj.AddExceptionMessageToLog(ex, "btnApplySyncSpecificState_Click()");
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                });
            }
        }


        private void BtnDeploy_Click(object sender, EventArgs e)
        {
            try
            {
                SetViewState(DBVersionsMangementViewType.InProcess);
                notificationsControl1.BeforeStart();

                ProcessTrace processResults = AutoVersionsDbAPI.Deploy(_projectConfigItem.ProjectCode, notificationsControl1.OnNotificationStateChanged);
                RefreshScriptFilesState();

                notificationsControl1.AfterComplete();
                SetViewState_AfterProcessComplete(processResults);

                OsProcessUtils.StartOsProcess(_projectConfigItem.DeployArtifactFolderPath);
            }
            catch (Exception ex)
            {
                //LogManagerObj.AddExceptionMessageToLog(ex, "deployToolStripMenuItem_Click()");
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVirtualExecution_Click(object sender, EventArgs e)
        {
            SetViewState(DBVersionsMangementViewType.SetDBStateManually);
        }

        private void BtnRecreateDbFromScratch2_Click(object sender, EventArgs e)
        {
            RunRecreateDBFromScratch();
        }

        private void RunRecreateDBFromScratch()
        {
            string warningMessage = $"This action will drop the Database and recreate it only by the scripts, you may loose Data. Are you sure?";
            if (MessageBox.Show(this, warningMessage, "Pay Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                Task.Run(() =>
                {
                    try
                    {
                        SetViewState(DBVersionsMangementViewType.InProcess);
                        notificationsControl1.BeforeStart();

                        ProcessTrace processResults = AutoVersionsDbAPI.RecreateDBFromScratch(_projectConfigItem.ProjectCode, TargetStateScriptFileName, notificationsControl1.OnNotificationStateChanged);
                        RefreshScriptFilesState();

                        notificationsControl1.AfterComplete();
                        SetViewState_AfterProcessComplete(processResults);
                    }
                    catch (Exception ex)
                    {
                        //LogManagerObj.AddExceptionMessageToLog(ex, "runRecreateDBFromScratch()");
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                });
            }
        }


        private void BtnRunSetDBStateManally_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    SetViewState(DBVersionsMangementViewType.InProcess);
                    notificationsControl1.BeforeStart();

                    ProcessTrace processResults = AutoVersionsDbAPI.SetDBStateByVirtualExecution(_projectConfigItem.ProjectCode, TargetStateScriptFileName, notificationsControl1.OnNotificationStateChanged);
                    RefreshScriptFilesState();

                    notificationsControl1.AfterComplete();
                    SetViewState_AfterProcessComplete(processResults);
                }
                catch (Exception ex)
                {
                    //LogManagerObj.AddExceptionMessageToLog(ex, "btnRunSetDBStateManally_Click()");
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            });
        }



        #endregion




        #region Set View State

        private void SetViewState(DBVersionsMangementViewType dbVersionsMangementViewType)
        {
            BindToUIElements(dbVersionsMangementViewType);

            HideAllActionPanels();

            SetControlHideOrVisible(pnlRepeatableFiles, true);
            if (_projectConfigItem.IsDevEnvironment)
            {
                SetControlHideOrVisible(pnlDevDummyDataFiles, true);
            }


            switch (dbVersionsMangementViewType)
            {
                case DBVersionsMangementViewType.ReadyToRunSync:

                    SetControlHideOrVisible(pnlMainActions, true);
                    SetControlHideOrVisible(lblColorTargetState_Square, false);
                    SetControlHideOrVisible(lblColorTargetState_Caption, false);



                    dgIncrementalScriptsFiles.BeginInvoke((MethodInvoker)(() =>
                    {
                        if (dgIncrementalScriptsFiles.DataSource is List<RuntimeScriptFileBase> scriptFileList
                            && scriptFileList.Count > 0)
                        {
                            TargetStateScriptFileName = scriptFileList.Last().Filename;
                        }
                    }));

                    EnableDisableGridToSelectTargetState(false);

                    EnanbleDisableForm(true);

                    break;

                case DBVersionsMangementViewType.ReadyToSyncToSpecificState:

                    SetControlHideOrVisible(pnlSyncToSpecificState, true);
                    SetControlHideOrVisible(lblColorTargetState_Square, true);
                    SetControlHideOrVisible(lblColorTargetState_Caption, true);

                    SetControlHideOrVisible(pnlRepeatableFiles, false);
                    SetControlHideOrVisible(pnlDevDummyDataFiles, false);


                    notificationsControl1.SetAttentionMessage("Select the target Database State, and click on Apply");

                    EnableDisableGridToSelectTargetState(true);

                    EnanbleDisableForm(true);


                    break;

                case DBVersionsMangementViewType.MissingSystemTables:
                case DBVersionsMangementViewType.HistoryExecutedFilesChanged:

                    SetControlHideOrVisible(pnlMissingSystemTables, true);
                    SetControlHideOrVisible(lblColorTargetState_Square, false);
                    SetControlHideOrVisible(lblColorTargetState_Caption, false);

                    dgIncrementalScriptsFiles.BeginInvoke((MethodInvoker)(() =>
                    {
                        if (dgIncrementalScriptsFiles.DataSource is List<RuntimeScriptFileBase> scriptFileList
                            && scriptFileList.Count > 0)
                        {
                            TargetStateScriptFileName = scriptFileList.Last().Filename;
                        }
                    }));

                    EnableDisableGridToSelectTargetState(false);

                    EnanbleDisableForm(true);

                    break;




                case DBVersionsMangementViewType.SetDBStateManually:

                    SetControlHideOrVisible(pnlSetDBStateManually, true);
                    SetControlHideOrVisible(lblColorTargetState_Square, true);
                    SetControlHideOrVisible(lblColorTargetState_Caption, true);

                    SetControlHideOrVisible(pnlRepeatableFiles, false);
                    SetControlHideOrVisible(pnlDevDummyDataFiles, false);

                    notificationsControl1.SetAttentionMessage("Select the Target Database State to virtually mark, and click on Apply");

                    EnableDisableGridToSelectTargetState(true);

                    EnanbleDisableForm(true);

                    break;

                case DBVersionsMangementViewType.InProcess:

                    EnanbleDisableForm(false);

                    break;

                case DBVersionsMangementViewType.RestoreDatabaseError:

                    EnanbleDisableForm(false);
                    SetControlEnableOrDisable(btnShowHistoricalBackups, true);

                    SetControlHideOrVisible(pnlRestoreDbError, true);
                    SetControlHideOrVisible(lblColorTargetState_Square, false);
                    SetControlHideOrVisible(lblColorTargetState_Caption, false);

                    break;

                default:
                    break;
            }

            btnRecreateDbFromScratchMain.BeginInvoke((MethodInvoker)(() =>
            {
                btnRecreateDbFromScratchMain.Visible = _projectConfigItem.IsDevEnvironment;
            }));
            lblRecreateDbFromScratchMain.BeginInvoke((MethodInvoker)(() =>
            {
                lblRecreateDbFromScratchMain.Visible = _projectConfigItem.IsDevEnvironment;
            }));

            btnDeploy.BeginInvoke((MethodInvoker)(() =>
            {
                btnDeploy.Visible = _projectConfigItem.IsDevEnvironment;
            }));
            lblDeploy.BeginInvoke((MethodInvoker)(() =>
            {
                lblDeploy.Visible = _projectConfigItem.IsDevEnvironment;
            }));

            btnRecreateDbFromScratch2.BeginInvoke((MethodInvoker)(() =>
            {
                btnRecreateDbFromScratch2.Visible = _projectConfigItem.IsDevEnvironment;
            }));
            lblRecreateDbFromScratch2.BeginInvoke((MethodInvoker)(() =>
            {
                lblRecreateDbFromScratch2.Visible = _projectConfigItem.IsDevEnvironment;
            }));

        }

        private void HideAllActionPanels()
        {
            SetControlHideOrVisible(pnlMainActions, false);
            SetControlHideOrVisible(pnlSyncToSpecificState, false);
            SetControlHideOrVisible(pnlMissingSystemTables, false);
            SetControlHideOrVisible(pnlSetDBStateManually, false);
            SetControlHideOrVisible(pnlRestoreDbError, false);
        }

        private void BindToUIElements(DBVersionsMangementViewType dbVersionsMangementViewType)
        {

            lblProjectName.BeginInvoke((MethodInvoker)(() =>
            {
                lblProjectName.Text = _projectConfigItem.ProjectFullName;
            }));


            if (_scriptFilesState != null)
            {
                BindIncrementalGrid(dbVersionsMangementViewType);
                BindRepeatableGrid();
                BindDevDummyDataGrid();
            }

        }


        private void BindIncrementalGrid(DBVersionsMangementViewType dbVersionsMangementViewType)
        {
            List<RuntimeScriptFileBase> allIncrementalScriptFiles = _scriptFilesState.IncrementalScriptFilesComparer.AllFileSystemScriptFiles.ToList();

            if (dbVersionsMangementViewType == DBVersionsMangementViewType.ReadyToSyncToSpecificState
                || dbVersionsMangementViewType == DBVersionsMangementViewType.SetDBStateManually)
            {
                RuntimeScriptFileBase emptyDBTargetState = new EmptyDbStateRuntimeScriptFile();
                allIncrementalScriptFiles.Insert(0, emptyDBTargetState);
            }

            BindGridDataSource(dgIncrementalScriptsFiles, allIncrementalScriptFiles);
        }

        private void BindRepeatableGrid()
        {
            List<RuntimeScriptFileBase> allRepeatableScriptFiles = _scriptFilesState.RepeatableScriptFilesComparer.AllFileSystemScriptFiles.ToList();

            BindGridDataSource(dgRepeatableScriptsFiles, allRepeatableScriptFiles);
        }

        private void BindDevDummyDataGrid()
        {
            if (_projectConfigItem.IsDevEnvironment)
            {
                List<RuntimeScriptFileBase> allDevDummyDataScriptFiles = _scriptFilesState.DevDummyDataScriptFilesComparer.AllFileSystemScriptFiles.ToList();

                BindGridDataSource(dgDevDummyDataScriptsFiles, allDevDummyDataScriptFiles);
            }

        }


        private static void BindGridDataSource(DataGridView dataGridView, List<RuntimeScriptFileBase> scriptFilesList)
        {

            dataGridView.BeginInvoke((MethodInvoker)(() =>
            {
                dataGridView.DataSource = null;
                dataGridView.DataSource = scriptFilesList;


                foreach (DataGridViewRow currGridRow in dataGridView.Rows)
                {
                    currGridRow.Cells[0].Value = currGridRow.Index + 1;
                    currGridRow.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    RuntimeScriptFileBase currRowFileInfo = currGridRow.DataBoundItem as RuntimeScriptFileBase;

                    currGridRow.Cells[1].Style.BackColor = currRowFileInfo.HashDiffType switch
                    {
                        HashDiffType.NotExist => Color.White,
                        HashDiffType.Different => Color.LightSalmon,
                        HashDiffType.Equal => Color.LightGreen,
                        _ => Color.White,
                    };
                }


                if (dataGridView.RowCount > 0)
                {
                    dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.RowCount - 1;
                }
            }));

        }



        private void EnableDisableGridToSelectTargetState(bool isEnable)
        {
            dgIncrementalScriptsFiles.BeginInvoke((MethodInvoker)(() =>
            {
                dgIncrementalScriptsFiles.Columns[2].Visible = isEnable;
            }));

            MarkUnMarkSelectedTargetInGrid();
        }

        private void MarkUnMarkSelectedTargetInGrid()
        {
            dgIncrementalScriptsFiles.BeginInvoke((MethodInvoker)(() =>
            {

                foreach (DataGridViewRow currGridRow in dgIncrementalScriptsFiles.Rows)
                {
                    currGridRow.Cells[0].Value = (currGridRow.Index + 1).ToString(CultureInfo.InvariantCulture);
                    currGridRow.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    RuntimeScriptFileBase currRowFileInfo = currGridRow.DataBoundItem as RuntimeScriptFileBase;

                    if (currRowFileInfo.Filename.Trim().ToUpperInvariant() == TargetStateScriptFileName.Trim().ToUpperInvariant())
                    {
                        currGridRow.Cells[2].Style.BackColor = Color.Yellow;
                    }
                    else
                    {
                        currGridRow.Cells[2].Style.BackColor = Color.White;
                    }
                }
            }));

        }

        private void EnanbleDisableForm(bool isEnable)
        {
            SetControlEnableOrDisable(pnlMainActions, isEnable);
            SetControlEnableOrDisable(pnlMissingSystemTables, isEnable);
            SetControlEnableOrDisable(pnlSetDBStateManually, isEnable);
            SetControlEnableOrDisable(btnRefresh, isEnable);
            SetControlEnableOrDisable(dgIncrementalScriptsFiles, isEnable);
            SetControlEnableOrDisable(btnShowHistoricalBackups, isEnable);
            SetControlEnableOrDisable(btnCreateNewIncrementalScriptFile, isEnable);
            SetControlEnableOrDisable(btnCreateNewRepeatableScriptFile, isEnable);
            SetControlEnableOrDisable(btnCreateNewDevDummyDataScriptFile, isEnable);
        }

        private static void SetControlEnableOrDisable(Control control, bool isEnable)
        {
            control.BeginInvoke((MethodInvoker)(() =>
            {
                control.Enabled = isEnable;
            }));
        }

        private static void SetControlHideOrVisible(Control control, bool isVisible)
        {
            control.BeginInvoke((MethodInvoker)(() =>
            {
                control.Visible = isVisible;
            }));
        }

        private void SetViewState_AfterProcessComplete(ProcessTrace processResults)
        {
            if (processResults.HasError
                && !string.IsNullOrWhiteSpace(processResults.InstructionsMessageStepName)
                && string.CompareOrdinal(processResults.InstructionsMessageStepName, RestoreDatabaseStep.StepNameStr) == 0)
            {
                SetViewState(DBVersionsMangementViewType.RestoreDatabaseError);
            }
            else
            {
                SetViewState(DBVersionsMangementViewType.ReadyToRunSync);
            }
        }








        #endregion


        #region Dispose

        // To detect redundant calls
        private bool _disposed = false;

        // Protected implementation of Dispose pattern.
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                //AutoVersionsDbAPI.Dispose();

                if (components != null)
                {
                    components.Dispose();
                }
            }

            _disposed = true;
            // Call base class implementation.
            base.Dispose(disposing);
        }

        #endregion

    }
}
