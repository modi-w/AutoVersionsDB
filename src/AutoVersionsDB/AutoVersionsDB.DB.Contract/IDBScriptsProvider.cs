namespace AutoVersionsDB.DB.Contract
{
    public interface IDBScriptsProvider
    {
        string CheckIfStoredProcedureExistScript(string schemaName, string spName);
        string CheckIfTableExistScript(string schemaName, string tableName);
        string DropAllDBObjectsScript();
        string GetAllDBTablesExceptSchemaScript(string schemaName);
        string GetAllTableDataScript(string tableName);
        string GetEmptyTableScript(string tableName);
        string GetExecutedFilesFromDBByFileTypeCodeScript(string executedFilesTableName, string scriptFileType);
        string RecreateDBVersionsTablesScript();

        string BackupDBScript(string filename, string dbName);
        string RestorDBScript(string filename, string dbName, string dbFilesBasePath);

        string GetDBAccessStateScript(string dbName);
        string GetDBSessionsScript(string dbName);
        string KillSessionScript(string seesionID);

        string DropDBScript(string dbName);
        string GetIsDBExsitScript(string dbName);

        string CreateDBScript(string dbName, string dbFilesBasePath);

        string GetNumOfOpenConnectionScript(string dbName);
        string GetQueryProcessStatusScript(string queryName);
    }
}