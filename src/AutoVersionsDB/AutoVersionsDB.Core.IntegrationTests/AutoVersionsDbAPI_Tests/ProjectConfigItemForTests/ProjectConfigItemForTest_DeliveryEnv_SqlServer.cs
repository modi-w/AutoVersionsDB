using AutoVersionsDB.Core.IntegrationTests.Helpers;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests
{
    public class ProjectConfigItemForTest_DeliveryEnv_SqlServer : ProjectConfigItemForTest_DeliveryEnv_ChangeScriptFolderBase
    {

        public ProjectConfigItemForTest_DeliveryEnv_SqlServer(string scriptFolderPath)
            : base(scriptFolderPath)
        {
            DBType = "SqlServer";
            Server = SqlServerInstanceHelpers.ConnectionStringBuilder.DataSource;
            Username = SqlServerInstanceHelpers.ConnectionStringBuilder.UserID;
            Password = SqlServerInstanceHelpers.ConnectionStringBuilder.Password;
            DBName = "AutoVersionsDB.Tests";
            ////ConnStr = IntegrationTestsSetting.SQLServer_ConnStr;
            ////ConnStrToMasterDB = IntegrationTestsSetting.SQLServer_ConnStrToMaster;
            //ConnectionString = $"{SqlServerInstanceHelpers.BaseConnStr};Database=AutoVersionsDB.Tests;";
            //ConnectionStringToMasterDB = $"{SqlServerInstanceHelpers.BaseConnStr};Database=Master;";
        }


    }
}
