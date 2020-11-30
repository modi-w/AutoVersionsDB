using AutoVersionsDB.Core.DBVersions.Processes.Validators;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories
{
    public class DBStateValidationsFactory : ValidationsFactory
    {
        public override string ValidationName => "DB State";

        public override ValidationsGroup Create(ProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            ValidationsGroup validationsGroup = new ValidationsGroup(false);

            DBVersionsProcessContext dbVersionsProcessContext = processContext as DBVersionsProcessContext;

            HistoryExecutedFilesChangedValidator isHistoryExecutedFilesChangedValidator =
                new HistoryExecutedFilesChangedValidator(dbVersionsProcessContext.ScriptFilesState);
            validationsGroup.Add(isHistoryExecutedFilesChangedValidator);

            return validationsGroup;
        }


    }
}
