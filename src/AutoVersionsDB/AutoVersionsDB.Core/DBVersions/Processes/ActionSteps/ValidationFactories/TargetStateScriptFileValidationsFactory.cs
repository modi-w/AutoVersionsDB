using AutoVersionsDB.Core.DBVersions.Processes.Validators;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories
{
    public class TargetStateScriptFileValidationsFactory : ValidationsFactory
    {
        public override string ValidationName => "Target State Script File";


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            DBVersionsProcessContext dbVersionsProcessContext = processContext as DBVersionsProcessContext;
            DBVersionsProcessArgs dbVersionsProcessArgs = dbVersionsProcessContext.ProcessArgs as DBVersionsProcessArgs;


            TargetStateScriptFileExistValidator targetStateScriptFileExistValidator = new TargetStateScriptFileExistValidator(dbVersionsProcessContext.ScriptFilesState, dbVersionsProcessArgs.TargetScripts.IncScriptFileName);
            validationsGroup.Add(targetStateScriptFileExistValidator);

            TargetScriptFiletAlreadyExecutedValidator isTargetScriptFiletAlreadyExecutedValidator = new TargetScriptFiletAlreadyExecutedValidator(dbVersionsProcessContext.ScriptFilesState, dbVersionsProcessArgs.TargetScripts.IncScriptFileName);
            validationsGroup.Add(isTargetScriptFiletAlreadyExecutedValidator);

            return validationsGroup;
        }

    }
}
