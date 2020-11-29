using AutoVersionsDB.Core.DBVersions.Processes.Validators;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories
{
    public class TargetStateScriptFileValidationsFactory : ValidationsFactory
    {
        public override string ValidationName => "Target State Script File";


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            DBVersionsProcessContext dbVersionsProcessContext = processContext as DBVersionsProcessContext;
            DBVersionsProcessParams dbVersionsProcessParams = dbVersionsProcessContext.ProcessParams as DBVersionsProcessParams;


            TargetStateScriptFileExistValidator targetStateScriptFileExistValidator = new TargetStateScriptFileExistValidator(dbVersionsProcessContext.ScriptFilesState, dbVersionsProcessParams.TargetStateScriptFileName);
            validationsGroup.Add(targetStateScriptFileExistValidator);

            TargetScriptFiletAlreadyExecutedValidator isTargetScriptFiletAlreadyExecutedValidator = new TargetScriptFiletAlreadyExecutedValidator(dbVersionsProcessContext.ScriptFilesState, dbVersionsProcessParams.TargetStateScriptFileName);
            validationsGroup.Add(isTargetScriptFiletAlreadyExecutedValidator);

            return validationsGroup;
        }

    }
}
