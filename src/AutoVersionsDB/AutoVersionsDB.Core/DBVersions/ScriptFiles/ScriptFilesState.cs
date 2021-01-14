using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ArtifactFile;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;
using AutoVersionsDB.DB;
using AutoVersionsDB.Helpers;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles
{
    public class ScriptFilesState
    {
        private readonly DBCommandsFactory _dbCommandsFactory;
        private readonly ScriptFilesComparerFactory _scriptFilesComparerFactory;
        private readonly ArtifactExtractorFactory _artifactExtractorFactory;

        public Dictionary<string, ScriptFilesComparerBase> ScriptFilesComparers { get; }

        public ScriptFilesComparerBase IncrementalScriptFilesComparer { get; private set; }
        public ScriptFilesComparerBase RepeatableScriptFilesComparer { get; private set; }
        public ScriptFilesComparerBase DevDummyDataScriptFilesComparer { get; private set; }

        public ScriptFilesState(DBCommandsFactory dbCommandsFactory,
                                ScriptFilesComparerFactory scriptFilesComparerFactory,
                                ArtifactExtractorFactory artifactExtractorFactory)
        {
            dbCommandsFactory.ThrowIfNull(nameof(dbCommandsFactory));
            scriptFilesComparerFactory.ThrowIfNull(nameof(scriptFilesComparerFactory));
            artifactExtractorFactory.ThrowIfNull(nameof(artifactExtractorFactory));

            _dbCommandsFactory = dbCommandsFactory;
            _scriptFilesComparerFactory = scriptFilesComparerFactory;
            _artifactExtractorFactory = artifactExtractorFactory;

            ScriptFilesComparers = new Dictionary<string, ScriptFilesComparerBase>();
        }



        public void Reload(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            using (ArtifactExtractor _currentArtifactExtractor = _artifactExtractorFactory.Create(projectConfig))
            {
                using (var dbCommands = _dbCommandsFactory.CreateDBCommand(projectConfig.DBConnectionInfo))
                {
                    IncrementalScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<IncrementalScriptFileType>(dbCommands, projectConfig.IncrementalScriptsFolderPath);
                    ScriptFilesComparers[IncrementalScriptFilesComparer.ScriptFileType.FileTypeCode] = IncrementalScriptFilesComparer;

                    RepeatableScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<RepeatableScriptFileType>(dbCommands, projectConfig.RepeatableScriptsFolderPath);
                    ScriptFilesComparers[RepeatableScriptFilesComparer.ScriptFileType.FileTypeCode] = RepeatableScriptFilesComparer;

                    if (projectConfig.DevEnvironment)
                    {
                        DevDummyDataScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<DevDummyDataScriptFileType>(dbCommands, projectConfig.DevDummyDataScriptsFolderPath);
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
