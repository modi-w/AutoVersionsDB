using AutoVersionsDB.Core.DBVersions.Processes.Validators;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories
{
    public class TargetStateScriptFileValidationsFactory : ValidationsFactory
    {
        public const string Name = "Target State Script File";
        public override string ValidationName => Name;



        public override ValidationsGroup Create(ProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            DBVersionsProcessContext dbVersionsProcessContext = processContext as DBVersionsProcessContext;
            DBVersionsProcessArgs dbVersionsProcessArgs = dbVersionsProcessContext.ProcessArgs as DBVersionsProcessArgs;


            TargetStateScriptFileExistValidator targetStateScriptFileExistValidatorInc = new TargetStateScriptFileExistValidator(dbVersionsProcessContext.ScriptFilesState.IncrementalScriptFilesComparer, dbVersionsProcessArgs.TargetScripts.IncScriptFileName);
            validationsGroup.Add(targetStateScriptFileExistValidatorInc);

            TargetScriptFileAlreadyExecutedValidator isTargetScriptFiletAlreadyExecutedValidatorInc = 
                new TargetScriptFileAlreadyExecutedValidator(dbVersionsProcessContext.ScriptFilesState.IncrementalScriptFilesComparer, dbVersionsProcessArgs.TargetScripts.IncScriptFileName);
            validationsGroup.Add(isTargetScriptFiletAlreadyExecutedValidatorInc);


            //if (dbVersionsProcessContext.ProjectConfig.DevEnvironment)
            //{
            //    TargetStateScriptFileExistValidator targetStateScriptFileExistValidatorDDD = new TargetStateScriptFileExistValidator(dbVersionsProcessContext.ScriptFilesState.DevDummyDataScriptFilesComparer, dbVersionsProcessArgs.TargetScripts.DDDScriptFileName);
            //    validationsGroup.Add(targetStateScriptFileExistValidatorDDD);

            //    TargetScriptFileAlreadyExecutedValidator isTargetScriptFiletAlreadyExecutedValidatorDDD =
            //    new TargetScriptFileAlreadyExecutedValidator(dbVersionsProcessContext.ScriptFilesState.DevDummyDataScriptFilesComparer, dbVersionsProcessArgs.TargetScripts.DDDScriptFileName);
            //    validationsGroup.Add(isTargetScriptFiletAlreadyExecutedValidatorDDD);
            //}

            return validationsGroup;
        }

    }
}
