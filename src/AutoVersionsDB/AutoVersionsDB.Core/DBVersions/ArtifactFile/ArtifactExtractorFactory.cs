using AutoVersionsDB.Core.ConfigProjects;

namespace AutoVersionsDB.Core.DBVersions.ArtifactFile
{
    public class ArtifactExtractorFactory
    {
        public virtual ArtifactExtractor Create(ProjectConfigItem projectConfigItem)
        {
            ArtifactExtractor newArtifactExtractor = new ArtifactExtractor(projectConfigItem);

            return newArtifactExtractor;
        }





    }
}
