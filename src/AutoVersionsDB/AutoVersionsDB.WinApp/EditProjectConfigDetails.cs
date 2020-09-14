using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;
using System.Threading.Tasks;
using System.Globalization;

namespace AutoVersionsDB.WinApp
{

    public partial class EditProjectConfigDetails : UserControl
    {
        private readonly List<DBType> _dbTypesList;

        private string _projectCode;

        public enum EditProjectConfigDetailsViewType
        {
            InPorcess,
            New,
            Update,
            EditProjectCode,
        }

        private EditProjectConfigDetailsViewType _viewType;


        public event OnNavToProcessHandler OnNavToProcess;



        public EditProjectConfigDetails()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _dbTypesList = AutoVersionsDbAPI.GetDbTypesList();
                cboConncectionType.DataSource = _dbTypesList;
                cboConncectionType.DisplayMember = "Name";
                cboConncectionType.ValueMember = "Code";
            }

            errPrvProjectDetails.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            btnNavToProcess.Visible = false;
            lblDbProcess.Visible = false;

            imgError.Location = new Point(imgValid.Location.X, imgValid.Location.Y);
            pnlDelEnvFields.Location = new Point(pnlDevEnvFoldersFields.Location.X, pnlDevEnvFoldersFields.Location.Y);

            imgValid.Visible = false;
            imgError.Visible = false;
        }

        public void CreateNewProjectConfig()
        {
            _projectCode = null;
            ChangeViewType(EditProjectConfigDetailsViewType.New);
        }


        public void SetProjectConfigItem(string projectCode)
        {
            _projectCode = projectCode;

            RefreshForm();
        }

        private void RefreshForm()
        {
            ProjectConfigItem projectConfig = AutoVersionsDbAPI.GetProjectConfigByProjectCode(_projectCode);
            BindToUIElements(projectConfig);

            ValidateAll();

            ChangeViewType(EditProjectConfigDetailsViewType.Update);
        }



        #region Validation

        private void ValidateAll()
        {
            ChangeViewType(EditProjectConfigDetailsViewType.InPorcess);

            Task.Run(() =>
            {
                notificationsControl1.Clear();

                ClearUIElementsErrors();

                ProcessTrace processResults = AutoVersionsDbAPI.ValidateProjectConfig(_projectCode, notificationsControl1.OnNotificationStateChanged);

                handleProcessErrors(processResults);
            });
        }

        private void handleCompleteProcess(ProcessTrace processResults)
        {
            if (processResults.HasError)
            {
                handleProcessErrors(processResults);
            }
            else
            {
                RefreshForm();
            }
        }


        private void handleProcessErrors(ProcessTrace processResults)
        {
            notificationsControl1.AfterComplete();

            SetErrorsToUiElements(processResults);

            SetControlVisableOrHide(imgError, processResults.HasError);
            SetControlVisableOrHide(imgValid, !processResults.HasError);
            SetControlVisableOrHide(btnNavToProcess, !processResults.HasError);
            SetControlVisableOrHide(lblDbProcess, !processResults.HasError);

            if (string.IsNullOrWhiteSpace(_projectCode))
            {
                ChangeViewType(EditProjectConfigDetailsViewType.New);
            }
            else
            {
                ChangeViewType(EditProjectConfigDetailsViewType.Update);
            }
        }

        private void SetErrorsToUiElements(ProcessTrace processResults)
        {
            if (processResults.HasError)
            {
                List<StepNotificationState> errorStates = processResults.StatesHistory.Where(e => e.HasError).ToList();

                foreach (StepNotificationState errorStateItem in errorStates)
                {
                    switch (errorStateItem.LowLevelErrorCode)
                    {
                        case "ProjectCode":

                            SetErrorInErrorProvider(tbProjectCode, errorStateItem.LowLevelErrorMessage);
                            break;

                        case "DBTypeCode":

                            SetErrorInErrorProvider(cboConncectionType, errorStateItem.LowLevelErrorMessage);
                            break;

                        case "ConnStr":

                            SetErrorInErrorProvider(tbConnStr, errorStateItem.LowLevelErrorMessage);
                            break;

                        case "ConnStrToMasterDB":

                            SetErrorInErrorProvider(tbConnStrToMasterDB, errorStateItem.LowLevelErrorMessage);
                            break;

                        case "DBBackupFolderPath":

                            SetErrorInErrorProvider(tbDBBackupFolder, errorStateItem.LowLevelErrorMessage);
                            break;

                        case "DeliveryArtifactFolderPath":

                            SetErrorInErrorProvider(tbDeliveryArtifactFolderPath, errorStateItem.LowLevelErrorMessage);
                            break;

                        case "DeployArtifactFolderPath":

                            SetErrorInErrorProvider(tbDeployArtifactFolderPath, errorStateItem.LowLevelErrorMessage);
                            break;

                        case "ScriptsFolderPath":

                            SetErrorInErrorProvider(tbDevScriptsFolderPath, errorStateItem.LowLevelErrorMessage);
                            break;

                        case "InitStateFilePath":

                            SetErrorInErrorProvider(lbllncrementalScriptsFolderPath, errorStateItem.LowLevelErrorMessage);
                            break;


                    }
                }
            }
        }

        private void ClearUIElementsErrors()
        {
            SetErrorInErrorProvider(tbProjectCode, null);
            SetErrorInErrorProvider(cboConncectionType, null);
            SetErrorInErrorProvider(tbConnStr, null);
            SetErrorInErrorProvider(tbConnStrToMasterDB, null);
            SetErrorInErrorProvider(tbDBBackupFolder, null);
            SetErrorInErrorProvider(tbDevScriptsFolderPath, null);
            SetErrorInErrorProvider(lbllncrementalScriptsFolderPath, null);
        }


        private void SetErrorInErrorProvider(Control control, string message)
        {
            control.BeginInvoke((MethodInvoker)(() =>
            {
                errPrvProjectDetails.SetError(control, message);
            }));
        }



        #endregion


        #region Binding To UIElements

        private void BindToUIElements(ProjectConfigItem projectConfig)
        {
            cboConncectionType.BeginInvoke((MethodInvoker)(() =>
            {
                cboConncectionType.SelectedIndex = -1;
            }));

            if (!string.IsNullOrWhiteSpace(projectConfig.DBTypeCode))
            {
                DBType currSelectedItem = _dbTypesList.FirstOrDefault(e => e.Code == projectConfig.DBTypeCode);
                if (currSelectedItem != null)
                {
                    cboConncectionType.BeginInvoke((MethodInvoker)(() =>
                    {
                        cboConncectionType.SelectedIndex = cboConncectionType.Items.IndexOf(currSelectedItem);
                    }));
                }
            }

            tbProjectCode.BeginInvoke((MethodInvoker)(() =>
            {
                tbProjectCode.Text = projectConfig.ProjectCode;
            }));
            tbProjectDescription.BeginInvoke((MethodInvoker)(() =>
            {
                tbProjectDescription.Text = projectConfig.ProjectDescription;
            }));
            tbConnStr.BeginInvoke((MethodInvoker)(() =>
            {
                tbConnStr.Text = projectConfig.ConnStr;
            }));
            tbConnStrToMasterDB.BeginInvoke((MethodInvoker)(() =>
            {
                tbConnStrToMasterDB.Text = projectConfig.ConnStrToMasterDB;
            }));
            tbDBBackupFolder.BeginInvoke((MethodInvoker)(() =>
            {
                tbDBBackupFolder.Text = projectConfig.DBBackupBaseFolder;
            }));

            tbDevScriptsFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                tbDevScriptsFolderPath.Text = projectConfig.DevScriptsBaseFolderPath;
            }));
            tbDeployArtifactFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                tbDeployArtifactFolderPath.Text = projectConfig.DeployArtifactFolderPath;
            }));
            tbDeliveryArtifactFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                tbDeliveryArtifactFolderPath.Text = projectConfig.DeliveryArtifactFolderPath;
            }));


            rbDevEnv.BeginInvoke((MethodInvoker)(() =>
            {
                rbDevEnv.Checked = projectConfig.IsDevEnvironment;
            }));
            rbDelEnv.BeginInvoke((MethodInvoker)(() =>
            {
                rbDelEnv.Checked = !projectConfig.IsDevEnvironment;
            }));


            if (projectConfig.DBCommandsTimeout > 0)
            {
                tbConnectionTimeout.BeginInvoke((MethodInvoker)(() =>
                {
                    tbConnectionTimeout.Text = projectConfig.DBCommandsTimeout.ToString(CultureInfo.InvariantCulture);
                }));
            }

            BindScriptsPathLabels(projectConfig);

            ResolveShowDevEnvAndDelEnvFileds();
        }


        private void ResolveShowDevEnvAndDelEnvFileds()
        {
            pnlDevEnvFoldersFields.BeginInvoke((MethodInvoker)(() =>
            {
                pnlDevEnvFoldersFields.Visible = rbDevEnv.Checked;
            }));
            pnlDevEnvDeplyFolder.BeginInvoke((MethodInvoker)(() =>
            {
                pnlDevEnvDeplyFolder.Visible = rbDevEnv.Checked;
            }));
            pnlDelEnvFields.BeginInvoke((MethodInvoker)(() =>
            {
                pnlDelEnvFields.Visible = !rbDevEnv.Checked;
            }));
        }

        private void BindScriptsPathLabels(ProjectConfigItem projectConfig)
        {
            lbllncrementalScriptsFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                lbllncrementalScriptsFolderPath.Text = projectConfig.IncrementalScriptsFolderPath;
            }));
            lblRepeatableScriptsFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                lblRepeatableScriptsFolderPath.Text = projectConfig.RepeatableScriptsFolderPath;
            }));
            lblDevDummyDataScriptsFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                lblDevDummyDataScriptsFolderPath.Text = projectConfig.DevDummyDataScriptsFolderPath;
            }));
        }

        #endregion

        #region Binding From UIElements

        private ProjectConfigItem BindFromUIElements()
        {
            ProjectConfigItem projectConfig = new ProjectConfigItem();

            projectConfig.ProjectCode = tbProjectCode.Text;
            projectConfig.ProjectDescription = tbProjectDescription.Text;
            projectConfig.DBTypeCode = Convert.ToString(cboConncectionType.SelectedValue, CultureInfo.InvariantCulture);
            projectConfig.ConnStr = tbConnStr.Text;
            projectConfig.ConnStrToMasterDB = tbConnStrToMasterDB.Text;
            projectConfig.DBBackupBaseFolder = tbDBBackupFolder.Text;

            projectConfig.IsDevEnvironment = rbDevEnv.Checked;

            //    _projectConfigItem.IsDevEnvironment = chkAllowDropDB.Checked;

            if (int.TryParse(tbConnectionTimeout.Text, out int parsedInt)
                && parsedInt > 0)
            {
                projectConfig.DBCommandsTimeout = parsedInt;
            }

            projectConfig.DevScriptsBaseFolderPath = tbDevScriptsFolderPath.Text;
            projectConfig.DeployArtifactFolderPath = tbDeployArtifactFolderPath.Text;
            projectConfig.DeliveryArtifactFolderPath = tbDeliveryArtifactFolderPath.Text;

            return projectConfig;
        }



        #endregion

        private void RbDevEnv_CheckedChanged(object sender, EventArgs e)
        {
            ResolveShowDevEnvAndDelEnvFileds();
        }

        private void RbDelEnv_CheckedChanged(object sender, EventArgs e)
        {
            ResolveShowDevEnvAndDelEnvFileds();
        }


        private void TbScriptsRootFolderPath_TextChanged(object sender, EventArgs e)
        {
            ProjectConfigItem projectConfig = new ProjectConfigItem();
            projectConfig.DevScriptsBaseFolderPath = tbDevScriptsFolderPath.Text;
            BindScriptsPathLabels(projectConfig);
        }

        private void BtnNavToProcess_Click(object sender, EventArgs e)
        {
            OnNavToProcess?.Invoke(_projectCode);
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            ChangeViewType(EditProjectConfigDetailsViewType.InPorcess);


            ProjectConfigItem projectConfig = BindFromUIElements();

            Task.Run(() =>
            {
                if (_viewType == EditProjectConfigDetailsViewType.New)
                {
                    ProcessTrace processResults = AutoVersionsDbAPI.SaveNewProjectConfig(projectConfig, notificationsControl1.OnNotificationStateChanged);

                    if (!processResults.HasError)
                    {
                        _projectCode = projectConfig.ProjectCode;
                    }

                    handleCompleteProcess(processResults);

                }
                else
                {
                    ProcessTrace processResults = AutoVersionsDbAPI.UpdateProjectConfig(projectConfig, notificationsControl1.OnNotificationStateChanged);

                    handleCompleteProcess(processResults);
                }
            });
        }


        private void btnEditProjectCode_Click(object sender, EventArgs e)
        {
            ChangeViewType(EditProjectConfigDetailsViewType.EditProjectCode);
        }
        private void btnCancelEditProjectCode_Click(object sender, EventArgs e)
        {
            RefreshForm();
        }
        private void btnSaveProjectCode_Click(object sender, EventArgs e)
        {
            ChangeViewType(EditProjectConfigDetailsViewType.InPorcess);


            Task.Run(() =>
            {
                ProcessTrace processResults = AutoVersionsDbAPI.ChangeProjectCode(_projectCode, tbProjectCode.Text, notificationsControl1.OnNotificationStateChanged);

                if (!processResults.HasError)
                {
                    _projectCode = tbProjectCode.Text;
                }


                handleCompleteProcess(processResults);
            });
        }


        private void ChangeViewType(EditProjectConfigDetailsViewType viewType)
        {
            _viewType = viewType;

            switch (_viewType)
            {
                case EditProjectConfigDetailsViewType.InPorcess:

                    SetAllControlsEnableDisable(false);
                    break;

                case EditProjectConfigDetailsViewType.New:

                    SetAllControlsEnableDisable(true);

                    SetControlVisableOrHide(btnEditProjectCode, false);
                    SetControlVisableOrHide(btnSaveProjectCode, false);
                    SetControlVisableOrHide(btnCancelEditProjectCode, false);

                    break;

                case EditProjectConfigDetailsViewType.Update:

                    SetAllControlsEnableDisable(true);

                    SetControlEnableOrDisable(tbProjectCode, false);

                    SetControlVisableOrHide(btnSaveProjectCode, false);
                    SetControlVisableOrHide(btnCancelEditProjectCode, false);
                    SetControlVisableOrHide(btnEditProjectCode, true);
                    SetControlEnableOrDisable(btnEditProjectCode, true);

                    this.BeginInvoke((MethodInvoker)(() =>
                    {
                        this.VerticalScroll.Value = 0;
                    }));

                    break;

                case EditProjectConfigDetailsViewType.EditProjectCode:

                    SetAllControlsEnableDisable(false);

                    SetControlEnableOrDisable(tbProjectCode, true);

                    SetControlVisableOrHide(btnEditProjectCode, false);
                    SetControlVisableOrHide(btnSaveProjectCode, true);
                    SetControlEnableOrDisable(btnSaveProjectCode, true);
                    SetControlVisableOrHide(btnCancelEditProjectCode, true);
                    SetControlEnableOrDisable(btnCancelEditProjectCode, true);


                    tbProjectCode.Focus();


                    break;
                default:
                    break;
            }
        }


        private void SetAllControlsEnableDisable(bool isEnable)
        {
            SetControlEnableOrDisable(tbConnectionTimeout, isEnable);
            SetControlEnableOrDisable(btnNavToProcess, isEnable);
            SetControlEnableOrDisable(tbConnStrToMasterDB, isEnable);
            SetControlEnableOrDisable(cboConncectionType, isEnable);
            SetControlEnableOrDisable(tbDevScriptsFolderPath, isEnable);
            SetControlEnableOrDisable(tbDBBackupFolder, isEnable);
            SetControlEnableOrDisable(tbConnStr, isEnable);
            SetControlEnableOrDisable(tbProjectCode, isEnable);
            SetControlEnableOrDisable(rbDevEnv, isEnable);
            SetControlEnableOrDisable(rbDelEnv, isEnable);
            SetControlEnableOrDisable(tbDeployArtifactFolderPath, isEnable);
            SetControlEnableOrDisable(tbDeliveryArtifactFolderPath, isEnable);
            SetControlEnableOrDisable(btnSave, isEnable);
            SetControlEnableOrDisable(tbProjectDescription, isEnable);
            SetControlEnableOrDisable(btnSaveProjectCode, isEnable);
            SetControlEnableOrDisable(btnEditProjectCode, isEnable);
        }

        private static void SetControlEnableOrDisable(Control control, bool isEnable)
        {
            control.BeginInvoke((MethodInvoker)(() =>
            {
                control.Enabled = isEnable;
            }));
        }

        private static void SetControlVisableOrHide(Control control, bool isVisible)
        {
            control.BeginInvoke((MethodInvoker)(() =>
            {
                control.Visible = isVisible;
            }));
        }


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
