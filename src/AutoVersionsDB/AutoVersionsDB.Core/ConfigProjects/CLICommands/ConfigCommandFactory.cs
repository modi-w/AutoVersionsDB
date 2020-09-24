using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions;
using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands
{
    public class ConfigCommandFactory : CLICommandFactory
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly IConsoleHandler _consoleHandler;

        private readonly EnvironmentCommandFactory _environmentCommandFactory;
        private readonly ChangeIdCommandFactory _changeIdCommandFactory;

        private readonly IdCLIOption _idOption;
        private readonly DescriptionCLIOption _descriptionOption;
        private readonly DBTypeCLIOption _dbTypeOption;
        private readonly ConnectionStringCLIOption _connectionStringOption;
        private readonly ConnectionStringToMasterDBCLIOption _connectionStringToMasterDBOption;
        private readonly BackupFolderPathCLIOption _backupFolderPathOption;
        private readonly ScriptsBaseFolderPathCLIOption _scriptsBaseFolderPathOption;
        private readonly DeployArtifactFolderPathCLIOption _deployArtifactFolderPathOption;
        private readonly DeliveryArtifactFolderPathCLIOption _deliveryArtifactFolderPathOption;



        public ConfigCommandFactory(ProjectConfigsAPI projectConfigsAPI,
                                    IConsoleHandler consoleHandler,
                                    EnvironmentCommandFactory environmentCommandFactory,
                                    ChangeIdCommandFactory changeIdCommandFactory,
                                    IdCLIOption idOption,
                                    DescriptionCLIOption descriptionOption,
                                    DBTypeCLIOption dbTypeOption,
                                    ConnectionStringCLIOption connectionStringOption,
                                    ConnectionStringToMasterDBCLIOption connectionStringToMasterDBOption,
                                    BackupFolderPathCLIOption backupFolderPathOption,
                                    ScriptsBaseFolderPathCLIOption scriptsBaseFolderPathOption,
                                    DeployArtifactFolderPathCLIOption deployArtifactFolderPathOption,
                                    DeliveryArtifactFolderPathCLIOption deliveryArtifactFolderPathOption)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _consoleHandler = consoleHandler;
            _environmentCommandFactory = environmentCommandFactory;
            _changeIdCommandFactory = changeIdCommandFactory;
            _idOption = idOption;
            _descriptionOption = descriptionOption;
            _dbTypeOption = dbTypeOption;
            _connectionStringOption = connectionStringOption;
            _connectionStringToMasterDBOption = connectionStringToMasterDBOption;
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
                _connectionStringOption,
                _connectionStringToMasterDBOption,
                _backupFolderPathOption,
                _scriptsBaseFolderPathOption,
                _deployArtifactFolderPathOption,
                _deliveryArtifactFolderPathOption,
            };

            command.Description = "Change project configuration.";

            command.Handler = CommandHandler
                .Create((ProjectConfigItem projectConfig) =>
                {
                    _consoleHandler.StartProcessMessage("config", projectConfig.Id);

                    ProjectConfigItem existProjectConfig = _projectConfigsAPI.GetProjectConfigById(projectConfig.Id);

                if (existProjectConfig == null)
                {
                    _consoleHandler.SetErrorMessage($"Id: '{projectConfig.Id}' is not exist. You can use the 'init' command to define new project.");
                }
                else
                {
                    overrideProjectConfigProperties(existProjectConfig, projectConfig);

                    _consoleHandler.StartSpiiner();
                    ProcessResults processResults = _projectConfigsAPI.UpdateProjectConfig(existProjectConfig, _consoleHandler.OnNotificationStateChanged);
                    _consoleHandler.StopSpinner();

                    _consoleHandler.ProcessComplete(processResults.Trace);
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
            if (projectConfig.ConnectionString != null)
            {
                existProjectConfig.ConnectionString = projectConfig.ConnectionString;
            }
            if (projectConfig.ConnectionStringToMasterDB != null)
            {
                existProjectConfig.ConnectionStringToMasterDB = projectConfig.ConnectionStringToMasterDB;
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
