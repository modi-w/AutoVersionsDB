using AutoVersionsDB.NotificationableEngine.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public abstract class ProcessStateBase
    {
        public EngineSettings EngineSettings { get; private set; }

        public ExecutionParams ExecutionParams { get; set; }

        public DateTime? StartProcessDateTime { get; set; }
        public DateTime? EndProcessDateTime { get; set; }

        public abstract bool CanRollback { get; }

        public bool IsRollbackExecuted { get; internal set; }


        public ProcessStateBase()
        {
        }

        internal void SetEngineSettings(EngineSettings engineSettings)
        {
            engineSettings.ThrowIfNull(nameof(engineSettings));

            EngineSettings = engineSettings;

        }
    }

}
