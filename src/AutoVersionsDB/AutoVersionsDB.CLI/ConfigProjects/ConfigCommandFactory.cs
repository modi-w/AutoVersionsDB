using AutoVersionsDB.CLI.ConfigProjects.CLIOptions;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;

namespace AutoVersionsDB.CLI.ConfigProjects
{
    public class ConfigCommandFactory : CLICommandFactory
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;

        private readonly EnvironmentCommandFactory _environmentCommandFactory;
        private readonly ChangeIdCommandFactory _changeIdCommandFactory;

        private readonly IdCLIOption _idOption;
        private readonly DescriptionCLIOption _descriptionOption;
        private readonly DBTypeCLIOption _dbTypeOption;
        private readonly ServerCLIOption _serverInstanceOption;
        private readonly DBNameCLIOption _dataBaseNameOption;
        private readonly UsernameCLIOption _dbUsernameOption;
        private readonly PasswordCLIOption _dbPasswordOption;

        private readonly BackupFolderPathCLIOption _backupFolderPathOption;
        private readonly ScriptsBaseFolderPathCLIOption _scriptsBaseFolderPathOption;
        private readonly DeployArtifactFolderPathCLIOption _deployArtifactFolderPathOption;
        private readonly DeliveryArtifactFolderPathCLIOption _deliveryArtifactFolderPathOption;



        public ConfigCommandFactory(ProjectConfigsAPI projectConfigsAPI,
                                    IConsoleProcessMessages consoleProcessMessages,
                                    EnvironmentCommandFactory environmentCommandFactory,
                                    ChangeIdCommandFactory changeIdCommandFactory,
                                    IdCLIOption idOption,
                                    DescriptionCLIOption descriptionOption,
                                    DBTypeCLIOption dbTypeOption,
                                    ServerCLIOption serverInstanceOption,
                                    DBNameCLIOption dataBaseNameOption,
                                    UsernameCLIOption dbUsernameOption,
                                    PasswordCLIOption dbPasswordOption,
                                    BackupFolderPathCLIOption backupFolderPathOption,
                                    ScriptsBaseFolderPathCLIOption scriptsBaseFolderPathOption,
                                    DeployArtifactFolderPathCLIOption deployArtifactFolderPathOption,
                                    DeliveryArtifactFolderPathCLIOption deliveryArtifactFolderPathOption)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _consoleProcessMessages = consoleProcessMessages;
            _environmentCommandFactory = environmentCommandFactory;
            _changeIdCommandFactory = changeIdCommandFactory;
            _idOption = idOption;
            _descriptionOption = descriptionOption;
            _dbTypeOption = dbTypeOption;
            _serverInstanceOption = serverInstanceOption;
            _dataBaseNameOption = dataBaseNameOption;
            _dbUsernameOption = dbUsernameOption;
            _dbPasswordOption = dbPasswordOption;
            _backupFolderPathOption = backupFolderPathOption;
            _scriptsBaseFolderPathOption = scriptsBaseFolderPathOption;
            _deployArtifactFolderPathOption = deployArtifactFolderPathOption;
            _deliveryArtifactFolderPathOption = deliveryArtifactFolderPathOption;
        }

        public override Command Create()
        {
            Command command = new Command("config")
            {
                _idOption,
                _descriptionOption,
                _dbTypeOption,
                _serverInstanceOption,
                _dataBaseNameOption,
                _dbUsernameOption,
                _dbPasswordOption,
                _backupFolderPathOption,
                _scriptsBaseFolderPathOption,
                _deployArtifactFolderPathOption,
                _deliveryArtifactFolderPathOption,
            };

            command.Description = "Change project configuration.";

            command.Handler = CommandHandler
                .Create((ProjectConfigItem projectConfig) =>
                {
                    _consoleProcessMessages.StartProcessMessage("config", projectConfig.Id);

                    ProjectConfigItem existProjectConfig = _projectConfigsAPI.GetProjectConfigById(projectConfig.Id);

                    if (existProjectConfig == null)
                    {
                        _consoleProcessMessages.SetErrorMessage($"Id: '{projectConfig.Id}' is not exist. You can use the 'init' command to define new project.");
                    }
                    else
                    {
                        overrideProjectConfigProperties(existProjectConfig, projectConfig);

                        _consoleProcessMessages.StartSpiiner();
                        ProcessResults processResults = _projectConfigsAPI.UpdateProjectConfig(existProjectConfig, _consoleProcessMessages.OnNotificationStateChanged);
                        _consoleProcessMessages.StopSpinner();

                        _consoleProcessMessages.ProcessComplete(processResults);
                    }
                });

            Command changeIdCommand = _changeIdCommandFactory.Create();
            command.Add(changeIdCommand);

            Command environmentCommand = _environmentCommandFactory.Create();
            command.Add(environmentCommand);

            return command;
        }

        private static void overrideProjectConfigProperties(ProjectConfigItem existProjectConfig, ProjectConfigItem projectConfig)
        {
            if (projectConfig.Description != null)
            {
                existProjectConfig.Description = projectConfig.Description;
            }
            if (projectConfig.DBType != null)
            {
                existProjectConfig.DBType = projectConfig.DBType;
            }
            if (projectConfig.Server != null)
            {
                existProjectConfig.Server = projectConfig.Server;
            }
            if (projectConfig.DBName != null)
            {
                existProjectConfig.DBName = projectConfig.DBName;
            }
            if (projectConfig.Username != null)
            {
                existProjectConfig.Username = projectConfig.Username;
            }
            if (projectConfig.Password != null)
            {
                existProjectConfig.Password = projectConfig.Password;
            }
            if (projectConfig.BackupFolderPath != null)
            {
                existProjectConfig.BackupFolderPath = projectConfig.BackupFolderPath;
            }
            if (projectConfig.DevScriptsBaseFolderPath != null)
            {
                existProjectConfig.DevScriptsBaseFolderPath = projectConfig.DevScriptsBaseFolderPath;
            }
            if (projectConfig.DeployArtifactFolderPath != null)
            {
                existProjectConfig.DeployArtifactFolderPath = projectConfig.DeployArtifactFolderPath;
            }
            if (projectConfig.DeliveryArtifactFolderPath != null)
            {
                existProjectConfig.DeliveryArtifactFolderPath = projectConfig.DeliveryArtifactFolderPath;
            }
        }
    }
}
