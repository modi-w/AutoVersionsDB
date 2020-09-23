using System;

namespace AutoVersionsDB.NotificationableEngine
{
    public abstract class ProcessContext
    {
        public ProcessDefinition ProcessDefinition { get; internal set; }
        public ProcessParams ProcessParams { get; set; }
        public object Results { get; set; }

        public DateTime? StartProcessDateTime { get; set; }
        public DateTime? EndProcessDateTime { get; set; }

        public double ProcessDurationInMs
        {
            get
            {
                double results = 0;

                if (StartProcessDateTime.HasValue)
                {
                    if (EndProcessDateTime.HasValue)
                    {
                        results = (EndProcessDateTime.Value - StartProcessDateTime.Value).TotalMilliseconds;
                    }
                    else
                    {
                        results = (DateTime.Now - StartProcessDateTime.Value).TotalMilliseconds;
                    }
                }

                return results;
            }
        }


        public abstract bool CanRollback { get; }

        public bool IsRollbackExecuted { get; internal set; }


        public ProcessContext()
        {

        }

      
    }

}
