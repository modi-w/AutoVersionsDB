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

        private ProjectConfigItem _projectConfig;
        private bool _isNewProjectConfig;

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

        public void SetProjectConfigItem(ProjectConfigItem projectConfigItem)
        {
            _projectConfig = projectConfigItem;

            BindToUIElements();

            ValidateAll();
        }

        public void CreateNewProjectConfig()
        {
            _projectConfig = new ProjectConfigItem();
            _isNewProjectConfig = true;
        }


        #region Validation

        private bool ValidateAll()
        {
            notificationsControl1.Clear();

            ClearUIElementsErrors();

            ProcessTrace processResults = AutoVersionsDbAPI.ValidateProjectConfig(_projectConfig.ProjectCode, notificationsControl1.OnNotificationStateChanged);

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

        private void BindToUIElements()
        {
            cboConncectionType.BeginInvoke((MethodInvoker)(() =>
            {
                cboConncectionType.SelectedIndex = -1;
            }));

            if (!string.IsNullOrWhiteSpace(_projectConfig.DBTypeCode))
            {
                DBType currSelectedItem = _dbTypesList.FirstOrDefault(e => e.Code == _projectConfig.DBTypeCode);
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
                tbProjectCode.Text = _projectConfig.ProjectCode;
            }));
            //lblProjectGuid.BeginInvoke((MethodInvoker)(() =>
            //{
            //    lblProjectGuid.Text = _projectConfigItem.ProjectGuid;
            //}));
            tbConnStr.BeginInvoke((MethodInvoker)(() =>
            {
                tbConnStr.Text = _projectConfig.ConnStr;
            }));
            tbConnStrToMasterDB.BeginInvoke((MethodInvoker)(() =>
            {
                tbConnStrToMasterDB.Text = _projectConfig.ConnStrToMasterDB;
            }));
            tbDBBackupFolder.BeginInvoke((MethodInvoker)(() =>
            {
                tbDBBackupFolder.Text = _projectConfig.DBBackupBaseFolder;
            }));

            tbDevScriptsFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                tbDevScriptsFolderPath.Text = _projectConfig.DevScriptsBaseFolderPath;
            }));
            tbDeployArtifactFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                tbDeployArtifactFolderPath.Text = _projectConfig.DeployArtifactFolderPath;
            }));
            tbDeliveryArtifactFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                tbDeliveryArtifactFolderPath.Text = _projectConfig.DeliveryArtifactFolderPath;
            }));


            rbDevEnv.BeginInvoke((MethodInvoker)(() =>
            {
                rbDevEnv.Checked = _projectConfig.IsDevEnvironment;
            }));
            rbDelEnv.BeginInvoke((MethodInvoker)(() =>
            {
                rbDelEnv.Checked = !_projectConfig.IsDevEnvironment;
            }));


            if (_projectConfig.DBCommandsTimeout > 0)
            {
                tbConnectionTimeout.BeginInvoke((MethodInvoker)(() =>
                {
                    tbConnectionTimeout.Text = _projectConfig.DBCommandsTimeout.ToString(CultureInfo.InvariantCulture);
                }));
            }

            BindScriptsPathLabels();

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

        private void BindScriptsPathLabels()
        {
            lbllncrementalScriptsFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                lbllncrementalScriptsFolderPath.Text = _projectConfig.IncrementalScriptsFolderPath;
            }));
            lblRepeatableScriptsFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                lblRepeatableScriptsFolderPath.Text = _projectConfig.RepeatableScriptsFolderPath;
            }));
            lblDevDummyDataScriptsFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                lblDevDummyDataScriptsFolderPath.Text = _projectConfig.DevDummyDataScriptsFolderPath;
            }));
        }

        #endregion

        #region Binding To UIElements

        private void BindFromUIElements()
        {
            _projectConfig.ProjectCode = tbProjectCode.Text;
            _projectConfig.DBTypeCode = Convert.ToString(cboConncectionType.SelectedValue, CultureInfo.InvariantCulture);
            _projectConfig.ConnStr = tbConnStr.Text;
            _projectConfig.ConnStrToMasterDB = tbConnStrToMasterDB.Text;
            _projectConfig.DBBackupBaseFolder = tbDBBackupFolder.Text;

            _projectConfig.IsDevEnvironment = rbDevEnv.Checked;

            //    _projectConfigItem.IsDevEnvironment = chkAllowDropDB.Checked;

            if (int.TryParse(tbConnectionTimeout.Text, out int parsedInt)
                && parsedInt > 0)
            {
                _projectConfig.DBCommandsTimeout = parsedInt;
            }

            _projectConfig.DevScriptsBaseFolderPath = tbDevScriptsFolderPath.Text;
            _projectConfig.DeployArtifactFolderPath = tbDeployArtifactFolderPath.Text;
            _projectConfig.DeliveryArtifactFolderPath = tbDeliveryArtifactFolderPath.Text;
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
            _projectConfig.DevScriptsBaseFolderPath = tbDevScriptsFolderPath.Text;
            BindScriptsPathLabels();
        }

        private void BtnNavToProcess_Click(object sender, EventArgs e)
        {
            OnNavToProcess?.Invoke(_projectConfig);
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {

            btnSave.BeginInvoke((MethodInvoker)(() =>
            {
                btnSave.Enabled = false;
            }));


            BindFromUIElements();

            Task.Run(() =>
            {
                if (_isNewProjectConfig)
                {
                    ProcessTrace processTrace = AutoVersionsDbAPI.SaveNewProjectConfig(_projectConfig, notificationsControl1.OnNotificationStateChanged);

                    if (!processTrace.HasError)
                    {
                        _isNewProjectConfig = false;
                    }

                    handleCompleteProcess(processTrace);

                }
                else
                {
                    AutoVersionsDbAPI.UpdateProjectConfig(_projectConfig, notificationsControl1.OnNotificationStateChanged);

                    ValidateAll();
                }
              

                btnSave.BeginInvoke((MethodInvoker)(() =>
                {
                    btnSave.Enabled = true;
                }));
            });
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
