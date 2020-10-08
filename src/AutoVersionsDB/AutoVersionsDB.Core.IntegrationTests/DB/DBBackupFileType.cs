using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DB
{
    public enum DBBackupFileType
    {
        None = 0,
        EmptyDB = 1,
        EmptyDBWithSystemTables = 2,
        AfterRunInitStateScript = 3,
        MiddleState = 4,
        FinalState_DevEnv = 5,
        FinalState_DeliveryEnv = 6,
        FinalState_MissingSystemTables = 7,
        AddDataAfterFinalState = 8,
    }
}
