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
    public class InitCommandFactory : CLICommandFactory
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly IConsoleHandler _consoleHandler;

        private readonly CodeCLIOption _codeOption;
        private readonly DescriptionCLIOption _descriptionOption;
        private readonly DBTypeCLIOption _dbTypeOption;
        private readonly ConnectionStringCLIOption _connectionStringOption;
        private readonly ConnectionStringForMasterDBCLIOption _connectionStringForMasterDBOption;
        private readonly BackupFolderPathCLIOption _backupFolderPathOption;
        private readonly DevEnironmentCLIOption _devEnironmentOption;
        private readonly ScriptsBaseFolderPathCLIOption _scriptsBaseFolderPathOption;
        private readonly DeployArtifactFolderPathCLIOption _deployArtifactFolderPathOption;
        private readonly DeliveryArtifactFolderPathCLIOption _deliveryArtifactFolderPathOption;



        public InitCommandFactory(ProjectConfigsAPI projectConfigsAPI,
                                    IConsoleHandler consoleHandler,
                                    CodeCLIOption codeOption,
                                    DescriptionCLIOption descriptionOption,
                                    DBTypeCLIOption dbTypeOption,
                                    ConnectionStringCLIOption connectionStringOption,
                                    ConnectionStringForMasterDBCLIOption connectionStringForMasterDBOption,
                                    BackupFolderPathCLIOption backupFolderPathOption,
                                    DevEnironmentCLIOption devEnironmentOption,
                                    ScriptsBaseFolderPathCLIOption scriptsBaseFolderPathOption,
                                    DeployArtifactFolderPathCLIOption deployArtifactFolderPathOption,
                                    DeliveryArtifactFolderPathCLIOption deliveryArtifactFolderPathOption)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _consoleHandler = consoleHandler;
            _codeOption = codeOption;
            _descriptionOption = descriptionOption;
            _dbTypeOption = dbTypeOption;
            _connectionStringOption = connectionStringOption;
            _connectionStringForMasterDBOption = connectionStringForMasterDBOption;
            _backupFolderPathOption = backupFolderPathOption;
            _devEnironmentOption = devEnironmentOption;
            _scriptsBaseFolderPathOption = scriptsBaseFolderPathOption;
            _deployArtifactFolderPathOption = deployArtifactFolderPathOption;
            _deliveryArtifactFolderPathOption = deliveryArtifactFolderPathOption;
        }

        public override Command Create()
        {
            Command command = new Command("init")
            {
                _codeOption,
                _descriptionOption,
                _dbTypeOption,
                _connectionStringOption,
                _connectionStringForMasterDBOption,
                _backupFolderPathOption,
                _devEnironmentOption,
                _scriptsBaseFolderPathOption,
                _deployArtifactFolderPathOption,
                _deliveryArtifactFolderPathOption,
            };

            command.Description = "Initiate project. Define a new project for AutoVersionsDB.";

            command.Handler = CommandHandler
                .Create<ProjectConfigItem>((ProjectConfigItem projectConfig) =>
            {
                _consoleHandler.StartSpiiner();
                ProcessTrace processReults = _projectConfigsAPI.SaveNewProjectConfig(projectConfig, _consoleHandler.OnNotificationStateChanged);
                _consoleHandler.StopSpinner();

                _consoleHandler.ProcessComplete(processReults);
            });

            return command;
        }
    }
}
