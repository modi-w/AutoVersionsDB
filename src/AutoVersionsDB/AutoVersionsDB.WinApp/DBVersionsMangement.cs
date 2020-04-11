using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.ScriptFiles.Incremental;
using System.ComponentModel;

namespace AutoVersionsDB.WinApp
{

    public partial class DBVersionsMangement : UserControl
    {
        public enum eDBVersionsMangementViewType
        {
            ReadyToRunSync,
            ReadyToSyncToSpecificState,
            MissingSystemTables,
            HistoryExecutedFilesChanged,
            SetDBStateManually,
            InProcess,
            RestoreDatabaseError
        }


        private AutoVersionsDbAPI _autoVersionsDbAPI = null;



        public event OnEditProjectHandler OnEditProject;

        public string TargetStateScriptFileName { get; private set; }


        public DBVersionsMangement()
        {
            InitializeComponent();

            dgIncrementalScriptsFiles.AutoGenerateColumns = false;
            dgIncrementalScriptsFiles.SelectionChanged += dgIncrementalScriptsFiles_SelectionChanged;

            dgRepeatableScriptsFiles.AutoGenerateColumns = false;
            dgRepeatableScriptsFiles.SelectionChanged += dgRepeatableScriptsFiles_SelectionChanged;

            dgDevDummyDataScriptsFiles.AutoGenerateColumns = false;
            dgDevDummyDataScriptsFiles.SelectionChanged += dgDevDummyDataScriptsFiles_SelectionChanged;

            //pnlSyncToSpecificState.Location = new Point(873, 14);
            //pnlMissingSystemTables.Location = new Point(600, 10);
            //pnlSetDBStateManually.Location = new Point(880, 24);
            //btnShowHistoricalBackups.Location = new Point(880, 24);

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _autoVersionsDbAPI = AutoVersionsDbAPI.Instance;
            }

            btnSetDBToSpecificState.Visible = false;

            setToolTips();
        }

        private void setToolTips()
        {
            ToolTip tooltipControl = new ToolTip();

            // Set up the delays for the ToolTip.
            tooltipControl.AutoPopDelay = 5000;
            tooltipControl.InitialDelay = 500;
            tooltipControl.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            tooltipControl.ShowAlways = true;

            // Set up the ToolTip text with the controls.
            tooltipControl.SetToolTip(this.btnRefresh, "Refresh");
            tooltipControl.SetToolTip(this.btnRunSync, "Sync the db with the missing scripts");
            tooltipControl.SetToolTip(this.btnRecreateDbFromScratchMain, "Recreate DB From Scratch");
            tooltipControl.SetToolTip(this.btnDeploy, "Create Deploy Package");
            tooltipControl.SetToolTip(this.btnSetDBToSpecificState, "Set DB To Specific State");
        }




        #region Refresh

        public void SetProjectConfigItem(ProjectConfigItem projectConfigItem)
        {
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfigItem);

            refreshAll();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshAll();
        }

        private void refreshAll()
        {
            TargetStateScriptFileName = null;

            pnlDevDummyDataFiles.Visible = _autoVersionsDbAPI.ProjectConfigItem.IsDevEnvironment;

            hideAllActionPanels();

            notificationsControl1.PreparingMesage();



            Task.Factory.StartNew(() =>
            {
                _autoVersionsDbAPI.Refresh();

                //      bindToUIElements();

                try
                {
                    _autoVersionsDbAPI.ValidateAll();

                    if (_autoVersionsDbAPI.HasError)
                    {
                        notificationsControl1.AfterComplete();

                        if (_autoVersionsDbAPI.ErrorCode == "SystemTables")
                        {
                            setViewState(eDBVersionsMangementViewType.MissingSystemTables);
                        }
                        else if (_autoVersionsDbAPI.ErrorCode == "IsHistoryExecutedFilesChanged")
                        {
                            setViewState(eDBVersionsMangementViewType.HistoryExecutedFilesChanged);
                        }
                        else
                        {
                            setViewState(eDBVersionsMangementViewType.ReadyToRunSync);
                        }
                    }
                    else
                    {
                        notificationsControl1.Clear();
                        setViewState(eDBVersionsMangementViewType.ReadyToRunSync);
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

        private void dgIncrementalScriptsFiles_SelectionChanged(object sender, EventArgs e)
        {
            dgIncrementalScriptsFiles.ClearSelection();
        }
        private void dgRepeatableScriptsFiles_SelectionChanged(object sender, EventArgs e)
        {
            dgRepeatableScriptsFiles.ClearSelection();
        }
        private void dgDevDummyDataScriptsFiles_SelectionChanged(object sender, EventArgs e)
        {
            dgDevDummyDataScriptsFiles.ClearSelection();
        }


        private void btnShowHistoricalBackups_Click_1(object sender, EventArgs e)
        {
            Process.Start(_autoVersionsDbAPI.ProjectConfigItem.DBBackupBaseFolder);
        }

        private void btnCancelSyncSpecificState_Click(object sender, EventArgs e)
        {
            setViewState(eDBVersionsMangementViewType.ReadyToRunSync);

        }

        private void btnSetDBToSpecificState_Click(object sender, EventArgs e)
        {
            setViewState(eDBVersionsMangementViewType.ReadyToSyncToSpecificState);
        }



        private void btnNavToEdit_Click(object sender, EventArgs e)
        {
            OnEditProject?.Invoke(_autoVersionsDbAPI.ProjectConfigItem);
        }


        private void dgScriptFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                RuntimeScriptFileBase currScriptFileInfo = (dgIncrementalScriptsFiles.DataSource as List<RuntimeScriptFileBase>)[e.RowIndex];

                TargetStateScriptFileName = currScriptFileInfo.Filename;

                markUnMarkSelectedTargetInGrid();
            }

        }

        private void btnCancelSetDBStateManually_Click(object sender, EventArgs e)
        {
            setViewState(eDBVersionsMangementViewType.MissingSystemTables);
        }


        private void btnOpenIncrementalScriptsFolder_Click(object sender, EventArgs e)
        {
            Process.Start(_autoVersionsDbAPI.ProjectConfigItem.IncrementalScriptsFolderPath);
        }
        private void btnCreateNewIncrementalScriptFile_Click(object sender, EventArgs e)
        {
            TextInputWindow textInputWindow = new TextInputWindow("Create new script script file, insert the script name:");
            textInputWindow.ShowDialog();

            if (textInputWindow.IsApply)
            {
                string newFileFullPath = _autoVersionsDbAPI.CreateNewIncrementalScriptFile(textInputWindow.ResultText);

                refreshAll();
                Process.Start(newFileFullPath);
            }
        }

        private void btnOpenRepeatableScriptsFolder_Click(object sender, EventArgs e)
        {
            Process.Start(_autoVersionsDbAPI.ProjectConfigItem.RepeatableScriptsFolderPath);
        }
        private void btnCreateNewRepeatableScriptFile_Click(object sender, EventArgs e)
        {
            TextInputWindow textInputWindow = new TextInputWindow("Create new script script file, insert the script name:");
            textInputWindow.ShowDialog();

            if (textInputWindow.IsApply)
            {
                string newFileFullPath = _autoVersionsDbAPI.CreateNewRepeatableScriptFile(textInputWindow.ResultText);
                refreshAll();
                Process.Start(newFileFullPath);
            }
        }


        private void btnOpenDevDummyDataScriptsFolder_Click(object sender, EventArgs e)
        {
            Process.Start(_autoVersionsDbAPI.ProjectConfigItem.DevDummyDataScriptsFolderPath);
        }
        private void btnCreateNewDevDummyDataScriptFile_Click(object sender, EventArgs e)
        {
            TextInputWindow textInputWindow = new TextInputWindow("Create new script script file, insert the script name:");
            textInputWindow.ShowDialog();

            if (textInputWindow.IsApply)
            {
                string newFileFullPath = _autoVersionsDbAPI.CreateNewDevDummyDataScriptFile(textInputWindow.ResultText);
                refreshAll();
                Process.Start(newFileFullPath);
            }
        }
        #endregion



        #region Run Process

        private void btnRunSync_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    setViewState(eDBVersionsMangementViewType.InProcess);
                    notificationsControl1.BeforeStart();

                    _autoVersionsDbAPI.SyncDB();

                    notificationsControl1.AfterComplete();
                    setViewState_AfterProcessComplete();
                }
                catch (Exception ex)
                {
                    //LogManagerObj.AddExceptionMessageToLog(ex, "btnRunSync_Click()");
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            });
        }





        private bool checkIsTargetStateHistory()
        {
            bool isAllowRun = true;

            if (!_autoVersionsDbAPI.ValdiateTargetStateAlreadyExecuted(TargetStateScriptFileName))
            {
                string warningMessage = $"This action will drop the Database and recreate it only by the scripts, you may lose Data. Are you sure?";
                isAllowRun = MessageBox.Show(this, warningMessage, "Pay Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes;
            }

            return isAllowRun;
        }

        private void btnRecreateDbFromScratchMain_Click(object sender, EventArgs e)
        {
            runRecreateDBFromScratch();
        }


        private void btnApplySyncSpecificState_Click_1(object sender, EventArgs e)
        {
            if (checkIsTargetStateHistory())
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        setViewState(eDBVersionsMangementViewType.InProcess);
                        notificationsControl1.BeforeStart();

                        _autoVersionsDbAPI.SetDBToSpecificState(TargetStateScriptFileName, true);

                        notificationsControl1.AfterComplete();
                        setViewState_AfterProcessComplete();

                    }
                    catch (Exception ex)
                    {
                        //LogManagerObj.AddExceptionMessageToLog(ex, "btnApplySyncSpecificState_Click()");
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                });
            }
        }


        private void btnDeploy_Click(object sender, EventArgs e)
        {
            try
            {
                setViewState(eDBVersionsMangementViewType.InProcess);
                notificationsControl1.BeforeStart();

                _autoVersionsDbAPI.Deploy();

                notificationsControl1.AfterComplete();
                setViewState_AfterProcessComplete();
            }
            catch (Exception ex)
            {
                //LogManagerObj.AddExceptionMessageToLog(ex, "deployToolStripMenuItem_Click()");
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVirtualExecution_Click(object sender, EventArgs e)
        {
            setViewState(eDBVersionsMangementViewType.SetDBStateManually);
        }

        private void btnRecreateDbFromScratch2_Click_1(object sender, EventArgs e)
        {
            runRecreateDBFromScratch();
        }

        private void runRecreateDBFromScratch()
        {
            string warningMessage = $"Are you sure?";
            if (MessageBox.Show(this, warningMessage, "Pay Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        setViewState(eDBVersionsMangementViewType.InProcess);
                        notificationsControl1.BeforeStart();

                        _autoVersionsDbAPI.RecreateDBFromScratch(TargetStateScriptFileName);

                        notificationsControl1.AfterComplete();
                        setViewState_AfterProcessComplete();
                    }
                    catch (Exception ex)
                    {
                        //LogManagerObj.AddExceptionMessageToLog(ex, "runRecreateDBFromScratch()");
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                });
            }
        }


        private void btnRunSetDBStateManally_Click_1(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    setViewState(eDBVersionsMangementViewType.InProcess);
                    notificationsControl1.BeforeStart();

                    _autoVersionsDbAPI.SetDBStateByVirtualExecution(TargetStateScriptFileName);

                    notificationsControl1.AfterComplete();
                    setViewState_AfterProcessComplete();
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

        private void setViewState(eDBVersionsMangementViewType dbVersionsMangementViewType)
        {
            bindToUIElements(dbVersionsMangementViewType);

            hideAllActionPanels();

            setControlHideOrVisible(pnlRepeatableFiles, true);
            if (_autoVersionsDbAPI.ProjectConfigItem.IsDevEnvironment)
            {
                setControlHideOrVisible(pnlDevDummyDataFiles, true);
            }


            switch (dbVersionsMangementViewType)
            {
                case eDBVersionsMangementViewType.ReadyToRunSync:

                    setControlHideOrVisible(pnlMainActions, true);
                    setControlHideOrVisible(lblColorTargetState_Square, false);
                    setControlHideOrVisible(lblColorTargetState_Caption, false);



                    dgIncrementalScriptsFiles.BeginInvoke((MethodInvoker)(() =>
                    {
                        List<RuntimeScriptFileBase> scriptFileList = dgIncrementalScriptsFiles.DataSource as List<RuntimeScriptFileBase>;

                        if (scriptFileList != null
                            && scriptFileList.Count > 0)
                        {
                            TargetStateScriptFileName = scriptFileList.Last().Filename;
                        }
                    }));

                    enableDisableGridToSelectTargetState(false);

                    enanbleDisableForm(true);

                    break;

                case eDBVersionsMangementViewType.ReadyToSyncToSpecificState:

                    setControlHideOrVisible(pnlSyncToSpecificState, true);
                    setControlHideOrVisible(lblColorTargetState_Square, true);
                    setControlHideOrVisible(lblColorTargetState_Caption, true);

                    setControlHideOrVisible(pnlRepeatableFiles, false);
                    setControlHideOrVisible(pnlDevDummyDataFiles, false);


                    notificationsControl1.SetAttentionMessage("Select the target Database State, and click on Apply");

                    enableDisableGridToSelectTargetState(true);

                    enanbleDisableForm(true);


                    break;

                case eDBVersionsMangementViewType.MissingSystemTables:
                case eDBVersionsMangementViewType.HistoryExecutedFilesChanged:

                    setControlHideOrVisible(pnlMissingSystemTables, true);
                    setControlHideOrVisible(lblColorTargetState_Square, false);
                    setControlHideOrVisible(lblColorTargetState_Caption, false);

                    dgIncrementalScriptsFiles.BeginInvoke((MethodInvoker)(() =>
                    {
                        List<RuntimeScriptFileBase> scriptFileList = dgIncrementalScriptsFiles.DataSource as List<RuntimeScriptFileBase>;

                        if (scriptFileList != null
                            && scriptFileList.Count > 0)
                        {
                            TargetStateScriptFileName = scriptFileList.Last().Filename;
                        }
                    }));

                    enableDisableGridToSelectTargetState(false);

                    enanbleDisableForm(true);

                    break;




                case eDBVersionsMangementViewType.SetDBStateManually:

                    setControlHideOrVisible(pnlSetDBStateManually, true);
                    setControlHideOrVisible(lblColorTargetState_Square, true);
                    setControlHideOrVisible(lblColorTargetState_Caption, true);

                    setControlHideOrVisible(pnlRepeatableFiles, false);
                    setControlHideOrVisible(pnlDevDummyDataFiles, false);

                    notificationsControl1.SetAttentionMessage("Select the Target Database State to virtually mark, and click on Apply");

                    enableDisableGridToSelectTargetState(true);

                    enanbleDisableForm(true);

                    break;

                case eDBVersionsMangementViewType.InProcess:

                    enanbleDisableForm(false);

                    break;

                case eDBVersionsMangementViewType.RestoreDatabaseError:

                    enanbleDisableForm(false);
                    setControlEnableOrDisable(btnShowHistoricalBackups, true);

                    setControlHideOrVisible(pnlRestoreDbError, true);
                    setControlHideOrVisible(lblColorTargetState_Square, false);
                    setControlHideOrVisible(lblColorTargetState_Caption, false);

                    break;

                default:
                    break;
            }

            btnRecreateDbFromScratchMain.BeginInvoke((MethodInvoker)(() =>
            {
                btnRecreateDbFromScratchMain.Visible = _autoVersionsDbAPI.ProjectConfigItem.IsDevEnvironment;
            }));
            btnDeploy.BeginInvoke((MethodInvoker)(() =>
            {
                btnDeploy.Visible = _autoVersionsDbAPI.ProjectConfigItem.IsDevEnvironment;
            }));
            btnRecreateDbFromScratch2.BeginInvoke((MethodInvoker)(() =>
            {
                btnRecreateDbFromScratch2.Visible = _autoVersionsDbAPI.ProjectConfigItem.IsDevEnvironment;
            }));


        }

        private void hideAllActionPanels()
        {
            setControlHideOrVisible(pnlMainActions, false);
            setControlHideOrVisible(pnlSyncToSpecificState, false);
            setControlHideOrVisible(pnlMissingSystemTables, false);
            setControlHideOrVisible(pnlSetDBStateManually, false);
            setControlHideOrVisible(pnlRestoreDbError, false);
        }

        private void bindToUIElements(eDBVersionsMangementViewType dbVersionsMangementViewType)
        {

            lblProjectName.BeginInvoke((MethodInvoker)(() =>
            {
                lblProjectName.Text = _autoVersionsDbAPI.ProjectConfigItem.ProjectName;
            }));


            if (!_autoVersionsDbAPI.HasError
                || _autoVersionsDbAPI.ErrorCode == "IsHistoryExecutedFilesChanged"
                || _autoVersionsDbAPI.ErrorCode == "SystemTables")
            {
                bindIncrementalGrid(dbVersionsMangementViewType);
                bindRepeatableGrid();
                bindDevDummyDataGrid();
            }

        }


        private void bindIncrementalGrid(eDBVersionsMangementViewType dbVersionsMangementViewType)
        {
            List<RuntimeScriptFileBase> allIncrementalScriptFiles = _autoVersionsDbAPI.ScriptFilesComparersProvider.IncrementalScriptFilesComparer.AllFileSystemScriptFiles.ToList();

            if (dbVersionsMangementViewType == eDBVersionsMangementViewType.ReadyToSyncToSpecificState
                || dbVersionsMangementViewType == eDBVersionsMangementViewType.SetDBStateManually)
            {
                RuntimeScriptFileBase emptyDBTargetState = new EmptyDbStateRuntimeScriptFile();
                allIncrementalScriptFiles.Insert(0, emptyDBTargetState);
            }

            bindGridDataSource(dgIncrementalScriptsFiles, allIncrementalScriptFiles);
        }

        private void bindRepeatableGrid()
        {
            List<RuntimeScriptFileBase> allRepeatableScriptFiles = _autoVersionsDbAPI.ScriptFilesComparersProvider.RepeatableScriptFilesComparer.AllFileSystemScriptFiles.ToList();

            bindGridDataSource(dgRepeatableScriptsFiles, allRepeatableScriptFiles);
        }

        private void bindDevDummyDataGrid()
        {
            if (_autoVersionsDbAPI.ProjectConfigItem.IsDevEnvironment)
            {
                List<RuntimeScriptFileBase> allDevDummyDataScriptFiles = _autoVersionsDbAPI.ScriptFilesComparersProvider.DevDummyDataScriptFilesComparer.AllFileSystemScriptFiles.ToList();

                bindGridDataSource(dgDevDummyDataScriptsFiles, allDevDummyDataScriptFiles);
            }

        }


        private void bindGridDataSource(DataGridView dataGridView, List<RuntimeScriptFileBase> scriptFilesList)
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

                    switch (currRowFileInfo.HashDiffType)
                    {
                        case eHashDiffType.NotExist:

                            currGridRow.Cells[1].Style.BackColor = Color.White;
                            break;

                        case eHashDiffType.Different:

                            currGridRow.Cells[1].Style.BackColor = Color.LightSalmon;
                            break;

                        case eHashDiffType.Equal:

                            currGridRow.Cells[1].Style.BackColor = Color.LightGreen;
                            break;

                        default:

                            currGridRow.Cells[1].Style.BackColor = Color.White;
                            break;
                    }

                }


                if (dataGridView.RowCount > 0)
                {
                    dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.RowCount - 1;
                }
            }));

        }



        private void enableDisableGridToSelectTargetState(bool isEnable)
        {
            dgIncrementalScriptsFiles.BeginInvoke((MethodInvoker)(() =>
            {
                dgIncrementalScriptsFiles.Columns[2].Visible = isEnable;
            }));

            markUnMarkSelectedTargetInGrid();
        }

        private void markUnMarkSelectedTargetInGrid()
        {
            dgIncrementalScriptsFiles.BeginInvoke((MethodInvoker)(() =>
            {

                foreach (DataGridViewRow currGridRow in dgIncrementalScriptsFiles.Rows)
                {
                    currGridRow.Cells[0].Value = string.Format("{0}", currGridRow.Index + 1);
                    currGridRow.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    RuntimeScriptFileBase currRowFileInfo = currGridRow.DataBoundItem as RuntimeScriptFileBase;

                    if (currRowFileInfo.Filename.Trim().ToLower() == TargetStateScriptFileName.Trim().ToLower())
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

        private void enanbleDisableForm(bool isEnable)
        {
            setControlEnableOrDisable(pnlMainActions, isEnable);
            setControlEnableOrDisable(pnlMissingSystemTables, isEnable);
            setControlEnableOrDisable(pnlSetDBStateManually, isEnable);
            setControlEnableOrDisable(btnRefresh, isEnable);
            setControlEnableOrDisable(dgIncrementalScriptsFiles, isEnable);
            setControlEnableOrDisable(btnShowHistoricalBackups, isEnable);
        }

        private void setControlEnableOrDisable(Control control, bool isEnable)
        {
            control.BeginInvoke((MethodInvoker)(() =>
            {
                control.Enabled = isEnable;
            }));
        }

        private void setControlHideOrVisible(Control control, bool isVisible)
        {
            control.BeginInvoke((MethodInvoker)(() =>
            {
                control.Visible = isVisible;
            }));
        }

        private void setViewState_AfterProcessComplete()
        {
            if (_autoVersionsDbAPI.HasError
                && !string.IsNullOrWhiteSpace(_autoVersionsDbAPI.InstructionsMessage_StepName)
                && _autoVersionsDbAPI.InstructionsMessage_StepName.Contains("Rollback (Restore) Database"))
            {
                setViewState(eDBVersionsMangementViewType.RestoreDatabaseError);
            }
            else
            {
                setViewState(eDBVersionsMangementViewType.ReadyToRunSync);
            }
        }








        #endregion

       
    }
}
