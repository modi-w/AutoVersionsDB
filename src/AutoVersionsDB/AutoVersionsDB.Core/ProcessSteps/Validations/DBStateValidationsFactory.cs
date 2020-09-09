using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessDefinitions;
using AutoVersionsDB.Core.Validations.DBStateValidators;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class DBStateValidationsFactory : ValidationsFactory
    {
        public override string ValidationName => "DB State";

        public override ValidationsGroup Create(ProjectConfigItem projectConfig, AutoVersionsDbProcessContext processContext)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));
            processContext.ThrowIfNull(nameof(processContext));

            ValidationsGroup validationsGroup = new ValidationsGroup(false);

            IsHistoryExecutedFilesChangedValidator isHistoryExecutedFilesChangedValidator = new IsHistoryExecutedFilesChangedValidator(processContext.ScriptFilesState);
            validationsGroup.Add(isHistoryExecutedFilesChangedValidator);

            return validationsGroup;
        }


    }
}
