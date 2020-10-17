using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;

using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
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


        public override TestContext Arrange(TestArgs testArgs)
        {
            return new TestContext(testArgs);
        }


        public override void Act(TestContext testContext)
        {
            testContext.Result = AutoVersionsDBAPI.GetDBTypes();
        }


        public override void Asserts(TestContext testContext)
        {
            List<DBType> dbTypesResults = testContext.Result as List<DBType>;

            Assert.That(dbTypesResults.Count == 1, $"{GetType().Name} -> The number of DBTypes should be 1, but was: {dbTypesResults.Count}");

            DBType firstDBType = dbTypesResults.First();
            Assert.That(firstDBType.Code == "SqlServer", $"{GetType().Name} -> The first DBType Code should be: 'SqlServer', but was: '{firstDBType.Code}'");
            Assert.That(firstDBType.Name == "Sql Server", $"{GetType().Name} -> The first DBType Name should be: 'Sql Server', but was: '{firstDBType.Name}'");
        }

        public override void Release(TestContext testContext)
        {
        }
    }
}
