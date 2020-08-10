using AutoVersionsDB.Core.ConfigProjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.ArtifactFile
{
    public static class ArtifactExtractorFactory
    {
        public static ArtifactExtractor Create(ProjectConfigItem projectConfigItem)
        {
            ArtifactExtractor newArtifactExtractor = new ArtifactExtractor(projectConfigItem);

            return newArtifactExtractor;
        }





    }
}
