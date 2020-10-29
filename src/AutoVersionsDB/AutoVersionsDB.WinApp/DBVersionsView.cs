using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI;
using AutoVersionsDB.UI.DBVersions;
using AutoVersionsDB.WinApp.Utils;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
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
                ViewModel.OnException += ViewModel_OnException;
                ViewModel.OnConfirm += ViewModel_OnConfirm;
                ViewModel.OnTextInput += ViewModel_OnTextInput;
                ViewModel.PropertyChanged += _viewModel_PropertyChanged;
                ViewModel.DBVersionsViewModelData.PropertyChanged += DBVersionsViewModelData_PropertyChanged;
                ViewModel.DBVersionsControls.PropertyChanged += DBVersionsControls_PropertyChanged;
                SetDataBindings();


                EnableDisableGridToSelectTargetState(ViewModel.DBVersionsControls.GridToSelectTargetStateEnabled);
            }


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
                tooltipControl.SetToolTip(this.btnRefresh, ViewModel.DBVersionsControls.BtnRefreshTooltip);
                tooltipControl.SetToolTip(this.btnRunSync, ViewModel.DBVersionsControls.BtnRunSyncTooltip);
                tooltipControl.SetToolTip(this.btnRecreateDbFromScratchMain, ViewModel.DBVersionsControls.BtnRecreateDbFromScratchMainTooltip);
                tooltipControl.SetToolTip(this.btnRecreateDbFromScratch2, ViewModel.DBVersionsControls.BtnRecreateDbFromScratchMainTooltip);
                tooltipControl.SetToolTip(this.btnDeploy, ViewModel.DBVersionsControls.BtnDeployTooltip);
                tooltipControl.SetToolTip(this.btnSetDBToSpecificState, ViewModel.DBVersionsControls.BtnSetDBToSpecificStateTooltip);
                tooltipControl.SetToolTip(this.btnVirtualExecution, ViewModel.DBVersionsControls.BtnVirtualExecutionTooltip);
                tooltipControl.SetToolTip(this.btnShowHistoricalBackups, ViewModel.DBVersionsControls.BtnShowHistoricalBackupsTooltip);
            }

        }

        private void ViewModel_OnException(object sender, string exceptionMessage)
        {
            MessageBox.Show(exceptionMessage);
        }
        private bool ViewModel_OnConfirm(object sender, string confirmMessage)
        {
            return MessageBox.Show(this, confirmMessage, "Pay Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes;
        }

        private TextInputResults ViewModel_OnTextInput(object sender, string instructionMessageText)
        {
            TextInputWindow textInputWindow = NinjectUtils_Winform.NinjectKernelContainer.Get<TextInputWindow>();
            textInputWindow.ShowDialog();

            return new TextInputResults()
            {
                ResultText = textInputWindow.ResultText,
                IsApply = textInputWindow.IsApply,
            };
        }


        private void ViewModel_OnShowTextInput(object sender, EventArgs e)
        {
        }




        private void _viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }


        private void SetDataBindings()
        {
            this.lblProjectName.DataBindings.Clear();
            this.lblProjectName.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblProjectName,
                    nameof(lblProjectName.Text),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblProjectNameText)
                    )
                );


            this.pnlMainActions.DataBindings.Clear();
            this.pnlMainActions.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlMainActions,
                    nameof(pnlMainActions.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.PnlMainActionsVisible)
                    )
                );

            this.pnlSyncToSpecificState.DataBindings.Clear();
            this.pnlSyncToSpecificState.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlSyncToSpecificState,
                    nameof(pnlSyncToSpecificState.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.PnlSyncToSpecificStateVisible)
                    )
                );

            this.pnlMissingSystemTables.DataBindings.Clear();
            this.pnlMissingSystemTables.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlMissingSystemTables,
                    nameof(pnlMissingSystemTables.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.PnlMissingSystemTablesVisible)
                    )
                );
            this.pnlMissingSystemTables.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlMissingSystemTables,
                    nameof(pnlMissingSystemTables.Enabled),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.PnlMissingSystemTablesEnabled)
                    )
                );

            this.pnlSetDBStateManually.DataBindings.Clear();
            this.pnlSetDBStateManually.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlSetDBStateManually,
                    nameof(pnlSetDBStateManually.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.PnlSetDBStateManuallyVisible)
                    )
                );
            this.pnlSetDBStateManually.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlSetDBStateManually,
                    nameof(pnlSetDBStateManually.Enabled),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.PnlSetDBStateManuallyEnabled)
                    )
                );

            this.pnlDevDummyDataFiles.DataBindings.Clear();
            this.pnlDevDummyDataFiles.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlDevDummyDataFiles,
                    nameof(pnlDevDummyDataFiles.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.PnlDevDummyDataFilesVisible)
                    )
                );

            this.pnlRestoreDbError.DataBindings.Clear();
            this.pnlRestoreDbError.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlRestoreDbError,
                    nameof(pnlRestoreDbError.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.PnlRestoreDbErrorVisible)
                    )
                );

            this.dgIncrementalScriptsFiles.DataBindings.Clear();
            this.dgIncrementalScriptsFiles.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    dgIncrementalScriptsFiles,
                    nameof(dgIncrementalScriptsFiles.Enabled),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.IncrementalScriptsGridEnabled)
                    )
                );

            this.btnRefresh.DataBindings.Clear();
            this.btnRefresh.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnRefresh,
                    nameof(btnRefresh.Enabled),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnRefreshEnable)
                    )
                );

            this.btnRecreateDbFromScratchMain.DataBindings.Clear();
            this.btnRecreateDbFromScratchMain.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnRecreateDbFromScratchMain,
                    nameof(btnRecreateDbFromScratchMain.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnRecreateDbFromScratchMainVisible)
                    )
                );

            this.lblRecreateDbFromScratchMain.DataBindings.Clear();
            this.lblRecreateDbFromScratchMain.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblRecreateDbFromScratchMain,
                    nameof(lblRecreateDbFromScratchMain.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnRecreateDbFromScratchMainVisible)
                    )
                );

            this.btnRecreateDbFromScratch2.DataBindings.Clear();
            this.btnRecreateDbFromScratch2.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnRecreateDbFromScratch2,
                    nameof(btnRecreateDbFromScratch2.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnRecreateDbFromScratchSecondaryVisible)
                    )
                );

            this.lblRecreateDbFromScratch2.DataBindings.Clear();
            this.lblRecreateDbFromScratch2.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblRecreateDbFromScratch2,
                    nameof(lblRecreateDbFromScratch2.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnRecreateDbFromScratchSecondaryVisible)
                    )
                );

            this.btnDeploy.DataBindings.Clear();
            this.btnDeploy.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnDeploy,
                    nameof(btnDeploy.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnDeployVisible)
                    )
                );

            this.lblDeploy.DataBindings.Clear();
            this.lblDeploy.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblDeploy,
                    nameof(lblDeploy.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnDeployVisible)
                    )
                );

            this.btnShowHistoricalBackups.DataBindings.Clear();
            this.btnShowHistoricalBackups.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnShowHistoricalBackups,
                    nameof(btnShowHistoricalBackups.Enabled),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnShowHistoricalBackupsEnabled)
                    )
                );

            this.btnCreateNewIncrementalScriptFile.DataBindings.Clear();
            this.btnCreateNewIncrementalScriptFile.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnCreateNewIncrementalScriptFile,
                    nameof(btnCreateNewIncrementalScriptFile.Enabled),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnCreateNewIncrementalScriptFileEnabled)
                    )
                );

            this.btnCreateNewRepeatableScriptFile.DataBindings.Clear();
            this.btnCreateNewRepeatableScriptFile.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnCreateNewRepeatableScriptFile,
                    nameof(btnCreateNewRepeatableScriptFile.Enabled),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnCreateNewRepeatableScriptFileEnabled)
                    )
                );

            this.btnCreateNewDevDummyDataScriptFile.DataBindings.Clear();
            this.btnCreateNewDevDummyDataScriptFile.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnCreateNewDevDummyDataScriptFile,
                    nameof(btnCreateNewDevDummyDataScriptFile.Enabled),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.BtnCreateNewDevDummyDataScriptFileEnabled)
                    )
                );

            this.lblColorTargetState_Square.DataBindings.Clear();
            this.lblColorTargetState_Square.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblColorTargetState_Square,
                    nameof(lblColorTargetState_Square.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblColorTargetState_SquareVisible)
                    )
                );

            this.lblColorTargetState_Caption.DataBindings.Clear();
            this.lblColorTargetState_Caption.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblColorTargetState_Caption,
                    nameof(lblColorTargetState_Caption.Visible),
                    ViewModel.DBVersionsControls,
                    nameof(ViewModel.DBVersionsControls.LblColorTargetState_CaptionVisible)
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
            ViewModel.NavToChooseProjectCommand.Execute();
        }


        private void DgScriptFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                RuntimeScriptFileBase currScriptFileInfo = (dgIncrementalScriptsFiles.DataSource as List<RuntimeScriptFileBase>)[e.RowIndex];

                ViewModel.SelectTargetStateScriptFileNameCommand.Execute(currScriptFileInfo.Filename);

                //MarkUnMarkSelectedTargetInGrid();
            }

        }

        private void BtnCancelSetDBStateManually_Click(object sender, EventArgs e)
        {
            ViewModel.CancelSetDBStateManuallyCommand.Execute();
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


        private void BtnRecreateDbFromScratchMain_Click(object sender, EventArgs e)
        {
            ViewModel.RecreateDbFromScratchCommand.Execute();
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
            ViewModel.SetDBStateManuallyViewStateCommand.Execute();
        }

        private void BtnRecreateDbFromScratch2_Click(object sender, EventArgs e)
        {
            ViewModel.RecreateDbFromScratchCommand.Execute();
        }




        private void BtnRunSetDBStateManally_Click(object sender, EventArgs e)
        {
            ViewModel.RunSetDBStateManallyCommand.Execute();
        }



        #endregion




        #region Set View State


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
            if (dgIncrementalScriptsFiles.InvokeRequired)
            {
                dgIncrementalScriptsFiles.BeginInvoke((MethodInvoker)(() =>
                {
                    MarkUnMarkSelectedTargetInGrid(isEnable);
                }));
            }
            else
            {
                MarkUnMarkSelectedTargetInGrid(isEnable);
            }
        }

        private void MarkUnMarkSelectedTargetInGrid(bool isEnable)
        {
            dgIncrementalScriptsFiles.Columns[2].Visible = isEnable;

            foreach (DataGridViewRow currGridRow in dgIncrementalScriptsFiles.Rows)
            {
                currGridRow.Cells[0].Value = (currGridRow.Index + 1).ToString(CultureInfo.InvariantCulture);
                currGridRow.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                RuntimeScriptFileBase currRowFileInfo = currGridRow.DataBoundItem as RuntimeScriptFileBase;

                if (currRowFileInfo.Filename.Trim().ToUpperInvariant() == ViewModel.DBVersionsViewModelData.TargetStateScriptFileName.Trim().ToUpperInvariant())
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
