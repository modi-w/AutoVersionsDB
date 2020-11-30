using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.DB.Contract;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes
{
    public class GetDBTypes_API : TestDefinition
    {

        public GetDBTypes_API()
        {
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            return new ProcessTestContext(testArgs);
        }


        public override void Act(ITestContext testContext)
        {
            testContext.Result = AutoVersionsDBAPI.DBTypes;
        }


        public override void Asserts(ITestContext testContext)
        {
            List<DBType> dbTypesResults = testContext.Result as List<DBType>;

            Assert.That(dbTypesResults.Count == 1, $"{GetType().Name} -> The number of DBTypes should be 1, but was: {dbTypesResults.Count}");

            DBType firstDBType = dbTypesResults.First();
            Assert.That(firstDBType.Code == IntegrationTestsConsts.SqlServerDBType, $"{GetType().Name} -> The first DBType Code should be: '{IntegrationTestsConsts.SqlServerDBType}', but was: '{firstDBType.Code}'");
            Assert.That(firstDBType.Name == "Sql Server", $"{GetType().Name} -> The first DBType Name should be: 'Sql Server', but was: '{firstDBType.Name}'");
        }

        public override void Release(ITestContext testContext)
        {
        }
    }
}
