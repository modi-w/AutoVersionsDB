﻿using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.UI;
using AutoVersionsDB.UI.DBVersions;
using AutoVersionsDB.UI.Notifications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts
{
    public class DBVersionsViewModelAsserts
    {
        private readonly ScriptFilesListsStateAsserts _scriptFilesListsStateAsserts;
        private readonly DBVersionsViewStateAsserts _dbVersionsViewStateAsserts;
        private readonly NotificationsViewModelAsserts _notificationsViewModelAsserts;

        public DBVersionsViewModelAsserts(ScriptFilesListsStateAsserts scriptFilesListsStateAsserts,
                                            DBVersionsViewStateAsserts dbVersionsViewStateAsserts,
                                            NotificationsViewModelAsserts notificationsViewModelAsserts)
        {
            _scriptFilesListsStateAsserts = scriptFilesListsStateAsserts;
            _dbVersionsViewStateAsserts = dbVersionsViewStateAsserts;
            _notificationsViewModelAsserts = notificationsViewModelAsserts;
        }

        public void AssertCompleteSuccessfullyAllFilesSync(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataDBFinalState(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _notificationsViewModelAsserts.AssertNotificationsViewModelCompleteSuccessfully(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }
        public void AssertCompleteSuccessfullyAllFilesSyncVrDDD(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataDBFinalStateVrDDD(testName, dbVersionsViewModel.DBVersionsViewModelData);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _notificationsViewModelAsserts.AssertNotificationsViewModelCompleteSuccessfully(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }

        public void AssertCompleteSuccessfullyInitDBSync(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataNewProject(testName, dbVersionsViewModel.DBVersionsViewModelData);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _notificationsViewModelAsserts.AssertNotificationsViewModelCompleteSuccessfully(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }



        public void AssertCompleteSuccessfullyAllFilesSyncVirtual(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataDBFinalStateVirtual(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _notificationsViewModelAsserts.AssertNotificationsViewModelCompleteSuccessfully(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }

        public void AssertWaitingForUserAllFilesSync(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataDBFinalState(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _notificationsViewModelAsserts.AssertNotificationsViewModelWaitingForUser(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }


        public void AssertCompleteSuccessForMiddleState(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataMiddleState(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _notificationsViewModelAsserts.AssertNotificationsViewModelCompleteSuccessfully(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }

        
        public void AssertCompleteSuccessForMiddleStateVirtual(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataMiddleStateVirtual(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _notificationsViewModelAsserts.AssertNotificationsViewModelCompleteSuccessfully(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }

        public void AssertNewIncScriptsFiles(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataNewIncScriptsFiles(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _notificationsViewModelAsserts.AssertNotificationsViewModelWaitingForUser(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }


        public void AssertNewRptScriptFile(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataNewRptScriptFile(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _notificationsViewModelAsserts.AssertNotificationsViewModelWaitingForUser(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }

        public void AssertNewDDDScriptFile(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataNewDDDScriptFile(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _notificationsViewModelAsserts.AssertNotificationsViewModelWaitingForUser(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }

        public void AssertIncrementalChanged(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataIncrementalChanged(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateScriptsOrSystemTableError(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _notificationsViewModelAsserts.AssertNotificationsViewModelError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, CoreTextResources.HistoryExecutedFilesChangedInstructionsMessage);
        }
        public void AssertIncrementalMissing(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataIncrementalMissing(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateScriptsOrSystemTableError(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _notificationsViewModelAsserts.AssertNotificationsViewModelError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, CoreTextResources.HistoryExecutedFilesChangedInstructionsMessage);
        }

        public void AssertNewProjectDevEnv(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataNewProject(testName, dbVersionsViewModel.DBVersionsViewModelData);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateNewProject(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            if (isDevEnv)
            {
                _notificationsViewModelAsserts.AssertNotificationsViewModelAttention(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, CoreTextResources.NewProjectDevEnvInstructionsMessage);
            }
            else
            {
                _notificationsViewModelAsserts.AssertNotificationsViewModelAttention(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, CoreTextResources.NewProjectDeliveryEnvNoscriptsFilesInstructionsMessage);
            }

        }

        public void AssertNewProjectDeliveryEnv(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataNewProject(testName, dbVersionsViewModel.DBVersionsViewModelData);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateScriptsOrSystemTableError(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            if (isDevEnv)
            {
                _notificationsViewModelAsserts.AssertNotificationsViewModelAttention(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, CoreTextResources.NewProjectDevEnvInstructionsMessage);
            }
            else
            {
                _notificationsViewModelAsserts.AssertNotificationsViewModelAttention(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, CoreTextResources.NewProjectDeliveryEnvNoscriptsFilesInstructionsMessage);
            }

        }

        public void AssertMissingSystemTables(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataMissingSystemTables(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateScriptsOrSystemTableError(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            if (isDevEnv)
            {
                _notificationsViewModelAsserts.AssertNotificationsViewModelError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, CoreTextResources.SystemTablesDevEnvInstructionsMessage);
            }
            else
            {
                _notificationsViewModelAsserts.AssertNotificationsViewModelError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, CoreTextResources.SystemTablesDeliveryEnvInstructionsMessage);
            }

        }

        public void AssertValidateArtifact(string testName, DBVersionsViewModel dbVersionsViewModel)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataNoFiles(testName, dbVersionsViewModel.DBVersionsViewModelData);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateProcessError(testName, dbVersionsViewModel.DBVersionsControls, false);
            _notificationsViewModelAsserts.AssertNotificationsViewModelError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, CoreTextResources.ArtifactFileNotExistErrorMessage);
        }

        public void AssertNotAllowMethodDBMiddleState(string testName, DBVersionsViewModel dbVersionsViewModel)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataMiddleState(testName, dbVersionsViewModel.DBVersionsViewModelData, false);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateProcessError(testName, dbVersionsViewModel.DBVersionsControls, false);
            _notificationsViewModelAsserts.AssertNotificationsViewModelError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, CoreTextResources.DeliveryEnvErrorMessage);
        }

        public void AssertNotAllowMethodDBFinalState(string testName, DBVersionsViewModel dbVersionsViewModel)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataDBFinalState(testName, dbVersionsViewModel.DBVersionsViewModelData, false);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateProcessError(testName, dbVersionsViewModel.DBVersionsControls, false);
            _notificationsViewModelAsserts.AssertNotificationsViewModelError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, CoreTextResources.DeliveryEnvErrorMessage);
        }



        public void AssertRepeatableChanged(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataRepeatableChanged(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _notificationsViewModelAsserts.AssertNotificationsViewModelWaitingForUser(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }


        public void AssertScriptErrorForMiddleState(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataMiddleState(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateProcessError(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _notificationsViewModelAsserts.AssertNotificationsViewModelProcessError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }

        //public void AssertScriptProcessErrorForDFinalState(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        //{
        //    _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataAllSync(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
        //    _dbVersionsViewStateAsserts.AssertDBVersionsViewStateProcessError(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
        //    _dbVersionsViewStateAsserts.AssertNotificationsViewModelProcessError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        //}

        public void AssertDBSpecificStateWarning(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataDBFinalState(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateProcessError(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _notificationsViewModelAsserts.AssertNotificationsViewModelProcessError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }

        public virtual void AssertViewStateHistory(string testName, IList<DBVersionsViewStateType> viewStateHistory, DBVersionsViewStateType finalViewState)
        {
            Assert.That(viewStateHistory.Count == 2, $"{testName} >>> Number of process ViewStates should be 2, but was {viewStateHistory.Count}");
            Assert.That(viewStateHistory[0] == DBVersionsViewStateType.InProcess, $"{testName} >>> ViewState 1 should be {DBVersionsViewStateType.InProcess}, but was '{viewStateHistory[0]}'");
            Assert.That(viewStateHistory[1] == finalViewState, $"{testName} >>> ViewState 2 should be {finalViewState}, but was '{viewStateHistory[1]}'");
        }

        public virtual void AssertViewStateHistoryForNewTwoScriptFiles(string testName, IList<DBVersionsViewStateType> viewStateHistory)
        {
            Assert.That(viewStateHistory.Count == 8, $"{testName} >>> Number of process ViewStates should be 8, but was {viewStateHistory.Count}");
            Assert.That(viewStateHistory[0] == DBVersionsViewStateType.InProcess, $"{testName} >>> ViewState 1 should be {DBVersionsViewStateType.InProcess}, but was '{viewStateHistory[0]}'");
            Assert.That(viewStateHistory[1] == DBVersionsViewStateType.ReadyToRunSync, $"{testName} >>> ViewState 2 should be {DBVersionsViewStateType.ReadyToRunSync}, but was '{viewStateHistory[1]}'");
            Assert.That(viewStateHistory[2] == DBVersionsViewStateType.InProcess, $"{testName} >>> ViewState 1 should be {DBVersionsViewStateType.InProcess}, but was '{viewStateHistory[2]}'");
            Assert.That(viewStateHistory[3] == DBVersionsViewStateType.ReadyToRunSync, $"{testName} >>> ViewState 2 should be {DBVersionsViewStateType.ReadyToRunSync}, but was '{viewStateHistory[3]}'");
            Assert.That(viewStateHistory[4] == DBVersionsViewStateType.InProcess, $"{testName} >>> ViewState 1 should be {DBVersionsViewStateType.InProcess}, but was '{viewStateHistory[4]}'");
            Assert.That(viewStateHistory[5] == DBVersionsViewStateType.ReadyToRunSync, $"{testName} >>> ViewState 2 should be {DBVersionsViewStateType.ReadyToRunSync}, but was '{viewStateHistory[5]}'");
            Assert.That(viewStateHistory[6] == DBVersionsViewStateType.InProcess, $"{testName} >>> ViewState 1 should be {DBVersionsViewStateType.InProcess}, but was '{viewStateHistory[6]}'");
            Assert.That(viewStateHistory[7] == DBVersionsViewStateType.ReadyToRunSync, $"{testName} >>> ViewState 2 should be {DBVersionsViewStateType.ReadyToRunSync}, but was '{viewStateHistory[7]}'");
        }

        public virtual void AssertViewStateHistoryForNewSingleScriptFile(string testName, IList<DBVersionsViewStateType> viewStateHistory)
        {
            Assert.That(viewStateHistory.Count == 4, $"{testName} >>> Number of process ViewStates should be 4, but was {viewStateHistory.Count}");
            Assert.That(viewStateHistory[0] == DBVersionsViewStateType.InProcess, $"{testName} >>> ViewState 1 should be {DBVersionsViewStateType.InProcess}, but was '{viewStateHistory[0]}'");
            Assert.That(viewStateHistory[1] == DBVersionsViewStateType.ReadyToRunSync, $"{testName} >>> ViewState 2 should be {DBVersionsViewStateType.ReadyToRunSync}, but was '{viewStateHistory[1]}'");
            Assert.That(viewStateHistory[2] == DBVersionsViewStateType.InProcess, $"{testName} >>> ViewState 1 should be {DBVersionsViewStateType.InProcess}, but was '{viewStateHistory[2]}'");
            Assert.That(viewStateHistory[3] == DBVersionsViewStateType.ReadyToRunSync, $"{testName} >>> ViewState 2 should be {DBVersionsViewStateType.ReadyToRunSync}, but was '{viewStateHistory[3]}'");
        }








    }
}
