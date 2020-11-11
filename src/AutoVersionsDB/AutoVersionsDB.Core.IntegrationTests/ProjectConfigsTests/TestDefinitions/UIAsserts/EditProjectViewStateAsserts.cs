using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.UI;
using AutoVersionsDB.UI.DBVersions;
using AutoVersionsDB.UI.EditProject;
using AutoVersionsDB.UI.Notifications;
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

        public void AssertEditProjectViewStateNew(string testName, EditProjectControls dbVersionsControls, bool isDevEnv, bool hasError)
        {
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.ImgErrorVisible), dbVersionsControls.ImgErrorVisible, hasError);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.ImgValidVisible), dbVersionsControls.ImgValidVisible, !hasError);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnNavToProcessVisible), dbVersionsControls.BtnNavToProcessVisible, !hasError);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnNavToProcessEnabled), dbVersionsControls.BtnNavToProcessEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnSaveEnabled), dbVersionsControls.BtnSaveEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnEditIdEnabled), dbVersionsControls.BtnEditIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnEditIdVisible), dbVersionsControls.BtnEditIdVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnSaveIdEnabled), dbVersionsControls.BtnSaveIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnSaveIdVisible), dbVersionsControls.BtnSaveIdVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnCancelEditIdVisible), dbVersionsControls.BtnCancelEditIdVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnCancelEditIdEnabled), dbVersionsControls.BtnCancelEditIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlDevEnvFoldersFieldsVisible), dbVersionsControls.PnlDevEnvFoldersFieldsVisible, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlDevEnvDeplyFolderVisible), dbVersionsControls.PnlDevEnvDeplyFolderVisible, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlDelEnvFieldsVisible), dbVersionsControls.PnlDelEnvFieldsVisible, !isDevEnv);
          
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.CboConncectionTypeEnabled), dbVersionsControls.CboConncectionTypeEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbServerEnabled), dbVersionsControls.TbServerEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbDBNameEnabled), dbVersionsControls.TbDBNameEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbUsernameEnabled), dbVersionsControls.TbUsernameEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbPasswordEnabled), dbVersionsControls.TbPasswordEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbDevScriptsFolderPathEnabled), dbVersionsControls.TbDevScriptsFolderPathEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbDBBackupFolderEnabled), dbVersionsControls.TbDBBackupFolderEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbIdEnabled), dbVersionsControls.TbIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.RbDevEnvEnabled), dbVersionsControls.RbDevEnvEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.RbDelEnvEnabled), dbVersionsControls.RbDelEnvEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbDeployArtifactFolderPathEnabled), dbVersionsControls.TbDeployArtifactFolderPathEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbDeliveryArtifactFolderPathEnabled), dbVersionsControls.TbDeliveryArtifactFolderPathEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbProjectDescriptionEnabled), dbVersionsControls.TbProjectDescriptionEnabled, true);

        }


        public void AssertEditProjectViewStateUpdate(string testName, EditProjectControls dbVersionsControls, bool isDevEnv, bool hasError)
        {
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.ImgErrorVisible), dbVersionsControls.ImgErrorVisible, hasError);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.ImgValidVisible), dbVersionsControls.ImgValidVisible, !hasError);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnNavToProcessVisible), dbVersionsControls.BtnNavToProcessVisible, !hasError);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnNavToProcessEnabled), dbVersionsControls.BtnNavToProcessEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnSaveEnabled), dbVersionsControls.BtnSaveEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnEditIdEnabled), dbVersionsControls.BtnEditIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnEditIdVisible), dbVersionsControls.BtnEditIdVisible, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnSaveIdEnabled), dbVersionsControls.BtnSaveIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnSaveIdVisible), dbVersionsControls.BtnSaveIdVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnCancelEditIdVisible), dbVersionsControls.BtnCancelEditIdVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnCancelEditIdEnabled), dbVersionsControls.BtnCancelEditIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlDevEnvFoldersFieldsVisible), dbVersionsControls.PnlDevEnvFoldersFieldsVisible, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlDevEnvDeplyFolderVisible), dbVersionsControls.PnlDevEnvDeplyFolderVisible, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlDelEnvFieldsVisible), dbVersionsControls.PnlDelEnvFieldsVisible, !isDevEnv);

            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.CboConncectionTypeEnabled), dbVersionsControls.CboConncectionTypeEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbServerEnabled), dbVersionsControls.TbServerEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbDBNameEnabled), dbVersionsControls.TbDBNameEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbUsernameEnabled), dbVersionsControls.TbUsernameEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbPasswordEnabled), dbVersionsControls.TbPasswordEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbDevScriptsFolderPathEnabled), dbVersionsControls.TbDevScriptsFolderPathEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbDBBackupFolderEnabled), dbVersionsControls.TbDBBackupFolderEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbIdEnabled), dbVersionsControls.TbIdEnabled, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.RbDevEnvEnabled), dbVersionsControls.RbDevEnvEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.RbDelEnvEnabled), dbVersionsControls.RbDelEnvEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbDeployArtifactFolderPathEnabled), dbVersionsControls.TbDeployArtifactFolderPathEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbDeliveryArtifactFolderPathEnabled), dbVersionsControls.TbDeliveryArtifactFolderPathEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbProjectDescriptionEnabled), dbVersionsControls.TbProjectDescriptionEnabled, true);

        }


        public void AssertEditProjectViewStateEditId(string testName, EditProjectControls dbVersionsControls, bool isDevEnv, bool hasError)
        {
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.ImgErrorVisible), dbVersionsControls.ImgErrorVisible, hasError);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.ImgValidVisible), dbVersionsControls.ImgValidVisible, !hasError);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnNavToProcessVisible), dbVersionsControls.BtnNavToProcessVisible, !hasError);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnNavToProcessEnabled), dbVersionsControls.BtnNavToProcessEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnSaveEnabled), dbVersionsControls.BtnSaveEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnEditIdEnabled), dbVersionsControls.BtnEditIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnEditIdVisible), dbVersionsControls.BtnEditIdVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnSaveIdEnabled), dbVersionsControls.BtnSaveIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnSaveIdVisible), dbVersionsControls.BtnSaveIdVisible, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnCancelEditIdVisible), dbVersionsControls.BtnCancelEditIdVisible, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnCancelEditIdEnabled), dbVersionsControls.BtnCancelEditIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlDevEnvFoldersFieldsVisible), dbVersionsControls.PnlDevEnvFoldersFieldsVisible, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlDevEnvDeplyFolderVisible), dbVersionsControls.PnlDevEnvDeplyFolderVisible, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlDelEnvFieldsVisible), dbVersionsControls.PnlDelEnvFieldsVisible, !isDevEnv);

            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.CboConncectionTypeEnabled), dbVersionsControls.CboConncectionTypeEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbServerEnabled), dbVersionsControls.TbServerEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbDBNameEnabled), dbVersionsControls.TbDBNameEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbUsernameEnabled), dbVersionsControls.TbUsernameEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbPasswordEnabled), dbVersionsControls.TbPasswordEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbDevScriptsFolderPathEnabled), dbVersionsControls.TbDevScriptsFolderPathEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbDBBackupFolderEnabled), dbVersionsControls.TbDBBackupFolderEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbIdEnabled), dbVersionsControls.TbIdEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.RbDevEnvEnabled), dbVersionsControls.RbDevEnvEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.RbDelEnvEnabled), dbVersionsControls.RbDelEnvEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbDeployArtifactFolderPathEnabled), dbVersionsControls.TbDeployArtifactFolderPathEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbDeliveryArtifactFolderPathEnabled), dbVersionsControls.TbDeliveryArtifactFolderPathEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.TbProjectDescriptionEnabled), dbVersionsControls.TbProjectDescriptionEnabled, true);

        }


        public void AssertError(string testName, ProjectConfigErrorMessages projectConfigErrorMessages)
        {

        }

    }
}
