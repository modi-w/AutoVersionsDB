using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.Core.Validations.DBStateValidators;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class DBStateValidationsFactory : ValidationsFactory
    {
        public override string ValidationName => "DB State";

        public override ValidationsGroup Create(ProcessContext processContext)
        {
            ProjectConfigItem projectConfig = (processContext as IProjectConfigable).ProjectConfig;


            ValidationsGroup validationsGroup = new ValidationsGroup(false);

            DBVersionsProcessContext dbVersionsProcessContext = processContext as DBVersionsProcessContext;

            IsHistoryExecutedFilesChangedValidator isHistoryExecutedFilesChangedValidator = 
                new IsHistoryExecutedFilesChangedValidator(dbVersionsProcessContext.ScriptFilesState);
            validationsGroup.Add(isHistoryExecutedFilesChangedValidator);

            return validationsGroup;
        }


    }
}
