using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions
{

    public class DBVersionsAPITestContext : ProjectConfigWithDBTestContext
    {
        public List<DBVersionsViewStateType> ViewStatesHistory { get; set; }

        public DBVersionsAPITestContext(ProjectConfigTestArgs projectConfigTestArgs, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
            : base(projectConfigTestArgs, dbBackupFileType, scriptFilesStateType)
        {
            ViewStatesHistory = new List<DBVersionsViewStateType>();
        }


        public override void ClearProcessData()
        {
            base.ClearProcessData();

            ViewStatesHistory.Clear();
        }

    }
}
