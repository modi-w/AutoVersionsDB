namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests
{
    public class ProjectConfigItemForTest_DeliveryEnv_SqlServer : ProjectConfigItemForTest_DeliveryEnv_ChangeScriptFolderBase
    {

        public ProjectConfigItemForTest_DeliveryEnv_SqlServer(string scriptFolderPath)
            : base(scriptFolderPath)
        {
            DBTypeCode = "SqlServer";
            ConnStr = AppGlobals.AppSetting.SQLServer_ConnStr;
            ConnStrToMasterDB = AppGlobals.AppSetting.SQLServer_ConnStrToMaster;
        }


    }
}
