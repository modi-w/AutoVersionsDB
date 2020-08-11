using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.ScriptFiles.Incremental;
using AutoVersionsDB.Core.ScriptFiles.Repeatable;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using System;

namespace AutoVersionsDB.Core.ScriptFiles
{
    public class ScriptFilesState
    {
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private readonly ScriptFilesComparerFactory _scriptFilesComparerFactory;
        private readonly ArtifactExtractorFactory _artifactExtractorFactory;
     
        public ScriptFilesComparerBase IncrementalScriptFilesComparer { get; private set; }
        public ScriptFilesComparerBase RepeatableScriptFilesComparer { get; private set; }
        public ScriptFilesComparerBase DevDummyDataScriptFilesComparer { get; private set; }

        public ScriptFilesState(DBCommandsFactoryProvider dbCommandsFactoryProvider,
                                ScriptFilesComparerFactory scriptFilesComparerFactory,
                                ArtifactExtractorFactory artifactExtractorFactory)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));
            scriptFilesComparerFactory.ThrowIfNull(nameof(scriptFilesComparerFactory));
            artifactExtractorFactory.ThrowIfNull(nameof(artifactExtractorFactory));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _scriptFilesComparerFactory = scriptFilesComparerFactory;
            _artifactExtractorFactory = artifactExtractorFactory;

        }



        public void Reload(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            using (ArtifactExtractor _currentArtifactExtractor = _artifactExtractorFactory.Create(projectConfig))
            {
                using (IDBCommands dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout))
                {
                    IncrementalScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<IncrementalScriptFileType>(dbCommands, projectConfig.IncrementalScriptsFolderPath);
                    RepeatableScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<RepeatableScriptFileType>(dbCommands, projectConfig.RepeatableScriptsFolderPath);

                    if (projectConfig.IsDevEnvironment)
                    {
                        DevDummyDataScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<DevDummyDataScriptFileType>(dbCommands, projectConfig.DevDummyDataScriptsFolderPath);
                    }
                    else
                    {
                        DevDummyDataScriptFilesComparer = null;
                    }
                }
            }
        
        }





    }
}
