using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB
{
    public class DBBackupFilesProvider
    {
        public virtual string GetDBBackupFilePath(DBBackupFileType dbBackupFileType, string dbType)
        {
            string filePath = dbBackupFileType switch
            {
                DBBackupFileType.EmptyDB => FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackup_EmptyDB).Replace("[DBType]", dbType),
                DBBackupFileType.EmptyDBWithSystemTables => FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackup_ExceptSystemTables).Replace("[DBType]", dbType),
                DBBackupFileType.AfterRunInitStateScript => FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackup_AfterRunInitStateScript).Replace("[DBType]", dbType),
                DBBackupFileType.MiddleState => FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackup_MiddleState).Replace("[DBType]", dbType),
                DBBackupFileType.FinalState_DevEnv => FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackup_FinalState_DevEnv).Replace("[DBType]", dbType),
                DBBackupFileType.FinalState_DeliveryEnv => FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackup_FinalState_DeliveryEnv).Replace("[DBType]", dbType),
                DBBackupFileType.FinalState_MissingSystemTables => FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackup_FinalState_MissingSystemTables).Replace("[DBType]", dbType),
                DBBackupFileType.AddDataAfterFinalState => FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackup_AddDataAfterFinalState).Replace("[DBType]", dbType),
                _ => throw new Exception($"Invalid DBBackupFileType: '{dbBackupFileType}'"),
            };
            return filePath;
        }
    }
}
