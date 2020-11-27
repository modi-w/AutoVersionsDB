using System;
using System.Collections.Generic;
using System.Data;


namespace AutoVersionsDB.DbCommands.Contract
{
    public interface IDBCommands //: IDisposable
    {
        string GetDataBaseName();

        DataTable GetEmptyTable(string tableName);

        DataTable GetTable(string tableName);


        void UpdateScriptsExecutionToDB(ScriptsExecution scriptsExecution);


        DataTable GetExecutedFilesFromDBByFileTypeCode(string scriptFileType);

        IEnumerable<string> SplitSqlStatementsToExecutionBlocks(string sqlUnifyScript);



        void ExecSQLCommandStr(string commandStr);

        bool CheckIfTableExist(string schemaName, string tableName);

        bool CheckIfStoredProcedureExist(string schemaName, string spName);

        DataTable GetAllDBSchemaExceptDBVersionSchema();

        void RecreateDBVersionsTables();

        void DropAllDBObjects();
    }
}
