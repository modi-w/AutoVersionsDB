using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.Common.Validators;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common
{
    public class ProjectConfigValidationsFactory : ValidationsFactory
    {
        private readonly DBCommandsFactory _dbCommandsFactoryProvider;

        public override string ValidationName => "Project Config";


        public ProjectConfigValidationsFactory(DBCommandsFactory dbCommandsFactory)
        {
            _dbCommandsFactoryProvider = dbCommandsFactory;
        }


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            ProjectConfigItem projectConfig = (processContext as CommonProcessContext).ProjectConfig;


            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            IdMandatory projectIdValidator = new IdMandatory(projectConfig.Id);
            validationsGroup.Add(projectIdValidator);

            DBTypeValidator dbTypeValidator = new DBTypeValidator(projectConfig.DBConnectionInfo.DBType, _dbCommandsFactoryProvider);
            validationsGroup.Add(dbTypeValidator);

            DBNameValidator dbNameValidator = new DBNameValidator(projectConfig.DBConnectionInfo.DBName);
            validationsGroup.Add(dbNameValidator);

            ConnectionStringValidator connectionStringValidator = new ConnectionStringValidator(projectConfig.DBConnectionInfo, _dbCommandsFactoryProvider);
            validationsGroup.Add(connectionStringValidator);

            AdminConnectionStringValidator adminConnectionStringValidator = new AdminConnectionStringValidator(projectConfig.DBConnectionInfo, _dbCommandsFactoryProvider);
            validationsGroup.Add(adminConnectionStringValidator);

            DBBackupFolderValidator dbBackupBaseFolderValidator = new DBBackupFolderValidator(projectConfig.BackupFolderPath);
            validationsGroup.Add(dbBackupBaseFolderValidator);

            if (projectConfig.DevEnvironment)
            {
                DevScriptsBaseFolderPathValidator scriptsRootFolderPathValidator = new DevScriptsBaseFolderPathValidator(projectConfig.DevEnvironment, projectConfig.DevScriptsBaseFolderPath);
                validationsGroup.Add(scriptsRootFolderPathValidator);

                if (!string.IsNullOrWhiteSpace(projectConfig.DevScriptsBaseFolderPath))
                {
                    ScriptsFolderPathValidator incrementalScriptsFolderPathValidator =
                        new ScriptsFolderPathValidator(nameof(projectConfig.IncrementalScriptsFolderPath), projectConfig.IncrementalScriptsFolderPath);
                    validationsGroup.Add(incrementalScriptsFolderPathValidator);

                    ScriptsFolderPathValidator repeatableScriptsFolderPathValidator =
                        new ScriptsFolderPathValidator(nameof(projectConfig.RepeatableScriptsFolderPath), projectConfig.RepeatableScriptsFolderPath);
                    validationsGroup.Add(repeatableScriptsFolderPathValidator);

                    ScriptsFolderPathValidator devDummyDataScriptsFolderPathValidator =
                        new ScriptsFolderPathValidator(nameof(projectConfig.DevDummyDataScriptsFolderPath), projectConfig.DevDummyDataScriptsFolderPath);
                    validationsGroup.Add(devDummyDataScriptsFolderPathValidator);
                }

                DeployArtifactFolderPathValidator deployArtifactFolderPathValidator = new DeployArtifactFolderPathValidator(projectConfig.DeployArtifactFolderPath);
                validationsGroup.Add(deployArtifactFolderPathValidator);


            }
            else
            {
                DeliveryArtifactFolderPathValidator deliveryArtifactFolderPathValidator = new DeliveryArtifactFolderPathValidator(projectConfig.DeliveryArtifactFolderPath);
                validationsGroup.Add(deliveryArtifactFolderPathValidator);
            }


            return validationsGroup;
        }
    }
}
