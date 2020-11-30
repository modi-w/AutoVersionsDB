using AutoVersionsDB.Core.DBVersions;
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

            command.Description = "Show list of the scripts files.";

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

                    renderFilesList(scriptFilesState.IncrementalScriptFilesComparer);

                    renderFilesList(scriptFilesState.RepeatableScriptFilesComparer);

                    if (scriptFilesState.DevDummyDataScriptFilesComparer != null)
                    {
                        renderFilesList(scriptFilesState.DevDummyDataScriptFilesComparer);
                    }
                }
            });

            appendIncrementalCommand(command);
            appendRepeatableCommand(command);
            appendDevDummyDataCommand(command);

            return command;
        }

        private void appendIncrementalCommand(Command command)
        {
            Command incrementalCommand = new Command("incremental")
            {
                _idOption,
            };
            incrementalCommand.Description = "Show only the incremental scripts files.";

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

                    renderFilesList(scriptFilesState.IncrementalScriptFilesComparer);
                }
            });

            command.Add(incrementalCommand);
        }

        private void appendRepeatableCommand(Command command)
        {
            Command incrementalCommand = new Command("repeatable")
            {
                _idOption,
            };
            incrementalCommand.Description = "Show only the repeatable scripts files.";

            incrementalCommand.Handler = CommandHandler.Create<string>((id) =>
            {
                _consoleProcessMessages.StartProcessMessage("files repeatable", id);

                _consoleProcessMessages.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.GetScriptFilesState(id, _consoleProcessMessages.OnNotificationStateChanged);
                _consoleProcessMessages.StopSpinner();

                _consoleProcessMessages.ProcessComplete(processResults);

                if (!processResults.Trace.HasError)
                {
                    ScriptFilesState scriptFilesState = processResults.Results as ScriptFilesState;

                    renderFilesList(scriptFilesState.RepeatableScriptFilesComparer);
                }
            });

            command.Add(incrementalCommand);
        }

        private void appendDevDummyDataCommand(Command command)
        {
            Command incrementalCommand = new Command("ddd")
            {
                _idOption,
            };
            incrementalCommand.Description = "Show only the DevDummyData scripts files.";

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
                        renderFilesList(scriptFilesState.DevDummyDataScriptFilesComparer);
                    }
                }
            });

            command.Add(incrementalCommand);
        }


        private void renderFilesList(ScriptFilesComparerBase scriptFilesComparer)
        {
            _consoleProcessMessages.SetInfoMessage("");
            _consoleProcessMessages.SetInfoMessage($"++ {scriptFilesComparer.ScriptFileType.FileTypeCode} Scripts:");


            string captionsMessage = $"{"  Status".PadRight(10)} |  File";
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
