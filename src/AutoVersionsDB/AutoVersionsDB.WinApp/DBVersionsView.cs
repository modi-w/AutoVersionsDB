using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.UI.DBVersions;
using AutoVersionsDB.WinApp.Utils;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp
{

    public partial class DBVersionsView : UserControlNinjectBase// UserControl
    {
        [Inject]
        public DBVersionsViewModel ViewModel { get; set; }



        public DBVersionsView()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                ViewModel.OnTextInput += ViewModel_OnTextInput;
                ViewModel.PropertyChanged += ViewModel_PropertyChanged;
                ViewModel.DBVersionsViewModelData.PropertyChanged += DBVersionsViewModelData_PropertyChanged;
                ViewModel.DBVersionsControls.PropertyChanged += DBVersionsControls_PropertyChanged;
                SetDataBindings();


                EnableDisableGridToSelectTargetState(ViewModel.DBVersionsControls.GridToSelectTargetStateEnabled);

                SetToolTips();

            }


            dgIncrementalScriptsFiles.AutoGenerateColumns = false;
            dgIncrementalScriptsFiles.SelectionChanged += DgIncrementalScriptsFiles_SelectionChanged;

            dgRepeatableScriptsFiles.AutoGenerateColumns = false;
            dgRepeatableScriptsFiles.SelectionChanged += DgRepeatableScriptsFiles_SelectionChanged;

            dgDevDummyDataScriptsFiles.AutoGenerateColumns = false;
            dgDevDummyDataScriptsFiles.SelectionChanged += DgDevDummyDataScriptsFiles_SelectionChanged;


            ChangeButtonsPanelsLocation(pnlMainActions);
            ChangeButtonsPanelsLocation(pnlMissingSystemTables);
            ChangeButtonsPanelsLocation(pnlRestoreDBError);
            ChangeButtonsPanelsLocation(pnlSyncToSpecificState);
            ChangeButtonsPanelsLocation(pnlSetDBStateManually);

            Controls.Remove(pnlActionButtons);

            //pnlSyncToSpecificState.Location = new Point(873, 14);
            //pnlMissingSystemTables.Location = new Point(600, 10);
            //pnlSetDBStateManually.Location = new Point(880, 24);
            //btnShowHistoricalBackups.Location = new Point(880, 24);


            //#if !DEBUG
            btnSetDBToSpecificState.Visible = false;
            lblSetDBToSpecificState.Visible = false;
            //#endif



        }

        private void DBVersionsControls_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.DBVersionsControls.GridToSelectTargetStateEnabled):

                    EnableDisableGridToSelectTargetState(ViewModel.DBVersionsControls.GridToSelectTargetStateEnabled);
                    break;


                default:
                    break;
            }
        }

        private void DBVersionsViewModelData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.DBVersionsViewModelData.IncrementalScriptFiles):

                    BindGridDataSource(dgIncrementalScriptsFiles, ViewModel.DBVersionsViewModelData.IncrementalScriptFiles);
                    break;

                case nameof(ViewModel.DBVersionsViewModelData.RepeatableScriptFiles):

                    BindGridDataSource(dgRepeatableScriptsFiles, ViewModel.DBVersionsViewModelData.RepeatableScriptFiles);
                    break;

                case nameof(ViewModel.DBVersionsViewModelData.DevDummyDataScriptFiles):

                    BindGridDataSource(dgDevDummyDataScriptsFiles, ViewModel.DBVersionsViewModelData.DevDummyDataScriptFiles);
                    break;


                default:
                    break;
            }
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
                tooltipControl.SetToolTip(btnRefresh, ViewModel.DBVersionsControls.BtnRefreshTooltip);
                tooltipControl.SetToolTip(btnRunSync, ViewModel.DBVersionsControls.BtnRunSyncTooltip);
                tooltipControl.SetToolTip(btnRecreateDBFromScratchMain, ViewModel.DBVersionsControls.BtnRecreateDBFromScratchMainTooltip);
                tooltipControl.SetToolTip(btnRecreateDBFromScratch2, ViewModel.DBVersionsControls.BtnRecreateDBFromScratchMainTooltip);
                tooltipControl.SetToolTip(btnDeploy, ViewModel.DBVersionsControls.BtnDeployTooltip);
                tooltipControl.SetToolTip(btnSetDBToSpecificState, ViewModel.DBVersionsControls.BtnSetDBToSpecificStateTooltip);
                tooltipControl.SetToolTip(btnVirtualExecution, ViewModel.DBVersionsControls.BtnVirtualExecutionTooltip);
                tooltipControl.SetToolTip(btnShowHistoricalBackups, ViewModel.DBVersionsControls.BtnShowHistoricalBackupsTooltip);
            }

        }



        private void ViewModel_OnTextInput(object sender, OnTextInputEventsEventArgs e)
        {
            TextInputWindow textInputWindow = new TextInputWindow(e.InstructionMessageText);
            textInputWindow.ShowDialog();

            e.Results = new TextInputResults()
            {
                ResultText = textInputWindow.ResultText,
                IsApply = textInputWindow.IsApply,
            };
        }






        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }


        private void SetDataBindings()
        {
            lblProjectName.DataBindings.Clear();
            lblProjectName.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblProjectName,
                    nameof(lblProjectName.Text),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblProjectNameText)
                    )
                );


            pnlMainActions.DataBindings.Clear();
            pnlMainActions.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlMainActions,
                    nameof(pnlMainActions.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.PnlMainActionsVisible)
                    )
                );

            pnlSyncToSpecificState.DataBindings.Clear();
            pnlSyncToSpecificState.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlSyncToSpecificState,
                    nameof(pnlSyncToSpecificState.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.PnlSyncToSpecificStateVisible)
                    )
                );

            pnlMissingSystemTables.DataBindings.Clear();
            pnlMissingSystemTables.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlMissingSystemTables,
                    nameof(pnlMissingSystemTables.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.PnlMissingSystemTablesVisible)
                    )
                );
            pnlMissingSystemTables.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlMissingSystemTables,
                    nameof(pnlMissingSystemTables.Enabled),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.PnlMissingSystemTablesEnabled)
                    )
                );

            pnlSetDBStateManually.DataBindings.Clear();
            pnlSetDBStateManually.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlSetDBStateManually,
                    nameof(pnlSetDBStateManually.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.PnlSetDBStateManuallyVisible)
                    )
                );
            pnlSetDBStateManually.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlSetDBStateManually,
                    nameof(pnlSetDBStateManually.Enabled),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.PnlSetDBStateManuallyEnabled)
                    )
                );

            pnlDevDummyDataFiles.DataBindings.Clear();
            pnlDevDummyDataFiles.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlDevDummyDataFiles,
                    nameof(pnlDevDummyDataFiles.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.PnlDevDummyDataFilesVisible)
                    )
                );

            pnlRestoreDBError.DataBindings.Clear();
            pnlRestoreDBError.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlRestoreDBError,
                    nameof(pnlRestoreDBError.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.PnlRestoreDBErrorVisible)
                    )
                );

            dgIncrementalScriptsFiles.DataBindings.Clear();
            dgIncrementalScriptsFiles.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    dgIncrementalScriptsFiles,
                    nameof(dgIncrementalScriptsFiles.Enabled),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.IncrementalScriptsGridEnabled)
                    )
                );

            btnRefresh.DataBindings.Clear();
            btnRefresh.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnRefresh,
                    nameof(btnRefresh.Enabled),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnRefreshEnable)
                    )
                );

            btnRecreateDBFromScratchMain.DataBindings.Clear();
            btnRecreateDBFromScratchMain.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnRecreateDBFromScratchMain,
                    nameof(btnRecreateDBFromScratchMain.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnRecreateDBFromScratchMainVisible)
                    )
                );

            lblRecreateDBFromScratchMain.DataBindings.Clear();
            lblRecreateDBFromScratchMain.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblRecreateDBFromScratchMain,
                    nameof(lblRecreateDBFromScratchMain.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnRecreateDBFromScratchMainVisible)
                    )
                );

            btnRecreateDBFromScratch2.DataBindings.Clear();
            btnRecreateDBFromScratch2.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnRecreateDBFromScratch2,
                    nameof(btnRecreateDBFromScratch2.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnRecreateDBFromScratchSecondaryVisible)
                    )
                );

            lblRecreateDBFromScratch2.DataBindings.Clear();
            lblRecreateDBFromScratch2.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblRecreateDBFromScratch2,
                    nameof(lblRecreateDBFromScratch2.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnRecreateDBFromScratchSecondaryVisible)
                    )
                );

            btnDeploy.DataBindings.Clear();
            btnDeploy.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnDeploy,
                    nameof(btnDeploy.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnDeployVisible)
                    )
                );

            lblDeploy.DataBindings.Clear();
            lblDeploy.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblDeploy,
                    nameof(lblDeploy.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnDeployVisible)
                    )
                );

            btnShowHistoricalBackups.DataBindings.Clear();
            btnShowHistoricalBackups.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnShowHistoricalBackups,
                    nameof(btnShowHistoricalBackups.Enabled),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnShowHistoricalBackupsEnabled)
                    )
                );

            btnCreateNewIncrementalScriptFile.DataBindings.Clear();
            btnCreateNewIncrementalScriptFile.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnCreateNewIncrementalScriptFile,
                    nameof(btnCreateNewIncrementalScriptFile.Enabled),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnCreateNewIncrementalScriptFileEnabled)
                    )
                );

            btnCreateNewRepeatableScriptFile.DataBindings.Clear();
            btnCreateNewRepeatableScriptFile.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnCreateNewRepeatableScriptFile,
                    nameof(btnCreateNewRepeatableScriptFile.Enabled),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnCreateNewRepeatableScriptFileEnabled)
                    )
                );

            btnCreateNewDevDummyDataScriptFile.DataBindings.Clear();
            btnCreateNewDevDummyDataScriptFile.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnCreateNewDevDummyDataScriptFile,
                    nameof(btnCreateNewDevDummyDataScriptFile.Enabled),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnCreateNewDevDummyDataScriptFileEnabled)
                    )
                );

            lblIncColorTargetState_Square.DataBindings.Clear();
            lblIncColorTargetState_Square.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblIncColorTargetState_Square,
                    nameof(lblIncColorTargetState_Square.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblColorTargetStateSquareVisible)
                    )
                );

            lblIncColorTargetState_Caption.DataBindings.Clear();
            lblIncColorTargetState_Caption.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblIncColorTargetState_Caption,
                    nameof(lblIncColorTargetState_Caption.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblColorTargetStateCaptionVisible)
                    )
                );
            lblRptColorTargetState_Square.DataBindings.Clear();
            lblRptColorTargetState_Square.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblRptColorTargetState_Square,
                    nameof(lblRptColorTargetState_Square.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblColorTargetStateSquareVisible)
                    )
                );

            lblRptColorTargetState_Caption.DataBindings.Clear();
            lblRptColorTargetState_Caption.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblRptColorTargetState_Caption,
                    nameof(lblRptColorTargetState_Caption.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblColorTargetStateCaptionVisible)
                    )
                );
            lblDDDColorTargetState_Square.DataBindings.Clear();
            lblDDDColorTargetState_Square.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblDDDColorTargetState_Square,
                    nameof(lblDDDColorTargetState_Square.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblColorTargetStateSquareVisible)
                    )
                );

            lblDDDColorTargetState_Caption.DataBindings.Clear();
            lblDDDColorTargetState_Caption.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblDDDColorTargetState_Caption,
                    nameof(lblDDDColorTargetState_Caption.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblColorTargetStateCaptionVisible)
                    )
                );


            lblIncNumOfExecuted.DataBindings.Clear();
            lblIncNumOfExecuted.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblIncNumOfExecuted,
                    nameof(lblIncNumOfExecuted.Text),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblIncNumOfExecutedText)
                    )
                );
            lblIncNumOfVirtual.DataBindings.Clear();
            lblIncNumOfVirtual.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblIncNumOfVirtual,
                    nameof(lblIncNumOfVirtual.Text),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblIncNumOfVirtualText)
                    )
                );
            lblIncNumOfChanged.DataBindings.Clear();
            lblIncNumOfChanged.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblIncNumOfChanged,
                    nameof(lblIncNumOfChanged.Text),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblIncNumOfChangedText)
                    )
                );

            lblRptNumOfExecuted.DataBindings.Clear();
            lblRptNumOfExecuted.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblRptNumOfExecuted,
                    nameof(lblRptNumOfExecuted.Text),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblRptNumOfExecutedText)
                    )
                );
            lblRptNumOfVirtual.DataBindings.Clear();
            lblRptNumOfVirtual.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblRptNumOfVirtual,
                    nameof(lblRptNumOfVirtual.Text),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblRptNumOfVirtualText)
                    )
                );
            lblRptNumOfChanged.DataBindings.Clear();
            lblRptNumOfChanged.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblRptNumOfChanged,
                    nameof(lblRptNumOfChanged.Text),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblRptNumOfChangedText)
                    )
                );

            lblDDDNumOfExecuted.DataBindings.Clear();
            lblDDDNumOfExecuted.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblDDDNumOfExecuted,
                    nameof(lblDDDNumOfExecuted.Text),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblDDDNumOfExecutedText)
                    )
                );
            lblDDDNumOfVirtual.DataBindings.Clear();
            lblDDDNumOfVirtual.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblDDDNumOfVirtual,
                    nameof(lblDDDNumOfVirtual.Text),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblDDDNumOfVirtualText)
                    )
                );
            lblDDDNumOfChanged.DataBindings.Clear();
            lblDDDNumOfChanged.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblDDDNumOfChanged,
                    nameof(lblDDDNumOfChanged.Text),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblDDDNumOfChangedText)
                    )
                );
        }



        #region Refresh


        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            ViewModel.RefreshAllCommand.Execute();
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
            ViewModel.ShowHistoricalBackupsCommand.Execute();
        }

        private void BtnCancelSyncSpecificState_Click(object sender, EventArgs e)
        {
            ViewModel.CancelSyncToSpecificStateCommand.Execute();
        }

        private void BtnSetDBToSpecificState_Click(object sender, EventArgs e)
        {
            ViewModel.SetDBToSpecificStateCommand.Execute();
        }



        private void BtnNavToEdit_Click(object sender, EventArgs e)
        {
            ViewModel.NavToEditProjectConfigCommand.Execute();
        }


        private void DgIncrementalScriptsFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                RuntimeScriptFileBase currScriptFileInfo = (dgIncrementalScriptsFiles.DataSource as List<RuntimeScriptFileBase>)[e.RowIndex];

                Task task = ViewModel.SelectTargetIncScriptFileNameCommand.ExecuteWrapped(currScriptFileInfo.Filename);
                task.Wait();
                MarkUnMarkSelectedTargetInGrid(dgIncrementalScriptsFiles, ViewModel.DBVersionsViewModelData.TargetIncScriptFileName, true);
            }
        }

        private void DgRepeatableScriptsFiles_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                RuntimeScriptFileBase currScriptFileInfo = (dgRepeatableScriptsFiles.DataSource as List<RuntimeScriptFileBase>)[e.RowIndex];

                Task task = ViewModel.SelectTargetRptScriptFileNameCommand.ExecuteWrapped(currScriptFileInfo.Filename);
                task.Wait();

                MarkUnMarkSelectedTargetInGrid(dgRepeatableScriptsFiles, ViewModel.DBVersionsViewModelData.TargetRptScriptFileName, true);
            }
        }

        private void DgDevDummyDataScriptsFiles_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                RuntimeScriptFileBase currScriptFileInfo = (dgDevDummyDataScriptsFiles.DataSource as List<RuntimeScriptFileBase>)[e.RowIndex];

                Task task = ViewModel.SelectTargetDDDScriptFileNameCommand.ExecuteWrapped(currScriptFileInfo.Filename);
                task.Wait();

                MarkUnMarkSelectedTargetInGrid(dgDevDummyDataScriptsFiles, ViewModel.DBVersionsViewModelData.TargetDDDScriptFileName, true);
            }
        }


        private void BtnCancelSetDBStateManually_Click(object sender, EventArgs e)
        {
            ViewModel.CancelStateByVirtualExecutionViewStateCommand.Execute();
        }


        private void BtnOpenIncrementalScriptsFolder_Click(object sender, EventArgs e)
        {
            ViewModel.OpenIncrementalScriptsFolderCommand.Execute();
        }
        private void BtnCreateNewIncrementalScriptFile_Click(object sender, EventArgs e)
        {
            ViewModel.CreateNewIncrementalScriptFileCommand.Execute();
        }

        private void BtnOpenRepeatableScriptsFolder_Click(object sender, EventArgs e)
        {
            ViewModel.OpenRepeatableScriptsFolderCommand.Execute();
        }
        private void BtnCreateNewRepeatableScriptFile_Click(object sender, EventArgs e)
        {
            ViewModel.CreateNewRepeatableScriptFileCommand.Execute();
        }


        private void BtnOpenDevDummyDataScriptsFolder_Click(object sender, EventArgs e)
        {
            ViewModel.OpenDevDummyDataScriptsFolderCommand.Execute();
        }
        private void BtnCreateNewDevDummyDataScriptFile_Click(object sender, EventArgs e)
        {
            ViewModel.CreateNewDevDummyDataScriptFileCommand.Execute();

        }
        #endregion



        #region Run Process

        private void BtnRunSync_Click(object sender, EventArgs e)
        {
            ViewModel.RunSyncCommand.Execute();
        }


        private void BtnRecreateDBFromScratchMain_Click(object sender, EventArgs e)
        {
            ViewModel.RecreateDBFromScratchCommand.Execute();
        }


        private void BtnApplySyncSpecificState_Click(object sender, EventArgs e)
        {
            ViewModel.ApplySyncSpecificStateCommand.Execute();
        }


        private void BtnDeploy_Click(object sender, EventArgs e)
        {
            ViewModel.DeployCommand.Execute();
        }

        private void BtnVirtualExecution_Click(object sender, EventArgs e)
        {
            ViewModel.StateByVirtualExecutionViewStateCommand.Execute();
        }

        private void BtnRecreateDBFromScratch2_Click(object sender, EventArgs e)
        {
            ViewModel.RecreateDBFromScratchCommand.Execute();
        }




        private void BtnRunSetDBStateManally_Click(object sender, EventArgs e)
        {
            ViewModel.RunStateByVirtualExecutionCommand.Execute();
        }



        #endregion




        #region Set View State


        private static void BindGridDataSource(DataGridView dataGridView, IList<RuntimeScriptFileBase> scriptFilesList)
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
                        HashDiffType.EqualVirtual => Color.LightSeaGreen,
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
            if (dgIncrementalScriptsFiles.InvokeRequired)
            {
                dgIncrementalScriptsFiles.BeginInvoke((MethodInvoker)(() =>
                {
                    MarkUnMarkSelectedTargetInGrid(dgIncrementalScriptsFiles, ViewModel.DBVersionsViewModelData.TargetIncScriptFileName, isEnable);

                    if (isEnable && dgIncrementalScriptsFiles.RowCount > 0)
                    {
                        dgIncrementalScriptsFiles.FirstDisplayedScrollingRowIndex = 0;
                    }
                }));
            }
            else
            {
                MarkUnMarkSelectedTargetInGrid(dgIncrementalScriptsFiles, ViewModel.DBVersionsViewModelData.TargetIncScriptFileName, isEnable);

                if (isEnable && dgIncrementalScriptsFiles.RowCount > 0)
                {
                    dgIncrementalScriptsFiles.FirstDisplayedScrollingRowIndex = 0;
                }
            }

            if (dgRepeatableScriptsFiles.InvokeRequired)
            {
                dgRepeatableScriptsFiles.BeginInvoke((MethodInvoker)(() =>
                {
                    MarkUnMarkSelectedTargetInGrid(dgRepeatableScriptsFiles, ViewModel.DBVersionsViewModelData.TargetRptScriptFileName, isEnable);

                    if (isEnable && dgRepeatableScriptsFiles.RowCount > 0)
                    {
                        dgRepeatableScriptsFiles.FirstDisplayedScrollingRowIndex = 0;
                    }
                }));
            }
            else
            {
                MarkUnMarkSelectedTargetInGrid(dgRepeatableScriptsFiles, ViewModel.DBVersionsViewModelData.TargetRptScriptFileName, isEnable);

                if (isEnable && dgRepeatableScriptsFiles.RowCount > 0)
                {
                    dgRepeatableScriptsFiles.FirstDisplayedScrollingRowIndex = 0;
                }

            }

            if (dgDevDummyDataScriptsFiles.InvokeRequired)
            {
                dgDevDummyDataScriptsFiles.BeginInvoke((MethodInvoker)(() =>
                {
                    MarkUnMarkSelectedTargetInGrid(dgDevDummyDataScriptsFiles, ViewModel.DBVersionsViewModelData.TargetDDDScriptFileName, isEnable);

                    if (isEnable && dgDevDummyDataScriptsFiles.RowCount > 0)
                    {
                        dgDevDummyDataScriptsFiles.FirstDisplayedScrollingRowIndex = 0;
                    }

                }));
            }
            else
            {
                MarkUnMarkSelectedTargetInGrid(dgDevDummyDataScriptsFiles, ViewModel.DBVersionsViewModelData.TargetDDDScriptFileName, isEnable);

                if (isEnable && dgDevDummyDataScriptsFiles.RowCount > 0)
                {
                    dgDevDummyDataScriptsFiles.FirstDisplayedScrollingRowIndex = 0;
                }

            }

        }

        private static void MarkUnMarkSelectedTargetInGrid(DataGridView dgGrid, string targetScriptFileName, bool isEnable)
        {
            dgGrid.Columns[2].Visible = isEnable;

            foreach (DataGridViewRow currGridRow in dgGrid.Rows)
            {
                currGridRow.Cells[0].Value = (currGridRow.Index + 1).ToString(CultureInfo.InvariantCulture);
                currGridRow.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                RuntimeScriptFileBase currRowFileInfo = currGridRow.DataBoundItem as RuntimeScriptFileBase;

                if (targetScriptFileName != null
                    && currRowFileInfo.Filename.Trim().ToUpperInvariant() == targetScriptFileName.Trim().ToUpperInvariant())
                {
                    currGridRow.Cells[2].Style.BackColor = Color.Yellow;
                }
                else
                {
                    currGridRow.Cells[2].Style.BackColor = Color.White;
                }
            }

            

        }












        #endregion


        #region Dispose

        // To detect redundant calls
        private bool _disposed;

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
                //AutoVersionsDBAPI.Dispose();

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
