using AutoVersionsDB.Core.IntegrationTests.Helpers;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests
{
    public class ProjectConfigItemForTest_DevEnv_SqlServer : ProjectConfigItemForTest_DevEnv_ChangeScriptFolderBase
    {

        public ProjectConfigItemForTest_DevEnv_SqlServer(string scriptFolderPath)
            : base(scriptFolderPath)
        {
            DBTypeCode = "SqlServer";
            //ConnStr = IntegrationTestsSetting.SQLServer_ConnStr;
            //ConnStrToMasterDB = IntegrationTestsSetting.SQLServer_ConnStrToMaster;
            ConnStr = $"{SqlServerInstanceHelpers.BaseConnStr};Database=AutoVersionsDB.Tests;";
            ConnStrToMasterDB = $"{SqlServerInstanceHelpers.BaseConnStr};Database=Master;";
        }


    }
}
