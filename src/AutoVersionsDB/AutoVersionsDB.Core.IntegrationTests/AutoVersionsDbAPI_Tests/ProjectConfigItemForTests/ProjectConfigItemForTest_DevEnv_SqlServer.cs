namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests
{
    public class ProjectConfigItemForTest_DevEnv_SqlServer : ProjectConfigItemForTest_DevEnv_ChangeScriptFolderBase
    {

        public ProjectConfigItemForTest_DevEnv_SqlServer(string scriptFolderPath)
            : base(scriptFolderPath)
        {
            DBTypeCode = "SqlServer";
            ConnStr = AppGlobals.AppSetting.SQLServer_ConnStr;
            ConnStrToMasterDB = AppGlobals.AppSetting.SQLServer_ConnStrToMaster;
        }


    }
}
