using AutoVersionsDB;
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
            _notificationsViewModelAsserts.AssertNotificationsViewModelError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, "History executed files changed, please 'Recreate DB From Scratch' or 'Set DB State as Virtual Execution'");
        }
        public void AssertIncrementalMissing(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataIncrementalMissing(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateScriptsOrSystemTableError(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _notificationsViewModelAsserts.AssertNotificationsViewModelError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, "History executed files changed, please 'Recreate DB From Scratch' or 'Set DB State as Virtual Execution'");
        }

        public void AssertNewProject(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataNewProject(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateScriptsOrSystemTableError(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            if (isDevEnv)
            {
                _notificationsViewModelAsserts.AssertNotificationsViewModelAttention(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, $"Welcome!!! This appear to be a new project.{Environment.NewLine}1) Run 'Recreate' or 'Virtual' for creating our DB system tables >> 2) Add your scripts files >> 3) Run 'Sync'");
            }
            else
            {
                _notificationsViewModelAsserts.AssertNotificationsViewModelAttention(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, $"Welcome!!! This appear to be a new project.{Environment.NewLine}1) Copy the artifact file that deployed from your dev environment >> 2) Run 'Virtual' to set the current DB state related to the scripts file >> 3) Run 'Sync' for executing the rest of the scripts files");
            }

        }

        public void AssertMissingSystemTables(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataMissingSystemTables(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateScriptsOrSystemTableError(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            if (isDevEnv)
            {
                _notificationsViewModelAsserts.AssertNotificationsViewModelError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, "The system tables has invalid structure. Please try to 'Recreate DB From Scratch' or 'Set DB State by Virtual Execution'.");
            }
            else
            {
                _notificationsViewModelAsserts.AssertNotificationsViewModelError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, "The system tables has invalid structure. Please try to 'Set DB State by Virtual Execution'.");
            }

        }

        public void AssertValidateArtifact(string testName, DBVersionsViewModel dbVersionsViewModel)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataNoFiles(testName, dbVersionsViewModel.DBVersionsViewModelData);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateProcessError(testName, dbVersionsViewModel.DBVersionsControls, false);
            _notificationsViewModelAsserts.AssertNotificationsViewModelError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, "Delivery Artifact File does not exist");
        }

        public void AssertNotAllowMethodDBMiddleState(string testName, DBVersionsViewModel dbVersionsViewModel)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataMiddleState(testName, dbVersionsViewModel.DBVersionsViewModelData, false);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateProcessError(testName, dbVersionsViewModel.DBVersionsControls, false);
            _notificationsViewModelAsserts.AssertNotificationsViewModelError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, "Could not run this command on Delivery Environment");
        }

        public void AssertNotAllowMethodDBFinalState(string testName, DBVersionsViewModel dbVersionsViewModel)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataDBFinalState(testName, dbVersionsViewModel.DBVersionsViewModelData, false);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateProcessError(testName, dbVersionsViewModel.DBVersionsControls, false);
            _notificationsViewModelAsserts.AssertNotificationsViewModelError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, "Could not run this command on Delivery Environment");
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

        public void AssertViewStateHistory(string testName, IList<DBVersionsViewStateType> viewStateHistory, DBVersionsViewStateType finalViewState)
        {
            Assert.That(viewStateHistory.Count == 2, $"{testName} >>> Number of process ViewStates should be 2, but was {viewStateHistory.Count}");
            Assert.That(viewStateHistory[0] == DBVersionsViewStateType.InProcess, $"{testName} >>> ViewState 1 should be {DBVersionsViewStateType.InProcess}, but was '{viewStateHistory[0]}'");
            Assert.That(viewStateHistory[1] == finalViewState, $"{testName} >>> ViewState 2 should be {finalViewState}, but was '{viewStateHistory[1]}'");
        }

        public void AssertViewStateHistoryForNewTwoScriptFiles(string testName, IList<DBVersionsViewStateType> viewStateHistory)
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

        public void AssertViewStateHistoryForNewSingleScriptFile(string testName, IList<DBVersionsViewStateType> viewStateHistory)
        {
            Assert.That(viewStateHistory.Count == 4, $"{testName} >>> Number of process ViewStates should be 4, but was {viewStateHistory.Count}");
            Assert.That(viewStateHistory[0] == DBVersionsViewStateType.InProcess, $"{testName} >>> ViewState 1 should be {DBVersionsViewStateType.InProcess}, but was '{viewStateHistory[0]}'");
            Assert.That(viewStateHistory[1] == DBVersionsViewStateType.ReadyToRunSync, $"{testName} >>> ViewState 2 should be {DBVersionsViewStateType.ReadyToRunSync}, but was '{viewStateHistory[1]}'");
            Assert.That(viewStateHistory[2] == DBVersionsViewStateType.InProcess, $"{testName} >>> ViewState 1 should be {DBVersionsViewStateType.InProcess}, but was '{viewStateHistory[2]}'");
            Assert.That(viewStateHistory[3] == DBVersionsViewStateType.ReadyToRunSync, $"{testName} >>> ViewState 2 should be {DBVersionsViewStateType.ReadyToRunSync}, but was '{viewStateHistory[3]}'");
        }








    }
}
