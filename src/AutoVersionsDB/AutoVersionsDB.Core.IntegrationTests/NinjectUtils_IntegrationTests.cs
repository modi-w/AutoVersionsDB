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




        public static IEnumerable<ITestDefinition> GetTestDefinitions<T1, T2>()
            where T1 : ITestDefinition
            where T2 : ITestDefinition
        {
            List<ITestDefinition> testDefinitions = new List<ITestDefinition>();

            testDefinitions.Add(NinjectKernelContainer.Get<T1>());
            testDefinitions.Add(NinjectKernelContainer.Get<T2>());

            return testDefinitions;
        }




    }
}
