using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{
    public enum DBBackupFileType
    {
        None = 0,
        EmptyDB = 1,
        ExceptSystemTables = 2,
        AfterRunInitStateScript = 3,
        MiddleState = 4,
        FinalState_DevEnv = 5,
        FinalState_DeliveryEnv = 6,
        FinalState_MissingSystemTables = 7,
        AddDataAfterFinalState = 8,
    }

    public static class DBBackupFilesProvider
    {

        public static string GetDBBackupFilePath(DBBackupFileType dbBackupFileType, string dbType)
        {
            string filePath;

            switch (dbBackupFileType)
            {
                case DBBackupFileType.EmptyDB:

                    filePath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DBBackup_EmptyDB).Replace("[DBType]", dbType);
                    break;
             
                case DBBackupFileType.ExceptSystemTables:

                    filePath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DBBackup_ExceptSystemTables).Replace("[DBType]", dbType);
                    break;

                case DBBackupFileType.AfterRunInitStateScript:

                    filePath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DBBackup_AfterRunInitStateScript).Replace("[DBType]", dbType);
                    break;

                case DBBackupFileType.MiddleState:

                    filePath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DBBackup_MiddleState).Replace("[DBType]", dbType);
                    break;

                case DBBackupFileType.FinalState_DevEnv:

                    filePath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DBBackup_FinalState_DevEnv).Replace("[DBType]", dbType);
                    break;

                case DBBackupFileType.FinalState_DeliveryEnv:

                    filePath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DBBackup_FinalState_DeliveryEnv).Replace("[DBType]", dbType);
                    break;

                case DBBackupFileType.FinalState_MissingSystemTables:

                    filePath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DBBackup_FinalState_MissingSystemTables).Replace("[DBType]", dbType);
                    break;

                case DBBackupFileType.AddDataAfterFinalState:

                    filePath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DBBackup_AddDataAfterFinalState).Replace("[DBType]", dbType);
                    break;

                default:
                    throw new Exception($"Invalid DBBackupFileType: '{dbBackupFileType}'");
            }

            return filePath;
        }
    }
}
