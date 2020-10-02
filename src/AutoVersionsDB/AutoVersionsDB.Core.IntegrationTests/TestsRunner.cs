using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.Process;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests
{
    public static class TestsRunner
    {
        public static void RunTest<T1, T2>(bool devEnvironment, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
          where T1 : ITestDefinition
          where T2 : ITestDefinition
        {
            List<ProjectConfigItem> projectConfigs = ProjectConfigsFactory.Create(devEnvironment, scriptFilesStateType);

            foreach (var projectConfig in projectConfigs)
            {
                var tests = NinjectUtils_IntegrationTests.GetTestDefinitions<T1, T2>();

                foreach (var test in tests)
                {
                    TestContext testContext = test.Arrange(projectConfig, dbBackupFileType, scriptFilesStateType);

                    test.Act(testContext);

                    test.Asserts(testContext);
                }
            }
        }
    }
}
