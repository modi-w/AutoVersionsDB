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
    public class ArtifactExtractorFactory
    {
#pragma warning disable CA1822 // Mark members as static. we dont set it as static becuase we need to inject and mock the class for unit testing
        public ArtifactExtractor Create(ProjectConfigItem projectConfigItem)
#pragma warning restore CA1822 // Mark members as static
        {
            ArtifactExtractor newArtifactExtractor = new ArtifactExtractor(projectConfigItem);

            return newArtifactExtractor;
        }





    }
}
