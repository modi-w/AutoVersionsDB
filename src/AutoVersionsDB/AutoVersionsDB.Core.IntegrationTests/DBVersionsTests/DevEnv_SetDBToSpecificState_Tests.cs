using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SetDBToSpecificState;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;


using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{
    [TestFixture]
    public class DevEnv_SetDBToSpecificState_Tests
    {
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }



        [Test]
        public void DevEnv_SetDBToSpecificState_DBInMiddleState_TargetFinalState()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_SetDBToSpecificState_DBInMiddleState_TargetFinalState_API>();
        }


        [Test]
        public void DevEnv_SetDBToSpecificState_TargetMiddleState()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_SetDBToSpecificState_TargetMiddleState_API>();
        }


        [Test]
        public void DevEnv_SetDBToSpecificState_Warning()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_SetDBToSpecificState_Warning_API>();
        }

        [Test]
        public void DevEnv_SetDBToSpecificState_IgnoreWarning()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_SetDBToSpecificState_IgnoreWarning_API>();
        }

        
    }
}
