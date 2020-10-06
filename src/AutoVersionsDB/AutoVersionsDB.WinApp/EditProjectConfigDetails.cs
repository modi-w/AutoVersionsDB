using System;
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

        private string _id;

        public enum EditProjectConfigDetailsViewType
        {
            InPorcess,
            New,
            Update,
            EditId,
        }

        private EditProjectConfigDetailsViewType _viewType;


        public event OnNavToProcessHandler OnNavToProcess;



        public EditProjectConfigDetails()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _dbTypesList = AutoVersionsDBAPI.GetDbTypesList();
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
            _id = null;
           
            notificationsControl1.Clear();

            ClearUIElementsErrors();

            ProjectConfigItem newProjectConfigItem = new ProjectConfigItem();
            newProjectConfigItem.DevEnvironment = true;
            newProjectConfigItem.SetDefaltValues();

            BindToUIElements(newProjectConfigItem);


            ChangeViewType(EditProjectConfigDetailsViewType.New);
        }


        public void SetProjectConfigItem(string id)
        {
            _id = id;

            RefreshForm();
        }

        private void RefreshForm()
        {
            ProjectConfigItem projectConfig = AutoVersionsDBAPI.GetProjectConfigById(_id);
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

                ProcessResults processResults = AutoVersionsDBAPI.ValidateProjectConfig(_id, notificationsControl1.OnNotificationStateChanged);

                handleProcessErrors(processResults.Trace);
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

            if (string.IsNullOrWhiteSpace(_id))
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
                        case "IdMandatory":

                            SetErrorInErrorProvider(tbId, errorStateItem.LowLevelErrorMessage);
                            break;

                        case "DBType":

                            SetErrorInErrorProvider(cboConncectionType, errorStateItem.LowLevelErrorMessage);
                            break;

                        case "DBName":

                            SetErrorInErrorProvider(tbDBName, errorStateItem.LowLevelErrorMessage);
                            break;

                        //case "ConnStr":

                        //    SetErrorInErrorProvider(tbConnStr, errorStateItem.LowLevelErrorMessage);
                        //    break;

                        //case "ConnStrToMasterDB":

                        //    SetErrorInErrorProvider(tbConnStrToMasterDB, errorStateItem.LowLevelErrorMessage);
                        //    break;

                        case "DBBackupFolderPath":

                            SetErrorInErrorProvider(tbDBBackupFolder, errorStateItem.LowLevelErrorMessage);
                            break;

                        case "DeliveryArtifactFolderPath":

                            SetErrorInErrorProvider(tbDeliveryArtifactFolderPath, errorStateItem.LowLevelErrorMessage);
                            break;

                        case "DeployArtifactFolderPath":

                            SetErrorInErrorProvider(tbDeployArtifactFolderPath, errorStateItem.LowLevelErrorMessage);
                            break;

                        case "DevScriptsBaseFolder":

                            SetErrorInErrorProvider(tbDevScriptsFolderPath, errorStateItem.LowLevelErrorMessage);
                            break;



                    }
                }
            }
        }

        private void ClearUIElementsErrors()
        {
            SetErrorInErrorProvider(tbId, null);
            SetErrorInErrorProvider(cboConncectionType, null);
            SetErrorInErrorProvider(tbServer, null);
            SetErrorInErrorProvider(tbDBName, null);
            SetErrorInErrorProvider(tbUsername, null);
            SetErrorInErrorProvider(tbPassword, null);
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

            if (!string.IsNullOrWhiteSpace(projectConfig.DBType))
            {
                DBType currSelectedItem = _dbTypesList.FirstOrDefault(e => e.Code == projectConfig.DBType);
                if (currSelectedItem != null)
                {
                    cboConncectionType.BeginInvoke((MethodInvoker)(() =>
                    {
                        cboConncectionType.SelectedIndex = cboConncectionType.Items.IndexOf(currSelectedItem);
                    }));
                }
            }

            tbId.BeginInvoke((MethodInvoker)(() =>
            {
                tbId.Text = projectConfig.Id;
            }));
            tbProjectDescription.BeginInvoke((MethodInvoker)(() =>
            {
                tbProjectDescription.Text = projectConfig.Description;
            }));
            tbServer.BeginInvoke((MethodInvoker)(() =>
            {
                tbServer.Text = projectConfig.Server;
            }));
            tbDBName.BeginInvoke((MethodInvoker)(() =>
            {
                tbDBName.Text = projectConfig.DBName;
            }));
            tbUsername.BeginInvoke((MethodInvoker)(() =>
            {
                tbUsername.Text = projectConfig.Username;
            }));
            tbPassword.BeginInvoke((MethodInvoker)(() =>
            {
                tbPassword.Text = projectConfig.Password;
            }));


            tbDBBackupFolder.BeginInvoke((MethodInvoker)(() =>
            {
                tbDBBackupFolder.Text = projectConfig.BackupFolderPath;
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
                rbDevEnv.Checked = projectConfig.DevEnvironment;
            }));
            rbDelEnv.BeginInvoke((MethodInvoker)(() =>
            {
                rbDelEnv.Checked = !projectConfig.DevEnvironment;
            }));


         

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

            projectConfig.Id = tbId.Text;
            projectConfig.Description = tbProjectDescription.Text;
            projectConfig.DBType = Convert.ToString(cboConncectionType.SelectedValue, CultureInfo.InvariantCulture);
            projectConfig.Server = tbServer.Text;
            projectConfig.DBName = tbDBName.Text;
            projectConfig.Username = tbUsername.Text;
            projectConfig.Password = tbPassword.Text;
            projectConfig.BackupFolderPath = tbDBBackupFolder.Text;

            projectConfig.DevEnvironment = rbDevEnv.Checked;

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
            OnNavToProcess?.Invoke(_id);
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            ChangeViewType(EditProjectConfigDetailsViewType.InPorcess);


            ProjectConfigItem projectConfig = BindFromUIElements();

            Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(_id))
                {
                    ProcessResults processResults = AutoVersionsDBAPI.SaveNewProjectConfig(projectConfig, notificationsControl1.OnNotificationStateChanged);

                    if (!processResults.Trace.HasError)
                    {
                        _id = projectConfig.Id;
                    }

                    handleCompleteProcess(processResults.Trace);

                }
                else
                {
                    ProcessResults processResults = AutoVersionsDBAPI.UpdateProjectConfig(projectConfig, notificationsControl1.OnNotificationStateChanged);

                    handleCompleteProcess(processResults.Trace);
                }
            });
        }


        private void btnEditId_Click(object sender, EventArgs e)
        {
            ChangeViewType(EditProjectConfigDetailsViewType.EditId);
        }
        private void btnCancelEditId_Click(object sender, EventArgs e)
        {
            RefreshForm();
        }
        private void btnSaveId_Click(object sender, EventArgs e)
        {
            ChangeViewType(EditProjectConfigDetailsViewType.InPorcess);


            Task.Run(() =>
            {
                ProcessResults processResults = AutoVersionsDBAPI.ChangeProjectId(_id, tbId.Text, notificationsControl1.OnNotificationStateChanged);

                if (!processResults.Trace.HasError)
                {
                    _id = tbId.Text;
                }


                handleCompleteProcess(processResults.Trace);
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

                    SetControlVisableOrHide(btnEditId, false);
                    SetControlVisableOrHide(btnSaveId, false);
                    SetControlVisableOrHide(btnCancelEditId, false);

                    break;

                case EditProjectConfigDetailsViewType.Update:

                    SetAllControlsEnableDisable(true);

                    SetControlEnableOrDisable(tbId, false);

                    SetControlVisableOrHide(btnSaveId, false);
                    SetControlVisableOrHide(btnCancelEditId, false);
                    SetControlVisableOrHide(btnEditId, true);
                    SetControlEnableOrDisable(btnEditId, true);

                    this.BeginInvoke((MethodInvoker)(() =>
                    {
                        this.VerticalScroll.Value = 0;
                    }));

                    break;

                case EditProjectConfigDetailsViewType.EditId:

                    SetAllControlsEnableDisable(false);

                    SetControlEnableOrDisable(tbId, true);

                    SetControlVisableOrHide(btnEditId, false);
                    SetControlVisableOrHide(btnSaveId, true);
                    SetControlEnableOrDisable(btnSaveId, true);
                    SetControlVisableOrHide(btnCancelEditId, true);
                    SetControlEnableOrDisable(btnCancelEditId, true);


                    tbId.Focus();


                    break;
                default:
                    break;
            }
        }


        private void SetAllControlsEnableDisable(bool isEnable)
        {
            SetControlEnableOrDisable(btnNavToProcess, isEnable);
            SetControlEnableOrDisable(cboConncectionType, isEnable);
            SetControlEnableOrDisable(tbServer, isEnable);
            SetControlEnableOrDisable(tbDBName, isEnable);
            SetControlEnableOrDisable(tbUsername, isEnable);
            SetControlEnableOrDisable(tbPassword, isEnable);
            SetControlEnableOrDisable(tbDevScriptsFolderPath, isEnable);
            SetControlEnableOrDisable(tbDBBackupFolder, isEnable);
            SetControlEnableOrDisable(tbId, isEnable);
            SetControlEnableOrDisable(rbDevEnv, isEnable);
            SetControlEnableOrDisable(rbDelEnv, isEnable);
            SetControlEnableOrDisable(tbDeployArtifactFolderPath, isEnable);
            SetControlEnableOrDisable(tbDeliveryArtifactFolderPath, isEnable);
            SetControlEnableOrDisable(btnSave, isEnable);
            SetControlEnableOrDisable(tbProjectDescription, isEnable);
            SetControlEnableOrDisable(btnSaveId, isEnable);
            SetControlEnableOrDisable(btnEditId, isEnable);
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
