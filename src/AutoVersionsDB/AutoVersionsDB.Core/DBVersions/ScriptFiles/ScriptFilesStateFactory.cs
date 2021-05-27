using AutoVersionsDB.Core.DBVersions.ArtifactFile;
using AutoVersionsDB.DB;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles
{
    public class ScriptFilesStateFactory
    {
        private readonly DBCommandsFactory _dbCommandsFactory;
        private readonly ScriptFilesComparerFactory _scriptFilesComparerFactory;
        private readonly ArtifactExtractorFactory _artifactExtractorFactory;

        public ScriptFilesStateFactory(DBCommandsFactory dbCommandsFactory,
                                       ScriptFilesComparerFactory scriptFilesComparerFactory,
                                        ArtifactExtractorFactory artifactExtractorFactory)
        {
            _dbCommandsFactory = dbCommandsFactory;
            _scriptFilesComparerFactory = scriptFilesComparerFactory;
            _artifactExtractorFactory = artifactExtractorFactory;
        }

        public ScriptFilesState Create()
        {
            ScriptFilesState scriptFilesState = new ScriptFilesState(_dbCommandsFactory, _scriptFilesComparerFactory, _artifactExtractorFactory);

            return scriptFilesState;
        }
    }
}
