using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsUtils
{

    public class ProjectConfigsStorageHelper
    {
        private readonly ProjectConfigsStorage _projectConfigsStorage;

        public ProjectConfigsStorageHelper(ProjectConfigsStorage projectConfigsStorage)
        {
            _projectConfigsStorage = projectConfigsStorage;
        }

        public void PrepareTestProject(ProjectConfigItem projectConfig)
        {
            ClearAllProjects();

            _projectConfigsStorage.SaveNewProjectConfig(projectConfig);
        }

        public void ClearAllProjects()
        {
            IEnumerable<ProjectConfigItem> projectConfigs = _projectConfigsStorage.GetAllProjectConfigs().Values;

            foreach (var projectConfig in projectConfigs)
            {
                _projectConfigsStorage.RemoveProjectConfig(projectConfig.Id);
            }
        }
    }
}
