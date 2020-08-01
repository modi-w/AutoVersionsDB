using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.Core.Validations.ProjectConfigValidators;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class ProjectConfigValidationStep : ValidationsStep
    {
        private DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        protected override bool ShouldContinueWhenFindError => true;


        public ProjectConfigValidationStep(SingleValidationStep singleValidationStep,
                                            DBCommandsFactoryProvider dbCommandsFactoryProvider)
         : base(singleValidationStep)
        {
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }


        protected override void SetValidators(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));


            ProjectNameValidator projectNameValidator = new ProjectNameValidator(projectConfig.ProjectName);
            Validators.Add(projectNameValidator);

            DBTypeValidator dbTypeValidator = new DBTypeValidator(projectConfig.DBTypeCode, _dbCommandsFactoryProvider);
            Validators.Add(dbTypeValidator);

            ConnStrValidator connStrValidator = new ConnStrValidator(projectConfig.ConnStr, projectConfig.DBTypeCode, _dbCommandsFactoryProvider);
            Validators.Add(connStrValidator);

            ConnStrValidator connStrToMasterDBValidator = new ConnStrValidator(projectConfig.ConnStrToMasterDB, projectConfig.DBTypeCode, _dbCommandsFactoryProvider);
            Validators.Add(connStrToMasterDBValidator);

            DBBackupFolderValidator dbBackupBaseFolderValidator = new DBBackupFolderValidator(projectConfig.DBBackupBaseFolder);
            Validators.Add(dbBackupBaseFolderValidator);

            if (projectConfig.IsDevEnvironment)
            {
                DevScriptsBaseFolderPathValidator scriptsRootFolderPathValidator = new DevScriptsBaseFolderPathValidator(projectConfig);
                Validators.Add(scriptsRootFolderPathValidator);

                if (!string.IsNullOrWhiteSpace(projectConfig.DevScriptsBaseFolderPath))
                {
                    ScriptsFolderPathValidator incrementalScriptsFolderPathValidator =
                        new ScriptsFolderPathValidator(projectConfig.IncrementalScriptsFolderPath);
                    Validators.Add(incrementalScriptsFolderPathValidator);

                    ScriptsFolderPathValidator repeatableScriptsFolderPathValidator =
                        new ScriptsFolderPathValidator(projectConfig.RepeatableScriptsFolderPath);
                    Validators.Add(repeatableScriptsFolderPathValidator);

                    ScriptsFolderPathValidator devDummyDataScriptsFolderPathValidator =
                        new ScriptsFolderPathValidator(projectConfig.DevDummyDataScriptsFolderPath);
                    Validators.Add(devDummyDataScriptsFolderPathValidator);
                }
            }
            else
            {
                DeliveryArtifactFolderPathValidator deliveryArtifactFolderPathValidator = new DeliveryArtifactFolderPathValidator(projectConfig);
                Validators.Add(deliveryArtifactFolderPathValidator);
            }
        }
    }
}
