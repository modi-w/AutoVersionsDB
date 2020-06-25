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
        private readonly List<IDBCommandsFactory> _dbCommandsFactoryList;

        public event OnNavToProcessHandler OnNavToProcess;


        private readonly AutoVersionsDbAPI _autoVersionsDbAPI = null;



        public EditProjectConfigDetails()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _autoVersionsDbAPI = AutoVersionsDbAPI.Instance;

                _dbCommandsFactoryList = _autoVersionsDbAPI.DBCommandsFactoryProvider.DBCommandsFactoryDictionary.Values.ToList();
                cboConncectionType.DataSource = _dbCommandsFactoryList;
                cboConncectionType.DisplayMember = "DBTypeName";
                cboConncectionType.ValueMember = "DBTypeCode";
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
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfigItem);

            BindToUIElements();

            ValidateAll();
        }


        #region Validation

        private bool ValidateAll()
        {
            notificationsControl1.Clear();

            ClearUIElementsErrors();

            _autoVersionsDbAPI.ValidateProjectConfig();

            notificationsControl1.AfterComplete();

            SetErrorsToUiElements();

            imgError.BeginInvoke((MethodInvoker)(() =>
            {
                imgError.Visible = _autoVersionsDbAPI.HasError;
            }));

            imgValid.BeginInvoke((MethodInvoker)(() =>
            {
                imgValid.Visible = !_autoVersionsDbAPI.HasError;
            }));
            btnNavToProcess.BeginInvoke((MethodInvoker)(() =>
            {
                btnNavToProcess.Visible = !_autoVersionsDbAPI.HasError;
            }));
            lblDbProcess.BeginInvoke((MethodInvoker)(() =>
            {
                lblDbProcess.Visible = !_autoVersionsDbAPI.HasError;
            }));

            return !_autoVersionsDbAPI.HasError;
        }

        private void SetErrorsToUiElements()
        {
            if (_autoVersionsDbAPI.NotificationExecutersFactoryManager.HasError)
            {
                List<NotificationStateItem> errorStates = _autoVersionsDbAPI.NotificationExecutersFactoryManager.NotifictionStatesHistoryManager.NotificationStatesProcessHistory.Where(e => e.HasError).ToList();

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

            if (!string.IsNullOrWhiteSpace(_autoVersionsDbAPI.ProjectConfigItem.DBTypeCode))
            {
                IDBCommandsFactory currSelectedItem = _dbCommandsFactoryList.FirstOrDefault(e => e.DBTypeCode == _autoVersionsDbAPI.ProjectConfigItem.DBTypeCode);
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
                tbProjectName.Text = _autoVersionsDbAPI.ProjectConfigItem.ProjectName;
            }));
            lblProjectGuid.BeginInvoke((MethodInvoker)(() =>
            {
                lblProjectGuid.Text = _autoVersionsDbAPI.ProjectConfigItem.ProjectGuid;
            }));
            tbConnStr.BeginInvoke((MethodInvoker)(() =>
            {
                tbConnStr.Text = _autoVersionsDbAPI.ProjectConfigItem.ConnStr;
            }));
            tbConnStrToMasterDB.BeginInvoke((MethodInvoker)(() =>
            {
                tbConnStrToMasterDB.Text = _autoVersionsDbAPI.ProjectConfigItem.ConnStrToMasterDB;
            }));
            tbDBBackupFolder.BeginInvoke((MethodInvoker)(() =>
            {
                tbDBBackupFolder.Text = _autoVersionsDbAPI.ProjectConfigItem.DBBackupBaseFolder;
            }));

            tbDevScriptsFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                tbDevScriptsFolderPath.Text = _autoVersionsDbAPI.ProjectConfigItem.DevScriptsBaseFolderPath;
            }));
            tbDeployArtifactFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                tbDeployArtifactFolderPath.Text = _autoVersionsDbAPI.ProjectConfigItem.DeployArtifactFolderPath;
            }));
            tbDeliveryArtifactFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                tbDeliveryArtifactFolderPath.Text = _autoVersionsDbAPI.ProjectConfigItem.DeliveryArtifactFolderPath;
            }));


            rbDevEnv.BeginInvoke((MethodInvoker)(() =>
            {
                rbDevEnv.Checked = _autoVersionsDbAPI.ProjectConfigItem.IsDevEnvironment;
            }));
            rbDelEnv.BeginInvoke((MethodInvoker)(() =>
            {
                rbDelEnv.Checked = !_autoVersionsDbAPI.ProjectConfigItem.IsDevEnvironment;
            }));


            if (_autoVersionsDbAPI.ProjectConfigItem.DBCommandsTimeout > 0)
            {
                tbConnectionTimeout.BeginInvoke((MethodInvoker)(() =>
                {
                    tbConnectionTimeout.Text = _autoVersionsDbAPI.ProjectConfigItem.DBCommandsTimeout.ToString(CultureInfo.InvariantCulture);
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
                lbllncrementalScriptsFolderPath.Text = _autoVersionsDbAPI.ProjectConfigItem.IncrementalScriptsFolderPath;
            }));
            lblRepeatableScriptsFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                lblRepeatableScriptsFolderPath.Text = _autoVersionsDbAPI.ProjectConfigItem.RepeatableScriptsFolderPath;
            }));
            lblDevDummyDataScriptsFolderPath.BeginInvoke((MethodInvoker)(() =>
            {
                lblDevDummyDataScriptsFolderPath.Text = _autoVersionsDbAPI.ProjectConfigItem.DevDummyDataScriptsFolderPath;
            }));
        }

        #endregion

        #region Binding To UIElements

        private void BindFromUIElements()
        {
            _autoVersionsDbAPI.ProjectConfigItem.ProjectName = tbProjectName.Text;
            _autoVersionsDbAPI.ProjectConfigItem.DBTypeCode = Convert.ToString(cboConncectionType.SelectedValue, CultureInfo.InvariantCulture);
            _autoVersionsDbAPI.ProjectConfigItem.ConnStr = tbConnStr.Text;
            _autoVersionsDbAPI.ProjectConfigItem.ConnStrToMasterDB = tbConnStrToMasterDB.Text;
            _autoVersionsDbAPI.ProjectConfigItem.DBBackupBaseFolder = tbDBBackupFolder.Text;

            _autoVersionsDbAPI.ProjectConfigItem.IsDevEnvironment = rbDevEnv.Checked;

            //    AutoVersionsDbAPI.ProjectConfigItem.IsDevEnvironment = chkAllowDropDB.Checked;

            if (int.TryParse(tbConnectionTimeout.Text, out int parsedInt)
                && parsedInt > 0)
            {
                _autoVersionsDbAPI.ProjectConfigItem.DBCommandsTimeout = parsedInt;
            }

            _autoVersionsDbAPI.ProjectConfigItem.DevScriptsBaseFolderPath = tbDevScriptsFolderPath.Text;
            _autoVersionsDbAPI.ProjectConfigItem.DeployArtifactFolderPath = tbDeployArtifactFolderPath.Text;
            _autoVersionsDbAPI.ProjectConfigItem.DeliveryArtifactFolderPath = tbDeliveryArtifactFolderPath.Text;
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
            _autoVersionsDbAPI.ProjectConfigItem.DevScriptsBaseFolderPath = tbDevScriptsFolderPath.Text;
            BindScriptsPathLabels();
        }

        private void BtnNavToProcess_Click(object sender, EventArgs e)
        {
            OnNavToProcess?.Invoke(_autoVersionsDbAPI.ProjectConfigItem);
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
                _autoVersionsDbAPI.SaveProjectConfig();
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
                _autoVersionsDbAPI.Dispose();

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
