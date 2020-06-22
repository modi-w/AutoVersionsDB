
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
            where TScriptFileType : ScriptFileTypeBase, new ()
        {
            DBExecutedFilesManager dbExecutedFilesManager = new DBExecutedFilesManager(dbCommands, ScriptFileTypeBase.Create<TScriptFileType>());

            ScriptFilesComparerBase scriptFilesComparer = null;

            if(typeof(TScriptFileType) ==typeof(IncrementalScriptFileType))
            {
                ScriptFilesManager sriptFilesManager = new ScriptFilesManager(_fileChecksumManager, ScriptFileTypeBase.Create<TScriptFileType>(), _incrementalRuntimeScriptFileFactory, folderPath);
                scriptFilesComparer = new IncrementalScriptFilesComparer(sriptFilesManager, dbExecutedFilesManager);
            }
            else if (typeof(TScriptFileType) == typeof(RepeatableScriptFileType))
            {
                ScriptFilesManager sriptFilesManager = new ScriptFilesManager(_fileChecksumManager, ScriptFileTypeBase.Create<TScriptFileType>(), _repeatableRuntimeScriptFileFactory, folderPath);
                scriptFilesComparer = new RepeatableScriptFilesComparer(sriptFilesManager, dbExecutedFilesManager);
            }
            else if (typeof(TScriptFileType) == typeof(DevDummyDataScriptFileType))
            {
                ScriptFilesManager sriptFilesManager = new ScriptFilesManager(_fileChecksumManager, ScriptFileTypeBase.Create<TScriptFileType>(), _devDummyDataruntimeScriptFileFactory, folderPath);
                scriptFilesComparer = new DevDummyDataScriptFilesComparer(sriptFilesManager, dbExecutedFilesManager);
            }

            return scriptFilesComparer;
        }

    }
}
