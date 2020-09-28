using AutoVersionsDB.Core.ConfigProjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{
    public static class FoldersHelper
    {
        public static void RemoveArtifactTempFolder(ProjectConfigItem projectConfig)
        {
            if (Directory.Exists(projectConfig.DeliveryExtractedFilesArtifactFolder))
            {
                Directory.Delete(projectConfig.DeliveryExtractedFilesArtifactFolder, true);
            }
        }
    }
}
