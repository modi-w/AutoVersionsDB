using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core
{
    public static class CoreTextResources
    {
        //Core Error Messages
        public const string ProjectConfigValidation = "Project Config Validation Error";

        public const string MandatoryFieldErrorMessage = "'[FieldName]' is mandatory";
        public const string AdminConnectionStringValidatorErrorMessage = "Could not connect to the Database with the following connection String: '[ConnectionString]'. Error Message: '[exMessage]'";
        public const string ConnectionStringValidatorErrorMessage = "Could not connect to the Database with the following properties: '[DBConnectionInfo]', Check each of the above properties. The used connection String was: '[ConnectionString]'. Error Message: '[exMessage]'";
        public const string DBTypeCodeNotValidErrorMessage = "The DBType: '[DBTypeCode]' is not valid";
        public const string DeliveryArtifactFolderNotExistErrorMessage = "Delivery Artifact Folder is not exist";
        public const string DevScriptsFolderNotExistErrorMessage = "Scripts Folder is not exist";
        public const string ScriptTypeFolderNotExistErrorMessage = "'[ScriptFolderPath]' Scripts Folder is not exist";
        public const string ProjectIdIsNotExistErrorMessage = "Project Id: '[Id]' is not exist.";
        public const string ProjectIdIsAlreadyExistErrorMessage = "Project Id is already exist";
        public const string ProjectIdIsAlreadyExistWithIdErrorMessage = "Project Id: '[Id]' is already exist";
        public const string ArtifactFileNotExistErrorMessage = "Delivery Artifact File does not exist";
        public const string ArtifactFolderExistErrorMessage = "Delivery Artifact Folder does not exist";
        public const string DeliveryEnvErrorMessage = "Could not run this command on Delivery Environment";
        public const string FilesChangedErrorMessage = "The following files changed: '[FilesList]'";
        public const string HistoryFilesNotExecutedErrorMessage = "The history file '[FileName]' is not executed on this Database";
        public const string HistoryExecutedFilesMissingErrorMessage = "The following files missing from the scripts folder: '[FilesList]'";
        public const string NewProjectDevEnvErrorMessage = "No scripts file, and no system tables on the DB, its probably a new project on dev environment.";
        public const string NewProjectDeliveryEnvErrorMessage = "No system tables on the DB, its probably a new project on delivery environment.";
        public const string NewProjectDeliveryEnvNoscriptsFilesEnvErrorMessage = "No scripts file, and no system tables on the DB, its probably a new project on delivery environment.";
        public const string InvalidFilenameErrorMessage = "Filename '[Filename]' not valid for script type: '[FileTypeCode]'. Should be like the following pattern: '[FilenamePattern]'";
        public const string TableNotExistErrorMessage = "The table '[TableName]' is not exist in the DB";
        public const string TableMissingColumnErrorMessage = "The table '[TableName]' is missing the column '[ColumnName]'";
        public const string TableColumnIvalidTypeErrorMessage = "The column '[ColumnName]' has the type '[DBDataType]' instead of '[StructDataType]', in the table {DBCommandsConsts.DBScriptsExecutionHistoryFullTableName}";
        public const string HistoricalTargetStateScriptErrorMessage = "The target file '[FileName]' is already executed on this database.";
        public const string TargetStateScriptFileNotExistErrorMessage = "The target file '[FileName]' is not exsit.";


        //Core Validators Instructions Messages
        public const string HistoryExecutedFilesChangedInstructionsMessage = "History executed files changed, please 'Recreate DB From Scratch' or 'Set DB State as Virtual Execution'";
        public const string RestoreDBFailInstructionsMessage = "The process fail when trying to 'Restore the Database', try to change the Timeout parameter and restore the database manually.";
        public const string NextScriptFileNameInstructionsMessage = "Invalid script name";
        public const string SystemTablesDevEnvInstructionsMessage = "The system tables has invalid structure. Please try to 'Recreate DB From Scratch' or 'Set DB State by Virtual Execution'.";
        public const string SystemTablesDeliveryEnvInstructionsMessage = "The system tables has invalid structure. Please try to 'Set DB State by Virtual Execution'.";
        public const string HistoricalTargetStateScriptInstructionsMessage = "Target State Script Should Not Be Historical";
        public const string TargetStateScriptFileNotExistInstructionsMessage = "Target State Script File does not exist";
        public static string NewProjectDevEnvInstructionsMessage => $"Welcome!!! This appear to be a new project.{Environment.NewLine}1) Run 'Recreate' or 'Virtual' for creating our DB system tables >> 2) Add your scripts files >> 3) Run 'Sync'";
        public static string NewProjectDeliveryEnvInstructionsMessage => $"Welcome!!! This appear to be a new project.{Environment.NewLine}1) Run 'Virtual' to set the current DB state related to the scripts file >> 2) Run 'Sync' for executing all other scripts files on the DB";
        public static string NewProjectDeliveryEnvNoscriptsFilesInstructionsMessage => $"Welcome!!! This appear to be a new project.{Environment.NewLine}1) Copy the artifact file that deployed from your dev environment >> 2) Run 'Virtual' to set the current DB state related to the scripts file >> 3) Run 'Sync' for executing the rest of the scripts files";


        //Core Process Messages
        public const string IgnoreBecauseVirtualExecution = " - Ignore (virtual execution)";

        
        //Core Exceptions Messages
        public const string CantDropDBOnDelEnvExecption = "Can't Drop DB when running on none dev enviroment (you can change the parameter in project setting).";
        //TODO: IncrementalRuntimeScriptFile Exceptions
        //TODO: RepeatableRuntimeScriptFile Exceptions
        //TODO: FileSystemScriptFiles Exceptions



    }
}
