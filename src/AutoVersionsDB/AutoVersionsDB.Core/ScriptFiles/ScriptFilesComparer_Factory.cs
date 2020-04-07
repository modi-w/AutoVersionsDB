
using AutoVersionsDB.Core.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.ScriptFiles.Incremental;
using AutoVersionsDB.Core.ScriptFiles.Repeatable;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;

namespace AutoVersionsDB.Core.ScriptFiles
{


    public class ScriptFilesComparer_Factory
    {
        private FileChecksumManager _fileChecksumManager;
        private IncrementalRuntimeScriptFile_Factory _incrementalRuntimeScriptFile_Factory;
        private RepeatableRuntimeScriptFile_Factory _repeatableRuntimeScriptFile_Factory;
        private DevDummyDataRuntimeScriptFile_Factory _devDummyDataruntimeScriptFile_Factory;

        public ScriptFilesComparer_Factory(FileChecksumManager fileChecksumManager,
                                            IncrementalRuntimeScriptFile_Factory incrementalRuntimeScriptFile_Factory,
                                            RepeatableRuntimeScriptFile_Factory repeatableRuntimeScriptFile_Factory,
                                            DevDummyDataRuntimeScriptFile_Factory devDummyDataruntimeScriptFile_Factory)
        {
            _fileChecksumManager = fileChecksumManager;
            _incrementalRuntimeScriptFile_Factory = incrementalRuntimeScriptFile_Factory;
            _repeatableRuntimeScriptFile_Factory = repeatableRuntimeScriptFile_Factory;
            _devDummyDataruntimeScriptFile_Factory = devDummyDataruntimeScriptFile_Factory;
        }




        public ScriptFilesComparerBase CreateScriptFilesComparer<TScriptFileType>(IDBCommands dbCommands, string folderPath)
            where TScriptFileType : ScriptFileTypeBase, new ()
        {
            DBExecutedFilesManager dbExecutedFilesManager = new DBExecutedFilesManager(dbCommands, ScriptFileTypeBase.Create<TScriptFileType>());

            ScriptFilesComparerBase scriptFilesComparer = null;

            if(typeof(TScriptFileType) ==typeof(IncrementalScriptFileType))
            {
                ScriptFilesManager sriptFilesManager = new ScriptFilesManager(_fileChecksumManager, ScriptFileTypeBase.Create<TScriptFileType>(), _incrementalRuntimeScriptFile_Factory, folderPath);
                scriptFilesComparer = new IncrementalScriptFilesComparer(sriptFilesManager, dbExecutedFilesManager);
            }
            else if (typeof(TScriptFileType) == typeof(RepeatableScriptFileType))
            {
                ScriptFilesManager sriptFilesManager = new ScriptFilesManager(_fileChecksumManager, ScriptFileTypeBase.Create<TScriptFileType>(), _repeatableRuntimeScriptFile_Factory, folderPath);
                scriptFilesComparer = new RepeatableScriptFilesComparer(sriptFilesManager, dbExecutedFilesManager);
            }
            else if (typeof(TScriptFileType) == typeof(DevDummyDataScriptFileType))
            {
                ScriptFilesManager sriptFilesManager = new ScriptFilesManager(_fileChecksumManager, ScriptFileTypeBase.Create<TScriptFileType>(), _devDummyDataruntimeScriptFile_Factory, folderPath);
                scriptFilesComparer = new DevDummyDataScriptFilesComparer(sriptFilesManager, dbExecutedFilesManager);
            }

            return scriptFilesComparer;
        }

    }
}
