using AutoVersionsDB.Core.ConfigProjects.Processes;
using AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects
{
    public class ProjectConfigsAPI
    {
        private readonly ProjectConfigsStorage _projectConfigsStorage;

        private readonly NotificationProcessRunner<SaveNewProjectConfigProcessDefinition, ProjectConfigProcessContext> _saveNewProjectConfigRunner;
        private readonly NotificationProcessRunner<UpdateProjectConfigProcessDefinition, ProjectConfigProcessContext> _updateProjectConfigRunner;
        private readonly NotificationProcessRunner<ChangeProjectCodeProcessDefinition, ProjectConfigProcessContext> _changeProjectCodeRunner;
        private readonly NotificationProcessRunner<RemoveProjectConfigProcessDefinition, ProjectConfigProcessContext> _removeProjectConfigProcessDefinition;


        public ProjectConfigsAPI(ProjectConfigsStorage projectConfigsStorage,
                                NotificationProcessRunner<SaveNewProjectConfigProcessDefinition, ProjectConfigProcessContext> saveNewProjectConfigRunner,
                                NotificationProcessRunner<UpdateProjectConfigProcessDefinition, ProjectConfigProcessContext> updateProjectConfigRunner,
                                NotificationProcessRunner<ChangeProjectCodeProcessDefinition, ProjectConfigProcessContext> changeProjectCodeRunner,
                                NotificationProcessRunner<RemoveProjectConfigProcessDefinition, ProjectConfigProcessContext> removeProjectConfigProcessDefinition)
        {
            _projectConfigsStorage = projectConfigsStorage;
            _saveNewProjectConfigRunner = saveNewProjectConfigRunner;
            _updateProjectConfigRunner = updateProjectConfigRunner;
            _changeProjectCodeRunner = changeProjectCodeRunner;
            _removeProjectConfigProcessDefinition = removeProjectConfigProcessDefinition;
        }



        public List<ProjectConfigItem> GetProjectsList()
        {
            return _projectConfigsStorage.GetAllProjectConfigs().Values.ToList();
        }

        public ProjectConfigItem GetProjectConfigByProjectCode(string projectCode)
        {
            return _projectConfigsStorage.GetProjectConfigByProjectCode(projectCode);
        }


        public ProcessTrace SaveNewProjectConfig(ProjectConfigItem projectConfig, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _saveNewProjectConfigRunner.Run(new ProjectConfigProcessParams(projectConfig), onNotificationStateChanged);
        }

        public ProcessTrace UpdateProjectConfig(ProjectConfigItem projectConfig, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _updateProjectConfigRunner.Run(new ProjectConfigProcessParams(projectConfig), onNotificationStateChanged);
        }

        public ProcessTrace ChangeProjectCode(string prevProjectCode, string newProjectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _changeProjectCodeRunner.Run(new ChangeProjectCodeProcessParams(prevProjectCode, newProjectCode), onNotificationStateChanged);
        }

        public ProcessTrace RemoveProjectConfig(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _removeProjectConfigProcessDefinition.Run(new ProjectConfigProcessParams(projectCode), onNotificationStateChanged);
        }

    }
}
