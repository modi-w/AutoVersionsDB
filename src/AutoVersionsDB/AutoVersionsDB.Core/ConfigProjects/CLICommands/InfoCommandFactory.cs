using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions;
using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands
{
    public class InfoCommandFactory : CLICommandFactory
    {
        private const int c_paddingRightForCaprions = 35;

        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly IConsoleHandler _consoleHandler;

        private readonly CodeCLIOption _codeOption;


        public InfoCommandFactory(ProjectConfigsAPI projectConfigsAPI,
                                    IConsoleHandler consoleHandler,
                                    CodeCLIOption codeOption)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _consoleHandler = consoleHandler;

            _codeOption = codeOption;
        }

        public override Command Create()
        {
            Command command = new Command("info")
            {
                _codeOption,
            };

            command.Description = "Show project details";

            command.Handler = CommandHandler
                .Create<string>((code) =>
             {
                 ProjectConfigItem projectConfig = _projectConfigsAPI.GetProjectConfigByProjectCode(code);

                 if (projectConfig == null)
                 {
                     _consoleHandler.SetErrorInstruction($"Project Code '{code}' not exist. Try run list command to see the existing projects on this machine.");
                 }
                 else
                 {
                     string message = $"{"Code".PadRight(c_paddingRightForCaprions)}: {projectConfig.Code}";
                     _consoleHandler.WriteLineInfo(message);

                     message = $"{"Description".PadRight(c_paddingRightForCaprions)}: {projectConfig.Description}";
                     _consoleHandler.WriteLineInfo(message);

                     message = $"{"DBType".PadRight(c_paddingRightForCaprions)}: {projectConfig.DBType}";
                     _consoleHandler.WriteLineInfo(message);

                     message = $"{"Connection String".PadRight(c_paddingRightForCaprions)}: {projectConfig.ConnectionString}";
                     _consoleHandler.WriteLineInfo(message);

                     message = $"{"Connection String To Master DB".PadRight(c_paddingRightForCaprions)}: {projectConfig.ConnectionStringToMasterDB}";
                     _consoleHandler.WriteLineInfo(message);

                     message = $"{"Backup Folder Path".PadRight(c_paddingRightForCaprions)}: {projectConfig.BackupFolderPath}";
                     _consoleHandler.WriteLineInfo(message);

                     message = $"{"Dev Environment".PadRight(c_paddingRightForCaprions)}: {projectConfig.DevEnvironment}";
                     _consoleHandler.WriteLineInfo(message);

                     if (projectConfig.DevEnvironment)
                     {
                         message = $"{"Scripts Base Folder".PadRight(c_paddingRightForCaprions)}: {projectConfig.DevScriptsBaseFolderPath}";
                         _consoleHandler.WriteLineInfo(message);

                         message = $"{" Incremental Scripts Folder".PadRight(c_paddingRightForCaprions)}: {projectConfig.IncrementalScriptsFolderPath}";
                         _consoleHandler.WriteLineInfo(message);
                         message = $"{" Repeatable Scripts Folder".PadRight(c_paddingRightForCaprions)}: {projectConfig.RepeatableScriptsFolderPath}";
                         _consoleHandler.WriteLineInfo(message);
                         message = $"{" Dev Dummy Data Scripts Folder".PadRight(c_paddingRightForCaprions)}: {projectConfig.DevDummyDataScriptsFolderPath}";
                         _consoleHandler.WriteLineInfo(message);


                         message = $"{"Deploy Artifact Folder".PadRight(c_paddingRightForCaprions)}: {projectConfig.DeployArtifactFolderPath}";
                         _consoleHandler.WriteLineInfo(message);
                     }
                     else
                     {
                         message = $"{"DeliveryArtifactFolderPath".PadRight(c_paddingRightForCaprions)}: {projectConfig.DeliveryArtifactFolderPath}";
                         _consoleHandler.WriteLineInfo(message);
                     }


                 }

             });

            return command;
        }


    }
}
