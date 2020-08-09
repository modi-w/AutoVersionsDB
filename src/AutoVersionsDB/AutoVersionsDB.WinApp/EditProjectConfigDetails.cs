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
       
        private ProjectConfig _projectConfigItem;

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

        public void SetProjectConfigItem(ProjectConfig projectConfigItem)
        {
            _projectConfigItem = projectConfigItem;

            BindToUIElements();

            ValidateAll();
        }


        #region Validation

        private bool ValidateAll()
        {
            notificationsControl1.Clear();

            ClearUIElementsErrors();

            ProcessTrace processResults = AutoVersionsDbAPI.ValidateProjectConfig(_projectConfigItem,notificationsControl1.OnNotificationStateChanged);

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

            return !processResults.HasError;
        }

        private void SetErrorsToUiElements(ProcessTrace processResults)
        {
            if (processResults.HasError)
            {
                List<NotificationStateItem> errorStates = processResults.StatesHistory.Where(e => e.HasError).ToList();

                foreach (NotificationStateItem errorStateItem in errorStates)
                {
                    switch (errorStateItem.LowLevelErrorCode)
                    {
                        case "ProjectName":

                            SetErrorInErrorProvider(tbProjectName, errorStateItem.LowLevelErrorMessage);
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
            SetErrorInErrorProvider(tbProjectName, null);
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

            if (!string.IsNullOrWhiteSpace(_projectConfigItem.DBTypeCode))
            {
                DBType currSelectedItem = _dbTypesList.FirstOrDefault(e => e.Code == _projectConfigItem.DBTypeCode);
                if (currSelectedItem != null)
                {
                    cboConncectionType.BeginInvoke((MethodInvoker)(() =>
                    {
                        cboConncectionType.SelectedIndex = cboConncectionType.Items.IndexOf(currSelectedItem);
                    }));
                }
            }

            tbProjectName.BeginInvoke((MethodInvoker)(() =>
            {
                tbProjectName.Text = _projectConfigItem.ProjectName;
            }));
            lblProjectGuid.BeginInvoke((MethodInvoker)(() =>
            {
                lblProjectGuid.Text = _projectConfigItem.ProjectGuid;
            }));
            tbConnStr.BeginInvoke((MethodInvoker)(() =>
            {
                tbConnStr.Text = _projectConfigItem.ConnStr;
            }));
            tbConnStrToMasterDB.BeginInvoke((MethodInvoker)(() =>
            {
                tbConnStrToMasterDB.Text = _projectConfigItem.ConnStrToMasterDB;
            }));
            tbDBBackupFolder.BeginInvoke((MethodInvoker)(() =>
            {
                tbDBBackupFolder.Text = _projectConfigItem.DBBackupBaseFolder;
            }));

            tbDevScriptsFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                tbDevScriptsFolderPath.Text = _projectConfigItem.DevScriptsBaseFolderPath;
            }));
            tbDeployArtifactFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                tbDeployArtifactFolderPath.Text = _projectConfigItem.DeployArtifactFolderPath;
            }));
            tbDeliveryArtifactFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                tbDeliveryArtifactFolderPath.Text = _projectConfigItem.DeliveryArtifactFolderPath;
            }));


            rbDevEnv.BeginInvoke((MethodInvoker)(() =>
            {
                rbDevEnv.Checked = _projectConfigItem.IsDevEnvironment;
            }));
            rbDelEnv.BeginInvoke((MethodInvoker)(() =>
            {
                rbDelEnv.Checked = !_projectConfigItem.IsDevEnvironment;
            }));


            if (_projectConfigItem.DBCommandsTimeout > 0)
            {
                tbConnectionTimeout.BeginInvoke((MethodInvoker)(() =>
                {
                    tbConnectionTimeout.Text = _projectConfigItem.DBCommandsTimeout.ToString(CultureInfo.InvariantCulture);
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
                lbllncrementalScriptsFolderPath.Text = _projectConfigItem.IncrementalScriptsFolderPath;
            }));
            lblRepeatableScriptsFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                lblRepeatableScriptsFolderPath.Text = _projectConfigItem.RepeatableScriptsFolderPath;
            }));
            lblDevDummyDataScriptsFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                lblDevDummyDataScriptsFolderPath.Text = _projectConfigItem.DevDummyDataScriptsFolderPath;
            }));
        }

        #endregion

        #region Binding To UIElements

        private void BindFromUIElements()
        {
            _projectConfigItem.ProjectName = tbProjectName.Text;
            _projectConfigItem.DBTypeCode = Convert.ToString(cboConncectionType.SelectedValue, CultureInfo.InvariantCulture);
            _projectConfigItem.ConnStr = tbConnStr.Text;
            _projectConfigItem.ConnStrToMasterDB = tbConnStrToMasterDB.Text;
            _projectConfigItem.DBBackupBaseFolder = tbDBBackupFolder.Text;

            _projectConfigItem.IsDevEnvironment = rbDevEnv.Checked;

            //    _projectConfigItem.IsDevEnvironment = chkAllowDropDB.Checked;

            if (int.TryParse(tbConnectionTimeout.Text, out int parsedInt)
                && parsedInt > 0)
            {
                _projectConfigItem.DBCommandsTimeout = parsedInt;
            }

            _projectConfigItem.DevScriptsBaseFolderPath = tbDevScriptsFolderPath.Text;
            _projectConfigItem.DeployArtifactFolderPath = tbDeployArtifactFolderPath.Text;
            _projectConfigItem.DeliveryArtifactFolderPath = tbDeliveryArtifactFolderPath.Text;
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
            _projectConfigItem.DevScriptsBaseFolderPath = tbDevScriptsFolderPath.Text;
            BindScriptsPathLabels();
        }

        private void BtnNavToProcess_Click(object sender, EventArgs e)
        {
            OnNavToProcess?.Invoke(_projectConfigItem);
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
                ValidateAll();
                //if (validateAll())
                //{
                AutoVersionsDbAPI.SaveProjectConfig(_projectConfigItem);
                //}

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
