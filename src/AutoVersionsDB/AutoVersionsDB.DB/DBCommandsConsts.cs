namespace AutoVersionsDB.DB
{
    public static class DBCommandsConsts
    {
        public const string DBSchemaName = "AutoVersionsDB";

        public const string DBScriptsExecutionHistoryTableName = "DBScriptsExecutionHistory";
        public static string DBScriptsExecutionHistoryFullTableName => $"{DBSchemaName}.{DBScriptsExecutionHistoryTableName}";

        public const string DBScriptsExecutionHistoryFilesTableName = "DBScriptsExecutionHistoryFiles";
        public static string DBScriptsExecutionHistoryFilesFullTableName => $"{DBSchemaName}.{DBScriptsExecutionHistoryFilesTableName}";


        public const int DBLongProcessGetStatusIntervalInMs = 800;

    }
}
