using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.Core.Validations.ExectutionParamsValidations;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class TargetStateScriptFileValidationsFactory : ValidationsFactory
    {
        internal override string ValidationName => "Target State Script File";


        internal override ValidationsGroup Create(ProcessContext processContext)
        {
            ProjectConfigItem projectConfig = (processContext as IProjectConfigable).ProjectConfig;

            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            DBVersionsProcessContext dbVersionsProcessContext = processContext as DBVersionsProcessContext;
            DBVersionsProcessParams dbVersionsProcessParams = dbVersionsProcessContext.ProcessParams as DBVersionsProcessParams;


            TargetStateScriptFileExistValidator targetStateScriptFileExistValidator = new TargetStateScriptFileExistValidator(dbVersionsProcessContext.ScriptFilesState, dbVersionsProcessParams.TargetStateScriptFileName);
            validationsGroup.Add(targetStateScriptFileExistValidator);

            IsTargetScriptFiletAlreadyExecutedValidator isTargetScriptFiletAlreadyExecutedValidator = new IsTargetScriptFiletAlreadyExecutedValidator(dbVersionsProcessContext.ScriptFilesState, dbVersionsProcessParams.TargetStateScriptFileName);
            validationsGroup.Add(isTargetScriptFiletAlreadyExecutedValidator);

            return validationsGroup;
        }

    }
}
