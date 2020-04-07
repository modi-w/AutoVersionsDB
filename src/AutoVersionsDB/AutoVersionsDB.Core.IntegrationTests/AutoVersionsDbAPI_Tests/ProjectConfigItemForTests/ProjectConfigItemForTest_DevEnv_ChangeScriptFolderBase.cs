using AutoVersionsDB.Core.IntegrationTests.Helpers;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests
{
    public abstract class ProjectConfigItemForTest_DevEnv_ChangeScriptFolderBase : ProjectConfigItemForTest_DevEnvBase
    {

        public ProjectConfigItemForTest_DevEnv_ChangeScriptFolderBase(string scriptFolderPath)
        {
            DevScriptsBaseFolderPath = FileSystemHelpers.ParsePathVaribles(scriptFolderPath);
        }


    }
}
