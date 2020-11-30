using AutoVersionsDB.DB.Contract;
using AutoVersionsDB.Helpers;

namespace AutoVersionsDB.DB.SqlServer
{
    public class SQLServerDBScriptsProvider : IDBScriptsProvider
    {

        public string GetEmptyTableScript(string tableName)
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("GetEmptyTable.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{tableName}", tableName);

            return sqlCmdStr;
        }



        public string GetExecutedFilesFromDBByFileTypeCodeScript(string executedFilesTableName, string scriptFileType)
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("GetExecutedFilesFromDBByFileTypeCode.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{executedFilesTableName}", executedFilesTableName)
                .Replace("{scriptFileType}", scriptFileType);

            return sqlCmdStr;
        }


        public string CheckIfTableExistScript(string schemaName, string tableName)
        {
            schemaName.ThrowIfNull(nameof(schemaName));
            tableName.ThrowIfNull(nameof(tableName));

            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("CheckIfTableExist.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{schemaName}", schemaName.Trim('[').Trim(']'))
                .Replace("{tableName}", tableName.Trim('[').Trim(']'));

            return sqlCmdStr;
        }


        public string CheckIfStoredProcedureExistScript(string schemaName, string spName)
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("CheckIfStoredProcedureExist.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{schemaName}", schemaName)
                .Replace("{spName}", spName);

            return sqlCmdStr;
        }

        public string GetAllTableDataScript(string tableName)
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("GetAllTableData.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{tableName}", tableName);

            return sqlCmdStr;
        }

        public string GetAllDBTablesExceptSchemaScript(string schemaName)
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("GetAllDBTablesExceptSchema.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{dbSchemaName}", schemaName);

            return sqlCmdStr;
        }

        public string RecreateDBVersionsTablesScript()
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("RecreateDBVersionsTables.sql");

            return sqlCmdStr;
        }

        public string DropAllDBObjectsScript()
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("DropAllDbObjects.sql");

            return sqlCmdStr;
        }





        public string BackupDBScript(string filename, string dbName)
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("BackupDB.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{dbName}", dbName)
                .Replace("{filename}", filename);

            return sqlCmdStr;
        }

        public string RestorDBScript(string filename, string dbName, string dbFilesBasePath)
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("RestorDB.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{dbName}", dbName)
                .Replace("{filename}", filename);

            string moveDBFilesScriptStr = "";
            if (!string.IsNullOrWhiteSpace(dbFilesBasePath))
            {
                moveDBFilesScriptStr = GetEmbeddedResourceFileSqlServerScript("MoveDBFilesScript.sql");
                moveDBFilesScriptStr =
                    moveDBFilesScriptStr
                    .Replace("{dbName}", dbName)
                    .Replace("{dbFilesBasePath}", dbFilesBasePath);
            }

            sqlCmdStr =
                sqlCmdStr
                .Replace("{moveDBFilesScript}", moveDBFilesScriptStr);



            return sqlCmdStr;
        }



        public string GetDBAccessStateScript(string dbName)
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("GetDBAccessState.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{dbName}", dbName);

            return sqlCmdStr;
        }

        public string GetDBSessionsScript(string dbName)
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("GetDBSessions.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{dbName}", dbName);

            return sqlCmdStr;
        }

        public string KillSessionScript(string seesionID)
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("KillSession.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{seesionID}", seesionID);

            return sqlCmdStr;
        }

        public string DropDBScript(string dbName)
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("DropDB.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{dbName}", dbName);

            return sqlCmdStr;
        }

        public string GetIsDBExsitScript(string dbName)
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("GetIsDBExsit.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{dbName}", dbName);

            return sqlCmdStr;
        }


        public string CreateDBScript(string dbName, string dbFilesBasePath)
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("CreateDB.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{dbName}", dbName);

            string appendDBFilesScriptStr = "";
            if (!string.IsNullOrWhiteSpace(dbFilesBasePath))
            {
                appendDBFilesScriptStr = GetEmbeddedResourceFileSqlServerScript("AppendDBFilesScript.sql");
                appendDBFilesScriptStr =
                    appendDBFilesScriptStr
                    .Replace("{dbName}", dbName)
                    .Replace("{dbFilesBasePath}", dbFilesBasePath);
            }

            sqlCmdStr =
                sqlCmdStr
                .Replace("{appendDBFilesScript}", appendDBFilesScriptStr);



            return sqlCmdStr;
        }



        public string GetNumOfOpenConnectionScript(string dbName)
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("GetNumOfOpenConnection.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{dbName}", dbName);

            return sqlCmdStr;
        }


        public string GetQueryProcessStatusScript(string queryName)
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("GetQueryProcessStatus.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{queryName}", queryName);

            return sqlCmdStr;
        }




        private string GetEmbeddedResourceFileSqlServerScript(string filename)
        {
            string sqlCommandStr =
                EmbeddedResources
                .GetEmbeddedResourceFile($"AutoVersionsDB.DB.SqlServer.DBScripts.{filename}");

            return sqlCommandStr;
        }


    }
}
