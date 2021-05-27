using AutoVersionsDB.CLI.ConfigProjects.CLIOptions;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace AutoVersionsDB.CLI.ConfigProjects
{
    public class InitCommandFactory : CLICommandFactory
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;

        private readonly IdCLIOption _idOption;
        private readonly DescriptionCLIOption _descriptionOption;
        private readonly DBTypeCLIOption _dbTypeOption;
        private readonly ServerCLIOption _serverOption;
        private readonly DBNameCLIOption _dbNameOption;
        private readonly UsernameCLIOption _usernameOption;
        private readonly PasswordCLIOption _passwordOption;
        private readonly BackupFolderPathCLIOption _backupFolderPathOption;
        private readonly DevEnvironmentCLIOption _devEnvironmentOption;
        private readonly ScriptsBaseFolderPathCLIOption _scriptsBaseFolderPathOption;
        private readonly DeployArtifactFolderPathCLIOption _deployArtifactFolderPathOption;
        private readonly DeliveryArtifactFolderPathCLIOption _deliveryArtifactFolderPathOption;



        public InitCommandFactory(ProjectConfigsAPI projectConfigsAPI,
                                    IConsoleProcessMessages consoleProcessMessages,
                                    IdCLIOption idOption,
                                    DescriptionCLIOption descriptionOption,
                                    DBTypeCLIOption dbTypeOption,
                                    ServerCLIOption serverCLIOption,
                                    DBNameCLIOption dbNameCLIOption,
                                    UsernameCLIOption usernameCLIOption,
                                    PasswordCLIOption passwordCLIOption,



        BackupFolderPathCLIOption backupFolderPathOption,
                                    DevEnvironmentCLIOption devEnvironmentOption,
                                    ScriptsBaseFolderPathCLIOption scriptsBaseFolderPathOption,
                                    DeployArtifactFolderPathCLIOption deployArtifactFolderPathOption,
                                    DeliveryArtifactFolderPathCLIOption deliveryArtifactFolderPathOption)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _consoleProcessMessages = consoleProcessMessages;
            _idOption = idOption;
            _descriptionOption = descriptionOption;
            _dbTypeOption = dbTypeOption;
            _serverOption = serverCLIOption;
            _dbNameOption = dbNameCLIOption;
            _usernameOption = usernameCLIOption;
            _passwordOption = passwordCLIOption;
            _backupFolderPathOption = backupFolderPathOption;
            _devEnvironmentOption = devEnvironmentOption;
            _scriptsBaseFolderPathOption = scriptsBaseFolderPathOption;
            _deployArtifactFolderPathOption = deployArtifactFolderPathOption;
            _deliveryArtifactFolderPathOption = deliveryArtifactFolderPathOption;
        }

        public override Command Create()
        {
            Command command = new Command("init")
            {
                _idOption,
                _descriptionOption,
                _dbTypeOption,
                _serverOption,
                _dbNameOption,
                _usernameOption,
                _passwordOption,
                _backupFolderPathOption,
                _devEnvironmentOption,
                _scriptsBaseFolderPathOption,
                _deployArtifactFolderPathOption,
                _deliveryArtifactFolderPathOption,
            };

            command.Description = CLITextResources.InitCommandDescription;

            command.Handler = CommandHandler
                .Create((ProjectConfigItem projectConfig) =>
            {
                _consoleProcessMessages.StartProcessMessage("init", projectConfig.Id);

                _consoleProcessMessages.StartSpiiner();
                ProcessResults processResults = _projectConfigsAPI.SaveNewProjectConfig(projectConfig, _consoleProcessMessages.OnNotificationStateChanged);
                _consoleProcessMessages.StopSpinner();

                _consoleProcessMessages.ProcessComplete(processResults);
            });

            return command;
        }
    }
}
