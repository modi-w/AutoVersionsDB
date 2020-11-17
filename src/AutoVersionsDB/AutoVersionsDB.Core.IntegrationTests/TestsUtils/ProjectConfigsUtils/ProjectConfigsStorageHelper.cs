using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ProjectConfigsUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils.ProjectConfigsUtils
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

        public void AppendProject(ProjectConfigItem projectConfig)
        {
            _projectConfigsStorage.SaveNewProjectConfig(projectConfig);
        }

        public void PrepareTestProject(ProjectConfigItem projectConfig1, ProjectConfigItem projectConfig2)
        {
            ClearAllProjects();

            _projectConfigsStorage.SaveNewProjectConfig(projectConfig1);
            _projectConfigsStorage.SaveNewProjectConfig(projectConfig2);
        }

        public bool IsIdExsit(string id)
        {
            return _projectConfigsStorage.IsIdExsit(id);
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
