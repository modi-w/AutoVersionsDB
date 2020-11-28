//using System;
//using System.Collections.Generic;
//using System.Data;


//namespace AutoVersionsDB.DbCommands.Contract
//{
//    public interface DBCommands //: IDisposable
//    {
//        string GetDataBaseName();

//        DataSet GetScriptsExecutionHistoryTableStructureFromDB();

//        void UpdateScriptsExecutionHistoryTableToDB(DataTable dbScriptsExecutionHistoryTable);

//        void UpdateScriptsExecutionHistoryFilesTableToDB(DataTable dbScriptsExecutionHistoryFilesTable);

//        DataTable GetExecutedFilesFromDBByFileTypeCode(string scriptFileType);

//        IEnumerable<string> SplitSqlStatementsToExecutionBlocks(string sqlUnifyScript);


//        DataTable GetTable(string tableName);

//        void ExecSQLCommandStr(string commandStr);

//        bool CheckIfTableExist(string schemaName, string tableName);

//        bool CheckIfStoredProcedureExist(string schemaName, string spName);

//        DataTable GetAllDBSchemaExceptDBVersionSchema();

//        void RecreateDBVersionsTables();

//        void DropAllDB();
//    }
//}
