using AutoVersionsDB.Core.IntegrationTests.Helpers;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests
{
    public class ProjectConfigItemForTest_DevEnv_SqlServer : ProjectConfigItemForTest_DevEnv_ChangeScriptFolderBase
    {

        public ProjectConfigItemForTest_DevEnv_SqlServer(string scriptFolderPath)
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
