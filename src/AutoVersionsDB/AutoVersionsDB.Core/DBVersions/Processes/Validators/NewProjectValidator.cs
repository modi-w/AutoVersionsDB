using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.DB;
using AutoVersionsDB.DB.Contract;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoVersionsDB.Core.DBVersions.Processes.Validators
{
    public class NewProjectValidator : ValidatorBase
    {
        private readonly DBCommandsFactory _dbCommandsFactory;
        private readonly DBConnectionInfo _dbConnectionInfo;
        private readonly bool _isDevEnvironment;
        private readonly ScriptFilesState _scriptFilesState;

        public const string Name = "NewProject";
        public override string ValidatorName => Name;


        private string _errorInstructionsMessage;
        public override string ErrorInstructionsMessage => _errorInstructionsMessage;

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Attention;


        public NewProjectValidator(DBCommandsFactory dbCommandsFactory,
                                        bool isDevEnvironment,
                                        DBConnectionInfo dbConnectionInfo,
                                        ScriptFilesState scriptFilesState)
        {
            _dbCommandsFactory = dbCommandsFactory;
            _isDevEnvironment = isDevEnvironment;
            _dbConnectionInfo = dbConnectionInfo;
            _scriptFilesState = scriptFilesState;
        }


        public override string Validate()
        {
            using (var dbCommands = _dbCommandsFactory.CreateDBCommand(_dbConnectionInfo))
            {
                if (!dbCommands.CheckIfTableExist(DBCommandsConsts.DBSchemaName, DBCommandsConsts.DBScriptsExecutionHistoryTableName)
                    && !dbCommands.CheckIfTableExist(DBCommandsConsts.DBSchemaName, DBCommandsConsts.DBScriptsExecutionHistoryFilesTableName))
                {
                    

                    if (_isDevEnvironment)
                    {
                        if (_scriptFilesState.IncrementalScriptFilesComparer.AllFileSystemScriptFiles.Count == 0
                            && _scriptFilesState.RepeatableScriptFilesComparer.AllFileSystemScriptFiles.Count == 0
                            && _scriptFilesState.DevDummyDataScriptFilesComparer.AllFileSystemScriptFiles.Count == 0)
                        {
                            _errorInstructionsMessage = CoreTextResources.NewProjectDevEnvInstructionsMessage;
                            return CoreTextResources.NewProjectDevEnvErrorMessage;
                        }
                    }
                    else
                    {
                        if (_scriptFilesState.IncrementalScriptFilesComparer.AllFileSystemScriptFiles.Count == 0
                            && _scriptFilesState.RepeatableScriptFilesComparer.AllFileSystemScriptFiles.Count == 0)
                        {
                            //Comment: this message could be confusing, because it can popup when the user deploy an artifact file, but empty one
                            //TODO: change the following message
                            _errorInstructionsMessage = CoreTextResources.NewProjectDeliveryEnvNoscriptsFilesInstructionsMessage;
                            string errorMsg = CoreTextResources.NewProjectDeliveryEnvNoscriptsFilesEnvErrorMessage;
                            return errorMsg;
                        }
                        else
                        {
                            _errorInstructionsMessage = CoreTextResources.NewProjectDeliveryEnvInstructionsMessage;
                            return CoreTextResources.NewProjectDeliveryEnvErrorMessage;
                        }
                    }

                }

            }


            return "";

        }
    }
}
