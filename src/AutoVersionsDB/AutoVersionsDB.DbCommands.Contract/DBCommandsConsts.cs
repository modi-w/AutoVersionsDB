
namespace AutoVersionsDB.DbCommands.Contract
{
    public static class DBCommandsConsts
    {
        public const string C_DB_SchemaName = "AutoVersionsDB";

        public const string C_DBScriptsExecutionHistory_TableName = "DBScriptsExecutionHistory";
        public static string C_DBScriptsExecutionHistory_FullTableName = string.Format("{0}.{1}", C_DB_SchemaName, C_DBScriptsExecutionHistory_TableName);

        public const string C_DBScriptsExecutionHistoryFiles_TableName = "DBScriptsExecutionHistoryFiles";
        public static string C_DBScriptsExecutionHistoryFiles_FullTableName = string.Format("{0}.{1}", C_DB_SchemaName, C_DBScriptsExecutionHistoryFiles_TableName);


        public const int C_DBLongProcessGetStatusIntervalInMs = 800;

    }
}
