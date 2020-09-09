using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace AutoVersionsDB.NotificationableEngine
{
    public sealed class NotificationProcessRunner<TProcessDefinition, TProcessContext>
        where TProcessDefinition: ProcessDefinition
        where TProcessContext : ProcessContext, new()
    {
        private TProcessDefinition _processDefinition;

        public NotificationProcessRunner(TProcessDefinition processDefinition)
        {
            _processDefinition = processDefinition;
        }


        public ProcessTrace Run(ProcessParams processParams, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            ProcessTrace results;

            using (NotificationEngine engine = Create())
            {
                results = engine.Run(processParams, onNotificationStateChanged);
            }

            return results;
        }


        private NotificationEngine Create()
        {
            ProcessTraceHandler processTraceHandler = new ProcessTraceHandler();
            TProcessContext processContext = new TProcessContext();
            processContext.ProcessDefinition = _processDefinition;

            NotificationEngine engine = new NotificationEngine(processTraceHandler, processContext);

            return engine;
        }
    }
}
