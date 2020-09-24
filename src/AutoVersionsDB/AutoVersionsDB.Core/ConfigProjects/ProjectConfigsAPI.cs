﻿using AutoVersionsDB.Core.ConfigProjects.Processes;
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
        private readonly NotificationProcessRunner<ChangeIdProcessDefinition, ProjectConfigProcessContext> _changeIdRunner;
        private readonly NotificationProcessRunner<RemoveProjectConfigProcessDefinition, ProjectConfigProcessContext> _removeProjectConfigProcessDefinition;


        public ProjectConfigsAPI(ProjectConfigsStorage projectConfigsStorage,
                                NotificationProcessRunner<SaveNewProjectConfigProcessDefinition, ProjectConfigProcessContext> saveNewProjectConfigRunner,
                                NotificationProcessRunner<UpdateProjectConfigProcessDefinition, ProjectConfigProcessContext> updateProjectConfigRunner,
                                NotificationProcessRunner<ChangeIdProcessDefinition, ProjectConfigProcessContext> changeIdRunner,
                                NotificationProcessRunner<RemoveProjectConfigProcessDefinition, ProjectConfigProcessContext> removeProjectConfigProcessDefinition)
        {
            _projectConfigsStorage = projectConfigsStorage;
            _saveNewProjectConfigRunner = saveNewProjectConfigRunner;
            _updateProjectConfigRunner = updateProjectConfigRunner;
            _changeIdRunner = changeIdRunner;
            _removeProjectConfigProcessDefinition = removeProjectConfigProcessDefinition;
        }



        public List<ProjectConfigItem> GetProjectsList()
        {
            return _projectConfigsStorage.GetAllProjectConfigs().Values.ToList();
        }

        public ProjectConfigItem GetProjectConfigById(string id)
        {
            return _projectConfigsStorage.GetProjectConfigById(id);
        }


        public ProcessResults SaveNewProjectConfig(ProjectConfigItem projectConfig, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _saveNewProjectConfigRunner.Run(new ProjectConfigProcessParams(projectConfig), onNotificationStateChanged);
        }

        public ProcessResults UpdateProjectConfig(ProjectConfigItem projectConfig, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _updateProjectConfigRunner.Run(new ProjectConfigProcessParams(projectConfig), onNotificationStateChanged);
        }

        public ProcessResults ChangeProjectId(string prevId, string newId, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _changeIdRunner.Run(new ChangeIdProcessParams(prevId, newId), onNotificationStateChanged);
        }

        public ProcessResults RemoveProjectConfig(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _removeProjectConfigProcessDefinition.Run(new ProjectConfigProcessParams(id), onNotificationStateChanged);
        }

    }
}
