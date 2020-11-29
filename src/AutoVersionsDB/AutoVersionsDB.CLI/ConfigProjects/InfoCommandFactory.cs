using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Helpers;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace AutoVersionsDB.CLI.ConfigProjects
{
    public class InfoCommandFactory : CLICommandFactory
    {
        private const int c_paddingRightForCaprions = 35;

        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;

        private readonly IdCLIOption _idOption;


        public InfoCommandFactory(ProjectConfigsAPI projectConfigsAPI,
                                    IConsoleProcessMessages consoleProcessMessages,
                                    IdCLIOption idOption)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _consoleProcessMessages = consoleProcessMessages;

            _idOption = idOption;
        }

        public override Command Create()
        {
            Command command = new Command("info")
            {
                _idOption,
            };

            command.Description = "Show project details";

            command.Handler = CommandHandler
                .Create<string>((id) =>
             {
                 _consoleProcessMessages.StartProcessMessage("info", id);

                 ProjectConfigItem projectConfig = _projectConfigsAPI.GetProjectConfigById(id);

                 if (projectConfig == null)
                 {
                     _consoleProcessMessages.SetErrorInstruction($"Id '{id}' not exist. Try run list command to see the existing projects on this machine.");
                 }
                 else
                 {
                     string message = $"{"Id".PadRight(c_paddingRightForCaprions)}: {projectConfig.Id}";
                     _consoleProcessMessages.SetInfoMessage(message);

                     message = $"{"Description".PadRight(c_paddingRightForCaprions)}: {projectConfig.Description}";
                     _consoleProcessMessages.SetInfoMessage(message);

                     message = $"{"DBType".PadRight(c_paddingRightForCaprions)}: {projectConfig.DBType}";
                     _consoleProcessMessages.SetInfoMessage(message);

                     message = $"{"ServerInstance".PadRight(c_paddingRightForCaprions)}: {projectConfig.Server}";
                     _consoleProcessMessages.SetInfoMessage(message);

                     message = $"{"DataBaseName".PadRight(c_paddingRightForCaprions)}: {projectConfig.DBName}";
                     _consoleProcessMessages.SetInfoMessage(message);

                     message = $"{"DBUsername".PadRight(c_paddingRightForCaprions)}: {projectConfig.Username}";
                     _consoleProcessMessages.SetInfoMessage(message);

                     message = $"{"DBPassword".PadRight(c_paddingRightForCaprions)}: {projectConfig.Password}";
                     _consoleProcessMessages.SetInfoMessage(message);

                     message = $"{"Backup Folder Path".PadRight(c_paddingRightForCaprions)}: {projectConfig.BackupFolderPath}";
                     _consoleProcessMessages.SetInfoMessage(message);

                     message = $"{"Dev Environment".PadRight(c_paddingRightForCaprions)}: {projectConfig.DevEnvironment}";
                     _consoleProcessMessages.SetInfoMessage(message);

                     if (projectConfig.DevEnvironment)
                     {
                         message = $"{"Scripts Base Folder".PadRight(c_paddingRightForCaprions)}: {projectConfig.DevScriptsBaseFolderPath}";
                         _consoleProcessMessages.SetInfoMessage(message);

                         message = $"{" Incremental Scripts Folder".PadRight(c_paddingRightForCaprions)}: {projectConfig.IncrementalScriptsFolderPath}";
                         _consoleProcessMessages.SetInfoMessage(message);
                         message = $"{" Repeatable Scripts Folder".PadRight(c_paddingRightForCaprions)}: {projectConfig.RepeatableScriptsFolderPath}";
                         _consoleProcessMessages.SetInfoMessage(message);
                         message = $"{" Dev Dummy Data Scripts Folder".PadRight(c_paddingRightForCaprions)}: {projectConfig.DevDummyDataScriptsFolderPath}";
                         _consoleProcessMessages.SetInfoMessage(message);


                         message = $"{"Deploy Artifact Folder".PadRight(c_paddingRightForCaprions)}: {projectConfig.DeployArtifactFolderPath}";
                         _consoleProcessMessages.SetInfoMessage(message);
                     }
                     else
                     {
                         message = $"{"Delivery Artifact Folder Path".PadRight(c_paddingRightForCaprions)}: {projectConfig.DeliveryArtifactFolderPath}";
                         _consoleProcessMessages.SetInfoMessage(message);
                     }


                 }

             });

            return command;
        }


    }
}
