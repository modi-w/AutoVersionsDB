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
        private EditProjectViewModel _viewModel;
        [Inject]
        public EditProjectViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                _viewModel = value;

                _viewModel.PropertyChanged += _viewModel_PropertyChanged;
                SetDataBindings();

                notificationsControl1.ViewModel = _viewModel.NotificationsViewModel;
            }
        }



        public EditProjectConfigDetails()
        {
            InitializeComponent();


            errPrvProjectDetails.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            imgError.Location = new Point(imgValid.Location.X, imgValid.Location.Y);
            pnlDelEnvFields.Location = new Point(pnlDevEnvFoldersFields.Location.X, pnlDevEnvFoldersFields.Location.Y);
        }


        private void _viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
                nameof(tbId.Text),
                ViewModel.ProjectConfig,
                nameof(ViewModel.ProjectConfig.Id),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.tbProjectDescription.DataBindings.Clear();
            this.tbProjectDescription.DataBindings.Add(
                nameof(tbProjectDescription.Text),
                ViewModel.ProjectConfig,
                nameof(ViewModel.ProjectConfig.Description),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.cboConncectionType.DataBindings.Clear();
            this.cboConncectionType.DataBindings.Add(
                nameof(cboConncectionType.SelectedValue),
                ViewModel.ProjectConfig,
                nameof(ViewModel.ProjectConfig.DBType),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.tbServer.DataBindings.Clear();
            this.tbServer.DataBindings.Add(
                nameof(tbServer.Text),
                ViewModel.ProjectConfig,
                nameof(ViewModel.ProjectConfig.Server),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.tbDBName.DataBindings.Clear();
            this.tbDBName.DataBindings.Add(
                nameof(tbServer.Text),
                ViewModel.ProjectConfig,
                nameof(ViewModel.ProjectConfig.DBName),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.tbUsername.DataBindings.Clear();
            this.tbUsername.DataBindings.Add(
                nameof(tbServer.Text),
                ViewModel.ProjectConfig,
                nameof(ViewModel.ProjectConfig.Username),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.tbPassword.DataBindings.Clear();
            this.tbPassword.DataBindings.Add(
                nameof(tbPassword.Text),
                ViewModel.ProjectConfig,
                nameof(ViewModel.ProjectConfig.Password),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.tbDBBackupFolder.DataBindings.Clear();
            this.tbDBBackupFolder.DataBindings.Add(
                nameof(tbDBBackupFolder.Text),
                ViewModel.ProjectConfig,
                nameof(ViewModel.ProjectConfig.BackupFolderPath),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.rbDevEnv.DataBindings.Clear();
            this.rbDevEnv.DataBindings.Add(
                nameof(rbDevEnv.Checked),
                ViewModel.ProjectConfig,
                nameof(ViewModel.ProjectConfig.DevEnvironment),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.rbDelEnv.DataBindings.Clear();
            this.rbDelEnv.DataBindings.Add(
                nameof(rbDelEnv.Checked),
                ViewModel.ProjectConfig,
                nameof(ViewModel.ProjectConfig.DeliveryEnvironment),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.tbDevScriptsFolderPath.DataBindings.Clear();
            this.tbDevScriptsFolderPath.DataBindings.Add(
                nameof(tbDevScriptsFolderPath.Text),
                ViewModel.ProjectConfig,
                nameof(ViewModel.ProjectConfig.DevScriptsBaseFolderPath),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.lblIncrementalScriptsFolderPath.DataBindings.Clear();
            this.lblIncrementalScriptsFolderPath.DataBindings.Add(
                nameof(lblIncrementalScriptsFolderPath.Text),
                ViewModel.ProjectConfig,
                nameof(ViewModel.ProjectConfig.IncrementalScriptsFolderPath),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.lblRepeatableScriptsFolderPath.DataBindings.Clear();
            this.lblRepeatableScriptsFolderPath.DataBindings.Add(
                nameof(lblRepeatableScriptsFolderPath.Text),
                ViewModel.ProjectConfig,
                nameof(ViewModel.ProjectConfig.RepeatableScriptsFolderPath),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.lblDevDummyDataScriptsFolderPath.DataBindings.Clear();
            this.lblDevDummyDataScriptsFolderPath.DataBindings.Add(
                nameof(lblDevDummyDataScriptsFolderPath.Text),
                ViewModel.ProjectConfig,
                nameof(ViewModel.ProjectConfig.DevDummyDataScriptsFolderPath),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.tbDeployArtifactFolderPath.DataBindings.Clear();
            this.tbDeployArtifactFolderPath.DataBindings.Add(
                nameof(tbDeployArtifactFolderPath.Text),
                ViewModel.ProjectConfig,
                nameof(ViewModel.ProjectConfig.DeployArtifactFolderPath),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.tbDeliveryArtifactFolderPath.DataBindings.Clear();
            this.tbDeliveryArtifactFolderPath.DataBindings.Add(
                nameof(tbDeliveryArtifactFolderPath.Text),
                ViewModel.ProjectConfig,
                nameof(ViewModel.ProjectConfig.DeliveryArtifactFolderPath),
                false,
                DataSourceUpdateMode.OnPropertyChanged);
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


            this.imgError.DataBindings.Clear();
            this.imgError.DataBindings.Add(
                nameof(imgError.Visible),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.ImgErrorVisible),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.imgValid.DataBindings.Clear();
            this.imgValid.DataBindings.Add(
                nameof(imgValid.Visible),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.ImgValidVisible),
                false,
                DataSourceUpdateMode.OnPropertyChanged);


            this.btnSave.DataBindings.Clear();
            this.btnSave.DataBindings.Add(
                nameof(btnSave.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.BtnSaveEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);
            this.btnSave.DataBindings.Add(
                nameof(btnSave.Visible),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.BtnSaveVisible),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.btnEditId.DataBindings.Clear();
            this.btnEditId.DataBindings.Add(
                nameof(btnEditId.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.BtnEditIdEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);
            this.btnEditId.DataBindings.Add(
                nameof(btnEditId.Visible),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.BtnEditIdVisible),
                false,
                DataSourceUpdateMode.OnPropertyChanged);


            this.btnSaveId.DataBindings.Clear();
            this.btnSaveId.DataBindings.Add(
                nameof(btnSaveId.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.BtnSaveIdEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);
            this.btnSaveId.DataBindings.Add(
                nameof(btnSaveId.Visible),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.BtnSaveIdVisible),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.btnCancelEditId.DataBindings.Clear();
            this.btnCancelEditId.DataBindings.Add(
                nameof(btnCancelEditId.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.BtnCancelEditIdEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);
            this.btnCancelEditId.DataBindings.Add(
                nameof(btnCancelEditId.Visible),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.BtnCancelEditIdVisible),
                false,
                DataSourceUpdateMode.OnPropertyChanged);




            this.btnNavToProcess.DataBindings.Clear();
            this.btnNavToProcess.DataBindings.Add(
                nameof(btnNavToProcess.Visible),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.BtnNavToProcessVisible),
                false,
                DataSourceUpdateMode.OnPropertyChanged);
            this.btnNavToProcess.DataBindings.Add(
                nameof(btnNavToProcess.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.BtnNavToProcessEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);




            this.btnNavToProcess.DataBindings.Clear();
            this.btnNavToProcess.DataBindings.Add(
                nameof(btnNavToProcess.Visible),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.BtnNavToProcessVisible),
                false,
                DataSourceUpdateMode.OnPropertyChanged);
            this.btnNavToProcess.DataBindings.Add(
                nameof(btnNavToProcess.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.BtnNavToProcessEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);


            this.lblDbProcess.DataBindings.Clear();
            this.lblDbProcess.DataBindings.Add(
                nameof(lblDbProcess.Visible),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.BtnNavToProcessVisible),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.cboConncectionType.DataBindings.Clear();
            this.cboConncectionType.DataBindings.Add(
                nameof(cboConncectionType.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.CboConncectionTypeEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.tbServer.DataBindings.Clear();
            this.tbServer.DataBindings.Add(
                nameof(tbServer.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.TbServerEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);


            this.tbDBName.DataBindings.Clear();
            this.tbDBName.DataBindings.Add(
                nameof(tbDBName.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.TbDBNameEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.tbUsername.DataBindings.Clear();
            this.tbUsername.DataBindings.Add(
                nameof(tbUsername.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.TbUsernameEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.tbPassword.DataBindings.Clear();
            this.tbPassword.DataBindings.Add(
                nameof(tbPassword.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.TbPasswordEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.tbDevScriptsFolderPath.DataBindings.Clear();
            this.tbDevScriptsFolderPath.DataBindings.Add(
                nameof(tbDevScriptsFolderPath.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.TbDevScriptsFolderPathEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.tbDBBackupFolder.DataBindings.Clear();
            this.tbDBBackupFolder.DataBindings.Add(
                nameof(tbDBBackupFolder.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.TbDBBackupFolderEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.tbId.DataBindings.Clear();
            this.tbId.DataBindings.Add(
                nameof(tbId.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.TbIdEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.rbDevEnv.DataBindings.Clear();
            this.rbDevEnv.DataBindings.Add(
                nameof(rbDevEnv.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.RbDevEnvEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.rbDelEnv.DataBindings.Clear();
            this.rbDelEnv.DataBindings.Add(
                nameof(rbDelEnv.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.RbDelEnvEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.tbDeployArtifactFolderPath.DataBindings.Clear();
            this.tbDeployArtifactFolderPath.DataBindings.Add(
                nameof(tbDeployArtifactFolderPath.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.TbDeployArtifactFolderPathEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.tbDeliveryArtifactFolderPath.DataBindings.Clear();
            this.tbDeliveryArtifactFolderPath.DataBindings.Add(
                nameof(tbDeliveryArtifactFolderPath.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.TbDeliveryArtifactFolderPathEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.tbProjectDescription.DataBindings.Clear();
            this.tbProjectDescription.DataBindings.Add(
                nameof(tbProjectDescription.Enabled),
                ViewModel.EditProjectControls,
                nameof(ViewModel.EditProjectControls.TbProjectDescriptionEnabled),
                false,
                DataSourceUpdateMode.OnPropertyChanged);
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
            _viewModel.NavToDBVersionsCommand.Execute(null);
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            _viewModel.SaveCommand.Execute(null);
        }


        private void btnEditId_Click(object sender, EventArgs e)
        {
            _viewModel.SetEditIdStateCommand.Execute(null);
        }
        private void btnCancelEditId_Click(object sender, EventArgs e)
        {
            _viewModel.CancelEditIdCommand.Execute(null);
        }
        private void btnSaveId_Click(object sender, EventArgs e)
        {
            _viewModel.SaveChangeIdCommand.Execute(null);
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
