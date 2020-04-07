using AutoVersionsDB.Core.IntegrationTests.Helpers;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests
{
    public abstract class ProjectConfigItemForTest_DeliveryEnv_ChangeScriptFolderBase : ProjectConfigItemForTest_DeliveryEnvBase
    {

        public ProjectConfigItemForTest_DeliveryEnv_ChangeScriptFolderBase(string scriptFolderPath)
        {
            DeliveryArtifactFolderPath = FileSystemHelpers.ParsePathVaribles(scriptFolderPath);
        }


    }
}
