﻿using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.UI.EditProject
{
    public class EditProjectViewSateManager
    {
        private readonly EditProjectControls _editProjectControls;
        private readonly ProjectConfigErrorMessages _projectConfigErrorMessages;
        private readonly NotificationsViewModel _notificationsViewModel;


        private EditProjectViewStateType _viewType;

        public EditProjectViewSateManager(EditProjectControls editProjectControls,
                                            ProjectConfigErrorMessages projectConfigErrorMessages,
                                            NotificationsViewModel notificationsViewModel)
        {
            _editProjectControls = editProjectControls;
            _projectConfigErrorMessages = projectConfigErrorMessages;
            _notificationsViewModel = notificationsViewModel;

        }

        public void ShowHideEnvFields(bool isDevEnv)
        {
            _editProjectControls.PnlDevEnvFoldersFieldsVisible = isDevEnv;
            _editProjectControls.PnlDevEnvDeplyFolderVisible = isDevEnv;
            _editProjectControls.PnlDelEnvFieldsVisible = !isDevEnv;
        }


        public void ChangeViewState(EditProjectViewStateType viewType)
        {
            _viewType = viewType;

            switch (_viewType)
            {
                case EditProjectViewStateType.InPorcess:

                    SetAllControlsEnableDisable(false);
                    break;

                case EditProjectViewStateType.New:

                    SetAllControlsEnableDisable(true);

                    _editProjectControls.BtnEditIdVisible = false;
                    _editProjectControls.BtnSaveIdVisible = false;
                    _editProjectControls.BtnCancelEditIdVisible = false;
                    break;

                case EditProjectViewStateType.Update:

                    SetAllControlsEnableDisable(true);

                    _editProjectControls.TbIdEnabled = false;


                    _editProjectControls.BtnSaveIdVisible = false;
                    _editProjectControls.BtnCancelEditIdVisible = false;
                    _editProjectControls.BtnEditIdVisible = true;
                    _editProjectControls.BtnEditIdEnabled = true;
                    break;

                case EditProjectViewStateType.EditId:

                    SetAllControlsEnableDisable(false);

                    _editProjectControls.TbIdEnabled = true;

                    _editProjectControls.BtnEditIdVisible = false;
                    _editProjectControls.BtnSaveIdVisible = true;
                    _editProjectControls.BtnSaveIdEnabled = true;
                    _editProjectControls.BtnCancelEditIdVisible = true;
                    _editProjectControls.BtnCancelEditIdEnabled = true;
                    break;
                default:
                    break;
            }
        }



        private void SetAllControlsEnableDisable(bool isEnable)
        {
            _editProjectControls.BtnNavToProcessEnabled = isEnable;
            _editProjectControls.CboConncectionTypeEnabled = isEnable;
            _editProjectControls.TbServerEnabled = isEnable;
            _editProjectControls.TbDBNameEnabled = isEnable;
            _editProjectControls.TbUsernameEnabled = isEnable;
            _editProjectControls.TbPasswordEnabled = isEnable;
            _editProjectControls.TbDevScriptsFolderPathEnabled = isEnable;
            _editProjectControls.TbDBBackupFolderEnabled = isEnable;
            _editProjectControls.TbIdEnabled = isEnable;
            _editProjectControls.RbDevEnvEnabled = isEnable;
            _editProjectControls.RbDelEnvEnabled = isEnable;
            _editProjectControls.TbDeployArtifactFolderPathEnabled = isEnable;
            _editProjectControls.TbDeliveryArtifactFolderPathEnabled = isEnable;
            _editProjectControls.BtnSaveEnabled = isEnable;
            _editProjectControls.TbProjectDescriptionEnabled = isEnable;
            _editProjectControls.BtnSaveIdEnabled = isEnable;
            _editProjectControls.BtnEditIdEnabled = isEnable;
        }



        public void handleProcessErrors(string projectId, ProcessTrace processResults)
        {
            _notificationsViewModel.AfterComplete();

            SetErrorsToUiElements(processResults);

            _editProjectControls.ImgErrorVisible = processResults.HasError;
            _editProjectControls.ImgValidVisible = !processResults.HasError;
            _editProjectControls.BtnNavToProcessVisible = !processResults.HasError;

            if (string.IsNullOrWhiteSpace(projectId))
            {
                ChangeViewState(EditProjectViewStateType.New);
            }
            else
            {
                ChangeViewState(EditProjectViewStateType.Update);
            }
        }




        public void ClearUIElementsErrors()
        {
            _projectConfigErrorMessages.IdErrorMessage = null;
            _projectConfigErrorMessages.DBTypeCodeErrorMessage = null;
            _projectConfigErrorMessages.ServerErrorMessage = null;
            _projectConfigErrorMessages.DBNameErrorMessage = null;
            _projectConfigErrorMessages.UsernameErrorMessage = null;
            _projectConfigErrorMessages.PasswordErrorMessage = null;
            _projectConfigErrorMessages.BackupFolderPathErrorMessage = null;
            _projectConfigErrorMessages.DevScriptsBaseFolderPathErrorMessage = null;
            _projectConfigErrorMessages.DeployArtifactFolderPathErrorMessage = null;
            _projectConfigErrorMessages.DeliveryArtifactFolderPathErrorMessage = null;

        }


        public void SetErrorsToUiElements(ProcessTrace processResults)
        {
            if (processResults.HasError)
            {
                List<StepNotificationState> errorStates = processResults.StatesLog.Where(e => e.HasError).ToList();

                foreach (StepNotificationState errorStateItem in errorStates)
                {
                    switch (errorStateItem.LowLevelErrorCode)
                    {
                        case "IdMandatory":

                            _projectConfigErrorMessages.IdErrorMessage = errorStateItem.LowLevelErrorMessage;
                            break;

                        case "DBType":

                            _projectConfigErrorMessages.DBTypeCodeErrorMessage = errorStateItem.LowLevelErrorMessage;
                            break;

                        case "DBName":

                            _projectConfigErrorMessages.DBNameErrorMessage = errorStateItem.LowLevelErrorMessage;
                            break;

                        //case "ConnStr":

                        //    SetErrorInErrorProvider(tbConnStr, errorStateItem.LowLevelErrorMessage);
                        //    break;

                        //case "ConnStrToMasterDB":

                        //    SetErrorInErrorProvider(tbConnStrToMasterDB, errorStateItem.LowLevelErrorMessage);
                        //    break;

                        case "DBBackupFolderPath":

                            _projectConfigErrorMessages.BackupFolderPathErrorMessage = errorStateItem.LowLevelErrorMessage;
                            break;

                        case "DeliveryArtifactFolderPath":

                            _projectConfigErrorMessages.DeliveryArtifactFolderPathErrorMessage = errorStateItem.LowLevelErrorMessage;
                            break;

                        case "DeployArtifactFolderPath":

                            _projectConfigErrorMessages.DeployArtifactFolderPathErrorMessage = errorStateItem.LowLevelErrorMessage;
                            break;

                        case "DevScriptsBaseFolder":

                            _projectConfigErrorMessages.DevScriptsBaseFolderPathErrorMessage = errorStateItem.LowLevelErrorMessage;
                            break;



                    }
                }
            }
        }


    }
}
