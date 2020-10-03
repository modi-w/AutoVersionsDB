using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.Process;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using Moq;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{
    [TestFixture]
    public class DeliveryEnv_NotAollowMethods
    {
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }





        [Test]
        public void DeliveryEnv_NotAollowMethods_Deploy()
        {
            TestsRunner.RunTest<DeliveryEnv_NotAllowMethods_Deploy_API, DeliveryEnv_NotAllowMethods_Deploy_CLI>(false, DBBackupFileType.MiddleState, ScriptFilesStateType.ValidScripts);
        }


        [Test]
        public void DeliveryEnv_NotAollowMethods_Recreate()
        {
            TestsRunner.RunTest<DeliveryEnv_NotAllowMethods_Recreate_API, DeliveryEnv_NotAllowMethods_Recreate_CLI>(false, DBBackupFileType.FinalState_DeliveryEnv, ScriptFilesStateType.ValidScripts);
        }


        [Test]
        public void DeliveryEnv_NotAollowMethods_SetDBToSpecificState()
        {
            TestsRunner.RunTest<DeliveryEnv_NotAllowMethods_SetDBToSpecificState_API>(false, DBBackupFileType.FinalState_DeliveryEnv, ScriptFilesStateType.ValidScripts);
        }

    }
}
