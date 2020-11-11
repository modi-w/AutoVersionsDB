using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.ChangeProjectId;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.UI.ChooseProject;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.ChangeProjectId
{
    public class ChangeProjectId_UI : TestDefinition
    {
        private readonly ChangeProjectId_API _changeProjectId_API;
        private readonly ChooseProjectViewModel _chooseProjectViewModel;
        private readonly EditProjectViewModelAsserts _editProjectViewModelAsserts;

        public ChangeProjectId_UI(ChangeProjectId_API changeProjectId_API,
                                    ChooseProjectViewModel chooseProjectViewModel,
                                    EditProjectViewModelAsserts editProjectViewModelAsserts)
        {
            _changeProjectId_API = changeProjectId_API;
            _chooseProjectViewModel = chooseProjectViewModel;
            _editProjectViewModelAsserts = editProjectViewModelAsserts;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _changeProjectId_API.Arrange(testArgs);
            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(TestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"config change-id -id={ChangeProjectId_API.OldProjectId} -nid={ChangeProjectId_API.NewProjectId}");
        }


        public override void Asserts(TestContext testContext)
        {
            _changeProjectId_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 2);
            assertTextByLines.AssertLineMessage("> Run 'change-id' for 'TestProject_Old'", true);
            assertTextByLines.AssertLineMessage("The process complete successfully", true);

        }


        public override void Release(TestContext testContext)
        {
            _changeProjectId_API.Release(testContext);

        }

    }
}
