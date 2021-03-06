﻿using AutoVersionsDB.Core.ConfigProjects.Processes;
using AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions;
using AutoVersionsDB.DB;
using AutoVersionsDB.DB.Contract;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoVersionsDB.Core.ConfigProjects
{
    public class ProjectConfigsAPI
    {
        private readonly ProjectConfigsStorage _projectConfigsStorage;
        private readonly DBCommandsFactory _dbCommandsFactory;

        private readonly NotificationProcessRunner<ProjectConfigValidationProcessDefinition, ProjectConfigProcessContext> _projectConfigValidationRunner;
        private readonly NotificationProcessRunner<SaveNewProjectConfigProcessDefinition, ProjectConfigProcessContext> _saveNewProjectConfigRunner;
        private readonly NotificationProcessRunner<UpdateProjectConfigProcessDefinition, ProjectConfigProcessContext> _updateProjectConfigRunner;
        private readonly NotificationProcessRunner<ChangeIdProcessDefinition, ProjectConfigProcessContext> _changeIdRunner;
        private readonly NotificationProcessRunner<RemoveProjectConfigProcessDefinition, ProjectConfigProcessContext> _removeProjectConfigProcessDefinition;


        public ProjectConfigsAPI(ProjectConfigsStorage projectConfigsStorage,
                                DBCommandsFactory dbCommandsFactory,
                                NotificationProcessRunner<ProjectConfigValidationProcessDefinition, ProjectConfigProcessContext> projectConfigValidationRunner,
                                NotificationProcessRunner<SaveNewProjectConfigProcessDefinition, ProjectConfigProcessContext> saveNewProjectConfigRunner,
                                NotificationProcessRunner<UpdateProjectConfigProcessDefinition, ProjectConfigProcessContext> updateProjectConfigRunner,
                                NotificationProcessRunner<ChangeIdProcessDefinition, ProjectConfigProcessContext> changeIdRunner,
                                NotificationProcessRunner<RemoveProjectConfigProcessDefinition, ProjectConfigProcessContext> removeProjectConfigProcessDefinition)
        {
            _projectConfigsStorage = projectConfigsStorage;
            _dbCommandsFactory = dbCommandsFactory;

            _projectConfigValidationRunner = projectConfigValidationRunner;
            _saveNewProjectConfigRunner = saveNewProjectConfigRunner;
            _updateProjectConfigRunner = updateProjectConfigRunner;
            _changeIdRunner = changeIdRunner;
            _removeProjectConfigProcessDefinition = removeProjectConfigProcessDefinition;
        }


        public IList<DBType> DBTypes => _dbCommandsFactory.DBTypes;

        public List<ProjectConfigItem> GetProjectsList()
        {
            return _projectConfigsStorage.GetAllProjectConfigs().Values.ToList();
        }

        public ProjectConfigItem GetProjectConfigById(string id)
        {
            return _projectConfigsStorage.GetProjectConfigById(id);
        }

        public ProcessResults ValidateProjectConfig(ProjectConfigItem projectConfig, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _projectConfigValidationRunner.Run(new ProjectConfigProcessArgs(projectConfig), onNotificationStateChanged);
        }



        public ProcessResults SaveNewProjectConfig(ProjectConfigItem projectConfig, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _saveNewProjectConfigRunner.Run(new ProjectConfigProcessArgs(projectConfig), onNotificationStateChanged);
        }

        public ProcessResults UpdateProjectConfig(ProjectConfigItem projectConfig, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _updateProjectConfigRunner.Run(new ProjectConfigProcessArgs(projectConfig), onNotificationStateChanged);
        }

        public ProcessResults ChangeProjectId(string prevId, string newId, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _changeIdRunner.Run(new ChangeIdProcessArgs(prevId, newId), onNotificationStateChanged);
        }

        public ProcessResults RemoveProjectConfig(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _removeProjectConfigProcessDefinition.Run(new ProjectConfigProcessArgs(id), onNotificationStateChanged);
        }

    }
}
