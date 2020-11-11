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



        public void AssertViewStateHistory(string testName, IList<EditProjectViewStateType> viewStateHistory, EditProjectViewStateType finalViewState)
        {
            Assert.That(viewStateHistory.Count == 2, $"{testName} -> Number of process ViewStates should be 2, but was {viewStateHistory.Count}");
            Assert.That(viewStateHistory[0] == EditProjectViewStateType.InProcess, $"{testName} -> ViewState 1 should be {EditProjectViewStateType.InProcess}, but was '{viewStateHistory[0]}'");
            Assert.That(viewStateHistory[1] == finalViewState, $"{testName} -> ViewState 2 should be {finalViewState}, but was '{viewStateHistory[1]}'");
        }









    }
}
