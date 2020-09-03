using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.ScriptFiles.Incremental;
using AutoVersionsDB.Core.ScriptFiles.Repeatable;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.ScriptFiles
{
    public class ScriptFilesState
    {
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private readonly ScriptFilesComparerFactory _scriptFilesComparerFactory;
        private readonly ArtifactExtractorFactory _artifactExtractorFactory;

        private readonly Dictionary<string, ScriptFilesComparerBase> _scriptFilesComparersDictionary;

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

            _scriptFilesComparersDictionary = new Dictionary<string, ScriptFilesComparerBase>();
        }



        public void Reload(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            using (ArtifactExtractor _currentArtifactExtractor = _artifactExtractorFactory.Create(projectConfig))
            {
                using (IDBCommands dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout))
                {
                    IncrementalScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<IncrementalScriptFileType>(dbCommands, projectConfig.IncrementalScriptsFolderPath);
                    _scriptFilesComparersDictionary[IncrementalScriptFilesComparer.ScriptFileType.FileTypeCode] = IncrementalScriptFilesComparer;

                    RepeatableScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<RepeatableScriptFileType>(dbCommands, projectConfig.RepeatableScriptsFolderPath);
                    _scriptFilesComparersDictionary[RepeatableScriptFilesComparer.ScriptFileType.FileTypeCode] =RepeatableScriptFilesComparer;

                    if (projectConfig.IsDevEnvironment)
                    {
                        DevDummyDataScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<DevDummyDataScriptFileType>(dbCommands, projectConfig.DevDummyDataScriptsFolderPath);
                        _scriptFilesComparersDictionary[DevDummyDataScriptFilesComparer.ScriptFileType.FileTypeCode]= DevDummyDataScriptFilesComparer;
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
            _scriptFilesComparersDictionary.TryGetValue(fileTypeCode, out ScriptFilesComparerBase scriptFilesComparer);

            return scriptFilesComparer;
        }



    }
}
