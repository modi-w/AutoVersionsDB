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

        public void AssertCompleteSuccessfully(DBVersionsViewModel dbVersionsViewModel)
        {
            _scriptFilesListsStateAsserts.AssertDBVersionsViewModelDataCompleteSuccessfully(GetType().Name, dbVersionsViewModel.DBVersionsViewModelData);
            _dbVersionsViewStateAsserts.AssertDBVersionsViewStateCompleteSuccessfully(GetType().Name, dbVersionsViewModel.DBVersionsControls);
            _dbVersionsViewStateAsserts.AssertNotificationsViewModelCompleteSuccessfully(GetType().Name, dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }










    }
}
