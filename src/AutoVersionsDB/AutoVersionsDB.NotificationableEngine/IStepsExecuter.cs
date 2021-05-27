using System.Collections.Generic;

namespace AutoVersionsDB.NotificationableEngine
{

    internal interface IStepsExecuter
    {
        void ExecuteSteps(IEnumerable<ActionStepBase> steps, bool isContinueOnError);
    }


}


