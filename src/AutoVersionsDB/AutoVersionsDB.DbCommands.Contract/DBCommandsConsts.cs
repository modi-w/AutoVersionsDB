
namespace AutoVersionsDB.DbCommands.Contract
{
    public static class DBCommandsConsts
    {
        public const string DbSchemaName = "AutoVersionsDB";

        public const string DbScriptsExecutionHistoryTableName = "DBScriptsExecutionHistory";
        public static string DbScriptsExecutionHistoryFullTableName = $"{DbSchemaName}.{DbScriptsExecutionHistoryTableName}";

        public const string DbScriptsExecutionHistoryFilesTableName = "DBScriptsExecutionHistoryFiles";
        public static string DbScriptsExecutionHistoryFilesFullTableName = $"{DbSchemaName}.{DbScriptsExecutionHistoryFilesTableName}";


        public const int DbLongProcessGetStatusIntervalInMs = 800;

    }
}
