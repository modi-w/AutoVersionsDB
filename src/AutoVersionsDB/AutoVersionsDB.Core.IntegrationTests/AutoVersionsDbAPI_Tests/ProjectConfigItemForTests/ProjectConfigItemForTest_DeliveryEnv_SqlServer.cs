using AutoVersionsDB.Core.IntegrationTests.DB;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests
{
    public class ProjectConfigItemForTest_DeliveryEnv_SqlServer : ProjectConfigItemForTest_DeliveryEnv_ChangeScriptFolderBase
    {

        public ProjectConfigItemForTest_DeliveryEnv_SqlServer(string scriptFolderPath)
            : base(scriptFolderPath)
        {
            DBType = "SqlServer";
            Server = SqlServerLocalDBConnection.ConnectionStringBuilder.DataSource;
            Username = SqlServerLocalDBConnection.ConnectionStringBuilder.UserID;
            Password = SqlServerLocalDBConnection.ConnectionStringBuilder.Password;
            DBName = "AutoVersionsDB.Tests";
            ////ConnStr = IntegrationTestsSetting.SQLServer_ConnStr;
            ////ConnStrToMasterDB = IntegrationTestsSetting.SQLServer_ConnStrToMaster;
            //ConnectionString = $"{SqlServerInstanceHelpers.BaseConnStr};Database=AutoVersionsDB.Tests;";
            //ConnectionStringToMasterDB = $"{SqlServerInstanceHelpers.BaseConnStr};Database=Master;";
        }


    }
}
