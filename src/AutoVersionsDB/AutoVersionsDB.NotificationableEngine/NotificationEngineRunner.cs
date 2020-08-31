using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace AutoVersionsDB.NotificationableEngine
{
    public sealed class NotificationEngineRunner<TEngineSettings,TEngineContext>
        where TEngineSettings: EngineSettings
        where TEngineContext : EngineContext, new()
    {
        private TEngineSettings _engineSettings;

        public NotificationEngineRunner(TEngineSettings engineSettings)
        {
            _engineSettings = engineSettings;
        }


        public ProcessTrace Run(ExecutionParams executionParams, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            ProcessTrace results;

            using (NotificationEngine engine = Create())
            {
                results = engine.Run(executionParams, onNotificationStateChanged);
            }

            return results;
        }


        private NotificationEngine Create()
        {
            ProcessTraceHandler processTraceHandler = new ProcessTraceHandler();
            TEngineContext engineContext = new TEngineContext();
            engineContext.EngineSettings = _engineSettings;

            NotificationEngine engine = new NotificationEngine(processTraceHandler, engineContext);

            return engine;
        }
    }
}
