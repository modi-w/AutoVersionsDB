using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ArtifactFile;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles
{
    public class ScriptFilesState
    {
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private readonly ScriptFilesComparerFactory _scriptFilesComparerFactory;
        private readonly ArtifactExtractorFactory _artifactExtractorFactory;

        public Dictionary<string, ScriptFilesComparerBase> ScriptFilesComparers { get; }

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

            ScriptFilesComparers = new Dictionary<string, ScriptFilesComparerBase>();
        }



        public void Reload(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            using (ArtifactExtractor _currentArtifactExtractor = _artifactExtractorFactory.Create(projectConfig))
            {
                using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBType, projectConfig.ConnectionString, projectConfig.DBCommandsTimeout).AsDisposable())
                {
                    IncrementalScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<IncrementalScriptFileType>(dbCommands.Instance, projectConfig.IncrementalScriptsFolderPath);
                    ScriptFilesComparers[IncrementalScriptFilesComparer.ScriptFileType.FileTypeCode] = IncrementalScriptFilesComparer;

                    RepeatableScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<RepeatableScriptFileType>(dbCommands.Instance, projectConfig.RepeatableScriptsFolderPath);
                    ScriptFilesComparers[RepeatableScriptFilesComparer.ScriptFileType.FileTypeCode] = RepeatableScriptFilesComparer;

                    if (projectConfig.DevEnvironment)
                    {
                        DevDummyDataScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<DevDummyDataScriptFileType>(dbCommands.Instance, projectConfig.DevDummyDataScriptsFolderPath);
                        ScriptFilesComparers[DevDummyDataScriptFilesComparer.ScriptFileType.FileTypeCode] = DevDummyDataScriptFilesComparer;
                    }
                    else
                    {
                        DevDummyDataScriptFilesComparer = null;
                    }
                }
            }

        }


        public ScriptFilesComparerBase GetScriptFilesComparerByType(string fileTypeCode)
        {
            ScriptFilesComparers.TryGetValue(fileTypeCode, out ScriptFilesComparerBase scriptFilesComparer);

            return scriptFilesComparer;
        }



    }
}
