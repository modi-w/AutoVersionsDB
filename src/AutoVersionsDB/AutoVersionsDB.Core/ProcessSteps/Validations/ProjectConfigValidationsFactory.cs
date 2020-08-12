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
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        public ProjectConfigValidationsFactory(DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }


        public override ValidationsGroup Create(ProjectConfigItem projectConfig, AutoVersionsDbProcessState processState)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            ProjectNameValidator projectNameValidator = new ProjectNameValidator(projectConfig.ProjectName);
            validationsGroup.Add(projectNameValidator);

            DBTypeValidator dbTypeValidator = new DBTypeValidator(projectConfig.DBTypeCode, _dbCommandsFactoryProvider);
            validationsGroup.Add(dbTypeValidator);

            ConnStrValidator connStrValidator = new ConnStrValidator(projectConfig.ConnStr, projectConfig.DBTypeCode, _dbCommandsFactoryProvider);
            validationsGroup.Add(connStrValidator);

            ConnStrValidator connStrToMasterDBValidator = new ConnStrValidator(projectConfig.ConnStrToMasterDB, projectConfig.DBTypeCode, _dbCommandsFactoryProvider);
            validationsGroup.Add(connStrToMasterDBValidator);

            DBBackupFolderValidator dbBackupBaseFolderValidator = new DBBackupFolderValidator(projectConfig.DBBackupBaseFolder);
            validationsGroup.Add(dbBackupBaseFolderValidator);

            if (projectConfig.IsDevEnvironment)
            {
                DevScriptsBaseFolderPathValidator scriptsRootFolderPathValidator = new DevScriptsBaseFolderPathValidator(projectConfig);
                validationsGroup.Add(scriptsRootFolderPathValidator);

                if (!string.IsNullOrWhiteSpace(projectConfig.DevScriptsBaseFolderPath))
                {
                    ScriptsFolderPathValidator incrementalScriptsFolderPathValidator =
                        new ScriptsFolderPathValidator(projectConfig.IncrementalScriptsFolderPath);
                    validationsGroup.Add(incrementalScriptsFolderPathValidator);

                    ScriptsFolderPathValidator repeatableScriptsFolderPathValidator =
                        new ScriptsFolderPathValidator(projectConfig.RepeatableScriptsFolderPath);
                    validationsGroup.Add(repeatableScriptsFolderPathValidator);

                    ScriptsFolderPathValidator devDummyDataScriptsFolderPathValidator =
                        new ScriptsFolderPathValidator(projectConfig.DevDummyDataScriptsFolderPath);
                    validationsGroup.Add(devDummyDataScriptsFolderPathValidator);
                }
            }
            else
            {
                DeliveryArtifactFolderPathValidator deliveryArtifactFolderPathValidator = new DeliveryArtifactFolderPathValidator(projectConfig);
                validationsGroup.Add(deliveryArtifactFolderPathValidator);
            }


            return validationsGroup;
        }
    }
}
