using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.Core.Validations.ProjectConfigValidators;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class ProjectConfigValidationsFactory : ValidationsFactory
    {
        private DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        public ProjectConfigValidationsFactory(DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }


        public override List<ValidatorBase> Create(ProjectConfig projectConfig, AutoVersionsDbProcessState processState)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));


            List<ValidatorBase> validators = new List<ValidatorBase>();

            ProjectNameValidator projectNameValidator = new ProjectNameValidator(projectConfig.ProjectName);
            validators.Add(projectNameValidator);

            DBTypeValidator dbTypeValidator = new DBTypeValidator(projectConfig.DBTypeCode, _dbCommandsFactoryProvider);
            validators.Add(dbTypeValidator);

            ConnStrValidator connStrValidator = new ConnStrValidator(projectConfig.ConnStr, projectConfig.DBTypeCode, _dbCommandsFactoryProvider);
            validators.Add(connStrValidator);

            ConnStrValidator connStrToMasterDBValidator = new ConnStrValidator(projectConfig.ConnStrToMasterDB, projectConfig.DBTypeCode, _dbCommandsFactoryProvider);
            validators.Add(connStrToMasterDBValidator);

            DBBackupFolderValidator dbBackupBaseFolderValidator = new DBBackupFolderValidator(projectConfig.DBBackupBaseFolder);
            validators.Add(dbBackupBaseFolderValidator);

            if (projectConfig.IsDevEnvironment)
            {
                DevScriptsBaseFolderPathValidator scriptsRootFolderPathValidator = new DevScriptsBaseFolderPathValidator(projectConfig);
                validators.Add(scriptsRootFolderPathValidator);

                if (!string.IsNullOrWhiteSpace(projectConfig.DevScriptsBaseFolderPath))
                {
                    ScriptsFolderPathValidator incrementalScriptsFolderPathValidator =
                        new ScriptsFolderPathValidator(projectConfig.IncrementalScriptsFolderPath);
                    validators.Add(incrementalScriptsFolderPathValidator);

                    ScriptsFolderPathValidator repeatableScriptsFolderPathValidator =
                        new ScriptsFolderPathValidator(projectConfig.RepeatableScriptsFolderPath);
                    validators.Add(repeatableScriptsFolderPathValidator);

                    ScriptsFolderPathValidator devDummyDataScriptsFolderPathValidator =
                        new ScriptsFolderPathValidator(projectConfig.DevDummyDataScriptsFolderPath);
                    validators.Add(devDummyDataScriptsFolderPathValidator);
                }
            }
            else
            {
                DeliveryArtifactFolderPathValidator deliveryArtifactFolderPathValidator = new DeliveryArtifactFolderPathValidator(projectConfig);
                validators.Add(deliveryArtifactFolderPathValidator);
            }


            return validators;
        }
    }
}
