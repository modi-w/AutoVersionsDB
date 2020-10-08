﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.Process;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using Ninject;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests
{
    public static class TestsRunner
    {
        private static ProjectConfigsFactory _projectConfigsFactory = NinjectUtils_IntegrationTests.NinjectKernelContainer.Get<ProjectConfigsFactory>();


        public static void RunTests<T1>()
        where T1 : TestDefinition
        {
            TestArgs testArgs = new ProjectConfigTestArgs(null);

            var tests = NinjectUtils_IntegrationTests.GetTestDefinitions<T1>();

            RunTests(tests, testArgs);
        }

        public static void RunTests<T1, T2>()
        where T1 : TestDefinition
              where T2 : TestDefinition
        {
            TestArgs testArgs = new ProjectConfigTestArgs(null);

            var tests = NinjectUtils_IntegrationTests.GetTestDefinitions<T1, T2>();

            RunTests(tests, testArgs);
        }


        public static void RunTestsForeachDBType<T1>()
         where T1 : TestDefinition
        {
            List<ProjectConfigItem> projectConfigs = _projectConfigsFactory.CreateProjectConfigsByDBTyps();

            foreach (var projectConfig in projectConfigs)
            {
                TestArgs testArgs = new ProjectConfigTestArgs(projectConfig);

                var tests = NinjectUtils_IntegrationTests.GetTestDefinitions<T1>();

                RunTests(tests, testArgs);
            }
        }



        public static void RunTestsForeachDBType<T1, T2>()
          where T1 : TestDefinition
          where T2 : TestDefinition
        {
            List<ProjectConfigItem> projectConfigs = _projectConfigsFactory.CreateProjectConfigsByDBTyps();

            foreach (var projectConfig in projectConfigs)
            {
                TestArgs testArgs = new ProjectConfigTestArgs(projectConfig);

                var tests = NinjectUtils_IntegrationTests.GetTestDefinitions<T1, T2>();

                RunTests(tests, testArgs);
            }
        }



        private static void RunTests(IEnumerable<TestDefinition> tests, TestArgs testArgs)
        {
            foreach (var test in tests)
            {
                TestContext testContext = null;

                try
                {
                    testContext = test.Arrange(testArgs);

                    test.Act(testContext);

                    test.Asserts(testContext);
                }
                finally
                {
                    test.Release(testContext);
                }
            }
        }
    }
}
