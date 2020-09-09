
using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.ScriptFiles.Incremental;
using AutoVersionsDB.Core.ScriptFiles.Repeatable;
using AutoVersionsDB.DbCommands.Contract;

namespace AutoVersionsDB.Core.ScriptFiles
{

    public class ScriptFilesComparerFactory
    {
        private readonly FileChecksum _fileChecksum;

        public ScriptFilesComparerFactory(FileChecksum fileChecksum)
        {
            _fileChecksum = fileChecksum;
        }




        public ScriptFilesComparerBase CreateScriptFilesComparer<TScriptFileType>(IDBCommands dbCommands, string folderPath)
            where TScriptFileType : ScriptFileTypeBase, new()
        {
            DBExecutedFiles dbExecutedFiles = new DBExecutedFiles(dbCommands, ScriptFileTypeBase.Create<TScriptFileType>());

            ScriptFilesComparerBase scriptFilesComparer = null;

            if (typeof(TScriptFileType) == typeof(IncrementalScriptFileType))
            {
                FileSystemScriptFiles fileSystemScriptFiles = new FileSystemScriptFiles(_fileChecksum, ScriptFileTypeBase.Create<TScriptFileType>(), folderPath);
                scriptFilesComparer = new IncrementalScriptFilesComparer(fileSystemScriptFiles, dbExecutedFiles);
            }
            else if (typeof(TScriptFileType) == typeof(RepeatableScriptFileType))
            {
                FileSystemScriptFiles fileSystemScriptFiles = new FileSystemScriptFiles(_fileChecksum, ScriptFileTypeBase.Create<TScriptFileType>(), folderPath);
                scriptFilesComparer = new RepeatableScriptFilesComparer(fileSystemScriptFiles, dbExecutedFiles);
            }
            else if (typeof(TScriptFileType) == typeof(DevDummyDataScriptFileType))
            {
                FileSystemScriptFiles fileSystemScriptFiles = new FileSystemScriptFiles(_fileChecksum, ScriptFileTypeBase.Create<TScriptFileType>(), folderPath);
                scriptFilesComparer = new DevDummyDataScriptFilesComparer(fileSystemScriptFiles, dbExecutedFiles);
            }

            return scriptFilesComparer;
        }

    }
}
