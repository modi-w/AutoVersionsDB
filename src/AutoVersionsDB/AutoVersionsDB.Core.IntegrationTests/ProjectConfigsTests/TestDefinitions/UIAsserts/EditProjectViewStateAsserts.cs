using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.UI;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI.DBVersions;
using AutoVersionsDB.UI.EditProject;
using AutoVersionsDB.UI.Notifications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.UIAsserts
{
    public class EditProjectViewStateAsserts
    {
        private readonly PropertiesAsserts _propertiesAsserts;

        public EditProjectViewStateAsserts(PropertiesAsserts propertiesAsserts)
        {
            _propertiesAsserts = propertiesAsserts;
        }

        public void AssertEditProjectViewStateNew(string testName, EditProjectControls editProjectControls, bool isDevEnv)
        {
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnNavToProcessEnabled), editProjectControls.BtnNavToProcessEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnSaveEnabled), editProjectControls.BtnSaveEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnEditIdEnabled), editProjectControls.BtnEditIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnEditIdVisible), editProjectControls.BtnEditIdVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnSaveIdEnabled), editProjectControls.BtnSaveIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnSaveIdVisible), editProjectControls.BtnSaveIdVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnCancelEditIdVisible), editProjectControls.BtnCancelEditIdVisible, false);
          //  _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnCancelEditIdEnabled), editProjectControls.BtnCancelEditIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.PnlDevEnvFoldersFieldsVisible), editProjectControls.PnlDevEnvFoldersFieldsVisible, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.PnlDevEnvDeplyFolderVisible), editProjectControls.PnlDevEnvDeplyFolderVisible, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.PnlDelEnvFieldsVisible), editProjectControls.PnlDelEnvFieldsVisible, !isDevEnv);

            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.CboConncectionTypeEnabled), editProjectControls.CboConncectionTypeEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbServerEnabled), editProjectControls.TbServerEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbDBNameEnabled), editProjectControls.TbDBNameEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbUsernameEnabled), editProjectControls.TbUsernameEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbPasswordEnabled), editProjectControls.TbPasswordEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbDevScriptsFolderPathEnabled), editProjectControls.TbDevScriptsFolderPathEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbDBBackupFolderEnabled), editProjectControls.TbDBBackupFolderEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbIdEnabled), editProjectControls.TbIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.RbDevEnvEnabled), editProjectControls.RbDevEnvEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.RbDelEnvEnabled), editProjectControls.RbDelEnvEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbDeployArtifactFolderPathEnabled), editProjectControls.TbDeployArtifactFolderPathEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbDeliveryArtifactFolderPathEnabled), editProjectControls.TbDeliveryArtifactFolderPathEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbProjectDescriptionEnabled), editProjectControls.TbProjectDescriptionEnabled, true);

        }


        public void AssertEditProjectViewStateUpdate(string testName, EditProjectControls editProjectControls, bool isDevEnv)
        {
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnNavToProcessEnabled), editProjectControls.BtnNavToProcessEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnSaveEnabled), editProjectControls.BtnSaveEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnEditIdEnabled), editProjectControls.BtnEditIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnEditIdVisible), editProjectControls.BtnEditIdVisible, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnSaveIdEnabled), editProjectControls.BtnSaveIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnSaveIdVisible), editProjectControls.BtnSaveIdVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnCancelEditIdVisible), editProjectControls.BtnCancelEditIdVisible, false);
           // _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnCancelEditIdEnabled), editProjectControls.BtnCancelEditIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.PnlDevEnvFoldersFieldsVisible), editProjectControls.PnlDevEnvFoldersFieldsVisible, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.PnlDevEnvDeplyFolderVisible), editProjectControls.PnlDevEnvDeplyFolderVisible, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.PnlDelEnvFieldsVisible), editProjectControls.PnlDelEnvFieldsVisible, !isDevEnv);

            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.CboConncectionTypeEnabled), editProjectControls.CboConncectionTypeEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbServerEnabled), editProjectControls.TbServerEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbDBNameEnabled), editProjectControls.TbDBNameEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbUsernameEnabled), editProjectControls.TbUsernameEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbPasswordEnabled), editProjectControls.TbPasswordEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbDevScriptsFolderPathEnabled), editProjectControls.TbDevScriptsFolderPathEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbDBBackupFolderEnabled), editProjectControls.TbDBBackupFolderEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbIdEnabled), editProjectControls.TbIdEnabled, false);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.RbDevEnvEnabled), editProjectControls.RbDevEnvEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.RbDelEnvEnabled), editProjectControls.RbDelEnvEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbDeployArtifactFolderPathEnabled), editProjectControls.TbDeployArtifactFolderPathEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbDeliveryArtifactFolderPathEnabled), editProjectControls.TbDeliveryArtifactFolderPathEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbProjectDescriptionEnabled), editProjectControls.TbProjectDescriptionEnabled, true);

        }


        public void AssertEditProjectViewStateEditId(string testName, EditProjectControls editProjectControls, bool isDevEnv)
        {
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnNavToProcessEnabled), editProjectControls.BtnNavToProcessEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnSaveEnabled), editProjectControls.BtnSaveEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnEditIdEnabled), editProjectControls.BtnEditIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnEditIdVisible), editProjectControls.BtnEditIdVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnSaveIdEnabled), editProjectControls.BtnSaveIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnSaveIdVisible), editProjectControls.BtnSaveIdVisible, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnCancelEditIdVisible), editProjectControls.BtnCancelEditIdVisible, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnCancelEditIdEnabled), editProjectControls.BtnCancelEditIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.PnlDevEnvFoldersFieldsVisible), editProjectControls.PnlDevEnvFoldersFieldsVisible, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.PnlDevEnvDeplyFolderVisible), editProjectControls.PnlDevEnvDeplyFolderVisible, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.PnlDelEnvFieldsVisible), editProjectControls.PnlDelEnvFieldsVisible, !isDevEnv);

            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.CboConncectionTypeEnabled), editProjectControls.CboConncectionTypeEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbServerEnabled), editProjectControls.TbServerEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbDBNameEnabled), editProjectControls.TbDBNameEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbUsernameEnabled), editProjectControls.TbUsernameEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbPasswordEnabled), editProjectControls.TbPasswordEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbDevScriptsFolderPathEnabled), editProjectControls.TbDevScriptsFolderPathEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbDBBackupFolderEnabled), editProjectControls.TbDBBackupFolderEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbIdEnabled), editProjectControls.TbIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.RbDevEnvEnabled), editProjectControls.RbDevEnvEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.RbDelEnvEnabled), editProjectControls.RbDelEnvEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbDeployArtifactFolderPathEnabled), editProjectControls.TbDeployArtifactFolderPathEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbDeliveryArtifactFolderPathEnabled), editProjectControls.TbDeliveryArtifactFolderPathEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.TbProjectDescriptionEnabled), editProjectControls.TbProjectDescriptionEnabled, true);

        }


        public void AssertNoErrors(string testName, EditProjectControls editProjectControls, ProjectConfigErrorMessages projectConfigErrorMessages)
        {
            AssertError(testName, editProjectControls, projectConfigErrorMessages, new List<string>());
        }

        public void AssertError(string testName, EditProjectControls editProjectControls, ProjectConfigErrorMessages projectConfigErrorMessages, string errorCode)
        {
            AssertError(testName, editProjectControls, projectConfigErrorMessages, new List<string>() { errorCode });
        }

        public void AssertError(string testName, EditProjectControls editProjectControls, ProjectConfigErrorMessages projectConfigErrorMessages, IList<string> expctedErrorCodes)
        {
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.ImgErrorVisible), editProjectControls.ImgErrorVisible, expctedErrorCodes.Count > 0);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.ImgValidVisible), editProjectControls.ImgValidVisible, expctedErrorCodes.Count == 0);
            _propertiesAsserts.AssertProperty(testName, nameof(editProjectControls.BtnNavToProcessVisible), editProjectControls.BtnNavToProcessVisible, expctedErrorCodes.Count == 0);


            AssertErrorMessage(testName, expctedErrorCodes.Contains("IdMandatory"), projectConfigErrorMessages.IdErrorMessage, nameof(projectConfigErrorMessages.IdErrorMessage));
            AssertErrorMessage(testName, expctedErrorCodes.Contains("DBType"), projectConfigErrorMessages.DBTypeCodeErrorMessage, nameof(projectConfigErrorMessages.DBTypeCodeErrorMessage));
            AssertErrorMessage(testName, expctedErrorCodes.Contains("DBName"), projectConfigErrorMessages.DBNameErrorMessage, nameof(projectConfigErrorMessages.DBNameErrorMessage));
            AssertErrorMessage(testName, expctedErrorCodes.Contains("DBBackupFolderPath"), projectConfigErrorMessages.BackupFolderPathErrorMessage, nameof(projectConfigErrorMessages.BackupFolderPathErrorMessage));
            AssertErrorMessage(testName, expctedErrorCodes.Contains("DeliveryArtifactFolderPath"), projectConfigErrorMessages.DeliveryArtifactFolderPathErrorMessage, nameof(projectConfigErrorMessages.DeliveryArtifactFolderPathErrorMessage));
            AssertErrorMessage(testName, expctedErrorCodes.Contains("DeployArtifactFolderPath"), projectConfigErrorMessages.DeployArtifactFolderPathErrorMessage, nameof(projectConfigErrorMessages.DeployArtifactFolderPathErrorMessage));
            AssertErrorMessage(testName, expctedErrorCodes.Contains("DevScriptsBaseFolder"), projectConfigErrorMessages.DevScriptsBaseFolderPathErrorMessage, nameof(projectConfigErrorMessages.DevScriptsBaseFolderPathErrorMessage));
            AssertErrorMessage(testName, expctedErrorCodes.Contains("IdMandatory"), projectConfigErrorMessages.IdErrorMessage, nameof(projectConfigErrorMessages.IdErrorMessage));
            AssertErrorMessage(testName, expctedErrorCodes.Contains("IdMandatory"), projectConfigErrorMessages.IdErrorMessage, nameof(projectConfigErrorMessages.IdErrorMessage));
            AssertErrorMessage(testName, expctedErrorCodes.Contains("IdMandatory"), projectConfigErrorMessages.IdErrorMessage, nameof(projectConfigErrorMessages.IdErrorMessage));
        }

        private void AssertErrorMessage(string testName,bool isErrorCodeExist, string errorMessageValue, string errorMessagePropertyName)
        {
            Assert.That(isErrorCodeExist == !string.IsNullOrWhiteSpace(errorMessageValue), $"{testName} -> Missing error message for '{errorMessagePropertyName}'");

        }


    }
}
