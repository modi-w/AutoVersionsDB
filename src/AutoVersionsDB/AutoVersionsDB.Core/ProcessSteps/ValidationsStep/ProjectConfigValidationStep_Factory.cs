using System.Collections.Generic;
using Ninject;
using AutoVersionsDB.Core.Validations.ProjectConfigValidators;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.DbCommands.Integration;

namespace AutoVersionsDB.Core.ProcessSteps.ValidationsStep
{
    public static class ProjectConfigValidationStepFluent
    {
        public static AutoVersionsDbEngine ProjectConfigValidation(this AutoVersionsDbEngine autoVersionsDbEngine, ProjectConfigItem projectConfig)
        {
            ProjectConfigValidationStep_Factory projectConfigValidationSteFactory = NinjectUtils.KernelInstance.Get<ProjectConfigValidationStep_Factory>();

            ValidationsStep projectConfigValidationStep = projectConfigValidationSteFactory.Create(projectConfig);

            autoVersionsDbEngine.AppendProcessStep(projectConfigValidationStep);

            return autoVersionsDbEngine;
        }
    }


    public class ProjectConfigValidationStep_Factory
    {


        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;
        private DBCommands_FactoryProvider _dbCommands_FactoryProvider;


        public ProjectConfigValidationStep_Factory(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                                            DBCommands_FactoryProvider dbCommands_FactoryProvider)



        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;

            _dbCommands_FactoryProvider = dbCommands_FactoryProvider;
        }

        public ValidationsStep Create(ProjectConfigItem projectConfig)
        {
            List<ValidatorBase> validators = new List<ValidatorBase>();

            ProjectNameValidator projectNameValidator = new ProjectNameValidator(projectConfig.ProjectName);
            validators.Add(projectNameValidator);

            DBTypeValidator dbTypeValidator = new DBTypeValidator(projectConfig.DBTypeCode, _dbCommands_FactoryProvider);
            validators.Add(dbTypeValidator);

            ConnStrValidator connStrValidator = new ConnStrValidator(projectConfig.ConnStr, projectConfig.DBTypeCode, _dbCommands_FactoryProvider);
            validators.Add(connStrValidator);

            ConnStrValidator connStrToMasterDBValidator = new ConnStrValidator(projectConfig.ConnStrToMasterDB, projectConfig.DBTypeCode, _dbCommands_FactoryProvider);
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
