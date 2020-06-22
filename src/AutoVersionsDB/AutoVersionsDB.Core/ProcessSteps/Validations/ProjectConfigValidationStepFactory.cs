using System.Collections.Generic;
using Ninject;
using AutoVersionsDB.Core.Validations.ProjectConfigValidators;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.Core.Utils;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public static class ProjectConfigValidationStepFluent
    {
        public static AutoVersionsDbEngine ProjectConfigValidation(this AutoVersionsDbEngine autoVersionsDbEngine, ProjectConfigItem projectConfig)
        {
            autoVersionsDbEngine.ThrowIfNull(nameof(autoVersionsDbEngine));
            projectConfig.ThrowIfNull(nameof(projectConfig));

            ProjectConfigValidationStepFactory projectConfigValidationSteFactory = NinjectUtils.KernelInstance.Get<ProjectConfigValidationStepFactory>();

            ValidationsStep projectConfigValidationStep = projectConfigValidationSteFactory.Create(projectConfig);

            autoVersionsDbEngine.AppendProcessStep(projectConfigValidationStep);

            return autoVersionsDbEngine;
        }
    }


    public class ProjectConfigValidationStepFactory
    {


        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;
        private DBCommandsFactoryProvider _dbCommandsFactoryProvider;


        public ProjectConfigValidationStepFactory(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                                            DBCommandsFactoryProvider dbCommandsFactoryProvider)



        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }

        public ValidationsStep Create(ProjectConfigItem projectConfig)
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



            SingleValidationStep singleValidationStep = new SingleValidationStep();

            ValidationsStep validationStep =
                new ValidationsStep(_notificationExecutersFactoryManager, true, singleValidationStep, validators);

            return validationStep;
        }

    }
}
