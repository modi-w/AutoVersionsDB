using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
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
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.UIAsserts
{
    public class EditProjectViewModelAsserts
    {
        private readonly EditProjectViewStateAsserts _editProjectViewStateAsserts;

        public EditProjectViewModelAsserts(EditProjectViewStateAsserts editProjectViewStateAsserts)
        {
            _editProjectViewStateAsserts = editProjectViewStateAsserts;
        }

        //public void AssertCompleteSuccessfullyAllFilesSync(string testName, DBVersionsViewModel dbVersionsViewModel, bool isDevEnv)
        //{
        //    _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataDBFinalState(testName, dbVersionsViewModel.DBVersionsViewModelData, isDevEnv);
        //    _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(testName, dbVersionsViewModel.DBVersionsControls, isDevEnv);
        //    _dbVersionsViewStateAsserts.AssertNotificationsViewModelCompleteSuccessfully(testName, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        //}





        public void AssertViewStateHistory(string testName, IList<EditProjectViewStateType> actualViewStateHistory, IList<EditProjectViewStateType> expectedViewStateHistory)
        {
            Assert.That(actualViewStateHistory.Count == expectedViewStateHistory.Count, $"{testName} -> Number of process ViewStates should be {expectedViewStateHistory.Count}, but was {actualViewStateHistory.Count}");
            for (int i = 0; i < expectedViewStateHistory.Count; i++)
            {
                var expectedViewStateItem = expectedViewStateHistory[i];
                var actualViewStateItem = actualViewStateHistory[i];

                Assert.That(actualViewStateItem == expectedViewStateItem, $"{testName} -> ViewState {i + 1} should be {expectedViewStateItem}, but was '{actualViewStateItem}'");
            }
        }








    }
}
