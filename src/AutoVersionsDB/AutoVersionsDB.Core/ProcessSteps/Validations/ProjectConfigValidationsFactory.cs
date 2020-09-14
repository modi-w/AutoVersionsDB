using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.Core.Validations.ProjectConfigValidators;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
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
            ProjectConfigItem projectConfig = (processContext as IProjectConfigable).ProjectConfig;


            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            ProjectCodeNotEmpty projectNameValidator = new ProjectCodeNotEmpty(projectConfig.ProjectCode);
            validationsGroup.Add(projectNameValidator);

            DBTypeValidator dbTypeValidator = new DBTypeValidator(projectConfig.DBTypeCode, _dbCommandsFactoryProvider);
            validationsGroup.Add(dbTypeValidator);

            ConnStrValidator connStrValidator = new ConnStrValidator(nameof(projectConfig.ConnStr), projectConfig.ConnStr, projectConfig.DBTypeCode, _dbCommandsFactoryProvider);
            validationsGroup.Add(connStrValidator);

            ConnStrValidator connStrToMasterDBValidator = new ConnStrValidator(nameof(projectConfig.ConnStrToMasterDB), projectConfig.ConnStrToMasterDB, projectConfig.DBTypeCode, _dbCommandsFactoryProvider);
            validationsGroup.Add(connStrToMasterDBValidator);

            DBBackupFolderValidator dbBackupBaseFolderValidator = new DBBackupFolderValidator(projectConfig.DBBackupBaseFolder);
            validationsGroup.Add(dbBackupBaseFolderValidator);

            if (projectConfig.IsDevEnvironment)
            {
                DevScriptsBaseFolderPathValidator scriptsRootFolderPathValidator = new DevScriptsBaseFolderPathValidator(projectConfig.IsDevEnvironment, projectConfig.DevScriptsBaseFolderPath);
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

                DeployArtifactFolderPathValidator deployArtifactFolderPathValidator = new DeployArtifactFolderPathValidator( projectConfig.DeployArtifactFolderPath);
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
