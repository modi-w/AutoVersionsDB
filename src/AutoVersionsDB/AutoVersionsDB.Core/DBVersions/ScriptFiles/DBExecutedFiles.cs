using AutoVersionsDB.DbCommands.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles
{
    public class DBExecutedFiles
    {
        private readonly DBCommands _dbCommands;

        public ScriptFileTypeBase ScriptFileType { get; private set; }

        public List<DataRow> ExecutedFilesList { get; private set; }

        public string LastFileOfLastExecutedFilename { get; private set; }


        public DBExecutedFiles(DBCommands dbCommands,
                                ScriptFileTypeBase scriptFileType)
        {
            _dbCommands = dbCommands;
            ScriptFileType = scriptFileType;
            Load();
        }


        public void Load()
        {
            LoadExecutedFilesList();

            DataRow lastRow = ExecutedFilesList.LastOrDefault();

            if (lastRow != null)
            {
                LastFileOfLastExecutedFilename = Convert.ToString(lastRow["Filename"], CultureInfo.InvariantCulture);
            }

        }


        private void LoadExecutedFilesList()
        {
            if (IsSystemTablesExist())
            {


                ExecutedFilesList =
                _dbCommands.GetExecutedFilesFromDBByFileTypeCode(ScriptFileType.FileTypeCode)
                .Rows
                .Cast<DataRow>() //Instead of: .AsEnumerable()
                .OrderBy(row => Convert.ToInt32(row["ID"], CultureInfo.InvariantCulture))
                .ToList();
            }
            else
            {
                ExecutedFilesList = new List<DataRow>();
            }
        }

        private bool IsSystemTablesExist()
        {
            return _dbCommands.CheckIfTableExist(DBCommandsConsts.DbSchemaName, DBCommandsConsts.DbScriptsExecutionHistoryTableName)
                     && _dbCommands.CheckIfTableExist(DBCommandsConsts.DbSchemaName, DBCommandsConsts.DbScriptsExecutionHistoryFilesTableName);
        }
    }
}
