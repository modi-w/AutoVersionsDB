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
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        public override string ValidationName => "Project Config";


        public ProjectConfigValidationsFactory(DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            ProjectConfigItem projectConfig = (processContext as CommonProcessContext).ProjectConfig;


            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            IdMandatory projectNameValidator = new IdMandatory(projectConfig.Id);
            validationsGroup.Add(projectNameValidator);

            DBTypeValidator dbTypeValidator = new DBTypeValidator(projectConfig.DBType, _dbCommandsFactoryProvider);
            validationsGroup.Add(dbTypeValidator);

            ConnStrValidator connStrValidator = new ConnStrValidator(nameof(projectConfig.ConnectionString), projectConfig.ConnectionString, projectConfig.DBType, _dbCommandsFactoryProvider);
            validationsGroup.Add(connStrValidator);

            ConnStrValidator connStrToMasterDBValidator = new ConnStrValidator(nameof(projectConfig.ConnectionStringToMasterDB), projectConfig.ConnectionStringToMasterDB, projectConfig.DBType, _dbCommandsFactoryProvider);
            validationsGroup.Add(connStrToMasterDBValidator);

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
