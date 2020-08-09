
using AutoVersionsDB.Core.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.ScriptFiles.Incremental;
using AutoVersionsDB.Core.ScriptFiles.Repeatable;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;

namespace AutoVersionsDB.Core.ScriptFiles
{

    public class ScriptFilesComparerFactory
    {
        private FileChecksumManager _fileChecksumManager;
        private IncrementalRuntimeScriptFileFactory _incrementalRuntimeScriptFileFactory;
        private RepeatableRuntimeScriptFileFactory _repeatableRuntimeScriptFileFactory;
        private DevDummyDataRuntimeScriptFileFactory _devDummyDataruntimeScriptFileFactory;

        public ScriptFilesComparerFactory(FileChecksumManager fileChecksumManager,
                                            IncrementalRuntimeScriptFileFactory incrementalRuntimeScriptFileFactory,
                                            RepeatableRuntimeScriptFileFactory repeatableRuntimeScriptFileFactory,
                                            DevDummyDataRuntimeScriptFileFactory devDummyDataruntimeScriptFileFactory)
        {
            _fileChecksumManager = fileChecksumManager;
            _incrementalRuntimeScriptFileFactory = incrementalRuntimeScriptFileFactory;
            _repeatableRuntimeScriptFileFactory = repeatableRuntimeScriptFileFactory;
            _devDummyDataruntimeScriptFileFactory = devDummyDataruntimeScriptFileFactory;
        }




        public ScriptFilesComparerBase CreateScriptFilesComparer<TScriptFileType>(IDBCommands dbCommands, string folderPath)
            where TScriptFileType : ScriptFileTypeBase, new()
        {
            DBExecutedFiles dbExecutedFiles = new DBExecutedFiles(dbCommands, ScriptFileTypeBase.Create<TScriptFileType>());

            ScriptFilesComparerBase scriptFilesComparer = null;

            if (typeof(TScriptFileType) == typeof(IncrementalScriptFileType))
            {
                FileSystemScriptFiles fileSystemScriptFiles = new FileSystemScriptFiles(_fileChecksumManager, ScriptFileTypeBase.Create<TScriptFileType>(), folderPath);
                scriptFilesComparer = new IncrementalScriptFilesComparer(fileSystemScriptFiles, dbExecutedFiles);
            }
            else if (typeof(TScriptFileType) == typeof(RepeatableScriptFileType))
            {
                FileSystemScriptFiles fileSystemScriptFiles = new FileSystemScriptFiles(_fileChecksumManager, ScriptFileTypeBase.Create<TScriptFileType>(), folderPath);
                scriptFilesComparer = new RepeatableScriptFilesComparer(fileSystemScriptFiles, dbExecutedFiles);
            }
            else if (typeof(TScriptFileType) == typeof(DevDummyDataScriptFileType))
            {
                FileSystemScriptFiles fileSystemScriptFiles = new FileSystemScriptFiles(_fileChecksumManager, ScriptFileTypeBase.Create<TScriptFileType>(), folderPath);
                scriptFilesComparer = new DevDummyDataScriptFilesComparer(fileSystemScriptFiles, dbExecutedFiles);
            }

            return scriptFilesComparer;
        }

    }
}
