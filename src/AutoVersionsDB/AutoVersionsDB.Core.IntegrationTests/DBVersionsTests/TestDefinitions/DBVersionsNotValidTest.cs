//using AutoVersionsDB;
//using AutoVersionsDB.Core;
//using AutoVersionsDB.Core.Common.CLI;
//using AutoVersionsDB.Core.ConfigProjects;
//using AutoVersionsDB.Core.IntegrationTests;
//using AutoVersionsDB.Core.IntegrationTests.DB;
//using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
//using AutoVersionsDB.Core.IntegrationTests.Process;
//using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
//using AutoVersionsDB.NotificationableEngine;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
//{
//    public class DBVersionsTestHelper : TestDefinition<DBVersionsTestContext>
//    {
//        private readonly DBVersionsTest _dbVersionsTest;
//        private readonly ProcessAsserts _processAsserts;

//        public DBVersionsTestHelper(DBVersionsTest dbVersionsTest,
//                                        ProcessAsserts processAsserts)
//        {
//            _dbVersionsTest = dbVersionsTest;
//            _processAsserts = processAsserts;
//        }

//        public override TestContext Arrange(TestArgs testArgs)
//        {
//            return _dbVersionsTest.Arrange(projectConfig, dbBackupFileType, scriptFilesStateType);
//        }

//        public override void Act(DBVersionsTestContext testContext)
//        {
//        }


//        public override void Asserts(DBVersionsTestContext testContext)
//        {
//            _dbVersionsTest.Asserts(testContext);

//            _processAsserts.AssertProccessHasErrors(GetType().Name, testContext.ProcessResults.Trace);
//        }


//        public override void Release(DBVersionsTestContext testContext)
//        {
//            _dbVersionsTest.Release(testContext);
//        }



//    }
//}
