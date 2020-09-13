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

        public void SetProjectConfigItem(string projectCode)
        {
            _projectCode = projectCode;

            ProjectConfigItem projectConfig = AutoVersionsDbAPI.GetProjectConfigByProjectCode(projectCode);
            BindToUIElements(projectConfig);

            ValidateAll(_projectCode);

            _viewType = EditProjectConfigDetailsViewType.Update;
            viewTypeChanged();
        }

        public void CreateNewProjectConfig()
        {
            _viewType = EditProjectConfigDetailsViewType.New;
            viewTypeChanged();
        }


        #region Validation

        private bool ValidateAll(string projectCode)
        {
            notificationsControl1.Clear();

            ClearUIElementsErrors();

            ProcessTrace processResults = AutoVersionsDbAPI.ValidateProjectConfig(projectCode, notificationsControl1.OnNotificationStateChanged);

            handleCompleteProcess(processResults);

            return !processResults.HasError;
        }

        private void handleCompleteProcess(ProcessTrace processResults)
        {
            notificationsControl1.AfterComplete();

            SetErrorsToUiElements(processResults);

            imgError.BeginInvoke((MethodInvoker)(() =>
            {
                imgError.Visible = processResults.HasError;
            }));

            imgValid.BeginInvoke((MethodInvoker)(() =>
            {
                imgValid.Visible = !processResults.HasError;
            }));
            btnNavToProcess.BeginInvoke((MethodInvoker)(() =>
            {
                btnNavToProcess.Visible = !processResults.HasError;
            }));
            lblDbProcess.BeginInvoke((MethodInvoker)(() =>
            {
                lblDbProcess.Visible = !processResults.HasError;
            }));
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
            //lblProjectGuid.BeginInvoke((MethodInvoker)(() =>
            //{
            //    lblProjectGuid.Text = _projectConfigItem.ProjectGuid;
            //}));
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

            btnSave.BeginInvoke((MethodInvoker)(() =>
            {
                btnSave.Enabled = false;
            }));


            ProjectConfigItem projectConfig = BindFromUIElements();

            Task.Run(() =>
            {
                if (_viewType == EditProjectConfigDetailsViewType.New)
                {
                    ProcessTrace processTrace = AutoVersionsDbAPI.SaveNewProjectConfig(projectConfig, notificationsControl1.OnNotificationStateChanged);

                    handleCompleteProcess(processTrace);

                    if (!processTrace.HasError)
                    {
                        _viewType = EditProjectConfigDetailsViewType.Update;
                        viewTypeChanged();
                    }
                }
                else
                {
                    ProcessTrace processTrace = AutoVersionsDbAPI.UpdateProjectConfig(projectConfig, notificationsControl1.OnNotificationStateChanged);

                    handleCompleteProcess(processTrace);
                }


                btnSave.BeginInvoke((MethodInvoker)(() =>
                {
                    btnSave.Enabled = true;
                }));
            });
        }


        private void btnEditProjectCode_Click(object sender, EventArgs e)
        {
            _viewType = EditProjectConfigDetailsViewType.EditProjectCode;
            viewTypeChanged();
        }

        private void btnSaveProjectCode_Click(object sender, EventArgs e)
        {
            btnSave.BeginInvoke((MethodInvoker)(() =>
            {
                btnSave.Enabled = false;
            }));


            Task.Run(() =>
            {
                ProcessTrace processTrace = AutoVersionsDbAPI.ChangeProjectCode(_projectCode, tbProjectCode.Text, notificationsControl1.OnNotificationStateChanged);

                handleCompleteProcess(processTrace);

                btnSave.BeginInvoke((MethodInvoker)(() =>
                {
                    btnSave.Enabled = true;
                }));
            });
        }


        private void viewTypeChanged()
        {
            switch (_viewType)
            {
                case EditProjectConfigDetailsViewType.New:

                    tbProjectCode.BeginInvoke((MethodInvoker)(() =>
                    {
                        tbProjectCode.Enabled = true;
                    }));
                    btnEditProjectCode.BeginInvoke((MethodInvoker)(() =>
                    {
                        btnEditProjectCode.Visible = false;
                    }));
                    btnSaveProjectCode.BeginInvoke((MethodInvoker)(() =>
                    {
                        btnSaveProjectCode.Visible = false;
                    }));

                    btnSave.BeginInvoke((MethodInvoker)(() =>
                    {
                        btnSave.Enabled = true;
                    }));

                    break;

                case EditProjectConfigDetailsViewType.Update:

                    tbProjectCode.BeginInvoke((MethodInvoker)(() =>
                    {
                        tbProjectCode.Enabled = false;
                    }));
                    btnEditProjectCode.BeginInvoke((MethodInvoker)(() =>
                    {
                        btnEditProjectCode.Visible = true;
                    }));
                    btnSaveProjectCode.BeginInvoke((MethodInvoker)(() =>
                    {
                        btnSaveProjectCode.Visible = false;
                    }));

                    btnSave.BeginInvoke((MethodInvoker)(() =>
                    {
                        btnSave.Enabled = true;
                    }));

                    break;

                case EditProjectConfigDetailsViewType.EditProjectCode:

                    tbProjectCode.BeginInvoke((MethodInvoker)(() =>
                    {
                        tbProjectCode.Enabled = true;
                    }));
                    btnEditProjectCode.BeginInvoke((MethodInvoker)(() =>
                    {
                        btnEditProjectCode.Visible = false;
                    }));
                    btnSaveProjectCode.BeginInvoke((MethodInvoker)(() =>
                    {
                        btnSaveProjectCode.Visible = true;
                    }));

                    btnSave.BeginInvoke((MethodInvoker)(() =>
                    {
                        btnSave.Enabled = false;
                    }));


                    break;
                default:
                    break;
            }
        }


        private void SetAllControlsEnableDisable()
        {

        }

        private static void SetControlEnableDisable(Control control, bool isEnable)
        {
            control.BeginInvoke((MethodInvoker)(() =>
            {
                control.Visible = isEnable;
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
