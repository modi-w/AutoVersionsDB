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

        public DBVersionsViewModelAsserts(ScriptFilesListsStateAsserts scriptFilesListsStateAsserts,
                                            DBVersionsViewStateAsserts dbVersionsViewStateAsserts)
        {
            _scriptFilesListsStateAsserts = scriptFilesListsStateAsserts;
            _dbVersionsViewStateAsserts = dbVersionsViewStateAsserts;
        }

        public void AssertCompleteSuccessfullyAllFilesSync(string testName,DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataAllSync(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _dbVersionsViewStateAsserts.AssertNotificationsViewModelCompleteSuccessfully(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }

        public void AssertCompleteSuccessForMiddleState(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataMiddleState(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _dbVersionsViewStateAsserts.AssertNotificationsViewModelCompleteSuccessfully(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }

        public void AssertNewIncScriptsFiles(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataNewIncScriptsFiles(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _dbVersionsViewStateAsserts.AssertNotificationsViewModelWaitingForUser(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }

        
        public void AssertNewRptScriptFile(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataNewRptScriptFile(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _dbVersionsViewStateAsserts.AssertNotificationsViewModelWaitingForUser(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }

        public void AssertNewDDDScriptFile(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataNewDDDScriptFile(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _dbVersionsViewStateAsserts.AssertNotificationsViewModelWaitingForUser(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }

        public void AssertIncrementalChanged(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataIncrementalChanged(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateScriptsOrSystemTableError(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _dbVersionsViewStateAsserts.AssertNotificationsViewModelError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData, "History executed files changed, please 'Recreate DB From Scratch' or 'Set DB State as Virtual Execution'");
        }
        public void AssertRepeatableChanged(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataRepeatableChanged(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _dbVersionsViewStateAsserts.AssertNotificationsViewModelWaitingForUser(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }


        public void AssertScriptErrorForMiddleState(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataMiddleState(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateProcessError(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _dbVersionsViewStateAsserts.AssertNotificationsViewModelPRocessError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }

        public void AssertDBSpecificStateWarning(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataAllSync(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateProcessError(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _dbVersionsViewStateAsserts.AssertNotificationsViewModelPRocessError(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }

        public void AssertProcessViewStates(string testName, IList<DBVersionsViewStateType> viewStateHistory, DBVersionsViewStateType finalViewState)
        {
            Assert.That(viewStateHistory.Count == 2, $"{testName} -> Number of process ViewStates should be 2, but was {viewStateHistory.Count}");
            Assert.That(viewStateHistory[0] == DBVersionsViewStateType.InProcess, $"{testName} -> ViewState 1 should be {DBVersionsViewStateType.InProcess}, but was '{viewStateHistory[0]}'");
            Assert.That(viewStateHistory[1] == finalViewState, $"{testName} -> ViewState 2 should be {finalViewState}, but was '{viewStateHistory[1]}'");
        }

        public void AssertProcessViewStatesForNewTwoScriptFiles(string testName, IList<DBVersionsViewStateType> viewStateHistory)
        {
            Assert.That(viewStateHistory.Count == 8, $"{testName} -> Number of process ViewStates should be 8, but was {viewStateHistory.Count}");
            Assert.That(viewStateHistory[0] == DBVersionsViewStateType.InProcess, $"{testName} -> ViewState 1 should be {DBVersionsViewStateType.InProcess}, but was '{viewStateHistory[0]}'");
            Assert.That(viewStateHistory[1] ==  DBVersionsViewStateType.ReadyToRunSync, $"{testName} -> ViewState 2 should be {DBVersionsViewStateType.ReadyToRunSync}, but was '{viewStateHistory[1]}'");
            Assert.That(viewStateHistory[2] == DBVersionsViewStateType.InProcess, $"{testName} -> ViewState 1 should be {DBVersionsViewStateType.InProcess}, but was '{viewStateHistory[2]}'");
            Assert.That(viewStateHistory[3] == DBVersionsViewStateType.ReadyToRunSync, $"{testName} -> ViewState 2 should be {DBVersionsViewStateType.ReadyToRunSync}, but was '{viewStateHistory[3]}'");
            Assert.That(viewStateHistory[4] == DBVersionsViewStateType.InProcess, $"{testName} -> ViewState 1 should be {DBVersionsViewStateType.InProcess}, but was '{viewStateHistory[4]}'");
            Assert.That(viewStateHistory[5] == DBVersionsViewStateType.ReadyToRunSync, $"{testName} -> ViewState 2 should be {DBVersionsViewStateType.ReadyToRunSync}, but was '{viewStateHistory[5]}'");
            Assert.That(viewStateHistory[6] == DBVersionsViewStateType.InProcess, $"{testName} -> ViewState 1 should be {DBVersionsViewStateType.InProcess}, but was '{viewStateHistory[6]}'");
            Assert.That(viewStateHistory[7] == DBVersionsViewStateType.ReadyToRunSync, $"{testName} -> ViewState 2 should be {DBVersionsViewStateType.ReadyToRunSync}, but was '{viewStateHistory[7]}'");
        }

        public void AssertProcessViewStatesForNewSingleScriptFile(string testName, IList<DBVersionsViewStateType> viewStateHistory)
        {
            Assert.That(viewStateHistory.Count == 4, $"{testName} -> Number of process ViewStates should be 4, but was {viewStateHistory.Count}");
            Assert.That(viewStateHistory[0] == DBVersionsViewStateType.InProcess, $"{testName} -> ViewState 1 should be {DBVersionsViewStateType.InProcess}, but was '{viewStateHistory[0]}'");
            Assert.That(viewStateHistory[1] == DBVersionsViewStateType.ReadyToRunSync, $"{testName} -> ViewState 2 should be {DBVersionsViewStateType.ReadyToRunSync}, but was '{viewStateHistory[1]}'");
            Assert.That(viewStateHistory[2] == DBVersionsViewStateType.InProcess, $"{testName} -> ViewState 1 should be {DBVersionsViewStateType.InProcess}, but was '{viewStateHistory[2]}'");
            Assert.That(viewStateHistory[3] == DBVersionsViewStateType.ReadyToRunSync, $"{testName} -> ViewState 2 should be {DBVersionsViewStateType.ReadyToRunSync}, but was '{viewStateHistory[3]}'");
        }








    }
}
