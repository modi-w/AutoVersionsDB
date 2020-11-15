using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestContexts
{
    public interface ITestContextResults
    {
        ProcessResults ProcessResults { get; set; }
        object Result { get; set; }
        void ClearProcessData();
    }


}
