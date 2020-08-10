﻿using AutoVersionsDB.DbCommands.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.ScriptFiles
{
    public class DBExecutedFiles
    {
        private IDBCommands _dbCommands;

        public ScriptFileTypeBase ScriptFileType { get; private set; }

        public List<DataRow> ExecutedFilesList { get; private set; }

        public string LastFileOfLastExecutedFilename { get; private set; }


        public DBExecutedFiles(IDBCommands dbCommands,
                                ScriptFileTypeBase scriptFileType)
        {
            _dbCommands = dbCommands;
            ScriptFileType = scriptFileType;
            Load();
        }


        public void Load()
        {
            loadExecutedFilesList();

            DataRow lastRow = ExecutedFilesList.LastOrDefault();

            if (lastRow != null)
            {
                LastFileOfLastExecutedFilename = Convert.ToString(lastRow["Filename"], CultureInfo.InvariantCulture);
            }

        }


        private void loadExecutedFilesList()
        {
            if (isSystemTablesExist())
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

        private bool isSystemTablesExist()
        {
            return _dbCommands.CheckIfTableExist(DBCommandsConsts.DbSchemaName, DBCommandsConsts.DbScriptsExecutionHistoryTableName)
                     && _dbCommands.CheckIfTableExist(DBCommandsConsts.DbSchemaName, DBCommandsConsts.DbScriptsExecutionHistoryFilesTableName);
        }
    }
}