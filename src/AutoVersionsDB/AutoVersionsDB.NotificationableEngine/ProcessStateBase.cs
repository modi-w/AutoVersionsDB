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
        public Dictionary<string, string> EngineMetaData { get; set; }

        public ExecutionParams ExecutionParams { get; set; }

        public DateTime? StartProcessDateTime { get; set; }
        public DateTime? EndProcessDateTime { get; set; }

        public abstract bool CanRollback { get; }

    }

}
