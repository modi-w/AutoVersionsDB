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
    public class DBBackupFilesProvider
    {
        public string GetDBBackupFilePath(DBBackupFileType dbBackupFileType, string dbType)
        {
            string filePath;

            switch (dbBackupFileType)
            {
                case DBBackupFileType.EmptyDB:

                    filePath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackup_EmptyDB).Replace("[DBType]", dbType);
                    break;

                case DBBackupFileType.ExceptSystemTables:

                    filePath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackup_ExceptSystemTables).Replace("[DBType]", dbType);
                    break;

                case DBBackupFileType.AfterRunInitStateScript:

                    filePath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackup_AfterRunInitStateScript).Replace("[DBType]", dbType);
                    break;

                case DBBackupFileType.MiddleState:

                    filePath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackup_MiddleState).Replace("[DBType]", dbType);
                    break;

                case DBBackupFileType.FinalState_DevEnv:

                    filePath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackup_FinalState_DevEnv).Replace("[DBType]", dbType);
                    break;

                case DBBackupFileType.FinalState_DeliveryEnv:

                    filePath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackup_FinalState_DeliveryEnv).Replace("[DBType]", dbType);
                    break;

                case DBBackupFileType.FinalState_MissingSystemTables:

                    filePath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackup_FinalState_MissingSystemTables).Replace("[DBType]", dbType);
                    break;

                case DBBackupFileType.AddDataAfterFinalState:

                    filePath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackup_AddDataAfterFinalState).Replace("[DBType]", dbType);
                    break;

                default:
                    throw new Exception($"Invalid DBBackupFileType: '{dbBackupFileType}'");
            }

            return filePath;
        }
    }
}
