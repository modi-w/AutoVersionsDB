using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestContexts
{
    public interface IDBTestContext
    {
        DBBackupFileType DBBackupFileType { get; }
        ScriptFilesStateType ScriptFilesStateType { get; }
        NumOfDBConnections NumOfConnectionsBefore { get; set; }
    }


}
