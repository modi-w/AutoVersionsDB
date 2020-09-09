using System;

namespace AutoVersionsDB.NotificationableEngine
{
    public abstract class ProcessContext
    {
        public ProcessDefinition ProcessDefinition { get; internal set; }

        public ProcessParams ProcessParams { get; set; }

        public DateTime? StartProcessDateTime { get; set; }
        public DateTime? EndProcessDateTime { get; set; }

        public abstract bool CanRollback { get; }

        public bool IsRollbackExecuted { get; internal set; }


        public ProcessContext()
        {

        }

      
    }

}
