using AutoVersionsDB;
using AutoVersionsDB.CLI;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.Processes.Validators;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations
{
    public class DeliveryEnv_Validate_HistoryExecutedFilesMissing_CLI : TestDefinition<CLITestContext>
    {

        private readonly DeliveryEnv_Validate_HistoryExecutedFilesMissing_API _deliveryEnv_Validate_HistoryExecutedFilesMissing_API;

        public DeliveryEnv_Validate_HistoryExecutedFilesMissing_CLI(DeliveryEnv_Validate_HistoryExecutedFilesMissing_API deliveryEnv_Validate_HistoryExecutedFilesMissing_API)
        {
            _deliveryEnv_Validate_HistoryExecutedFilesMissing_API = deliveryEnv_Validate_HistoryExecutedFilesMissing_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_deliveryEnv_Validate_HistoryExecutedFilesMissing_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            CLIRunner.CLIRun($"validate -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _deliveryEnv_Validate_HistoryExecutedFilesMissing_API.Asserts(testContext);

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 1);
            assertConsoleOutTextByLines.AssertLineMessage(CLITextResources.StartProcessMessageWithArgs.Replace("[processName]", "validate").Replace("[args]", "IntegrationTestProject"), true);


            AssertTextByLines assertErrorsTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError, 5);
            assertErrorsTextByLines.AssertLineMessage(CLITextResources.ProcessCompleteWithErrors, true);
            assertErrorsTextByLines.AssertLineMessage("--------------------------------", true);
            assertErrorsTextByLines.AssertLineMessage($"{HistoryExecutedFilesChangedValidator.Name}. Error: {CoreTextResources.HistoryExecutedFilesMissingErrorMessage.Replace("[FilesList]", "incScript_0003_CreateLookupTable2.sql").Replace("[FileTypeCode]", IncrementalScriptFileType.Code)}", false);
            assertErrorsTextByLines.AssertLineMessage("", true);
            assertErrorsTextByLines.AssertLineMessage(CoreTextResources.HistoryExecutedFilesChangedInstructionsMessage, true);

        }



        public override void Release(CLITestContext testContext)
        {
            _deliveryEnv_Validate_HistoryExecutedFilesMissing_API.Release(testContext);
        }


    }
}
