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
using AutoVersionsDB.UI;
using Ninject;
using AutoVersionsDB.WinApp.Utils;
using AutoVersionsDB.UI.EditProject;

namespace AutoVersionsDB.WinApp
{

    public partial class EditProjectConfigDetails : UserControlNinjectBase
    {
        [Inject]
        public EditProjectViewModel ViewModel { get; set; }



        public EditProjectConfigDetails()
        {
            InitializeComponent();

            this.Load += EditProjectConfigDetails_Load;


            errPrvProjectDetails.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            imgError.Location = new Point(imgValid.Location.X, imgValid.Location.Y);
            pnlDelEnvFields.Location = new Point(pnlDevEnvFoldersFields.Location.X, pnlDevEnvFoldersFields.Location.Y);
        }

        private void EditProjectConfigDetails_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                ViewModel.PropertyChanged += ViewModel_PropertyChanged;
                SetDataBindings();
            }
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            errorMessageChanged(e);

            switch (e.PropertyName)
            {
                case nameof(ViewModel.ProjectConfig):

                    bindProjectConfig();
                    break;

                default:
                    break;
            }
        }

        private void bindProjectConfig()
        {
            this.tbId.DataBindings.Clear();
            this.tbId.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbId,
                    nameof(tbId.Text),
                    ViewModel.ProjectConfig,
                    nameof(ViewModel.ProjectConfig.Id)
                )
            );

            this.tbProjectDescription.DataBindings.Clear();
            this.tbProjectDescription.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbProjectDescription,
                    nameof(tbProjectDescription.Text),
                    ViewModel.ProjectConfig,
                    nameof(ViewModel.ProjectConfig.Description)
                )
            );


            this.cboConncectionType.DataBindings.Clear();
            this.cboConncectionType.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    cboConncectionType,
                    nameof(cboConncectionType.SelectedValue),
                    ViewModel.ProjectConfig,
                    nameof(ViewModel.ProjectConfig.DBType)
                )
            );

            this.tbServer.DataBindings.Clear();
            this.tbServer.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbServer,
                    nameof(tbServer.Text),
                    ViewModel.ProjectConfig,
                    nameof(ViewModel.ProjectConfig.Server)
                )
            );

            this.tbDBName.DataBindings.Clear();
            this.tbDBName.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbDBName,
                    nameof(tbServer.Text),
                    ViewModel.ProjectConfig,
                    nameof(ViewModel.ProjectConfig.DBName)
                )
            );

            this.tbUsername.DataBindings.Clear();
            this.tbUsername.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbUsername,
                    nameof(tbServer.Text),
                    ViewModel.ProjectConfig,
                    nameof(ViewModel.ProjectConfig.Username)
                )
            );

            this.tbPassword.DataBindings.Clear();
            this.tbPassword.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbPassword,
                    nameof(tbPassword.Text),
                    ViewModel.ProjectConfig,
                    nameof(ViewModel.ProjectConfig.Password)
                )
            );

            this.tbDBBackupFolder.DataBindings.Clear();
            this.tbDBBackupFolder.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbDBBackupFolder,
                    nameof(tbDBBackupFolder.Text),
                    ViewModel.ProjectConfig,
                    nameof(ViewModel.ProjectConfig.BackupFolderPath)
                )
            );

            this.rbDevEnv.DataBindings.Clear();
            this.rbDevEnv.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    rbDevEnv,
                    nameof(rbDevEnv.Checked),
                    ViewModel.ProjectConfig,
                    nameof(ViewModel.ProjectConfig.DevEnvironment)
                )
            );

            this.rbDelEnv.DataBindings.Clear();
            this.rbDelEnv.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    rbDelEnv,
                    nameof(rbDelEnv.Checked),
                    ViewModel.ProjectConfig,
                    nameof(ViewModel.ProjectConfig.DeliveryEnvironment)
                )
            );

            this.tbDevScriptsFolderPath.DataBindings.Clear();
            this.tbDevScriptsFolderPath.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbDevScriptsFolderPath,
                    nameof(tbDevScriptsFolderPath.Text),
                    ViewModel.ProjectConfig,
                    nameof(ViewModel.ProjectConfig.DevScriptsBaseFolderPath)
                )
            );

            this.lblIncrementalScriptsFolderPath.DataBindings.Clear();
            this.lblIncrementalScriptsFolderPath.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblIncrementalScriptsFolderPath,
                    nameof(lblIncrementalScriptsFolderPath.Text),
                    ViewModel.ProjectConfig,
                    nameof(ViewModel.ProjectConfig.IncrementalScriptsFolderPath)
                )
            );

            this.lblRepeatableScriptsFolderPath.DataBindings.Clear();
            this.lblRepeatableScriptsFolderPath.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblRepeatableScriptsFolderPath,
                    nameof(lblRepeatableScriptsFolderPath.Text),
                    ViewModel.ProjectConfig,
                    nameof(ViewModel.ProjectConfig.RepeatableScriptsFolderPath)
                )
            );

            this.lblDevDummyDataScriptsFolderPath.DataBindings.Clear();
            this.lblDevDummyDataScriptsFolderPath.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblDevDummyDataScriptsFolderPath,
                    nameof(lblDevDummyDataScriptsFolderPath.Text),
                    ViewModel.ProjectConfig,
                    nameof(ViewModel.ProjectConfig.DevDummyDataScriptsFolderPath)
                )
            );


            this.tbDeployArtifactFolderPath.DataBindings.Clear();
            this.tbDeployArtifactFolderPath.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbDeployArtifactFolderPath,
                    nameof(tbDeployArtifactFolderPath.Text),
                    ViewModel.ProjectConfig,
                    nameof(ViewModel.ProjectConfig.DeployArtifactFolderPath)
                )
            );

            this.tbDeliveryArtifactFolderPath.DataBindings.Clear();
            this.tbDeliveryArtifactFolderPath.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbDeliveryArtifactFolderPath,
                    nameof(tbDeliveryArtifactFolderPath.Text),
                    ViewModel.ProjectConfig,
                    nameof(ViewModel.ProjectConfig.DeliveryArtifactFolderPath)
                )
            );

        }

        private void errorMessageChanged(PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.ProjectConfigErrorMessages.IdErrorMessage):

                    SetErrorInErrorProvider(tbId, ViewModel.ProjectConfigErrorMessages.IdErrorMessage);
                    break;

                case nameof(ViewModel.ProjectConfigErrorMessages.DBTypeCodeErrorMessage):

                    SetErrorInErrorProvider(tbId, ViewModel.ProjectConfigErrorMessages.DBTypeCodeErrorMessage);
                    break;

                case nameof(ViewModel.ProjectConfigErrorMessages.ServerErrorMessage):

                    SetErrorInErrorProvider(tbId, ViewModel.ProjectConfigErrorMessages.ServerErrorMessage);
                    break;

                case nameof(ViewModel.ProjectConfigErrorMessages.DBNameErrorMessage):

                    SetErrorInErrorProvider(tbId, ViewModel.ProjectConfigErrorMessages.DBNameErrorMessage);
                    break;

                case nameof(ViewModel.ProjectConfigErrorMessages.UsernameErrorMessage):

                    SetErrorInErrorProvider(tbId, ViewModel.ProjectConfigErrorMessages.UsernameErrorMessage);
                    break;

                case nameof(ViewModel.ProjectConfigErrorMessages.PasswordErrorMessage):

                    SetErrorInErrorProvider(tbId, ViewModel.ProjectConfigErrorMessages.PasswordErrorMessage);
                    break;

                case nameof(ViewModel.ProjectConfigErrorMessages.BackupFolderPathErrorMessage):

                    SetErrorInErrorProvider(tbId, ViewModel.ProjectConfigErrorMessages.BackupFolderPathErrorMessage);
                    break;

                case nameof(ViewModel.ProjectConfigErrorMessages.DevScriptsBaseFolderPathErrorMessage):

                    SetErrorInErrorProvider(tbId, ViewModel.ProjectConfigErrorMessages.DevScriptsBaseFolderPathErrorMessage);
                    break;

                case nameof(ViewModel.ProjectConfigErrorMessages.DeployArtifactFolderPathErrorMessage):

                    SetErrorInErrorProvider(tbId, ViewModel.ProjectConfigErrorMessages.DeployArtifactFolderPathErrorMessage);
                    break;

                case nameof(ViewModel.ProjectConfig.DeliveryArtifactFolderPath):

                    SetErrorInErrorProvider(tbId, ViewModel.ProjectConfigErrorMessages.DeliveryArtifactFolderPathErrorMessage);
                    break;

                default:
                    break;
            }
        }

        private void SetDataBindings()
        {
            cboConncectionType.DataSource = ViewModel.DBTypes;
            cboConncectionType.DisplayMember = "Name";
            cboConncectionType.ValueMember = "Code";

            this.pnlDevEnvFoldersFields.DataBindings.Clear();
            this.pnlDevEnvFoldersFields.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlDevEnvFoldersFields,
                    nameof(pnlDevEnvFoldersFields.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.PnlDevEnvFoldersFieldsVisible)
                    )
                );

            pnlDevEnvDeplyFolder.DataBindings.Clear();
            pnlDevEnvDeplyFolder.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlDevEnvDeplyFolder,
                    nameof(pnlDevEnvDeplyFolder.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.PnlDevEnvDeplyFolderVisible)
                    )
                );


            this.pnlDelEnvFields.DataBindings.Clear();
            this.pnlDelEnvFields.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlDelEnvFields,
                    nameof(pnlDelEnvFields.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.PnlDelEnvFieldsVisible)
                    )
                );


            this.imgError.DataBindings.Clear();
            this.imgError.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    imgError,
                    nameof(imgError.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.ImgErrorVisible)
                    )
                );


            this.imgValid.DataBindings.Clear();
            this.imgValid.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    imgValid,
                    nameof(imgValid.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.ImgValidVisible)
                    )
                );


            this.btnSave.DataBindings.Clear();
            this.btnSave.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnSave,
                    nameof(btnSave.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnSaveEnabled)
                    )
                );
            this.btnSave.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnSave,
                    nameof(btnSave.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnSaveVisible)
                    )
                );

            this.btnEditId.DataBindings.Clear();
            this.btnEditId.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnEditId,
                    nameof(btnEditId.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnEditIdEnabled)
                    )
                );
            this.btnEditId.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnEditId,
                    nameof(btnEditId.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnEditIdVisible)
                    )
                );


            this.btnSaveId.DataBindings.Clear();
            this.btnSaveId.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnSaveId,
                    nameof(btnSaveId.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnSaveIdEnabled)
                    )
                );
            this.btnSaveId.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnSaveId,
                    nameof(btnSaveId.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnSaveIdVisible)
                    )
                );


            this.btnCancelEditId.DataBindings.Clear();
            this.btnCancelEditId.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnCancelEditId,
                    nameof(btnCancelEditId.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnCancelEditIdEnabled)
                    )
                );
            this.btnCancelEditId.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnCancelEditId,
                    nameof(btnCancelEditId.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnCancelEditIdVisible)
                    )
                );

            this.btnNavToProcess.DataBindings.Clear();
            this.btnNavToProcess.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnNavToProcess,
                    nameof(btnNavToProcess.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnNavToProcessVisible)
                    )
                );
            this.btnNavToProcess.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnNavToProcess,
                    nameof(btnNavToProcess.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnNavToProcessEnabled)
                    )
                );


            this.btnNavToProcess.DataBindings.Clear();
            this.btnNavToProcess.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnNavToProcess,
                    nameof(btnNavToProcess.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnNavToProcessVisible)
                    )
                );
            this.btnNavToProcess.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnNavToProcess,
                    nameof(btnNavToProcess.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnNavToProcessEnabled)
                    )
                );


            this.lblDbProcess.DataBindings.Clear();
            this.lblDbProcess.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblDbProcess,
                    nameof(lblDbProcess.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnNavToProcessVisible)
                    )
                );

            this.cboConncectionType.DataBindings.Clear();
            this.cboConncectionType.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    cboConncectionType,
                    nameof(cboConncectionType.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.CboConncectionTypeEnabled)
                    )
                );

            this.tbServer.DataBindings.Clear();
            this.tbServer.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbServer,
                    nameof(tbServer.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.TbServerEnabled)
                    )
                );

            this.tbDBName.DataBindings.Clear();
            this.tbDBName.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbDBName,
                    nameof(tbDBName.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.TbDBNameEnabled)
                    )
                );

            this.tbUsername.DataBindings.Clear();
            this.tbUsername.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbUsername,
                    nameof(tbUsername.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.TbUsernameEnabled)
                    )
                );

            this.tbPassword.DataBindings.Clear();
            this.tbPassword.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbPassword,
                    nameof(tbPassword.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.TbPasswordEnabled)
                    )
                );

            this.tbDevScriptsFolderPath.DataBindings.Clear();
            this.tbDevScriptsFolderPath.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbDevScriptsFolderPath,
                    nameof(tbDevScriptsFolderPath.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.TbDevScriptsFolderPathEnabled)
                    )
                );

            this.tbDBBackupFolder.DataBindings.Clear();
            this.tbDBBackupFolder.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbDBBackupFolder,
                    nameof(tbDBBackupFolder.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.TbDBBackupFolderEnabled)
                    )
                );

            this.tbId.DataBindings.Clear();
            this.tbId.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbId,
                    nameof(tbId.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.TbIdEnabled)
                    )
                );

            this.rbDevEnv.DataBindings.Clear();
            this.rbDevEnv.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    rbDevEnv,
                    nameof(rbDevEnv.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.RbDevEnvEnabled)
                    )
                );

            this.rbDelEnv.DataBindings.Clear();
            this.rbDelEnv.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    rbDelEnv,
                    nameof(rbDelEnv.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.RbDelEnvEnabled)
                    )
                );

            this.tbDeployArtifactFolderPath.DataBindings.Clear();
            this.tbDeployArtifactFolderPath.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbDeployArtifactFolderPath,
                    nameof(tbDeployArtifactFolderPath.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.TbDeployArtifactFolderPathEnabled)
                    )
                );

            this.tbDeliveryArtifactFolderPath.DataBindings.Clear();
            this.tbDeliveryArtifactFolderPath.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbDeliveryArtifactFolderPath,
                    nameof(tbDeliveryArtifactFolderPath.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.TbDeliveryArtifactFolderPathEnabled)
                    )
                );


            this.tbProjectDescription.DataBindings.Clear();
            this.tbProjectDescription.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbProjectDescription,
                    nameof(tbProjectDescription.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.TbProjectDescriptionEnabled)
                    )
                );
        }



        private void SetErrorInErrorProvider(Control control, string message)
        {
            control.BeginInvoke((MethodInvoker)(() =>
            {
                errPrvProjectDetails.SetError(control, message);
            }));
        }





        private void BtnNavToProcess_Click(object sender, EventArgs e)
        {
            ViewModel.NavToDBVersionsCommand.Execute(null);
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            ViewModel.SaveCommand.Execute(null);
        }


        private void btnEditId_Click(object sender, EventArgs e)
        {
            ViewModel.SetEditIdStateCommand.Execute(null);
        }
        private void btnCancelEditId_Click(object sender, EventArgs e)
        {
            ViewModel.CancelEditIdCommand.Execute(null);
        }
        private void btnSaveId_Click(object sender, EventArgs e)
        {
            ViewModel.SaveChangeIdCommand.Execute(null);
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
