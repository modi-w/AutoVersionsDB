using AutoVersionsDB.Core.DBVersions.Processes.Validators;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories
{
    public class DBStateValidationsFactory : ValidationsFactory
    {
        public const string Name = "DB State";
        public override string ValidationName => Name;


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            ValidationsGroup validationsGroup = new ValidationsGroup(false);

            DBVersionsProcessContext dbVersionsProcessContext = processContext as DBVersionsProcessContext;

            HistoryExecutedFilesChangedValidator isHistoryExecutedFilesChangedValidatorInc =
                new HistoryExecutedFilesChangedValidator(dbVersionsProcessContext.ScriptFilesState.IncrementalScriptFilesComparer);
            validationsGroup.Add(isHistoryExecutedFilesChangedValidatorInc);

            if (dbVersionsProcessContext.ProjectConfig.DevEnvironment)
            {
                HistoryExecutedFilesChangedValidator isHistoryExecutedFilesChangedValidatorDDD =
                    new HistoryExecutedFilesChangedValidator(dbVersionsProcessContext.ScriptFilesState.DevDummyDataScriptFilesComparer);
                validationsGroup.Add(isHistoryExecutedFilesChangedValidatorDDD);
            }

            return validationsGroup;
        }


    }
}
