﻿using AutoVersionsDB.Core.Common.Validators;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.DB;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common
{
    public class ProjectConfigValidationsFactory : ValidationsFactory
    {
        private readonly DBCommandsFactory _dbCommandsFactory;

        public const string Name = "Project Config";
        public override string ValidationName => Name;



        public ProjectConfigValidationsFactory(DBCommandsFactory dbCommandsFactory)
        {
            _dbCommandsFactory = dbCommandsFactory;
        }


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            ProjectConfigItem projectConfig = (processContext as CommonProcessContext).ProjectConfig;


            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            IdMandatory projectIdValidator = new IdMandatory(projectConfig.Id);
            validationsGroup.Add(projectIdValidator);

            DBTypeValidator dbTypeValidator = new DBTypeValidator(projectConfig.DBConnectionInfo.DBType, _dbCommandsFactory);
            validationsGroup.Add(dbTypeValidator);


            DBNameValidator dbNameValidator = new DBNameValidator(projectConfig.DBConnectionInfo.DBName);
            validationsGroup.Add(dbNameValidator);

            ConnectionStringValidator connectionStringValidator = new ConnectionStringValidator(projectConfig.DBConnectionInfo, _dbCommandsFactory);
            validationsGroup.Add(connectionStringValidator);

            AdminConnectionStringValidator adminConnectionStringValidator = new AdminConnectionStringValidator(projectConfig.DBConnectionInfo, _dbCommandsFactory);
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
