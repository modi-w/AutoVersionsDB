using AutoVersionsDB.UI.EditProject;
using AutoVersionsDB.WinApp.Utils;
using Ninject;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp
{

    public partial class EditProjectView : UserControlNinjectBase
    {
        [Inject]
        public EditProjectViewModel ViewModel { get; set; }



        public EditProjectView()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                ViewModel.PropertyChanged += ViewModel_PropertyChanged;
                SetDataBindings();
            }

            errPrvProjectDetails.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            imgError.Location = new Point(imgValid.Location.X, imgValid.Location.Y);
            pnlDelEnvFields.Location = new Point(pnlDevEnvFoldersFields.Location.X, pnlDevEnvFoldersFields.Location.Y);
        }

        private void EditProjectConfigDetails_Load(object sender, EventArgs e)
        {

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
            BeginInvoke((MethodInvoker)(() =>
            {
                tbId.DataBindings.Clear();
                tbId.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbId,
                        nameof(tbId.Text),
                        ViewModel.ProjectConfig,
                        nameof(ViewModel.ProjectConfig.Id)
                    )
                );
                tbId.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbId,
                        nameof(tbId.Enabled),
                        ViewModel.EditProjectControls,
                        nameof(ViewModel.EditProjectControls.TbIdEnabled)
                        )
                    );


                tbProjectDescription.DataBindings.Clear();
                tbProjectDescription.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbProjectDescription,
                        nameof(tbProjectDescription.Text),
                        ViewModel.ProjectConfig,
                        nameof(ViewModel.ProjectConfig.Description)
                    )
                );
                tbProjectDescription.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbProjectDescription,
                        nameof(tbProjectDescription.Enabled),
                        ViewModel.EditProjectControls,
                        nameof(ViewModel.EditProjectControls.TbProjectDescriptionEnabled)
                        )
                    );


                cboConncectionType.DataBindings.Clear();
                cboConncectionType.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        cboConncectionType,
                        nameof(cboConncectionType.SelectedValue),
                        ViewModel.ProjectConfig,
                        nameof(ViewModel.ProjectConfig.DBType)
                    )
                );
                cboConncectionType.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        cboConncectionType,
                        nameof(cboConncectionType.Enabled),
                        ViewModel.EditProjectControls,
                        nameof(ViewModel.EditProjectControls.CboConncectionTypeEnabled)
                        )
                    );


                tbServer.DataBindings.Clear();
                tbServer.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbServer,
                        nameof(tbServer.Text),
                        ViewModel.ProjectConfig,
                        nameof(ViewModel.ProjectConfig.Server)
                    )
                );
                tbServer.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbServer,
                        nameof(tbServer.Enabled),
                        ViewModel.EditProjectControls,
                        nameof(ViewModel.EditProjectControls.TbServerEnabled)
                        )
                    );


                tbDBName.DataBindings.Clear();
                tbDBName.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbDBName,
                        nameof(tbDBName.Text),
                        ViewModel.ProjectConfig,
                        nameof(ViewModel.ProjectConfig.DBName)
                    )
                );
                tbDBName.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbDBName,
                        nameof(tbDBName.Enabled),
                        ViewModel.EditProjectControls,
                        nameof(ViewModel.EditProjectControls.TbDBNameEnabled)
                        )
                    );

                tbUsername.DataBindings.Clear();
                tbUsername.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbUsername,
                        nameof(tbUsername.Text),
                        ViewModel.ProjectConfig,
                        nameof(ViewModel.ProjectConfig.Username)
                    )
                );
                tbUsername.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbUsername,
                        nameof(tbUsername.Enabled),
                        ViewModel.EditProjectControls,
                        nameof(ViewModel.EditProjectControls.TbUsernameEnabled)
                        )
                    );

                tbPassword.DataBindings.Clear();
                tbPassword.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbPassword,
                        nameof(tbPassword.Text),
                        ViewModel.ProjectConfig,
                        nameof(ViewModel.ProjectConfig.Password)
                    )
                );
                tbPassword.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbPassword,
                        nameof(tbPassword.Enabled),
                        ViewModel.EditProjectControls,
                        nameof(ViewModel.EditProjectControls.TbPasswordEnabled)
                        )
                    );

                tbDBBackupFolder.DataBindings.Clear();
                tbDBBackupFolder.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbDBBackupFolder,
                        nameof(tbDBBackupFolder.Text),
                        ViewModel.ProjectConfig,
                        nameof(ViewModel.ProjectConfig.BackupFolderPath)
                    )
                );
                tbDBBackupFolder.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbDBBackupFolder,
                        nameof(tbDBBackupFolder.Enabled),
                        ViewModel.EditProjectControls,
                        nameof(ViewModel.EditProjectControls.TbDBBackupFolderEnabled)
                        )
                    );

                rbDevEnv.DataBindings.Clear();
                rbDevEnv.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        rbDevEnv,
                        nameof(rbDevEnv.Checked),
                        ViewModel.ProjectConfig,
                        nameof(ViewModel.ProjectConfig.DevEnvironment)
                    )
                );
                rbDevEnv.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        rbDevEnv,
                        nameof(rbDevEnv.Enabled),
                        ViewModel.EditProjectControls,
                        nameof(ViewModel.EditProjectControls.RbDevEnvEnabled)
                        )
                    );


                rbDelEnv.DataBindings.Clear();
                rbDelEnv.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        rbDelEnv,
                        nameof(rbDelEnv.Checked),
                        ViewModel.ProjectConfig,
                        nameof(ViewModel.ProjectConfig.DeliveryEnvironment)
                    )
                );
                rbDelEnv.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        rbDelEnv,
                        nameof(rbDelEnv.Enabled),
                        ViewModel.EditProjectControls,
                        nameof(ViewModel.EditProjectControls.RbDelEnvEnabled)
                        )
                    );

                tbDevScriptsFolderPath.DataBindings.Clear();
                tbDevScriptsFolderPath.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbDevScriptsFolderPath,
                        nameof(tbDevScriptsFolderPath.Text),
                        ViewModel.ProjectConfig,
                        nameof(ViewModel.ProjectConfig.DevScriptsBaseFolderPath)
                    )
                );
                tbDevScriptsFolderPath.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbDevScriptsFolderPath,
                        nameof(tbDevScriptsFolderPath.Enabled),
                        ViewModel.EditProjectControls,
                        nameof(ViewModel.EditProjectControls.TbDevScriptsFolderPathEnabled)
                        )
                    );

                lblIncrementalScriptsFolderPath.DataBindings.Clear();
                lblIncrementalScriptsFolderPath.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        lblIncrementalScriptsFolderPath,
                        nameof(lblIncrementalScriptsFolderPath.Text),
                        ViewModel.ProjectConfig,
                        nameof(ViewModel.ProjectConfig.IncrementalScriptsFolderPath)
                    )
                );

                lblRepeatableScriptsFolderPath.DataBindings.Clear();
                lblRepeatableScriptsFolderPath.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        lblRepeatableScriptsFolderPath,
                        nameof(lblRepeatableScriptsFolderPath.Text),
                        ViewModel.ProjectConfig,
                        nameof(ViewModel.ProjectConfig.RepeatableScriptsFolderPath)
                    )
                );

                lblDevDummyDataScriptsFolderPath.DataBindings.Clear();
                lblDevDummyDataScriptsFolderPath.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        lblDevDummyDataScriptsFolderPath,
                        nameof(lblDevDummyDataScriptsFolderPath.Text),
                        ViewModel.ProjectConfig,
                        nameof(ViewModel.ProjectConfig.DevDummyDataScriptsFolderPath)
                    )
                );


                tbDeployArtifactFolderPath.DataBindings.Clear();
                tbDeployArtifactFolderPath.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbDeployArtifactFolderPath,
                        nameof(tbDeployArtifactFolderPath.Text),
                        ViewModel.ProjectConfig,
                        nameof(ViewModel.ProjectConfig.DeployArtifactFolderPath)
                    )
                );
                tbDeployArtifactFolderPath.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbDeployArtifactFolderPath,
                        nameof(tbDeployArtifactFolderPath.Enabled),
                        ViewModel.EditProjectControls,
                        nameof(ViewModel.EditProjectControls.TbDeployArtifactFolderPathEnabled)
                        )
                    );

                tbDeliveryArtifactFolderPath.DataBindings.Clear();
                tbDeliveryArtifactFolderPath.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbDeliveryArtifactFolderPath,
                        nameof(tbDeliveryArtifactFolderPath.Text),
                        ViewModel.ProjectConfig,
                        nameof(ViewModel.ProjectConfig.DeliveryArtifactFolderPath)
                    )
                );
                tbDeliveryArtifactFolderPath.DataBindings.Add(
                    AsyncBindingHelper.GetBinding(
                        tbDeliveryArtifactFolderPath,
                        nameof(tbDeliveryArtifactFolderPath.Enabled),
                        ViewModel.EditProjectControls,
                        nameof(ViewModel.EditProjectControls.TbDeliveryArtifactFolderPathEnabled)
                        )
                    );

            }));

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

                //case nameof(ViewModel.ProjectConfigErrorMessages.ServerErrorMessage):

                //    SetErrorInErrorProvider(tbId, ViewModel.ProjectConfigErrorMessages.ServerErrorMessage);
                //    break;

                case nameof(ViewModel.ProjectConfigErrorMessages.DBNameErrorMessage):

                    SetErrorInErrorProvider(tbId, ViewModel.ProjectConfigErrorMessages.DBNameErrorMessage);
                    break;

                //case nameof(ViewModel.ProjectConfigErrorMessages.UsernameErrorMessage):

                //    SetErrorInErrorProvider(tbId, ViewModel.ProjectConfigErrorMessages.UsernameErrorMessage);
                //    break;

                //case nameof(ViewModel.ProjectConfigErrorMessages.PasswordErrorMessage):

                //    SetErrorInErrorProvider(tbId, ViewModel.ProjectConfigErrorMessages.PasswordErrorMessage);
                //    break;

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

            pnlDevEnvFoldersFields.DataBindings.Clear();
            pnlDevEnvFoldersFields.DataBindings.Add(
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


            pnlDelEnvFields.DataBindings.Clear();
            pnlDelEnvFields.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pnlDelEnvFields,
                    nameof(pnlDelEnvFields.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.PnlDelEnvFieldsVisible)
                    )
                );


            imgError.DataBindings.Clear();
            imgError.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    imgError,
                    nameof(imgError.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.ImgErrorVisible)
                    )
                );


            imgValid.DataBindings.Clear();
            imgValid.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    imgValid,
                    nameof(imgValid.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.ImgValidVisible)
                    )
                );


            btnSave.DataBindings.Clear();
            btnSave.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnSave,
                    nameof(btnSave.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnSaveEnabled)
                    )
                );
            //this.btnSave.DataBindings.Add(
            //    AsyncBindingHelper.GetBinding(
            //        btnSave,
            //        nameof(btnSave.Visible),
            //        ViewModel.EditProjectControls,
            //        nameof(ViewModel.EditProjectControls.BtnSaveVisible)
            //        )
            //    );

            btnEditId.DataBindings.Clear();
            btnEditId.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnEditId,
                    nameof(btnEditId.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnEditIdEnabled)
                    )
                );
            btnEditId.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnEditId,
                    nameof(btnEditId.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnEditIdVisible)
                    )
                );


            btnSaveId.DataBindings.Clear();
            btnSaveId.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnSaveId,
                    nameof(btnSaveId.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnSaveIdEnabled)
                    )
                );
            btnSaveId.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnSaveId,
                    nameof(btnSaveId.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnSaveIdVisible)
                    )
                );


            btnCancelEditId.DataBindings.Clear();
            btnCancelEditId.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnCancelEditId,
                    nameof(btnCancelEditId.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnCancelEditIdEnabled)
                    )
                );
            btnCancelEditId.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnCancelEditId,
                    nameof(btnCancelEditId.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnCancelEditIdVisible)
                    )
                );

            btnNavToProcess.DataBindings.Clear();
            btnNavToProcess.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnNavToProcess,
                    nameof(btnNavToProcess.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnNavToProcessVisible)
                    )
                );
            btnNavToProcess.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnNavToProcess,
                    nameof(btnNavToProcess.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnNavToProcessEnabled)
                    )
                );


            btnNavToProcess.DataBindings.Clear();
            btnNavToProcess.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnNavToProcess,
                    nameof(btnNavToProcess.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnNavToProcessVisible)
                    )
                );
            btnNavToProcess.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnNavToProcess,
                    nameof(btnNavToProcess.Enabled),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnNavToProcessEnabled)
                    )
                );


            lblDbProcess.DataBindings.Clear();
            lblDbProcess.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblDbProcess,
                    nameof(lblDbProcess.Visible),
                    ViewModel.EditProjectControls,
                    nameof(ViewModel.EditProjectControls.BtnNavToProcessVisible)
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
