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
    public class ArtifactExtractor_Factory
    {
        public ArtifactExtractor_Factory()
        {

        }

        public ArtifactExtractor Create(ProjectConfigItem projectConfigItem)
        {
            ArtifactExtractor newArtifactExtractor = new ArtifactExtractor(projectConfigItem);

            return newArtifactExtractor;
        }





    }
}
