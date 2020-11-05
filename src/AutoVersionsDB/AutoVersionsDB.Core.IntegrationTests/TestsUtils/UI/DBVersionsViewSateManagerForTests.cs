using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils.UI
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





        public void ChangeViewState_AfterProcessComplete(ProcessTrace processResults)
        {
            _dbVersionsViewSateManager.ChangeViewState_AfterProcessComplete(processResults);

            ChangeViewStateForMockSniffer(LastViewState);
        }

        public virtual void ChangeViewStateForMockSniffer(DBVersionsViewStateType viewType)
        {

        }

    }
}
