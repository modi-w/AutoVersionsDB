using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.ChooseProjectTests.TestDefinitions.SerchProjectText;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Deploy;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Recreate;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ProjectConfigsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.UI.ChooseProject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ChooseProjectTests.TestDefinitions.SerchProjectText
{
    public class SerchProjectText_UI : TestDefinition
    {
        private readonly ChooseProjectViewModel _chooseProjectViewModel;
        private readonly ProjectConfigsStorageHelper _projectConfigsStorageHelper;

        public SerchProjectText_UI(ChooseProjectViewModel chooseProjectViewModel,
                                    ProjectConfigsStorageHelper projectConfigsStorageHelper)
        {
            _chooseProjectViewModel = chooseProjectViewModel;
            _projectConfigsStorageHelper = projectConfigsStorageHelper;
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            _projectConfigsStorageHelper.ClearAllProjects();

            ProjectConfigItem projectConfigItem1 = new ProjectConfigItem()
            {
                Id = "aa1",
                Description = "Desc1"
            };
            _projectConfigsStorageHelper.AppendProject(projectConfigItem1);

            ProjectConfigItem projectConfigItem2 = new ProjectConfigItem()
            {
                Id = "Id2",
                Description = "aa2"
            };
            _projectConfigsStorageHelper.AppendProject(projectConfigItem2);

            ProjectConfigItem projectConfigItem3 = new ProjectConfigItem()
            {
                Id = "Id3",
                Description = "Desc3"
            };
            _projectConfigsStorageHelper.AppendProject(projectConfigItem3);


            ITestContext testContext = new ProcessTestContext(null);


            return testContext;
        }

        public override void Act(ITestContext testContext)
        {
            _chooseProjectViewModel.ChooseProjectViewModelData.SerchProjectText = "aa";
        }


        public override void Asserts(ITestContext testContext)
        {
            Assert.That(_chooseProjectViewModel.ChooseProjectViewModelData.FilteredProjectList.Count == 2, $"{this.GetType().Name} -> FilteredProjectList Count should be 2, but was '{_chooseProjectViewModel.ChooseProjectViewModelData.FilteredProjectList.Count}'");
            Assert.That(_chooseProjectViewModel.ChooseProjectViewModelData.FilteredProjectList.Any(e => e.Id == "aa1"), $"{this.GetType().Name} -> FilteredProjectList should include project with Id='aa1'");
            Assert.That(_chooseProjectViewModel.ChooseProjectViewModelData.FilteredProjectList.Any(e => e.Description == "aa2"), $"{this.GetType().Name} -> FilteredProjectList should include project with Description='aa2'");
        }

        public override void Release(ITestContext testContext)
        {
            _projectConfigsStorageHelper.ClearAllProjects();
        }



    }
}
