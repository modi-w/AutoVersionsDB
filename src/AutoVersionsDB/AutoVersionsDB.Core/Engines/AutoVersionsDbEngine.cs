using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;


namespace AutoVersionsDB.Core.Engines
{
    public class AutoVersionsDbEngine<TEngineSettings> : NotificationEngine<TEngineSettings, AutoVersionsDbProcessState, AutoVersionsDBExecutionParams>
        where TEngineSettings : AutoVersionsDbEngineSettingBase
    {


        public AutoVersionsDbEngine(TEngineSettings engineSettings,
                                    ProcessTraceStateChangeHandler processStateChangeHandler,
                                    StepsExecuter stepsExecuter)
            : base(engineSettings, processStateChangeHandler, stepsExecuter)
        {

        }



    }
}
