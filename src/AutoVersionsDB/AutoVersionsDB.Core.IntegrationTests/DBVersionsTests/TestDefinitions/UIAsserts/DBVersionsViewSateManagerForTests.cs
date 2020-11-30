using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.UI;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts
{
    public class DBVersionsViewSateManagerForTests : IDBVersionsViewSateManager
    {
        private readonly DBVersionsViewSateManager _dbVersionsViewSateManager;

        public DBVersionsViewStateType LastViewState => _dbVersionsViewSateManager.LastViewState;


        public DBVersionsViewSateManagerForTests(DBVersionsViewSateManager dbVersionsViewSateManager)
        {
            _dbVersionsViewSateManager = dbVersionsViewSateManager;
        }

        public void ChangeViewState(DBVersionsViewStateType viewType)
        {
            _dbVersionsViewSateManager.ChangeViewState(viewType);

            ChangeViewStateForMockSniffer(LastViewState);
        }





        public void ChangeViewStateAfterProcessComplete(ProcessTrace processResults)
        {
            _dbVersionsViewSateManager.ChangeViewStateAfterProcessComplete(processResults);

            ChangeViewStateForMockSniffer(LastViewState);
        }

        public virtual void ChangeViewStateForMockSniffer(DBVersionsViewStateType viewType)
        {

        }

    }
}
