using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI.DBVersions;
using AutoVersionsDB.UI.EditProject;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions
{

    public class EditProjectAPITestContext : ProjectConfigTestContext
    {
        public List<EditProjectViewStateType> ViewStatesHistory { get; set; }

        public EditProjectAPITestContext(ProjectConfigTestArgs projectConfigTestArgs)
            : base(projectConfigTestArgs)
        {
            ViewStatesHistory = new List<EditProjectViewStateType>();
        }


        public override void ClearProcessData()
        {
            base.ClearProcessData();

            ViewStatesHistory.Clear();
        }

    }
}
