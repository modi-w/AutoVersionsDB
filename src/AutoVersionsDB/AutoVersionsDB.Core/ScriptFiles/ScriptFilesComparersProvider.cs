using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.ScriptFiles.Incremental;
using AutoVersionsDB.Core.ScriptFiles.Repeatable;
using AutoVersionsDB.DbCommands.Contract;


namespace AutoVersionsDB.Core.ScriptFiles
{
    public class ScriptFilesComparersProvider
    {
        private ScriptFilesComparerFactory _scriptFilesComparerFactory;
        private IDBCommands _dbCommands;
        private ProjectConfigItem _projectConfig;

        public ScriptFilesComparerBase IncrementalScriptFilesComparer { get; private set; }
        public ScriptFilesComparerBase RepeatableScriptFilesComparer { get; private set; }
        public ScriptFilesComparerBase DevDummyDataScriptFilesComparer { get; private set; }

        public ScriptFilesComparersProvider(ScriptFilesComparerFactory scriptFilesComparerFactory, IDBCommands dbCommands, ProjectConfigItem projectConfig)
        {
            _scriptFilesComparerFactory = scriptFilesComparerFactory;
            _dbCommands = dbCommands;
            _projectConfig = projectConfig;

            Reload();
        }

        public void Reload()
        {
            IncrementalScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<IncrementalScriptFileType>(_dbCommands, _projectConfig.IncrementalScriptsFolderPath);
            RepeatableScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<RepeatableScriptFileType>(_dbCommands, _projectConfig.RepeatableScriptsFolderPath);

            if (_projectConfig.IsDevEnvironment)
            {
                DevDummyDataScriptFilesComparer  = _scriptFilesComparerFactory.CreateScriptFilesComparer<DevDummyDataScriptFileType>(_dbCommands, _projectConfig.DevDummyDataScriptsFolderPath);
            }
            else
            {
                DevDummyDataScriptFilesComparer = null;
            }
        }

    }
}
