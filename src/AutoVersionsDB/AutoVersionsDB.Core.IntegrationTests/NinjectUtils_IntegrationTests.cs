using AutoVersionsDB.Core;
using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.IO;
using System.Reflection;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests
{
    public static class NinjectUtils_IntegrationTests
    {
        public static IKernel NinjectKernelContainer { get; private set; }


        public static void CreateKernel()
        {

            NinjectKernelContainer = new StandardKernel();
            NinjectKernelContainer.Load(Assembly.GetExecutingAssembly());

            RegisterServices(NinjectKernelContainer);

            NinjectUtils.SetKernelInstance(NinjectKernelContainer);
        }

        private static void RegisterServices(IKernel kernel)
        {
            MockObjectsProvider.Init(kernel);
        }




        public static IEnumerable<TestDefinition> GetTestDefinitions<T1>()
                where T1 : TestDefinition
        {
            List<TestDefinition> testDefinitions = new List<TestDefinition>();

            testDefinitions.Add(NinjectKernelContainer.Get<T1>());

            return testDefinitions;
        }
        public static IEnumerable<TestDefinition> GetTestDefinitions<T1, T2>()
            where T1 : TestDefinition
            where T2 : TestDefinition
        {
            List<TestDefinition> testDefinitions = new List<TestDefinition>();

            testDefinitions.Add(NinjectKernelContainer.Get<T1>());
            testDefinitions.Add(NinjectKernelContainer.Get<T2>());

            return testDefinitions;
        }

        public static IEnumerable<TestDefinition> GetTestDefinitions<T1, T2, T3>()
            where T1 : TestDefinition
            where T2 : TestDefinition
            where T3 : TestDefinition
        {
            List<TestDefinition> testDefinitions = new List<TestDefinition>();

            testDefinitions.Add(NinjectKernelContainer.Get<T1>());
            testDefinitions.Add(NinjectKernelContainer.Get<T2>());
            testDefinitions.Add(NinjectKernelContainer.Get<T3>());

            return testDefinitions;
        }

        public static IEnumerable<TestDefinition> GetTestDefinitions<T1, T2, T3, T4>()
            where T1 : TestDefinition
            where T2 : TestDefinition
            where T3 : TestDefinition
            where T4 : TestDefinition
        {
            List<TestDefinition> testDefinitions = new List<TestDefinition>();

            testDefinitions.Add(NinjectKernelContainer.Get<T1>());
            testDefinitions.Add(NinjectKernelContainer.Get<T2>());
            testDefinitions.Add(NinjectKernelContainer.Get<T3>());
            testDefinitions.Add(NinjectKernelContainer.Get<T4>());

            return testDefinitions;
        }


        public static IEnumerable<TestDefinition> GetTestDefinitions<T1, T2, T3, T4, T5>()
            where T1 : TestDefinition
            where T2 : TestDefinition
            where T3 : TestDefinition
            where T4 : TestDefinition
            where T5 : TestDefinition
        {
            List<TestDefinition> testDefinitions = new List<TestDefinition>();

            testDefinitions.Add(NinjectKernelContainer.Get<T1>());
            testDefinitions.Add(NinjectKernelContainer.Get<T2>());
            testDefinitions.Add(NinjectKernelContainer.Get<T3>());
            testDefinitions.Add(NinjectKernelContainer.Get<T4>());
            testDefinitions.Add(NinjectKernelContainer.Get<T5>());

            return testDefinitions;
        }



    }
}
