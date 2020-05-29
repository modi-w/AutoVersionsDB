using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.ScriptFiles.Incremental;
using AutoVersionsDB.Core.ScriptFiles.Repeatable;
using AutoVersionsDB.DbCommands.Contract;


namespace AutoVersionsDB.Core.ScriptFiles
{
    public class ScriptFilesComparersProvider
    {
        private ScriptFilesComparer_Factory _scriptFilesComparer_Factory;
        private IDBCommands _dbCommands;
        private ProjectConfigItem _projectConfig;

        public ScriptFilesComparerBase IncrementalScriptFilesComparer { get; private set; }
        public ScriptFilesComparerBase RepeatableScriptFilesComparer { get; private set; }
        public ScriptFilesComparerBase DevDummyDataScriptFilesComparer { get; private set; }

        public ScriptFilesComparersProvider(ScriptFilesComparer_Factory scriptFilesComparer_Factory, IDBCommands dbCommands, ProjectConfigItem projectConfig)
        {
            _scriptFilesComparer_Factory = scriptFilesComparer_Factory;
            _dbCommands = dbCommands;
            _projectConfig = projectConfig;

            Reload();
        }

        public void Reload()
        {
            IncrementalScriptFilesComparer = _scriptFilesComparer_Factory.CreateScriptFilesComparer<IncrementalScriptFileType>(_dbCommands, _projectConfig.IncrementalScriptsFolderPath);
            RepeatableScriptFilesComparer = _scriptFilesComparer_Factory.CreateScriptFilesComparer<RepeatableScriptFileType>(_dbCommands, _projectConfig.RepeatableScriptsFolderPath);

            if (_projectConfig.IsDevEnvironment)
            {
                DevDummyDataScriptFilesComparer  = _scriptFilesComparer_Factory.CreateScriptFilesComparer<DevDummyDataScriptFileType>(_dbCommands, _projectConfig.DevDummyDataScriptsFolderPath);
            }
            else
            {
                DevDummyDataScriptFilesComparer = null;
            }
        }

    }
}
