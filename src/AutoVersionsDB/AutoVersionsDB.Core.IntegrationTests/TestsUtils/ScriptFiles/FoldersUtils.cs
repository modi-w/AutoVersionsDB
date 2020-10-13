using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles
{
    public class FoldersUtils
    {
        public void RemoveArtifactTempFolder(ProjectConfigItem projectConfig)
        {
            if (Directory.Exists(projectConfig.DeliveryExtractedFilesArtifactFolder))
            {
                Directory.Delete(projectConfig.DeliveryExtractedFilesArtifactFolder, true);
            }
        }
    }
}
