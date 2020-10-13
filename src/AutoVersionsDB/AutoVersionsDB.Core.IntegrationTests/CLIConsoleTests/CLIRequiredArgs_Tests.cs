using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.CLIConsoleTests
{
    [TestFixture]
    class CLIRequiredArgs_Tests
    {
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }


        [Test]
        public void Info_ID_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"info");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--id' is required.", true);
        }

        [Test]
        public void Init_ID_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"init");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--id' is required.", true);
        }

        [Test]
        public void Init_Dev_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"init -id={IntegrationTestsConsts.TestProjectId}");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--dev-environment' is required.", true);
        }

        [Test]
        public void Config_ID_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"config");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--id' is required.", true);
        }

        [Test]
        public void Remove_ID_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"remove");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--id' is required.", true);
        }

        [Test]
        public void Validate_ID_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"validate");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--id' is required.", true);
        }

        [Test]
        public void Files_ID_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"files");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--id' is required.", true);
        }

        [Test]
        public void Files_Incremental_ID_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"files incremental");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--id' is required.", true);
        }

        [Test]
        public void Files_Repeatablel_ID_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"files repeatable");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--id' is required.", true);
        }

        [Test]
        public void Files_DDD_ID_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"files ddd");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--id' is required.", true);
        }


        [Test]
        public void Sync_ID_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"sync");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--id' is required.", true);
        }

        [Test]
        public void Recreate_ID_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"recreate");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--id' is required.", true);
        }

        [Test]
        public void Virtual_ID_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"virtual");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--id' is required.", true);
        }


        [Test]
        public void Deploy_ID_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"deploy");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--id' is required.", true);
        }

        [Test]
        public void New_Incremental_ID_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"new incremental");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--id' is required.", true);
        }

        [Test]
        public void New_Incremental_ScriptName_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"new incremental -id={IntegrationTestsConsts.TestProjectId}");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--script-name' is required.", true);
        }

        [Test]
        public void New_Repeatable_ID_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"new repeatable");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--id' is required.", true);
        }

        [Test]
        public void New_Repeatable_ScriptName_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"new repeatable -id={IntegrationTestsConsts.TestProjectId}");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--script-name' is required.", true);
        }

        [Test]
        public void New_DDD_ID_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"new ddd");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--id' is required.", true);
        }


        [Test]
        public void New_DDD_ScriptName_IsRequired()
        {
            //Arrange
            TestContext testContext = new TestContext(new TestArgs());
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            AutoVersionsDBAPI.CLIRun($"new ddd -id={IntegrationTestsConsts.TestProjectId}");

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertConsoleOutTextByLines.AssertLineMessage(0, "Option '--script-name' is required.", true);
        }


    }
}
