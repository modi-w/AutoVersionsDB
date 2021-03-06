﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests
{
    public static class TestsRunner
    {
        private static readonly ProjectConfigsFactory _projectConfigsFactory = DIConfig.Kernel.Get<ProjectConfigsFactory>();

        private static readonly object _testRunSync = new object();

        public static void RunTests<T1>()
            where T1 : TestDefinition
        {
            TestArgs testArgs = new ProjectConfigTestArgs(null);

            var tests = DIConfig.GetTestDefinitions<T1>();

            RunTests(tests, testArgs);
        }

        public static void RunTests<T1, T2>()
            where T1 : TestDefinition
              where T2 : TestDefinition
        {
            TestArgs testArgs = new ProjectConfigTestArgs(null);

            var tests = DIConfig.GetTestDefinitions<T1, T2>();

            RunTests(tests, testArgs);
        }
        public static void RunTests<T1, T2, T3>()
            where T1 : TestDefinition
            where T2 : TestDefinition
            where T3 : TestDefinition
        {
            TestArgs testArgs = new ProjectConfigTestArgs(null);

            var tests = DIConfig.GetTestDefinitions<T1, T2, T3>();

            RunTests(tests, testArgs);
        }
        public static void RunTests<T1, T2, T3, T4>()
            where T1 : TestDefinition
            where T2 : TestDefinition
            where T3 : TestDefinition
            where T4 : TestDefinition
        {
            TestArgs testArgs = new ProjectConfigTestArgs(null);

            var tests = DIConfig.GetTestDefinitions<T1, T2, T3, T4>();

            RunTests(tests, testArgs);
        }





        public static void RunTestsForeachDBType<T1>()
            where T1 : TestDefinition
        {
            List<ProjectConfigItem> projectConfigs = _projectConfigsFactory.CreateProjectConfigsByDBTyps();

            foreach (var projectConfig in projectConfigs)
            {
                TestArgs testArgs = new ProjectConfigTestArgs(projectConfig);

                var tests = DIConfig.GetTestDefinitions<T1>();

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

                var tests = DIConfig.GetTestDefinitions<T1, T2>();

                RunTests(tests, testArgs);
            }
        }

        public static void RunTestsForeachDBType<T1, T2, T3>()
            where T1 : TestDefinition
            where T2 : TestDefinition
            where T3 : TestDefinition
        {
            List<ProjectConfigItem> projectConfigs = _projectConfigsFactory.CreateProjectConfigsByDBTyps();

            foreach (var projectConfig in projectConfigs)
            {
                TestArgs testArgs = new ProjectConfigTestArgs(projectConfig);

                var tests = DIConfig.GetTestDefinitions<T1, T2, T3>();

                RunTests(tests, testArgs);
            }
        }

        public static void RunTestsForeachDBType<T1, T2, T3, T4>()
            where T1 : TestDefinition
            where T2 : TestDefinition
            where T3 : TestDefinition
            where T4 : TestDefinition
        {
            List<ProjectConfigItem> projectConfigs = _projectConfigsFactory.CreateProjectConfigsByDBTyps();

            foreach (var projectConfig in projectConfigs)
            {
                TestArgs testArgs = new ProjectConfigTestArgs(projectConfig);

                var tests = DIConfig.GetTestDefinitions<T1, T2, T3, T4>();

                RunTests(tests, testArgs);
            }
        }

        public static void RunTestsForeachDBType<T1, T2, T3, T4, T5>()
            where T1 : TestDefinition
            where T2 : TestDefinition
            where T3 : TestDefinition
            where T4 : TestDefinition
            where T5 : TestDefinition
        {
            List<ProjectConfigItem> projectConfigs = _projectConfigsFactory.CreateProjectConfigsByDBTyps();

            foreach (var projectConfig in projectConfigs)
            {
                TestArgs testArgs = new ProjectConfigTestArgs(projectConfig);

                var tests = DIConfig.GetTestDefinitions<T1, T2, T3, T4, T5>();

                RunTests(tests, testArgs);
            }
        }

        public static void RunTestsForeachDBType<T1, T2, T3, T4, T5, T6>()
            where T1 : TestDefinition
            where T2 : TestDefinition
            where T3 : TestDefinition
            where T4 : TestDefinition
            where T5 : TestDefinition
            where T6 : TestDefinition
        {
            List<ProjectConfigItem> projectConfigs = _projectConfigsFactory.CreateProjectConfigsByDBTyps();

            foreach (var projectConfig in projectConfigs)
            {
                TestArgs testArgs = new ProjectConfigTestArgs(projectConfig);

                var tests = DIConfig.GetTestDefinitions<T1, T2, T3, T4, T5, T6>();

                RunTests(tests, testArgs);
            }
        }



        private static void RunTests(IEnumerable<TestDefinition> tests, TestArgs testArgs)
        {
            foreach (var test in tests)
            {
                lock (_testRunSync)
                {
                    ITestContext testContext = null;

                    try
                    {
                        Console.WriteLine($"{test.GetType().Name} >>> Start");
                        Debug.WriteLine($"{test.GetType().Name} >>> Start");

                        testContext = test.Arrange(testArgs);

                        test.Act(testContext);

                        test.Asserts(testContext);
                    }
                    catch
                    {
                        if (testContext != null
                            && testContext.ProcessResults != null
                            && testContext.ProcessResults.Trace != null)
                        {
                            Console.WriteLine(testContext.ProcessResults.Trace.GetAllStatesLogAsString());
                            Debug.WriteLine(testContext.ProcessResults.Trace.GetAllStatesLogAsString());
                        }

                        throw;
                    }
                    finally
                    {
                        test.Release(testContext);

                        Console.WriteLine($"{test.GetType().Name} >>> Complete");
                        Debug.WriteLine($"{test.GetType().Name} >>> Complete");
                    }
                }
            }
        }
    }
}
