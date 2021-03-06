﻿using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace AutoVersionsDB.CLI.DBVersions
{
    public class FilesCommandFactory : CLICommandFactory
    {
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;
        private readonly IdCLIOption _idOption;

        public FilesCommandFactory(DBVersionsAPI dbVersionsAPI,
                                    IConsoleProcessMessages consoleProcessMessages,
                                    IdCLIOption idOption)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleProcessMessages = consoleProcessMessages;
            _idOption = idOption;
        }

        public override Command Create()
        {
            Command command = new Command("files")
            {
                _idOption,
            };

            command.Description = CLITextResources.FilesCommandDescription;

            command.Handler = CommandHandler.Create<string>((id) =>
            {
                _consoleProcessMessages.StartProcessMessage("files", id);

                _consoleProcessMessages.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.GetScriptFilesState(id, _consoleProcessMessages.OnNotificationStateChanged);
                _consoleProcessMessages.StopSpinner();

                _consoleProcessMessages.ProcessComplete(processResults);

                if (!processResults.Trace.HasError)
                {
                    ScriptFilesState scriptFilesState = processResults.Results as ScriptFilesState;

                    RenderFilesList(scriptFilesState.IncrementalScriptFilesComparer);

                    RenderFilesList(scriptFilesState.RepeatableScriptFilesComparer);

                    if (scriptFilesState.DevDummyDataScriptFilesComparer != null)
                    {
                        RenderFilesList(scriptFilesState.DevDummyDataScriptFilesComparer);
                    }
                }
            });

            AppendIncrementalCommand(command);
            AppendRepeatableCommand(command);
            AppendDevDummyDataCommand(command);

            return command;
        }

        private void AppendIncrementalCommand(Command command)
        {
            Command incrementalCommand = new Command("incremental")
            {
                _idOption,
            };
            incrementalCommand.Description = CLITextResources.FilesSignleTypeCommandDescription.Replace("[ScriptFileType]", "Incremental");

            incrementalCommand.Handler = CommandHandler.Create<string>((id) =>
            {
                _consoleProcessMessages.StartProcessMessage("files", id);

                _consoleProcessMessages.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.GetScriptFilesState(id, _consoleProcessMessages.OnNotificationStateChanged);
                _consoleProcessMessages.StopSpinner();

                _consoleProcessMessages.ProcessComplete(processResults);

                if (!processResults.Trace.HasError)
                {
                    ScriptFilesState scriptFilesState = processResults.Results as ScriptFilesState;

                    RenderFilesList(scriptFilesState.IncrementalScriptFilesComparer);
                }
            });

            command.Add(incrementalCommand);
        }

        private void AppendRepeatableCommand(Command command)
        {
            Command repeatableCommand = new Command("repeatable")
            {
                _idOption,
            };
            repeatableCommand.Description = CLITextResources.FilesSignleTypeCommandDescription.Replace("[ScriptFileType]", "Repeatable"); 

            repeatableCommand.Handler = CommandHandler.Create<string>((id) =>
            {
                _consoleProcessMessages.StartProcessMessage("files repeatable", id);

                _consoleProcessMessages.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.GetScriptFilesState(id, _consoleProcessMessages.OnNotificationStateChanged);
                _consoleProcessMessages.StopSpinner();

                _consoleProcessMessages.ProcessComplete(processResults);

                if (!processResults.Trace.HasError)
                {
                    ScriptFilesState scriptFilesState = processResults.Results as ScriptFilesState;

                    RenderFilesList(scriptFilesState.RepeatableScriptFilesComparer);
                }
            });

            command.Add(repeatableCommand);
        }

        private void AppendDevDummyDataCommand(Command command)
        {
            Command incrementalCommand = new Command("ddd")
            {
                _idOption,
            };
            incrementalCommand.Description = CLITextResources.FilesSignleTypeCommandDescription.Replace("[ScriptFileType]", "DevDummyData"); 

            incrementalCommand.Handler = CommandHandler.Create<string>((id) =>
            {
                _consoleProcessMessages.StartProcessMessage("files ddd", id);

                _consoleProcessMessages.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.GetScriptFilesState(id, _consoleProcessMessages.OnNotificationStateChanged);
                _consoleProcessMessages.StopSpinner();

                _consoleProcessMessages.ProcessComplete(processResults);

                if (!processResults.Trace.HasError)
                {
                    ScriptFilesState scriptFilesState = processResults.Results as ScriptFilesState;

                    if (scriptFilesState.DevDummyDataScriptFilesComparer != null)
                    {
                        RenderFilesList(scriptFilesState.DevDummyDataScriptFilesComparer);
                    }
                }
            });

            command.Add(incrementalCommand);
        }


        private void RenderFilesList(ScriptFilesComparerBase scriptFilesComparer)
        {
            _consoleProcessMessages.SetInfoMessage("");
            _consoleProcessMessages.SetInfoMessage($"++ {scriptFilesComparer.ScriptFileType.FileTypeCode} Scripts:");


            string captionsMessage = $"{"  Status",-10} |  File";
            _consoleProcessMessages.SetInfoMessage(captionsMessage);

            string captionsLineMessage = "-".PadRight(55, '-');
            _consoleProcessMessages.SetInfoMessage(captionsLineMessage);


            foreach (var file in scriptFilesComparer.AllFileSystemScriptFiles)
            {
                switch (file.HashDiffType)
                {
                    case HashDiffType.Different:

                        _consoleProcessMessages.SetInlineInfoMessage($"   changed".PadRight(11), ConsoleColor.DarkRed);
                        break;
                    case HashDiffType.Equal:

                        _consoleProcessMessages.SetInlineInfoMessage($"   sync".PadRight(11), ConsoleColor.DarkGreen);
                        break;
                   
                    case HashDiffType.EqualVirtual:

                        _consoleProcessMessages.SetInlineInfoMessage($"   sync vr".PadRight(11), ConsoleColor.DarkGreen);
                        break;

                    case HashDiffType.NotExist:
                    default:

                        _consoleProcessMessages.SetInlineInfoMessage($" ".PadRight(11), ConsoleColor.Gray);
                        break;
                }

                _consoleProcessMessages.SetInlineInfoMessage($"| {file.Filename}", ConsoleColor.Gray);

                _consoleProcessMessages.SetInfoMessage("");
            }
        }
    }
}
