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

        public void AssertCompleteSuccessfully(string testName,DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
            _dbVersionsViewStateAsserts.AssertNotificationsViewModelCompleteSuccessfully(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }

        public void AssertProcessViewStates(string testName, IList<DBVersionsViewStateType> viewStateHistory, DBVersionsViewStateType finalViewState)
        {
            Assert.That(viewStateHistory.Count == 2, $"{testName} -> Number of process ViewStates should be 2");
            Assert.That(viewStateHistory[0] == DBVersionsViewStateType.InProcess, $"{testName} -> ViewState 1 should be {DBVersionsViewStateType.InProcess}");
            Assert.That(viewStateHistory[1] == DBVersionsViewStateType.ReadyToRunSync, $"{testName} -> ViewState 2 should be {finalViewState}");

        }








    }
}
